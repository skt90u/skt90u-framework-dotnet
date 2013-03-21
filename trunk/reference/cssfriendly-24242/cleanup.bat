@echo off

setlocal

set tempfile=CSSFriendly_tempfile
set sln=CSSFriendly
dir /ad /b > %tempfile%

FOR /F %%i IN (%tempfile%) DO (
	cd %%i
	call cleanup.bat
	cd ..
)

del /q %tempfile%

attrib -H %sln%.suo
del /q  %sln%.suo

endlocal