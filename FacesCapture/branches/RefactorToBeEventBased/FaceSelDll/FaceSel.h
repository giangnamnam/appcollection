#ifndef _IF_FACESEL_H_
#define _IF_FACESEL_H_

#ifdef DLL_EXPORTS
#define DLL_API _declspec(dllexport)
#else
#define DLL_API _declspec(dllimport)
#endif

#include "frame.h"

extern "C"
{

/////////////////New Interface for Face Recognition -- 20090929 Added///////////////////////////////////
	DLL_API void FaceImagePreprocess( IplImage* imgIn, IplImage* &imgNorm, CvRect roi = cvRect(0,0,0,0), int i = 0 );//imgIn:��ͼ��roi:�����沿������ΪcvRect(0,0,0,0)��ʾ����ͼ��Ϊ����ͼ�񣩣�imgNorm�����һ��ͼ��
	//��Ҫ��imgIn��imgNorm��ʹ����������cvReleaseImage�ͷ�
	DLL_API void FaceImagePreprocess_ForTrain( IplImage* imgIn, ImageArray &normImages, CvRect roi = cvRect(0,0,0,0), int i = 0 );//normImages:����ѵ���Ĺ�һ��������ͼ��ImageArray���ݽṹ��frame.h�ж���
	//��Ҫ��imgIn�����cvReleaseImage�����ͷţ�normImages�����ReleaseImageArray���������£������ͷ�
	DLL_API void ReleaseImageArray( ImageArray &images );
/////////////////End -- New Interface for Face Recognition -- 20090929 Added///////////////////////////
};

#endif