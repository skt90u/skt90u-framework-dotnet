@echo off

setlocal

set curDIR=E:\GoogleProjectHosting\jelly-dotnet-framework\src\JUtil.Web\JavaScript
rem set prjDIR=%1
rem set curDIR=%prjDIR%JavaScript
set dependences= Framework, Log, ControlAccessor, MessageBox, AjaxControlToolKitPlugin, WebService


set yuicompressor=%curDIR%\yuicompressor-2.4.6.jar
set debugFile=%curDIR%\jutil-all-debug.js
set releaseFile=%curDIR%\jutil-all.js

del /s /q %debugFile%     2>nul
del /s /q %releaseFile%   2>nul

for %%A in (%dependences%) do (
	if exist %%A (
		cd %%A
		call pack.bat %debugFile% 2>nul
		cd ..	
	)
)

java -jar %yuicompressor% --type js --charset utf-8 -o %releaseFile% %debugFile%
rem copy %debugFile% %releaseFile% 

endlocal