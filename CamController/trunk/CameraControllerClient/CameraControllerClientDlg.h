#include "..\CamControl\CamController.h"

// CameraControllerClientDlg.h : ͷ�ļ�
//

#pragma once


// CCameraControllerClientDlg �Ի���
class CCameraControllerClientDlg : public CDialog
{
// ����
public:
	CCameraControllerClientDlg(CWnd* pParent = NULL);	// ��׼���캯��

// �Ի�������
	enum { IDD = IDD_CAMERACONTROLLERCLIENT_DIALOG };

	protected:
	virtual void DoDataExchange(CDataExchange* pDX);	// DDX/DDV ֧��


// ʵ��
protected:
	HICON m_hIcon;

	// ���ɵ���Ϣӳ�亯��
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
