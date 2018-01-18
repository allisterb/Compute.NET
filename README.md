# Compute.NET: .NET bindings for native numerical computing
![bind program screenshot](https://lh3.googleusercontent.com/2RbLRXz9dAq5_sFpDNa6zp81bUFRzBmaAr0EVUOZrNYgs89Ni_RYpDLQ69Vai8L1m0-zGwbputYat3EmmQAMuZpRlWehfnW3sPUm8irAnEodaVGxWfiAGL5msiGUTmVGyzEpoVekJUVRyUlSFl5HrYD0MW1T-yzHc6VtxJICWW2nUIneSxZiiewLqKfXaw7VEWLr_zBctJ9buuTib2LNZEHKMg2xuBEc6ld5FbC86Gqj-PS1Mh-j15ekrboaVpQmUCr7sokiY1xPYgcE9bAYH8iLmCacX-Ndoqg_eL6KVf7WJnHb4XZYEcRtsspV8E1j_l6Yyf5HM-ini2RSJZjpgj8l8TgBiTkFZn3GU1gZWf9iHyXkNZVjmA4ldluhIgaCyhD_68Vg6HEtVoTblRrMoT7DaRGGVd6QHd7DdjxlJJoxDACwx7HlFX_FzN6TsDJglkSaxalYfQ7xozNiYvbto7bhvnsbNRisDqSuso3E3JYVqnBool64S3CJmfx2bflIKnJ8ZmDe3zvu2jWwJMqzKLR0HUp_I3UYiiOYTj4Hzl8D5zS7jLifrpWHfVpu2X-QrZQcUJNGWKsPRrjdT3H6gAEPBNnwaVxprctX9w4=w845-h394-no)
Get the latest release from the Compute.NET [package feed](https://www.myget.org/feed/Packages/computedotnet).

## About
Compute.NET provides auto-generated bindings for native numerical computing libraries like [Intel Math Kernel Library](https://software.intel.com/en-us/mkl), [AMD Core Math Library](https://developer.amd.com/tools-and-sdks/archive/acml-downloads-resources/) (and its successors), AMD [clBLAS](https://gpuopen.com/compute-product/clblas/), cl* and others. The bindings are auto-generated from the library's C headers using the excellent [CppSharp](https:/github.com/Mono/CppSharp) library. The generator is a CLI program that be can used to generate individual modules of each library as well as customize key aspects of the generated code, such as the use of .NET structs instead of classes for complex data types, and marshalling array parameters in native code functions (either as managed arrays or pointers.) 

## Status
* CLI Bindings Generator: Works on Windows for Intel Math Kernel Library. Blas and Vml modules can be bound.

* Bindings: 
	* Compute.Bindings.IntelMKL [package](https://www.myget.org/feed/computedotnet/package/nuget/Compute.Bindings.IntelMKL) available on Myget [feed](https://www.myget.org/F/computedotnet/api/v2) with BLAS and vector math functions. This library is not Windows-specific but I haven't tested it on Linux or other platforms yet.

* Native Library Packages: 
	* Compute.Winx64.IntelMKL [package](https://www.myget.org/feed/computedotnet/package/nuget/Compute.Winx64.IntelMKL) available on MyGet [feed](https://www.myget.org/F/computedotnet/api/v2).
 
## Usage

### Intel MKL Bindings
0. Add the [Compute.NET](https://www.myget.org/feed/Packages/computedotnet) package feed to your NuGet package sources: https://www.myget.org/F/computedotnet/api/v2
1. Install the bindings [package](https://www.myget.org/feed/computedotnet/package/nuget/Compute.Bindings.IntelMKL) into your project: `Install-Package Compute.Bindings.IntelMKL`.
2. (Optional) Install the native library [package](https://www.myget.org/feed/computedotnet/package/nuget/Compute.Winx64.IntelMKL) into your project: `Install-Package Compute.Winx64.IntelMKL`.

Without step 2 you will need to make sure the .NET runtime can locate the native MKL library DLLs or shared library files. You can set your path to include the directory where the library files are located (typically %MKLROOT%\redist). Or you can copy the needed files into your project output directory with a build task.

With the packages installed you can use the MKL BLAS or vector math routines in your code. E.g.:
```
using IntelMKL.ILP64;
class Program
{
	static void Main(string[] args)
	{
    		float[] a = new float[3] { 1f, 2f, 3f };
    		float[] b = new float[3] { 4f, 5f, 6f };
    		float[] r = new float[3];
    		Vml.VsAdd(2, ref a[0], ref b[0], ref r[0]);

    		double[] a1 = new double[3] { 10d, 11d, 12d };
    		double[] a2 = new double[3] { 20d, 22d, 24d };
    		double[] r2 = new double[3];
    		Vml.VdAdd(3, ref a1[0], ref a2[0], ref r2[0]);
	}
}
```

You pass `double[]` and `float[] arrays` to the BLAS `VsAdd` and `VdAdd` functions using a `ref` alias to the first element of the array which is converted to a pointer and passed to the native function. You can use either LP64 or ILP64 array indexing depending on the namespace you import. 

### Bindings Generator
The basic syntax is `bind LIBRARY MODULE [OPTIONS]` e.g ` bind mkl --lp64 --vml -n IntelMKL -o .\Compute.Bindings.IntelMKL` will create bindings for the Intel MKL VML module, with LP64 indexing, in the .NET namespace `IntelMKL` and the `.\Compute.Bindings.IntelMKL` output directory.   
