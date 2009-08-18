// PreProcess.cpp : Defines the exported functions for the DLL application.
//1/32����

/************************************************************************************************************
Copyright (c) 2009, �ɶ���ʶ���ܼ������޹�˾
All right reserved!

�ļ�����PreProcess.cpp
ժ  Ҫ���ó��򣬶�ÿһ֡����ͼƬ��������֡��ַ�����ָ�����������ڽ����˶�Ŀ���⡣�����������ڼ�⵽�˶�Ŀ��
        ʱ����ȫͼ��Χ�ڽ����˶�Ŀ���ʾ���������򣬲��������������Է��ء���󣬶����˶�Ŀ���ͼƬ�����з���
		���������ʱ������true,���򣬷���false

��ǰ�汾��3.1
��    �ߣ�Ѧ����
������ڣ�2009��8��11��  
*************************************************************************************************************/
#include "stdafx.h"
#include "PreProcess.h" 
#include "iostream"

Frame LastFrame ;  

int temp1 = 0;//���ڱ�ʾ��������ı�־
int temp2 = 0;//���ڱ�ʾ��������ı�־ 

int signelCount = 0;

bool firstFrameRec = false;//��һ֡�Ƿ��յ�
bool secondFrameRec = false;//�ڶ�֡�Ƿ��յ�

IplImage *currentImage;
IplImage *lastGrayImage;//��һ֡�Ҷ�ͼ
IplImage *lastDiffImage;//��һ֡���ͼ�Ķ�ֵ��ͼ

bool drawRect = false; //��־�Ƿ񻭿�

int xLeftAlarm = 100; //���徯����������������λ��
int yTopAlarm = 400;
int xRightAlarm = 1000;
int yBottomAlarm = 500;

int faceCount = 1200; 
int groupCount = 5;

//����ѡ�����������ص����ֵ֮��,������ֵ����
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

//�жϾ����������Ƿ����˶�Ŀ��,���˶�Ŀ�귵��true,���򣬷���false
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

	f = fopen("C:/ImgPoint.txt", "r");//���ļ���׼�����뾯�����������λ�� 

	while(!feof(f))
	{
		fgets(str, 50, f);//���ļ��е����ݶ����ַ���
	}

	for (int i=0; i<50; i++)//�ҵ��ĸ��ո��λ��
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

	for (int i=0; i<bk1; i++)//���ĸ��ո���������ݶ����ַ���
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

	xLeftAlarm = atoi(data1);//���ĸ��ַ���ת��Ϊ��ֵ
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

	f = fopen("C:/PreProcess.txt", "r");//���ļ���׼�����뾯�����������λ�� 

	while(!feof(f))
	{
		fgets(str, 50, f);//���ļ��е����ݶ����ַ���
	}

	for (int i=0; i<50; i++)//�ҵ��ĸ��ո��λ��
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

	for (int i=0; i<bk1; i++)//���ĸ��ո���������ݶ����ַ���
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

//ÿ�δ�����ͷ���һ��ͼƬ����ã������һ������󷵻�true, ����û�������򷵻�false
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

	int height,width,step;//����ͼ��ĸߣ�������
	int SumInRect = 0;//ָ��������ͼ������֮��
	int y1, y2, x1, x2;//���˶�Ŀ�껭��ʱ���ĸ������λ��
	y1 = 0;
	y2 = 0;
	x1 = 0; 
	x2 = 0;

	/*int threshold_4 = faceCount*255/4;
	int threshold_16 = faceCount*255/16;
	int threshold_32 = faceCount*255/32;*/ 

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
	step = GrayImage->widthStep;

	//int left_x = width;//�����˶�Ŀ�������������λ��
	//int left_y = height;
	//int right_x = 0;
	//int right_y = 0; 

	bool alarm = false;//���������Ƿ����˶�Ŀ��   

	CvRect rect;//������ο�

	if(!firstFrameRec)//����ǵ�һ֡
	{
		firstFrameRec = true;
		lastGrayImage = cvCreateImage(ImageSize,IPL_DEPTH_8U,1);
		lastDiffImage = cvCreateImage(ImageSize,IPL_DEPTH_8U,1);
		cvCopy(GrayImage,lastGrayImage,NULL);//����ǵ�һ֡������Ϊ����

		ReadImgPoint();//���벼��ѡ��ľ�������λ��
		ReadPreProcess();

		//��ֹ���Ͻ����곬����Χ
		xLeftAlarm = xLeftAlarm>0 ? xLeftAlarm:0;
		xLeftAlarm = xLeftAlarm>(width-10) ? (width-10):xLeftAlarm;
		yTopAlarm = yTopAlarm>0 ? yTopAlarm:0;
		yTopAlarm = yTopAlarm>(height-10) ? (height-10):yTopAlarm;

		//��ֹ���½����곬����Χ
		xRightAlarm = xRightAlarm>(width-8) ? (width-8):xRightAlarm;
		xRightAlarm = xRightAlarm>xLeftAlarm ? xRightAlarm:(xRightAlarm+1);
		yBottomAlarm = yBottomAlarm>(height-8) ? (height-8):yBottomAlarm;
		yBottomAlarm = yBottomAlarm>yTopAlarm ? yBottomAlarm:(yTopAlarm+1);

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

			//cvRectangle(TempFrame.image, cvPoint(xLeftAlarm, yTopAlarm), cvPoint(xRightAlarm, yBottomAlarm), CV_RGB(0,255,0),1, CV_AA, 0);
			alarm = AlarmArea(xLeftAlarm, yTopAlarm, xRightAlarm, yBottomAlarm, DiffImage_2); 
		}
		 
		cvCopy(DiffImage,lastDiffImage,NULL);//���ݵ�ǰ���ͼ�Ķ�ֵ��ͼ

		cvReleaseImage(&DiffImage); 


		//SumInRect = RegionSum(0, 0, width, height, DiffImage_2);
		if (alarm)//����������ͼƬ���˶�Ŀ��
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

		if (!secondFrameRec)//���õڶ�֡�Ѿ��յ�
		{
			secondFrameRec = true; 
		}

	}

	if(y2 != 0)//�����ǰ֡��⵽�˶�Ŀ�꣬�򣬻���,����
	{
		x2 = (x2+100)>width ? width:(x2+100);
		y2 = (y2+100)>height ? height:(y2+100); 
		//y1 = (y1-100)>0 ? (y1-100):0;  
		if (drawRect) cvRectangle(TempFrame.image,cvPoint(x1,y1),cvPoint(x2,y2),CV_RGB(255,0,0),3,CV_AA,0); 

		rect = cvRect(x1,y1,x2-x1,y2-y1); 
		TempFrame.searchRect = rect;

		if((y1 < 360) && ((x2-x1) < 400))//�����⵽��Ϊ���˴�С
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

void SetDrawRect(bool draw)
{
	drawRect = draw;
}



