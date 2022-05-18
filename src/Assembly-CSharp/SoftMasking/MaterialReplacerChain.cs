using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SoftMasking
{
	// Token: 0x02000A0E RID: 2574
	public class MaterialReplacerChain : IMaterialReplacer
	{
		// Token: 0x060042A2 RID: 17058 RVA: 0x0002F7DE File Offset: 0x0002D9DE
		public MaterialReplacerChain(IEnumerable<IMaterialReplacer> replacers, IMaterialReplacer yetAnother)
		{
			this._replacers = replacers.ToList<IMaterialReplacer>();
			this._replacers.Add(yetAnother);
			this.Initialize();
		}

		// Token: 0x170007B0 RID: 1968
		// (get) Token: 0x060042A3 RID: 17059 RVA: 0x0002F804 File Offset: 0x0002DA04
		// (set) Token: 0x060042A4 RID: 17060 RVA: 0x0002F80C File Offset: 0x0002DA0C
		public int order { get; private set; }

		// Token: 0x060042A5 RID: 17061 RVA: 0x001CAFC4 File Offset: 0x001C91C4
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

		// Token: 0x060042A6 RID: 17062 RVA: 0x001CB008 File Offset: 0x001C9208
		private void Initialize()
		{
			this._replacers.Sort((IMaterialReplacer a, IMaterialReplacer b) => a.order.CompareTo(b.order));
			this.order = this._replacers[0].order;
		}

		// Token: 0x04003AF2 RID: 15090
		private readonly List<IMaterialReplacer> _replacers;
	}
}
