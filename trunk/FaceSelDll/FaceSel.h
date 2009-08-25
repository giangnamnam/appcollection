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
////////////////////////////////新增接口///////////////////////////////////////////////////////////////////////////
	DLL_API void AddInFrame(Frame frame);//依次添加一组图片
	DLL_API int SearchFaces(Target** targets);//一组添加完后，调用这个函数，返回脸数目
	DLL_API void ReleaseMem();//在调用完SearchFaces后，下一次调用AddInframe之前，必须显式调用释放内存
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

	DLL_API void SetROI( int x, int y, int width, int height );//如果不调用本函数设置检测区，检测区即为全图
	DLL_API void SetFaceParas( int iMinFace, double dFaceChangeRatio = 5.0f );//设置脸的大小，iMinFace:最小的脸宽，dFaceChangeRatio：最大脸宽与最小脸宽之间的比例
	DLL_API void SetDwSmpRatio( double dRatio );//设置降采样率, 如果希望将图像降采样处理，dRatio应小于1.0，该参数默认为0.5，即找脸时将图像长宽均变为原来的0.5倍，以节约搜索时间

	//SetExRatio : 设置输出的图片应在人脸的四个方向扩展多少比例
	//如果人脸框大小保持不变，4个值都应该为0.0f
	DLL_API void SetExRatio( double topExRatio, double bottomExRatio, double leftExRatio, double rightExRatio );

	DLL_API void SetLightMode(int iMode);//光线环境 0-顺光 1-逆光 2-强逆光
};

#endif