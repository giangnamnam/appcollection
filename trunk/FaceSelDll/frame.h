#ifndef _FACESEL_IF_DATASTRUCT_H_
#define _FACESEL_IF_DATASTRUCT_H_

#include <windows.h>

struct Frame
{
	BYTE cameraID;
	IplImage *image;//cv ת�����ͼƬ
	CvRect searchRect;//�������ķ�Χ
	LONGLONG timeStamp;
};



struct Target
{
	Frame BaseFrame;//��ͼƬ
	int FaceCount;//������
	IplImage** FaceData;//������
};
#endif