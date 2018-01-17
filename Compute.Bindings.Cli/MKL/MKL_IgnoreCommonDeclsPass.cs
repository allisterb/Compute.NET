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
    public class MKL_IgnoreCommonDeclsPass : TranslationUnitPass
    {
        protected Generator G;
        protected Library Library;
        public MKL_IgnoreCommonDeclsPass(Library lib, Generator gen) : base()
        {
            G = gen;
            Library = lib;
        }

        public override bool VisitFunctionDecl(Function function)
        {
            if (function.Name.StartsWith("MKL"))
            {
                function.ExplicitlyIgnore();
                return false;
            }
            else
            {
                return true;
            }
        }

        public override bool VisitClassDecl(Class @class)
        {
            if (@class.Name.StartsWith("MKL") || @class.Name.StartsWith("_MKL") || @class.Name.StartsWith("Mkl"))
            {
                @class.ExplicitlyIgnore();
                return false;
            }
            else
            {
                return true;
            }
        }

        public override bool VisitEnumDecl(Enumeration @enum)
        {
            if (@enum.Name.StartsWith("MKL") || @enum.Name.StartsWith("Mkl"))
            {
                @enum.ExplicitlyIgnore();
                return false;
            }
            else
            {
                return true;
            }
        }

    }
}
