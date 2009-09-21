/////////////////////////////////////////////////////////////////
//ac6651 include files
//wwlab (c)2004/11   by york wang


//for DIO
extern "C" long __declspec(dllimport) __stdcall AC6651_DI(HANDLE hHandle,long ionum);
extern "C" long __declspec(dllimport) __stdcall AC6651_DO(HANDLE hHandle,long ionum,long iodata);
extern "C" long __declspec(dllimport) __stdcall AC6651_SetIOMode(HANDLE hHandle,long iomode);
extern "C" long __declspec(dllimport) __stdcall AC6651_GetIOMode(HANDLE hHandle);

//for timer
extern "C" long __declspec(dllimport) __stdcall AC6651_SetTData(HANDLE hHandle,long tch,long tdata);
extern "C" long __declspec(dllimport) __stdcall AC6651_GetTData(HANDLE hHandle,long tch);
extern "C" long __declspec(dllimport) __stdcall AC6651_GetTST(HANDLE hHandle);
extern "C" long __declspec(dllimport) __stdcall AC6651_SetTMode(HANDLE hHandle,long tch,long tmode);

extern "C" HANDLE __declspec(dllimport) __stdcall AC6651_OpenDevice(long DeviceNum);
extern "C" long __declspec(dllimport) __stdcall AC6651_CloseDevice(HANDLE hHandle);

