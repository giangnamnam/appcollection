#pragma once

#include <stdafx.h>
#include "../../Library/FaceProcessing/FaceReco.h"
#include "FaceSpecification.h"
#include "FaceSpecificationConverter.h"

using namespace System;
using namespace System::Runtime::InteropServices;

namespace FaceProcessingWrapper {


	public ref class FaceRecoWrapper
	{
	public:
		static FaceRecoWrapper^ FromModel(String^ model, String^ classifier)
		{
			return gcnew FaceRecoWrapper(model, classifier);
		}

		~FaceRecoWrapper()
		{
			if (pReco != NULL)
			{
				delete pReco;
			}
		}

		bool CalcFeature(OpenCvSharp::IplImage^ colorFaceImg, FaceSpecification^ fp)
		{
			::FaceProperty ufp;
			bool success = pReco->CalcFeature((IplImage*) colorFaceImg->CvPtr.ToPointer(), ufp);
			if (success == true)
			{
				fp = FaceSpecificationConverter::FromUnmanaged(ufp);
			}

			return success;
		}

		float CmpFace(FaceSpecification^ fp1, FaceSpecification^ fp2)
		{
			FaceProperty ufp1 = FaceSpecificationConverter::ToUnmanaged(fp1);
			FaceProperty ufp2 = FaceSpecificationConverter::ToUnmanaged(fp2);
			float result = pReco->CmpFace(ufp1, ufp2);
			delete[] ufp1.features;
			delete[] ufp2.features;

			return result;
		}

	private:
		FaceRecoWrapper(String^ model, String^ classifier)
		{
			pReco = NULL;

			IntPtr modelPtr = IntPtr::Zero;
			IntPtr classifierPtr = IntPtr::Zero;

			try
			{
				modelPtr = Marshal::StringToHGlobalAnsi(model);
				classifierPtr = Marshal::StringToHGlobalAnsi(classifier);
				pReco = new Damany::Imaging::FaceCompare::FaceReco((char*) modelPtr.ToPointer(), (char*) classifierPtr.ToPointer());
			}
			finally
			{
				if (modelPtr != IntPtr::Zero) Marshal::FreeHGlobal(modelPtr);
				if (classifierPtr != IntPtr::Zero) Marshal::FreeHGlobal(classifierPtr);
			}
			
		}

		Damany::Imaging::FaceCompare::FaceReco *pReco;

	};

}