#include "FaceSelect.h"
#include "FaceSel.h"


static CFaceSelect g_FaceSelect;

void SetLightMode(int iMode)
{
	g_FaceSelect.SetLightMode( iMode );
}

void AddInImage( IplImage* pImg, CvRect roi )
{
	g_FaceSelect.AddInImage( pImg, roi );
}

char* SelectBestImage()
{
	return g_FaceSelect.SelectBestImage( );
}

void SetROI( int x, int y, int width, int height )
{
	g_FaceSelect.SetROI( x, y, width, height );
}
void SetFaceParas( int iMinFace, double dFaceChangeRatio )
{
	g_FaceSelect.SetFaceParas( iMinFace, dFaceChangeRatio );
}
void SetDwSmpRatio( double dRatio )
{
	g_FaceSelect.SetDwSmpRatio( dRatio );
}
void SetOutputDir( const char* dir )
{
	g_FaceSelect.SetOutputDir( dir );
}
void SetExRatio( double topExRatio, double bottomExRatio, double leftExRatio, double rightExRatio )
{
	g_FaceSelect.SetExRatio( topExRatio, bottomExRatio, leftExRatio, rightExRatio );
}




/////////////////////////////////20090716 newly defined/////////////////////////////////////////
void AddInFrame(Frame *frame)//依次添加一组图片
{
	g_FaceSelect.AddInFrame( frame );
}
int SearchFaces(Target** targets)//一组添加完后，调用这个函数，返回脸数目
{
	return g_FaceSelect.SearchFaces( targets );
}
void ReleaseMem()
{
	g_FaceSelect.ReleaseTargets();
}