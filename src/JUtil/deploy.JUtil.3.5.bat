@echo off

rem
rem VS2008 Setup �L�k�B�z�̪ۨ�Project�b�P�@��Setup Project
rem ��Deploy�A�]���N�̩��h��Project��X��S�w�ؿ�
rem ��LProject�ѷӦ��ؿ�����assembly�A�Ӥ��ѷө��hProject
rem 
rem �o�Ӱ��D�bVS2010�w�g�ѨM�A���ݭn�o�˳B�z�C 
rem 
set from="%1"
set to="C:\Program Files\JFramework\.NET Framework 3.5\JUtil.dll"
set toDir="C:\Program Files\JFramework\.NET Framework 3.5"

rem JELLY
rem �S�����n����H�U�ʧ@�F
mkdir %toDir% 2>NUL
copy /Y %from% %to%