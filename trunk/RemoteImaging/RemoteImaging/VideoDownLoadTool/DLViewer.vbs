Dim objFol
Dim strFuncName
Dim strParmName
Dim strArg
Set objFol = CreateObject("Scripting.FileSystemObject").GetFolder(".")
IF ( Right (objFol.Path, 1) = "\" ) Then
strFuncName = objFol.Path & "bin\dlviewer.exe" 
Else
strFuncName = objFol.Path & "\bin\dlviewer.exe" 
End IF
For Each strArg In WScript.Arguments
strParmName = strParmName & " " & strArg
Next
CreateObject("WScript.Shell").Run """" & strFuncName & """" & strParmName, 0
Set objFol = Nothing
