#include "Stdafx.h"
#include "..\..\Library\FaceProcessing\FaceReco.h"
#include "FaceSpecificationConverter.h"


FaceProperty FaceProcessingWrapper::FaceSpecificationConverter::ToUnmanaged(FaceSpecification^ faceSpecification)
{
	FaceProperty ufp;

	ufp.eyeBrowRatio = faceSpecification->EyebrowRatio;
	ufp.eyeBrowShape = faceSpecification->EyebrowShape;
	ufp.features = new float(faceSpecification->Features->Length);
	for (int i=0; i<faceSpecification->Features->Length; ++i)
	{
		ufp.features[i] = faceSpecification->Features[i];
	}

	return ufp;
}

FaceProcessingWrapper::FaceSpecification^ FaceProcessingWrapper::FaceSpecificationConverter::FromUnmanaged(FaceProperty faceProperty)
{
	FaceSpecification^ fp = gcnew FaceSpecification();

	fp->EyebrowRatio = faceProperty.eyeBrowRatio;
	fp->EyebrowShape = faceProperty.eyeBrowShape;
	fp->Features = gcnew array<float>(faceProperty.featuresDim);
	for (int i=0; i<faceProperty.featuresDim; ++i)
	{
		fp->Features[i] = faceProperty.features[i];
	}

	return fp;
}



