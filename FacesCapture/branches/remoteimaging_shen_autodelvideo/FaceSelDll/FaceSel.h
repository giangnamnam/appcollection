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
////////////////////////////////�����ӿ�///////////////////////////////////////////////////////////////////////////
	DLL_API void AddInFrame(Frame frame);//�������һ��ͼƬ
	DLL_API int SearchFaces(Target** targets);//һ�������󣬵��������������������Ŀ
	DLL_API void ReleaseMem();//�ڵ�����SearchFaces����һ�ε���AddInframe֮ǰ��������ʽ�����ͷ��ڴ�
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

	DLL_API void SetROI( int x, int y, int width, int height );//��������ñ��������ü�������������Ϊȫͼ
	DLL_API void SetFaceParas( int iMinFace, double dFaceChangeRatio = 5.0f );//�������Ĵ�С��iMinFace:��С������dFaceChangeRatio�������������С����֮��ı���
	DLL_API void SetDwSmpRatio( double dRatio );//���ý�������, ���ϣ����ͼ�񽵲�������dRatioӦС��1.0���ò���Ĭ��Ϊ0.5��������ʱ��ͼ�񳤿����Ϊԭ����0.5�����Խ�Լ����ʱ��

	//SetExRatio : ���������ͼƬӦ���������ĸ�������չ���ٱ���
	//����������С���ֲ��䣬4��ֵ��Ӧ��Ϊ0.0f
	DLL_API void SetExRatio( double topExRatio, double bottomExRatio, double leftExRatio, double rightExRatio );

	DLL_API void SetLightMode(int iMode);//���߻��� 0-˳�� 1-��� 2-ǿ���
};

#endif