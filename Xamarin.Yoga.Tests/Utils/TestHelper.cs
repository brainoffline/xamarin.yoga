using System;
using System.Collections.Generic;
using System.Text;

namespace Xamarin.Yoga.Tests.Utils
{
    using static YGGlobal;
    using YGConfigRef = YGConfig;
    using YGNodeRef = YGNode;
    using YGVector = List<YGNode>;

    public static class TestHelper
    {
        public static bool AreEqual(YGVector left, YGVector right)
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
