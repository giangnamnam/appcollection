// This is the main DLL file.

#include "stdafx.h"
#include "FaceSearchWrapper.h"


//计算两个Rect的交集
static OpenCvSharp::CvRect^ Intersect(OpenCvSharp::CvRect^ a, OpenCvSharp::CvRect^ b)
{
    int x = Math::Max(a->X, b->X);
    int num2 = Math::Min((int) (a->X + a->Width), (int) (b->X + b->Width));
    int y = Math::Max(a->Y, b->Y);
    int num4 = Math::Min((int) (a->Y + a->Height), (int) (b->Y + b->Height));
    if ((num2 >= x) && (num4 >= y))
    {
        return gcnew OpenCvSharp::CvRect(x, y, num2 - x, num4 - y);
    }
    return gcnew OpenCvSharp::CvRect(0, 0, 0, 0);
}



FaceSearchWrapper::FaceSearch::FaceSearch(void)
{
	this->pFaceSearch = new Damany::Imaging::FaceSearch::FaceFind();
	this->roi = gcnew OpenCvSharp::CvRect(0,0,0,0);
}

FaceSearchWrapper::FaceSearch::~FaceSearch(void)
{
	delete pFaceSearch;
}


//Michael Add -- 设置类变量的接口
void FaceSearchWrapper::FaceSearch::SetRoi( int x, int y, int width, int height )
{
	pFaceSearch->SetRoi(x, y, width, height);
	this->roi = gcnew OpenCvSharp::CvRect(x, y, width, height);
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
	OpenCvSharp::CvRect^ intersect = roi;
	if (this->roi->Width > 0 && this->roi->Height >0)
	{
		roi = Intersect(roi, this->roi);
	}

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


