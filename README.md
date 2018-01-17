# Compute.NET: .NET bindings for native numerical computing

Compute.NET provides auto-generated bindings for native numerical computing libraries like [Intel Math Kernel Library](https://software.intel.com/en-us/mkl), [AMD Core Math Library](https://developer.amd.com/tools-and-sdks/archive/acml-downloads-resources/) (and its successors), AMD [clBLAS](https://gpuopen.com/compute-product/clblas/), cl* and others. The bindings are auto-generated from the library's C headers using the excellent [CppSharp](https:/github.com/Mono/CppSharp) library. The generator is a CLI program that be can used to generate individual modules of each library as well as customize key aspects of the generated code, such as the use of .NET structs instead of classes for complex data types, and marshalling array parameters in native code functions (either as managed arrays or pointers.) 

## Status

* CLI Bindings Generator: Works on Windows for Intel Math Kernel Library. Blas and Vml modules can be bound.

* Bindings: 
	* Compute.Bindings.IntelMKL [package](https://www.myget.org/feed/computedotnet/package/nuget/Compute.Bindings.IntelMKL) available on Myget [feed](https://www.myget.org/F/computedotnet/api/v2) with MKL BLAS and vector math functions. This library is not Windows-specific but I haven't tested it on Linux or other platforms yet.

* Native Library Packages: 
	* Compute.Winx64.IntelMKL packahe available on MyGet [feed](https://www.myget.org/F/computedotnet/api/v2).
 
## Usage

### Intel MKL Bindings
0. Add the [Compute.NET](https://www.myget.org/feed/Packages/computedotnet) package feed to your NuGet package sources: https://www.myget.org/F/computedotnet/api/v2
1. Install the bindings [package](https://www.myget.org/feed/computedotnet/package/nuget/Compute.Bindings.IntelMKL) into your project: `Install-Package Compute.Bindings.IntelMKL`.
2. (Optional) Install the native library [package](https://www.myget.org/feed/computedotnet/package/nuget/Compute.Winx64.IntelMKL) into your project: `Install-Package Compute.Winx64.IntelMKL`.

Without step 2 you will need to make sure the .NET runtime can locate the native MKL library DLLs or shared library files. You can set your path to include the directory where the library files are located (typically %MKLROOT%\redist). Or you can copy the needed files into your project output directory with a build task.

### Bindings Generator
The basic syntax is `bind LIBRARY MODULE [OPTIONS]` e.g ` bind mkl --vml --without-common -n IntelMKL -o .\Compute.Bindings.IntelMKL` will create bindings for the Intel MKL VML module, without including common types and functions, in the .NET namespace `IntelMKL` and the `.\Compute.Bindings.IntelMKL` output directory.   
