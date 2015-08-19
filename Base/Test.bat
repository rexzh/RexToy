@echo ****************************************
@echo Base
@echo ****************************************

echo off
::del .\unitTest\bin\Debug\temp\*.* /s /q
nunit-console.exe .\unittest\bin\debug\unittest.dll

if errorlevel 1 goto :ERROR

..\Tools\setcolor green
echo Test success.
GOTO :END

:ERROR
..\Tools\setcolor red
echo Test case fail. Find detail information in test log.

:END
..\Tools\setcolor
