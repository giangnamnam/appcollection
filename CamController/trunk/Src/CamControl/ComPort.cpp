#include "StdAfx.h"
#include "ComPort.h"

CComPort::CComPort(void)
{
	this->baundRate = 9600;
	this->byteSize = 8;
	this->parity = NOPARITY;
	this->stopBits = ONESTOPBIT;
	this->hComPort = INVALID_HANDLE_VALUE;
}

CComPort::~CComPort(void)
{
	this->Close();
}


void CComPort::SetBaundRate(int baundRate)
{
	this->baundRate = baundRate;

}

bool CComPort::Open(LPCTSTR port)
{
	this->hComPort  = CreateFile( port,
									GENERIC_READ | GENERIC_WRITE,
									0,    // must be opened with exclusive-access
									NULL, // default security attributes
									OPEN_EXISTING, // must use OPEN_EXISTING
									0,    // not overlapped I/O
									NULL  // hTemplate must be NULL for comm devices
									);

	if (this->hComPort == INVALID_HANDLE_VALUE)
	{
		return FALSE;
	}

	DCB dcb;
	SecureZeroMemory(&dcb, sizeof(DCB));
	dcb.DCBlength = sizeof(DCB);
	BOOL suc = ::GetCommState(this->hComPort, &dcb);

	if (!suc)
	{
		this->Close();
		return FALSE;
	}

	dcb.BaudRate = this->baundRate;
	dcb.ByteSize = this->byteSize;
	dcb.Parity = NOPARITY;
	dcb.StopBits = ONESTOPBIT;

	suc = ::SetCommState(this->hComPort, &dcb);
	if (!suc)
	{
		this->Close();
		return FALSE;
	}

	return TRUE;

}


void CComPort::Close()
{
	if (this->hComPort != INVALID_HANDLE_VALUE)
	{
		::CloseHandle(this->hComPort);
		this->hComPort = INVALID_HANDLE_VALUE;
	}
}


DWORD CComPort::WriteBytes(const BYTE* pData, int length)
{
	if (this->hComPort != INVALID_HANDLE_VALUE)
	{
		DWORD count = 0;
		::WriteFile(this->hComPort, pData, length, &count, NULL);
		return count;
	}

	return 0;

}

