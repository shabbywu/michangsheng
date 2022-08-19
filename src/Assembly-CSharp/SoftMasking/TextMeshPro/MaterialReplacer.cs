using System;
using UnityEngine;

namespace SoftMasking.TextMeshPro
{
	// Token: 0x020006E2 RID: 1762
	[GlobalMaterialReplacer]
	public class MaterialReplacer : IMaterialReplacer
	{
		// Token: 0x1700059D RID: 1437
		// (get) Token: 0x060038E5 RID: 14565 RVA: 0x00142574 File Offset: 0x00140774
		public int order
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x060038E6 RID: 14566 RVA: 0x001849C8 File Offset: 0x00182BC8
		public Material Replace(Material material)
		{
			if (material && material.shader && material.shader.name.StartsWith("TextMeshPro/"))
			{
				Shader shader = Shader.Find("Soft Mask/" + material.shader.name);
				if (shader)
				{
					Material material2 = new Material(shader);
					material2.CopyPropertiesFromMaterial(material);
					return material2;
				}
			}
			return null;
		}
	}
}
