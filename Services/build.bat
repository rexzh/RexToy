@echo off
if {%1}=={debug} SET bld=debug
if {%1}=={} SET bld=debug
if {%1}=={release} set bld=release

devenv Services.sln /clean
devenv Services.sln /rebuild %bld% /out buildlog.txt

if errorlevel 1 goto :ERROR

type buildlog.txt>>..\buildlog.txt
del buildlog.txt

..\Tools\setcolor green
echo Build success.

copy UnitTest.Extension\bin\%bld%\rextoy.*.dll .\..\buildtarget\
copy UnitTest.Extension\bin\%bld%\rextoy.*.pdb .\..\buildtarget\
copy UnitTest.ORM\bin\%bld%\rextoy.orm.dll .\..\buildtarget\
copy UnitTest.ORM\bin\%bld%\rextoy.orm.pdb .\..\buildtarget\
copy UnitTest.ORM\bin\%bld%\rextoy.orm.*.dll .\..\buildtarget\
copy UnitTest.ORM\bin\%bld%\rextoy.orm.*.pdb .\..\buildtarget\
copy UnitTest.Template\bin\%bld%\rextoy.template.dll .\..\buildtarget\
copy UnitTest.Template\bin\%bld%\rextoy.template.pdb .\..\buildtarget\
copy RexToy.WebService\bin\%bld%\rextoy.webservice.dll .\..\buildtarget\
copy RexToy.WebService\bin\%bld%\rextoy.webservice.pdb .\..\buildtarget\

..\Tools\setcolor
goto :END

:ERROR
..\Tools\setcolor red
echo Build fail. Find detail information in build log.
..\Tools\setcolor
type buildlog.txt>>..\buildlog.txt
del buildlog.txt

:END