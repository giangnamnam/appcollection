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
	DLL_API void FaceImagePreprocess( IplImage* imgIn, IplImage* &imgNorm, CvRect roi = cvRect(0,0,0,0), int i = 0 );//imgIn:大图像，roi:设置面部区域（若为cvRect(0,0,0,0)表示整幅图均为人脸图像），imgNorm输出归一化图像
	//重要：imgIn，imgNorm在使用完后请调用cvReleaseImage释放
	DLL_API void FaceImagePreprocess_ForTrain( IplImage* imgIn, ImageArray &normImages, CvRect roi = cvRect(0,0,0,0), int i = 0 );//normImages:用于训练的归一化后的五幅图像，ImageArray数据结构在frame.h中定义
	//重要：imgIn请调用cvReleaseImage进行释放，normImages请调用ReleaseImageArray（定义如下）进行释放
	DLL_API void ReleaseImageArray( ImageArray &images );
/////////////////End -- New Interface for Face Recognition -- 20090929 Added///////////////////////////
};

#endif