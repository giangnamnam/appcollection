#pragma once

class CComPort;
class CPacketPacker;

class __declspec(dllexport) CCamController
{
public:
	CCamController(void);
	~CCamController(void);

	void SetBaundRate(int baundRate);
	bool Open(LPCTSTR port);

	bool SetShutterSpeed(byte cameraID, byte shutterSpeed);
	bool SetZengYi(byte cameraID, byte zengyi);
	bool SetCameraID(byte ID);
	bool SetInitialShutterSpeed(byte cameraID, byte shutterSpeed);


private:
	CComPort *pComPort;
	CPacketPacker *pPackPacker;
	bool WriteComPort(const CByteArray *pData);
};
