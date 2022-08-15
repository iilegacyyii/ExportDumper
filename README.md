# ExportDumper
A small tool I made to dump the export table of PE files. The primary use case was intended for use within DLL proxying.

# Usage
```powershell
.\ExportDump dllpath
```
e.g. 
```powershell
.\ExportDump C:\Windows\System32\version.dll
[*] Dumping all 17 exported functions from C:\windows\system32\version.dll.
[+] Dumping done. Check pragma.h
```

Copy paste the contents of pragma.h into your project, or just include it, and all exported functions should be linked from the copy you have on disk.
