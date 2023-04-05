using System;
using MechJebLib.Primitives;
using Xunit;

namespace MechJebLibTest.Structs
{
    public class V3Tests
    {

        [Fact]
        private void ComponentTests()
        {
            Assert.Equal(1.0, new V3(1, 2, 3).min_magnitude);
            Assert.Equal(1.0, new V3(2, 1, 3).min_magnitude);
            Assert.Equal(1.0, new V3(3, 2, 1).min_magnitude);
            Assert.Equal(1.0, new V3(2, 3, 1).min_magnitude);
            Assert.Equal(1.0, new V3(1, 3, 2).min_magnitude);
            Assert.Equal(1.0, new V3(3, 1, 2).min_magnitude);
            Assert.Equal(1.0, new V3(-1, -2, -3).min_magnitude);
            Assert.Equal(1.0, new V3(-2, -1, -3).min_magnitude);
            Assert.Equal(1.0, new V3(-3, -2, -1).min_magnitude);
            Assert.Equal(1.0, new V3(-2, -3, -1).min_magnitude);
            Assert.Equal(1.0, new V3(-1, -3, -2).min_magnitude);
            Assert.Equal(1.0, new V3(-3, -1, -2).min_magnitude);
            Assert.Equal(3.0, new V3(1, 2, 3).max_magnitude);
            Assert.Equal(3.0, new V3(2, 1, 3).max_magnitude);
            Assert.Equal(3.0, new V3(3, 2, 1).max_magnitude);
            Assert.Equal(3.0, new V3(2, 3, 1).max_magnitude);
            Assert.Equal(3.0, new V3(1, 3, 2).max_magnitude);
            Assert.Equal(3.0, new V3(3, 1, 2).max_magnitude);
            Assert.Equal(3.0, new V3(-1, -2, -3).max_magnitude);
            Assert.Equal(3.0, new V3(-2, -1, -3).max_magnitude);
            Assert.Equal(3.0, new V3(-3, -2, -1).max_magnitude);
            Assert.Equal(3.0, new V3(-2, -3, -1).max_magnitude);
            Assert.Equal(3.0, new V3(-1, -3, -2).max_magnitude);
            Assert.Equal(3.0, new V3(-3, -1, -2).max_magnitude);
            Assert.Equal(0, new V3(1, 2, 3).min_magnitude_index);
            Assert.Equal(1, new V3(2, 1, 3).min_magnitude_index);
            Assert.Equal(2, new V3(3, 2, 1).min_magnitude_index);
            Assert.Equal(2, new V3(2, 3, 1).min_magnitude_index);
            Assert.Equal(0, new V3(1, 3, 2).min_magnitude_index);
            Assert.Equal(1, new V3(3, 1, 2).min_magnitude_index);
            Assert.Equal(0, new V3(-1, -2, -3).min_magnitude_index);
            Assert.Equal(1, new V3(-2, -1, -3).min_magnitude_index);
            Assert.Equal(2, new V3(-3, -2, -1).min_magnitude_index);
            Assert.Equal(2, new V3(-2, -3, -1).min_magnitude_index);
            Assert.Equal(0, new V3(-1, -3, -2).min_magnitude_index);
            Assert.Equal(1, new V3(-3, -1, -2).min_magnitude_index);
            Assert.Equal(2, new V3(1, 2, 3).max_magnitude_index);
            Assert.Equal(2, new V3(2, 1, 3).max_magnitude_index);
            Assert.Equal(0, new V3(3, 2, 1).max_magnitude_index);
            Assert.Equal(1, new V3(2, 3, 1).max_magnitude_index);
            Assert.Equal(1, new V3(1, 3, 2).max_magnitude_index);
            Assert.Equal(0, new V3(3, 1, 2).max_magnitude_index);
            Assert.Equal(2, new V3(-1, -2, -3).max_magnitude_index);
            Assert.Equal(2, new V3(-2, -1, -3).max_magnitude_index);
            Assert.Equal(0, new V3(-3, -2, -1).max_magnitude_index);
            Assert.Equal(1, new V3(-2, -3, -1).max_magnitude_index);
            Assert.Equal(1, new V3(-1, -3, -2).max_magnitude_index);
            Assert.Equal(0, new V3(-3, -1, -2).max_magnitude_index);
        }

        [Fact]
        private void MagnitudeLargeTest()
        {
            double large = 1e+190;

            var largeV = new V3(large, large, large);
            Assert.Equal(Math.Sqrt(3) * large, largeV.magnitude);
        }

        [Fact]
        private void MagnitudeSmallTest()
        {
            double small = 1e-190;
            var smallV = new V3(small, small, small);

            Assert.Equal(Math.Sqrt(3) * small, smallV.magnitude);
        }

        [Fact]
        private void NormalizeLargeTest()
        {
            double large = 1e+190;
            var largeV = new V3(large, large, large);

            Assert.Equal(1.0 / Math.Sqrt(3), largeV.normalized[0]);
            Assert.Equal(1.0 / Math.Sqrt(3), largeV.normalized[1]);
            Assert.Equal(1.0 / Math.Sqrt(3), largeV.normalized[2]);
        }

        [Fact]
        private void NormalizeSmallTest()
        {
            double small = 1e-190;
            var smallV = new V3(small, small, small);

            Assert.Equal(1.0 / Math.Sqrt(3), smallV.normalized[0]);
            Assert.Equal(1.0 / Math.Sqrt(3), smallV.normalized[1]);
            Assert.Equal(1.0 / Math.Sqrt(3), smallV.normalized[2]);
        }

        [Fact]
        private void ZeroTests()
        {
            Assert.Equal(0, V3.zero.magnitude);
            Assert.Equal(0, V3.zero.sqrMagnitude);
            Assert.Equal(V3.zero, V3.zero.normalized);
        }
    }
}
