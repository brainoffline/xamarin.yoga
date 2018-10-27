using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Xamarin.Yoga.Tests
{
    using static YGGlobal;
    using static YogaConst;
    
    
    

    [TestClass]
    public class YGRoundingFunctionTests
    {
        [TestMethod]
        public void rounding_value()
        {
            // Test that whole numbers are rounded to whole despite ceil/floor flags
            Assert.AreEqual(6.0, NumberExtensions.RoundValueToPixelGrid(6.000001f, 2.0f, false, false));
            Assert.AreEqual(6.0, NumberExtensions.RoundValueToPixelGrid(6.000001f, 2.0f, true,  false));
            Assert.AreEqual(6.0, NumberExtensions.RoundValueToPixelGrid(6.000001f, 2.0f, false, true));
            Assert.AreEqual(6.0, NumberExtensions.RoundValueToPixelGrid(5.999999f, 2.0f, false, false));
            Assert.AreEqual(6.0, NumberExtensions.RoundValueToPixelGrid(5.999999f, 2.0f, true,  false));
            Assert.AreEqual(6.0, NumberExtensions.RoundValueToPixelGrid(5.999999f, 2.0f, false, true));

            // Test that numbers with fraction are rounded correctly accounting for ceil/floor flags
            Assert.AreEqual(6.0, NumberExtensions.RoundValueToPixelGrid(6.01f, 2.0f, false, false));
            Assert.AreEqual(6.5, NumberExtensions.RoundValueToPixelGrid(6.01f, 2.0f, true,  false));
            Assert.AreEqual(6.0, NumberExtensions.RoundValueToPixelGrid(6.01f, 2.0f, false, true));
            Assert.AreEqual(6.0, NumberExtensions.RoundValueToPixelGrid(5.99f, 2.0f, false, false));
            Assert.AreEqual(6.0, NumberExtensions.RoundValueToPixelGrid(5.99f, 2.0f, true,  false));
            Assert.AreEqual(5.5, NumberExtensions.RoundValueToPixelGrid(5.99f, 2.0f, false, true));
        }

    }
}
