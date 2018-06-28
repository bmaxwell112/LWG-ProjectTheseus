@echo off
Setlocal enabledelayedexpansion

Set "Pattern=left"
Set "Replace=Left"

For %%a in (*) Do (
    Set "File=%%~a"
    Ren "%%a" "!File:%Pattern%=%Replace%!"
)

Set "Pattern=right"
Set "Replace=Right"

For %%a in (*) Do (
    Set "File=%%~a"
    Ren "%%a" "!File:%Pattern%=%Replace%!"
)

Exit