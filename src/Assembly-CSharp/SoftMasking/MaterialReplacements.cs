using System;
using System.Collections.Generic;
using UnityEngine;

namespace SoftMasking
{
	// Token: 0x02000A08 RID: 2568
	internal class MaterialReplacements
	{
		// Token: 0x06004287 RID: 17031 RVA: 0x0002F6DE File Offset: 0x0002D8DE
		public MaterialReplacements(IMaterialReplacer replacer, Action<Material> applyParameters)
		{
			this._replacer = replacer;
			this._applyParameters = applyParameters;
		}

		// Token: 0x06004288 RID: 17032 RVA: 0x001CACCC File Offset: 0x001C8ECC
		public Material Get(Material original)
		{
			for (int i = 0; i < this._overrides.Count; i++)
			{
				MaterialReplacements.MaterialOverride materialOverride = this._overrides[i];
				if (materialOverride.original == original)
				{
					Material material = materialOverride.Get();
					if (material)
					{
						material.CopyPropertiesFromMaterial(original);
						this._applyParameters(material);
					}
					return material;
				}
			}
			Material material2 = this._replacer.Replace(original);
			if (material2)
			{
				material2.hideFlags = 61;
				this._applyParameters(material2);
			}
			this._overrides.Add(new MaterialReplacements.MaterialOverride(original, material2));
			return material2;
		}

		// Token: 0x06004289 RID: 17033 RVA: 0x001CAD68 File Offset: 0x001C8F68
		public void Release(Material replacement)
		{
			for (int i = 0; i < this._overrides.Count; i++)
			{
				MaterialReplacements.MaterialOverride materialOverride = this._overrides[i];
				if (materialOverride.replacement == replacement && materialOverride.Release())
				{
					Object.DestroyImmediate(replacement);
					this._overrides.RemoveAt(i);
					return;
				}
			}
		}

		// Token: 0x0600428A RID: 17034 RVA: 0x001CADC4 File Offset: 0x001C8FC4
		public void ApplyAll()
		{
			for (int i = 0; i < this._overrides.Count; i++)
			{
				Material replacement = this._overrides[i].replacement;
				if (replacement)
				{
					this._applyParameters(replacement);
				}
			}
		}

		// Token: 0x0600428B RID: 17035 RVA: 0x001CAE10 File Offset: 0x001C9010
		public void DestroyAllAndClear()
		{
			for (int i = 0; i < this._overrides.Count; i++)
			{
				Object.DestroyImmediate(this._overrides[i].replacement);
			}
			this._overrides.Clear();
		}

		// Token: 0x04003AE5 RID: 15077
		private readonly IMaterialReplacer _replacer;

		// Token: 0x04003AE6 RID: 15078
		private readonly Action<Material> _applyParameters;

		// Token: 0x04003AE7 RID: 15079
		private readonly List<MaterialReplacements.MaterialOverride> _overrides = new List<MaterialReplacements.MaterialOverride>();

		// Token: 0x02000A09 RID: 2569
		private class MaterialOverride
		{
			// Token: 0x0600428C RID: 17036 RVA: 0x0002F6FF File Offset: 0x0002D8FF
			public MaterialOverride(Material original, Material replacement)
			{
				this.original = original;
				this.replacement = replacement;
				this._useCount = 1;
			}

			// Token: 0x170007AC RID: 1964
			// (get) Token: 0x0600428D RID: 17037 RVA: 0x0002F71C File Offset: 0x0002D91C
			// (set) Token: 0x0600428E RID: 17038 RVA: 0x0002F724 File Offset: 0x0002D924
			public Material original { get; private set; }

			// Token: 0x170007AD RID: 1965
			// (get) Token: 0x0600428F RID: 17039 RVA: 0x0002F72D File Offset: 0x0002D92D
			// (set) Token: 0x06004290 RID: 17040 RVA: 0x0002F735 File Offset: 0x0002D935
			public Material replacement { get; private set; }

			// Token: 0x06004291 RID: 17041 RVA: 0x0002F73E File Offset: 0x0002D93E
			public Material Get()
			{
				this._useCount++;
				return this.replacement;
			}

			// Token: 0x06004292 RID: 17042 RVA: 0x001CAE54 File Offset: 0x001C9054
			public bool Release()
			{
				int num = this._useCount - 1;
				this._useCount = num;
				return num == 0;
			}

			// Token: 0x04003AE8 RID: 15080
			private int _useCount;
		}
	}
}
