@echo ****************************************
@echo Services
@echo ****************************************

echo off
::del .\unitTest\bin\Debug\temp\*.* /s /q
nunit-console.exe .\unittest.extension\bin\debug\unittest.extension.dll .\unittest.template\bin\debug\unittest.template.dll .\UnitTest.ORM\bin\Debug\UnitTest.ORM.dll /exclude=IntegrateDB

if errorlevel 1 goto :ERROR

..\Tools\setcolor green
echo Test success.
GOTO :END

:ERROR
..\Tools\setcolor red
echo Test case fail. Find detail information in test log.

:END
..\Tools\setcolor
