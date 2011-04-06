#ifndef FACEDATA_H
#define FACEDATA_H

struct FaceData 
{
	CvRect *faceRectEx;//扩展后的人脸位置,用于显示、保存
	CvRect *faceRect;//只含有面部的人脸位置，用于识别
};
#endif