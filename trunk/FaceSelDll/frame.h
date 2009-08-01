#ifndef _FACESEL_IF_DATASTRUCT_H_
#define _FACESEL_IF_DATASTRUCT_H_

#include <windows.h>

struct Frame
{
	IplImage *image;//cv ת�����ͼƬ
	CvRect searchRect;//�������ķ�Χ
	long timeStamp;
};

struct ByteArray
{
	byte *bytes;
	int length;
};


struct Target
{
	Frame *BaseFrame;//��ͼƬ
	int FaceCount;//������
	IplImage** FaceData;//������
};
#endif