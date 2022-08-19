using System;
using System.Collections.Generic;
using UnityEngine;

namespace WXB
{
	// Token: 0x020006A1 RID: 1697
	internal static class FontMaterial
	{
		// Token: 0x06003582 RID: 13698 RVA: 0x00170E48 File Offset: 0x0016F048
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

		// Token: 0x04002F0B RID: 12043
		private static Dictionary<Font, Material> FontToMaterials = new Dictionary<Font, Material>();
	}
}
