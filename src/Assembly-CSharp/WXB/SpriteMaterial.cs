using System;
using System.Collections.Generic;
using UnityEngine;

namespace WXB
{
	// Token: 0x020009B5 RID: 2485
	internal static class SpriteMaterial
	{
		// Token: 0x06003F41 RID: 16193 RVA: 0x001B8DD8 File Offset: 0x001B6FD8
		public static Material Get(Texture t)
		{
			Material material = null;
			if (SpriteMaterial.SpriteToMaterials.TryGetValue(t, out material))
			{
				return material;
			}
			material = new Material(Canvas.GetDefaultCanvasMaterial());
			material.mainTexture = t;
			SpriteMaterial.SpriteToMaterials.Add(t, material);
			return material;
		}

		// Token: 0x040038C5 RID: 14533
		private static Dictionary<Texture, Material> SpriteToMaterials = new Dictionary<Texture, Material>();
	}
}
