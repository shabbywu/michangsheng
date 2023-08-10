using System;
using UnityEngine;

namespace Fungus;

[Serializable]
public struct SpriteData
{
	[SerializeField]
	[VariableProperty("<Value>", new Type[] { typeof(SpriteVariable) })]
	public SpriteVariable spriteRef;

	[SerializeField]
	public Sprite spriteVal;

	public Sprite Value
	{
		get
		{
			if (!((Object)(object)spriteRef == (Object)null))
			{
				return spriteRef.Value;
			}
			return spriteVal;
		}
		set
		{
			if ((Object)(object)spriteRef == (Object)null)
			{
				spriteVal = value;
			}
			else
			{
				spriteRef.Value = value;
			}
		}
	}

	public SpriteData(Sprite v)
	{
		spriteVal = v;
		spriteRef = null;
	}

	public static implicit operator Sprite(SpriteData spriteData)
	{
		return spriteData.Value;
	}

	public string GetDescription()
	{
		if ((Object)(object)spriteRef == (Object)null)
		{
			return ((object)spriteVal).ToString();
		}
		return spriteRef.Key;
	}
}
