 /*******************************************************************
 *		Face Recognition Module
 *    DESCRIPTION:
 *		face data base
 *              /__________faceNum___________\
 *              \                            /
 *		 / \    image1 image2 image3 ... imageN
 *        |		image1 image2 image3 ... imageN
 *        |		image1 image2 image3 ... imageN
 *        |		image1 image2 image3 ... imageN
 *  imageLen	image1 image2 image3 ... imageN
 *        |		image1 image2 image3 ... imageN
 *        |		image1 image2 image3 ... imageN
 *       \ /    image1 image2 image3 ... imageN
 *      The output (similarity, float type) is stored at similarityResultPtr one by one
 *		  | similarity#1 | similarity#2 | similarity#i | similarity#N | 
 *      NOTE: similarity result#i is the similarity btw image to be recoginized and i-th train face image 
 *    AUTHOR:
 *		Zeng Yinhui, zengyinhui@msn.com
 *    HISTORY:
 *		<10/21/2009><Zeng Yinhui> Init Version.
 *		<10/24/2009><Zeng Yinhui> Updated the similarity definition
 *		<11/03/2009><Zeng Yinhui> Updated the interface
 *******************************************************************/
#include <stdio.h>
#include <iostream>
#include <limits>
#include <complex>		
#include <algorithm>
#include <vector>
#include <math.h>
#include <fstream>

#include "Matrix.h"		
#include "EigenvalueVector.h" //header for eigen

//openCV to pre-process img, such as img resize
//#define  SKIP_INCLUDES
//#include <afxwin.h>
#include "stdafx.h"

#include "cv.h"
#include "highgui.h"
#include "cxcore.h"


using namespace std;	

/*macro definition */
#define ORIGINAL_IMG_WIDTH	200
#define ORIGINAL_IMG_HEIGHT 200
#define ORIGINAL_IMG_LEN (ORIGINAL_IMG_WIDTH*ORIGINAL_IMG_HEIGHT)


struct similarityMat
{
	float similarity;
	char *fileName;
};


/*global variable definition */

CString gc_faceSampleRoot = "C:\\faceRecognition\\faceSample\\*.jpg";
CString gc_eigenVectorFile = "C:\\faceRecognition\\data\\EigenVector.txt";
CString gc_AverageValueFile = "C:\\faceRecognition\\data\\AverageValue.txt";
CString gc_SampleCoefficientFile = "C:\\faceRecognition\\data\\SampleCoefficient.txt";
CString gc_FileNameFile = "C:\\faceRecognition\\data\\FileName.txt";

char** gc_sampleFileName;//定义样本的文件名
int gi_sampleCount;

matrixf* meanVectorPtr;
matrixf* trainedEigenSpaceMatPtr;
matrixf* signatureFaceDBPtr;

/*local function definition */
bool smallFront (int i,int j) { return (i<j); }
bool bigFront (int i,int j) { return (i>j); }

void myLog(char* myLog)
{
	CTime time1;
	time1 = CTime::GetCurrentTime();
	int hour1 = time1.GetHour();
	int mins1 = time1.GetMinute();
	int sec1 = time1.GetSecond();
	cout<<"\n"<<hour1<<":"<<mins1<<":"<<sec1<<"> "<<myLog<<endl;
}

#define TIMELOG(X) //myLog(X)

