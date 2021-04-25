using System;
using Microsoft.Windows.Sdk;

var hInst = PInvoke.LoadLibraryEx(@"C:\Windows\Notepad.exe",
    null, LoadLibraryEx_dwFlags.LOAD_LIBRARY_AS_DATAFILE);
try
{
    PInvoke.EnumResourceTypes(hInst, EnumerateTypes, 0);
}
finally
{
    PInvoke.FreeLibrary(hInst);
}

BOOL EnumerateTypes(nint hModule, PWSTR lpType, nint lParam)
{
    Console.WriteLine(PwStrToString(lpType));
    PInvoke.EnumResourceNames(hModule, lpType, EnumNames, 0);
    return true;
}

string PwStrToString(PWSTR str)
{
    unsafe
    {
        return ((ulong)str.Value & 0xFFFF0000) == 0 ?
            ((ulong)str.Value).ToString() :
            str.AsSpan().ToString();
    }
}

BOOL EnumNames(nint hModule, PCWSTR lpType, PWSTR lpName, nint lParam)
{
    Console.WriteLine("  " + PwStrToString(lpName));
    return true;
}
