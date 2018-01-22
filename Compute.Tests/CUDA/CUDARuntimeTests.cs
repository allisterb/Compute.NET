using System;
using System.Collections.Generic;
using System.Text;

using Xunit;

using CUDA;

namespace Compute.Tests.CUDA
{
    public class CUDARuntimeTests
    {
        [Fact]
        public unsafe void Can()
        {
            CudaSharedMemConfig c;
            RuntimeApi.CudaDeviceGetSharedMemConfig(&c);
        }

    }
}
