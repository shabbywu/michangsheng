using System;
using System.Collections.Generic;
using UnityEngine;

namespace WXB
{
	// Token: 0x020006A0 RID: 1696
	internal static class SpriteMaterial
	{
		// Token: 0x06003580 RID: 13696 RVA: 0x00170DFC File Offset: 0x0016EFFC
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

		// Token: 0x04002F0A RID: 12042
		private static Dictionary<Texture, Material> SpriteToMaterials = new Dictionary<Texture, Material>();
	}
}
