using System.Collections.Generic;
using System.Linq;
using CppSharp.AST;
using CppSharp.AST.Extensions;
using CppSharp.Generators;
using CppSharp.Generators.AST;
using CppSharp.Generators.CLI;
using CppSharp.Types;
using CppSharp.Generators.CSharp;

namespace Compute.Bindings
{
    //[TypeMap("float[]", GeneratorKind = GeneratorKind.CSharp)]
    public class CFloatArray : TypeMap
    {
        public override string CSharpSignature(TypePrinterContext ctx)
        {
            return CSharpTypePrinter.IntPtrType;
        }

        public override void CSharpMarshalToNative(CSharpMarshalContext ctx)
        {
            ctx.Return.Write(ctx.Parameter.Name);
        }

        public override void CSharpMarshalToManaged(CSharpMarshalContext ctx)
        {
            ctx.Return.Write(ctx.ReturnVarName);
        }

        public override bool DoesMarshalling => true;
    }

    //[TypeMap("double[]", GeneratorKind = GeneratorKind.CSharp)]
    public class CDoubleArray : TypeMap
    {
        public override string CSharpSignature(TypePrinterContext ctx)
        {
            return CSharpTypePrinter.IntPtrType;
        }

        public override void CSharpMarshalToNative(CSharpMarshalContext ctx)
        {
            ctx.Return.Write(ctx.Parameter.Name);
        }

        public override void CSharpMarshalToManaged(CSharpMarshalContext ctx)
        {
            ctx.Return.Write(ctx.ReturnVarName);
        }
        public override bool DoesMarshalling => true;
    }
}