//return sample count
extern "C" int _declspec(dllexport) FaceTraining(int imgWidth=20, int imgHeight=20, int eigenNum=40)
{
//read image samples to get the number and file list.
	CFileFind imageFile;
	int sampleCount = 0;//训练样本的图片个数

TIMELOG("enter face training...");
	bool FileExist = imageFile.FindFile(_T(gc_faceSampleRoot),0); 
	
	FILE* fp;
	if (!(fp=fopen(gc_FileNameFile,"w")))
	{

		return -1;
	}

	while (FileExist)
	{
		FileExist = imageFile.FindNextFile();   
		if (!imageFile.IsDots()) 
		{
			fprintf(fp,"%s\n",imageFile.GetFilePath());
			sampleCount++;//遍历得到训练样本图片的总数
		}
	}
	fclose(fp);

TIMELOG("load img samples to faceDBPtr...");
//load img samples to faceDBPtr

	if (!(fp=fopen(gc_FileNameFile,"r")))
	{
		return -1;
	}
	int i=0;
	IplImage *bigImg;
	IplImage *smallImg;
	smallImg = cvCreateImage(cvSize(imgWidth, imgHeight), 8, 1);
	int smallImgLen = imgHeight*imgWidth;
	int height = smallImg->height;
	int width = smallImg->width;
	int step = smallImg->widthStep;
	
	matrixf faceDBMat(smallImgLen,sampleCount);
	
	for (i=0;i<sampleCount;i++)
	{
		char sPath[200];
		fscanf(fp,"\n%s",sPath);
		bigImg = cvLoadImage(sPath, 0);
		cvResize(bigImg, smallImg, CV_INTER_LINEAR); //resize image to small size
		cvReleaseImage(&bigImg);

		uchar *smallImgData = (uchar*)smallImg->imageData;

		for (int h=0; h<height; h++)
		{
			for (int w=0; w<width; w++)
			{
				faceDBMat(h*step+w,i) = smallImgData[h*step+w];
			}
		}
	}
	fclose(fp);
	cvReleaseImage(&smallImg);

TIMELOG("calc eigen vector/value...");

//calc eigen vector/value
	//calc the mean matrix
	matrixf meanVector(smallImgLen,1); //store mean result
	matrixf oneVector(1,sampleCount);//
	matrixf meanMat(smallImgLen,sampleCount);//every column is same: meanVector
	matrixf faceDBzmMat(smallImgLen,sampleCount);
	
	oneVector =0*oneVector;
	oneVector -=-1;// oneVector = oneVector + 1;

	MatrixMean(faceDBMat,meanVector,2);//calc mean along with column
	
	meanMat = meanVector * oneVector;
	meanMat = -meanMat;
	faceDBzmMat = faceDBMat + meanMat; // remove mean
	
	
	matrixf faceDBzmMatT(sampleCount,smallImgLen);//transpose matrix
	MatrixTranspose(faceDBzmMat,faceDBzmMatT);
	
	matrixf eigInMat(sampleCount,sampleCount);
	matrixf eigVectorMat(sampleCount,sampleCount);
	eigInMat = faceDBzmMatT * faceDBzmMat;//////////////////////////////////////////////////////////////////////////take too much time
	
	//calc the eigen.
	//diagonal cell in eigInMat is eigValue.
	//eigVectoreMat store the eig vector
	//EigenvalueVectorRealSymmetryJacobiB(eigInMat,eigVectorMat,(float)0.00000000001); 
	EigenvalueVectorRealSymmetryJacobi(eigInMat,eigVectorMat,(float)0.00000000001,200); //////////////////////////////////////////////////////////////////////////
	matrixf eigVectorFinalMat(smallImgLen,sampleCount); //V=single(vzm)*V;
	eigVectorFinalMat = faceDBzmMat * eigVectorMat;//////////////////////////////////////////////////////////////////////////too much time here

	float* eigValue = new float[sampleCount];
	for(i=0;i<sampleCount;i++){
		eigValue[i] = eigInMat(i,i);
	}
	vector<float> eigOriginalVector(eigValue,eigValue+sampleCount);	//store eigValue into vector in order to be easy to find.
	
	vector<float> eigSortVector(eigValue,eigValue+sampleCount);	//store eigValue into vector in order to be easy to find.
	vector<float>::iterator eigSortIt;
	
	//sort eigValue by big one in front
	sort(eigSortVector.begin(), eigSortVector.end(),bigFront); 
	
	//pick up the eigNum eigVectors of largest eigvalue
	//store to trainedEigenSpaceMat, eigenNum eigen vectors are picked up.
	//V=V(:,end:-1:end-(N-1)); 

	matrixf trainedEigenSpaceMat(smallImgLen,eigenNum);
	eigSortIt = eigSortVector.begin();
	int offset=0;
	
	for(i=0;i<eigenNum;i++){
		offset = find(eigOriginalVector.begin(),eigOriginalVector.end(),*(eigSortVector.begin()+i))-eigOriginalVector.begin();//search n-th biggest eigen value.
		for(int row=0;row<smallImgLen;row++){
			trainedEigenSpaceMat(row,i) = eigVectorFinalMat(row,offset);//pick up the eigen vector with n-th biggest eigen value.
		}
	}
	//calc signature for each face image
	//Each row in signatureFaceDB is the signature for one image.

	matrixf signatureFaceDB(sampleCount,eigenNum);
	signatureFaceDB = faceDBzmMatT * trainedEigenSpaceMat;	

TIMELOG("store eigen vector/value/train image path to file...");
//store eigen vector/value/train image path to file
	//store mean value, smallImgLen lines
	if (!(fp=fopen(gc_AverageValueFile,"w")))
	{
		return false;
	}
	else
	{
		for (i=0;i<smallImgLen;i++)
		{
			fprintf(fp,"%f\n",meanVector(i,0));
		}
	}
	fclose(fp);
	//store eigen vector, smallImgLen*eigenNum
	if (!(fp=fopen(gc_eigenVectorFile,"w")))
	{
		return false;
	}
	else
	{
		for (int row=0;row<smallImgLen;row++)
		{
			for (int col=0;col<eigenNum;col++)
			{
				fprintf(fp,"%12.8f ",trainedEigenSpaceMat(row,col));
			}
			fprintf(fp,"\n");
		}
	}
	fclose(fp);
	//store coefficient that sample in eigen vector, smallImgLen*eigenNum
	if (!(fp=fopen(gc_SampleCoefficientFile,"w")))
	{
		return false;
	}
	else
	{
		for (int row=0;row<sampleCount;row++)
		{
			for (int col=0;col<eigenNum;col++)
			{
				fprintf(fp,"%12.8f ",signatureFaceDB(row,col));
			}
			fprintf(fp,"\n");
		}
	}
	fclose(fp);

TIMELOG("training is done");
	return sampleCount;
}

