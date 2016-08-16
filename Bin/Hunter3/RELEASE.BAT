rd RELEASE /s /q
md RELEASE

copy /y hunter3.exe RELEASE\hunter3.exe
copy /y MySql.Data.dll RELEASE\MySql.Data.dll
copy /y proxy.hip RELEASE\proxy.hip

rd RELEASE\strategy /s /q
md RELEASE\strategy
copy /y strategy\*.* RELEASE\strategy

rd RELEASE\config /s /q
md RELEASE\config
copy /y config\*.* RELEASE\config

rd RELEASE\plugins /s /q
md RELEASE\plugins
REM copy neccessary plugins for hunter
copy plugins\Hunter3Plugin.Update.dll RELEASE\plugins\Hunter3Plugin.Update.dll
copy plugins\Hunter3Plugin.Homepage.dll RELEASE\plugins\Hunter3Plugin.Homepage.dll