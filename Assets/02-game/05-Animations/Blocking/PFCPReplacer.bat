@echo off
Setlocal enabledelayedexpansion

Set "Pattern=Facing"
Set "Replace=Blocking"

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