extern "C"  bool _declspec(dllexport) InitData( int sampleCount, int imgLen=400, int eigenNum=40)
{
TIMELOG("InitData Entry");
	meanVectorPtr = new matrixf(imgLen,1); //store mean result
	trainedEigenSpaceMatPtr = new matrixf(imgLen,eigenNum);
	signatureFaceDBPtr = new matrixf (sampleCount,eigenNum);

	FILE* fp;
	int i;

	gc_sampleFileName = new char*[sampleCount];
	gi_sampleCount = sampleCount;
	if (!(fp=fopen(gc_FileNameFile,"r")))
	{
		return false;
	}
	for (i=0;i<sampleCount;i++)
	{
		gc_sampleFileName[i] = new char[200];
		fscanf(fp,"%s\n",gc_sampleFileName[i]);
	}
	fclose(fp);

	//load eigen vector/value/train image path to file
	//load mean value, smallImgLen lines
	if (!(fp=fopen(gc_AverageValueFile,"r")))
	{
		return false;
	}
	else
	{
		for (i=0;i<imgLen;i++)
		{
			fscanf(fp,"%f\n",&((*meanVectorPtr)(i,0)));
		}
	}
	fclose(fp);
	//MatrixLinePrint(*meanVectorPtr);
	//load eigen vector, smallImgLen*eigenNum
	if (!(fp=fopen(gc_eigenVectorFile,"r")))
	{
		return false;
	}
	else
	{
		for (int row=0;row<imgLen;row++)
		{
			for (int col=0;col<eigenNum;col++)
			{
				fscanf(fp,"%f ",&((*trainedEigenSpaceMatPtr)(row,col)));
			}
		}
	}
	fclose(fp);
	//load coefficient that sample in eigen vector, smallImgLen*eigenNum
	if (!(fp=fopen(gc_SampleCoefficientFile,"r")))
	{
		return false;
	}
	else
	{
		for (int row=0;row<sampleCount;row++)
		{
			for (int col=0;col<eigenNum;col++)
			{
				fscanf(fp,"%f ",&((*signatureFaceDBPtr)(row,col)));
			}
			//fscanf(fp,"\n");
		}
	}
	fclose(fp);
TIMELOG("IniData End");
	//MatrixLinePrint(*signatureFaceDBPtr);
	return true;
}

extern "C" void _declspec(dllexport) FaceRecognition(float*currentFace, int sampleCount, similarityMat* similarityPtr, int imgLen, int eigenNum=40)
{
TIMELOG("Entry FaceRecognition");
	//fill the image to be recognized into matrix
	matrixf imageMat(currentFace,imgLen, 1); 
	//calc signature for image needing to be recognized
	matrixf signatureImage(1,eigenNum);
	matrixf imageZmMat(imgLen, 1); 
	matrixf imageZmMatT(1,imgLen); 
	imageZmMat = imageMat - (*meanVectorPtr); //col vector of imageLen,p=r-m; 
	MatrixTranspose(imageZmMat,imageZmMatT);

	signatureImage = imageZmMatT * (*trainedEigenSpaceMatPtr); //s=single(p)'*V;
	
	float deltaSum=0.0;
	float myMin=(float)3.40282e+038;//numeric_limits<float>::max();
	float myMax=(float)1.17549e-038;//numeric_limits<float>::min();
	int faceIndex=0;

	//calc the normalized delta btw image and face data base image.
	for(faceIndex=0;faceIndex<sampleCount;faceIndex++){
		//fetch signature of one face, store to signatureTem
		deltaSum = 0.0;
		for(int j=0;j<eigenNum;j++){
			deltaSum += fabs((*signatureFaceDBPtr)(faceIndex,j) - signatureImage(0,j));
		}
		//store sum of absolute of the delta btw signatureImage & signatureFaceDB
		similarityPtr[faceIndex].similarity = deltaSum;
		//store max/min
		myMin = Min(myMin,deltaSum);
		myMax = Max(myMax,deltaSum);
	}
	//calc the percents on every trained face image: %=(face[i]-min)/(max-min)
	for(faceIndex=0;faceIndex<sampleCount;faceIndex++)
	{
		similarityPtr[faceIndex].similarity = 1 - (similarityPtr[faceIndex].similarity-myMin) / (myMax-myMin);
		similarityPtr[faceIndex].fileName	= gc_sampleFileName[faceIndex];
	}
TIMELOG("End FaceRecognition");
}

extern "C"  void _declspec(dllexport) FreeData()
{
	for (int i=0;i<gi_sampleCount;i++)
	{
		delete []gc_sampleFileName[i];
	}
	delete []gc_sampleFileName;
	
	delete meanVectorPtr;
	delete trainedEigenSpaceMatPtr;
	delete signatureFaceDBPtr;
}





//end