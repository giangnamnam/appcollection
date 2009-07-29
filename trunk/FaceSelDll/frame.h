#ifndef _FACESEL_IF_DATASTRUCT_H_
#define _FACESEL_IF_DATASTRUCT_H_

#include <windows.h>

struct Frame
{
	int dataLength;
	byte *data;//ԭʼ����
	IplImage *image;//cv ת�����ͼƬ
	CvRect *searchRect;//�������ķ�Χ
	long timeStamp;
	const char* fileName;
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