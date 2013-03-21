@echo off

setlocal

set tempfile=tempfile
dir /ad /b > %tempfile%

FOR /F %%i IN (%tempfile%) DO (
	cd %%i
	call cleanup.bat
	cd ..
)

del /q %tempfile%

attrib -H *.suo
del /q  *.suo

del /s /q *.ReSharper.user
rd /s /q _ReSharper.Deploy.JFramework2008
rd /s /q _ReSharper.JFramework2008
 
endlocal