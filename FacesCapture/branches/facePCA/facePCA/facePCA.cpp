// facePCA.cpp : Defines the exported functions for the DLL application.
/*******************************************************************************************************
Copyright (c) �ɶ�������Ƽ����޹�˾
All right reserved!

�ļ�����facePCA.cpp
ժ  Ҫ������PCA���������������ѵ��ѧϰ��������Ӧ������Ϊtxt�ļ��������ڴ����룬�ͷ��ڴ档��������ʶ��
        �ȶԣ����������ƶȼ���Ӧ���ļ�����

��    �ߣ�Ѧ����
������ڣ�2009��11��19�� 
��ǰ�汾��1.1
*******************************************************************************************************/
#include "stdafx.h"
#include "facePCA.h" 

/***************************************************************************************
FaceTraining(int imgWidth, int imgHeight, int eigenNum)
˵����
imgWidth:����ʶ������ź��ͼƬ�Ŀ�ȣ��ò���Ϊ�������ڲ�ʹ��
imgHeight:����ʶ������ź��ͼƬ�ĸ߶�,�ò���Ϊ�������ڲ�ʹ��
eigenNum:ͶӰ�ռ��ά��
***************************************************************************************/
FACEPCA_API int FaceTraining(int imgWidth, int imgHeight, int eigenNum)
{
	CFileFind imageFile;
	CString fileName;
	CString imgFileAdd = "C:\\faceRecognition\\faceSample\\091028212230000_0000.jpg";  

	int sampleCount = 0;//ѵ��������ͼƬ����

	bool FileExist = imageFile.FindFile(_T("C:\\faceRecognition\\faceSample\\*.jpg"),0); 
	while (FileExist)
	{
		FileExist = imageFile.FindNextFileW(); 
		if (!imageFile.IsDots()) 
		{
			sampleCount++;//�����õ�ѵ������ͼƬ������
		}
	}

	if (eigenNum > sampleCount)
	{
		cvSetErrMode(CV_ErrModeParent);
		cvGuiBoxReport(CV_StsBadArg, "FaceTraining", "sampleCount should greater than(or equal to) eigenNum", __FILE__, __LINE__, NULL);
	}

	char *imgFileName[50]; //����ָ�����������洢�����ļ��� 
	for (int i=0; i<50; i++)
	{
		imgFileName[i] = new char[sampleCount];
	}

	CvMat *faceDB = cvCreateMat(imgWidth*imgHeight, sampleCount, CV_32FC1);//ѵ������������ɵľ���ÿ������Ϊһ��
	CvMat *selEigenVector = cvCreateMat(eigenNum, sampleCount, CV_32FC1);
	CvMat *EigenVectorFinal = cvCreateMat(imgWidth*imgHeight, eigenNum, CV_32FC1);
	CvMat *reltiveMat = cvCreateMat(sampleCount, sampleCount, CV_32FC1);
	CvMat *AvgVector = cvCreateMat(imgWidth*imgHeight, 1, CV_32FC1);//ƽ��ֵ����
	CvMat *EigenValue = cvCreateMat(1, sampleCount, CV_32FC1);//Э������������ֵ
	CvMat *EigenVector = cvCreateMat(sampleCount, sampleCount, CV_32FC1);//Э����������������  
	CvMat *resCoeff = cvCreateMat(sampleCount, eigenNum, CV_32FC1);//ȡǰeigenNum���������ֵ

	int imgCount = 0;

	FileExist = imageFile.FindFile(_T("C:\\faceRecognition\\faceSample\\*.jpg"),0); 
	while (FileExist)
	{
		FileExist = imageFile.FindNextFile();   
		if (!imageFile.IsDots()) 
		{
			fileName = imageFile.GetFileName();
			int fileNameLen = fileName.GetLength();
			for (int i=0; i<fileNameLen; i++)
			{
				imgFileName[i][imgCount] = fileName[i];
			}
			imgFileName[fileNameLen][imgCount] = '\0';

			imgFileAdd = "C:\\faceRecognition\\faceSample\\" + fileName;

			int strLen = imgFileAdd.GetLength();
			char *fileAddress = new char[strLen+1];

			for (int i=0; i<strLen; i++)
			{
				fileAddress[i] = imgFileAdd[i];
			}
			fileAddress[strLen] = '\0';

			IplImage *bigImg = cvLoadImage(fileAddress, 0);

			delete []fileAddress;

			IplImage *smallImg = cvCreateImage(cvSize(imgWidth, imgHeight), 8, 1);
			uchar *smallImgData = (uchar*)smallImg->imageData;

			cvResize(bigImg, smallImg, CV_INTER_LINEAR);

			int height = smallImg->height;
			int width = smallImg->width;

			for (int i=0; i<height; i++)
			{
				for (int j=0; j<width; j++)
				{
					cvmSet(faceDB, i*width+j, imgCount, smallImgData[i*width+j]);
				}
			}

			cvReleaseImage(&bigImg);
			cvReleaseImage(&smallImg);

			imgCount++;

			if (imgCount == sampleCount)
			{

				float faceColSum = 0.0;
				for (int i=0; i<imgWidth*imgHeight; i++)
				{
					faceColSum = 0.0;
					for (int j=0; j<sampleCount; j++)
					{
						faceColSum += (float)cvGetReal2D(faceDB, i, j);
					}
					faceColSum = faceColSum/sampleCount;
					cvmSet(AvgVector, i, 0, faceColSum);
				}

				for (int i=0; i<imgWidth*imgHeight; i++)
				{
					for (int j=0; j<sampleCount; j++)
					{
						cvmSet(faceDB, i, j, (double)(cvGetReal2D(faceDB, i, j) - cvGetReal2D(AvgVector, i, 0)));
					}
				}

				cvMulTransposed(faceDB, reltiveMat, 1);  

				cvEigenVV(reltiveMat, EigenVector, EigenValue, 1.0e-6F);
				for (int i=0; i<eigenNum; i++)
				{
					for (int j=0; j<sampleCount; j++)
					{
						cvmSet(selEigenVector, i, j, (double)cvGetReal2D(EigenVector, i, j));
					}
				}

				cvGEMM(faceDB, selEigenVector, 1, NULL, 0, EigenVectorFinal, CV_GEMM_B_T);

				cvGEMM(faceDB, EigenVectorFinal, 1, NULL, 0, resCoeff, CV_GEMM_A_T); 

				//ƽ��ֵ������ֵ����������
				fstream txtFile;
				bool txtExist = false;

				txtFile.open("C:\\faceRecognition\\data\\AverageValue.txt");
				if (txtFile)
				{
					txtExist = true;
				}
				txtFile.close();

				if (txtExist)
				{
					remove("C:\\faceRecognition\\data\\AverageValue.txt");//����ļ����ڣ���ɾ��
				}

				ofstream out1("C:\\faceRecognition\\data\\AverageValue.txt");//��ƽ��ֵ�������ļ���
				if (out1.fail())
				{
					cvSetErrMode(CV_ErrModeParent);
					cvGuiBoxReport(CV_StsBadArg, "FaceTraining", "file write path error!!", __FILE__, __LINE__, NULL);
				}
				for (int i=0; i<imgWidth*imgHeight; i++)
				{
					out1<<(float)cvGetReal2D(AvgVector, i, 0)<<endl;
				}
				out1.close();

				txtExist = false;
				txtFile.open("C:\\faceRecognition\\data\\EigenVector.txt");
				if (txtFile)
				{
					txtExist = true;
				}
				txtFile.close();

				if (txtExist)
				{
					remove("C:\\faceRecognition\\data\\EigenVector.txt");//����ļ����ڣ���ɾ��
				}

				ofstream out2("C:\\faceRecognition\\data\\EigenVector.txt");//�����������������ļ���
				if (out2.fail())
				{
					cvSetErrMode(CV_ErrModeParent);
					cvGuiBoxReport(CV_StsBadArg, "FaceTraining", "file write path error!!!", __FILE__, __LINE__, NULL);
				}
				for (int i=0; i<EigenVectorFinal->height; i++)
				{
					for (int j=0; j<EigenVectorFinal->width; j++)
					{
						out2<<(float)cvGetReal2D(EigenVectorFinal, i, j)<<" ";
					}
					out2<<endl;
				}
				out2.close();  

				txtExist = false;
				txtFile.open("C:\\faceRecognition\\data\\SampleCoefficient.txt");
				if (txtFile)
				{
					txtExist = true;
				}
				txtFile.close();

				if (txtExist)
				{
					remove("C:\\faceRecognition\\data\\SampleCoefficient.txt");//����ļ����ڣ���ɾ��
				}

				txtExist = false;
				txtFile.open("C:\\faceRecognition\\data\\FileName.txt");
				if (txtFile)
				{
					txtExist = true;
				}
				txtFile.close();

				if (txtExist)
				{
					remove("C:\\faceRecognition\\data\\FileName.txt");//����ļ����ڣ���ɾ��
				}

				ofstream out3("C:\\faceRecognition\\data\\SampleCoefficient.txt");//������ͼƬ��ͶӰϵ���������ļ���
				ofstream out4("C:\\faceRecognition\\data\\FileName.txt");//������ͼƬ���ļ����������ļ���
				if (out3.fail())
				{
					cvSetErrMode(CV_ErrModeParent);
					cvGuiBoxReport(CV_StsBadArg, "FaceTraining", "file write error!!!", __FILE__, __LINE__, NULL);
				}
				if (out4.fail())
				{
					cvSetErrMode(CV_ErrModeParent);
					cvGuiBoxReport(CV_StsBadArg, "FaceTraining", "file write error!!!", __FILE__, __LINE__, NULL); 
				}
				for (int i=0; i<sampleCount; i++)
				{
					for (int j=0; j<fileNameLen; j++)
					{
						out4<<imgFileName[j][i];
					}
					out4<<'\0'<<endl;
					for (int j=0; j<eigenNum; j++)
					{
						out3<<(float)cvGetReal2D(resCoeff, i, j)<<" ";
					}
					out3<<endl; 
				}
				out3.close();
				out4.close();
			}
		}
	}

	//delete []fileAddress;
	for (int i=0; i<50; i++)
	{
		delete []imgFileName[i];
	}
	cvReleaseMat(&faceDB);
	cvReleaseMat(&selEigenVector);
	cvReleaseMat(&EigenVectorFinal);
	cvReleaseMat(&reltiveMat);
	cvReleaseMat(&AvgVector);
	cvReleaseMat(&EigenValue);
	cvReleaseMat(&EigenVector);
	cvReleaseMat(&resCoeff);

	return sampleCount;
}

