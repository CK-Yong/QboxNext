# QboxNext

## Introduction

This repository is a modified clone of Qplatform.
The modifications are:
- only the code needed for Qserver and the Qbox simulator was kept, the rest was removed,
- a stand-alone version DumpQBX was added,
- all occurrences of 'Qurrent' were replaces with 'QboxNext',
- all sensitive information like connectionstrings and signing keys was removed,
- the actual encryption key to encrypt and decrypt Qbox messages was replaced by an empty key.
- all csprojs have been rebuilt as .NET Core projects.

## Notes

- When starting Qserver from Visual Studio, the log files end up in QboxNext.Qserver\bin\Debug\netcoreapp2.1\logs
- When you change nlog.config of a project, you have to do a rebuild of the project, otherwise the modified file will not be copied to the output directory.
