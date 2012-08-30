namespace DominoChain
{
	using System.Collections.Generic;
	using System.Diagnostics;
	using System.Linq;

	public class Domino
	{

		public bool BuildChain(string inputChain, out string outputChain) {
			var bones = ConvertStringToBones(inputChain);
			if (bones.Count == 0) {
				outputChain = null;
				return false;
			}
			List<Bone> separateBones;
			var pairs = BuildBonePairs(bones, out separateBones);
			if (separateBones.Count != 0) {
				outputChain = null;
				return false;
			}
			var closingBones = bones.Where(b => b.Edges.Count == 1).ToList();
			if (closingBones.Count > 2) {
				outputChain = null;
				return false;
			}
			LinkedList<Bone> boness = new LinkedList<Bone>();
			Bone firstBone = closingBones.Count > 0 ? closingBones[0] : bones.OrderBy(b => b.Edges.Count).First();
			outputChain = VisitBones(firstBone, true, '0');
			return outputChain.Split(' ').Length == bones.Count;
		}

		internal List<Bone> ConvertStringToBones(string dominoes) {
			return dominoes.Split(' ')
				.Where(d => !string.IsNullOrEmpty(d) && d.Length == 2)
				.Select(d => new Bone {
					A = d[0],
					B = d[1]
				}).ToList();
		}

		internal HashSet<BonePair> BuildBonePairs(List<Bone> bones, out List<Bone> separateBones) {
			var pairs = new HashSet<BonePair>();
			separateBones = new List<Bone>(bones);
			for (int i = 0; i < bones.Count; i++) {
				for (int j = i + 1; j < bones.Count; j++) {
					bool isPair = false;
					if (bones[i].A == bones[j].A) {
						isPair = pairs.Add(new BonePair(bones[i], bones[j], BonePairType.AA));
					} else if (bones[i].A == bones[j].B) {
						isPair = pairs.Add(new BonePair(bones[i], bones[j], BonePairType.AB));
					} else if (bones[i].B == bones[j].A) {
						isPair = pairs.Add(new BonePair(bones[i], bones[j], BonePairType.BA));
					} else if (bones[i].B == bones[j].B) {
						isPair = pairs.Add(new BonePair(bones[i], bones[j], BonePairType.BB));
					}

					if (isPair) {
						separateBones.Remove(bones[i]);
						separateBones.Remove(bones[j]);
					}
				}
			}
			return pairs;
		}

		private string VisitBones(Bone bone, bool isFirst, char lastNum) {
			var pair = bone.Edges.FirstOrDefault(p => !p.Visited);
			if (pair == null) {
				return lastNum.ToString();
			}
			pair.Visited = true;
			var nextBone = bone == pair.BoneX ? pair.BoneY : pair.BoneX;
			var pairType = pair.Type;
			if (pair.BoneY == bone) {
				pairType = pairType ^ BonePairType.InvertMask;
			}
			switch (pairType) {
				case BonePairType.AA:
					return (isFirst ? bone.B.ToString() : string.Empty) + bone.A.ToString() + " " + nextBone.A.ToString() + VisitBones(nextBone, false, nextBone.B);
				case BonePairType.BB:
					return (isFirst ? bone.A.ToString() : string.Empty) + bone.B.ToString() + " " + nextBone.B.ToString() + VisitBones(nextBone, false, nextBone.A);
				case BonePairType.AB:
					return (isFirst ? bone.B.ToString() : string.Empty) + bone.A.ToString() + " " + nextBone.B.ToString() + VisitBones(nextBone, false, nextBone.A);
				case BonePairType.BA:
					return (isFirst ? bone.A.ToString() : string.Empty) + bone.B.ToString() + " " + nextBone.A.ToString() + VisitBones(nextBone, false, nextBone.B);
				default:
					return string.Empty;
			}
		}

	}

	public class Bone
	{

		public char A {
			get;
			set;
		}
		public char B {
			get;
			set;
		}

		private HashSet<BonePair> _edges;
		public HashSet<BonePair> Edges {
			get {
				return _edges ?? (_edges = new HashSet<BonePair>());
			}
		}


		public override string ToString() {
			return A.ToString() + B.ToString();
		}
	}

	public enum BonePairType
	{
		AA = 1,  // 0001
		BB = 14, // 1110
		AB = 4,  // 0111
		BA = 11, // 1011
		InvertMask = 15 //1111
	}

	[DebuggerDisplay("{BoneX} {BoneY}  ({Type})")]
	public class BonePair
	{

		public BonePair(Bone boneX, Bone boneY, BonePairType type) {
			BoneX = boneX;
			BoneY = boneY;
			Type = type;
			BoneX.Edges.Add(this);
			BoneY.Edges.Add(this);
		}

		public Bone BoneX {
			get;
			private set;
		}
		public Bone BoneY {
			get;
			private set;
		}
		public BonePairType Type {
			get;
			private set;
		}
		public bool Visited {
			get;
			set;
		}

		public override int GetHashCode() {
			return BoneX.GetHashCode() | BoneY.GetHashCode();
		}

		public override bool Equals(object obj) {
			var pair = (BonePair)obj;
			return
				(pair.Type == Type
					&& pair.BoneX.Equals(BoneX) && pair.BoneY.Equals(BoneY)) ||
				(pair.Type == (Type ^ BonePairType.InvertMask)
					&& pair.BoneY.Equals(BoneX) && pair.BoneX.Equals(BoneY));
		}

	}

}