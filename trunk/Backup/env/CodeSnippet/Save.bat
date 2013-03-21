@echo off
set project=jelly-dotnet-framework
set from="C:\Documents and Settings\Administrator\Application Data\VisualAssist\Autotext"
set to="Autotext\"

echo ----------------------------------------
echo Save Code Snippet To %project% Project's Code Snippet
echo ----------------------------------------
echo xcopy %from% %to%
xcopy /W /E /H /Y %from% %to%