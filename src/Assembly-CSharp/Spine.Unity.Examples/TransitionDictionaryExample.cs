using System;
using System.Collections.Generic;
using UnityEngine;

namespace Spine.Unity.Examples;

public sealed class TransitionDictionaryExample : MonoBehaviour
{
	[Serializable]
	public struct SerializedEntry
	{
		public AnimationReferenceAsset from;

		public AnimationReferenceAsset to;

		public AnimationReferenceAsset transition;
	}

	[SerializeField]
	private List<SerializedEntry> transitions = new List<SerializedEntry>();

	private readonly Dictionary<AnimationPair, Animation> dictionary = new Dictionary<AnimationPair, Animation>();

	private void Start()
	{
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		dictionary.Clear();
		foreach (SerializedEntry transition in transitions)
		{
			dictionary.Add(new AnimationPair(transition.from.Animation, transition.to.Animation), transition.transition.Animation);
		}
	}

	public Animation GetTransition(Animation from, Animation to)
	{
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		dictionary.TryGetValue(new AnimationPair(from, to), out var value);
		return value;
	}
}
