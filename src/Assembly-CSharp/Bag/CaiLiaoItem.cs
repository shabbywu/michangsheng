using System;
using JSONClass;
using UnityEngine;

namespace Bag
{
	// Token: 0x02000D23 RID: 3363
	[Serializable]
	public class CaiLiaoItem : BaseItem
	{
		// Token: 0x0600500E RID: 20494 RVA: 0x00039A19 File Offset: 0x00037C19
		public override void SetItem(int id, int count)
		{
			base.SetItem(id, count);
			this.WuWeiType = _ItemJsonData.DataDict[id].WuWeiType;
			this.ShuXingType = _ItemJsonData.DataDict[id].ShuXingType;
		}

		// Token: 0x0600500F RID: 20495 RVA: 0x00039A4F File Offset: 0x00037C4F
		public string GetZhongLei()
		{
			return LianQiWuWeiBiao.DataDict[this.WuWeiType].desc;
		}

		// Token: 0x06005010 RID: 20496 RVA: 0x00039A66 File Offset: 0x00037C66
		public string GetShuXing()
		{
			return LianQiShuXinLeiBie.DataDict[this.ShuXingType].desc;
		}

		// Token: 0x06005011 RID: 20497 RVA: 0x00039A7D File Offset: 0x00037C7D
		public LianQiCaiLiaoYinYang GetYinYang()
		{
			if (this.ShuXingType % 10 == 1)
			{
				return LianQiCaiLiaoYinYang.阳;
			}
			return LianQiCaiLiaoYinYang.阴;
		}

		// Token: 0x06005012 RID: 20498 RVA: 0x00218804 File Offset: 0x00216A04
		public LianQiCaiLiaoType GetLianQiCaiLiaoType()
		{
			switch (this.ShuXingType / 10)
			{
			case 0:
				return LianQiCaiLiaoType.金;
			case 1:
				return LianQiCaiLiaoType.木;
			case 2:
				return LianQiCaiLiaoType.水;
			case 3:
				return LianQiCaiLiaoType.火;
			case 4:
				return LianQiCaiLiaoType.土;
			case 5:
				return LianQiCaiLiaoType.混元;
			case 6:
				return LianQiCaiLiaoType.神念;
			case 7:
				return LianQiCaiLiaoType.剑;
			default:
				Debug.LogError(string.Format("不存在的材料属性{0},itemId{1}", this.ShuXingType, this.Id));
				return LianQiCaiLiaoType.全部;
			}
		}

		// Token: 0x0400515A RID: 20826
		public int WuWeiType;

		// Token: 0x0400515B RID: 20827
		public int ShuXingType;
	}
}
