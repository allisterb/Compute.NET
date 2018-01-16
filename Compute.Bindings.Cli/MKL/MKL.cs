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
        public bool Sequential { get; protected set; }
        public bool TBB { get; protected set; }

        public bool Blas { get; protected set; }
        public bool Vml { get; protected set; }

        #endregion

        #region Overriden members
        public override LibraryKind Kind { get; } = LibraryKind.MKL;
        public override void Setup(Driver driver)
        {
            base.Setup(driver);

            Info($"Using {R} as MKL root directory.");
            Module.Defines.Add("MKL_CALL_CONV=__cdecl");
            Module.IncludeDirs.Add(Path.Combine(R, "include"));

            if (Environment.Is64BitOperatingSystem)
            {
                Info("Using intel64 architecture.");
                Module.LibraryDirs.Add(Path.Combine(R, "lib", "intel64"));                
            }
            else
            {
                Info("Using ia32 architecture.");
                Module.LibraryDirs.Add(Path.Combine("lib", "ia32"));
            }

            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
            {
                Module.Libraries.Add("mkl_core_dll.lib");
                if (Ilp64)
                {
                    this.Module.Libraries.Add("mkl_intel_ilp64_dll.lib");
                    Info("Using ilp64 array interface.");
                }
                else
                {
                    this.Module.Libraries.Add("mkl_intel_lp64_dll.lib");
                    Info("Using lp64 array interface.");
                }

                if (Sequential)
                {
                    this.Module.Libraries.Add("mkl_sequential_dll.lib");
                    Info("Using sequential threading.");
                }
                else if (TBB)
                {
                    this.Module.Libraries.Add("mkl_tbb_dll.lib");
                    Info("Using Intel Thread Building Blocks threading.");
                }
                else
                {
                    this.Module.Libraries.Add("mkl_intel_thread_dll.lib");
                    Info("Using default threading.");
                }
            }
            else throw new PlatformNotSupportedException("Non-Windows platforms not currently supported.");

            if (Blas)
            {
                this.ModuleName = "blas";
                this.Module.LibraryName = "blas";
                this.Module.Headers.Add("mkl_blas.h");    
                Info("Creating bindings for Blas routines.");
            }
            else if (Vml)
            {
                this.ModuleName = "vml";
                this.Module.LibraryName = "vml";
                this.Module.Headers.Add("mkl_vml.h");
                Info("Creating bindings for Vector Math routines.");
            }
            else
            {
                throw new NotSupportedException();
            }
            
        }

        public override void SetupPasses(Driver driver)
        {
            base.SetupPasses(driver);
            driver.AddTranslationUnitPass(new MKL_IgnoreFortranFunctionDecls(driver.Generator));
        }
        /// Do transformations that should happen before passes are processed.
        public override void Preprocess(Driver driver, ASTContext ctx)
        {
            
               
        }

        /// Do transformations that should happen after passes are processed.
        public override void Postprocess(Driver driver, ASTContext ctx)
        {
            IEnumerable<Class> classes = ctx.FindClass("MKL_Complex8");
            foreach(Class c in classes)
            {
                ctx.SetClassAsValueType(c.Name);
            }

            classes = ctx.FindClass("MKL_Complex16");
            foreach (Class c in classes)
            {
                ctx.SetClassAsValueType(c.Name);
            }

            classes = ctx.FindClass("MKLVersion");
            foreach (Class c in classes)
            {
                ctx.SetClassAsValueType(c.Name);

            }
            classes = ctx.FindClass("DefVmlErrorContext");
            foreach (Class c in classes)
            {
                ctx.SetClassAsValueType(c.Name);
            }
        }

        #endregion
    }
}
