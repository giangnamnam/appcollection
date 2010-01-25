#ifdef _CLASSINDLL
#define CLASSINDLL_CLASS_DECL  __declspec(dllexport)
#else
#define CLASSINDLL_CLASS_DECL __declspec(dllimport)
#endif

#include "stdafx.h"
#include "cv.h"
#include "cxcore.h"
#include "highgui.h" 
#pragma once

class CLASSINDLL_CLASS_DECL frontalFaceDetect
{
private:
	IplImage* eyeTemplate;//�۾�ģ��ͼ��
	IplImage* img;//��ǰ��Ҫ�ж�����������ͼƬ
	int Front,T;      //Front���Ƿ����������ı�־����Ϊ1��T�Զ���������ֵ�����м����
	float t1,t2,t3;   //�ֱ������������ĺ�����ƫ����ֵ��������ƫ����ֵ�����۾����ȱ���ֵ��
	                  //�����ж��Ƿ���б�������͵�̧ͷ
	IplImage* Tresult;
	IplImage* eye;

private: 
	void PlotBox(IplImage* Tresult, IplImage* target, IplImage* templat, CvMat* M);
	int tmp(IplImage* templat, IplImage* target, IplImage* Tresult);
	int Threshold(IplImage* Tresult, int* T);
	int FrontFace(IplImage* eye, float t1, float t2, float t3, int* Front);
	int FrontFaceCenter(IplImage* P,int* center_x,int* center_y, float* column_row_k);
	int Detection(IplImage* Tresult, IplImage* eye, int* T);

public:
	frontalFaceDetect();
	~frontalFaceDetect();
	int LoadEyeTemplage(char* eyeImgAdd);//�����۾�ģ��ͼƬ
	bool FrontalDetect(IplImage* targetNormFace);//���ش�ʶ�������ͼƬ������������⣬����
};

