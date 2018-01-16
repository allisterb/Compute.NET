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
        public bool Blas { get; protected set; }
        public bool Ilp64 { get; protected set; }
        public bool Sequential { get; protected set; }
        public bool TBB { get; protected set; }


        #endregion

        #region Overriden members
        public override LibraryKind Kind { get; } = LibraryKind.MKL;
        public override void Setup(Driver driver)
        {
            base.Setup(driver);

            Info($"Using {R} as MKL root directory.");

            Module.IncludeDirs.Add(Path.Combine(R, "include"));

            if (Environment.Is64BitOperatingSystem)
            {   
                Module.LibraryDirs.Add(Path.Combine(R, "lib", "intel64"));                
            }
            else
            {
                Module.LibraryDirs.Add(Path.Combine("lib", "ia32"));
            }

            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
            {
                Module.Libraries.Add("mkl_core_dll.lib");
                if (Ilp64)
                {
                    this.Module.Libraries.Add("mkl_intel_ilp64_dll.lib");
                }
                else
                {
                    this.Module.Libraries.Add("mkl_intel_lp64_dll.lib");
                }

                if (Sequential)
                {
                    this.Module.Libraries.Add("mkl_sequential_dll.lib");
                }
                else if (TBB)
                {
                    this.Module.Libraries.Add("mkl_tbb_dll.lib");
                }
                else
                {
                    this.Module.Libraries.Add("mkl_intel_thread_dll.lib");
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
            else
            {
                throw new NotSupportedException();
            }
            
        }
        #endregion
    }
}
