@echo off
set project=jelly-dotnet-framework
set from="Autotext"
set to="C:\Documents and Settings\Administrator\Application Data\VisualAssist\Autotext\"

echo ----------------------------------------
echo Load %project% Project's Code Snippet
echo ----------------------------------------
echo xcopy %from% %to%
xcopy /W /E /H /Y %from% %to%