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
    public class CUDA_IgnoreCommonClassesPass : TranslationUnitPass
    {
        protected Generator G;
        protected Library Library;
        public CUDA_IgnoreCommonClassesPass(Library lib, Generator gen) : base()
        {
            Library = lib;
            G = gen;
        }

        public override bool VisitClassDecl(Class @class)
        {
            @class.ExplicitlyIgnore();
            return false;
        }

        public override bool VisitClassTemplateDecl(ClassTemplate template)
        {

            template.ExplicitlyIgnore();
            return false;
        }

        public override bool VisitClassTemplateSpecializationDecl(ClassTemplateSpecialization specialization)
        {
            specialization.ExplicitlyIgnore();
            return false;
        }

    }
}
