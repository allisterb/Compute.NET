using System;
using System.Collections.Generic;
using System.Text;

using Xunit;

using IntelMKL.LP64;

namespace Compute.Tests.MKL
{
    public class BlasTests
    {
        public const int GENERAL_MATRIX = 0;
        public const int UPPER_MATRIX = 1;
        public const int LOWER_MATRIX = -1;

        [Fact]
        public void CanRunBlasExample1()
        {
            int m = 3, n = 2, i, j;
            int lda = 3, ldb = 3, ldc = 3;
            int rmaxa, cmaxa, rmaxb, cmaxb, rmaxc, cmaxc;
            float alpha = 0.5f, beta = 2.0f;
            float[] a, b, c;
            CBLAS_LAYOUT layout = CBLAS_LAYOUT.CblasRowMajor;
            CBLAS_SIDE side = CBLAS_SIDE.CblasLeft;
            CBLAS_UPLO uplo = CBLAS_UPLO.CblasUpper;
            int ma, na, typeA;
            if (side == CBLAS_SIDE.CblasLeft)
            {
                rmaxa = m + 1;
                cmaxa = m;
                ma = m;
                na = m;
            }
            else
            {
                rmaxa = n + 1;
                cmaxa = n;
                ma = n;
                na = n;
            }
            rmaxb = m + 1;
            cmaxb = n;
            rmaxc = m + 1;
            cmaxc = n;
            a = new float[rmaxa * cmaxa];
            b = new float[rmaxb * cmaxb];
            c = new float[rmaxc * cmaxc];
            if (layout == CBLAS_LAYOUT.CblasRowMajor)
            {
                lda = cmaxa;
                ldb = cmaxb;
                ldc = cmaxc;
            }
            else
            {
                lda = rmaxa;
                ldb = rmaxb;
                ldc = rmaxc;
            }
            if (uplo == CBLAS_UPLO.CblasUpper)
                typeA = UPPER_MATRIX;
            else
                typeA = LOWER_MATRIX;
            for (i = 0; i < m; i++)
            {
                for (j = 0; j < m; j++)
                {
                    a[i + j * lda] = 1.0f;
                }
            }
            for (i = 0; i < m; i++)
            {
                for (j = 0; j < n; j++)
                {
                    c[i + j * ldc] = 1.0f;
                    b[i + j * ldb] = 2.0f;
                }
            } 
            CBlas.Ssymm(layout, side, uplo, m, n, alpha, ref a[0], lda, ref b[0], ldb, beta, ref c[0], ldc);
            
        }
    }
}
