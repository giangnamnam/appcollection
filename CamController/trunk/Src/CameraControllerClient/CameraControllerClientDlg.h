#include "..\CamControl\CamController.h"

// CameraControllerClientDlg.h : 头文件
//

#pragma once


// CCameraControllerClientDlg 对话框
class CCameraControllerClientDlg : public CDialog
{
// 构造
public:
	CCameraControllerClientDlg(CWnd* pParent = NULL);	// 标准构造函数

// 对话框数据
	enum { IDD = IDD_CAMERACONTROLLERCLIENT_DIALOG };

	protected:
	virtual void DoDataExchange(CDataExchange* pDX);	// DDX/DDV 支持


// 实现
protected:
	HICON m_hIcon;

	// 生成的消息映射函数
	virtual BOOL OnInitDialog();
	afx_msg void OnSysCommand(UINT nID, LPARAM lParam);
	afx_msg void OnPaint();
	afx_msg HCURSOR OnQueryDragIcon();
	DECLARE_MESSAGE_MAP()
private:
	CCamController camController;

public:
	CString comPort;
	BYTE destCameraID;
	BYTE shutterSpeed;
	BYTE zengYi;
	BYTE newCameraID;
	BYTE bootShutterSpeed;
	afx_msg void OnBnClickedButtonOpenport();
	afx_msg void OnBnClickedButtonSetShutter();
	afx_msg void OnBnClickedButtonSetZengyi();
	afx_msg void OnBnClickedButtonSetNewCameraId();
	afx_msg void OnBnClickedButtonSetBootShutter();
};
