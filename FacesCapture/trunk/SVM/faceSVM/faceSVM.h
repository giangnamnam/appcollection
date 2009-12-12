#ifdef FACESVM_EXPORTS
#define FACESVM_API __declspec(dllexport)
#else
#define FACESVM_API __declspec(dllimport)
#endif

#include "afxwin.h" 
#include "stdafx.h"
#include "cv.h"
#include "cxcore.h"
#include "highgui.h"
#include "svm.h"
#include "iostream"
#include "fstream"
using namespace std;

CvMat *svmAvgVector;//平均值向量
CvMat *svmEigenVector;//协方差矩阵的特征向量  
struct svm_model* testModel; //SVM预测用的结构体

extern "C"
{
	FACESVM_API void SvmTrain(int imgWidth, int imgHeight, int eigenNum, char *option);
	FACESVM_API double SvmPredict(float *currentFace);
	FACESVM_API void InitSvmData(int sampleCount, int imgLen, int eigenNum);
}