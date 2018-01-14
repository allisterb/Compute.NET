using System;
using System.Collections.Generic;
using System.Text;

using CommandLine;
using CommandLine.Text;

namespace Compute.Bindings
{
    class Options
    {
        [Option('b', "int8", Required = false, HelpText = "Map Int8 or byte types to the specified .NET type.", SetName = "type")]
        public string Int8 { get; set; }

        [Option('c', "char", Required = false, HelpText = "Map char types to the specified .NET type.", SetName = "type")]
        public string Char { get; set; }

        [Option('h', "int16", Required = false, HelpText = "Map Int16 or short integer types to the specified .NET type.", SetName = "type")]
        public string Int16 { get; set; }

        [Option('i', "int32", Required = false, HelpText = "Map Int32 or integer types to the specified .NET type.", SetName = "type")]
        public string Int32 { get; set; }

        [Option('l', "int64", Required = false, HelpText = "Map Int64 or long integer types to the specified .NET type.", SetName = "type")]
        public string Int64 { get; set; }

        [Option('f', "float", Required = false, HelpText = "Map float or single-precision floating point types to the specified .NET type.", SetName = "type")]
        public string Float { get; set; }

        [Option('d', "double", Required = false, HelpText = "Map double-precision floating point type to the specified .NET type.", SetName = "type")]
        public string Double { get; set; }

        [Option('m', "root", Required = false, HelpText = "Set the root directory on the local filesystem for the native comute library.")]
        public string MKLRoot { get; set; }

        [Option('o', "file", Required = false, HelpText = "Set the name and path of the output file for the bindings.")]
        public string OutputFile { get; set; }

    }

    [Verb("blas", HelpText = "Generate bindings for the MKL libraries.")]
    class MKLOptions : Options
    {
        
 
    }
}
