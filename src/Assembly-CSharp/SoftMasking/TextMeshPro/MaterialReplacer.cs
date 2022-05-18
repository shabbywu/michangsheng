using System;
using UnityEngine;

namespace SoftMasking.TextMeshPro
{
	// Token: 0x02000A21 RID: 2593
	[GlobalMaterialReplacer]
	public class MaterialReplacer : IMaterialReplacer
	{
		// Token: 0x170007CF RID: 1999
		// (get) Token: 0x0600434C RID: 17228 RVA: 0x000251E2 File Offset: 0x000233E2
		public int order
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x0600434D RID: 17229 RVA: 0x001CC544 File Offset: 0x001CA744
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
