﻿using System;
using System.Collections.Generic;
using System.Text;

using Xunit;

using IntelMKL.ILP64;

namespace Compute.Tests.MKL
{
    public class BlasTests
    {
        
        public void CanAdd()
        {
            float[] a = new float[3] { 1f, 2f, 3f };
            float[] b = new float[3] { 4f, 5f, 6f };
            float[] r = new float[3];
            
            Vml.VsAdd(2, ref a[0], ref b[0], ref r[0]);
            Assert.Equal(5, r[0]);
            
        }
    }
}
