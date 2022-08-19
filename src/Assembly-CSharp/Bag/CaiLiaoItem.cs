using System;
using JSONClass;
using UnityEngine;

namespace Bag
{
	// Token: 0x0200099E RID: 2462
	[Serializable]
	public class CaiLiaoItem : BaseItem
	{
		// Token: 0x060044B2 RID: 17586 RVA: 0x001D3D8F File Offset: 0x001D1F8F
		public override void SetItem(int id, int count)
		{
			base.SetItem(id, count);
			this.WuWeiType = _ItemJsonData.DataDict[id].WuWeiType;
			this.ShuXingType = _ItemJsonData.DataDict[id].ShuXingType;
		}

		// Token: 0x060044B3 RID: 17587 RVA: 0x001D3DC5 File Offset: 0x001D1FC5
		public string GetZhongLei()
		{
			return LianQiWuWeiBiao.DataDict[this.WuWeiType].desc;
		}

		// Token: 0x060044B4 RID: 17588 RVA: 0x001D3DDC File Offset: 0x001D1FDC
		public string GetShuXing()
		{
			return LianQiShuXinLeiBie.DataDict[this.ShuXingType].desc;
		}

		// Token: 0x060044B5 RID: 17589 RVA: 0x001D3DF3 File Offset: 0x001D1FF3
		public LianQiCaiLiaoYinYang GetYinYang()
		{
			if (this.ShuXingType % 10 == 1)
			{
				return LianQiCaiLiaoYinYang.阳;
			}
			return LianQiCaiLiaoYinYang.阴;
		}

		// Token: 0x060044B6 RID: 17590 RVA: 0x001D3E04 File Offset: 0x001D2004
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

		// Token: 0x0400465C RID: 18012
		public int WuWeiType;

		// Token: 0x0400465D RID: 18013
		public int ShuXingType;
	}
}
