﻿using System;
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
    public class ConvertFunctionParameterDeclsPass : TranslationUnitPass
    {
        protected Generator G;
        protected Library Library;
        public ConvertFunctionParameterDeclsPass(Library lib, Generator gen) : base()
        {
            G = gen;
            Library = lib;
        }

        public override bool VisitParameterDecl(Parameter parameter)
        {
            switch (parameter.Type)
            {
                case ArrayType a:
                    parameter.QualifiedType = new QualifiedType(new PointerType(new QualifiedType(a.Type)), parameter.QualifiedType.Qualifiers);
                    break;                  
                default:
                    break;
            }
            return true;
        }
    }
}
