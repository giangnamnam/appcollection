// FaceSearchWrapper.h

#pragma once
#include "../../OutPut/FaceSellDll/FaceSelect.h"

using namespace System;
using namespace System::Runtime::InteropServices;


namespace FaceSearchWrapper {



	public ref class FaceSearch
	{
		// TODO: Add your methods for this class here.
	public:
		FaceSearch()
		{
			this->pFaceSearch = new CFaceSelect();
		}
		~FaceSearch()
		{
			delete pFaceSearch;
		}

		//Michael Add -- 设置类变量的接口
		void SetROI( int x, int y, int width, int height )
		{
			pFaceSearch->SetROI(x, y, width, height);
		}

		void SetFaceParas( int iMinFace, double dFaceChangeRatio)
		{
			pFaceSearch->SetFaceParas(iMinFace, dFaceChangeRatio);
		}


		void SetDwSmpRatio( double dRatio )
		{
			pFaceSearch->SetDwSmpRatio(dRatio);

		}


		void SetOutputDir( const char* dir )
		{
			pFaceSearch->SetOutputDir(dir);
		}

		//SetExRatio : 设置输出的图片应在人脸的四个方向扩展多少比例
		//如果人脸框大小保持不变，4个值都应该为0.0f
		void SetExRatio( double topExRatio, double bottomExRatio, double leftExRatio, double rightExRatio )
		{
			pFaceSearch->SetExRatio(topExRatio, bottomExRatio, leftExRatio, rightExRatio);
		}


		void SetLightMode(int iMode)
		{
			pFaceSearch->SetLightMode(iMode);
		}

		void AddInFrame(ImageProcess::Frame^ frame)
		{

			Frame frm;

			frm.cameraID = frame->cameraID;
			frm.image = (::IplImage *) frame->image->CvPtr.ToPointer();

			frm.searchRect.x = frame->searchRect.X;
			frm.searchRect.y = frame->searchRect.Y;
			frm.searchRect.width = frame->searchRect.Width;
			frm.searchRect.height = frame->searchRect.Height;

			frm.timeStamp = frame->timeStamp;

			pFaceSearch->AddInFrame(frm);

		}

		array<ImageProcess::Target^>^ SearchFaces()
		{

			::Target *pFacesFound = NULL;

			int faceGroupCount = pFaceSearch->SearchFaces(&pFacesFound);

			System::Diagnostics::Debug::WriteLine("after search in wrapper");

			array<ImageProcess::Target^>^ mtArray = gcnew array<ImageProcess::Target^>(faceGroupCount);

			for (int i=0; i<faceGroupCount; ++i)
			{
				mtArray[i] = gcnew ImageProcess::Target;

			    mtArray[i]->BaseFrame = gcnew ImageProcess::Frame;
				Frame unmanagedBaseFrame = pFacesFound[i].BaseFrame;

				mtArray[i]->BaseFrame->cameraID = pFacesFound[i].BaseFrame.cameraID;
				mtArray[i]->BaseFrame->image = gcnew OpenCvSharp::IplImage( (IntPtr) unmanagedBaseFrame.image  );

				mtArray[i]->BaseFrame->image->IsEnabledDispose = false;

 				mtArray[i]->BaseFrame->searchRect.X = unmanagedBaseFrame.searchRect.x;
				mtArray[i]->BaseFrame->searchRect.Y = unmanagedBaseFrame.searchRect.y;
				mtArray[i]->BaseFrame->searchRect.Width = unmanagedBaseFrame.searchRect.width;
				mtArray[i]->BaseFrame->searchRect.Height = unmanagedBaseFrame.searchRect.height;

 			    mtArray[i]->BaseFrame->timeStamp = unmanagedBaseFrame.timeStamp;


				int facesCount = pFacesFound[i].FaceCount;


				mtArray[i]->Faces = gcnew array<OpenCvSharp::IplImage^>(facesCount);
				mtArray[i]->FacesRects  = gcnew array<OpenCvSharp::CvRect>(facesCount);
				mtArray[i]->FacesRectsForCompare = gcnew array<OpenCvSharp::CvRect>(facesCount);

				for (int j=0; j<facesCount; ++j)
				{

					mtArray[i]->Faces[j] = gcnew OpenCvSharp::IplImage( (IntPtr) pFacesFound[i].FaceData[j] );

					mtArray[i]->Faces[j]->IsEnabledDispose = false;

					mtArray[i]->FacesRects[j] = 
						OpenCvSharp::CvRect(pFacesFound[i].FaceRects[j].x,
 											      pFacesFound[i].FaceRects[j].y,
 												  pFacesFound[i].FaceRects[j].width,
 												  pFacesFound[i].FaceRects[j].height);
						
				}
				
			}

			return mtArray;
		}


		OpenCvSharp::IplImage^ NormalizeImage(OpenCvSharp::IplImage^ imgIn, OpenCvSharp::CvRect roi)
		{

			IplImage* unmanagedIn = (IplImage *) imgIn->CvPtr.ToPointer();
			IplImage* unmanagedNormalized = NULL;

			::CvRect *pUnmanagedRect = ManagedRectToUnmanaged(roi);
			pFaceSearch->FaceImagePreprocess(unmanagedIn, unmanagedNormalized, *pUnmanagedRect);

			assert(unmanagedNormalized != NULL);

			OpenCvSharp::IplImage^ normalized = gcnew OpenCvSharp::IplImage((IntPtr) unmanagedNormalized);
			normalized->IsEnabledDispose = false;

		    Marshal::FreeHGlobal((IntPtr) pUnmanagedRect);

			return normalized;

		}


		array<OpenCvSharp::IplImage^>^ NormalizeImageForTraining(OpenCvSharp::IplImage^ imgIn, OpenCvSharp::CvRect roi)
		{
			::IplImage *pUnmanagedIn = (::IplImage *) imgIn->CvPtr.ToPointer();
			ImageArray trainedImages;
			::CvRect *pUnmanagedRect = ManagedRectToUnmanaged(roi);

			pFaceSearch->FaceImagePreprocess_ForTrain(pUnmanagedIn, trainedImages, *pUnmanagedRect);

			array<OpenCvSharp::IplImage^>^ returnArray = gcnew array<OpenCvSharp::IplImage^>(trainedImages.nImageCount);
			for (int i=0; i<trainedImages.nImageCount; ++i)
			{
				assert(trainedImages.imageArr[i] != NULL);

				returnArray[i] = gcnew OpenCvSharp::IplImage((IntPtr) trainedImages.imageArr[i]);
				returnArray[i]->IsEnabledDispose = false;
			}

			pFaceSearch->ReleaseImageArray(trainedImages);

			return returnArray;
		}



	private:
		::CvRect* ManagedRectToUnmanaged(OpenCvSharp::CvRect^ managedRect)
		{
			IntPtr unmanagedPtr = Marshal::AllocHGlobal(sizeof(managedRect));
			Marshal::StructureToPtr(managedRect, unmanagedPtr, true);
			return (::CvRect *) unmanagedPtr.ToPointer();
		}
		CFaceSelect *pFaceSearch;
	};
}
