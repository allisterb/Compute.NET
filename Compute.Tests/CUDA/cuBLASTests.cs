using System;
using System.Collections.Generic;
using System.Text;

using Xunit;

using CUDA; 
namespace Compute.Tests
{
    public class cuBLASTests
    {
        public cuBLASTests()
        {
            cuBlas.Init();
        }

        [Fact(DisplayName = "Can get the version")]
        public void CanAdd()
        {
            int v = 0;
            cuBlas.GetVersion(ref v);
            Assert.True(v > 9000);
        }
    }
}
