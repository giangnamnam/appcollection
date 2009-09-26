// PreProcessClassDll.cpp : Defines the exported functions for the DLL application.
//

#include "stdafx.h"
#include "PreProcessClass.h"
#include "BackGroundUpdate.h"

PreProcess *pPro;
BakGroundUpdate *backGround;  

void InitPreProcess(int num)
{
	pPro = new PreProcess[num];
}
void InitBkImgUpdate(int num)
{
	backGround = new BakGroundUpdate[num];
}
bool PreProcessFrame(Frame frame, Frame *lastFrame, int index)
{
	bool flag = false; 
	flag = pPro[index].PreProcessFrame(frame, lastFrame); 
	return flag;
}
void SetDrawRect(bool draw, int index)
{
	pPro[index].SetDrawRect(draw);
}
void SetAlarmArea(const int leftX, const int leftY, const int rightX, const int rightY, bool draw, int index)
{
	pPro[index].SetAlarmArea(leftX, leftY, rightX, rightY, draw);
}
void SetRectThr(const int fCount, const int gCount, int index)
{
	pPro[index].SetRectThr(fCount, gCount);
}
bool BkImgUpdate(IplImage *curColorImg, IplImage *bkImg, int index)
{
	bool flag = false;
	flag = backGround[index].BkImgUpdate(curColorImg, bkImg);
	return flag;
}

