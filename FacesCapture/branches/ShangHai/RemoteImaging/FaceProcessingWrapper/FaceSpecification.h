#pragma once

#include "stdafx.h"
#include "../../Library/FaceProcessing/FaceReco.h"

using namespace System;
using namespace System::Runtime::InteropServices;

namespace FaceProcessingWrapper
{
	public ref class FaceSpecification
	{
	public:
		array<float>^ Features;
		int EyebrowRatio;
		int EyebrowShape;
	};
}
