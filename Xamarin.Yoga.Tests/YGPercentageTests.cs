using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Xamarin.Yoga.Tests
{
    using static YGGlobal;
    using static YGConst;
    using YGConfigRef = YGConfig;
    using YGNodeRef = YGNode;
    using YGVector = List<YGNode>;

    [TestClass]
    public class YGPercentageTests
    {
        [TestMethod]
        public void percentage_width_height()
        {
             YGConfigRef config = YGConfigNew();

             YGNodeRef root = YGNodeNewWithConfig(config);
            YGNodeStyleSetFlexDirection(root, YGFlexDirection.Row);
            YGNodeStyleSetWidth(root, 200f);
            YGNodeStyleSetHeight(root, 200f);

             YGNodeRef root_child0 = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidthPercent(root_child0, 30f);
            YGNodeStyleSetHeightPercent(root_child0, 30f);
            YGNodeInsertChild(root, root_child0, 0);
            YGNodeCalculateLayout(root, YGUndefined, YGUndefined, YGDirection.LTR);

            Assert.AreEqual(0f, YGNodeLayoutGetLeft(root));
            Assert.AreEqual(0f, YGNodeLayoutGetTop(root));
            Assert.AreEqual(200f, YGNodeLayoutGetWidth(root));
            Assert.AreEqual(200f, YGNodeLayoutGetHeight(root));

            Assert.AreEqual(0f, YGNodeLayoutGetLeft(root_child0));
            Assert.AreEqual(0f, YGNodeLayoutGetTop(root_child0));
            Assert.AreEqual(60f, YGNodeLayoutGetWidth(root_child0));
            Assert.AreEqual(60f, YGNodeLayoutGetHeight(root_child0));

            YGNodeCalculateLayout(root, YGUndefined, YGUndefined, YGDirection.RTL);

            Assert.AreEqual(0f, YGNodeLayoutGetLeft(root));
            Assert.AreEqual(0f, YGNodeLayoutGetTop(root));
            Assert.AreEqual(200f, YGNodeLayoutGetWidth(root));
            Assert.AreEqual(200f, YGNodeLayoutGetHeight(root));

            Assert.AreEqual(140f, YGNodeLayoutGetLeft(root_child0));
            Assert.AreEqual(0f, YGNodeLayoutGetTop(root_child0));
            Assert.AreEqual(60f, YGNodeLayoutGetWidth(root_child0));
            Assert.AreEqual(60f, YGNodeLayoutGetHeight(root_child0));

            YGNodeFreeRecursive(root);

            YGConfigFree(config);
        }

        [TestMethod]
        public void percentage_position_left_top()
        {
             YGConfigRef config = YGConfigNew();

             YGNodeRef root = YGNodeNewWithConfig(config);
            YGNodeStyleSetFlexDirection(root, YGFlexDirection.Row);
            YGNodeStyleSetWidth(root, 400f);
            YGNodeStyleSetHeight(root, 400f);

             YGNodeRef root_child0 = YGNodeNewWithConfig(config);
            YGNodeStyleSetPositionPercent(root_child0, YGEdge.Left, 10);
            YGNodeStyleSetPositionPercent(root_child0, YGEdge.Top, 20);
            YGNodeStyleSetWidthPercent(root_child0, 45f);
            YGNodeStyleSetHeightPercent(root_child0, 55f);
            YGNodeInsertChild(root, root_child0, 0);
            YGNodeCalculateLayout(root, YGUndefined, YGUndefined, YGDirection.LTR);

            Assert.AreEqual(0f, YGNodeLayoutGetLeft(root));
            Assert.AreEqual(0f, YGNodeLayoutGetTop(root));
            Assert.AreEqual(400f, YGNodeLayoutGetWidth(root));
            Assert.AreEqual(400f, YGNodeLayoutGetHeight(root));

            Assert.AreEqual(40f, YGNodeLayoutGetLeft(root_child0));
            Assert.AreEqual(80f, YGNodeLayoutGetTop(root_child0));
            Assert.AreEqual(180f, YGNodeLayoutGetWidth(root_child0));
            Assert.AreEqual(220f, YGNodeLayoutGetHeight(root_child0));

            YGNodeCalculateLayout(root, YGUndefined, YGUndefined, YGDirection.RTL);

            Assert.AreEqual(0f, YGNodeLayoutGetLeft(root));
            Assert.AreEqual(0f, YGNodeLayoutGetTop(root));
            Assert.AreEqual(400f, YGNodeLayoutGetWidth(root));
            Assert.AreEqual(400f, YGNodeLayoutGetHeight(root));

            Assert.AreEqual(260f, YGNodeLayoutGetLeft(root_child0));
            Assert.AreEqual(80f, YGNodeLayoutGetTop(root_child0));
            Assert.AreEqual(180f, YGNodeLayoutGetWidth(root_child0));
            Assert.AreEqual(220f, YGNodeLayoutGetHeight(root_child0));

            YGNodeFreeRecursive(root);

            YGConfigFree(config);
        }

        [TestMethod]
        public void percentage_position_bottom_right()
        {
             YGConfigRef config = YGConfigNew();

             YGNodeRef root = YGNodeNewWithConfig(config);
            YGNodeStyleSetFlexDirection(root, YGFlexDirection.Row);
            YGNodeStyleSetWidth(root, 500f);
            YGNodeStyleSetHeight(root, 500f);

             YGNodeRef root_child0 = YGNodeNewWithConfig(config);
            YGNodeStyleSetPositionPercent(root_child0, YGEdge.Right, 20f);
            YGNodeStyleSetPositionPercent(root_child0, YGEdge.Bottom, 10f);
            YGNodeStyleSetWidthPercent(root_child0, 55f);
            YGNodeStyleSetHeightPercent(root_child0, 15f);
            YGNodeInsertChild(root, root_child0, 0);
            YGNodeCalculateLayout(root, YGUndefined, YGUndefined, YGDirection.LTR);

            Assert.AreEqual(0f, YGNodeLayoutGetLeft(root));
            Assert.AreEqual(0f, YGNodeLayoutGetTop(root));
            Assert.AreEqual(500f, YGNodeLayoutGetWidth(root));
            Assert.AreEqual(500f, YGNodeLayoutGetHeight(root));

            Assert.AreEqual(-100f, YGNodeLayoutGetLeft(root_child0));
            Assert.AreEqual(-50f, YGNodeLayoutGetTop(root_child0));
            Assert.AreEqual(275f, YGNodeLayoutGetWidth(root_child0));
            Assert.AreEqual(75f, YGNodeLayoutGetHeight(root_child0));

            YGNodeCalculateLayout(root, YGUndefined, YGUndefined, YGDirection.RTL);

            Assert.AreEqual(0f, YGNodeLayoutGetLeft(root));
            Assert.AreEqual(0f, YGNodeLayoutGetTop(root));
            Assert.AreEqual(500f, YGNodeLayoutGetWidth(root));
            Assert.AreEqual(500f, YGNodeLayoutGetHeight(root));

            Assert.AreEqual(125f, YGNodeLayoutGetLeft(root_child0));
            Assert.AreEqual(-50f, YGNodeLayoutGetTop(root_child0));
            Assert.AreEqual(275f, YGNodeLayoutGetWidth(root_child0));
            Assert.AreEqual(75f, YGNodeLayoutGetHeight(root_child0));

            YGNodeFreeRecursive(root);

            YGConfigFree(config);
        }

        [TestMethod]
        public void percentage_flex_basis()
        {
             YGConfigRef config = YGConfigNew();

             YGNodeRef root = YGNodeNewWithConfig(config);
            YGNodeStyleSetFlexDirection(root, YGFlexDirection.Row);
            YGNodeStyleSetWidth(root, 200);
            YGNodeStyleSetHeight(root, 200);

             YGNodeRef root_child0 = YGNodeNewWithConfig(config);
            YGNodeStyleSetFlexGrow(root_child0, 1);
            YGNodeStyleSetFlexBasisPercent(root_child0, 50);
            YGNodeInsertChild(root, root_child0, 0);

             YGNodeRef root_child1 = YGNodeNewWithConfig(config);
            YGNodeStyleSetFlexGrow(root_child1, 1);
            YGNodeStyleSetFlexBasisPercent(root_child1, 25);
            YGNodeInsertChild(root, root_child1, 1);
            YGNodeCalculateLayout(root, YGUndefined, YGUndefined, YGDirection.LTR);

            Assert.AreEqual(0, YGNodeLayoutGetLeft(root));
            Assert.AreEqual(0, YGNodeLayoutGetTop(root));
            Assert.AreEqual(200, YGNodeLayoutGetWidth(root));
            Assert.AreEqual(200, YGNodeLayoutGetHeight(root));

            Assert.AreEqual(0, YGNodeLayoutGetLeft(root_child0));
            Assert.AreEqual(0, YGNodeLayoutGetTop(root_child0));
            Assert.AreEqual(125, YGNodeLayoutGetWidth(root_child0));
            Assert.AreEqual(200, YGNodeLayoutGetHeight(root_child0));

            Assert.AreEqual(125, YGNodeLayoutGetLeft(root_child1));
            Assert.AreEqual(0, YGNodeLayoutGetTop(root_child1));
            Assert.AreEqual(75, YGNodeLayoutGetWidth(root_child1));
            Assert.AreEqual(200, YGNodeLayoutGetHeight(root_child1));

            YGNodeCalculateLayout(root, YGUndefined, YGUndefined, YGDirection.RTL);

            Assert.AreEqual(0, YGNodeLayoutGetLeft(root));
            Assert.AreEqual(0, YGNodeLayoutGetTop(root));
            Assert.AreEqual(200, YGNodeLayoutGetWidth(root));
            Assert.AreEqual(200, YGNodeLayoutGetHeight(root));

            Assert.AreEqual(75, YGNodeLayoutGetLeft(root_child0));
            Assert.AreEqual(0, YGNodeLayoutGetTop(root_child0));
            Assert.AreEqual(125, YGNodeLayoutGetWidth(root_child0));
            Assert.AreEqual(200, YGNodeLayoutGetHeight(root_child0));

            Assert.AreEqual(0, YGNodeLayoutGetLeft(root_child1));
            Assert.AreEqual(0, YGNodeLayoutGetTop(root_child1));
            Assert.AreEqual(75, YGNodeLayoutGetWidth(root_child1));
            Assert.AreEqual(200, YGNodeLayoutGetHeight(root_child1));

            YGNodeFreeRecursive(root);

            YGConfigFree(config);
        }

        [TestMethod]
        public void percentage_flex_basis_cross()
        {
             YGConfigRef config = YGConfigNew();

             YGNodeRef root = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(root, 200);
            YGNodeStyleSetHeight(root, 200);

             YGNodeRef root_child0 = YGNodeNewWithConfig(config);
            YGNodeStyleSetFlexGrow(root_child0, 1);
            YGNodeStyleSetFlexBasisPercent(root_child0, 50);
            YGNodeInsertChild(root, root_child0, 0);

             YGNodeRef root_child1 = YGNodeNewWithConfig(config);
            YGNodeStyleSetFlexGrow(root_child1, 1);
            YGNodeStyleSetFlexBasisPercent(root_child1, 25);
            YGNodeInsertChild(root, root_child1, 1);
            YGNodeCalculateLayout(root, YGUndefined, YGUndefined, YGDirection.LTR);

            Assert.AreEqual(0, YGNodeLayoutGetLeft(root));
            Assert.AreEqual(0, YGNodeLayoutGetTop(root));
            Assert.AreEqual(200, YGNodeLayoutGetWidth(root));
            Assert.AreEqual(200, YGNodeLayoutGetHeight(root));

            Assert.AreEqual(0, YGNodeLayoutGetLeft(root_child0));
            Assert.AreEqual(0, YGNodeLayoutGetTop(root_child0));
            Assert.AreEqual(200, YGNodeLayoutGetWidth(root_child0));
            Assert.AreEqual(125, YGNodeLayoutGetHeight(root_child0));

            Assert.AreEqual(0, YGNodeLayoutGetLeft(root_child1));
            Assert.AreEqual(125, YGNodeLayoutGetTop(root_child1));
            Assert.AreEqual(200, YGNodeLayoutGetWidth(root_child1));
            Assert.AreEqual(75, YGNodeLayoutGetHeight(root_child1));

            YGNodeCalculateLayout(root, YGUndefined, YGUndefined, YGDirection.RTL);

            Assert.AreEqual(0, YGNodeLayoutGetLeft(root));
            Assert.AreEqual(0, YGNodeLayoutGetTop(root));
            Assert.AreEqual(200, YGNodeLayoutGetWidth(root));
            Assert.AreEqual(200, YGNodeLayoutGetHeight(root));

            Assert.AreEqual(0, YGNodeLayoutGetLeft(root_child0));
            Assert.AreEqual(0, YGNodeLayoutGetTop(root_child0));
            Assert.AreEqual(200, YGNodeLayoutGetWidth(root_child0));
            Assert.AreEqual(125, YGNodeLayoutGetHeight(root_child0));

            Assert.AreEqual(0, YGNodeLayoutGetLeft(root_child1));
            Assert.AreEqual(125, YGNodeLayoutGetTop(root_child1));
            Assert.AreEqual(200, YGNodeLayoutGetWidth(root_child1));
            Assert.AreEqual(75, YGNodeLayoutGetHeight(root_child1));

            YGNodeFreeRecursive(root);

            YGConfigFree(config);
        }

        [TestMethod]
        public void percentage_flex_basis_cross_min_height()
        {
             YGConfigRef config = YGConfigNew();

             YGNodeRef root = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(root, 200);
            YGNodeStyleSetHeight(root, 200);

             YGNodeRef root_child0 = YGNodeNewWithConfig(config);
            YGNodeStyleSetFlexGrow(root_child0, 1);
            YGNodeStyleSetMinHeightPercent(root_child0, 60);
            YGNodeInsertChild(root, root_child0, 0);

             YGNodeRef root_child1 = YGNodeNewWithConfig(config);
            YGNodeStyleSetFlexGrow(root_child1, 2);
            YGNodeStyleSetMinHeightPercent(root_child1, 10);
            YGNodeInsertChild(root, root_child1, 1);
            YGNodeCalculateLayout(root, YGUndefined, YGUndefined, YGDirection.LTR);

            Assert.AreEqual(0, YGNodeLayoutGetLeft(root));
            Assert.AreEqual(0, YGNodeLayoutGetTop(root));
            Assert.AreEqual(200, YGNodeLayoutGetWidth(root));
            Assert.AreEqual(200, YGNodeLayoutGetHeight(root));

            Assert.AreEqual(0, YGNodeLayoutGetLeft(root_child0));
            Assert.AreEqual(0, YGNodeLayoutGetTop(root_child0));
            Assert.AreEqual(200, YGNodeLayoutGetWidth(root_child0));
            Assert.AreEqual(140, YGNodeLayoutGetHeight(root_child0));

            Assert.AreEqual(0, YGNodeLayoutGetLeft(root_child1));
            Assert.AreEqual(140, YGNodeLayoutGetTop(root_child1));
            Assert.AreEqual(200, YGNodeLayoutGetWidth(root_child1));
            Assert.AreEqual(60, YGNodeLayoutGetHeight(root_child1));

            YGNodeCalculateLayout(root, YGUndefined, YGUndefined, YGDirection.RTL);

            Assert.AreEqual(0, YGNodeLayoutGetLeft(root));
            Assert.AreEqual(0, YGNodeLayoutGetTop(root));
            Assert.AreEqual(200, YGNodeLayoutGetWidth(root));
            Assert.AreEqual(200, YGNodeLayoutGetHeight(root));

            Assert.AreEqual(0, YGNodeLayoutGetLeft(root_child0));
            Assert.AreEqual(0, YGNodeLayoutGetTop(root_child0));
            Assert.AreEqual(200, YGNodeLayoutGetWidth(root_child0));
            Assert.AreEqual(140, YGNodeLayoutGetHeight(root_child0));

            Assert.AreEqual(0, YGNodeLayoutGetLeft(root_child1));
            Assert.AreEqual(140, YGNodeLayoutGetTop(root_child1));
            Assert.AreEqual(200, YGNodeLayoutGetWidth(root_child1));
            Assert.AreEqual(60, YGNodeLayoutGetHeight(root_child1));

            YGNodeFreeRecursive(root);

            YGConfigFree(config);
        }

        [TestMethod]
        public void percentage_flex_basis_main_max_height()
        {
             YGConfigRef config = YGConfigNew();

             YGNodeRef root = YGNodeNewWithConfig(config);
            YGNodeStyleSetFlexDirection(root, YGFlexDirection.Row);
            YGNodeStyleSetWidth(root, 200);
            YGNodeStyleSetHeight(root, 200);

             YGNodeRef root_child0 = YGNodeNewWithConfig(config);
            YGNodeStyleSetFlexGrow(root_child0, 1);
            YGNodeStyleSetFlexBasisPercent(root_child0, 10);
            YGNodeStyleSetMaxHeightPercent(root_child0, 60);
            YGNodeInsertChild(root, root_child0, 0);

             YGNodeRef root_child1 = YGNodeNewWithConfig(config);
            YGNodeStyleSetFlexGrow(root_child1, 4);
            YGNodeStyleSetFlexBasisPercent(root_child1, 10);
            YGNodeStyleSetMaxHeightPercent(root_child1, 20);
            YGNodeInsertChild(root, root_child1, 1);
            YGNodeCalculateLayout(root, YGUndefined, YGUndefined, YGDirection.LTR);

            Assert.AreEqual(0, YGNodeLayoutGetLeft(root));
            Assert.AreEqual(0, YGNodeLayoutGetTop(root));
            Assert.AreEqual(200, YGNodeLayoutGetWidth(root));
            Assert.AreEqual(200, YGNodeLayoutGetHeight(root));

            Assert.AreEqual(0, YGNodeLayoutGetLeft(root_child0));
            Assert.AreEqual(0, YGNodeLayoutGetTop(root_child0));
            Assert.AreEqual(52, YGNodeLayoutGetWidth(root_child0));
            Assert.AreEqual(120, YGNodeLayoutGetHeight(root_child0));

            Assert.AreEqual(52, YGNodeLayoutGetLeft(root_child1));
            Assert.AreEqual(0, YGNodeLayoutGetTop(root_child1));
            Assert.AreEqual(148, YGNodeLayoutGetWidth(root_child1));
            Assert.AreEqual(40, YGNodeLayoutGetHeight(root_child1));

            YGNodeCalculateLayout(root, YGUndefined, YGUndefined, YGDirection.RTL);

            Assert.AreEqual(0, YGNodeLayoutGetLeft(root));
            Assert.AreEqual(0, YGNodeLayoutGetTop(root));
            Assert.AreEqual(200, YGNodeLayoutGetWidth(root));
            Assert.AreEqual(200, YGNodeLayoutGetHeight(root));

            Assert.AreEqual(148, YGNodeLayoutGetLeft(root_child0));
            Assert.AreEqual(0, YGNodeLayoutGetTop(root_child0));
            Assert.AreEqual(52, YGNodeLayoutGetWidth(root_child0));
            Assert.AreEqual(120, YGNodeLayoutGetHeight(root_child0));

            Assert.AreEqual(0, YGNodeLayoutGetLeft(root_child1));
            Assert.AreEqual(0, YGNodeLayoutGetTop(root_child1));
            Assert.AreEqual(148, YGNodeLayoutGetWidth(root_child1));
            Assert.AreEqual(40, YGNodeLayoutGetHeight(root_child1));

            YGNodeFreeRecursive(root);

            YGConfigFree(config);
        }

        [TestMethod]
        public void percentage_flex_basis_cross_max_height()
        {
             YGConfigRef config = YGConfigNew();

             YGNodeRef root = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(root, 200);
            YGNodeStyleSetHeight(root, 200);

             YGNodeRef root_child0 = YGNodeNewWithConfig(config);
            YGNodeStyleSetFlexGrow(root_child0, 1);
            YGNodeStyleSetFlexBasisPercent(root_child0, 10);
            YGNodeStyleSetMaxHeightPercent(root_child0, 60);
            YGNodeInsertChild(root, root_child0, 0);

             YGNodeRef root_child1 = YGNodeNewWithConfig(config);
            YGNodeStyleSetFlexGrow(root_child1, 4);
            YGNodeStyleSetFlexBasisPercent(root_child1, 10);
            YGNodeStyleSetMaxHeightPercent(root_child1, 20);
            YGNodeInsertChild(root, root_child1, 1);
            YGNodeCalculateLayout(root, YGUndefined, YGUndefined, YGDirection.LTR);

            Assert.AreEqual(0, YGNodeLayoutGetLeft(root));
            Assert.AreEqual(0, YGNodeLayoutGetTop(root));
            Assert.AreEqual(200, YGNodeLayoutGetWidth(root));
            Assert.AreEqual(200, YGNodeLayoutGetHeight(root));

            Assert.AreEqual(0, YGNodeLayoutGetLeft(root_child0));
            Assert.AreEqual(0, YGNodeLayoutGetTop(root_child0));
            Assert.AreEqual(200, YGNodeLayoutGetWidth(root_child0));
            Assert.AreEqual(120, YGNodeLayoutGetHeight(root_child0));

            Assert.AreEqual(0, YGNodeLayoutGetLeft(root_child1));
            Assert.AreEqual(120, YGNodeLayoutGetTop(root_child1));
            Assert.AreEqual(200, YGNodeLayoutGetWidth(root_child1));
            Assert.AreEqual(40, YGNodeLayoutGetHeight(root_child1));

            YGNodeCalculateLayout(root, YGUndefined, YGUndefined, YGDirection.RTL);

            Assert.AreEqual(0, YGNodeLayoutGetLeft(root));
            Assert.AreEqual(0, YGNodeLayoutGetTop(root));
            Assert.AreEqual(200, YGNodeLayoutGetWidth(root));
            Assert.AreEqual(200, YGNodeLayoutGetHeight(root));

            Assert.AreEqual(0, YGNodeLayoutGetLeft(root_child0));
            Assert.AreEqual(0, YGNodeLayoutGetTop(root_child0));
            Assert.AreEqual(200, YGNodeLayoutGetWidth(root_child0));
            Assert.AreEqual(120, YGNodeLayoutGetHeight(root_child0));

            Assert.AreEqual(0, YGNodeLayoutGetLeft(root_child1));
            Assert.AreEqual(120, YGNodeLayoutGetTop(root_child1));
            Assert.AreEqual(200, YGNodeLayoutGetWidth(root_child1));
            Assert.AreEqual(40, YGNodeLayoutGetHeight(root_child1));

            YGNodeFreeRecursive(root);

            YGConfigFree(config);
        }

        [TestMethod]
        public void percentage_flex_basis_main_max_width()
        {
             YGConfigRef config = YGConfigNew();

             YGNodeRef root = YGNodeNewWithConfig(config);
            YGNodeStyleSetFlexDirection(root, YGFlexDirection.Row);
            YGNodeStyleSetWidth(root, 200);
            YGNodeStyleSetHeight(root, 200);

             YGNodeRef root_child0 = YGNodeNewWithConfig(config);
            YGNodeStyleSetFlexGrow(root_child0, 1);
            YGNodeStyleSetFlexBasisPercent(root_child0, 15);
            YGNodeStyleSetMaxWidthPercent(root_child0, 60);
            YGNodeInsertChild(root, root_child0, 0);

             YGNodeRef root_child1 = YGNodeNewWithConfig(config);
            YGNodeStyleSetFlexGrow(root_child1, 4);
            YGNodeStyleSetFlexBasisPercent(root_child1, 10);
            YGNodeStyleSetMaxWidthPercent(root_child1, 20);
            YGNodeInsertChild(root, root_child1, 1);
            YGNodeCalculateLayout(root, YGUndefined, YGUndefined, YGDirection.LTR);

            Assert.AreEqual(0, YGNodeLayoutGetLeft(root));
            Assert.AreEqual(0, YGNodeLayoutGetTop(root));
            Assert.AreEqual(200, YGNodeLayoutGetWidth(root));
            Assert.AreEqual(200, YGNodeLayoutGetHeight(root));

            Assert.AreEqual(0, YGNodeLayoutGetLeft(root_child0));
            Assert.AreEqual(0, YGNodeLayoutGetTop(root_child0));
            Assert.AreEqual(120, YGNodeLayoutGetWidth(root_child0));
            Assert.AreEqual(200, YGNodeLayoutGetHeight(root_child0));

            Assert.AreEqual(120, YGNodeLayoutGetLeft(root_child1));
            Assert.AreEqual(0, YGNodeLayoutGetTop(root_child1));
            Assert.AreEqual(40, YGNodeLayoutGetWidth(root_child1));
            Assert.AreEqual(200, YGNodeLayoutGetHeight(root_child1));

            YGNodeCalculateLayout(root, YGUndefined, YGUndefined, YGDirection.RTL);

            Assert.AreEqual(0, YGNodeLayoutGetLeft(root));
            Assert.AreEqual(0, YGNodeLayoutGetTop(root));
            Assert.AreEqual(200, YGNodeLayoutGetWidth(root));
            Assert.AreEqual(200, YGNodeLayoutGetHeight(root));

            Assert.AreEqual(80, YGNodeLayoutGetLeft(root_child0));
            Assert.AreEqual(0, YGNodeLayoutGetTop(root_child0));
            Assert.AreEqual(120, YGNodeLayoutGetWidth(root_child0));
            Assert.AreEqual(200, YGNodeLayoutGetHeight(root_child0));

            Assert.AreEqual(40, YGNodeLayoutGetLeft(root_child1));
            Assert.AreEqual(0, YGNodeLayoutGetTop(root_child1));
            Assert.AreEqual(40, YGNodeLayoutGetWidth(root_child1));
            Assert.AreEqual(200, YGNodeLayoutGetHeight(root_child1));

            YGNodeFreeRecursive(root);

            YGConfigFree(config);
        }

        [TestMethod]
        public void percentage_flex_basis_cross_max_width()
        {
             YGConfigRef config = YGConfigNew();

             YGNodeRef root = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(root, 200);
            YGNodeStyleSetHeight(root, 200);

             YGNodeRef root_child0 = YGNodeNewWithConfig(config);
            YGNodeStyleSetFlexGrow(root_child0, 1);
            YGNodeStyleSetFlexBasisPercent(root_child0, 10);
            YGNodeStyleSetMaxWidthPercent(root_child0, 60);
            YGNodeInsertChild(root, root_child0, 0);

             YGNodeRef root_child1 = YGNodeNewWithConfig(config);
            YGNodeStyleSetFlexGrow(root_child1, 4);
            YGNodeStyleSetFlexBasisPercent(root_child1, 15);
            YGNodeStyleSetMaxWidthPercent(root_child1, 20);
            YGNodeInsertChild(root, root_child1, 1);
            YGNodeCalculateLayout(root, YGUndefined, YGUndefined, YGDirection.LTR);

            Assert.AreEqual(0, YGNodeLayoutGetLeft(root));
            Assert.AreEqual(0, YGNodeLayoutGetTop(root));
            Assert.AreEqual(200, YGNodeLayoutGetWidth(root));
            Assert.AreEqual(200, YGNodeLayoutGetHeight(root));

            Assert.AreEqual(0, YGNodeLayoutGetLeft(root_child0));
            Assert.AreEqual(0, YGNodeLayoutGetTop(root_child0));
            Assert.AreEqual(120, YGNodeLayoutGetWidth(root_child0));
            Assert.AreEqual(50, YGNodeLayoutGetHeight(root_child0));

            Assert.AreEqual(0, YGNodeLayoutGetLeft(root_child1));
            Assert.AreEqual(50, YGNodeLayoutGetTop(root_child1));
            Assert.AreEqual(40, YGNodeLayoutGetWidth(root_child1));
            Assert.AreEqual(150, YGNodeLayoutGetHeight(root_child1));

            YGNodeCalculateLayout(root, YGUndefined, YGUndefined, YGDirection.RTL);

            Assert.AreEqual(0, YGNodeLayoutGetLeft(root));
            Assert.AreEqual(0, YGNodeLayoutGetTop(root));
            Assert.AreEqual(200, YGNodeLayoutGetWidth(root));
            Assert.AreEqual(200, YGNodeLayoutGetHeight(root));

            Assert.AreEqual(80, YGNodeLayoutGetLeft(root_child0));
            Assert.AreEqual(0, YGNodeLayoutGetTop(root_child0));
            Assert.AreEqual(120, YGNodeLayoutGetWidth(root_child0));
            Assert.AreEqual(50, YGNodeLayoutGetHeight(root_child0));

            Assert.AreEqual(160, YGNodeLayoutGetLeft(root_child1));
            Assert.AreEqual(50, YGNodeLayoutGetTop(root_child1));
            Assert.AreEqual(40, YGNodeLayoutGetWidth(root_child1));
            Assert.AreEqual(150, YGNodeLayoutGetHeight(root_child1));

            YGNodeFreeRecursive(root);

            YGConfigFree(config);
        }

        [TestMethod]
        public void percentage_flex_basis_main_min_width()
        {
             YGConfigRef config = YGConfigNew();

             YGNodeRef root = YGNodeNewWithConfig(config);
            YGNodeStyleSetFlexDirection(root, YGFlexDirection.Row);
            YGNodeStyleSetWidth(root, 200f);
            YGNodeStyleSetHeight(root, 200f);

             YGNodeRef root_child0 = YGNodeNewWithConfig(config);
            YGNodeStyleSetFlexGrow(root_child0, 1f);
            YGNodeStyleSetFlexBasisPercent(root_child0, 15f);
            YGNodeStyleSetMinWidthPercent(root_child0, 60f);
            YGNodeInsertChild(root, root_child0, 0);

             YGNodeRef root_child1 = YGNodeNewWithConfig(config);
            YGNodeStyleSetFlexGrow(root_child1, 4f);
            YGNodeStyleSetFlexBasisPercent(root_child1, 10f);
            YGNodeStyleSetMinWidthPercent(root_child1, 20f);
            YGNodeInsertChild(root, root_child1, 1);
            YGNodeCalculateLayout(root, YGUndefined, YGUndefined, YGDirection.LTR);

            Assert.AreEqual(0f, YGNodeLayoutGetLeft(root));
            Assert.AreEqual(0f, YGNodeLayoutGetTop(root));
            Assert.AreEqual(200f, YGNodeLayoutGetWidth(root));
            Assert.AreEqual(200f, YGNodeLayoutGetHeight(root));

            Assert.AreEqual(0f, YGNodeLayoutGetLeft(root_child0));
            Assert.AreEqual(0f, YGNodeLayoutGetTop(root_child0));
            Assert.AreEqual(120f, YGNodeLayoutGetWidth(root_child0));
            Assert.AreEqual(200f, YGNodeLayoutGetHeight(root_child0));

            Assert.AreEqual(120f, YGNodeLayoutGetLeft(root_child1));
            Assert.AreEqual(0f, YGNodeLayoutGetTop(root_child1));
            Assert.AreEqual(80f, YGNodeLayoutGetWidth(root_child1));
            Assert.AreEqual(200f, YGNodeLayoutGetHeight(root_child1));

            YGNodeCalculateLayout(root, YGUndefined, YGUndefined, YGDirection.RTL);

            Assert.AreEqual(0f, YGNodeLayoutGetLeft(root));
            Assert.AreEqual(0f, YGNodeLayoutGetTop(root));
            Assert.AreEqual(200f, YGNodeLayoutGetWidth(root));
            Assert.AreEqual(200f, YGNodeLayoutGetHeight(root));

            Assert.AreEqual(80f, YGNodeLayoutGetLeft(root_child0));
            Assert.AreEqual(0f, YGNodeLayoutGetTop(root_child0));
            Assert.AreEqual(120f, YGNodeLayoutGetWidth(root_child0));
            Assert.AreEqual(200f, YGNodeLayoutGetHeight(root_child0));

            Assert.AreEqual(0f, YGNodeLayoutGetLeft(root_child1));
            Assert.AreEqual(0f, YGNodeLayoutGetTop(root_child1));
            Assert.AreEqual(80f, YGNodeLayoutGetWidth(root_child1));
            Assert.AreEqual(200f, YGNodeLayoutGetHeight(root_child1));

            YGNodeFreeRecursive(root);

            YGConfigFree(config);
        }

        [TestMethod]
        public void percentage_flex_basis_cross_min_width()
        {
             YGConfigRef config = YGConfigNew();

             YGNodeRef root = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(root, 200f);
            YGNodeStyleSetHeight(root, 200f);

             YGNodeRef root_child0 = YGNodeNewWithConfig(config);
            YGNodeStyleSetFlexGrow(root_child0, 1f);
            YGNodeStyleSetFlexBasisPercent(root_child0, 10f);
            YGNodeStyleSetMinWidthPercent(root_child0, 60f);
            YGNodeInsertChild(root, root_child0, 0);

             YGNodeRef root_child1 = YGNodeNewWithConfig(config);
            YGNodeStyleSetFlexGrow(root_child1, 4f);
            YGNodeStyleSetFlexBasisPercent(root_child1, 15f);
            YGNodeStyleSetMinWidthPercent(root_child1, 20f);
            YGNodeInsertChild(root, root_child1, 1);
            YGNodeCalculateLayout(root, YGUndefined, YGUndefined, YGDirection.LTR);

            Assert.AreEqual(0f, YGNodeLayoutGetLeft(root));
            Assert.AreEqual(0f, YGNodeLayoutGetTop(root));
            Assert.AreEqual(200f, YGNodeLayoutGetWidth(root));
            Assert.AreEqual(200f, YGNodeLayoutGetHeight(root));

            Assert.AreEqual(0f, YGNodeLayoutGetLeft(root_child0));
            Assert.AreEqual(0f, YGNodeLayoutGetTop(root_child0));
            Assert.AreEqual(200f, YGNodeLayoutGetWidth(root_child0));
            Assert.AreEqual(50f, YGNodeLayoutGetHeight(root_child0));

            Assert.AreEqual(0f, YGNodeLayoutGetLeft(root_child1));
            Assert.AreEqual(50f, YGNodeLayoutGetTop(root_child1));
            Assert.AreEqual(200f, YGNodeLayoutGetWidth(root_child1));
            Assert.AreEqual(150f, YGNodeLayoutGetHeight(root_child1));

            YGNodeCalculateLayout(root, YGUndefined, YGUndefined, YGDirection.RTL);

            Assert.AreEqual(0f, YGNodeLayoutGetLeft(root));
            Assert.AreEqual(0f, YGNodeLayoutGetTop(root));
            Assert.AreEqual(200f, YGNodeLayoutGetWidth(root));
            Assert.AreEqual(200f, YGNodeLayoutGetHeight(root));

            Assert.AreEqual(0f, YGNodeLayoutGetLeft(root_child0));
            Assert.AreEqual(0f, YGNodeLayoutGetTop(root_child0));
            Assert.AreEqual(200f, YGNodeLayoutGetWidth(root_child0));
            Assert.AreEqual(50f, YGNodeLayoutGetHeight(root_child0));

            Assert.AreEqual(0f, YGNodeLayoutGetLeft(root_child1));
            Assert.AreEqual(50f, YGNodeLayoutGetTop(root_child1));
            Assert.AreEqual(200f, YGNodeLayoutGetWidth(root_child1));
            Assert.AreEqual(150f, YGNodeLayoutGetHeight(root_child1));

            YGNodeFreeRecursive(root);

            YGConfigFree(config);
        }

        [TestMethod]
        public void percentage_multiple_nested_with_padding_margin_and_percentage_values()
        {
             YGConfigRef config = YGConfigNew();

             YGNodeRef root = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(root, 200f);
            YGNodeStyleSetHeight(root, 200f);

             YGNodeRef root_child0 = YGNodeNewWithConfig(config);
            YGNodeStyleSetFlexGrow(root_child0, 1);
            YGNodeStyleSetFlexBasisPercent(root_child0, 10f);
            YGNodeStyleSetMargin(root_child0, YGEdge.Left, 5f);
            YGNodeStyleSetMargin(root_child0, YGEdge.Top, 5f);
            YGNodeStyleSetMargin(root_child0, YGEdge.Right, 5f);
            YGNodeStyleSetMargin(root_child0, YGEdge.Bottom, 5f);
            YGNodeStyleSetPadding(root_child0, YGEdge.Left, 3f);
            YGNodeStyleSetPadding(root_child0, YGEdge.Top, 3f);
            YGNodeStyleSetPadding(root_child0, YGEdge.Right, 3f);
            YGNodeStyleSetPadding(root_child0, YGEdge.Bottom, 3f);
            YGNodeStyleSetMinWidthPercent(root_child0, 60f);
            YGNodeInsertChild(root, root_child0, 0);

             YGNodeRef root_child0_child0 = YGNodeNewWithConfig(config);
            YGNodeStyleSetMargin(root_child0_child0, YGEdge.Left, 5f);
            YGNodeStyleSetMargin(root_child0_child0, YGEdge.Top, 5f);
            YGNodeStyleSetMargin(root_child0_child0, YGEdge.Right, 5f);
            YGNodeStyleSetMargin(root_child0_child0, YGEdge.Bottom, 5f);
            YGNodeStyleSetPaddingPercent(root_child0_child0, YGEdge.Left, 3f);
            YGNodeStyleSetPaddingPercent(root_child0_child0, YGEdge.Top, 3f);
            YGNodeStyleSetPaddingPercent(root_child0_child0, YGEdge.Right, 3f);
            YGNodeStyleSetPaddingPercent(root_child0_child0, YGEdge.Bottom, 3f);
            YGNodeStyleSetWidthPercent(root_child0_child0, 50f);
            YGNodeInsertChild(root_child0, root_child0_child0, 0);

             YGNodeRef root_child0_child0_child0 = YGNodeNewWithConfig(config);
            YGNodeStyleSetMarginPercent(root_child0_child0_child0, YGEdge.Left, 5f);
            YGNodeStyleSetMarginPercent(root_child0_child0_child0, YGEdge.Top, 5f);
            YGNodeStyleSetMarginPercent(root_child0_child0_child0, YGEdge.Right, 5f);
            YGNodeStyleSetMarginPercent(root_child0_child0_child0, YGEdge.Bottom, 5f);
            YGNodeStyleSetPadding(root_child0_child0_child0, YGEdge.Left, 3f);
            YGNodeStyleSetPadding(root_child0_child0_child0, YGEdge.Top, 3f);
            YGNodeStyleSetPadding(root_child0_child0_child0, YGEdge.Right, 3f);
            YGNodeStyleSetPadding(root_child0_child0_child0, YGEdge.Bottom, 3f);
            YGNodeStyleSetWidthPercent(root_child0_child0_child0, 45f);
            YGNodeInsertChild(root_child0_child0, root_child0_child0_child0, 0);

             YGNodeRef root_child1 = YGNodeNewWithConfig(config);
            YGNodeStyleSetFlexGrow(root_child1, 4f);
            YGNodeStyleSetFlexBasisPercent(root_child1, 15f);
            YGNodeStyleSetMinWidthPercent(root_child1, 20f);
            YGNodeInsertChild(root, root_child1, 1);
            YGNodeCalculateLayout(root, YGUndefined, YGUndefined, YGDirection.LTR);

            Assert.AreEqual(0f, YGNodeLayoutGetLeft(root));
            Assert.AreEqual(0f, YGNodeLayoutGetTop(root));
            Assert.AreEqual(200f, YGNodeLayoutGetWidth(root));
            Assert.AreEqual(200f, YGNodeLayoutGetHeight(root));

            Assert.AreEqual(5f, YGNodeLayoutGetLeft(root_child0));
            Assert.AreEqual(5f, YGNodeLayoutGetTop(root_child0));
            Assert.AreEqual(190f, YGNodeLayoutGetWidth(root_child0));
            Assert.AreEqual(48f, YGNodeLayoutGetHeight(root_child0));

            Assert.AreEqual(8f, YGNodeLayoutGetLeft(root_child0_child0));
            Assert.AreEqual(8f, YGNodeLayoutGetTop(root_child0_child0));
            Assert.AreEqual(92f, YGNodeLayoutGetWidth(root_child0_child0));
            Assert.AreEqual(25f, YGNodeLayoutGetHeight(root_child0_child0));

            Assert.AreEqual(10f, YGNodeLayoutGetLeft(root_child0_child0_child0));
            Assert.AreEqual(10f, YGNodeLayoutGetTop(root_child0_child0_child0));
            Assert.AreEqual(36f, YGNodeLayoutGetWidth(root_child0_child0_child0));
            Assert.AreEqual(6f, YGNodeLayoutGetHeight(root_child0_child0_child0));

            Assert.AreEqual(0f, YGNodeLayoutGetLeft(root_child1));
            Assert.AreEqual(58f, YGNodeLayoutGetTop(root_child1));
            Assert.AreEqual(200f, YGNodeLayoutGetWidth(root_child1));
            Assert.AreEqual(142f, YGNodeLayoutGetHeight(root_child1));

            YGNodeCalculateLayout(root, YGUndefined, YGUndefined, YGDirection.RTL);

            Assert.AreEqual(0f, YGNodeLayoutGetLeft(root));
            Assert.AreEqual(0f, YGNodeLayoutGetTop(root));
            Assert.AreEqual(200f, YGNodeLayoutGetWidth(root));
            Assert.AreEqual(200f, YGNodeLayoutGetHeight(root));

            Assert.AreEqual(5f, YGNodeLayoutGetLeft(root_child0));
            Assert.AreEqual(5f, YGNodeLayoutGetTop(root_child0));
            Assert.AreEqual(190f, YGNodeLayoutGetWidth(root_child0));
            Assert.AreEqual(48f, YGNodeLayoutGetHeight(root_child0));

            Assert.AreEqual(90f, YGNodeLayoutGetLeft(root_child0_child0));
            Assert.AreEqual(8f, YGNodeLayoutGetTop(root_child0_child0));
            Assert.AreEqual(92f, YGNodeLayoutGetWidth(root_child0_child0));
            Assert.AreEqual(25f, YGNodeLayoutGetHeight(root_child0_child0));

            Assert.AreEqual(46f, YGNodeLayoutGetLeft(root_child0_child0_child0));
            Assert.AreEqual(10f, YGNodeLayoutGetTop(root_child0_child0_child0));
            Assert.AreEqual(36f, YGNodeLayoutGetWidth(root_child0_child0_child0));
            Assert.AreEqual(6f, YGNodeLayoutGetHeight(root_child0_child0_child0));

            Assert.AreEqual(0f, YGNodeLayoutGetLeft(root_child1));
            Assert.AreEqual(58f, YGNodeLayoutGetTop(root_child1));
            Assert.AreEqual(200f, YGNodeLayoutGetWidth(root_child1));
            Assert.AreEqual(142f, YGNodeLayoutGetHeight(root_child1));

            YGNodeFreeRecursive(root);

            YGConfigFree(config);
        }

        [TestMethod]
        public void percentage_margin_should_calculate_based_only_on_width()
        {
             YGConfigRef config = YGConfigNew();

             YGNodeRef root = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(root, 200f);
            YGNodeStyleSetHeight(root, 100f);

             YGNodeRef root_child0 = YGNodeNewWithConfig(config);
            YGNodeStyleSetFlexGrow(root_child0, 1);
            YGNodeStyleSetMarginPercent(root_child0, YGEdge.Left, 10f);
            YGNodeStyleSetMarginPercent(root_child0, YGEdge.Top, 10f);
            YGNodeStyleSetMarginPercent(root_child0, YGEdge.Right, 10f);
            YGNodeStyleSetMarginPercent(root_child0, YGEdge.Bottom, 10f);
            YGNodeInsertChild(root, root_child0, 0);

             YGNodeRef root_child0_child0 = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(root_child0_child0, 10f);
            YGNodeStyleSetHeight(root_child0_child0, 10f);
            YGNodeInsertChild(root_child0, root_child0_child0, 0);
            YGNodeCalculateLayout(root, YGUndefined, YGUndefined, YGDirection.LTR);

            Assert.AreEqual(0f, YGNodeLayoutGetLeft(root));
            Assert.AreEqual(0f, YGNodeLayoutGetTop(root));
            Assert.AreEqual(200f, YGNodeLayoutGetWidth(root));
            Assert.AreEqual(100f, YGNodeLayoutGetHeight(root));

            Assert.AreEqual(20f, YGNodeLayoutGetLeft(root_child0));
            Assert.AreEqual(20f, YGNodeLayoutGetTop(root_child0));
            Assert.AreEqual(160f, YGNodeLayoutGetWidth(root_child0));
            Assert.AreEqual(60f, YGNodeLayoutGetHeight(root_child0));

            Assert.AreEqual(0f, YGNodeLayoutGetLeft(root_child0_child0));
            Assert.AreEqual(0f, YGNodeLayoutGetTop(root_child0_child0));
            Assert.AreEqual(10f, YGNodeLayoutGetWidth(root_child0_child0));
            Assert.AreEqual(10f, YGNodeLayoutGetHeight(root_child0_child0));

            YGNodeCalculateLayout(root, YGUndefined, YGUndefined, YGDirection.RTL);

            Assert.AreEqual(0f, YGNodeLayoutGetLeft(root));
            Assert.AreEqual(0f, YGNodeLayoutGetTop(root));
            Assert.AreEqual(200f, YGNodeLayoutGetWidth(root));
            Assert.AreEqual(100f, YGNodeLayoutGetHeight(root));

            Assert.AreEqual(20f, YGNodeLayoutGetLeft(root_child0));
            Assert.AreEqual(20f, YGNodeLayoutGetTop(root_child0));
            Assert.AreEqual(160f, YGNodeLayoutGetWidth(root_child0));
            Assert.AreEqual(60f, YGNodeLayoutGetHeight(root_child0));

            Assert.AreEqual(150f, YGNodeLayoutGetLeft(root_child0_child0));
            Assert.AreEqual(0f, YGNodeLayoutGetTop(root_child0_child0));
            Assert.AreEqual(10f, YGNodeLayoutGetWidth(root_child0_child0));
            Assert.AreEqual(10f, YGNodeLayoutGetHeight(root_child0_child0));

            YGNodeFreeRecursive(root);

            YGConfigFree(config);
        }

        [TestMethod]
        public void percentage_padding_should_calculate_based_only_on_width()
        {
             YGConfigRef config = YGConfigNew();

             YGNodeRef root = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(root, 200f);
            YGNodeStyleSetHeight(root, 100f);

             YGNodeRef root_child0 = YGNodeNewWithConfig(config);
            YGNodeStyleSetFlexGrow(root_child0, 1);
            YGNodeStyleSetPaddingPercent(root_child0, YGEdge.Left, 10f);
            YGNodeStyleSetPaddingPercent(root_child0, YGEdge.Top, 10f);
            YGNodeStyleSetPaddingPercent(root_child0, YGEdge.Right, 10f);
            YGNodeStyleSetPaddingPercent(root_child0, YGEdge.Bottom, 10f);
            YGNodeInsertChild(root, root_child0, 0);

             YGNodeRef root_child0_child0 = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(root_child0_child0, 10f);
            YGNodeStyleSetHeight(root_child0_child0, 10f);
            YGNodeInsertChild(root_child0, root_child0_child0, 0);
            YGNodeCalculateLayout(root, YGUndefined, YGUndefined, YGDirection.LTR);

            Assert.AreEqual(0f, YGNodeLayoutGetLeft(root));
            Assert.AreEqual(0f, YGNodeLayoutGetTop(root));
            Assert.AreEqual(200f, YGNodeLayoutGetWidth(root));
            Assert.AreEqual(100f, YGNodeLayoutGetHeight(root));

            Assert.AreEqual(0f, YGNodeLayoutGetLeft(root_child0));
            Assert.AreEqual(0f, YGNodeLayoutGetTop(root_child0));
            Assert.AreEqual(200f, YGNodeLayoutGetWidth(root_child0));
            Assert.AreEqual(100f, YGNodeLayoutGetHeight(root_child0));

            Assert.AreEqual(20f, YGNodeLayoutGetLeft(root_child0_child0));
            Assert.AreEqual(20f, YGNodeLayoutGetTop(root_child0_child0));
            Assert.AreEqual(10f, YGNodeLayoutGetWidth(root_child0_child0));
            Assert.AreEqual(10f, YGNodeLayoutGetHeight(root_child0_child0));

            YGNodeCalculateLayout(root, YGUndefined, YGUndefined, YGDirection.RTL);

            Assert.AreEqual(0f, YGNodeLayoutGetLeft(root));
            Assert.AreEqual(0f, YGNodeLayoutGetTop(root));
            Assert.AreEqual(200f, YGNodeLayoutGetWidth(root));
            Assert.AreEqual(100f, YGNodeLayoutGetHeight(root));

            Assert.AreEqual(0f, YGNodeLayoutGetLeft(root_child0));
            Assert.AreEqual(0f, YGNodeLayoutGetTop(root_child0));
            Assert.AreEqual(200f, YGNodeLayoutGetWidth(root_child0));
            Assert.AreEqual(100f, YGNodeLayoutGetHeight(root_child0));

            Assert.AreEqual(170f, YGNodeLayoutGetLeft(root_child0_child0));
            Assert.AreEqual(20f, YGNodeLayoutGetTop(root_child0_child0));
            Assert.AreEqual(10f, YGNodeLayoutGetWidth(root_child0_child0));
            Assert.AreEqual(10f, YGNodeLayoutGetHeight(root_child0_child0));

            YGNodeFreeRecursive(root);

            YGConfigFree(config);
        }

        [TestMethod]
        public void percentage_absolute_position()
        {
             YGConfigRef config = YGConfigNew();

             YGNodeRef root = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(root, 200f);
            YGNodeStyleSetHeight(root, 100f);

             YGNodeRef root_child0 = YGNodeNewWithConfig(config);
            YGNodeStyleSetPositionType(root_child0, YGPositionType.Absolute);
            YGNodeStyleSetPositionPercent(root_child0, YGEdge.Left, 30f);
            YGNodeStyleSetPositionPercent(root_child0, YGEdge.Top, 10f);
            YGNodeStyleSetWidth(root_child0, 10f);
            YGNodeStyleSetHeight(root_child0, 10f);
            YGNodeInsertChild(root, root_child0, 0);
            YGNodeCalculateLayout(root, YGUndefined, YGUndefined, YGDirection.LTR);

            Assert.AreEqual(0f, YGNodeLayoutGetLeft(root));
            Assert.AreEqual(0f, YGNodeLayoutGetTop(root));
            Assert.AreEqual(200f, YGNodeLayoutGetWidth(root));
            Assert.AreEqual(100f, YGNodeLayoutGetHeight(root));

            Assert.AreEqual(60f, YGNodeLayoutGetLeft(root_child0));
            Assert.AreEqual(10f, YGNodeLayoutGetTop(root_child0));
            Assert.AreEqual(10f, YGNodeLayoutGetWidth(root_child0));
            Assert.AreEqual(10f, YGNodeLayoutGetHeight(root_child0));

            YGNodeCalculateLayout(root, YGUndefined, YGUndefined, YGDirection.RTL);

            Assert.AreEqual(0f, YGNodeLayoutGetLeft(root));
            Assert.AreEqual(0f, YGNodeLayoutGetTop(root));
            Assert.AreEqual(200f, YGNodeLayoutGetWidth(root));
            Assert.AreEqual(100f, YGNodeLayoutGetHeight(root));

            Assert.AreEqual(60f, YGNodeLayoutGetLeft(root_child0));
            Assert.AreEqual(10f, YGNodeLayoutGetTop(root_child0));
            Assert.AreEqual(10f, YGNodeLayoutGetWidth(root_child0));
            Assert.AreEqual(10f, YGNodeLayoutGetHeight(root_child0));

            YGNodeFreeRecursive(root);

            YGConfigFree(config);
        }

        [TestMethod]
        public void percentage_width_height_undefined_parent_size()
        {
             YGConfigRef config = YGConfigNew();

             YGNodeRef root = YGNodeNewWithConfig(config);

             YGNodeRef root_child0 = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidthPercent(root_child0, 50f);
            YGNodeStyleSetHeightPercent(root_child0, 50f);
            YGNodeInsertChild(root, root_child0, 0);
            YGNodeCalculateLayout(root, YGUndefined, YGUndefined, YGDirection.LTR);

            Assert.AreEqual(0f, YGNodeLayoutGetLeft(root));
            Assert.AreEqual(0f, YGNodeLayoutGetTop(root));
            Assert.AreEqual(0f, YGNodeLayoutGetWidth(root));
            Assert.AreEqual(0f, YGNodeLayoutGetHeight(root));

            Assert.AreEqual(0f, YGNodeLayoutGetLeft(root_child0));
            Assert.AreEqual(0f, YGNodeLayoutGetTop(root_child0));
            Assert.AreEqual(0f, YGNodeLayoutGetWidth(root_child0));
            Assert.AreEqual(0f, YGNodeLayoutGetHeight(root_child0));

            YGNodeCalculateLayout(root, YGUndefined, YGUndefined, YGDirection.RTL);

            Assert.AreEqual(0f, YGNodeLayoutGetLeft(root));
            Assert.AreEqual(0f, YGNodeLayoutGetTop(root));
            Assert.AreEqual(0f, YGNodeLayoutGetWidth(root));
            Assert.AreEqual(0f, YGNodeLayoutGetHeight(root));

            Assert.AreEqual(0f, YGNodeLayoutGetLeft(root_child0));
            Assert.AreEqual(0f, YGNodeLayoutGetTop(root_child0));
            Assert.AreEqual(0f, YGNodeLayoutGetWidth(root_child0));
            Assert.AreEqual(0f, YGNodeLayoutGetHeight(root_child0));

            YGNodeFreeRecursive(root);

            YGConfigFree(config);
        }

        [TestMethod]
        public void percent_within_flex_grow()
        {
             YGConfigRef config = YGConfigNew();

             YGNodeRef root = YGNodeNewWithConfig(config);
            YGNodeStyleSetFlexDirection(root, YGFlexDirection.Row);
            YGNodeStyleSetWidth(root, 350f);
            YGNodeStyleSetHeight(root, 100f);

             YGNodeRef root_child0 = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(root_child0, 100f);
            YGNodeInsertChild(root, root_child0, 0);

             YGNodeRef root_child1 = YGNodeNewWithConfig(config);
            YGNodeStyleSetFlexGrow(root_child1, 1f);
            YGNodeInsertChild(root, root_child1, 1);

             YGNodeRef root_child1_child0 = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidthPercent(root_child1_child0, 100f);
            YGNodeInsertChild(root_child1, root_child1_child0, 0);

             YGNodeRef root_child2 = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(root_child2, 100f);
            YGNodeInsertChild(root, root_child2, 2);
            YGNodeCalculateLayout(root, YGUndefined, YGUndefined, YGDirection.LTR);

            Assert.AreEqual(0f, YGNodeLayoutGetLeft(root));
            Assert.AreEqual(0f, YGNodeLayoutGetTop(root));
            Assert.AreEqual(350f, YGNodeLayoutGetWidth(root));
            Assert.AreEqual(100f, YGNodeLayoutGetHeight(root));

            Assert.AreEqual(0f, YGNodeLayoutGetLeft(root_child0));
            Assert.AreEqual(0f, YGNodeLayoutGetTop(root_child0));
            Assert.AreEqual(100f, YGNodeLayoutGetWidth(root_child0));
            Assert.AreEqual(100f, YGNodeLayoutGetHeight(root_child0));

            Assert.AreEqual(100f, YGNodeLayoutGetLeft(root_child1));
            Assert.AreEqual(0f, YGNodeLayoutGetTop(root_child1));
            Assert.AreEqual(150f, YGNodeLayoutGetWidth(root_child1));
            Assert.AreEqual(100f, YGNodeLayoutGetHeight(root_child1));

            Assert.AreEqual(0f, YGNodeLayoutGetLeft(root_child1_child0));
            Assert.AreEqual(0f, YGNodeLayoutGetTop(root_child1_child0));
            Assert.AreEqual(150f, YGNodeLayoutGetWidth(root_child1_child0));
            Assert.AreEqual(0f, YGNodeLayoutGetHeight(root_child1_child0));

            Assert.AreEqual(250f, YGNodeLayoutGetLeft(root_child2));
            Assert.AreEqual(0f, YGNodeLayoutGetTop(root_child2));
            Assert.AreEqual(100f, YGNodeLayoutGetWidth(root_child2));
            Assert.AreEqual(100f, YGNodeLayoutGetHeight(root_child2));

            YGNodeCalculateLayout(root, YGUndefined, YGUndefined, YGDirection.RTL);

            Assert.AreEqual(0f, YGNodeLayoutGetLeft(root));
            Assert.AreEqual(0f, YGNodeLayoutGetTop(root));
            Assert.AreEqual(350f, YGNodeLayoutGetWidth(root));
            Assert.AreEqual(100f, YGNodeLayoutGetHeight(root));

            Assert.AreEqual(250f, YGNodeLayoutGetLeft(root_child0));
            Assert.AreEqual(0f, YGNodeLayoutGetTop(root_child0));
            Assert.AreEqual(100f, YGNodeLayoutGetWidth(root_child0));
            Assert.AreEqual(100f, YGNodeLayoutGetHeight(root_child0));

            Assert.AreEqual(100f, YGNodeLayoutGetLeft(root_child1));
            Assert.AreEqual(0f, YGNodeLayoutGetTop(root_child1));
            Assert.AreEqual(150f, YGNodeLayoutGetWidth(root_child1));
            Assert.AreEqual(100f, YGNodeLayoutGetHeight(root_child1));

            Assert.AreEqual(0f, YGNodeLayoutGetLeft(root_child1_child0));
            Assert.AreEqual(0f, YGNodeLayoutGetTop(root_child1_child0));
            Assert.AreEqual(150f, YGNodeLayoutGetWidth(root_child1_child0));
            Assert.AreEqual(0f, YGNodeLayoutGetHeight(root_child1_child0));

            Assert.AreEqual(0f, YGNodeLayoutGetLeft(root_child2));
            Assert.AreEqual(0f, YGNodeLayoutGetTop(root_child2));
            Assert.AreEqual(100f, YGNodeLayoutGetWidth(root_child2));
            Assert.AreEqual(100f, YGNodeLayoutGetHeight(root_child2));

            YGNodeFreeRecursive(root);

            YGConfigFree(config);
        }

        [TestMethod]
        public void percentage_container_in_wrapping_container()
        {
             YGConfigRef config = YGConfigNew();

             YGNodeRef root = YGNodeNewWithConfig(config);
            YGNodeStyleSetJustifyContent(root, YGJustify.Center);
            YGNodeStyleSetAlignItems(root, YGAlign.Center);
            YGNodeStyleSetWidth(root, 200f);
            YGNodeStyleSetHeight(root, 200f);

             YGNodeRef root_child0 = YGNodeNewWithConfig(config);
            YGNodeInsertChild(root, root_child0, 0);

             YGNodeRef root_child0_child0 = YGNodeNewWithConfig(config);
            YGNodeStyleSetFlexDirection(root_child0_child0, YGFlexDirection.Row);
            YGNodeStyleSetJustifyContent(root_child0_child0, YGJustify.Center);
            YGNodeStyleSetWidthPercent(root_child0_child0, 100f);
            YGNodeInsertChild(root_child0, root_child0_child0, 0);

             YGNodeRef root_child0_child0_child0 = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(root_child0_child0_child0, 50f);
            YGNodeStyleSetHeight(root_child0_child0_child0, 50f);
            YGNodeInsertChild(root_child0_child0, root_child0_child0_child0, 0);

             YGNodeRef root_child0_child0_child1 = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(root_child0_child0_child1, 50f);
            YGNodeStyleSetHeight(root_child0_child0_child1, 50f);
            YGNodeInsertChild(root_child0_child0, root_child0_child0_child1, 1);
            YGNodeCalculateLayout(root, YGUndefined, YGUndefined, YGDirection.LTR);

            Assert.AreEqual(0f, YGNodeLayoutGetLeft(root));
            Assert.AreEqual(0f, YGNodeLayoutGetTop(root));
            Assert.AreEqual(200f, YGNodeLayoutGetWidth(root));
            Assert.AreEqual(200f, YGNodeLayoutGetHeight(root));

            Assert.AreEqual(50f, YGNodeLayoutGetLeft(root_child0));
            Assert.AreEqual(75f, YGNodeLayoutGetTop(root_child0));
            Assert.AreEqual(100f, YGNodeLayoutGetWidth(root_child0));
            Assert.AreEqual(50f, YGNodeLayoutGetHeight(root_child0));

            Assert.AreEqual(0f, YGNodeLayoutGetLeft(root_child0_child0));
            Assert.AreEqual(0f, YGNodeLayoutGetTop(root_child0_child0));
            Assert.AreEqual(100f, YGNodeLayoutGetWidth(root_child0_child0));
            Assert.AreEqual(50f, YGNodeLayoutGetHeight(root_child0_child0));

            Assert.AreEqual(0f, YGNodeLayoutGetLeft(root_child0_child0_child0));
            Assert.AreEqual(0f, YGNodeLayoutGetTop(root_child0_child0_child0));
            Assert.AreEqual(50f, YGNodeLayoutGetWidth(root_child0_child0_child0));
            Assert.AreEqual(50f, YGNodeLayoutGetHeight(root_child0_child0_child0));

            Assert.AreEqual(50f, YGNodeLayoutGetLeft(root_child0_child0_child1));
            Assert.AreEqual(0f, YGNodeLayoutGetTop(root_child0_child0_child1));
            Assert.AreEqual(50f, YGNodeLayoutGetWidth(root_child0_child0_child1));
            Assert.AreEqual(50f, YGNodeLayoutGetHeight(root_child0_child0_child1));

            YGNodeCalculateLayout(root, YGUndefined, YGUndefined, YGDirection.RTL);

            Assert.AreEqual(0f, YGNodeLayoutGetLeft(root));
            Assert.AreEqual(0f, YGNodeLayoutGetTop(root));
            Assert.AreEqual(200f, YGNodeLayoutGetWidth(root));
            Assert.AreEqual(200f, YGNodeLayoutGetHeight(root));

            Assert.AreEqual(50f, YGNodeLayoutGetLeft(root_child0));
            Assert.AreEqual(75f, YGNodeLayoutGetTop(root_child0));
            Assert.AreEqual(100f, YGNodeLayoutGetWidth(root_child0));
            Assert.AreEqual(50f, YGNodeLayoutGetHeight(root_child0));

            Assert.AreEqual(0f, YGNodeLayoutGetLeft(root_child0_child0));
            Assert.AreEqual(0f, YGNodeLayoutGetTop(root_child0_child0));
            Assert.AreEqual(100f, YGNodeLayoutGetWidth(root_child0_child0));
            Assert.AreEqual(50f, YGNodeLayoutGetHeight(root_child0_child0));

            Assert.AreEqual(50f, YGNodeLayoutGetLeft(root_child0_child0_child0));
            Assert.AreEqual(0f, YGNodeLayoutGetTop(root_child0_child0_child0));
            Assert.AreEqual(50f, YGNodeLayoutGetWidth(root_child0_child0_child0));
            Assert.AreEqual(50f, YGNodeLayoutGetHeight(root_child0_child0_child0));

            Assert.AreEqual(0f, YGNodeLayoutGetLeft(root_child0_child0_child1));
            Assert.AreEqual(0f, YGNodeLayoutGetTop(root_child0_child0_child1));
            Assert.AreEqual(50f, YGNodeLayoutGetWidth(root_child0_child0_child1));
            Assert.AreEqual(50f, YGNodeLayoutGetHeight(root_child0_child0_child1));

            YGNodeFreeRecursive(root);

            YGConfigFree(config);
        }

        [TestMethod]
        public void percent_absolute_position()
        {
             YGConfigRef config = YGConfigNew();

             YGNodeRef root = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidth(root, 60f);
            YGNodeStyleSetHeight(root, 50f);

             YGNodeRef root_child0 = YGNodeNewWithConfig(config);
            YGNodeStyleSetFlexDirection(root_child0, YGFlexDirection.Row);
            YGNodeStyleSetPositionType(root_child0, YGPositionType.Absolute);
            YGNodeStyleSetPositionPercent(root_child0, YGEdge.Left, 50);
            YGNodeStyleSetWidthPercent(root_child0, 100f);
            YGNodeStyleSetHeight(root_child0, 50f);
            YGNodeInsertChild(root, root_child0, 0);

             YGNodeRef root_child0_child0 = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidthPercent(root_child0_child0, 100f);
            YGNodeInsertChild(root_child0, root_child0_child0, 0);

             YGNodeRef root_child0_child1 = YGNodeNewWithConfig(config);
            YGNodeStyleSetWidthPercent(root_child0_child1, 100f);
            YGNodeInsertChild(root_child0, root_child0_child1, 1);
            YGNodeCalculateLayout(root, YGUndefined, YGUndefined, YGDirection.LTR);

            Assert.AreEqual(0f, YGNodeLayoutGetLeft(root));
            Assert.AreEqual(0f, YGNodeLayoutGetTop(root));
            Assert.AreEqual(60f, YGNodeLayoutGetWidth(root));
            Assert.AreEqual(50f, YGNodeLayoutGetHeight(root));

            Assert.AreEqual(30f, YGNodeLayoutGetLeft(root_child0));
            Assert.AreEqual(0f, YGNodeLayoutGetTop(root_child0));
            Assert.AreEqual(60f, YGNodeLayoutGetWidth(root_child0));
            Assert.AreEqual(50f, YGNodeLayoutGetHeight(root_child0));

            Assert.AreEqual(0f, YGNodeLayoutGetLeft(root_child0_child0));
            Assert.AreEqual(0f, YGNodeLayoutGetTop(root_child0_child0));
            Assert.AreEqual(60f, YGNodeLayoutGetWidth(root_child0_child0));
            Assert.AreEqual(50f, YGNodeLayoutGetHeight(root_child0_child0));

            Assert.AreEqual(60f, YGNodeLayoutGetLeft(root_child0_child1));
            Assert.AreEqual(0f, YGNodeLayoutGetTop(root_child0_child1));
            Assert.AreEqual(60f, YGNodeLayoutGetWidth(root_child0_child1));
            Assert.AreEqual(50f, YGNodeLayoutGetHeight(root_child0_child1));

            YGNodeCalculateLayout(root, YGUndefined, YGUndefined, YGDirection.RTL);

            Assert.AreEqual(0f, YGNodeLayoutGetLeft(root));
            Assert.AreEqual(0f, YGNodeLayoutGetTop(root));
            Assert.AreEqual(60f, YGNodeLayoutGetWidth(root));
            Assert.AreEqual(50f, YGNodeLayoutGetHeight(root));

            Assert.AreEqual(30f, YGNodeLayoutGetLeft(root_child0));
            Assert.AreEqual(0f, YGNodeLayoutGetTop(root_child0));
            Assert.AreEqual(60f, YGNodeLayoutGetWidth(root_child0));
            Assert.AreEqual(50f, YGNodeLayoutGetHeight(root_child0));

            Assert.AreEqual(0f, YGNodeLayoutGetLeft(root_child0_child0));
            Assert.AreEqual(0f, YGNodeLayoutGetTop(root_child0_child0));
            Assert.AreEqual(60f, YGNodeLayoutGetWidth(root_child0_child0));
            Assert.AreEqual(50f, YGNodeLayoutGetHeight(root_child0_child0));

            Assert.AreEqual(-60f, YGNodeLayoutGetLeft(root_child0_child1));
            Assert.AreEqual(0f, YGNodeLayoutGetTop(root_child0_child1));
            Assert.AreEqual(60f, YGNodeLayoutGetWidth(root_child0_child1));
            Assert.AreEqual(50f, YGNodeLayoutGetHeight(root_child0_child1));

            YGNodeFreeRecursive(root);

            YGConfigFree(config);
        }

    }
}
