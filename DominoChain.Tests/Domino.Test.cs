namespace DominoChain.Tests
{
	using System.Collections.Generic;
	using Microsoft.VisualStudio.TestTools.UnitTesting;

	[TestClass]
	public class DominoTestCase
	{

		[TestMethod]
		public void BuildChainTest() {
			var domino = new Domino();
			string chain;
			Assert.IsTrue(domino.BuildChain("01 12 23 34 45", out chain));
			Assert.IsFalse(domino.BuildChain("01 12 23 34 45 66", out chain));
			Assert.IsFalse(domino.BuildChain("01 12 23 44 45 56", out chain));
		}

		[TestMethod]
		public void BuildBonePairsTets() {
			var domino = new Domino();
			List<Bone> singleBones;
			var pairs = domino.BuildBonePairs(domino.ConvertStringToBones("01 12 23 43 45"), out singleBones);
			Assert.AreEqual(4, pairs.Count);
			Assert.AreEqual(0, singleBones.Count);
		}

		[TestMethod]
		public void BuildBonePairsTets2() {
			var domino = new Domino();
			List<Bone> singleBones;
			var pairs = domino.BuildBonePairs(domino.ConvertStringToBones("11 22 34 45 56"), out singleBones);
			Assert.AreEqual(2, pairs.Count);
			Assert.AreEqual(2, singleBones.Count);
		}

		#region Bone Pair Tests

		[TestMethod]
		public void PairWithSameBonesAndTypeAreEqualsTest() {
			var boneX = new Bone {
				A = '1',
				B = '2'
			};
			var boneY = new Bone {
				A = '2',
				B = '3'
			};
			var bonePair1 = new BonePair(boneX, boneY, BonePairType.BA);
			var bonePair2 = new BonePair(boneX, boneY, BonePairType.BA);

			var set = new HashSet<BonePair>();
			set.Add(bonePair1);
			set.Add(bonePair2);
			set.Add(bonePair2);
			Assert.AreEqual(1, set.Count);
			Assert.AreEqual(1, boneX.Edges.Count);
			Assert.AreEqual(1, boneY.Edges.Count);
		}

		[TestMethod]
		public void InvertPairTypeTest() {
			Assert.AreEqual(BonePairType.BB, BonePairType.AA ^ BonePairType.InvertMask);
			Assert.AreEqual(BonePairType.AA, BonePairType.BB ^ BonePairType.InvertMask);
			Assert.AreEqual(BonePairType.BA, BonePairType.AB ^ BonePairType.InvertMask);
			Assert.AreEqual(BonePairType.AB, BonePairType.BA ^ BonePairType.InvertMask);
		}

		[TestMethod]
		public void PairWithSameBonesAndSwapedNumbersAreEqualsTest() {
			var boneX = new Bone {
				A = '1',
				B = '2'
			};
			var boneY = new Bone {
				A = '2',
				B = '3'
			};
			var bonePair1 = new BonePair(boneX, boneY, BonePairType.BA);
			var bonePair2 = new BonePair(boneY, boneX, BonePairType.AB);

			var set = new HashSet<BonePair>();
			set.Add(bonePair1);
			set.Add(bonePair2);
			set.Add(bonePair2);
			Assert.AreEqual(1, set.Count);
			Assert.AreEqual(1, boneX.Edges.Count);
			Assert.AreEqual(1, boneY.Edges.Count);
		}

		#endregion

	}
}
