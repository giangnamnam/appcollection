// FaceSearchWrapper.h

#pragma once
#include "../../OutPut/FaceSellDll/FaceSelect.h"

using namespace System;
using namespace OpenCvSharp;
using namespace System::Runtime::InteropServices;


namespace FaceSearchWrapper {


	public ref class ManagedFrame
	{
	public:
		byte cameraID;

		OpenCvSharp::IplImage^ image;

		OpenCvSharp::CvRect^ searchRect;

		long timeStamp;
	};


	public ref class ManagedTarget
	{
	public:
		OpenCvSharp::IplImage^ BaseFrame;

		array<OpenCvSharp::IplImage^>^ Faces;

		array<OpenCvSharp::CvRect^>^ EnlargedFaceRect;

		array<OpenCvSharp::CvRect^>^ OriginalFaceRect;
	};



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

		void AddInFrame(ManagedFrame^ frame)
		{
			IntPtr pFrameIntptr = Marshal::AllocHGlobal(Marshal::SizeOf(frame));
			Marshal::StructureToPtr(frame, pFrameIntptr, true);

			Frame *pFrame = (Frame *) pFrameIntptr.ToPointer();

			pFaceSearch->AddInFrame(*pFrame);

			Marshal::FreeHGlobal(pFrameIntptr);

		}

		array<ManagedTarget^>^ SearchFaces()
		{
			::Target *pFacesFound = NULL;

			int faceGroupCount = pFaceSearch->SearchFaces(&pFacesFound);

			array<ManagedTarget^>^ mtArray = gcnew array<ManagedTarget^>(faceGroupCount);

			for (int i=0; i<faceGroupCount; ++i)
			{
				mtArray[i]->BaseFrame = gcnew OpenCvSharp::IplImage( (IntPtr) pFacesFound->BaseFrame.image);

				int facesCount = pFacesFound[i].FaceCount;

				mtArray[i]->EnlargedFaceRect = gcnew array<OpenCvSharp::CvRect^>(facesCount);
				mtArray[i]->OriginalFaceRect = gcnew array<OpenCvSharp::CvRect^>(facesCount);

				for (int j=0; j<facesCount; ++j)
				{
					mtArray[i]->EnlargedFaceRect[j] = 
						gcnew OpenCvSharp::CvRect(pFacesFound[i].FaceRects[j].x,
												  pFacesFound[i].FaceRects[j].y,
												  pFacesFound[i].FaceRects[j].width,
												  pFacesFound[i].FaceRects[j].height);
						
				}
				
			}

			pFaceSearch->ReleaseTargets();

			return mtArray;
		}


	private:
		CFaceSelect *pFaceSearch;
	};
}
