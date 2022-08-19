using System;
using System.Collections.Generic;
using UnityEngine;

namespace SoftMasking
{
	// Token: 0x020006DA RID: 1754
	internal class MaterialReplacements
	{
		// Token: 0x06003865 RID: 14437 RVA: 0x00183374 File Offset: 0x00181574
		public MaterialReplacements(IMaterialReplacer replacer, Action<Material> applyParameters)
		{
			this._replacer = replacer;
			this._applyParameters = applyParameters;
		}

		// Token: 0x06003866 RID: 14438 RVA: 0x00183398 File Offset: 0x00181598
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

		// Token: 0x06003867 RID: 14439 RVA: 0x00183434 File Offset: 0x00181634
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

		// Token: 0x06003868 RID: 14440 RVA: 0x00183490 File Offset: 0x00181690
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

		// Token: 0x06003869 RID: 14441 RVA: 0x001834DC File Offset: 0x001816DC
		public void DestroyAllAndClear()
		{
			for (int i = 0; i < this._overrides.Count; i++)
			{
				Object.DestroyImmediate(this._overrides[i].replacement);
			}
			this._overrides.Clear();
		}

		// Token: 0x040030CD RID: 12493
		private readonly IMaterialReplacer _replacer;

		// Token: 0x040030CE RID: 12494
		private readonly Action<Material> _applyParameters;

		// Token: 0x040030CF RID: 12495
		private readonly List<MaterialReplacements.MaterialOverride> _overrides = new List<MaterialReplacements.MaterialOverride>();

		// Token: 0x02001514 RID: 5396
		private class MaterialOverride
		{
			// Token: 0x060082F6 RID: 33526 RVA: 0x002DD494 File Offset: 0x002DB694
			public MaterialOverride(Material original, Material replacement)
			{
				this.original = original;
				this.replacement = replacement;
				this._useCount = 1;
			}

			// Token: 0x17000B32 RID: 2866
			// (get) Token: 0x060082F7 RID: 33527 RVA: 0x002DD4B1 File Offset: 0x002DB6B1
			// (set) Token: 0x060082F8 RID: 33528 RVA: 0x002DD4B9 File Offset: 0x002DB6B9
			public Material original { get; private set; }

			// Token: 0x17000B33 RID: 2867
			// (get) Token: 0x060082F9 RID: 33529 RVA: 0x002DD4C2 File Offset: 0x002DB6C2
			// (set) Token: 0x060082FA RID: 33530 RVA: 0x002DD4CA File Offset: 0x002DB6CA
			public Material replacement { get; private set; }

			// Token: 0x060082FB RID: 33531 RVA: 0x002DD4D3 File Offset: 0x002DB6D3
			public Material Get()
			{
				this._useCount++;
				return this.replacement;
			}

			// Token: 0x060082FC RID: 33532 RVA: 0x002DD4EC File Offset: 0x002DB6EC
			public bool Release()
			{
				int num = this._useCount - 1;
				this._useCount = num;
				return num == 0;
			}

			// Token: 0x04006E5B RID: 28251
			private int _useCount;
		}
	}
}
