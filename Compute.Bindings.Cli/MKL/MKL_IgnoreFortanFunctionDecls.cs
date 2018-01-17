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
    public class MKL_IgnoreFortranFunctionDecls : TranslationUnitPass
    {
        protected Generator G;
        public MKL_IgnoreFortranFunctionDecls(Generator gen) : base()
        {
            G = gen;
        }

        public override bool VisitFunctionDecl(Function function)
        {
            if (function.PreprocessedEntities.Any(p => p.ToString().StartsWith("_MKL_API") || p.ToString().StartsWith("_mkl_api")))
            {
                function.Ignore = true;
                return false;
            }
            else
            {
                return true;
            }
            
        }

    }
}
