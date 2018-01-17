# Compute.NET: .NET bindings for native numerical computing libraries

Compute.NET provides auto-generated bindings for native numerical computing libraries like Intel MKL, AMD ACML, clFFT, cl* and others. The bindings are auto-generated from the 
library's C headers using the excellent (CppSharp)[https:/github.com/Mono/CppSharp] library. The generator is a CLI program that be can used to generate individual modules of each library as well as customize key 
aspects of the generated code, such as the use of .NET structs instead of classes for complex data types, and how array parameters are marshalled from managed to native code functions (either as managed arrays or pointers.) 


## Status

* CLI Bindings Generator: Works on Windows for Intel MKL. Blas and VML modules can be bound.

* Bindings: 
	Compute.Bindings.IntelMKL available on Myget [feed](https://www.myget.org/F/computedotnet/api/v2) with MKL BLAS and Vector Math functions.

Native Library Packages: 
	Compute.Winx64.IntelMKL available on MyGet [feed](https://www.myget.org/F/computedotnet/api/v2).
 

## Usage

### Bindings
0. Add the Compute.NET package feed to your NuGet package sources: https://www.myget.org/F/computedotnet/api/v2
1. Install the bindings package into your project: `Install-Package Compute.Bindings.IntelMKL`.
2. (Optional) Install the native library package into your project: `Install-Package Compute.Winx64.IntelMKL`.

Without step 2 you will need to make sure the .NET runtime can locate the native MKL library DLLs or shared library files. You can set your path to include the directory
where the library files are located (typically %MKLROOT%\redist). Or you can copy the needed files into 
The basic syntax is `bind LIBRARY MODULE [OPTIONS]`

You will need the runtime li