/**********************************************************************************
void InitData( int sampleCount, int imgLen=400, int eigenNum=40);
˵����
sampleCount:ΪC:\faceRecognition\faceSample\·����ͼƬ�ĸ���
imgLen:��ֵ����FaceTraining����ʱ��imgWidth*imgHeight
eigenNum:ͶӰ�ռ��ά��,Ҳ��������ֵ�ĸ���
**********************************************************************************/
FACEPCA_API void InitData(int sampleCount, int imgLen, int eigenNum)
{
	sampleAvgVal = new float[imgLen];

	ifstream fileIn1("C:\\faceRecognition\\data\\AverageValue.txt");
	if (fileIn1.fail())//����ļ�������
	{
		cvSetErrMode(CV_ErrModeParent);
		cvGuiBoxReport(CV_StsBadArg, "FaceRecognition", "AverageValue.txt was not found!!!", __FILE__, __LINE__, NULL);
	}

	float avgData;
	for (int i=0; i<imgLen; i++)
	{
		fileIn1>>avgData;
		sampleAvgVal[i] = avgData;//��ȡѵ��������ƽ��ֵ������AvgVector
	}
	fileIn1.close();

	sampleEigenVector = new float[imgLen*eigenNum];

	ifstream fileIn2("C:\\faceRecognition\\data\\EigenVector.txt");
	if (fileIn2.fail())
	{
		cvSetErrMode(CV_ErrModeParent);
		cvGuiBoxReport(CV_StsBadArg, "FaceRecognition", "EigenVector.txt was not found!!!", __FILE__, __LINE__, NULL);
	}

	float eigenVectorData;
	for (int i=0; i<imgLen*eigenNum; i++)
	{
		fileIn2>>eigenVectorData;
		sampleEigenVector[i] = eigenVectorData;
	}
	fileIn2.close();

	sampleCoeff = new float[sampleCount*eigenNum];

	ifstream filein3("C:\\faceRecognition\\data\\SampleCoefficient.txt");
	if (filein3.fail())
	{
		cvSetErrMode(CV_ErrModeParent);
		cvGuiBoxReport(CV_StsBadArg, "FaceRecognition", "SampleCoefficient.txt was not found!!!", __FILE__, __LINE__, NULL);
	}

	float refCoeffData;
	for (int i=0; i<sampleCount*eigenNum; i++)
	{
		filein3>>refCoeffData;
		sampleCoeff[i] = refCoeffData;
	}
	filein3.close();

	sampleFileName = new char[sampleCount*50];

	ifstream fileIn4("C:\\faceRecognition\\data\\FileName.txt");
	if (fileIn4.fail())
	{
		cvSetErrMode(CV_ErrModeParent);
		cvGuiBoxReport(CV_StsBadArg, "FaceRecognition", "FileName.txt was not found!!!", __FILE__, __LINE__, NULL);
	}

	char *name = new char[50];
	for (int i=0; i<sampleCount; i++)
	{
		fileIn4.getline(name, 50, '\0');
		//fileIn4.getline(name, 50);
		for (int j=0; name[j-1]!='g'; j++)
		{
			sampleFileName[i*50+j] = name[j];
		}
	}
	fileIn4.close();
	delete []name;
}

