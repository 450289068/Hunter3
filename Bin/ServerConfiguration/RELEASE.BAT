rd RELEASE /s /q
md RELEASE

copy /y Hunter3ServerConfiguration.exe RELEASE\Hunter3ServerConfiguration.exe
copy /y dictionary.txt RELEASE\dictionary.txt
copy /y MySql.Data.dll RELEASE\MySql.Data.dll
copy /y keywords.exe RELEASE\keywords.exe
copy /y SpecimenSortServer.exe RELEASE\SpecimenSortServer.exe
copy /y SpecimenDelete.exe RELEASE\SpecimenDelete.exe