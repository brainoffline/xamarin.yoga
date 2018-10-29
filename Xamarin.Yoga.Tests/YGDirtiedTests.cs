﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Xamarin.Yoga.Tests
{
    
    using static YogaConst;
    
    
    

    [TestClass]
    public class YGDirtiedTests
    {
        [TestMethod]
        public void dirtied()
        {
            YogaNode root = new YogaNode();
            root.Style.AlignItems = AlignType.FlexStart;
            root.Style.Width = 100;
            root.Style.Height = 100;

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

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
            YogaNode root = new YogaNode();
            root.Style.AlignItems = AlignType.FlexStart;
            root.Style.Width = 100;
            root.Style.Height = 100;

            YogaNode root_child0 = new YogaNode();
            root_child0.Style.Width = 50;
            root_child0.Style.Height = 20;
            root.Children.Add(root_child0);

            YogaNode root_child1 = new YogaNode();
            root_child1.Style.Width = 50;
            root_child1.Style.Height = 20;
            root.Children.Insert(1, root_child1);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

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
            YogaNode root = new YogaNode();
            root.Style.AlignItems = AlignType.FlexStart;
            root.Style.Width = 100;
            root.Style.Height = 100;

            YogaNode root_child0 = new YogaNode();
            root_child0.Style.Width = 50;
            root_child0.Style.Height = 20;
            root.Children.Add(root_child0);

            YogaNode root_child1 = new YogaNode();
            root_child1.Style.Width = 50;
            root_child1.Style.Height = 20;
            root.Children.Insert(1, root_child1);

            root.Calc.CalculateLayout(float.NaN, float.NaN, DirectionType.LTR);

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
