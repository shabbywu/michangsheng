using System;
using System.Collections.Generic;
using UnityEngine;

namespace WXB
{
	// Token: 0x020009B6 RID: 2486
	internal static class FontMaterial
	{
		// Token: 0x06003F43 RID: 16195 RVA: 0x001B8E18 File Offset: 0x001B7018
		public static Material Get(Font f)
		{
			Material material = null;
			if (FontMaterial.FontToMaterials.TryGetValue(f, out material))
			{
				if (!(material == null))
				{
					return material;
				}
				FontMaterial.FontToMaterials.Remove(f);
			}
			material = new Material(Shader.Find("UI/Unlit/Text Detail"));
			material.mainTexture = f.material.mainTexture;
			FontMaterial.FontToMaterials.Add(f, material);
			return material;
		}

		// Token: 0x040038C6 RID: 14534
		private static Dictionary<Font, Material> FontToMaterials = new Dictionary<Font, Material>();
	}
}
