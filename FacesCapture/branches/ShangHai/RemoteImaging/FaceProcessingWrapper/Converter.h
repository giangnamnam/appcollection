#pragma once

#include "stdafx.h"
#include "../../Library/FaceProcessing/MotionDetector.h"


using namespace System;
using namespace System::Runtime::InteropServices;

namespace FaceProcessingWrapper
{
	static public ref class FrameConverter
	{
		public:
			static Frame ToUnManaged(Damany::Imaging::Common::Frame^ managed);
	};
}

