// PreProcess.cpp : Defines the exported functions for the DLL application.
/************************************************************************************************************
Copyright (c) 2009, �ɶ���ʶ���ܼ������޹�˾
All right reserved!

�ļ�����PreProcess.cpp
ժ  Ҫ���ó��򣬶�ÿһ֡����ͼƬ��������֡��ַ�����ָ�����������ڽ����˶�Ŀ���⡣�����������ڼ�⵽�˶�Ŀ��
ʱ����ȫͼ��Χ�ڽ����˶�Ŀ���ʾ���������򣬲��������������Է��ء���󣬶����˶�Ŀ���ͼƬ�����з���,
���������ʱ������true,���򣬷���false.���غͻ�����صĲ���ͨ��UI���ú�����ʵ��.

��ǰ�汾��3.2
��    �ߣ�Ѧ����
������ڣ�2009��8��31��  
*************************************************************************************************************/
#include "stdafx.h"
#include "PreProcess.h"  
//#include "iostream" 
//#include <fstream> 
//using namespace std; 

//ofstream outobj("C:\\rectPosition1.txt");    
//outobj<<"���ϽǺ�����"<<"���Ͻ�������"<<"���½Ǻ�����"<<"���½�������"<<endl; 

Frame LastFrame ;//�洢��һ֡��frame   

bool firstFrameRec = false;//��һ֡�Ƿ��յ�
bool secondFrameRec = false;//�ڶ�֡�Ƿ��յ�

IplImage *currentImage;//��ǰ֡��ͼƬ
IplImage *lastGrayImage;//��һ֡�Ҷ�ͼ
IplImage *lastDiffImage;//��һ֡���ͼ�Ķ�ֵ��ͼ

int xLeftAlarm = 100; //���岢��ʼ��������������������λ��
int yTopAlarm = 400;
int xRightAlarm = 600;
int yBottomAlarm = 500;  

int minLeftX = 3000;//���岢��ʼ���������������λ��
int minLeftY = 3000; 
int maxRightX = 0;
int maxRightY = 0;

int faceCount = 500; //�������ֵ
int groupCount = 5;//����ͼƬ����
int signelCount = 0;//��¼��ǰ�����е�ͼƬ����

bool drawAlarmArea = false;//��־�Ƿ񻭳���������
bool drawRect = false; //��־�Ƿ񻭿�

//��ʶ�Ƿ񻭳���ɫ�Ŀ��,ͨ��UI���趨
void SetDrawRect(bool draw)
{
	drawRect = draw;
}

//���ò��صľ�������ͨ��UI�����øú���;
void SetAlarmArea(const int leftX, const int leftY, const int rightX, const int rightY, bool draw)
{
	xLeftAlarm = leftX;
	yTopAlarm = leftY;
	xRightAlarm = rightX;
	yBottomAlarm = rightY; 
	drawAlarmArea = draw;//�����Ƿ񻭳��������򣬸�����Ϊ��ɫ�߿�
}

//�����˶����Ŀ������ֵ�ͷ��������ͨ��UI�����øú���
void SetRectThr(const int fCount, const int gCount)
{
	faceCount = fCount;
	groupCount = gCount;
}

//����ѡ�����������ص����ֵ֮��,������ֵ����,�ú�����PreProcessFrame�������� 
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

//�жϾ����������Ƿ����˶�Ŀ��,���˶�Ŀ�귵��true,���򣬷���false,�ú�����PreProcessFrame��������
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

//�ҵ���������������λ��,�ú�����PreProcessFrame��������
void FindRectX(IplImage *img, const int leftY, const int rightY)
{
	int count = (img->width - 100)/20 + 1;

	int *leftX = new int [count];
	int *rightX = new int [count];

	int sumInRect = 0;

	for (int i=0; i<count; i++)
	{
		if (i==0)
		{
			leftX[i] = 0;
			rightX[i] = 100;
		}
		else
		{
			leftX[i] = leftX[i-1] + 20;
			rightX[i] = rightX[i-1] + 20;
		}
		sumInRect = RegionSum(leftX[i], leftY, rightX[i], rightY, img);
		if (sumInRect > faceCount*255)
		{
			minLeftX = minLeftX<leftX[i] ? minLeftX:leftX[i];
			maxRightX = maxRightX>rightX[i] ? maxRightX:rightX[i];
		}
	}

	delete []leftX;
	delete []rightX;
}

