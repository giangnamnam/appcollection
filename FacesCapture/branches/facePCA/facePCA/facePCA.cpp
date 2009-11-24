// facePCA.cpp : Defines the exported functions for the DLL application.
/*******************************************************************************************************
Copyright (c) 成都丹玛尼科技有限公司
All right reserved!

文件名：facePCA.cpp
摘  要：利用PCA进行人脸样本库的训练学习，保存相应的数据为txt文件。进行内存申请，释放内存。进行人脸识别
        比对，并返回相似度及相应的文件名。

作    者：薛晓利
完成日期：2009年11月19日 
当前版本：1.1
*******************************************************************************************************/
#include "stdafx.h"
#include "facePCA.h" 

/***************************************************************************************
FaceTraining(int imgWidth, int imgHeight, int eigenNum)
说明：
imgWidth:用于识别的缩放后的图片的宽度，该参数为本函数内部使用
imgHeight:用于识别的缩放后的图片的高度,该参数为本函数内部使用
eigenNum:投影空间的维数
***************************************************************************************/
FACEPCA_API int FaceTraining(int imgWidth, int imgHeight, int eigenNum)
{
	CFileFind imageFile;
	CString fileName;
	CString imgFileAdd = "C:\\faceRecognition\\faceSample\\091028212230000_0000.jpg";  

	int sampleCount = 0;//训练样本的图片个数

	bool FileExist = imageFile.FindFile(_T("C:\\faceRecognition\\faceSample\\*.jpg"),0); 
	while (FileExist)
	{
		FileExist = imageFile.FindNextFileW(); 
		if (!imageFile.IsDots()) 
		{
			sampleCount++;//遍历得到训练样本图片的总数
		}
	}

	if (eigenNum > sampleCount)
	{
		cvSetErrMode(CV_ErrModeParent);
		cvGuiBoxReport(CV_StsBadArg, "FaceTraining", "sampleCount should greater than(or equal to) eigenNum", __FILE__, __LINE__, NULL);
	}

	char *imgFileName[50]; //定义指针数组用来存储样本文件名 
	for (int i=0; i<50; i++)
	{
		imgFileName[i] = new char[sampleCount];
	}

	CvMat *faceDB = cvCreateMat(imgWidth*imgHeight, sampleCount, CV_32FC1);//训练样本数据组成的矩阵，每个样本为一列
	CvMat *selEigenVector = cvCreateMat(eigenNum, sampleCount, CV_32FC1);
	CvMat *EigenVectorFinal = cvCreateMat(imgWidth*imgHeight, eigenNum, CV_32FC1);
	CvMat *reltiveMat = cvCreateMat(sampleCount, sampleCount, CV_32FC1);
	CvMat *AvgVector = cvCreateMat(imgWidth*imgHeight, 1, CV_32FC1);//平均值向量
	CvMat *EigenValue = cvCreateMat(1, sampleCount, CV_32FC1);//协方差矩阵的特征值
	CvMat *EigenVector = cvCreateMat(sampleCount, sampleCount, CV_32FC1);//协方差矩阵的特征向量  
	CvMat *resCoeff = cvCreateMat(sampleCount, eigenNum, CV_32FC1);//取前eigenNum个最大特征值

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

				//平均值和特征值，特征向量
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
					remove("C:\\faceRecognition\\data\\AverageValue.txt");//如果文件存在，则删除
				}

				ofstream out1("C:\\faceRecognition\\data\\AverageValue.txt");//将平均值保存在文件中
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
					remove("C:\\faceRecognition\\data\\EigenVector.txt");//如果文件存在，则删除
				}

				ofstream out2("C:\\faceRecognition\\data\\EigenVector.txt");//将特征向量保存在文件中
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
					remove("C:\\faceRecognition\\data\\SampleCoefficient.txt");//如果文件存在，则删除
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
					remove("C:\\faceRecognition\\data\\FileName.txt");//如果文件存在，则删除
				}

				ofstream out3("C:\\faceRecognition\\data\\SampleCoefficient.txt");//将样本图片的投影系数保存在文件中
				ofstream out4("C:\\faceRecognition\\data\\FileName.txt");//将样本图片的文件名保存在文件中
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
说明：
sampleCount:为C:\faceRecognition\faceSample\路径下图片的个数
imgLen:该值等于FaceTraining调用时的imgWidth*imgHeight
eigenNum:投影空间的维数,也就是特征值的个数
**********************************************************************************/
FACEPCA_API void InitData(int sampleCount, int imgLen, int eigenNum)
{
	sampleAvgVal = new float[imgLen];

	ifstream fileIn1("C:\\faceRecognition\\data\\AverageValue.txt");
	if (fileIn1.fail())//如果文件不存在
	{
		cvSetErrMode(CV_ErrModeParent);
		cvGuiBoxReport(CV_StsBadArg, "FaceRecognition", "AverageValue.txt was not found!!!", __FILE__, __LINE__, NULL);
	}

	float avgData;
	for (int i=0; i<imgLen; i++)
	{
		fileIn1>>avgData;
		sampleAvgVal[i] = avgData;//读取训练样本的平均值数据至AvgVector
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
说明：
该函数主要用于释放InitData()申请的内存。
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
说明：
currentFace:待识别的数据，为一个imgLen行的列向量
sampleCount：为C:\faceRecognition\faceSample\路径下图片的个数
similarity:待识别图片样本图片之间的相似度，及其所对应的样本图片文件名
eigenNum:特征值的个数，也就是投影空间的维数 
*********************************************************************************/
FACEPCA_API void FaceRecognition(float *currentFace, int sampleCount, similarityMat *similarity, int imgLen, int eigenNum)
{
	CvMat *avgVector = cvCreateMat(imgLen, 1, CV_32FC1);//平均值向量
	CvMat *eigenVector = cvCreateMat(imgLen, eigenNum, CV_32FC1);//协方差矩阵的特征向量  
	CvMat *resCoeff = cvCreateMat(sampleCount, eigenNum, CV_32FC1);//取前eigenNum个最大特征值

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

	for (int i=0; i<sampleCount; i++)//计算得到待识别图片与训练样本之间的差
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
		remove("C:\\faceRecognition\\data\\diff.txt");//如果文件存在，则删除
	}

	//ofstream out1("C:\\faceRecognition\\data\\diff.txt"); 
	float minNum = similarity[0].similarity;//定义最小值
	float maxNum = similarity[0].similarity;//定义最大值
	for (int i=1; i<sampleCount; i++)//计算得到待识别图片与样本图片映射系数之间差值的最大和最小值
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

	/////////////////////////////////////加入距离判定后的相似度表示方法////////////////////////////////////////////////
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


