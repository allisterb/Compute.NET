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

        [Option('r', "root", Required = false, HelpText = "Set the root directory on the local filesystem for the native comute library.")]
        public string Root { get; set; }

        [Option('o', "output", Required = false, HelpText = "Set the output directory for the class files for the bindings.")]
        public string OutputDirName { get; set; }

        [Option("class", Required = false, HelpText = "Specify the name of the .NET class that the binding methods will be belong to.")]
        public string ClassName { get; set; }

        [Option('n', "namespace", Required = false, HelpText = "Specify the namespace that the bindings will belong to.")]
        public string Namespace { get; set; }

    }

    [Verb("mkl", HelpText = "Generate bindings for the Intel Math Kernel Libraries.")]
    class MKLOptions : Options
    {
       
 
    }
}
