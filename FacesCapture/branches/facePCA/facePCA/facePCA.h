#ifdef FACEPCA_EXPORTS
#define FACEPCA_API __declspec(dllexport)
#else
#define FACEPCA_API __declspec(dllimport)  
#endif

#include "stdafx.h" 
#include "afxwin.h"
#include "cv.h"
#include "highgui.h"
#include "cxcore.h" 
#include "iostream"
#include "fstream"
using namespace std; 

float *sampleAvgVal = 0;//指向样本平均值，列向量
float *sampleEigenVector = NULL;//指向样本的特征向量，imgLen行，imgLen列
float *sampleCoeff = NULL;//指向样本的投影系数，sampleCount行，eigenNum列
char *sampleFileName = NULL;//定义样本的文件名

struct similarityMat
{
	float similarity;//返回的相似度，为0---1之间的小数,越接近1表示相似度越高
	char *fileName;//返回的与相似度对应的图片名称
};

extern "C"
{
	FACEPCA_API int FaceTraining(int imgWidth=20, int imgHeight=20, int eigenNum=40);
	FACEPCA_API void InitData( int sampleCount, int imgLen=400, int eigenNum=40);
	FACEPCA_API void FreeData();
	FACEPCA_API void FaceRecognition(float *currentFace, int sampleCount, similarityMat *similarity,int imgLen=400, int eigenNum=40);
};