//�ҵ���������������λ��,�ú�����PreProcessFrame��������
void FindRectY(IplImage *img, const int leftX, const int rightX)
{
	int count = (img->height - 100)/20 + 1;

	int *leftY = new int[count];
	int *rightY = new int[count];

	int sumInRect = 0;

	for (int i=0; i<count; i++)
	{
		if (i == 0)
		{
			leftY[i] = 0;
			rightY[i] = 100;
		}
		else
		{
			leftY[i] = leftY[i-1] + 20;
			rightY[i] = rightY[i-1] + 20;
		}
		sumInRect = RegionSum(leftX, leftY[i], rightX, rightY[i], img);  
		if (sumInRect > faceCount*255)
		{
			minLeftY = minLeftY<leftY[i] ? minLeftY:leftY[i];
			maxRightY = maxRightY>rightY[i] ? maxRightY:rightY[i]; 
		}
	}

	delete []leftY;
	delete []rightY; 
}

/*ÿ�δ�����ͷ���һ��ͼƬ����ã������˶�Ŀ���⣬������
  �����һ������󷵻�true, ����û�������򷵻�false,
  �ú�����UI������*/
PREPROCESS_API bool PreProcessFrame(Frame frame, Frame *lastFrame)
{
	Frame TempFrame;
	TempFrame = LastFrame; 

	currentImage = frame.image; 

	CvSize ImageSize = cvSize(currentImage->width,currentImage->height);
	IplImage *GrayImage = cvCreateImage(ImageSize,IPL_DEPTH_8U,1);//��ǰ֡�ĻҶ�ͼ
	IplImage *GxImage = cvCreateImage(ImageSize,IPL_DEPTH_8U,1);//��ǰ֡��X�����ݶ�ͼ
	IplImage *GyImage = cvCreateImage(ImageSize,IPL_DEPTH_8U,1);//��ǰ֡��Y�����ݶ�ͼ
	IplImage *DiffImage = cvCreateImage(ImageSize,IPL_DEPTH_8U,1);//��ǰ֡�Ĳ��ͼ
	IplImage *DiffImage_2 = cvCreateImage(ImageSize,IPL_DEPTH_8U,1);//ǰһ֡���ͼ
	IplImage *pyr = cvCreateImage(cvSize((ImageSize.width&-2)/2,(ImageSize.height&-2)/2),8,1); //���и�ʴȥ���������м���ʱͼƬ

	uchar *DiffImageData_2;
	DiffImageData_2 = (uchar*)DiffImage_2->imageData;//�õ�ǰһ֡���ͼ������

	int height,width;//����ͼ��ĸߣ�������

	char Kx[9] = {1,0,-1,2,0,-2,1,0,-1};//X������ģ�����ڵõ�X�����ݶ�ͼ
	char Ky[9] = {1,2,1,0,0,0,-1,-2,-1};//Y������ģ�����ڵõ�Y�����ݶ�ͼ
	CvMat KX,KY;
	KX = cvMat(3,3,CV_8S,Kx);//������ģ�ں�
	KY = cvMat(3,3,CV_8S,Ky);//������ģ�ں�

	cvCvtColor(currentImage,GrayImage,CV_BGR2GRAY);
	cvSmooth(GrayImage,GrayImage,CV_GAUSSIAN,7,7);//����ƽ������
	cvFilter2D(GrayImage,GxImage,&KX,cvPoint(-1,-1));//�õ�X������ݶ�ͼ
	cvFilter2D(GrayImage,GyImage,&KY,cvPoint(-1,-1));//�õ�Y������ݶ�ͼ
	cvAdd(GxImage,GyImage,GrayImage,NULL);//�õ��ݶ�ͼ

	cvReleaseImage(&GxImage);
	cvReleaseImage(&GyImage);

	height = GrayImage->height;
	width = GrayImage->width; 

	bool alarm = false;//���������Ƿ����˶�Ŀ��   

	CvRect rect;//������ο�

	if(!firstFrameRec)//����ǵ�һ֡
	{
		firstFrameRec = true; 
		lastGrayImage = cvCreateImage(ImageSize,IPL_DEPTH_8U,1);
		lastDiffImage = cvCreateImage(ImageSize,IPL_DEPTH_8U,1);
		cvCopy(GrayImage,lastGrayImage,NULL);//����ǵ�һ֡������Ϊ���� 

		xRightAlarm = currentImage->width - 100;
		yBottomAlarm = currentImage->height - 100;
		yTopAlarm = currentImage->height - 200;
	}
	else
	{
		cvAbsDiff(GrayImage,lastGrayImage,DiffImage);//�õ���ǰ֡�Ĳ��ͼ
		cvCopy(GrayImage,lastGrayImage,NULL);//����ǰ֡���ݶ�ͼ��Ϊ��һ֡�ı���
		cvThreshold(DiffImage,DiffImage,15,255,CV_THRESH_BINARY);//��ֵ����ǰ���ͼ
		if(secondFrameRec)//������ڵ��ڵ���֡
		{
			cvAnd(DiffImage,lastDiffImage,DiffImage_2);//���С��롱���㣬�õ�ǰһ֡�Ҷ�ͼ�ġ�׼ȷ���˶�Ŀ��
			cvPyrDown(DiffImage_2,pyr,7);//���²���
			cvErode(pyr,pyr,0,1);//��ʴ������С������
			cvPyrUp(pyr,DiffImage_2,7); 
			cvReleaseImage(&pyr);

			if(drawAlarmArea)	cvRectangle(TempFrame.image, cvPoint(xLeftAlarm, yTopAlarm), cvPoint(xRightAlarm, yBottomAlarm), CV_RGB(0, 0, 255), 3, CV_AA, 0);
			alarm = AlarmArea(xLeftAlarm, yTopAlarm, xRightAlarm, yBottomAlarm, DiffImage_2);  
		}

		cvCopy(DiffImage,lastDiffImage,NULL);//���ݵ�ǰ���ͼ�Ķ�ֵ��ͼ

		cvReleaseImage(&DiffImage); 

		minLeftX = 3000;
		minLeftY = 3000; 
		maxRightX = 0;
		maxRightY = 0;

		if (alarm)//����������ͼƬ���˶�Ŀ��
		{
			FindRectX(DiffImage_2, 0, height);
			FindRectY(DiffImage_2, minLeftX, maxRightX);
		}

		if (!secondFrameRec)//���õڶ�֡�Ѿ��յ� 
		{
			secondFrameRec = true; 
		}
	}

	if(maxRightX*maxRightY)//�����ǰ֡��⵽�˶�Ŀ�꣬�򣬻���,����
	{
		//��ֹ���½ǳ���
		maxRightX = maxRightX>1 ? maxRightX:2; 
		maxRightY = maxRightY>1 ? maxRightY:2;
		maxRightX = maxRightX<(width+1) ? maxRightX:width;
		maxRightY = maxRightY<(height+1) ? maxRightY:height;

		//��ֹ���Ͻǳ���
		minLeftX = minLeftX>0 ? minLeftX:1;
		minLeftY = minLeftY>0 ? minLeftY:1; 
		minLeftX = minLeftX<maxRightX ? minLeftX:(maxRightX-1);
		minLeftY = minLeftY<maxRightY ? minLeftY:(maxRightY-1);

		if (drawRect) cvRectangle(TempFrame.image, cvPoint(minLeftX, minLeftY), cvPoint(maxRightX, maxRightY), CV_RGB(255, 0, 0), 3, CV_AA, 0);
		
		//outobj<<minLeftX<<"	"<<minLeftY<<"	"<<maxRightX-minLeftX<<"	"<<maxRightY-minLeftY<<endl; 

		rect = cvRect(minLeftX, minLeftY, maxRightX-minLeftX, maxRightY-minLeftY); 
		TempFrame.searchRect = rect;

		signelCount++;  
		cvReleaseImage(&GrayImage);
		cvReleaseImage(&DiffImage_2);   
		LastFrame = frame;
		*lastFrame = TempFrame;

		//if(signelCount == groupCount)//���������⵽5�����˵������������� 
		//{
		//	signelCount = 0; 
		//	return true;
		//}
		//else
		//{
		//	return false;
		//}
		

		if((minLeftY < 360) && ((maxRightX-minLeftX) < 420))//�����⵽��Ϊ���˴�С
		{
			signelCount++; 
			cvReleaseImage(&GrayImage);
			cvReleaseImage(&DiffImage_2);  
			LastFrame = frame;
			*lastFrame = TempFrame;

			if(signelCount == groupCount)//���������⵽5�����˵������������� 
			{
				signelCount = 0;
				return true;
			}
			else
			{
				return false;
			}	
		}
		else //�����⵽���������ÿ��ͼƬ��Ϊһ��
		{
			signelCount = 0;
			cvReleaseImage(&GrayImage);
			cvReleaseImage(&DiffImage_2);
			LastFrame = frame;
			*lastFrame = TempFrame;
			return true;
		} 
	}
	else //��ǰ֡û��⵽
	{ 
		cvReleaseImage(&GrayImage);
		cvReleaseImage(&DiffImage_2);
		LastFrame = frame;
		*lastFrame = TempFrame;  

		if (signelCount > 0)//���ǰһ֡Ϊ���ˣ���ǰ֡û����
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





