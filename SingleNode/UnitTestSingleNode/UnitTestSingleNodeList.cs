//-----------------------------------------------------------------------
// <copyright file="UnitTestSingleNodeList.cs" company="Graham D Wardle">
//     The Single node Linked list sampler.
// </copyright>
//-----------------------------------------------------------------------

namespace UnitTestSingleNode
{
    using System.Collections.Generic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using SingleNode;

    /// <summary>
    /// The Unit tests for the Single Node Linked list class.
    /// </summary>
    [TestClass]
    public class UnitTestSingleNodeList
    {
        /// <summary>
        /// The test for a single node.
        /// </summary>
        [TestMethod]
        public void TestMethodSingle()
        {
            SingleNodeList root = new SingleNodeList(60);
            Assert.IsFalse(root.Contains(10));
            Assert.IsTrue(root.Contains(60));
            List<int> list = root.GetList();
            Assert.AreEqual(1, list.Count);
            Assert.AreEqual(60, list[0]);
        }

        /// <summary>
        /// The test for a two different nodes.
        /// </summary>
        [TestMethod]
        public void TestMethodDouble()
        {
            SingleNodeList root = new SingleNodeList(60);
            root.AddNode(300);
            List<int> list = root.GetList();
            Assert.AreEqual(2, list.Count);
            Assert.AreEqual(60, list[0]);
        }

        /// <summary>
        /// The test for a three different nodes.
        /// </summary>
        [TestMethod]
        public void TestMethodTriple()
        {
            SingleNodeList root = new SingleNodeList(60);
            root.AddNode(300);
            root.AddNode(30);
            List<int> list = root.GetList();
            Assert.AreEqual(3, list.Count);
            Assert.AreEqual(30, list[0]);
            Assert.AreEqual(60, list[1]);
            Assert.AreEqual(300, list[2]);
        }

        /// <summary>
        /// The test for a four different nodes.
        /// </summary>
        [TestMethod]
        public void TestMethodAddFour()
        {
            SingleNodeList root = new SingleNodeList(60);
            root.AddNode(300);
            root.AddNode(30);
            root.AddNode(20);
            Assert.IsFalse(root.Contains(10));
            Assert.IsTrue(root.Contains(60));

            List<int> list = root.GetList();
            Assert.AreEqual(4, list.Count);
            Assert.AreEqual(20, list[0]);
            Assert.AreEqual(30, list[1]);
            Assert.AreEqual(60, list[2]);
            Assert.AreEqual(300, list[3]);
        }

        /// <summary>
        /// The test for a multiple nodes and a duplicate.
        /// </summary>
        [TestMethod]
        public void TestMethodAddDuplicate()
        {
            SingleNodeList root = new SingleNodeList(60);
            root.AddNode(300);
            root.AddNode(30);
            root.AddNode(20);
            root.AddNode(30);
            Assert.IsFalse(root.Contains(10));
            Assert.IsTrue(root.Contains(60));
            Assert.IsFalse(root.Contains(110));
            Assert.IsTrue(root.Contains(30));

            List<int> list = root.GetList();
            Assert.AreEqual(5, list.Count);
            Assert.AreEqual(20, list[0]);
            Assert.AreEqual(30, list[1]);
            Assert.AreEqual(30, list[2]);
            Assert.AreEqual(60, list[3]);
            Assert.AreEqual(300, list[4]);
        }
    }
}