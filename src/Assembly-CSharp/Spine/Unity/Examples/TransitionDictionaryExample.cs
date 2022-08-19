using System;
using System.Collections.Generic;
using UnityEngine;

namespace Spine.Unity.Examples
{
	// Token: 0x02000AE4 RID: 2788
	public sealed class TransitionDictionaryExample : MonoBehaviour
	{
		// Token: 0x06004DFC RID: 19964 RVA: 0x00214E64 File Offset: 0x00213064
		private void Start()
		{
			this.dictionary.Clear();
			foreach (TransitionDictionaryExample.SerializedEntry serializedEntry in this.transitions)
			{
				this.dictionary.Add(new AnimationStateData.AnimationPair(serializedEntry.from.Animation, serializedEntry.to.Animation), serializedEntry.transition.Animation);
			}
		}

		// Token: 0x06004DFD RID: 19965 RVA: 0x00214EEC File Offset: 0x002130EC
		public Animation GetTransition(Animation from, Animation to)
		{
			Animation result;
			this.dictionary.TryGetValue(new AnimationStateData.AnimationPair(from, to), out result);
			return result;
		}

		// Token: 0x04004D58 RID: 19800
		[SerializeField]
		private List<TransitionDictionaryExample.SerializedEntry> transitions = new List<TransitionDictionaryExample.SerializedEntry>();

		// Token: 0x04004D59 RID: 19801
		private readonly Dictionary<AnimationStateData.AnimationPair, Animation> dictionary = new Dictionary<AnimationStateData.AnimationPair, Animation>();

		// Token: 0x020015C3 RID: 5571
		[Serializable]
		public struct SerializedEntry
		{
			// Token: 0x04007060 RID: 28768
			public AnimationReferenceAsset from;

			// Token: 0x04007061 RID: 28769
			public AnimationReferenceAsset to;

			// Token: 0x04007062 RID: 28770
			public AnimationReferenceAsset transition;
		}
	}
}
