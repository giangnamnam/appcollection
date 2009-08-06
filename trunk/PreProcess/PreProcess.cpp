// PreProcess.cpp : Defines the exported functions for the DLL application.
#include "stdafx.h"
#include "PreProcess.h" 
//#include <Strsafe.h>

Frame LastFrame ; 

//int Index = 0;
//int Index1 = 0;
int ImageCount = 0;//ImageFrame的中image的个数
//int ImageCount_1 = 0;//备份ImageFrame的中image的个数

int CallTime = 0;//定义调用次数计数器 
int temp1 = 0;//用于标示分组结束的标志
int temp2 = 0;//用于标示分组结束的标志
//bool FirstLoop = false; 

IplImage* CurrentImage;
IplImage* BackGroundImage;//上一帧灰度图
IplImage* DiffImage_1;//上一帧差分图的二值化图

bool drawRect = false;

//每次从摄像头获得一张图片后调用，当完成一个分组后返回true
PREPROCESS_API bool PreProcessFrame(Frame frame, Frame *lastFrame)
{
	Frame TempFrame;
	TempFrame = LastFrame;

	CallTime++;
	if(CallTime > 10)//防止溢出
	{
		CallTime = 10;
	}

	CurrentImage = frame.image; 

	CvSize ImageSize = cvSize(CurrentImage->width,CurrentImage->height);
	IplImage* GrayImage = cvCreateImage(ImageSize,IPL_DEPTH_8U,1);//当前帧的灰度图
	IplImage* GxImage = cvCreateImage(ImageSize,IPL_DEPTH_8U,1);//当前帧的X方向梯度图
	IplImage* GyImage = cvCreateImage(ImageSize,IPL_DEPTH_8U,1);//当前帧的Y方向梯度图
	IplImage* DiffImage = cvCreateImage(ImageSize,IPL_DEPTH_8U,1);//当前帧的差分图
	IplImage* DiffImage_2 = cvCreateImage(ImageSize,IPL_DEPTH_8U,1);//前一帧差分图
	IplImage* pyr = cvCreateImage(cvSize((ImageSize.width&-2)/2,(ImageSize.height&-2)/2),8,1); //进行腐蚀去除噪声的中间临时图片

	uchar* DiffImageData_2;
	DiffImageData_2 = (uchar*)DiffImage_2->imageData;//得到前一帧差分图的数据

	int height,width,step;//定义图像的高，宽，步长
	int SumInRect = 0;//指定矩形内图像数据之和
	int y1, y2, x1, x2;//对运动目标画框时的四个坐标点位置
	y1 = 0;
	y2 = 0;
	x1 = 0;
	x2 = 0;

	char Kx[9] = {1,0,-1,2,0,-2,1,0,-1};//X方向掩模，用于得到X方向梯度图
	char Ky[9] = {1,2,1,0,0,0,-1,-2,-1};//Y方向掩模，用于得到Y方向梯度图
	CvMat KX,KY;
	KX = cvMat(3,3,CV_8S,Kx);//构建掩模内核
	KY = cvMat(3,3,CV_8S,Ky);//构建掩模内核

	cvCvtColor(CurrentImage,GrayImage,CV_BGR2GRAY);//将当前帧转化为灰度图
	cvSmooth(GrayImage,GrayImage,CV_GAUSSIAN,7,7);//进行平滑处理
	cvFilter2D(GrayImage,GxImage,&KX,cvPoint(-1,-1));//得到X方向的梯度图
	cvFilter2D(GrayImage,GyImage,&KY,cvPoint(-1,-1));//得到Y方向的梯度图
	cvAdd(GxImage,GyImage,GrayImage,NULL);//得到梯度图

	height = GrayImage->height;
	width = GrayImage->width;
	step = GrayImage->widthStep;

	CvRect rect;//定义矩形框

	if(CallTime == 1)//如果是第一帧
	{
		BackGroundImage = cvCreateImage(ImageSize,IPL_DEPTH_8U,1);
		DiffImage_1 = cvCreateImage(ImageSize,IPL_DEPTH_8U,1);
		cvCopy(GrayImage,BackGroundImage,NULL);//如果是第一帧，设置为背景
	}
	else
	{
		cvAbsDiff(GrayImage,BackGroundImage,DiffImage);//得到当前帧的差分图
		cvCopy(GrayImage,BackGroundImage,NULL);//将当前帧的梯度图作为下一帧的背景
		cvThreshold(DiffImage,DiffImage,15,255,CV_THRESH_BINARY);//二值化当前差分图
		if(CallTime > 2)//如果大于等于第三帧
		{
			cvAnd(DiffImage,DiffImage_1,DiffImage_2);//进行“与”运算，得到前一帧灰度图的“准确”运动目标
		}
		cvPyrDown(DiffImage_2,pyr,7);//向下采样
		cvErode(pyr,pyr,0,1);//腐蚀，消除小的噪声
		cvPyrUp(pyr,DiffImage_2,7);
		cvCopy(DiffImage,DiffImage_1,NULL);//备份当前差分图的二值化图

		//#pragma omp parallel
		for(int i=0;i<height-200;i+=20)
		{
			for(int j=0;j<width-200;j+=20)
			{
				for(int m =i;m<i+200;m++)
					for(int n=j;n<j+200;n++)
						SumInRect += DiffImageData_2[m*step+n];
				if(SumInRect > 1250*255)
				{
					if(y1 == 0)
					{
						y1 = i;
						y2 = i + 200;
						x1 = j;
						x2 = j + 200;
					}
					else if(i > y2)
					{
						y2 = i + 200;
					}
					else if(j < x1)
					{
						x1 = j;
					}
					else if(j > x2)
					{
						x2 = j + 200;
					}
				}
				SumInRect = 0;
			}
		}
		if((x2!= 0) && (x2+100 < width))//稍微扩大搜索到的矩形范围，减少误差
		{
			x2 += 100; 
		}
		if((y2!=0) && (y2+100 < height))
		{
			y2 += 100;
		}

	}

	if(y2 != 0)//如果检测到运动目标，则，画框，保存结构体
	{
		if (drawRect) cvRectangle(TempFrame.image,cvPoint(x1,y1),cvPoint(x2,y2),CV_RGB(255,0,0),3,CV_AA,0);  
		
		rect = cvRect(x1,y1,x2-x1,y2-y1);
		
		TempFrame.searchRect = rect;
		//TempFrame.groupCaptured = false;

		if((y1 < 360) && ((x2-x1) < 450))//如果连续检测到框为单人大小，则自动划分为一组
		{
			if(temp1 == 0)//如果初始时刻检测到单人
			{
				temp1 = temp2 = 1;
			}
			temp1 = temp2;
			temp2 = 1;
		}
		else 
		{
			if(temp1 == 0)//如果初始时刻检测到多人
			{
				temp1 = temp2 = 2; 
			}
			temp1 = temp2;
			temp2 = 2;
		} 
	}
	else //当前帧没检测到
	{
		if(temp1 == 0)//如果初始时刻没检测到人
		{
			temp1 = temp2 = 3;
		}
		temp1 = temp2 ;
		temp2 = 3;
	}

	cvReleaseImage(&GxImage);
	cvReleaseImage(&GyImage);
	cvReleaseImage(&GrayImage);
	cvReleaseImage(&DiffImage);
	cvReleaseImage(&DiffImage_2);
	cvReleaseImage(&pyr); 

	LastFrame = frame;
	*lastFrame = TempFrame;

	if((((temp1 != temp2) && temp2 !=3) || ((temp1 !=3) && (temp2 == 3))))//若完成一个分组，则返回true
	{
		return true;
	}

	return false;
}


void SetDrawRect(bool draw)
{
	drawRect = draw;
}



