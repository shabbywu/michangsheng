using System;
using UnityEngine;

namespace Fungus;

[Serializable]
public struct AudioSourceData
{
	[SerializeField]
	[VariableProperty("<Value>", new Type[] { typeof(AudioSourceVariable) })]
	public AudioSourceVariable audioSourceRef;

	[SerializeField]
	public AudioSource audioSourceVal;

	public AudioSource Value
	{
		get
		{
			if (!((Object)(object)audioSourceRef == (Object)null))
			{
				return audioSourceRef.Value;
			}
			return audioSourceVal;
		}
		set
		{
			if ((Object)(object)audioSourceRef == (Object)null)
			{
				audioSourceVal = value;
			}
			else
			{
				audioSourceRef.Value = value;
			}
		}
	}

	public static implicit operator AudioSource(AudioSourceData audioSourceData)
	{
		return audioSourceData.Value;
	}

	public AudioSourceData(AudioSource v)
	{
		audioSourceVal = v;
		audioSourceRef = null;
	}

	public string GetDescription()
	{
		if ((Object)(object)audioSourceRef == (Object)null)
		{
			return ((object)audioSourceVal).ToString();
		}
		return audioSourceRef.Key;
	}
}
