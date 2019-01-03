# QboxNext

## Introduction

This repository is a modified clone of Qplatform.
The modifications are:

- only the code needed for Qserver and the Qbox simulator was kept, the rest was removed,
- a stand-alone version DumpQBX was added,
- all occurrences of 'Qurrent' were replaces with 'QboxNext',
- all sensitive information like connectionstrings and signing keys was removed,
- the actual encryption key to encrypt and decrypt Qbox messages was replaced by an empty key,
- all csprojs have been rebuilt as .NET Core projects,
- only the latest firmware will be returned by the firmware controller,
- all databases have been removed: metadata SQL database, metadata cache Redis database and status Mongo database),
- no Qbox metadata is being retrieved from a database, for now only a smartmeter with S0 (Eltako) is supported.
- all Qbox data is written to d:\QboxNextData.

## Qserver

An ASP.NET application that receives and processes messages from Qboxes. When run it uses the built-in [Kestrel](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/servers/kestrel?view=aspnetcore-2.2) web server. 
It will run on port 5000, the default Kestrel port.

When Qserver is running it can be tested using Powershell. Open Powershell and run this snippet:

```powershell
$body = @"
FAFB070DABB7440780/KFM5KAIFA-METER 1-3:0.2.8(40) 0-0:1.0.0(000102045905W) 
0-0:96.1.1(4530303033303030303030303032343133) 1-0:1.8.1(000001.011*kWh) 
1-0:1.8.2(000000.000*kWh) 1-0:2.8.1(000000.000*kWh) 
1-0:2.8.2(000000.000*kWh) 0-0:96.14.0(0001) 1-0:1.7.0(00.034*kW) 
1-0:2.7.0(00.000*kW) 0-0:17.0.0(999.9*kW) 0-0:96.3.10(1) 0-0:96.7.21(00073) 
0-0:96.7.9(00020) 1-0:99.97.0(3)(0-0:96.7.19)(000124235657W)(0000003149*s)(000124225935W)(0000000289*s)(000101000001W)(2147483647*s) 
1-0:32.32.0(00005) 1-0:52.32.0(00006) 1-0:72.32.0(00001) 1-0:32.36.0(00000) 
1-0:52.36.0(00000) 1-0:72.36.0(00000) 0-0:96.13.1() 0-0:96.13.0() 1-0:31.7.0(000*A) 
1-0:51.7.0(000*A) 1-0:71.7.0(000*A) 1-0:21.7.0(00.034*kW) 1-0:22.7.0(00.000*kW) 1-0:41.7.0(00.000*kW) 
1-0:42.7.0(00.000*kW) 1-0:61.7.0(00.000*kW) 1-0:62.7.0(00.000*kW) 0-1:24.1.0(003) 
0-1:96.1.0(4730303131303033303832373133363133) 0-1:24.2.1(000102043601W)(62869.839*m3) 0-1:24.4.0(1) !583C
"@
invoke-webrequest -method POST http://localhost:5000/device/qbox/6618-1400-0200/15-46-002-442 -body $body -ContentType text/html
```

It should show something like this:

```
StatusCode        : 200
StatusDescription : OK
Content           : FB1695698300
RawContent        : HTTP/1.1 200 OK
                    Content-Length: 14
                    Content-Type: text/plain; charset=utf-8
                    Date: Thu, 03 Jan 2019 06:23:45 GMT
                    Server: Kestrel

                    FB1695698300
Forms             : {}
Headers           : {[Content-Length, 14], [Content-Type, text/plain; charset=utf-8], [Date, Thu, 03 Jan 2019 06:23:45
                    GMT], [Server, Kestrel]}
Images            : {}
InputFields       : {}
Links             : {}
ParsedHtml        : mshtml.HTMLDocumentClass
RawContentLength  : 14
```

Another option is to run the Qbox simulator. See SimulateQbox.

### Notes

- When starting Qserver from Visual Studio, the log files end up in QboxNext.Qserver\bin\Debug\netcoreapp2.1\logs

## DumpQbx

DumpQbx is a tool to convert a QBX file into a readable text file. To use it open a command shell in the local git repo and enter:

```dos
cd QboxNext.DumpQbx\bin\Debug\netcoreapp2.1
dotnet QboxNext.DumpQbx.dll --qbx=d:\QboxNextData\Qbox_15-46-002-442\15-46-002-442_00000181.qbx --values > d:\QboxNextData\Qbox_15-46-002-442\15-46-002-442_00000181.qbx.txt
```

The generated text file will look like this:

```
StartOfFile: 2014-04-08 14:23:00
EndOfFile:   2014-04-15 14:23:00
ID:          d19a4946-229d-4525-b68a-3090bd3b3c0c
Timestamp NL     :        raw,        kWh,      money, quality (kWh can be kWh, Wh or mWh depending on Precision setting)
2014-04-08 14:23 :       1011,          0,          0,     0
2014-04-08 14:24 : empty slot
...
```

The 'raw' column contains the counter value as received by Qserver. In the current implementation the unit is Wh. 
The column 'kWh' is the raw value converted to kWh.
The 'money' column is obsolete and can be ignored.
The column 'quality' specifies if the value was a value received by Qserver or an interpolated value.

## SimulateQbox

SimulateQbox is a simulator that can simulate Qboxes attached to several types of meters and simulate several usage and generation patterns.
To use it make sure Qserver is running, open a command shell in the local git repo and enter:

```dos
cd QboxNext.SimulateQbox\bin\Debug\netcoreapp2.1
dotnet QboxNext.SimulateQbox.dll --qserver=http://localhost:5000 --qboxserial=00-00-000-000 --metertype=smart --pattern=181:flat(2);182:zero;281:zero;282:zero;2421:zero
```

To view an explanation of the different meter types and patterns, run SimulateQbox without parameters:

```dos
dotnet QboxNext.SimulateQbox.dll
```

## General notes

- When you change nlog.config of a project, you have to do a rebuild of the project, otherwise the modified file may not be copied to the output directory.
