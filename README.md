
# CSharp Function Dumper [![MIT license](https://img.shields.io/badge/License-MIT-blue.svg)](https://lbesson.mit-license.org/)

An application to dump .net PE files, into a nicely formatted output file.

## Why?
So you can easily add documentation, or interface with other application without
access to source files. It also doesn't require Reflection to handle the PE files,
and the parser is implemented in it's entirety is implemented here. You can also 
use this application programatically instead of having to rely on a heavy
decompiler.

## How?
The commands:
```
CsharpFunctionDumper.exe --in myAssemblyPath.dll --out DumpedFunctions.txt
```

## Issue?
Check if any issues have existed before for it, if not, then create an issue. There's not template for it just make sure it contains your environment you're running this on.
