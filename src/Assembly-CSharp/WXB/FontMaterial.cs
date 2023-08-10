using System.Collections.Generic;
using UnityEngine;

namespace WXB;

internal static class FontMaterial
{
	private static Dictionary<Font, Material> FontToMaterials = new Dictionary<Font, Material>();

	public static Material Get(Font f)
	{
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Expected O, but got Unknown
		Material value = null;
		if (FontToMaterials.TryGetValue(f, out value))
		{
			if (!((Object)(object)value == (Object)null))
			{
				return value;
			}
			FontToMaterials.Remove(f);
		}
		value = new Material(Shader.Find("UI/Unlit/Text Detail"));
		value.mainTexture = f.material.mainTexture;
		FontToMaterials.Add(f, value);
		return value;
	}
}
