#include "StdAfx.h"
#include "PacketPacker.h"

CPacketPacker::CPacketPacker(void)
				: cameraIDOffset(8),
				  cmdIDOffset(9), 
				  cmdParmOffset(11)
{
	//³õÊ¼»¯buffer
	const int len = 13;
	this->byteBuffer.SetSize(len);
	BYTE *pData = this->byteBuffer.GetData();
	BYTE data[len] = {0xaa,0xaa,0xaa,0xaa,0xaa,0xaa,0xaa,0x24,0x00,0x02,0x00,0x00,0x00};
	memcpy(pData, data, len);

}

CPacketPacker::~CPacketPacker(void)
{
}


const CByteArray* CPacketPacker::PackShutterSpeedCmd(byte cameraID, byte shutterSpeed)
{

	return this->PackCommand(cameraID, SetShutterSpeed, shutterSpeed);
}

const CByteArray* CPacketPacker::PackCameraZengYiCmd(byte cameraID, byte zengyi)
{

	return this->PackCommand(cameraID, SetZengYi, zengyi);
}

const CByteArray* CPacketPacker::PackCameraIDCmd(byte ID)
{

	return this->PackCommand(0xff, SetCameraID, ID);
}

const CByteArray* CPacketPacker::PackFixedShutterSpeedCmd(byte cameraID, byte shutterSpeed)
{
	return this->PackCommand(cameraID, SetFixedShutterSpeed,  shutterSpeed);
}

const CByteArray* CPacketPacker::PackCommand(byte cameraID, CommandID cmdID, byte cmdParm)
{
	this->byteBuffer[this->cameraIDOffset] = cameraID;
	this->byteBuffer[this->cmdIDOffset] = cmdID;
	this->byteBuffer[this->cmdParmOffset] = cmdParm;

	return  &(this->byteBuffer);
}




