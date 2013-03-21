@echo off

setlocal

set tempfile=tempfile
set sln=jelly-dotnet-framework
dir /ad /b > %tempfile%

FOR /F %%i IN (%tempfile%) DO (
	cd %%i
	call cleanup.bat
	cd ..
)

del /q %tempfile%

rem attrib -H %sln%.suo
rem del /q  %sln%.suo

endlocal