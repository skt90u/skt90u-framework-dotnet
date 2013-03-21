@echo off

set prj=demoUploadify
rd /s /q bin
rd /s /q obj
rd /s /q uploads
del /q %prj%.user

attrib -H %prj%.suo
del /s /q %prj%.suo