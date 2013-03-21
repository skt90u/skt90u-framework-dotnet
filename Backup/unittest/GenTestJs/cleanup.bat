@echo off
rd /s /q Debug

del /s /q GenBigJs.ncb
del /s /q GenBigJs.sln
del /s /q GenBigJs.vcproj.JELLY-DESKTOP.Administrator.user  

attrib -H GenBigJs.suo
del /s /q GenBigJs.suo
