
// CameraControllerClient.h : PROJECT_NAME Ӧ�ó������ͷ�ļ�
//

#pragma once

#ifndef __AFXWIN_H__
	#error "�ڰ������ļ�֮ǰ������stdafx.h�������� PCH �ļ�"
#endif

#include "resource.h"		// ������


// CCameraControllerClientApp:
// �йش����ʵ�֣������ CameraControllerClient.cpp
//

class CCameraControllerClientApp : public CWinAppEx
{
public:
	CCameraControllerClientApp();

// ��д
	public:
	virtual BOOL InitInstance();

// ʵ��

	DECLARE_MESSAGE_MAP()
};

extern CCameraControllerClientApp theApp;