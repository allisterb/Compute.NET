using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CppSharp;
using CppSharp.AST;
using CppSharp.Generators;
using CppSharp.Passes;

namespace Compute.Bindings
{
    internal class MKLLibrary : Library
    {
        #region Constructors
        internal MKLLibrary(Dictionary<string, object> options) : base(options)
        {
        }
        #endregion



        #region Properties
        public override LibraryKind Kind { get; } = LibraryKind.MKL;
        #endregion
    }
}
