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
    public class MKL_IgnoreFortranAndUpperCaseDeclsPass : TranslationUnitPass
    {
        protected Generator G;
        protected Library Library;
        public MKL_IgnoreFortranAndUpperCaseDeclsPass(Library lib, Generator gen) : base()
        {
            Library = lib;
            G = gen;
        }

        public override bool VisitFunctionDecl(Function function)
        {
            if (function.PreprocessedEntities.Any(p => p.ToString().StartsWith("_MKL_API") || p.ToString().StartsWith("_mkl_api")))
            {
                function.ExplicitlyIgnore();
                return false;
            }
    
            if (function.Name.EndsWith("_") || function.Name.ToUpperInvariant() == function.Name)
            {
                function.ExplicitlyIgnore();
                return false;
            }
            
            return true;
        }
    }
}
