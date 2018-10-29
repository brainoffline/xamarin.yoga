using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Xamarin.Yoga.Tests
{
    
    using static YogaConst;


    [TestClass]
    public class YGHadOverflowTests
    {
        YogaNode   root;
        YogaConfig config;

        [TestInitialize]
        public void YogaTest_HadOverflowTests_Init()
        {
            config = new YogaConfig();
            root   = new YogaNode(config);
            root.Style.Width = 200;
            root.Style.Height = 100;
            root.Style.FlexDirection = FlexDirectionType.Column;
            root.Style.FlexWrap = WrapType.NoWrap;
        }

        [TestCleanup]
        public void YogaTest_HadOverflowTests_Cleanup() { }


        [TestMethod]
        public void children_overflow_no_wrap_and_no_flex_children()
        {
            YogaNode child0 = new YogaNode(config);
            child0.Style.Width = 80;
            child0.Style.Height = 40;
            child0.Style.Margin.Top =    10;
            child0.Style.Margin.Bottom = 15;
            root.Children.Add(child0);
            YogaNode child1 = new YogaNode(config);
            child1.Style.Width = 80;
            child1.Style.Height = 40;
            child1.Style.Margin.Bottom = 5;
            root.Children.Insert(1, child1);

            root.Calc.CalculateLayout(200, 100, DirectionType.LTR);

            Assert.IsTrue(root.Layout.HadOverflow);
        }

        [TestMethod]
        public void spacing_overflow_no_wrap_and_no_flex_children()
        {
            YogaNode child0 = new YogaNode(config);
            child0.Style.Width = 80;
            child0.Style.Height = 40;
            child0.Style.Margin.Top =    10;
            child0.Style.Margin.Bottom = 10;
            root.Children.Add(child0);
            YogaNode child1 = new YogaNode(config);
            child1.Style.Width = 80;
            child1.Style.Height = 40;
            child1.Style.Margin.Bottom = 5;
            root.Children.Insert(1, child1);

            root.Calc.CalculateLayout(200, 100, DirectionType.LTR);

            Assert.IsTrue(root.Layout.HadOverflow);
        }

        [TestMethod]
        public void no_overflow_no_wrap_and_flex_children()
        {
            YogaNode child0 = new YogaNode(config);
            child0.Style.Width = 80;
            child0.Style.Height = 40;
            child0.Style.Margin.Top =    10;
            child0.Style.Margin.Bottom = 10;
            root.Children.Add(child0);
            YogaNode child1 = new YogaNode(config);
            child1.Style.Width = 80;
            child1.Style.Height = 40;
            child1.Style.Margin.Bottom = 5;
            child1.Style.FlexShrink = 1;
            root.Children.Insert(1, child1);

            root.Calc.CalculateLayout(200, 100, DirectionType.LTR);

            Assert.IsFalse(root.Layout.HadOverflow);
        }

        [TestMethod]
        public void hadOverflow_gets_reset_if_not_logger_valid()
        {
            YogaNode child0 = new YogaNode(config);
            child0.Style.Width = 80;
            child0.Style.Height = 40;
            child0.Style.Margin.Top =    10;
            child0.Style.Margin.Bottom = 10;
            root.Children.Add(child0);
            YogaNode child1 = new YogaNode(config);
            child1.Style.Width = 80;
            child1.Style.Height = 40;
            child1.Style.Margin.Bottom = 5;
            root.Children.Insert(1, child1);

            root.Calc.CalculateLayout(200, 100, DirectionType.LTR);

            Assert.IsTrue(root.Layout.HadOverflow);

            child1.Style.FlexShrink = 1;

            root.Calc.CalculateLayout(200, 100, DirectionType.LTR);

            Assert.IsFalse(root.Layout.HadOverflow);
        }

        [TestMethod]
        public void spacing_overflow_in_nested_nodes()
        {
            YogaNode child0 = new YogaNode(config);
            child0.Style.Width = 80;
            child0.Style.Height = 40;
            child0.Style.Margin.Top =    10;
            child0.Style.Margin.Bottom = 10;
            root.Children.Add(child0);
            YogaNode child1 = new YogaNode(config);
            child1.Style.Width = 80;
            child1.Style.Height = 40;
            root.Children.Insert(1, child1);
            YogaNode child1_1 = new YogaNode(config);
            child1_1.Style.Width = 80;
            child1_1.Style.Height = 40;
            child1_1.Style.Margin.Bottom = 5;
            child1.Children.Add(child1_1);

            root.Calc.CalculateLayout(200, 100, DirectionType.LTR);

            Assert.IsTrue(root.Layout.HadOverflow);
        }
    }
}
