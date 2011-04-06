#ifndef FACEFINDSKIN_H
#define FACEFINDSKIN_H

#include "cv.h"
#include "facedata.h"

class CFaceSelect;

#ifdef DLL_EXPORTS
#define DLL_API _declspec(dllexport)
#else
#define DLL_API _declspec(dllimport)
#endif

namespace Damany {

	namespace Imaging {

		namespace FaceSearch{

			class FaceVarify;

class DLL_API FaceFind
{
public:
	FaceFind();
	~FaceFind();
	/*********************************************************
	//函数名：FaceSearch()
	//功能：用于在图片中进行找人脸
	//输入：3通道彩图,如果在整张图片中找人脸，则输入为整张图片，
	        若需要在某个指定的子图中搜人脸，输入为该指定的子图
	//返回值：0：未找到人脸，正整数：找到的人脸个数，-1：输入参数错误
	//返回值：FaceData结构体，其中，含有扩展前和扩展后的人脸区域
	***********************************************************/
	int FaceSearch(IplImage *colorIn, CvRect roi, FaceData &fd);
	void SetRoi(int x, int y, int width, int height);
	void SetFaceParas( int minFaceScale, double ratio = 5.0f );
	
private:
	int FaceSearch(IplImage *colorIn, CvRect roi);	
	void GetFaceData(FaceData &fd);//返回找到的人脸坐标位置
	void AdjustRect(CvRect roi);
	void ReleaseMem();//释放内存
	void ClearSeq();

	int faceCount;
	CvRect *faceRectEx;
	CvRect *faceRect;

	CFaceSelect *faceSel;
	FaceVarify *fv;

	CvMemStorage *faceRectSeqStorage;
	CvMemStorage *faceRectExSeqStorage;
	CvSeq *faceRectExSeq;
	CvSeq *faceRectSeq;
};
		}
	}
}

#endif

