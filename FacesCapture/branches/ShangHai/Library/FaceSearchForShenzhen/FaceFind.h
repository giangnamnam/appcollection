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
	//��������FaceSearch()
	//���ܣ�������ͼƬ�н���������
	//���룺3ͨ����ͼ,���������ͼƬ����������������Ϊ����ͼƬ��
	        ����Ҫ��ĳ��ָ������ͼ��������������Ϊ��ָ������ͼ
	//����ֵ��0��δ�ҵ����������������ҵ�������������-1�������������
	//����ֵ��FaceData�ṹ�壬���У�������չǰ����չ�����������
	***********************************************************/
	int FaceSearch(IplImage *colorIn, CvRect roi, FaceData &fd);
	void SetRoi(int x, int y, int width, int height);
	void SetFaceParas( int minFaceScale, double ratio = 5.0f );
	
private:
	int FaceSearch(IplImage *colorIn, CvRect roi);	
	void GetFaceData(FaceData &fd);//�����ҵ�����������λ��
	void AdjustRect(CvRect roi);
	void ReleaseMem();//�ͷ��ڴ�
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

