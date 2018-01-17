using System;
using System.Runtime.InteropServices;
using System.Security;

namespace IntelMKL
{
    public enum MKL_MIC_TARGET_TYPE
    {
        MKL_TARGET_NONE = 0,
        MKL_TARGET_HOST = 1,
        MKL_TARGET_MIC = 2
    }

    public enum MKL_BLACS
    {
        MKL_BLACS_CUSTOM = 0,
        MKL_BLACS_MSMPI = 1,
        MKL_BLACS_INTELMPI = 2,
        MKL_BLACS_MPICH2 = 3,
        MKL_BLACS_LASTMPI = 4
    }

    public enum MKL_LAYOUT
    {
        MKL_ROW_MAJOR = 101,
        MKL_COL_MAJOR = 102
    }

    public enum MKL_TRANSPOSE
    {
        MKL_NOTRANS = 111,
        MKL_TRANS = 112,
        MKL_CONJTRANS = 113
    }

    public enum MKL_UPLO
    {
        MKL_UPPER = 121,
        MKL_LOWER = 122
    }

    public enum MKL_DIAG
    {
        MKL_NONUNIT = 131,
        MKL_UNIT = 132
    }

    public enum MKL_SIDE
    {
        MKL_LEFT = 141,
        MKL_RIGHT = 142
    }

    public enum MKL_COMPACT_PACK
    {
        MKL_COMPACT_SSE = 181,
        MKL_COMPACT_AVX = 182,
        MKL_COMPACT_AVX512 = 183
    }


    [SuppressUnmanagedCodeSecurity, UnmanagedFunctionPointer(global::System.Runtime.InteropServices.CallingConvention.Cdecl)]
    public unsafe delegate void XerblaEntry([MarshalAs(UnmanagedType.LPStr)] string Name, int* Num, int Len);

    [SuppressUnmanagedCodeSecurity, UnmanagedFunctionPointer(global::System.Runtime.InteropServices.CallingConvention.Cdecl)]
    public unsafe delegate int ProgressEntry(int* thread, int* step, sbyte* stage, int stage_len);

    [SuppressUnmanagedCodeSecurity, UnmanagedFunctionPointer(global::System.Runtime.InteropServices.CallingConvention.Cdecl)]
    public unsafe delegate void MKLExitHandler(int why);

    public unsafe partial class Service
    {
        public partial struct __Internal
        {
            [SuppressUnmanagedCodeSecurity]
            [DllImport("mkl_rt", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl,
                EntryPoint = "MKL_Get_Version")]
            internal static extern void MKL_GetVersion(global::System.IntPtr ver);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("mkl_rt", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl,
                EntryPoint = "MKL_Get_Version_String")]
            internal static extern void MKL_GetVersionString(sbyte* buffer, int len);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("mkl_rt", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl,
                EntryPoint = "MKL_Free_Buffers")]
            internal static extern void MKL_FreeBuffers();

            [SuppressUnmanagedCodeSecurity]
            [DllImport("mkl_rt", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl,
                EntryPoint = "MKL_Thread_Free_Buffers")]
            internal static extern void MKL_ThreadFreeBuffers();

