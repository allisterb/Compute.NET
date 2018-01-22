﻿using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
            ACML,
            CUDA
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
            if (string.IsNullOrEmpty(Class))
            {
                Class = ModuleName;
            }
            if (string.IsNullOrEmpty(Namespace))
            {
                Namespace = Name;
            }
            
            Contract.Requires(!string.IsNullOrEmpty(ModuleName));
            Contract.Requires(!string.IsNullOrEmpty(OutputDirName));
            Contract.Requires(!string.IsNullOrEmpty(Class));
            Contract.Requires(!string.IsNullOrEmpty(Namespace));
            F = Path.Combine(Path.GetFullPath(OutputDirName), ModuleName + ".cs");

            Info($"Binding library module {ModuleName}.");
            Info($"Using {R} as library directory.");
            Info($"Using {Path.GetFullPath(OutputDirName)} as output directory.");
            Info($"Using {Namespace} as library namespace.");
            
            if (File.Exists(F))
            {
                Warn($"Module file {F} will be overwritten.");
            }
            else
            {
                Info($"Module file is {F}.");
            }
        }
        #endregion

        #region Implemented members
        /// Setup the driver options here.
        public virtual void Setup(Driver driver)
        {
            DriverOptions options = driver.Options;
            options.GeneratorKind = GeneratorKind.CSharp;
            options.Verbose = this.Verbose;
            options.MarshalCharAsManagedChar = false;
            Module = options.AddModule(ModuleName);
            Module.OutputNamespace = Namespace;
            options.OutputDir = OutputDirName;
            options.GenerateSingleCSharpFile = true;
        }

        /// Setup your passes here.
        public virtual void SetupPasses(Driver driver)
        {
            driver.AddTranslationUnitPass(new GetAllClassDeclsPass(this, driver.Generator));
            driver.AddTranslationUnitPass(new ConvertFunctionParameterDeclsPass(this, driver.Generator));
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
        public string F { get; protected set; } 
        public string OutputDirName { get; internal set; }
        public string OutputFileName { get; internal set; }
        public string ModuleName { get; internal set; }
        public Module Module { get; internal set; }
        public string Class { get; internal set; }
        public string Namespace { get; internal set; }
        public bool WithoutCommon { get; protected set; }
        public bool Verbose { get; internal set; }
        #endregion

        #region Methods
        public virtual bool CleanAndFixup()
        {
            if (File.Exists(Path.Combine(OutputDirName, Module.OutputNamespace + "-symbols.cpp")))
            {
                File.Delete(Path.Combine(OutputDirName, Module.OutputNamespace + "-symbols.cpp"));
                Info($"Removing unneeded file {Path.Combine(OutputDirName, Module.OutputNamespace + "-symbols.cpp")}");
            }
            if (File.Exists(Path.Combine(OutputDirName, "Std.cs")))
            {
                File.Delete(Path.Combine(OutputDirName, "Std.cs"));
                Info($"Removing unneeded file {Path.Combine(OutputDirName, "Std.cs")}");
            }
            if (!string.IsNullOrEmpty(OutputFileName))
            {
                string f = Path.Combine(Path.GetFullPath(OutputDirName), OutputFileName);
                if (!string.IsNullOrEmpty(OutputFileName) && F != f)
                {
                    if (Environment.OSVersion.Platform == PlatformID.Win32NT && F.ToLowerInvariant() == f.ToLowerInvariant())
                    {

                    }
                    else if (File.Exists(f))
                    {
                        Warn($"Overwriting file {f}.");
                        File.Delete(f);
                    }
                    File.Move(F, f);
                    F = f;
                }
            }
            if (!string.IsNullOrEmpty(Class))
            {
                string s = File.ReadAllText(F);
                s = Regex.Replace(s, $"public unsafe partial class {ModuleName}\\r?$", "public unsafe partial class " + Class, RegexOptions.Multiline);
                File.WriteAllText(F, s);
            }
            return true;
        }

        protected void Info(string m, params object[] o) => L.Information(m, o);
        protected void Warn(string m, params object[] o) => L.Warning(m, o);
        protected void Error(string m, params object[] o) => L.Error(m, o);
        #endregion

        #region Fields
        public List<string> ClassDecls = new List<string>();
        #endregion
    }
}
