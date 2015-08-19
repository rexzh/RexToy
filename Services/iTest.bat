@echo ****************************************
@echo Services
@echo ****************************************

echo off
nunit-console.exe .\UnitTest.ORM\bin\Debug\UnitTest.ORM.dll

if errorlevel 1 goto :ERROR

..\Tools\setcolor green
echo Test success.
GOTO :END

:ERROR
..\Tools\setcolor red
echo Test case fail. Find detail information in test log.

:END
..\Tools\setcolor
