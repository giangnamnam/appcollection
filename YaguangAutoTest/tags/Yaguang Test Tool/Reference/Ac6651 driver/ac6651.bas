Attribute VB_Name = "Module1"

' 设备函数
'------------------------------------------------------------
' Creat Device
'Devicenum: Device number
'hd1 handle of this device
Declare Function AC6651_OpenDevice Lib "AC6651.dll " (ByVal DeviceNum As Long) As Long

'----------------------------------------------------------------
'closed the device
Declare Function AC6651_CloseDevice Lib "AC6651.dll " (ByVal HANDLE As Long) As Long




'------------------------------------------------------------------------------------------
'开关量控制函数


'write data to DIO
'ionum=0-3
Declare Function AC6651_DO Lib "AC6651.dll" (ByVal hd As Long, ByVal ionum As Long, ByVal iodata As Long) As Long


'read data from DIO
'ionum=0-3
' data return by func
Declare Function AC6651_DI Lib "AC6651.dll" (ByVal hd As Long, ByVal ionum As Long) As Long


'set DIO Mode
'bit0-bit3 to: Port0-Port3's mode
'bit=0: DI / bit=1:DO
Declare Function AC6651_SetIOMode Lib "AC6651.dll" (ByVal hd As Long, ByVal iomode As Long) As Long


'------------------------------------------------------------------------------------------
'8254控制函数

'set 8254mode
'tch:8254 channel, 0-2
'tmode:0-5, set 8254's mode0-mode5
Declare Function AC6651_SetTMode Lib "AC6651.dll" (ByVal hd As Long, ByVal tch As Long, ByVal tmode As Long) As Long

'set 8254 data
'tdata=1-65535
'tch:0-2 , 8254's channel
Declare Function AC6651_SetTData Lib "AC6651.dll" (ByVal hd As Long, ByVal tch As Long, ByVal tdata As Long) As Long

'read 8254 data
'tch:0-2 , 8254's channel
'data return by function
Declare Function AC6651_GetTData Lib "AC6651.dll" (ByVal hd As Long, ByVal tch As Long) As Long


'get 8254 out state
'return state, bit0-bit2:
'bit0:out0 state/bit1:out1 state/bit2:out2 state
Declare Function AC6651_GetTST Lib "AC6651.dll" (ByVal hd As Long) As Long



'********************************************************************************
'定义的全局变量
Public hd6651 As Long  '句柄


