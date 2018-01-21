using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CppSharp;
using CppSharp.AST;
using CppSharp.Generators;
using CppSharp.Passes;

namespace Compute.Bindings
{
    public class MKL_RemoveCBlasFunctionPrefixPass : TranslationUnitPass
    {
        protected Generator G;
        protected Library Library;
        public MKL_RemoveCBlasFunctionPrefixPass(Library lib, Generator gen) : base()
        {
            Library = lib;
            G = gen;
        }

        public override bool VisitFunctionDecl(Function function)
        {
            if (function.Name.StartsWith("cblas_"))
            {
                function.Name = function.Name.Replace("cblas_", string.Empty);
                return false;
            }
            
            return true;
        }
    }
}
