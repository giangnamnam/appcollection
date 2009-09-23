// CamControl.h : main header file for the CamControl DLL
//

#pragma once

#ifndef __AFXWIN_H__
	#error "include 'stdafx.h' before including this file for PCH"
#endif

#include "resource.h"		// main symbols


// CCamControlApp
// See CamControl.cpp for the implementation of this class
//

class CCamControlApp : public CWinApp
{
public:
	CCamControlApp();

// Overrides
public:
	virtual BOOL InitInstance();

	DECLARE_MESSAGE_MAP()
};
