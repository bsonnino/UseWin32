using System;
using System.Linq;
using Microsoft.Windows.Sdk;

using var handle = PInvoke.FindFirstFile("*.*", out var findData);
if (handle.IsInvalid)
    return;
bool result;
do
{
    Console.WriteLine(ConvertFileNameToString(findData.cFileName.AsSpan()));
    result = PInvoke.FindNextFile(handle, out findData);
} while (result);

string ConvertFileNameToString(Span<ushort> span)
{
    return string.Join("", span.ToArray().TakeWhile(i => i != 0).Select(i => (char)i));
}