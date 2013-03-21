@echo off

setlocal

set tempfile=unittest
dir /ad /b > %tempfile%

FOR /F %%i IN (%tempfile%) DO (
	cd %%i
	call cleanup.bat
	cd ..
)

del /q %tempfile%

endlocal