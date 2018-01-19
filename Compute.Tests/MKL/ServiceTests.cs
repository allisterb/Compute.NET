using System;
using System.Collections.Generic;
using System.Text;

using Xunit;

using IntelMKL;

namespace Compute.Tests.MKl
{
    public class ServiceTests
    {
        [Fact(DisplayName = "Can call service functions")]
        public void CanCallService()
        {
            double d = Service.MKL_GetClocksFrequency();
            Assert.True(d > 2);
            MKLVersion v = new MKLVersion();
            Service.MKL_GetVersion(ref v);
            Assert.True(v.MajorVersion == 2018);
        }
    }
}
