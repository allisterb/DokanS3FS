@echo off
pushd
@setlocal
src\DokanS3FS.Control\bin\Debug\net6.0-windows\DokanS3FS.Control.exe %*

:end
@endlocal
popd
exit /B %ERROR_CODE%