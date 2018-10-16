using System;
using System.Collections.Generic;
using System.Text;

namespace Xamarin.Yoga.Tests.Utils
{
    

    public static class TestHelper
    {
        public static bool AreEqual(List<YGNode> left, List<YGNode> right)
        {
            if (ReferenceEquals(left, right))
                return true;
            if (left.Count != right.Count)
                return false;
            for (int i = 0; i < left.Count; i++)
            {
                if (left[i] != right[i])
                    return false;
            }

            return true;
        }

    }
}
