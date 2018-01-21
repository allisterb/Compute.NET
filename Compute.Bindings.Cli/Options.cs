using System;
using System.Collections.Generic;
using System.Text;

using CommandLine;
using CommandLine.Text;

namespace Compute.Bindings
{
    class Options
    {
        /*
        [Option('b', "int8", Required = false, HelpText = "Map int8_t or byte types to the specified .NET type.", SetName = "type")]
        public string Int8 { get; set; }

        [Option('c', "char", Required = false, HelpText = "Map char types to the specified .NET type.", SetName = "type")]
        public string Char { get; set; }

        [Option('h', "int16", Required = false, HelpText = "Map int16_t or short integer types to the specified .NET type.", SetName = "type")]
        public string Int16 { get; set; }

        [Option('i', "int32", Required = false, HelpText = "Map int32_t or integer types to the specified .NET type.", SetName = "type")]
        public string Int32 { get; set; }

        [Option('l', "int64", Required = false, HelpText = "Map int64_t or long integer types to the specified .NET type.", SetName = "type")]
        public string Int64 { get; set; }

        [Option('f', "float", Required = false, HelpText = "Map float or single-precision floating point types to the specified .NET type.", SetName = "type")]
        public string Float { get; set; }

        [Option('d', "double", Required = false, HelpText = "Map double-precision floating point type to the specified .NET type.", SetName = "type")]
        public string Double { get; set; }

        */
        [Option('r', "root", Required = false, HelpText = "Set the root directory on the local filesystem for the native compute library.")]
        public string Root { get; set; }

        [Option('m', "module", Required = false, HelpText = "Specify the name of a module or subset of the library to generate bindings for.")]
        public string ModuleName { get; set; }

        [Option('o', "output", Required = false, HelpText = "Set the output directory for the class file for the bindings.")]
        public string OutputDirName { get; set; }

        [Option('f', "file", Required = false, HelpText = "Set the output filename for the class file for the bindings. If this is not specified then the module name is used.")]
        public string OutputFileName { get; set; }

        [Option('c', "class", Required = false, HelpText = "Specify the class name for the bindings.")]
        public string Class { get; set; }

        [Option('n', "namespace", Required = false, HelpText = "Specify the namespace that the bindings class will belong to.")]
        public string Namespace { get; set; }

        [Option('v', "verbose", Required = false, HelpText = "Enable verbose output from CppSharp.", Default = false)]
        public bool Verbose { get; set; }
    }

    [Verb("mkl", HelpText = "Generate bindings for the Intel Math Kernel Libraries.")]
    class MKLOptions : Options
    {
        [Option("ilp64", Required = false, HelpText = "Use ILP64 integer types.")]
        public bool Ilp64 { get; set; }

        [Option("lp64", Required = false, HelpText = "Use LP64 integer types.")]
        public bool Lp64 { get => !Ilp64; set => Ilp64 = !value; }

        [Option("sequential", Required = false, HelpText = "Use the sequential library threading model.", Default = false)]
        public bool Sequential { get; set; }

        [Option("tbb", Required = false, HelpText = "Use the Intel Thread Building Blocks library threading model.", Default = false)]
        public bool TBB { get; set; }

        [Option("blas", Required = false, HelpText = "Generate bindings for the MKL Basic Linear Algebra Subroutines (BLAS) routines. See https://software.intel.com/en-us/node/528682")]
        public bool Blas { get => ModuleName == "blas"; set => ModuleName = "blas"; }

        [Option("cblas", Required = false, HelpText = "Generate bindings for the MKL C interface to BLAS. See https://software.intel.com/en-us/node/528682")]
        public bool CBlas { get => ModuleName == "cblas"; set => ModuleName = "cblas"; }

        [Option("spblas", Required = false, HelpText = "Generate bindings for the MKL Sparse BLAS routines. See https://software.intel.com/en-us/node/528682")]
        public bool SpBlas { get => ModuleName == "spblas"; set => ModuleName = "spblas"; }

        [Option("blacs", Required = false, HelpText = "Generate bindings for the MKL BLACS routines. See https://software.intel.com/en-us/node/528682")]
        public bool Blacs { get => ModuleName == "blacs"; set => ModuleName = "blacs"; }

        [Option("pblas", Required = false, HelpText = "Generate bindings for the MKL PBLAS (Parallel BLAS) routines. See https://software.intel.com/en-us/node/528682")]
        public bool PBlas { get => ModuleName == "pblas"; set => ModuleName = "pblas"; }

        [Option("vml", Required = false, HelpText = "Generate bindings for the MKL vector math routines. See https://software.intel.com/en-us/node/528682")]
        public bool Vml { get => ModuleName == "vml"; set => ModuleName = "vml"; }

        [Option("lapack", Required = false, HelpText = "Generate bindings for the MKL LAPACK routines. See https://software.intel.com/en-us/node/528682")]
        public bool Lapack { get => ModuleName == "lapack"; set => ModuleName = "lapack"; }

        [Option("scalapack", Required = false, HelpText = "Generate bindings for the MKL SCALAPACK routines. See https://software.intel.com/en-us/node/528682")]
        public bool ScaLapack { get => ModuleName == "scalapack"; set => ModuleName = "scalapack"; }

        [Option("vsl", Required = false, HelpText = "Generate bindings for the MKL vector statistics routines. See https://software.intel.com/en-us/node/528682")]
        public bool Vsl { get => ModuleName == "vsl"; set => ModuleName = "vsl"; }

        [Option("without-common", Required = false, HelpText = "Do not generate bindings for common MKL data structures and functions", Default = false)]
        public bool WithoutCommon { get; set; }
    }

    [Verb("cuda", HelpText = "Generate bindings for the nVidia CUDA libraries.")]
    class CUDAOptions : Options
    {
        [Option("32bit", Required = false, HelpText = "Use the 32-bit versions of the CUDAlibrary. By default the x86_64 version is used.")]
        public bool Use32bit
        {
            get; set;
        }

        [Option("cublas", Required = false, HelpText = "Generate bindings for the NVIDIA cuBLAS routines. See https://developer.nvidia.com/gpu-accelerated-libraries")]
        public bool cuBlas { get => ModuleName == "cublas"; set => ModuleName = "cublas"; }

    }
}