// This is the main DLL file.

#include "stdafx.h"
#include "FaceSearchWrapper.h"



FaceSearchWrapper::FaceSearch::FaceSearch(void)
{
	this->pFaceSearch = new Damany::Imaging::FaceSearch::FaceFind();
}

FaceSearchWrapper::FaceSearch::~FaceSearch(void)
{
	delete pFaceSearch;
}


//Michael Add -- 设置类变量的接口
void FaceSearchWrapper::FaceSearch::SetRoi( int x, int y, int width, int height )
{
	pFaceSearch->SetRoi(x, y, width, height);
}

void FaceSearchWrapper::FaceSearch::SetFaceParas( int iMinFace, double dFaceChangeRatio)
{
	pFaceSearch->SetFaceParas(iMinFace, dFaceChangeRatio);
}

array<Damany::Imaging::Common::PortraitBounds^>^ FaceSearchWrapper::FaceSearch::SearchFace(OpenCvSharp::IplImage^ colorImage)
{
	OpenCvSharp::CvRect^ rc = gcnew OpenCvSharp::CvRect(0, 0, colorImage->Width, colorImage->Height);
	return SearchFace(colorImage, rc);
}


array<Damany::Imaging::Common::PortraitBounds^>^ FaceSearchWrapper::FaceSearch::SearchFace(OpenCvSharp::IplImage^ colorImage, OpenCvSharp::CvRect^ roi)
{
	::CvRect uc = ManagedRectToUnmanaged(roi);
	FaceData data;
	int faceCount = pFaceSearch->FaceSearch((IplImage*) colorImage->CvPtr.ToPointer(), uc, data);

	array<Damany::Imaging::Common::PortraitBounds^>^ faceRects = 
		gcnew array<Damany::Imaging::Common::PortraitBounds^>(faceCount);

	for (int i=0; i<faceCount; ++i)
	{
		faceRects[i] = gcnew Damany::Imaging::Common::PortraitBounds;

		//整个头部位置
		::CvRect portraitRect = data.faceRectEx[i];
		//面部位置
		::CvRect faceRect = data.faceRect[i];
		//调整为相对坐标
		faceRect.x -= portraitRect.x;
		faceRect.y -= portraitRect.y;
		faceRects[i]->Bounds = UnmanagedRectToManaged(portraitRect);
		faceRects[i]->FaceBoundsInPortrait = UnmanagedRectToManaged(faceRect);
	}

	return faceRects;
}


::CvRect FaceSearchWrapper::FaceSearch::ManagedRectToUnmanaged(OpenCvSharp::CvRect^ managedRect)
{
	::CvRect unmanagedRect;

	unmanagedRect.x = managedRect->X;
	unmanagedRect.y = managedRect->Y;
	unmanagedRect.width = managedRect->Width;
	unmanagedRect.height = managedRect->Height;

	return unmanagedRect;
}

OpenCvSharp::CvRect FaceSearchWrapper::FaceSearch::UnmanagedRectToManaged(const ::CvRect& unmanaged)
{
	OpenCvSharp::CvRect managed = OpenCvSharp::CvRect(
		unmanaged.x,   
		unmanaged.y,     
		unmanaged.width,
		unmanaged.height
		);

	return managed;
}


