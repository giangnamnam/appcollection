#include "Stdafx.h"
#include "..\..\Library\FaceProcessing\FaceReco.h"
#include "FaceSpecification.h"

using namespace System::Runtime::InteropServices;

namespace FaceProcessingWrapper{

	static public ref class FaceSpecificationConverter
	{
	public:
		[System::Runtime::CompilerServices::Extension]
		static FaceProperty ToUnmanaged(FaceSpecification^ faceSpecification);
		static FaceSpecification^ FromUnmanaged(FaceProperty faceProperty);
	};



}

