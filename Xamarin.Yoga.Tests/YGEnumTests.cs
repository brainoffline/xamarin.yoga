using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
// ReSharper disable InconsistentNaming

namespace Xamarin.Yoga.Tests
{
    
    using static YogaConst;
    
    
    

    [TestClass]
    public class YGEnumTests
    {

        [TestMethod]
        public void ConvertEnumToString()
        {
            var result = AlignType.Auto.ToDescription();

            Assert.AreEqual("auto", result);
        }
    }
}
