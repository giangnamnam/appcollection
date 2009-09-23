
// CameraControllerClientDlg.cpp : ʵ���ļ�
//

#include "stdafx.h"
#include "CameraControllerClient.h"
#include "CameraControllerClientDlg.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#endif


// ����Ӧ�ó��򡰹��ڡ��˵���� CAboutDlg �Ի���

class CAboutDlg : public CDialog
{
public:
	CAboutDlg();

// �Ի�������
	enum { IDD = IDD_ABOUTBOX };

	protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV ֧��

// ʵ��
protected:
	DECLARE_MESSAGE_MAP()
};

CAboutDlg::CAboutDlg() : CDialog(CAboutDlg::IDD)
{
}

void CAboutDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
}

BEGIN_MESSAGE_MAP(CAboutDlg, CDialog)
END_MESSAGE_MAP()


// CCameraControllerClientDlg �Ի���




CCameraControllerClientDlg::CCameraControllerClientDlg(CWnd* pParent /*=NULL*/)
	: CDialog(CCameraControllerClientDlg::IDD, pParent)
	, comPort(_T(""))
	, destCameraID(0)
	, shutterSpeed(0)
	, zengYi(0)
	, newCameraID(0)
	, bootShutterSpeed(0)
{
	m_hIcon = AfxGetApp()->LoadIcon(IDR_MAINFRAME);
}

void CCameraControllerClientDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
	DDX_Text(pDX, IDC_EDIT_COM_PORT, comPort);
	DDX_Text(pDX, IDC_EDIT_DST_CAMERA, destCameraID);
	DDX_Text(pDX, IDC_EDIT_SHUTTERSPEED, shutterSpeed);
	DDX_Text(pDX, IDC_EDIT_ZENGYI, zengYi);
	DDX_Text(pDX, IDC_EDIT_NEW_CAMERA_ID, newCameraID);
	DDX_Text(pDX, IDC_EDIT_BOOT_SHUTTER_SPEED, bootShutterSpeed);
}

BEGIN_MESSAGE_MAP(CCameraControllerClientDlg, CDialog)
	ON_WM_SYSCOMMAND()
	ON_WM_PAINT()
	ON_WM_QUERYDRAGICON()
	//}}AFX_MSG_MAP
	ON_BN_CLICKED(IDC_BUTTON_OPENPORT, &CCameraControllerClientDlg::OnBnClickedButtonOpenport)
	ON_BN_CLICKED(IDC_BUTTON_SET_SHUTTER, &CCameraControllerClientDlg::OnBnClickedButtonSetShutter)
	ON_BN_CLICKED(IDC_BUTTON_SET_ZENGYI, &CCameraControllerClientDlg::OnBnClickedButtonSetZengyi)
	ON_BN_CLICKED(IDC_BUTTON_SET_NEW_CAMERA_ID, &CCameraControllerClientDlg::OnBnClickedButtonSetNewCameraId)
	ON_BN_CLICKED(IDC_BUTTON_SET_BOOT_SHUTTeR, &CCameraControllerClientDlg::OnBnClickedButtonSetBootShutter)
END_MESSAGE_MAP()


// CCameraControllerClientDlg ��Ϣ�������

BOOL CCameraControllerClientDlg::OnInitDialog()
{
	CDialog::OnInitDialog();

	// ��������...���˵�����ӵ�ϵͳ�˵��С�

	// IDM_ABOUTBOX ������ϵͳ���Χ�ڡ�
	ASSERT((IDM_ABOUTBOX & 0xFFF0) == IDM_ABOUTBOX);
	ASSERT(IDM_ABOUTBOX < 0xF000);

	CMenu* pSysMenu = GetSystemMenu(FALSE);
	if (pSysMenu != NULL)
	{
		BOOL bNameValid;
		CString strAboutMenu;
		bNameValid = strAboutMenu.LoadString(IDS_ABOUTBOX);
		ASSERT(bNameValid);
		if (!strAboutMenu.IsEmpty())
		{
			pSysMenu->AppendMenu(MF_SEPARATOR);
			pSysMenu->AppendMenu(MF_STRING, IDM_ABOUTBOX, strAboutMenu);
		}
	}

	// ���ô˶Ի����ͼ�ꡣ��Ӧ�ó��������ڲ��ǶԻ���ʱ����ܽ��Զ�
	//  ִ�д˲���
	SetIcon(m_hIcon, TRUE);			// ���ô�ͼ��
	SetIcon(m_hIcon, FALSE);		// ����Сͼ��

	// TODO: �ڴ���Ӷ���ĳ�ʼ������

	return TRUE;  // ���ǽ��������õ��ؼ������򷵻� TRUE
}

void CCameraControllerClientDlg::OnSysCommand(UINT nID, LPARAM lParam)
{
	if ((nID & 0xFFF0) == IDM_ABOUTBOX)
	{
		CAboutDlg dlgAbout;
		dlgAbout.DoModal();
	}
	else
	{
		CDialog::OnSysCommand(nID, lParam);
	}
}

// �����Ի��������С����ť������Ҫ����Ĵ���
//  �����Ƹ�ͼ�ꡣ����ʹ���ĵ�/��ͼģ�͵� MFC Ӧ�ó���
//  �⽫�ɿ���Զ���ɡ�

void CCameraControllerClientDlg::OnPaint()
{
	if (IsIconic())
	{
		CPaintDC dc(this); // ���ڻ��Ƶ��豸������

		SendMessage(WM_ICONERASEBKGND, reinterpret_cast<WPARAM>(dc.GetSafeHdc()), 0);

		// ʹͼ���ڹ����������о���
		int cxIcon = GetSystemMetrics(SM_CXICON);
		int cyIcon = GetSystemMetrics(SM_CYICON);
		CRect rect;
		GetClientRect(&rect);
		int x = (rect.Width() - cxIcon + 1) / 2;
		int y = (rect.Height() - cyIcon + 1) / 2;

		// ����ͼ��
		dc.DrawIcon(x, y, m_hIcon);
	}
	else
	{
		CDialog::OnPaint();
	}
}

//���û��϶���С������ʱϵͳ���ô˺���ȡ�ù��
//��ʾ��
HCURSOR CCameraControllerClientDlg::OnQueryDragIcon()
{
	return static_cast<HCURSOR>(m_hIcon);
}



void CCameraControllerClientDlg::OnBnClickedButtonOpenport()
{
	// TODO: Add your control notification handler code here
	UpdateData();

	bool suc = this->camController.Open(this->comPort);
	if (suc)
	{
		AfxMessageBox(L"���ڴ򿪳ɹ�");
	}
	else if (!suc)
	{
		AfxMessageBox(L"���ڴ�ʧ��");
	}
}

void CCameraControllerClientDlg::OnBnClickedButtonSetShutter()
{
	// TODO: Add your control notification handler code here
	UpdateData();
	this->camController.SetShutterSpeed(this->destCameraID, this->shutterSpeed);
}

void CCameraControllerClientDlg::OnBnClickedButtonSetZengyi()
{
	// TODO: Add your control notification handler code here
	UpdateData();
	this->camController.SetZengYi(this->destCameraID, this->zengYi);
}

void CCameraControllerClientDlg::OnBnClickedButtonSetNewCameraId()
{
	// TODO: Add your control notification handler code here
	UpdateData();
	this->camController.SetCameraID(this->newCameraID);
}

void CCameraControllerClientDlg::OnBnClickedButtonSetBootShutter()
{
	// TODO: Add your control notification handler code here
	UpdateData();
	this->camController.SetInitialShutterSpeed(this->destCameraID, this->shutterSpeed);
}
