// PreProcess.cpp : Defines the exported functions for the DLL application.
//1/32区域

/************************************************************************************************************
Copyright (c) 2009, 成都精识智能技术有限公司
All right reserved!

文件名：PreProcess.cpp
摘  要：该程序，对每一帧输入图片，利用三帧差分法，在指定警戒区域内进行运动目标检测。当警戒区域内检测到运动目标
        时，在全图范围内进行运动目标标示，即：画框，并将框的坐标点予以返回。最后，对有运动目标的图片，进行分组
		当分组结束时，返回true,否则，返回false

当前版本：3.1
作    者：薛晓利
完成日期：2009年8月11日  
*************************************************************************************************************/
#include "stdafx.h"
#include "PreProcess.h" 
#include "iostream"

Frame LastFrame ;  

int temp1 = 0;//用于标示分组结束的标志
int temp2 = 0;//用于标示分组结束的标志 

int signelCount = 0;

bool firstFrameRec = false;//第一帧是否收到
bool secondFrameRec = false;//第二帧是否收到

IplImage *currentImage;
IplImage *lastGrayImage;//上一帧灰度图
IplImage *lastDiffImage;//上一帧差分图的二值化图

bool drawRect = false; //标志是否画框

int xLeftAlarm = 100; //定义警戒区域的两个坐标点位置
int yTopAlarm = 400;
int xRightAlarm = 1000;
int yBottomAlarm = 500;

int faceCount = 1200; 
int groupCount = 5;

//计算选定区域内像素点的数值之和,并将数值返回
int RegionSum(const int left_x, const int left_y, const int right_x, const int right_y, IplImage *img)
{
	uchar *data = (uchar*)img->imageData;
	int step = img->widthStep;
	int sum = 0;
	for (int i=left_y; i<right_y; i++)
	{
		for (int j=left_x; j<right_x; j++)
		{
			sum += data[i*step+j];
		}
	}
	return sum;
}

//判断警戒区域内是否有运动目标,有运动目标返回true,否则，返回false
bool AlarmArea(const int x_left, const int y_top, const int x_right, const int y_bottom, IplImage *lastDiffImg) 
{
	int step = lastDiffImg->widthStep;
	uchar *lastDiffData = (uchar*)lastDiffImg->imageData;
	int area = (x_right-x_left)*(y_bottom-y_top);
	int lastDiffCount = 0;

	for (int i=y_top; i<y_bottom; i++)
	{
		for (int j=x_left; j<x_right; j++)
		{
			if (lastDiffData[i*step+j] > 10)
			{
				lastDiffCount++;
			}
		}
	}
	if (lastDiffCount > 0.01*area)
	{
		return true;
	}
	else
	{
		return false;
	}
}

void ReadImgPoint()
{
	FILE *f;
	char str[50]; 
	int bk1 = 0;
	int bk2 = 0;
	int bk3 = 0; 
	int bk4 = 0; 

	f = fopen("C:/ImgPoint.txt", "r");//打开文件，准备读入警戒区域的坐标位置 

	while(!feof(f))
	{
		fgets(str, 50, f);//将文件中的内容读入字符串
	}

	for (int i=0; i<50; i++)//找到四个空格的位置
	{
		if (str[i] == ' ')
		{
			if (bk1 == 0)
			{
				bk1 = i;
			}
			else if (bk2 == 0)
			{
				bk2 = i;
			}
			else if (bk3 == 0)
			{
				bk3 = i;
			}
			else if (bk4 == 0)
			{
				bk4 = i;
			}
		}
	}
	char *data1 = new char[bk1];
	char *data2 = new char[bk2-bk1-1];
	char *data3 = new char[bk3-bk2-1];
	char *data4 = new char[bk4-bk3-1];

	for (int i=0; i<bk1; i++)//将四个空格隔开的数据读入字符串
	{
		data1[i] = str[i];
	}
	int j=0;
	for (int i=bk1+1; i<bk2; i++)
	{
		data2[j] = str[i];
		j++;
	}
	j=0;
	for (int i=bk2+1; i<bk3; i++)
	{
		data3[j] = str[i];
		j++;
	}
	j = 0;
	for (int i=bk3+1; i<bk4; i++)
	{
		data4[j] = str[i];
		j++;
	}
	j = 0;

	xLeftAlarm = atoi(data1);//将四个字符串转换为数值
	yTopAlarm = atoi(data2);
	xRightAlarm = atoi(data3);
	yBottomAlarm = atoi(data4);  

	delete []data1;
	delete []data2;
	delete []data3; 
	delete []data4;

	fclose(f);
}

