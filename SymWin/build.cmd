@echo Off
set config=%1
if "%config%" == "" (
   set config=debug
)

.nuget\NuGet.exe restore SymWin.sln -nocache
%windir%\Microsoft.NET\Framework64\v4.0.30319\msbuild.exe %~dp0\SymWin.csproj /p:Configuration="%config%" /v:M /fl /flp:LogFile=msbuild.log;Verbosity=Normal /nr:false