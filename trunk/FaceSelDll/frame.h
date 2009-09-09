#ifndef _FACESEL_IF_DATASTRUCT_H_
#define _FACESEL_IF_DATASTRUCT_H_

#include <windows.h>

struct Frame
{
	BYTE cameraID;
	IplImage *image;//cv 转换后的图片
	CvRect searchRect;//搜索脸的范围
	LONGLONG timeStamp;
};



struct Target
{
	Frame BaseFrame;//大图片
	int FaceCount;//脸数量
	IplImage** FaceData;//脸数据
};
#endif