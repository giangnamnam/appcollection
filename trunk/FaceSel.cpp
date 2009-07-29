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
void AddInFrame(Frame *frame)//�������һ��ͼƬ
{
	g_FaceSelect.AddInFrame( frame );
}
int SearchFaces(Target** targets)//һ�������󣬵��������������������Ŀ
{
	return g_FaceSelect.SearchFaces( targets );
}
void ReleaseMem()
{
	g_FaceSelect.ReleaseTargets();
}