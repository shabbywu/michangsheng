using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SoftMasking
{
	// Token: 0x020006DE RID: 1758
	public class MaterialReplacerChain : IMaterialReplacer
	{
		// Token: 0x06003872 RID: 14450 RVA: 0x001836C8 File Offset: 0x001818C8
		public MaterialReplacerChain(IEnumerable<IMaterialReplacer> replacers, IMaterialReplacer yetAnother)
		{
			this._replacers = replacers.ToList<IMaterialReplacer>();
			this._replacers.Add(yetAnother);
			this.Initialize();
		}

		// Token: 0x17000583 RID: 1411
		// (get) Token: 0x06003873 RID: 14451 RVA: 0x001836EE File Offset: 0x001818EE
		// (set) Token: 0x06003874 RID: 14452 RVA: 0x001836F6 File Offset: 0x001818F6
		public int order { get; private set; }

		// Token: 0x06003875 RID: 14453 RVA: 0x00183700 File Offset: 0x00181900
		public Material Replace(Material material)
		{
			for (int i = 0; i < this._replacers.Count; i++)
			{
				Material material2 = this._replacers[i].Replace(material);
				if (material2 != null)
				{
					return material2;
				}
			}
			return null;
		}

		// Token: 0x06003876 RID: 14454 RVA: 0x00183744 File Offset: 0x00181944
		private void Initialize()
		{
			this._replacers.Sort((IMaterialReplacer a, IMaterialReplacer b) => a.order.CompareTo(b.order));
			this.order = this._replacers[0].order;
		}

		// Token: 0x040030D1 RID: 12497
		private readonly List<IMaterialReplacer> _replacers;
	}
}
