
cd %~dp0

rmdir /S /Q dist

mkdir dist

mkdir dist\nfaw-io
xcopy /s  nfaw-io dist\nfaw-io\

mkdir dist\ff_win32
xcopy /s  ff_win32 dist\ff_win32\

mkdir dist\ff_profile
copy ff_profile\cert8.db dist\ff_profile\
copy ff_profile\prefs.js dist\ff_profile\

copy wTryNetFree\wTryNetFree\bin\Debug\wTryNetFree.exe dist\

rem del dist.7z
rem 7-Zip\7z.exe a -t7z dist.7z .\dist\*
rem copy /b 7zS.sfx + 7z_sfx.conf + dist.7z trynf.exe


rem WinRAR\rar.exe a -r -sfx -z"rar_sfx.conf" dist trynf.exe

del trynf.exe
makesfx.exe  dist trynf.exe  -mf -ft -we -di -un -op dist\wTryNetFree.exe