            [SuppressUnmanagedCodeSecurity]
            [DllImport("mkl_rt", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl,
                EntryPoint = "MKL_Mem_Stat")]
            internal static extern long MKL_MemStat(int* nbuffers);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("mkl_rt", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl,
                EntryPoint = "MKL_Peak_Mem_Usage")]
            internal static extern long MKL_PeakMemUsage(int reset);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("mkl_rt", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl,
                EntryPoint = "MKL_malloc")]
            internal static extern global::System.IntPtr MKL_malloc(ulong size, int align);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("mkl_rt", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl,
                EntryPoint = "MKL_calloc")]
            internal static extern global::System.IntPtr MKL_calloc(ulong num, ulong size, int align);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("mkl_rt", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl,
                EntryPoint = "MKL_realloc")]
            internal static extern global::System.IntPtr MKL_realloc(global::System.IntPtr ptr, ulong size);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("mkl_rt", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl,
                EntryPoint = "MKL_free")]
            internal static extern void MKL_free(global::System.IntPtr ptr);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("mkl_rt", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl,
                EntryPoint = "MKL_Disable_Fast_MM")]
            internal static extern int MKL_DisableFastMM();

            [SuppressUnmanagedCodeSecurity]
            [DllImport("mkl_rt", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl,
                EntryPoint = "MKL_Get_Cpu_Clocks")]
            internal static extern void MKL_GetCpuClocks(ulong* _0);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("mkl_rt", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl,
                EntryPoint = "MKL_Get_Cpu_Frequency")]
            internal static extern double MKL_GetCpuFrequency();

            [SuppressUnmanagedCodeSecurity]
            [DllImport("mkl_rt", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl,
                EntryPoint = "MKL_Get_Max_Cpu_Frequency")]
            internal static extern double MKL_GetMaxCpuFrequency();

            [SuppressUnmanagedCodeSecurity]
            [DllImport("mkl_rt", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl,
                EntryPoint = "MKL_Get_Clocks_Frequency")]
            internal static extern double MKL_GetClocksFrequency();

            [SuppressUnmanagedCodeSecurity]
            [DllImport("mkl_rt", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl,
                EntryPoint = "MKL_Set_Num_Threads_Local")]
            internal static extern int MKL_SetNumThreadsLocal(int nth);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("mkl_rt", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl,
                EntryPoint = "MKL_Set_Num_Threads")]
            internal static extern void MKL_SetNumThreads(int nth);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("mkl_rt", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl,
                EntryPoint = "MKL_Get_Max_Threads")]
            internal static extern int MKL_GetMaxThreads();

            [SuppressUnmanagedCodeSecurity]
            [DllImport("mkl_rt", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl,
                EntryPoint = "MKL_Set_Num_Stripes")]
            internal static extern void MKL_SetNumStripes(int nstripes);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("mkl_rt", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl,
                EntryPoint = "MKL_Get_Num_Stripes")]
            internal static extern int MKL_GetNumStripes();

            [SuppressUnmanagedCodeSecurity]
            [DllImport("mkl_rt", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl,
                EntryPoint = "MKL_Domain_Set_Num_Threads")]
            internal static extern int MKL_DomainSetNumThreads(int nth, int MKL_DOMAIN);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("mkl_rt", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl,
                EntryPoint = "MKL_Domain_Get_Max_Threads")]
            internal static extern int MKL_DomainGetMaxThreads(int MKL_DOMAIN);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("mkl_rt", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl,
                EntryPoint = "MKL_Set_Dynamic")]
            internal static extern void MKL_SetDynamic(int bool_MKL_DYNAMIC);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("mkl_rt", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl,
                EntryPoint = "MKL_Get_Dynamic")]
            internal static extern int MKL_GetDynamic();

            [SuppressUnmanagedCodeSecurity]
            [DllImport("mkl_rt", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl,
                EntryPoint = "MKL_PROGRESS")]
            internal static extern int MKL_PROGRESS(int* thread, int* step, sbyte* stage, int lstage);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("mkl_rt", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl,
                EntryPoint = "MKL_PROGRESS_")]
            internal static extern int MKL_PROGRESS_(int* thread, int* step, sbyte* stage, int lstage);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("mkl_rt", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl,
                EntryPoint = "mkl_progress")]
            internal static extern int MklProgress(int* thread, int* step, sbyte* stage, int lstage);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("mkl_rt", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl,
                EntryPoint = "mkl_progress_")]
            internal static extern int mkl_progress_(int* thread, int* step, sbyte* stage, int lstage);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("mkl_rt", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl,
                EntryPoint = "MKL_Enable_Instructions")]
            internal static extern int MKL_EnableInstructions(int _0);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("mkl_rt", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl,
                EntryPoint = "MKL_Set_Interface_Layer")]
            internal static extern int MKL_SetInterfaceLayer(int code);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("mkl_rt", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl,
                EntryPoint = "MKL_Set_Threading_Layer")]
            internal static extern int MKL_SetThreadingLayer(int code);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("mkl_rt", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl,
                EntryPoint = "mkl_set_xerbla")]
            internal static extern global::System.IntPtr MklSetXerbla(global::System.IntPtr xerbla);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("mkl_rt", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl,
                EntryPoint = "mkl_set_progress")]
            internal static extern global::System.IntPtr MklSetProgress(global::System.IntPtr progress);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("mkl_rt", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl,
                EntryPoint = "MKL_MIC_Enable")]
            internal static extern int MKL_MIC_Enable();

            [SuppressUnmanagedCodeSecurity]
            [DllImport("mkl_rt", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl,
                EntryPoint = "MKL_MIC_Disable")]
            internal static extern int MKL_MIC_Disable();

            [SuppressUnmanagedCodeSecurity]
            [DllImport("mkl_rt", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl,
                EntryPoint = "MKL_MIC_Get_Device_Count")]
            internal static extern int MKL_MIC_GetDeviceCount();

            [SuppressUnmanagedCodeSecurity]
            [DllImport("mkl_rt", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl,
                EntryPoint = "MKL_MIC_Get_Cpuinfo")]
            internal static extern int MKL_MIC_GetCpuinfo(global::IntelMKL.MKL_MIC_TARGET_TYPE target_type, int target_number, int* ncores, int* nthreads, double* freq);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("mkl_rt", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl,
                EntryPoint = "MKL_MIC_Get_Meminfo")]
            internal static extern int MKL_MIC_GetMeminfo(global::IntelMKL.MKL_MIC_TARGET_TYPE target_type, int target_number, int* totalmem, int* freemem);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("mkl_rt", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl,
                EntryPoint = "MKL_MIC_Set_Workdivision")]
            internal static extern int MKL_MIC_SetWorkdivision(global::IntelMKL.MKL_MIC_TARGET_TYPE target_type, int target_number, double wd);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("mkl_rt", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl,
                EntryPoint = "MKL_MIC_Get_Workdivision")]
            internal static extern int MKL_MIC_GetWorkdivision(global::IntelMKL.MKL_MIC_TARGET_TYPE target_type, int target_number, double* wd);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("mkl_rt", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl,
                EntryPoint = "MKL_MIC_Set_Max_Memory")]
            internal static extern int MKL_MIC_SetMaxMemory(global::IntelMKL.MKL_MIC_TARGET_TYPE target_type, int target_number, ulong card_mem_mbytes);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("mkl_rt", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl,
                EntryPoint = "MKL_MIC_Free_Memory")]
            internal static extern int MKL_MIC_FreeMemory(global::IntelMKL.MKL_MIC_TARGET_TYPE target_type, int target_number);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("mkl_rt", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl,
                EntryPoint = "MKL_MIC_Set_Offload_Report")]
            internal static extern int MKL_MIC_SetOffloadReport(int enabled);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("mkl_rt", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl,
                EntryPoint = "MKL_MIC_Set_Device_Num_Threads")]
            internal static extern int MKL_MIC_SetDeviceNumThreads(global::IntelMKL.MKL_MIC_TARGET_TYPE target_type, int target_number, int num_threads);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("mkl_rt", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl,
                EntryPoint = "MKL_MIC_Set_Resource_Limit")]
            internal static extern int MKL_MIC_SetResourceLimit(double fraction);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("mkl_rt", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl,
                EntryPoint = "MKL_MIC_Get_Resource_Limit")]
            internal static extern int MKL_MIC_GetResourceLimit(double* fraction);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("mkl_rt", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl,
                EntryPoint = "MKL_MIC_Get_Flags")]
            internal static extern int MKL_MIC_GetFlags();

            [SuppressUnmanagedCodeSecurity]
            [DllImport("mkl_rt", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl,
                EntryPoint = "MKL_MIC_Set_Flags")]
            internal static extern int MKL_MIC_SetFlags(int flag);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("mkl_rt", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl,
                EntryPoint = "MKL_MIC_Get_Status")]
            internal static extern int MKL_MIC_GetStatus();

            [SuppressUnmanagedCodeSecurity]
            [DllImport("mkl_rt", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl,
                EntryPoint = "MKL_MIC_Clear_Status")]
            internal static extern void MKL_MIC_ClearStatus();

            [SuppressUnmanagedCodeSecurity]
            [DllImport("mkl_rt", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl,
                EntryPoint = "MKL_CBWR_Get")]
            internal static extern int MKL_CBWR_Get(int _0);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("mkl_rt", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl,
                EntryPoint = "MKL_CBWR_Set")]
            internal static extern int MKL_CBWR_Set(int _0);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("mkl_rt", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl,
                EntryPoint = "MKL_CBWR_Get_Auto_Branch")]
            internal static extern int MKL_CBWR_GetAutoBranch();

            [SuppressUnmanagedCodeSecurity]
            [DllImport("mkl_rt", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl,
                EntryPoint = "MKL_Set_Env_Mode")]
            internal static extern int MKL_SetEnvMode(int _0);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("mkl_rt", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl,
                EntryPoint = "MKL_Verbose")]
            internal static extern int MKL_Verbose(int _0);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("mkl_rt", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl,
                EntryPoint = "MKL_Set_Exit_Handler")]
            internal static extern void MKL_SetExitHandler(global::System.IntPtr h);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("mkl_rt", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl,
                EntryPoint = "MKL_MIC_register_memory")]
            internal static extern void MKL_MIC_registerMemory(int enable);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("mkl_rt", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl,
                EntryPoint = "MKL_Set_Memory_Limit")]
            internal static extern int MKL_SetMemoryLimit(int mem_type, ulong limit);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("mkl_rt", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl,
                EntryPoint = "MKL_Set_mpi")]
            internal static extern int MKL_SetMpi(int vendor, [MarshalAs(UnmanagedType.LPStr)] string custom_library_name);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("mkl_rt", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl,
                EntryPoint = "MKL_Finalize")]
            internal static extern void MKL_Finalize();
        }

        public static void MKL_GetVersion(global::IntelMKL.MKLVersion ver)
        {
            var ____arg0 = ver.__Instance;
            var __arg0 = new global::System.IntPtr(&____arg0);
            __Internal.MKL_GetVersion(__arg0);
        }

        public static void MKL_GetVersionString(sbyte* buffer, int len)
        {
            __Internal.MKL_GetVersionString(buffer, len);
        }

        public static void MKL_FreeBuffers()
        {
            __Internal.MKL_FreeBuffers();
        }

        public static void MKL_ThreadFreeBuffers()
        {
            __Internal.MKL_ThreadFreeBuffers();
        }

        public static long MKL_MemStat(ref int nbuffers)
        {
            fixed (int* __refParamPtr0 = &nbuffers)
            {
                var __arg0 = __refParamPtr0;
                var __ret = __Internal.MKL_MemStat(__arg0);
                return __ret;
            }
        }

        public static long MKL_PeakMemUsage(int reset)
        {
            var __ret = __Internal.MKL_PeakMemUsage(reset);
            return __ret;
        }

        public static global::System.IntPtr MKL_malloc(ulong size, int align)
        {
            var __ret = __Internal.MKL_malloc(size, align);
            return __ret;
        }

        public static global::System.IntPtr MKL_calloc(ulong num, ulong size, int align)
        {
            var __ret = __Internal.MKL_calloc(num, size, align);
            return __ret;
        }

        public static global::System.IntPtr MKL_realloc(global::System.IntPtr ptr, ulong size)
        {
            var __ret = __Internal.MKL_realloc(ptr, size);
            return __ret;
        }

        public static void MKL_free(global::System.IntPtr ptr)
        {
            __Internal.MKL_free(ptr);
        }

        public static int MKL_DisableFastMM()
        {
            var __ret = __Internal.MKL_DisableFastMM();
            return __ret;
        }

        public static void MKL_GetCpuClocks(ref ulong _0)
        {
            fixed (ulong* __refParamPtr0 = &_0)
            {
                var __arg0 = __refParamPtr0;
                __Internal.MKL_GetCpuClocks(__arg0);
            }
        }

        public static double MKL_GetCpuFrequency()
        {
            var __ret = __Internal.MKL_GetCpuFrequency();
            return __ret;
        }

        public static double MKL_GetMaxCpuFrequency()
        {
            var __ret = __Internal.MKL_GetMaxCpuFrequency();
            return __ret;
        }

        public static double MKL_GetClocksFrequency()
        {
            var __ret = __Internal.MKL_GetClocksFrequency();
            return __ret;
        }

        public static int MKL_SetNumThreadsLocal(int nth)
        {
            var __ret = __Internal.MKL_SetNumThreadsLocal(nth);
            return __ret;
        }

        public static void MKL_SetNumThreads(int nth)
        {
            __Internal.MKL_SetNumThreads(nth);
        }

        public static int MKL_GetMaxThreads()
        {
            var __ret = __Internal.MKL_GetMaxThreads();
            return __ret;
        }

        public static void MKL_SetNumStripes(int nstripes)
        {
            __Internal.MKL_SetNumStripes(nstripes);
        }

        public static int MKL_GetNumStripes()
        {
            var __ret = __Internal.MKL_GetNumStripes();
            return __ret;
        }

        public static int MKL_DomainSetNumThreads(int nth, int MKL_DOMAIN)
        {
            var __ret = __Internal.MKL_DomainSetNumThreads(nth, MKL_DOMAIN);
            return __ret;
        }

        public static int MKL_DomainGetMaxThreads(int MKL_DOMAIN)
        {
            var __ret = __Internal.MKL_DomainGetMaxThreads(MKL_DOMAIN);
            return __ret;
        }

        public static void MKL_SetDynamic(int bool_MKL_DYNAMIC)
        {
            __Internal.MKL_SetDynamic(bool_MKL_DYNAMIC);
        }

        public static int MKL_GetDynamic()
        {
            var __ret = __Internal.MKL_GetDynamic();
            return __ret;
        }

        public static int MKL_PROGRESS(ref int thread, ref int step, sbyte* stage, int lstage)
        {
            fixed (int* __refParamPtr0 = &thread)
            {
                var __arg0 = __refParamPtr0;
                fixed (int* __refParamPtr1 = &step)
                {
                    var __arg1 = __refParamPtr1;
                    var __ret = __Internal.MKL_PROGRESS(__arg0, __arg1, stage, lstage);
                    return __ret;
                }
            }
        }

        public static int MKL_PROGRESS_(ref int thread, ref int step, sbyte* stage, int lstage)
        {
            fixed (int* __refParamPtr0 = &thread)
            {
                var __arg0 = __refParamPtr0;
                fixed (int* __refParamPtr1 = &step)
                {
                    var __arg1 = __refParamPtr1;
                    var __ret = __Internal.MKL_PROGRESS_(__arg0, __arg1, stage, lstage);
                    return __ret;
                }
            }
        }

        public static int MklProgress(ref int thread, ref int step, sbyte* stage, int lstage)
        {
            fixed (int* __refParamPtr0 = &thread)
            {
                var __arg0 = __refParamPtr0;
                fixed (int* __refParamPtr1 = &step)
                {
                    var __arg1 = __refParamPtr1;
                    var __ret = __Internal.MklProgress(__arg0, __arg1, stage, lstage);
                    return __ret;
                }
            }
        }

        public static int mkl_progress_(ref int thread, ref int step, sbyte* stage, int lstage)
        {
            fixed (int* __refParamPtr0 = &thread)
            {
                var __arg0 = __refParamPtr0;
                fixed (int* __refParamPtr1 = &step)
                {
                    var __arg1 = __refParamPtr1;
                    var __ret = __Internal.mkl_progress_(__arg0, __arg1, stage, lstage);
                    return __ret;
                }
            }
        }

        public static int MKL_EnableInstructions(int _0)
        {
            var __ret = __Internal.MKL_EnableInstructions(_0);
            return __ret;
        }

        public static int MKL_SetInterfaceLayer(int code)
        {
            var __ret = __Internal.MKL_SetInterfaceLayer(code);
            return __ret;
        }

        public static int MKL_SetThreadingLayer(int code)
        {
            var __ret = __Internal.MKL_SetThreadingLayer(code);
            return __ret;
        }

        public static global::IntelMKL.XerblaEntry MklSetXerbla(global::IntelMKL.XerblaEntry xerbla)
        {
            var __arg0 = xerbla == null ? global::System.IntPtr.Zero : Marshal.GetFunctionPointerForDelegate(xerbla);
            var __ret = __Internal.MklSetXerbla(__arg0);
            var __ptr0 = __ret;
            return __ptr0 == IntPtr.Zero ? null : (global::IntelMKL.XerblaEntry)Marshal.GetDelegateForFunctionPointer(__ptr0, typeof(global::IntelMKL.XerblaEntry));
        }

        public static global::IntelMKL.ProgressEntry MklSetProgress(global::IntelMKL.ProgressEntry progress)
        {
            var __arg0 = progress == null ? global::System.IntPtr.Zero : Marshal.GetFunctionPointerForDelegate(progress);
            var __ret = __Internal.MklSetProgress(__arg0);
            var __ptr0 = __ret;
            return __ptr0 == IntPtr.Zero ? null : (global::IntelMKL.ProgressEntry)Marshal.GetDelegateForFunctionPointer(__ptr0, typeof(global::IntelMKL.ProgressEntry));
        }

        public static int MKL_MIC_Enable()
        {
            var __ret = __Internal.MKL_MIC_Enable();
            return __ret;
        }

        public static int MKL_MIC_Disable()
        {
            var __ret = __Internal.MKL_MIC_Disable();
            return __ret;
        }

        public static int MKL_MIC_GetDeviceCount()
        {
            var __ret = __Internal.MKL_MIC_GetDeviceCount();
            return __ret;
        }

        public static int MKL_MIC_GetCpuinfo(global::IntelMKL.MKL_MIC_TARGET_TYPE target_type, int target_number, ref int ncores, ref int nthreads, ref double freq)
        {
            fixed (int* __refParamPtr2 = &ncores)
            {
                var __arg2 = __refParamPtr2;
                fixed (int* __refParamPtr3 = &nthreads)
                {
                    var __arg3 = __refParamPtr3;
                    fixed (double* __refParamPtr4 = &freq)
                    {
                        var __arg4 = __refParamPtr4;
                        var __ret = __Internal.MKL_MIC_GetCpuinfo(target_type, target_number, __arg2, __arg3, __arg4);
                        return __ret;
                    }
                }
            }
        }

        public static int MKL_MIC_GetMeminfo(global::IntelMKL.MKL_MIC_TARGET_TYPE target_type, int target_number, ref int totalmem, ref int freemem)
        {
            fixed (int* __refParamPtr2 = &totalmem)
            {
                var __arg2 = __refParamPtr2;
                fixed (int* __refParamPtr3 = &freemem)
                {
                    var __arg3 = __refParamPtr3;
                    var __ret = __Internal.MKL_MIC_GetMeminfo(target_type, target_number, __arg2, __arg3);
                    return __ret;
                }
            }
        }

        public static int MKL_MIC_SetWorkdivision(global::IntelMKL.MKL_MIC_TARGET_TYPE target_type, int target_number, double wd)
        {
            var __ret = __Internal.MKL_MIC_SetWorkdivision(target_type, target_number, wd);
            return __ret;
        }

        public static int MKL_MIC_GetWorkdivision(global::IntelMKL.MKL_MIC_TARGET_TYPE target_type, int target_number, ref double wd)
        {
            fixed (double* __refParamPtr2 = &wd)
            {
                var __arg2 = __refParamPtr2;
                var __ret = __Internal.MKL_MIC_GetWorkdivision(target_type, target_number, __arg2);
                return __ret;
            }
        }

        public static int MKL_MIC_SetMaxMemory(global::IntelMKL.MKL_MIC_TARGET_TYPE target_type, int target_number, ulong card_mem_mbytes)
        {
            var __ret = __Internal.MKL_MIC_SetMaxMemory(target_type, target_number, card_mem_mbytes);
            return __ret;
        }

        public static int MKL_MIC_FreeMemory(global::IntelMKL.MKL_MIC_TARGET_TYPE target_type, int target_number)
        {
            var __ret = __Internal.MKL_MIC_FreeMemory(target_type, target_number);
            return __ret;
        }

        public static int MKL_MIC_SetOffloadReport(int enabled)
        {
            var __ret = __Internal.MKL_MIC_SetOffloadReport(enabled);
            return __ret;
        }

        public static int MKL_MIC_SetDeviceNumThreads(global::IntelMKL.MKL_MIC_TARGET_TYPE target_type, int target_number, int num_threads)
        {
            var __ret = __Internal.MKL_MIC_SetDeviceNumThreads(target_type, target_number, num_threads);
            return __ret;
        }

        public static int MKL_MIC_SetResourceLimit(double fraction)
        {
            var __ret = __Internal.MKL_MIC_SetResourceLimit(fraction);
            return __ret;
        }

        public static int MKL_MIC_GetResourceLimit(ref double fraction)
        {
            fixed (double* __refParamPtr0 = &fraction)
            {
                var __arg0 = __refParamPtr0;
                var __ret = __Internal.MKL_MIC_GetResourceLimit(__arg0);
                return __ret;
            }
        }

        public static int MKL_MIC_GetFlags()
        {
            var __ret = __Internal.MKL_MIC_GetFlags();
            return __ret;
        }

        public static int MKL_MIC_SetFlags(int flag)
        {
            var __ret = __Internal.MKL_MIC_SetFlags(flag);
            return __ret;
        }

        public static int MKL_MIC_GetStatus()
        {
            var __ret = __Internal.MKL_MIC_GetStatus();
            return __ret;
        }

        public static void MKL_MIC_ClearStatus()
        {
            __Internal.MKL_MIC_ClearStatus();
        }

        public static int MKL_CBWR_Get(int _0)
        {
            var __ret = __Internal.MKL_CBWR_Get(_0);
            return __ret;
        }

        public static int MKL_CBWR_Set(int _0)
        {
            var __ret = __Internal.MKL_CBWR_Set(_0);
            return __ret;
        }

        public static int MKL_CBWR_GetAutoBranch()
        {
            var __ret = __Internal.MKL_CBWR_GetAutoBranch();
            return __ret;
        }

        public static int MKL_SetEnvMode(int _0)
        {
            var __ret = __Internal.MKL_SetEnvMode(_0);
            return __ret;
        }

        public static int MKL_Verbose(int _0)
        {
            var __ret = __Internal.MKL_Verbose(_0);
            return __ret;
        }

        public static void MKL_SetExitHandler(global::IntelMKL.MKLExitHandler h)
        {
            var __arg0 = h == null ? global::System.IntPtr.Zero : Marshal.GetFunctionPointerForDelegate(h);
            __Internal.MKL_SetExitHandler(__arg0);
        }

        public static void MKL_MIC_registerMemory(int enable)
        {
            __Internal.MKL_MIC_registerMemory(enable);
        }

        public static int MKL_SetMemoryLimit(int mem_type, ulong limit)
        {
            var __ret = __Internal.MKL_SetMemoryLimit(mem_type, limit);
            return __ret;
        }

        public static int MKL_SetMpi(int vendor, string custom_library_name)
        {
            var __ret = __Internal.MKL_SetMpi(vendor, custom_library_name);
            return __ret;
        }

        public static void MKL_Finalize()
        {
            __Internal.MKL_Finalize();
        }
    }

  
    public unsafe partial struct MKL_Complex8
    {
        [StructLayout(LayoutKind.Explicit, Size = 8)]
        public partial struct __Internal
        {
            [FieldOffset(0)]
            internal float real;

            [FieldOffset(4)]
            internal float imag;

            [SuppressUnmanagedCodeSecurity]
            [DllImport("mkl_rt", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl,
                EntryPoint = "??0_MKL_Complex8@@QEAA@AEBU0@@Z")]
            internal static extern global::System.IntPtr cctor(global::System.IntPtr instance, global::System.IntPtr _0);
        }

        private MKL_Complex8.__Internal __instance;
        internal MKL_Complex8.__Internal __Instance { get { return __instance; } }

        internal static global::IntelMKL.MKL_Complex8 __CreateInstance(global::System.IntPtr native, bool skipVTables = false)
        {
            return new global::IntelMKL.MKL_Complex8(native.ToPointer(), skipVTables);
        }

        internal static global::IntelMKL.MKL_Complex8 __CreateInstance(global::IntelMKL.MKL_Complex8.__Internal native, bool skipVTables = false)
        {
            return new global::IntelMKL.MKL_Complex8(native, skipVTables);
        }

        private MKL_Complex8(global::IntelMKL.MKL_Complex8.__Internal native, bool skipVTables = false)
            : this()
        {
            __instance = native;
        }

        private MKL_Complex8(void* native, bool skipVTables = false) : this()
        {
            __instance = *(global::IntelMKL.MKL_Complex8.__Internal*)native;
        }

        public MKL_Complex8(global::IntelMKL.MKL_Complex8 _0)
            : this()
        {
            var ____arg0 = _0.__Instance;
            var __arg0 = new global::System.IntPtr(&____arg0);
            fixed (__Internal* __instancePtr = &__instance)
            {
                __Internal.cctor(new global::System.IntPtr(__instancePtr), __arg0);
            }
        }

        public float Real
        {
            get
            {
                return __instance.real;
            }

            set
            {
                __instance.real = value;
            }
        }

        public float Imag
        {
            get
            {
                return __instance.imag;
            }

            set
            {
                __instance.imag = value;
            }
        }
    }

    public unsafe partial struct MKL_Complex16
    {
        [StructLayout(LayoutKind.Explicit, Size = 16)]
        public partial struct __Internal
        {
            [FieldOffset(0)]
            internal double real;

            [FieldOffset(8)]
            internal double imag;

            [SuppressUnmanagedCodeSecurity]
            [DllImport("mkl_rt", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl,
                EntryPoint = "??0_MKL_Complex16@@QEAA@AEBU0@@Z")]
            internal static extern global::System.IntPtr cctor(global::System.IntPtr instance, global::System.IntPtr _0);
        }

        private MKL_Complex16.__Internal __instance;
        internal MKL_Complex16.__Internal __Instance { get { return __instance; } }

        internal static global::IntelMKL.MKL_Complex16 __CreateInstance(global::System.IntPtr native, bool skipVTables = false)
        {
            return new global::IntelMKL.MKL_Complex16(native.ToPointer(), skipVTables);
        }

        internal static global::IntelMKL.MKL_Complex16 __CreateInstance(global::IntelMKL.MKL_Complex16.__Internal native, bool skipVTables = false)
        {
            return new global::IntelMKL.MKL_Complex16(native, skipVTables);
        }

        private MKL_Complex16(global::IntelMKL.MKL_Complex16.__Internal native, bool skipVTables = false)
            : this()
        {
            __instance = native;
        }

        private MKL_Complex16(void* native, bool skipVTables = false) : this()
        {
            __instance = *(global::IntelMKL.MKL_Complex16.__Internal*)native;
        }

        public MKL_Complex16(global::IntelMKL.MKL_Complex16 _0)
            : this()
        {
            var ____arg0 = _0.__Instance;
            var __arg0 = new global::System.IntPtr(&____arg0);
            fixed (__Internal* __instancePtr = &__instance)
            {
                __Internal.cctor(new global::System.IntPtr(__instancePtr), __arg0);
            }
        }

        public double Real
        {
            get
            {
                return __instance.real;
            }

            set
            {
                __instance.real = value;
            }
        }

        public double Imag
        {
            get
            {
                return __instance.imag;
            }

            set
            {
                __instance.imag = value;
            }
        }
    }

    public unsafe partial struct MKLVersion
    {
        [StructLayout(LayoutKind.Explicit, Size = 48)]
        public partial struct __Internal
        {
            [FieldOffset(0)]
            internal int MajorVersion;

            [FieldOffset(4)]
            internal int MinorVersion;

            [FieldOffset(8)]
            internal int UpdateVersion;

            [FieldOffset(16)]
            internal global::System.IntPtr ProductStatus;

            [FieldOffset(24)]
            internal global::System.IntPtr Build;

            [FieldOffset(32)]
            internal global::System.IntPtr Processor;

            [FieldOffset(40)]
            internal global::System.IntPtr Platform;

            [SuppressUnmanagedCodeSecurity]
            [DllImport("mkl_rt", CallingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl,
                EntryPoint = "??0MKLVersion@@QEAA@AEBU0@@Z")]
            internal static extern global::System.IntPtr cctor(global::System.IntPtr instance, global::System.IntPtr _0);
        }

        private MKLVersion.__Internal __instance;
        internal MKLVersion.__Internal __Instance { get { return __instance; } }

        internal static global::IntelMKL.MKLVersion __CreateInstance(global::System.IntPtr native, bool skipVTables = false)
        {
            return new global::IntelMKL.MKLVersion(native.ToPointer(), skipVTables);
        }

        internal static global::IntelMKL.MKLVersion __CreateInstance(global::IntelMKL.MKLVersion.__Internal native, bool skipVTables = false)
        {
            return new global::IntelMKL.MKLVersion(native, skipVTables);
        }

        private MKLVersion(global::IntelMKL.MKLVersion.__Internal native, bool skipVTables = false)
            : this()
        {
            __instance = native;
        }

        private MKLVersion(void* native, bool skipVTables = false) : this()
        {
            __instance = *(global::IntelMKL.MKLVersion.__Internal*)native;
        }

        public MKLVersion(global::IntelMKL.MKLVersion _0)
            : this()
        {
            var ____arg0 = _0.__Instance;
            var __arg0 = new global::System.IntPtr(&____arg0);
            fixed (__Internal* __instancePtr = &__instance)
            {
                __Internal.cctor(new global::System.IntPtr(__instancePtr), __arg0);
            }
        }

        public int MajorVersion
        {
            get
            {
                return __instance.MajorVersion;
            }

            set
            {
                __instance.MajorVersion = value;
            }
        }

        public int MinorVersion
        {
            get
            {
                return __instance.MinorVersion;
            }

            set
            {
                __instance.MinorVersion = value;
            }
        }

        public int UpdateVersion
        {
            get
            {
                return __instance.UpdateVersion;
            }

            set
            {
                __instance.UpdateVersion = value;
            }
        }

        public sbyte* ProductStatus
        {
            get
            {
                return (sbyte*)__instance.ProductStatus;
            }

            set
            {
                __instance.ProductStatus = (global::System.IntPtr)value;
            }
        }

        public sbyte* Build
        {
            get
            {
                return (sbyte*)__instance.Build;
            }

            set
            {
                __instance.Build = (global::System.IntPtr)value;
            }
        }

        public sbyte* Processor
        {
            get
            {
                return (sbyte*)__instance.Processor;
            }

            set
            {
                __instance.Processor = (global::System.IntPtr)value;
            }
        }

        public sbyte* Platform
        {
            get
            {
                return (sbyte*)__instance.Platform;
            }

            set
            {
                __instance.Platform = (global::System.IntPtr)value;
            }
        }
    }

}
