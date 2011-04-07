// FaceSearchWrapper.h

#pragma once
#include "../../Library/FaceSearchForShenZhen/FaceFind.h"
#include "FaceSearchConfiguration.h"

using namespace System;
using namespace System::Runtime::InteropServices;


namespace FaceSearchWrapper {

	public ref class FaceSearch
	{
		// TODO: Add your methods for this class here.
	public:
		FaceSearch::FaceSearch(void);
		FaceSearch::~FaceSearch(void);

		array<Damany::Imaging::Common::PortraitBounds^>^ SearchFace(OpenCvSharp::IplImage^ colorImage, OpenCvSharp::CvRect^ roi);
		array<Damany::Imaging::Common::PortraitBounds^>^ SearchFace(OpenCvSharp::IplImage^ colorImage);
		void SetRoi(int x, int y, int width, int height);
		void SetFaceParas( int minFaceScale, double ratio);

		property FaceSearchConfiguration^ Configuration
		{
			FaceSearchConfiguration^ get()
			{
				return this->config;
			}

			void set(FaceSearchConfiguration^ cfg)
			{
				this->config = cfg;

				this->pFaceSearch->SetFaceParas(this->config->MinFaceWidth, this->config->FaceWidthRatio);
				this->pFaceSearch->SetRoi(this->config->SearchRectangle->Left,
					                      this->config->SearchRectangle->Top,
										  this->config->SearchRectangle->Width,
										  this->config->SearchRectangle->Height);

			}
		}
		

	private:
		::CvRect ManagedRectToUnmanaged(OpenCvSharp::CvRect^ managedRect);
		OpenCvSharp::CvRect UnmanagedRectToManaged(const ::CvRect& unmanaged);
		FaceSearchConfiguration^ config;
		Damany::Imaging::FaceSearch::FaceFind  *pFaceSearch;
		OpenCvSharp::CvRect^ roi;
	};
}
