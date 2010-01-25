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
	IplImage* eyeTemplate;//眼睛模版图像
	IplImage* img;//当前需要判断正面人脸的图片
	int Front,T;      //Front，是否是正面脸的标志，是为1，T自动检测出的阈值，是中间变量
	float t1,t2,t3;   //分别是左右眼中心横坐标偏移阈值，纵坐标偏移阈值，和眼睛宽长比比阈值，
	                  //用于判断是否倾斜、侧脸和低抬头
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
	int LoadEyeTemplage(char* eyeImgAdd);//加载眼睛模版图片
	bool FrontalDetect(IplImage* targetNormFace);//加载待识别的人脸图片，进行人脸检测，搜索
};

