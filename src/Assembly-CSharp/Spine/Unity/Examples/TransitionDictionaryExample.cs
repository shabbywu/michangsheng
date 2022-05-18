using System;
using System.Collections.Generic;
using UnityEngine;

namespace Spine.Unity.Examples
{
	// Token: 0x02000E31 RID: 3633
	public sealed class TransitionDictionaryExample : MonoBehaviour
	{
		// Token: 0x06005778 RID: 22392 RVA: 0x00245038 File Offset: 0x00243238
		private void Start()
		{
			this.dictionary.Clear();
			foreach (TransitionDictionaryExample.SerializedEntry serializedEntry in this.transitions)
			{
				this.dictionary.Add(new AnimationStateData.AnimationPair(serializedEntry.from.Animation, serializedEntry.to.Animation), serializedEntry.transition.Animation);
			}
		}

		// Token: 0x06005779 RID: 22393 RVA: 0x002450C0 File Offset: 0x002432C0
		public Animation GetTransition(Animation from, Animation to)
		{
			Animation result;
			this.dictionary.TryGetValue(new AnimationStateData.AnimationPair(from, to), out result);
			return result;
		}

		// Token: 0x04005761 RID: 22369
		[SerializeField]
		private List<TransitionDictionaryExample.SerializedEntry> transitions = new List<TransitionDictionaryExample.SerializedEntry>();

		// Token: 0x04005762 RID: 22370
		private readonly Dictionary<AnimationStateData.AnimationPair, Animation> dictionary = new Dictionary<AnimationStateData.AnimationPair, Animation>();

		// Token: 0x02000E32 RID: 3634
		[Serializable]
		public struct SerializedEntry
		{
			// Token: 0x04005763 RID: 22371
			public AnimationReferenceAsset from;

			// Token: 0x04005764 RID: 22372
			public AnimationReferenceAsset to;

			// Token: 0x04005765 RID: 22373
			public AnimationReferenceAsset transition;
		}
	}
}
