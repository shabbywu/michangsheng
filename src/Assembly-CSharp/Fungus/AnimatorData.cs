using System;
using UnityEngine;

namespace Fungus;

[Serializable]
public struct AnimatorData
{
	[SerializeField]
	[VariableProperty("<Value>", new Type[] { typeof(AnimatorVariable) })]
	public AnimatorVariable animatorRef;

	[SerializeField]
	public Animator animatorVal;

	public Animator Value
	{
		get
		{
			if (!((Object)(object)animatorRef == (Object)null))
			{
				return animatorRef.Value;
			}
			return animatorVal;
		}
		set
		{
			if ((Object)(object)animatorRef == (Object)null)
			{
				animatorVal = value;
			}
			else
			{
				animatorRef.Value = value;
			}
		}
	}

	public static implicit operator Animator(AnimatorData animatorData)
	{
		return animatorData.Value;
	}

	public AnimatorData(Animator v)
	{
		animatorVal = v;
		animatorRef = null;
	}

	public string GetDescription()
	{
		if ((Object)(object)animatorRef == (Object)null)
		{
			return ((object)animatorVal).ToString();
		}
		return animatorRef.Key;
	}
}
