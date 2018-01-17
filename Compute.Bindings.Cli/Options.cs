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
        [Option('r', "root", Required = false, HelpText = "Set the root directory on the local filesystem for the native comute library.")]
        public string Root { get; set; }

        [Option('o', "output", Required = false, HelpText = "Set the output directory for the class files for the bindings.")]
        public string OutputDirName { get; set; }

        [Option('m', "module", Required = false, HelpText = "Specify the name of a module or subset of the library to generate bindings to. If this is omitted then all routines will be included.")]
        public string ModuleName { get; set; }

        [Option('n', "namespace", Required = false, HelpText = "Specify the namespace that the bindings will belong to.")]
        public string Namespace { get; set; }

        [Option('v', "verbose", Required = false, HelpText = "Enable verbose output from CppSharp.", Default = false)]
        public bool Verbose { get; set; }
    }

    [Verb("mkl", HelpText = "Generate bindings for the Intel Math Kernel Libraries.")]
    class MKLOptions : Options
    {
        [Option("blas", Required = false, HelpText = "Generate bindings for the MKL BLAS routines. See https://software.intel.com/en-us/node/528682")]
        public bool Blas { get => ModuleName == "blas"; set => ModuleName = "blas"; }

        [Option("vml", Required = false, HelpText = "Generate bindings for the MKL vector math routines. See https://software.intel.com/en-us/node/528682")]
        public bool Vml { get => ModuleName == "vml"; set => ModuleName = "vml"; }

        [Option("sequential", Required = false, HelpText = "Use the sequential library threading model.", Default = false)]
        public bool Sequential { get; set; }

        [Option("tbb", Required = false, HelpText = "Use the Intel Thread Building Blocks library threading model.", Default = false)]
        public bool TBB { get; set; }

        [Option("without-common", Required = false, HelpText = "Do not generate bindings for common MKL data structures and functions", Default = false)]
        public bool WithoutCommon { get; set; }

    }
}
