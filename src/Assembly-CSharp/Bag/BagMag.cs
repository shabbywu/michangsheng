using System;
using System.Collections.Generic;
using UnityEngine;

namespace Bag
{
	// Token: 0x02000D15 RID: 3349
	public class BagMag
	{
		// Token: 0x170007D9 RID: 2009
		// (get) Token: 0x06004FBE RID: 20414 RVA: 0x000396B5 File Offset: 0x000378B5
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

		// Token: 0x06004FBF RID: 20415 RVA: 0x00216C60 File Offset: 0x00214E60
		public BagMag()
		{
			this.QualityDict = ResManager.inst.LoadSpriteAtlas("Bag/QualityBg");
			this.QualityUpDict = ResManager.inst.LoadSpriteAtlas("Bag/QualityLine");
			this.JiaoBiaoDict = ResManager.inst.LoadSpriteAtlas("Bag/JiaoBiao");
		}

		// Token: 0x04005111 RID: 20753
		private static BagMag _inst;

		// Token: 0x04005112 RID: 20754
		public Dictionary<string, Sprite> QualityDict = new Dictionary<string, Sprite>();

		// Token: 0x04005113 RID: 20755
		public Dictionary<string, Sprite> QualityUpDict = new Dictionary<string, Sprite>();

		// Token: 0x04005114 RID: 20756
		public Dictionary<string, Sprite> JiaoBiaoDict = new Dictionary<string, Sprite>();
	}
}
