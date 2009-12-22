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

float *sampleAvgVal = 0;//ָ������ƽ��ֵ��������
float *sampleEigenVector = NULL;//ָ������������������imgLen�У�imgLen��
float *sampleCoeff = NULL;//ָ��������ͶӰϵ����sampleCount�У�eigenNum��
char *sampleFileName = NULL;//�����������ļ���

struct similarityMat
{
	float similarity;//���ص����ƶȣ�Ϊ0---1֮���С��,Խ�ӽ�1��ʾ���ƶ�Խ��
	char *fileName;//���ص������ƶȶ�Ӧ��ͼƬ����
};

extern "C"
{
	FACEPCA_API int FaceTraining(int imgWidth=20, int imgHeight=20, int eigenNum=40);
	FACEPCA_API void InitData( int sampleCount, int imgLen=400, int eigenNum=40);
	FACEPCA_API void FreeData();
	FACEPCA_API void FaceRecognition(float *currentFace, int sampleCount, similarityMat *similarity,int imgLen=400, int eigenNum=40);
};