void ReadPreProcess()
{
	FILE *f;
	char str[50]; 
	int bk1 = 0;
	int bk2 = 0;

	f = fopen("C:/PreProcess.txt", "r");//打开文件，准备读入警戒区域的坐标位置 

	while(!feof(f))
	{
		fgets(str, 50, f);//将文件中的内容读入字符串
	}

	for (int i=0; i<50; i++)//找到四个空格的位置
	{
		if (str[i] == ' ')
		{
			if (bk1 == 0)
			{
				bk1 = i;
			}
			else if (bk2 == 0)
			{
				bk2 = i;
			}
		}
	}

	char *data1 = new char[bk1];
	char *data2 = new char[bk2-bk1-1];

	for (int i=0; i<bk1; i++)//将四个空格隔开的数据读入字符串
	{
		data1[i] = str[i];
	}
	int j=0;
	for (int i=bk1+1; i<bk2; i++)
	{
		data2[j] = str[i];
		j++;
	}
	j=0;

	faceCount = atoi(data1);
	groupCount = atoi(data2);

	delete []data1;
	delete []data2;

	fclose(f);
}

//每次从摄像头获得一张图片后调用，当完成一个分组后返回true, 分组没结束，则返回false
PREPROCESS_API bool PreProcessFrame(Frame frame, Frame *lastFrame)
{
	Frame TempFrame;
	TempFrame = LastFrame;

	currentImage = frame.image; 

	CvSize ImageSize = cvSize(currentImage->width,currentImage->height);
	IplImage *GrayImage = cvCreateImage(ImageSize,IPL_DEPTH_8U,1);//当前帧的灰度图
	IplImage *GxImage = cvCreateImage(ImageSize,IPL_DEPTH_8U,1);//当前帧的X方向梯度图
	IplImage *GyImage = cvCreateImage(ImageSize,IPL_DEPTH_8U,1);//当前帧的Y方向梯度图
	IplImage *DiffImage = cvCreateImage(ImageSize,IPL_DEPTH_8U,1);//当前帧的差分图
	IplImage *DiffImage_2 = cvCreateImage(ImageSize,IPL_DEPTH_8U,1);//前一帧差分图
	IplImage *pyr = cvCreateImage(cvSize((ImageSize.width&-2)/2,(ImageSize.height&-2)/2),8,1); //进行腐蚀去除噪声的中间临时图片

	uchar *DiffImageData_2;
	DiffImageData_2 = (uchar*)DiffImage_2->imageData;//得到前一帧差分图的数据

	int height,width,step;//定义图像的高，宽，步长
	int SumInRect = 0;//指定矩形内图像数据之和
	int y1, y2, x1, x2;//对运动目标画框时的四个坐标点位置
	y1 = 0;
	y2 = 0;
	x1 = 0; 
	x2 = 0;

	/*int threshold_4 = faceCount*255/4;
	int threshold_16 = faceCount*255/16;
	int threshold_32 = faceCount*255/32;*/ 

	char Kx[9] = {1,0,-1,2,0,-2,1,0,-1};//X方向掩模，用于得到X方向梯度图
	char Ky[9] = {1,2,1,0,0,0,-1,-2,-1};//Y方向掩模，用于得到Y方向梯度图
	CvMat KX,KY;
	KX = cvMat(3,3,CV_8S,Kx);//构建掩模内核
	KY = cvMat(3,3,CV_8S,Ky);//构建掩模内核

	cvCvtColor(currentImage,GrayImage,CV_BGR2GRAY);
	cvSmooth(GrayImage,GrayImage,CV_GAUSSIAN,7,7);//进行平滑处理
	cvFilter2D(GrayImage,GxImage,&KX,cvPoint(-1,-1));//得到X方向的梯度图
	cvFilter2D(GrayImage,GyImage,&KY,cvPoint(-1,-1));//得到Y方向的梯度图
	cvAdd(GxImage,GyImage,GrayImage,NULL);//得到梯度图

	cvReleaseImage(&GxImage);
	cvReleaseImage(&GyImage);

	height = GrayImage->height;
	width = GrayImage->width; 
	step = GrayImage->widthStep;

	//int left_x = width;//定义运动目标框的两个坐标点位置
	//int left_y = height;
	//int right_x = 0;
	//int right_y = 0; 

	bool alarm = false;//警戒区域是否有运动目标   

	CvRect rect;//定义矩形框

	if(!firstFrameRec)//如果是第一帧
	{
		firstFrameRec = true;
		lastGrayImage = cvCreateImage(ImageSize,IPL_DEPTH_8U,1);
		lastDiffImage = cvCreateImage(ImageSize,IPL_DEPTH_8U,1);
		cvCopy(GrayImage,lastGrayImage,NULL);//如果是第一帧，设置为背景

		ReadImgPoint();//读入布控选择的警戒区域位置
		ReadPreProcess();

		//防止左上角坐标超出范围
		xLeftAlarm = xLeftAlarm>0 ? xLeftAlarm:0;
		xLeftAlarm = xLeftAlarm>(width-10) ? (width-10):xLeftAlarm;
		yTopAlarm = yTopAlarm>0 ? yTopAlarm:0;
		yTopAlarm = yTopAlarm>(height-10) ? (height-10):yTopAlarm;

		//防止右下角坐标超出范围
		xRightAlarm = xRightAlarm>(width-8) ? (width-8):xRightAlarm;
		xRightAlarm = xRightAlarm>xLeftAlarm ? xRightAlarm:(xRightAlarm+1);
		yBottomAlarm = yBottomAlarm>(height-8) ? (height-8):yBottomAlarm;
		yBottomAlarm = yBottomAlarm>yTopAlarm ? yBottomAlarm:(yTopAlarm+1);

	}
	else
	{
		cvAbsDiff(GrayImage,lastGrayImage,DiffImage);//得到当前帧的差分图
		cvCopy(GrayImage,lastGrayImage,NULL);//将当前帧的梯度图作为下一帧的背景
		cvThreshold(DiffImage,DiffImage,15,255,CV_THRESH_BINARY);//二值化当前差分图
		if(secondFrameRec)//如果大于等于第三帧
		{
			cvAnd(DiffImage,lastDiffImage,DiffImage_2);//进行“与”运算，得到前一帧灰度图的“准确”运动目标
			cvPyrDown(DiffImage_2,pyr,7);//向下采样
			cvErode(pyr,pyr,0,1);//腐蚀，消除小的噪声
			cvPyrUp(pyr,DiffImage_2,7);
			cvReleaseImage(&pyr);

			//cvRectangle(TempFrame.image, cvPoint(xLeftAlarm, yTopAlarm), cvPoint(xRightAlarm, yBottomAlarm), CV_RGB(0,255,0),1, CV_AA, 0);
			alarm = AlarmArea(xLeftAlarm, yTopAlarm, xRightAlarm, yBottomAlarm, DiffImage_2); 
		}
		 
		cvCopy(DiffImage,lastDiffImage,NULL);//备份当前差分图的二值化图

		cvReleaseImage(&DiffImage); 


		//SumInRect = RegionSum(0, 0, width, height, DiffImage_2);
		if (alarm)//若检测出整个图片有运动目标
		{
			for(int i=0;i<height-200;i+=20)
			{
				for(int j=0;j<width-200;j+=20)
				{
					for(int m =i;m<i+200;m++)
						for(int n=j;n<j+200;n++)
							SumInRect += DiffImageData_2[m*step+n];
					if(SumInRect > faceCount*255)
					{
						if(y1 == 0)
						{
							y1 = i;
							y2 = (i + 200)>height ? height:(i+200);
							x1 = j;
							x2 = (j + 200)>width ? width:(j+200);
						}
						else if(i > y2)
						{
							y2 = (i + 200)>height ? height:(i+200);
						}
						else if(j < x1)
						{
							x1 = j;
						}
						else if(j > x2)
						{
							x2 = (j + 200)>width ? width:(j+200);
						}
					}
					SumInRect = 0; 
				}
			}
		}

		if (!secondFrameRec)//设置第二帧已经收到
		{
			secondFrameRec = true; 
		}

	}

	if(y2 != 0)//如果当前帧检测到运动目标，则，画框,分组
	{
		x2 = (x2+100)>width ? width:(x2+100);
		y2 = (y2+100)>height ? height:(y2+100); 
		//y1 = (y1-100)>0 ? (y1-100):0;  
		if (drawRect) cvRectangle(TempFrame.image,cvPoint(x1,y1),cvPoint(x2,y2),CV_RGB(255,0,0),3,CV_AA,0); 

		rect = cvRect(x1,y1,x2-x1,y2-y1); 
		TempFrame.searchRect = rect;

		if((y1 < 360) && ((x2-x1) < 400))//如果检测到框为单人大小
		{
				signelCount++; 
				cvReleaseImage(&GrayImage);
				cvReleaseImage(&DiffImage_2);
				LastFrame = frame;
				*lastFrame = TempFrame;

				if(signelCount == groupCount)//如果连续检测到5个单人的情况，分组结束 
				{
					signelCount = 0;
					return true;
				}
				else
				{
					return false;
				}	
		}
		else //如果检测到多人情况，每张图片分为一组
		{
			signelCount = 0;
			cvReleaseImage(&GrayImage);
			cvReleaseImage(&DiffImage_2);
			LastFrame = frame;
			*lastFrame = TempFrame;
			return true;
		} 
	}
	else //当前帧没检测到
	{ 
		cvReleaseImage(&GrayImage);
		cvReleaseImage(&DiffImage_2);
		LastFrame = frame;
		*lastFrame = TempFrame;

		if (signelCount > 0)//如果前一帧为单人，当前帧没有人
		{
			signelCount = 0;
			return true;
		}
		else
		{
			return false;
		}
	}

}

void SetDrawRect(bool draw)
{
	drawRect = draw;
}



