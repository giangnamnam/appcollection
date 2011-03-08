#ifndef FACERECO_H
#define FACERECO_H

#include "stdafx.h"
#include "cv.h"
#include "highgui.h"

#ifdef DLL_EXPORTS
#define DLL_API _declspec(dllexport)
#else
#define DLL_API _declspec(dllimport)
#endif

struct FaceProperty
{
	float *features;
	int featuresDim;
	int eyeBrowRatio;
	int eyeBrowShape;
};

namespace Damany {

	namespace Imaging {

		namespace FaceCompare {
			class FaceAlignment;
			class LBP;

class DLL_API FaceReco
{
public:
	FaceReco(char *modelAdd, char *classifierAdd);
	~FaceReco();
	bool CalcFeature(IplImage *colorFaceImg, FaceProperty &fp);
	float CmpFace(FaceProperty fp1, FaceProperty fp2);


private:
	int EyeBrowShape(CvPoint fpt[]);
	int EyeRatioClassifier(CvPoint fpt[]);
	int EyeBrowSlopeClassifier(CvPoint fpt[]);
	int EyeBrowRatioClassifier(CvPoint fpt[]);//判断是否是细长的眉毛

private:
	FaceAlignment *fa;
	LBP *faceCmp;
	float *m_feature;
};
		}
	}
}

#endif