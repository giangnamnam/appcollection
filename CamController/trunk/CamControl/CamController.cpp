#include "StdAfx.h"
#include "CamController.h"
#include "comport.h"
#include "packetpacker.h"

CCamController::CCamController(void)
{
	this->pComPort = new CComPort();
	this->pPackPacker = new CPacketPacker();
}

CCamController::~CCamController(void)
{
	if (this->pComPort != NULL)
	{
		delete this->pComPort;
	}

	if (this->pPackPacker != NULL)
	{
		delete this->pPackPacker;
	}
}


void CCamController::SetBaundRate(int baundRate)
{
	this->pComPort->SetBaundRate(baundRate);
}


bool CCamController::Open(LPCTSTR port)
{
	return this->pComPort->Open(port);
}

bool CCamController::SetShutterSpeed(byte cameraID, byte shutterSpeed)
{
	const CByteArray *pData = this->pPackPacker->PackShutterSpeedCmd(cameraID, shutterSpeed);
	return this->WriteComPort(pData);
}

bool CCamController::WriteComPort(const CByteArray *pData)
{
	int count = pData->GetSize();
	int byteSent = this->pComPort->WriteBytes(pData->GetData(), pData->GetSize());
	
	return count == byteSent;
}

bool CCamController::SetZengYi(byte cameraID, byte zengyi)
{
	const CByteArray* pData = this->pPackPacker->PackCameraZengYiCmd(cameraID, zengyi);
	return this->WriteComPort(pData);
}


bool CCamController::SetCameraID(byte ID)
{
	const CByteArray* pData = this->pPackPacker->PackCameraIDCmd(ID);
	return this->WriteComPort(pData);
}


bool CCamController::SetInitialShutterSpeed(byte cameraID, byte shutterSpeed)
{

	const CByteArray* pData = this->pPackPacker->PackFixedShutterSpeedCmd(cameraID, shutterSpeed);
	return this->WriteComPort(pData);
}

