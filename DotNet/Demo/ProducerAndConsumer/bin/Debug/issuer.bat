echo off

ProducerAndConsumer.exe %1

If errorlevel 1639 goto NoArg 
if errorlevel 534 goto Overflow
if errorlevel 160 goto BadArg
if errorlevel 1 goto BadException

echo %errorlevel%
pause

if errorlevel 0 echo Completed Successfully with para %1 
pause
goto :EOF

:NoArg
echo Missing argument
goto :EOF

:Overflow
echo Arithmetic overflow
goto :EOF

:BadArg
echo Invalid argument
goto :EOF

:BadException
echo Bad exception
goto :EOF