#pragma once

#include "command.h"


class CPacketPacker
{
public:
	CPacketPacker(void);
	~CPacketPacker(void);

	const CByteArray* PackShutterSpeedCmd(byte cameraID, byte shutterSpeed);
	const CByteArray* PackCameraZengYiCmd(byte cameraID, byte zengyi);
	const CByteArray* PackCameraIDCmd(byte ID);
	const CByteArray* PackFixedShutterSpeedCmd(byte cameraID, byte shutterSpeed);
	const CByteArray* PackCommand(byte cameraID, CommandID cmdID, byte cmdParm);

private:
	CByteArray byteBuffer;
	const int cameraIDOffset;
	const int cmdIDOffset;
	const int cmdParmOffset;
};
