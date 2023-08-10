using System.Collections.Generic;
using UnityEngine;

namespace WXB;

internal static class SpriteMaterial
{
	private static Dictionary<Texture, Material> SpriteToMaterials = new Dictionary<Texture, Material>();

	public static Material Get(Texture t)
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Expected O, but got Unknown
		Material value = null;
		if (SpriteToMaterials.TryGetValue(t, out value))
		{
			return value;
		}
		value = new Material(Canvas.GetDefaultCanvasMaterial());
		value.mainTexture = t;
		SpriteToMaterials.Add(t, value);
		return value;
	}
}
