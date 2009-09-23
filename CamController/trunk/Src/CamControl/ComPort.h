#pragma once

class CComPort
{
public:
	CComPort(void);
	~CComPort(void);

	void SetBaundRate(int baundRate);
	bool Open(LPCTSTR port);
	void Close();
	DWORD WriteBytes(const BYTE* pData, int length);

private:
	int baundRate;
	HANDLE hComPort;
	BYTE byteSize;
	BYTE parity;
	BYTE stopBits;


};
