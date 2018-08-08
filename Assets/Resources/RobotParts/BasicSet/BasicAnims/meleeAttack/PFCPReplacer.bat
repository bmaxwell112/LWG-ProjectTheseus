@echo off
Setlocal enabledelayedexpansion

Set "Pattern=MeleeLeft"
Set "Replace=BML"

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