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
    public class CUDA_RemoveCuBlasFunctionPrefixPass : TranslationUnitPass
    {
        protected Generator G;
        protected Library Library;
        public CUDA_RemoveCuBlasFunctionPrefixPass(Library lib, Generator gen) : base()
        {
            Library = lib;
            G = gen;
        }

        public override bool VisitFunctionDecl(Function function)
        {
            if (function.Name.StartsWith("cublas"))
            {
                function.Name = function.Name.Replace("cublas", string.Empty);
                return false;
            }
            
            return true;
        }
    }
}
