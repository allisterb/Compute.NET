using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CppSharp;
using CppSharp.AST;
using CppSharp.Generators;
using CppSharp.Passes;

namespace Compute.Bindings
{
    public class MKL : Library
    {
        #region Constructors
        public MKL(Dictionary<string, object> options) : base(options)
        {
            
        }
        #endregion

        #region Properties
        public bool Ilp64 { get; protected set; }
        public bool WithoutCommon { get; protected set; }

        public bool Sequential { get; protected set; }
        public bool TBB { get; protected set; }

        public bool Blas { get; protected set; }
        public bool Vml { get; protected set; }
        public bool Lapack { get; protected set; }
        public bool Vsl { get; protected set; }
        
        #endregion

        #region Overriden members
        public override LibraryKind Kind { get; } = LibraryKind.MKL;
        public override void Setup(Driver driver)
        {
            base.Setup(driver);
            Module.Defines.Add("MKL_CALL_CONV=__cdecl");
            Module.IncludeDirs.Add(Path.Combine(R, "include"));
            if (Environment.Is64BitOperatingSystem)
            {
                Info("Using Intel64 architecture.");
                Module.LibraryDirs.Add(Path.Combine(R, "lib", "intel64"));                
            }
            else
            {
                Info("Using IA32 architecture.");
                Module.LibraryDirs.Add(Path.Combine("lib", "ia32"));
            }
            this.Module.SharedLibraryName = "mkl_rt";
            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
            {
                if (Sequential)
                {
                    this.Module.Libraries.Add("mkl_sequential_dll.lib");
                    Info("Using sequential threading.");
                }
                else if (TBB)
                {
                    this.Module.Libraries.Add("mkl_tbb_dll.lib");
                    Info("Using Intel Thread Building Blocks threading library.");
                }
                else
                {
                    this.Module.Libraries.Add("mkl_intel_thread_dll.lib");
                    Info("Using default Intel threading library.");
                }
                Module.Libraries.Add("mkl_rt.lib");
            }
            else throw new PlatformNotSupportedException("Non-Windows platforms not currently supported.");

            this.Module.Defines.Add("MKL_INT64=long long int");
            this.Module.Defines.Add("MKL_UINT64=unsigned long long int");

            if (!WithoutCommon)
            {
                this.Module.Headers.Add("mkl_version.h");
                this.Module.Headers.Add("mkl_service.h");
            }

            else
            {
                Info("Not binding MKL common data structures and functions.");
            }

            if (Ilp64)
            {
                this.Module.Defines.Add("MKL_ILP64=1");
                this.Module.Defines.Add("MKL_INT=MKL_INT64");
                this.Module.Defines.Add("MKL_UINT=MKL_UINT64");
                Info("Using ILP64 (Int64) array indexing");
            }
            else
            {
                this.Module.Defines.Add("MKL_LP64=1");
                this.Module.Defines.Add("MKL_INT=int");
                this.Module.Defines.Add("MKL_UINT=unsigned int");
                Info("Using LP64 (Int32) array indexing");
            }

            if (Blas)
            {
                this.Module.Headers.Add("mkl_blas.h");    
                Info("Creating bindings for BLAS routines...");
            }
            else if (Lapack)
            {
                this.Module.Headers.Add("mkl_lapack.h");
                Info("Creating bindings for LAPACK routines...");
            }
            else if (Vml)
            {    
                this.Module.Headers.Add("mkl_vml.h");
                Info("Creating bindings for Vector Math routines...");
            }
            else if (Vsl)
            {
                this.ModuleName = "vsl";
                this.Module.Headers.Add("mkl_vsl.h");
                Info("Creating bindings for Vector Statistics routines...");
            }
            else
            {
                throw new InvalidOperationException("Invalid module.");
            }
            
        }

        public override void SetupPasses(Driver driver)
        {
            base.SetupPasses(driver);
            driver.AddTranslationUnitPass(new MKL_IgnoreFortranFunctionDeclsPass(this, driver.Generator)); 
            driver.AddTranslationUnitPass(new MKL_ConvertFunctionParameterDeclsPass(this, driver.Generator));
            if (WithoutCommon)
            {
                driver.AddTranslationUnitPass(new MKL_IgnoreCommonDeclsPass(this, driver.Generator));
            }
        }
        /// Do transformations that should happen before passes are processed.
        public override void Preprocess(Driver driver, ASTContext ctx)
        {
            
               
        }

        /// Do transformations that should happen after passes are processed.
        public override void Postprocess(Driver driver, ASTContext ctx)
        {
            IEnumerable<Class> classes = ctx.FindClass("MKL_Complex8").Concat(ctx.FindClass("MKL_Complex16")).Concat(ctx.FindClass("MKLVersion")).Concat(ctx.FindClass("VSLBRngProperties"));//VSLBRngProperties

            foreach (Class c in classes)
            {
                ctx.SetClassAsValueType(c.Name);
            }
        }

        public override bool CleanAndFixup()
        {
            if (!base.CleanAndFixup())
            {
                return false;
            }
            
            string s = File.ReadAllText(F);
            StringBuilder sb = new StringBuilder(s);
            if (s.Contains($" *(global::{Module.OutputNamespace}.MKL_Complex8.__Internal*) "))
            {
                sb.Replace($" *(global::{Module.OutputNamespace}.MKL_Complex8.__Internal*) ", string.Empty);
            }
            if (s.Contains($" *(global::{Module.OutputNamespace}.MKL_Complex16.__Internal*) "))
            {
                sb.Replace($" *(global::{Module.OutputNamespace}.MKL_Complex16.__Internal*) ", string.Empty);
            }
            if (s.Contains($" *(global::{Module.OutputNamespace}.MKLVersion.__Internal*) "))
            {
                sb.Replace($" *(global::{Module.OutputNamespace}.MKLVersion.__Internal*) ", string.Empty);
            }
            File.WriteAllText(F, sb.ToString());
            return true;
        }

        #endregion

        #region Methods
        #endregion
    }
}
