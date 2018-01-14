using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CppSharp;
using CppSharp.AST;
using CppSharp.Generators;
using CppSharp.Passes;

namespace Compute.Bindings
{
    internal class MKLLibrary : ILibrary
    {
        #region Constructors
        internal MKLLibrary(Dictionary<string, object> options)
        {
            BindOptions = options;
            foreach (System.Reflection.PropertyInfo prop in this.GetType().GetProperties())
            {
                if (BindOptions.ContainsKey(prop.Name) && prop.CanWrite)
                {
                    prop.SetValue(this, BindOptions[prop.Name]);
                }
            }
            Contract.Requires(!string.IsNullOrEmpty(MKLRoot));
        }
        #endregion

        #region Implemented members
        /// Setup the driver options here.
        public void Setup(Driver driver)
        {
            DriverOptions options = driver.Options;
            options.GeneratorKind = GeneratorKind.CSharp;
            Module module = options.AddModule("jemalloc");
            module.Defines.AddRange(@"JEMALLOC_NO_PRIVATE_NAMESPACE;REENTRANT;WINDLL;DLLEXPORT;JEMALLOC_DEBUG;DEBUG".Split(';'));
            module.IncludeDirs.Add(@"..\..\jemalloc\include\jemalloc");
            module.IncludeDirs.Add(@"..\..\jemalloc\include\jemalloc\internal");
            module.IncludeDirs.Add(@"..\..\jemalloc\include\msvc_compat");
            module.IncludeDirs.Add(@".\");
            module.Headers.Add("jemalloc-win-msvc.h");
            module.LibraryDirs.Add(@".\");
            module.Libraries.Add("jemallocd.lib");
            module.OutputNamespace = "jemalloc";
            options.OutputDir = @".\";
            options.Verbose = true;
            
        }

        /// Setup your passes here.
        public void SetupPasses(Driver driver)
        {
            //driver.AddGeneratorOutputPass(new )
        }

        /// Do transformations that should happen before passes are processed.
        public void Preprocess(Driver driver, ASTContext ctx)
        {
            //ctx.SetMet
        }

        /// Do transformations that should happen after passes are processed.
        public void Postprocess(Driver driver, ASTContext ctx)
        {
            ctx.SetClassBindName("ExtentHooksS", "ExtentHooks");
            //driver.Options.
        }
        #endregion

        #region Properties
        public Dictionary<string, object> BindOptions { get; internal set; }
        public string MKLRoot { get; internal set; }
        #endregion
    }
}
