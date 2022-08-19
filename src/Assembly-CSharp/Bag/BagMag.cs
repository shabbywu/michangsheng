using System;
using System.Collections.Generic;
using UnityEngine;

namespace Bag
{
	// Token: 0x02000995 RID: 2453
	public class BagMag
	{
		// Token: 0x170005A6 RID: 1446
		// (get) Token: 0x06004468 RID: 17512 RVA: 0x001D1F07 File Offset: 0x001D0107
		public static BagMag Inst
		{
			get
			{
				if (BagMag._inst == null)
				{
					BagMag._inst = new BagMag();
				}
				return BagMag._inst;
			}
		}

		// Token: 0x06004469 RID: 17513 RVA: 0x001D1F20 File Offset: 0x001D0120
		public BagMag()
		{
			this.QualityDict = ResManager.inst.LoadSpriteAtlas("Bag/QualityBg");
			this.QualityUpDict = ResManager.inst.LoadSpriteAtlas("Bag/QualityLine");
			this.JiaoBiaoDict = ResManager.inst.LoadSpriteAtlas("Bag/JiaoBiao");
		}

		// Token: 0x0400461D RID: 17949
		private static BagMag _inst;

		// Token: 0x0400461E RID: 17950
		public Dictionary<string, Sprite> QualityDict = new Dictionary<string, Sprite>();

		// Token: 0x0400461F RID: 17951
		public Dictionary<string, Sprite> QualityUpDict = new Dictionary<string, Sprite>();

		// Token: 0x04004620 RID: 17952
		public Dictionary<string, Sprite> JiaoBiaoDict = new Dictionary<string, Sprite>();
	}
}
