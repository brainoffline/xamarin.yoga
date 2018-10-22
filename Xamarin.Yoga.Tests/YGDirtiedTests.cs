using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Xamarin.Yoga.Tests
{
    using static YGGlobal;
    using static YGConst;
    
    
    

    [TestClass]
    public class YGDirtiedTests
    {
        [TestMethod]
        public void dirtied()
        {
            YGNode root = new YGNode();
            root.Style.AlignItems = YGAlign.FlexStart;
            root.Style.Width = 100;
            root.Style.Height = 100;

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            int dirtiedCount = 0;
            root.Context = dirtiedCount ;
            root.DirtiedFunc = n => dirtiedCount++;

            Assert.AreEqual(0, dirtiedCount);

            // `_dirtied` MUST be called in case of explicit dirtying.
            root.IsDirty = true;
            Assert.AreEqual(1, dirtiedCount);

            // `_dirtied` MUST be called ONCE.
            root.IsDirty = true;
            Assert.AreEqual(1, dirtiedCount);
        }

        [TestMethod]
        public void dirtied_propagation()
        {
            YGNode root = new YGNode();
            root.Style.AlignItems = YGAlign.FlexStart;
            root.Style.Width = 100;
            root.Style.Height = 100;

            YGNode root_child0 = new YGNode();
            root_child0.Style.Width = 50;
            root_child0.Style.Height = 20;
            root.Children.Add(root_child0);

            YGNode root_child1 = new YGNode();
            root_child1.Style.Width = 50;
            root_child1.Style.Height = 20;
            root.Children.Insert(1, root_child1);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            int dirtiedCount = 0;
            root.Context = dirtiedCount;
            root.DirtiedFunc = n => { dirtiedCount++; };

            Assert.AreEqual(0, dirtiedCount);

            // `_dirtied` MUST be called for the first time.
            root_child0.MarkDirty();
            Assert.AreEqual(1, dirtiedCount);

            // `_dirtied` must NOT be called for the second time.
            root_child0.MarkDirty();
            Assert.AreEqual(1, dirtiedCount);
        }

        [TestMethod]
        public void dirtied_hierarchy()
        {
            YGNode root = new YGNode();
            root.Style.AlignItems = YGAlign.FlexStart;
            root.Style.Width = 100;
            root.Style.Height = 100;

            YGNode root_child0 = new YGNode();
            root_child0.Style.Width = 50;
            root_child0.Style.Height = 20;
            root.Children.Add(root_child0);

            YGNode root_child1 = new YGNode();
            root_child1.Style.Width = 50;
            root_child1.Style.Height = 20;
            root.Children.Insert(1, root_child1);

            YGNodeCalculateLayout(root, float.NaN, float.NaN, YGDirection.LTR);

            int dirtiedCount = 0;
            root_child0.Context = dirtiedCount;
            root_child0.DirtiedFunc = n => { dirtiedCount++; };

            Assert.AreEqual(0, dirtiedCount);

            // `_dirtied` must NOT be called for descendants.
            root.MarkDirty();
            Assert.AreEqual(0, dirtiedCount);

            // `_dirtied` must NOT be called for the sibling node.
            root_child1.MarkDirty();
            Assert.AreEqual(0, dirtiedCount);

            // `_dirtied` MUST be called in case of explicit dirtying.
            root_child0.MarkDirty();
            Assert.AreEqual(1, dirtiedCount);
        }
    }
}
