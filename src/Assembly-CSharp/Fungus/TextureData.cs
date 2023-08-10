using System;
using UnityEngine;

namespace Fungus;

[Serializable]
public struct TextureData
{
	[SerializeField]
	[VariableProperty("<Value>", new Type[] { typeof(TextureVariable) })]
	public TextureVariable textureRef;

	[SerializeField]
	public Texture textureVal;

	public Texture Value
	{
		get
		{
			if (!((Object)(object)textureRef == (Object)null))
			{
				return textureRef.Value;
			}
			return textureVal;
		}
		set
		{
			if ((Object)(object)textureRef == (Object)null)
			{
				textureVal = value;
			}
			else
			{
				textureRef.Value = value;
			}
		}
	}

	public TextureData(Texture v)
	{
		textureVal = v;
		textureRef = null;
	}

	public static implicit operator Texture(TextureData textureData)
	{
		return textureData.Value;
	}

	public string GetDescription()
	{
		if ((Object)(object)textureRef == (Object)null)
		{
			return ((object)textureVal).ToString();
		}
		return textureRef.Key;
	}
}
