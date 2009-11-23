#include "FaceSelect.h"
#include "FaceSel.h"



void ReleaseImageArray( ImageArray &images )
{
	int i = 0;
	for( i = 0; i < images.nImageCount; i++ )
	{
		cvReleaseImage( &images.imageArr[i] );
	}
	images.nImageCount = 0;
	delete[] images.imageArr;
	images.imageArr = 0;
}
//////////////////////////End -- 20090929 Added for Face Recognition///////////////////////////