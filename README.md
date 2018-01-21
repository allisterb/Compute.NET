# Compute.NET: .NET bindings for native numerical computing
![bind program screenshot](https://cmewmw.dm2301.livefilestore.com/y4mojOOFQ38XiWWFEhjDv855htWsnP-nFXeDW2Rtnm3z4csuLumwHDe7_YvHWvCOQEvTI00N6vV4ZDk6CcTHxQz6XbK0GEwJnHpA0lDoOFWl-26Goi6UihJMK32cGGhSNsGv2m_loxDF1cUncS2Qj1Uv9Ly5A6TeN9S2thjBtOkSMKxfI5K6XlUKITGtwd1xhuH4ADNHmelYoNW9AypM5j9hA?width=845&height=394&cropmode=none)

Get the latest release from the Compute.NET [package feed](https://www.myget.org/feed/Packages/computedotnet).

## About
Compute.NET provides auto-generated bindings for native numerical computing libraries like [Intel Math Kernel Library](https://software.intel.com/en-us/mkl), [AMD Core Math Library](https://developer.amd.com/tools-and-sdks/archive/acml-downloads-resources/) (and its successors), AMD [clBLAS](https://gpuopen.com/compute-product/clblas/), cl* and others. The bindings are auto-generated from the library's C headers using the excellent [CppSharp](https:/github.com/Mono/CppSharp) library. The generator is a CLI program that be can used to generate individual modules of each library as well as customize key aspects of the generated code, such as the use of .NET structs instead of classes for complex data types, and marshalling array parameters in native code functions (either as managed arrays or pointers.) 

## Status
* CLI Bindings Generator: Works on Windows for Intel Math Kernel Library.

* Bindings: 
	* Compute.Bindings.IntelMKL [package](https://www.myget.org/feed/computedotnet/package/nuget/Compute.Bindings.IntelMKL) available on Myget [feed](https://www.myget.org/F/computedotnet/api/v2). This library is not Windows-specific but I haven't tested it on Linux or other platforms yet. The following modules are available:
		* BLAS, CBLAS, SpBLAS, and PBLAS
		* LAPACK and SCALAPACK
		* VML
		* VSL
	

* Native Library Packages: 
	* Compute.Winx64.IntelMKL [package](https://www.myget.org/feed/computedotnet/package/nuget/Compute.Winx64.IntelMKL) available on MyGet [feed](https://www.myget.org/F/computedotnet/api/v2).
 
## Usage

### Intel MKL Bindings
0. Add the [Compute.NET](https://www.myget.org/feed/Packages/computedotnet) package feed to your NuGet package sources: https://www.myget.org/F/computedotnet/api/v2
1. Install the bindings [package](https://www.myget.org/feed/computedotnet/package/nuget/Compute.Bindings.IntelMKL) into your project: `Install-Package Compute.Bindings.IntelMKL`.
2. (Optional) Install the native library [package](https://www.myget.org/feed/computedotnet/package/nuget/Compute.Winx64.IntelMKL) into your project: `Install-Package Compute.Winx64.IntelMKL`.

Without step 2 you will need to make sure the .NET runtime can locate the native MKL library DLLs or shared library files. You can set your path to include the directory where the library files are located (typically %MKLROOT%\redist). Or you can copy the needed files into your project output directory with a build task.

With the packages installed you can use the MKL BLAS or vector math or other routines in your code. E.g the following code is translated from the Intel MKL examples for CBLAS:
```
	using IntelMKL.ILP64;
	public class BlasExamples
    {
        public const int GENERAL_MATRIX = 0;
        public const int UPPER_MATRIX = 1;
        public const int LOWER_MATRIX = -1;

        public void RunBlasExample1()
        {
            int m = 3, n = 2, i, j;
            int lda = 3, ldb = 3, ldc = 3;
            int rmaxa, cmaxa, rmaxb, cmaxb, rmaxc, cmaxc;
            float alpha = 0.5f, beta = 2.0f;
            float[] a, b, c;
            CBLAS_LAYOUT layout = CBLAS_LAYOUT.CblasRowMajor;
            CBLAS_SIDE side = CBLAS_SIDE.CblasLeft;
            CBLAS_UPLO uplo = CBLAS_UPLO.CblasUpper;
            int ma, na, typeA;
            if (side == CBLAS_SIDE.CblasLeft)
            {
                rmaxa = m + 1;
                cmaxa = m;
                ma = m;
                na = m;
            }
            else
            {
                rmaxa = n + 1;
                cmaxa = n;
                ma = n;
                na = n;
            }
            rmaxb = m + 1;
            cmaxb = n;
            rmaxc = m + 1;
            cmaxc = n;
            a = new float[rmaxa * cmaxa];
            b = new float[rmaxb * cmaxb];
            c = new float[rmaxc * cmaxc];
            if (layout == CBLAS_LAYOUT.CblasRowMajor)
            {
                lda = cmaxa;
                ldb = cmaxb;
                ldc = cmaxc;
            }
            else
            {
                lda = rmaxa;
                ldb = rmaxb;
                ldc = rmaxc;
            }
            if (uplo == CBLAS_UPLO.CblasUpper)
                typeA = UPPER_MATRIX;
            else
                typeA = LOWER_MATRIX;
            for (i = 0; i < m; i++)
            {
                for (j = 0; j < m; j++)
                {
                    a[i + j * lda] = 1.0f;
                }
            }
            for (i = 0; i < m; i++)
            {
                for (j = 0; j < n; j++)
                {
                    c[i + j * ldc] = 1.0f;
                    b[i + j * ldb] = 2.0f;
                }
            } 
            CBlas.Ssymm(layout, side, uplo, m, n, alpha, ref a[0], lda, ref b[0], ldb, beta, ref c[0], ldc);
        }
    }
```

Enums like `CBLAS_UPLO` are generated from the CBLAS header file. You pass `double[]` and `float[]` arrays to the BLAS functions using a `ref` alias to the first element of the array which is converted to a pointer and passed to the native function. You can use either LP64 or ILP64 array indexing depending on the namespace you import. 

### Bindings Generator
The basic syntax is `bind LIBRARY MODULE [OPTIONS]` e.g ` bind mkl --vml --ilp64 -n IntelMKL -o .\Compute.Bindings.IntelMKL -c Vml --file vml.ilp64.cs` will create bindings for the Intel MKL VML routines, with ILP64 array indexing, in the .NET class `Vml` and namespace `IntelMKL` and in the file `vmk.ilp64.cs` in the `.\Compute.Bindings.IntelMKL` output directory.   
