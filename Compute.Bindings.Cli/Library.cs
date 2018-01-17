using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Serilog;
using CppSharp;
using CppSharp.AST;
using CppSharp.Generators;

namespace Compute.Bindings
{
    public abstract class Library : ILibrary
    {
        public enum LibraryKind
        {
            MKL,
            ACML
        }
        
        #region Constructors
        public Library(Dictionary<string, object> options)
        {
            BindOptions = options;
            foreach (System.Reflection.PropertyInfo prop in this.GetType().GetProperties())
            {
                if (BindOptions.ContainsKey(prop.Name) && prop.CanWrite)
                {
                    prop.SetValue(this, BindOptions[prop.Name]);
                }
            }
            Contract.Requires(!ReferenceEquals(RootDirectory, null));
            Contract.Requires(RootDirectory.Exists);
            if (string.IsNullOrEmpty(OutputDirName))
            {
                OutputDirName = Directory.GetCurrentDirectory();
            }
            if (string.IsNullOrEmpty(ClassName))
            {
                ClassName = ModuleName;
            }
            if (string.IsNullOrEmpty(Namespace))
            {
                Namespace = Name;
            }
            Contract.Requires(!string.IsNullOrEmpty(OutputDirName));
            Contract.Requires(!string.IsNullOrEmpty(ClassName));
            Contract.Requires(!string.IsNullOrEmpty(Namespace));
        }
        #endregion

        #region Implemented members
        /// Setup the driver options here.
        public virtual void Setup(Driver driver)
        {
            DriverOptions options = driver.Options;
            options.GeneratorKind = GeneratorKind.CSharp;
            options.Verbose = this.Verbose;
            Module = options.AddModule(ModuleName);
            Module.OutputNamespace = Namespace;
            options.OutputDir = OutputDirName;

        }

        /// Setup your passes here.
        public virtual void SetupPasses(Driver driver)
        {
           
        }

        /// Do transformations that should happen before passes are processed.
        public virtual void Preprocess(Driver driver, ASTContext ctx)
        {
            
        }

        /// Do transformations that should happen after passes are processed.
        public virtual void Postprocess(Driver driver, ASTContext ctx)
        {
        }
        #endregion


        #region Properties
        public ILogger L { get; } = Log.Logger.ForContext<Library>();
        public abstract LibraryKind Kind { get; }
        public string Name => Kind.ToString();
        public Dictionary<string, object> BindOptions { get; internal set; }
        public DirectoryInfo RootDirectory { get; internal set; }
        public string R => RootDirectory.FullName;
        public string OutputDirName { get; internal set; }
        public string ModuleName { get; internal set; }
        public Module Module { get; internal set; }
        public string ClassName { get; internal set; }
        public string Namespace { get; internal set; }
        public bool Verbose { get; internal set; }
        #endregion

        #region Methods
        protected void Info(string m, params object[] o) => L.Information(m, o); 
        #endregion
    }
}
