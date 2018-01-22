using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using CppSharp;
using CppSharp.AST;
using CppSharp.Generators;
using CppSharp.Passes;

namespace Compute.Bindings
{
    public class CUDA : Library
    {
        #region Constructors
        public CUDA(Dictionary<string, object> options) : base(options)
        {
            
        }
        #endregion

        #region Properties
        public bool cuBlas { get; protected set; }
        public bool Use32bit { get; protected set; }
        #endregion

        #region Overriden members
        public override LibraryKind Kind { get; } = LibraryKind.CUDA;
        public override void Setup(Driver driver)
        {
            base.Setup(driver);
            Module.IncludeDirs.Add(Path.Combine(R, "include"));
            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
            {
                if (Use32bit)
                {
                    Info("Using 32bit CUDA library.");
                    Module.LibraryDirs.Add(Path.Combine(R, "lib", "Win32"));
                }
                else
                {
                    Info("Using 64bit CUDA library.");
                    Module.LibraryDirs.Add(Path.Combine(R, "lib", "x64"));
                }
                //string[] libs = "cublas.lib;cudart.lib;kernel32.lib;user32.lib;gdi32.lib;winspool.lib;comdlg32.lib;advapi32.lib;shell32.lib;ole32.lib;oleaut32.lib;uuid.lib;odbc32.lib;odbccp32.lib;".Split(';');
                string[] libs = "cublas.lib;cudart.lib".Split(';');
                foreach (string s in  libs)
                {
                    this.Module.Libraries.Add(s);
                }
            }
            else throw new PlatformNotSupportedException("Non-Windows platforms not currently supported.");

            if (cuBlas)
            {
                this.Module.Headers.Add("cublas.h");
                this.Module.SharedLibraryName = "cudart64_91";
                Info("Creating bindings for cuBLAS routines...");
            }
        }

        public override void SetupPasses(Driver driver)
        {
            base.SetupPasses(driver);
            if (WithoutCommon)
            {
                driver.AddTranslationUnitPass(new CUDA_IgnoreCommonClassesPass(this, driver.Generator));
            }

            if (cuBlas)
            {
                driver.AddTranslationUnitPass(new CUDA_RemoveCuBlasFunctionPrefixPass(this, driver.Generator));
            }            
        }
        /// Do transformations that should happen before passes are processed.
        public override void Preprocess(Driver driver, ASTContext ctx)
        {

            
        }

        /// Do transformations that should happen after passes are processed.
        public override void Postprocess(Driver driver, ASTContext ctx)
        {
         
            foreach (string s in ClassDecls)
            {
                IEnumerable<Class> classes = ctx.FindClass(s);
                foreach (Class c in classes)
                {
                    if (WithoutCommon && !c.Name.Contains(Class))
                    {
                        ctx.IgnoreClassWithName(c.Name);
                    }
                    else
                    {
                        ctx.SetClassAsValueType(c.Name);
                    }
                    
                }
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
            if (s.Contains($"return ((global::{this.Namespace}.CudaLaunchParams.__Internal*) __Instance)->args;"))
            {
                sb.Replace($"return ((global::{this.Namespace}.CudaLaunchParams.__Internal*) __Instance)->args;", $"return (void**) ((global::{this.Namespace}.CudaLaunchParams.__Internal*) __Instance)->args;");
            }
            if (s.Contains("DllImport(\"cublas\""))
            {
                sb.Replace("DllImport(\"cublas\"", "DllImport(\"cublas64_91\"");
            }
            if (s.Contains("DllImport(\"cudart\""))
            {
                sb.Replace("DllImport(\"cudart\"", "DllImport(\"cudart64_91\"");
            }
            File.WriteAllText(F, sb.ToString());
            return true;
        }

        #endregion
    }
}