/**********************************************************************************
void FreeData();
˵����
�ú�����Ҫ�����ͷ�InitData()������ڴ档
**********************************************************************************/
FACEPCA_API void FreeData()
{

	delete []sampleAvgVal;
	delete []sampleEigenVector;
	delete []sampleCoeff;
	delete []sampleFileName;
}

/*********************************************************************************
void FaceRecognition(float *currentFace, int sampleCount, similarityMat *similarity,int imgLen=400, int eigenNum=40);
˵����
currentFace:��ʶ������ݣ�Ϊһ��imgLen�е�������
sampleCount��ΪC:\faceRecognition\faceSample\·����ͼƬ�ĸ���
similarity:��ʶ��ͼƬ����ͼƬ֮������ƶȣ���������Ӧ������ͼƬ�ļ���
eigenNum:����ֵ�ĸ�����Ҳ����ͶӰ�ռ��ά�� 
*********************************************************************************/
FACEPCA_API void FaceRecognition(float *currentFace, int sampleCount, similarityMat *similarity, int imgLen, int eigenNum)
{
	CvMat *avgVector = cvCreateMat(imgLen, 1, CV_32FC1);//ƽ��ֵ����
	CvMat *eigenVector = cvCreateMat(imgLen, eigenNum, CV_32FC1);//Э����������������  
	CvMat *resCoeff = cvCreateMat(sampleCount, eigenNum, CV_32FC1);//ȡǰeigenNum���������ֵ

	for (int i=0; i<imgLen; i++)
	{
		cvmSet(avgVector, i, 0, sampleAvgVal[i]);
	}

	int eigenCol = 0;
	int eigenRow = 0;
	for (int i=0; i<imgLen*eigenNum; i++)
	{
		if ((i!=0) && (i%eigenNum == 0))
		{
			eigenCol = 0;
			eigenRow++;
		}
		cvmSet(eigenVector, eigenRow, eigenCol, sampleEigenVector[i]);
		eigenCol++;
	}

	int resCoeffRow = 0;
	int resCoeffCol = 0;
	for (int i=0; i<sampleCount*eigenNum; i++)
	{
		if ((i!=0) && (i%eigenNum == 0))
		{
			resCoeffCol = 0;
			resCoeffRow++;
		}
		cvmSet(resCoeff, resCoeffRow, resCoeffCol, sampleCoeff[i]);
		resCoeffCol++;
	}

	char *name = new char[50];
	int index1 = 0;
	int index2 = 0;
	for (int i=0; i<50*sampleCount; i++)
	{
		if ((i!=0) && (i%50==0))
		{
			index1 = 0;
			for (int j=0; name[j-1]!='g'; j++)
			{
				similarity[index2].fileName[j] = name[j];
			} 
			index2++;
		}
		name[index1] = sampleFileName[i];
		index1++;
	}
	delete []name;

	CvMat *targetMat = cvCreateMat(imgLen, 1, CV_32FC1);
	CvMat *targetResult = cvCreateMat(1, eigenNum, CV_32FC1);

	for (int i=0; i<imgLen; i++)
	{
		cvmSet(targetMat, i, 0, currentFace[i]);
	}

	cvSub(targetMat, avgVector, targetMat, NULL);
	cvGEMM(targetMat, eigenVector, 1, NULL, 0, targetResult, CV_GEMM_A_T);
	//cvProjectPCA(targetMat, avgVector, eigenVector, targetResult);


	float diff = 0.0;
	float den = 0.0;
	float mol = 0.0;
	float *resCoeffData = resCoeff->data.fl;
	int resCols = resCoeff->cols;
	float *targetResultData = targetResult->data.fl;

	float coeff = 0.0;

	for (int i=0; i<sampleCount; i++)//����õ���ʶ��ͼƬ��ѵ������֮��Ĳ�
	{
		diff = 0.0;
		for (int j=0; j<eigenNum; j++)
		{
			diff += abs(resCoeffData[i*resCols+j] - targetResultData[j]); 
		}
		similarity[i].similarity = diff;
	}

	fstream txtFile;
	bool txtExist;

	txtExist = false;
	txtFile.open("C:\\faceRecognition\\data\\diff.txt");
	if (txtFile)
	{
		txtExist = true;
	}
	txtFile.close();

	if (txtExist)
	{
		remove("C:\\faceRecognition\\data\\diff.txt");//����ļ����ڣ���ɾ��
	}

	//ofstream out1("C:\\faceRecognition\\data\\diff.txt"); 
	float minNum = similarity[0].similarity;//������Сֵ
	float maxNum = similarity[0].similarity;//�������ֵ
	for (int i=1; i<sampleCount; i++)//����õ���ʶ��ͼƬ������ͼƬӳ��ϵ��֮���ֵ��������Сֵ
	{
		//out1<<similarity[i].similarity<<endl; 
		if (similarity[i].similarity > maxNum)
		{
			maxNum = similarity[i].similarity;
		}
		if (similarity[i].similarity < minNum)
		{
			minNum = similarity[i].similarity;
		}
	}
	maxNum = maxNum>minNum ? maxNum:(minNum+1); 
	//out1.close();

	/////////////////////////////////////��������ж�������ƶȱ�ʾ����////////////////////////////////////////////////
	if (minNum < 130000000)
	{
		for (int i=0; i<sampleCount; i++)
		{
			similarity[i].similarity = 1 - (similarity[i].similarity-minNum)/(maxNum-minNum);
		}
	}
	else
	{
		for (int i=0; i<sampleCount; i++)
		{
			similarity[i].similarity = 0.0;
		}
	}
	////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

	cvReleaseMat(&targetMat);
	cvReleaseMat(&targetResult);

	cvReleaseMat(&avgVector);
	cvReleaseMat(&eigenVector);
	cvReleaseMat(&resCoeff);
}


