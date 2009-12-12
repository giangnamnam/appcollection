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

extern "C"
{
	FACESVM_API void SvmTrain(int imgWidth, int imgHeight, int eigenNum, char *option);
	FACESVM_API double SvmPredict(float *currentFace); 
}