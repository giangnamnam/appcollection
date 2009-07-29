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
	int dataLength;//������
	BYTE *data;//ԭʼ���ݣ�������
	IplImage *image;//ת������Ҫ���������Ĵ�ͼƬ,Ѧ��������
	CvRect *searchRect;//�������ķ�Χ��Ѧ��������
	long timeStamp;//������
	const char *fileName;
};

extern "C"
{
	PREPROCESS_API bool PreProcessFrame(Frame *frame);
	PREPROCESS_API int GetGroupedFrames(Frame** frames);//ȡ�õ�ǰ�ķ���, �����������ͼƬ��Ŀ
	PREPROCESS_API void ReleaseMem(Frame *frames);//�ͷ�����ڴ�,�������˳�ʱ����
}
