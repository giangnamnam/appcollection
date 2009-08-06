// PreProcess.cpp : Defines the exported functions for the DLL application.
#include "stdafx.h"
#include "PreProcess.h" 
//#include <Strsafe.h>

Frame LastFrame ; 

//int Index = 0;
//int Index1 = 0;
int ImageCount = 0;//ImageFrame����image�ĸ���
//int ImageCount_1 = 0;//����ImageFrame����image�ĸ���

int CallTime = 0;//������ô��������� 
int temp1 = 0;//���ڱ�ʾ��������ı�־
int temp2 = 0;//���ڱ�ʾ��������ı�־
//bool FirstLoop = false; 

IplImage* CurrentImage;
IplImage* BackGroundImage;//��һ֡�Ҷ�ͼ
IplImage* DiffImage_1;//��һ֡���ͼ�Ķ�ֵ��ͼ

bool drawRect = false;

//ÿ�δ�����ͷ���һ��ͼƬ����ã������һ������󷵻�true
PREPROCESS_API bool PreProcessFrame(Frame frame, Frame *lastFrame)
{
	Frame TempFrame;
	TempFrame = LastFrame;

	CallTime++;
	if(CallTime > 10)//��ֹ���
	{
		CallTime = 10;
	}

	CurrentImage = frame.image; 

	CvSize ImageSize = cvSize(CurrentImage->width,CurrentImage->height);
	IplImage* GrayImage = cvCreateImage(ImageSize,IPL_DEPTH_8U,1);//��ǰ֡�ĻҶ�ͼ
	IplImage* GxImage = cvCreateImage(ImageSize,IPL_DEPTH_8U,1);//��ǰ֡��X�����ݶ�ͼ
	IplImage* GyImage = cvCreateImage(ImageSize,IPL_DEPTH_8U,1);//��ǰ֡��Y�����ݶ�ͼ
	IplImage* DiffImage = cvCreateImage(ImageSize,IPL_DEPTH_8U,1);//��ǰ֡�Ĳ��ͼ
	IplImage* DiffImage_2 = cvCreateImage(ImageSize,IPL_DEPTH_8U,1);//ǰһ֡���ͼ
	IplImage* pyr = cvCreateImage(cvSize((ImageSize.width&-2)/2,(ImageSize.height&-2)/2),8,1); //���и�ʴȥ���������м���ʱͼƬ

	uchar* DiffImageData_2;
	DiffImageData_2 = (uchar*)DiffImage_2->imageData;//�õ�ǰһ֡���ͼ������

	int height,width,step;//����ͼ��ĸߣ�������
	int SumInRect = 0;//ָ��������ͼ������֮��
	int y1, y2, x1, x2;//���˶�Ŀ�껭��ʱ���ĸ������λ��
	y1 = 0;
	y2 = 0;
	x1 = 0;
	x2 = 0;

	char Kx[9] = {1,0,-1,2,0,-2,1,0,-1};//X������ģ�����ڵõ�X�����ݶ�ͼ
	char Ky[9] = {1,2,1,0,0,0,-1,-2,-1};//Y������ģ�����ڵõ�Y�����ݶ�ͼ
	CvMat KX,KY;
	KX = cvMat(3,3,CV_8S,Kx);//������ģ�ں�
	KY = cvMat(3,3,CV_8S,Ky);//������ģ�ں�

	cvCvtColor(CurrentImage,GrayImage,CV_BGR2GRAY);//����ǰ֡ת��Ϊ�Ҷ�ͼ
	cvSmooth(GrayImage,GrayImage,CV_GAUSSIAN,7,7);//����ƽ������
	cvFilter2D(GrayImage,GxImage,&KX,cvPoint(-1,-1));//�õ�X������ݶ�ͼ
	cvFilter2D(GrayImage,GyImage,&KY,cvPoint(-1,-1));//�õ�Y������ݶ�ͼ
	cvAdd(GxImage,GyImage,GrayImage,NULL);//�õ��ݶ�ͼ

	height = GrayImage->height;
	width = GrayImage->width;
	step = GrayImage->widthStep;

	CvRect rect;//������ο�

	if(CallTime == 1)//����ǵ�һ֡
	{
		BackGroundImage = cvCreateImage(ImageSize,IPL_DEPTH_8U,1);
		DiffImage_1 = cvCreateImage(ImageSize,IPL_DEPTH_8U,1);
		cvCopy(GrayImage,BackGroundImage,NULL);//����ǵ�һ֡������Ϊ����
	}
	else
	{
		cvAbsDiff(GrayImage,BackGroundImage,DiffImage);//�õ���ǰ֡�Ĳ��ͼ
		cvCopy(GrayImage,BackGroundImage,NULL);//����ǰ֡���ݶ�ͼ��Ϊ��һ֡�ı���
		cvThreshold(DiffImage,DiffImage,15,255,CV_THRESH_BINARY);//��ֵ����ǰ���ͼ
		if(CallTime > 2)//������ڵ��ڵ���֡
		{
			cvAnd(DiffImage,DiffImage_1,DiffImage_2);//���С��롱���㣬�õ�ǰһ֡�Ҷ�ͼ�ġ�׼ȷ���˶�Ŀ��
		}
		cvPyrDown(DiffImage_2,pyr,7);//���²���
		cvErode(pyr,pyr,0,1);//��ʴ������С������
		cvPyrUp(pyr,DiffImage_2,7);
		cvCopy(DiffImage,DiffImage_1,NULL);//���ݵ�ǰ���ͼ�Ķ�ֵ��ͼ

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
		if((x2!= 0) && (x2+100 < width))//��΢�����������ľ��η�Χ���������
		{
			x2 += 100; 
		}
		if((y2!=0) && (y2+100 < height))
		{
			y2 += 100;
		}

	}

	if(y2 != 0)//�����⵽�˶�Ŀ�꣬�򣬻��򣬱���ṹ��
	{
		if (drawRect) cvRectangle(TempFrame.image,cvPoint(x1,y1),cvPoint(x2,y2),CV_RGB(255,0,0),3,CV_AA,0);  
		
		rect = cvRect(x1,y1,x2-x1,y2-y1);
		
		TempFrame.searchRect = rect;
		//TempFrame.groupCaptured = false;

		if((y1 < 360) && ((x2-x1) < 450))//���������⵽��Ϊ���˴�С�����Զ�����Ϊһ��
		{
			if(temp1 == 0)//�����ʼʱ�̼�⵽����
			{
				temp1 = temp2 = 1;
			}
			temp1 = temp2;
			temp2 = 1;
		}
		else 
		{
			if(temp1 == 0)//�����ʼʱ�̼�⵽����
			{
				temp1 = temp2 = 2; 
			}
			temp1 = temp2;
			temp2 = 2;
		} 
	}
	else //��ǰ֡û��⵽
	{
		if(temp1 == 0)//�����ʼʱ��û��⵽��
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

	if((((temp1 != temp2) && temp2 !=3) || ((temp1 !=3) && (temp2 == 3))))//�����һ�����飬�򷵻�true
	{
		return true;
	}

	return false;
}


void SetDrawRect(bool draw)
{
	drawRect = draw;
}



