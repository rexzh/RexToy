@echo off
if {%1}=={debug} SET bld=debug
if {%1}=={} SET bld=debug
if {%1}=={release} set bld=release

devenv Base.sln /clean
devenv Base.sln /rebuild %bld% /out buildlog.txt

if errorlevel 1 goto :ERROR

type buildlog.txt>>..\buildlog.txt
del buildlog.txt

..\Tools\setcolor green
echo Build success.

copy UnitTest\bin\%bld%\rextoy.*.dll .\..\buildtarget\
copy UnitTest\bin\%bld%\rextoy.*.pdb .\..\buildtarget\

..\Tools\setcolor
goto :END

:ERROR
..\Tools\setcolor red
echo Build fail. Find detail information in build log.
..\Tools\setcolor
type buildlog.txt>>..\buildlog.txt
del buildlog.txt

:END