// The following ifdef block is the standard way of creating macros which make exporting 
// from a DLL simpler. All files within this DLL are compiled with the PREPROCESS_EXPORTS
// symbol defined on the command line. this symbol should not be defined on any project
// that uses this DLL. This way any other project whose source files include this file see 
// PREPROCESS_API functions as being imported from a DLL, whereas this DLL sees symbols
// defined with this macro as being exported.
#ifdef PREPROCESS_EXPORTS
#define PREPROCESS_API __declspec(dllexport)
#else
#define PREPROCESS_API __declspec(dllimport)
#endif

#include "highgui.h"
#include "cv.h"
#include "omp.h"

struct Frame
{
	int dataLength;//沈斌给出
	BYTE *data;//原始数据，沈斌给出
	IplImage *image;//转换后需要搜索人脸的大图片,薛晓利给出
	CvRect *searchRect;//搜索脸的范围，薛晓利给出
	long timeStamp;//沈斌给出
	const char *fileName;
};

extern "C"
{
	PREPROCESS_API bool PreProcessFrame(Frame *frame);
	PREPROCESS_API int GetGroupedFrames(Frame** frames);//取得当前的分组, 返回组里面的图片数目
	PREPROCESS_API void ReleaseMem(Frame *frames);//释放相关内存,主程序退出时调用
}
