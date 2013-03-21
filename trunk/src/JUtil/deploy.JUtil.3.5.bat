@echo off

rem
rem VS2008 Setup 無法處理相依的Project在同一個Setup Project
rem 做Deploy，因此將最底層的Project輸出到特定目錄
rem 其他Project參照此目錄中的assembly，而不參照底層Project
rem 
rem 這個問題在VS2010已經解決，不需要這樣處理。 
rem 
set from="%1"
set to="C:\Program Files\JFramework\.NET Framework 3.5\JUtil.dll"
set toDir="C:\Program Files\JFramework\.NET Framework 3.5"

rem JELLY
rem 沒有必要執行以下動作了
mkdir %toDir% 2>NUL
copy /Y %from% %to%