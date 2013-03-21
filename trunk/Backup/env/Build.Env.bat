rem ----------------------------------------------------------------------
rem 開發階段建立測試環境
rem ----------------------------------------------------------------------

set src=%1
set dest=%2

copy /Y %src%Scripts.xml %dest%Scripts.xml

mkdir %dest%Layout   2>NUL
mkdir %dest%Argument 2>NUL

copy /Y %src%Script01.Layout.xml %dest%Layout\Script01.Layout.xml
copy /Y %src%Script02.Layout.xml %dest%Layout\Script02.Layout.xml

copy /Y %src%Argument.xml %dest%Argument\Script01.Block01.PlayContent01.xml
copy /Y %src%Argument.xml %dest%Argument\Script01.Block01.PlayContent02.xml
copy /Y %src%Argument.xml %dest%Argument\Script01.Block01.PlayContent03.xml
copy /Y %src%Argument.xml %dest%Argument\Script01.Block01.PlayContent04.xml
copy /Y %src%Argument.xml %dest%Argument\Script01.Block02.PlayContent01.xml
copy /Y %src%Argument.xml %dest%Argument\Script01.Block02.PlayContent02.xml
copy /Y %src%Argument.xml %dest%Argument\Script01.Block02.PlayContent03.xml
copy /Y %src%Argument.xml %dest%Argument\Script01.Block02.PlayContent04.xml
copy /Y %src%Argument.xml %dest%Argument\Script01.Block03.PlayContent01.xml
copy /Y %src%Argument.xml %dest%Argument\Script01.Block03.PlayContent02.xml
copy /Y %src%Argument.xml %dest%Argument\Script01.Block03.PlayContent03.xml
copy /Y %src%Argument.xml %dest%Argument\Script01.Block03.PlayContent04.xml
copy /Y %src%Argument.xml %dest%Argument\Script01.Block04.PlayContent01.xml
copy /Y %src%Argument.xml %dest%Argument\Script01.Block04.PlayContent02.xml
copy /Y %src%Argument.xml %dest%Argument\Script01.Block04.PlayContent03.xml
copy /Y %src%Argument.xml %dest%Argument\Script01.Block04.PlayContent04.xml

copy /Y %src%Argument.xml %dest%Argument\Script02.Block01.PlayContent01.xml
copy /Y %src%Argument.xml %dest%Argument\Script02.Block01.PlayContent02.xml
copy /Y %src%Argument.xml %dest%Argument\Script02.Block01.PlayContent03.xml
copy /Y %src%Argument.xml %dest%Argument\Script02.Block01.PlayContent04.xml
copy /Y %src%Argument.xml %dest%Argument\Script02.Block02.PlayContent01.xml
copy /Y %src%Argument.xml %dest%Argument\Script02.Block02.PlayContent02.xml
copy /Y %src%Argument.xml %dest%Argument\Script02.Block02.PlayContent03.xml
copy /Y %src%Argument.xml %dest%Argument\Script02.Block02.PlayContent04.xml
copy /Y %src%Argument.xml %dest%Argument\Script02.Block03.PlayContent01.xml
copy /Y %src%Argument.xml %dest%Argument\Script02.Block03.PlayContent02.xml
copy /Y %src%Argument.xml %dest%Argument\Script02.Block03.PlayContent03.xml
copy /Y %src%Argument.xml %dest%Argument\Script02.Block03.PlayContent04.xml
copy /Y %src%Argument.xml %dest%Argument\Script02.Block04.PlayContent01.xml
copy /Y %src%Argument.xml %dest%Argument\Script02.Block04.PlayContent02.xml
copy /Y %src%Argument.xml %dest%Argument\Script02.Block04.PlayContent03.xml
copy /Y %src%Argument.xml %dest%Argument\Script02.Block04.PlayContent04.xml