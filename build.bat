@echo off

del .\buildtarget\*.* /q

%windir%\Microsoft.NET\Framework\v4.0.30319\csc /out:.\tools\setcolor.exe .\tools\setcolor.cs
%windir%\Microsoft.NET\Framework\v4.0.30319\csc /out:.\tools\versioncontrol.exe .\tools\versioncontrol.cs

.\tools\versioncontrol . 5.0

echo build start>buildlog.txt

cd Base
call build %1
cd ..

cd Services
call build %1
cd ..



echo build finish>>buildlog.txt

%windir%\Microsoft.NET\Framework\v4.0.30319\csc /out:.\tools\codestat.exe .\tools\codestat.cs 
Tools\CodeStat . cs,js > lines.txt