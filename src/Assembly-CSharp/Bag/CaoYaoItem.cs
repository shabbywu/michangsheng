using System;
using GUIPackage;
using JSONClass;

namespace Bag
{
	// Token: 0x0200099F RID: 2463
	[Serializable]
	public class CaoYaoItem : BaseItem
	{
		// Token: 0x060044B8 RID: 17592 RVA: 0x001D3E84 File Offset: 0x001D2084
		public override void SetItem(int id, int count)
		{
			base.SetItem(id, count);
			this.YaoYin = _ItemJsonData.DataDict[id].yaoZhi1;
			this.ZhuYao = _ItemJsonData.DataDict[id].yaoZhi2;
			this.FuYao = _ItemJsonData.DataDict[id].yaoZhi3;
		}

		// Token: 0x060044B9 RID: 17593 RVA: 0x001D3EDB File Offset: 0x001D20DB
		public string GetZhuYao()
		{
			if (Tools.instance.getPlayer().GetHasZhuYaoShuXin(this.Id, this.Quality))
			{
				return Tools.getLiDanLeiXinStr(this.ZhuYao);
			}
			return "未知";
		}

		// Token: 0x060044BA RID: 17594 RVA: 0x001D3F0B File Offset: 0x001D210B
		public string GetFuYao()
		{
			if (Tools.instance.getPlayer().GetHasFuYaoShuXin(this.Id, this.Quality))
			{
				return Tools.getLiDanLeiXinStr(this.FuYao);
			}
			return "未知";
		}

		// Token: 0x060044BB RID: 17595 RVA: 0x001D3F3B File Offset: 0x001D213B
		public string GetYaoYin()
		{
			if (Tools.instance.getPlayer().GetHasYaoYinShuXin(this.Id, this.Quality))
			{
				return Tools.getLiDanLeiXinStr(this.YaoYin);
			}
			return "未知";
		}

		// Token: 0x060044BC RID: 17596 RVA: 0x001D3F6B File Offset: 0x001D216B
		public int GetZhuYaoId()
		{
			if (Tools.instance.getPlayer().GetHasZhuYaoShuXin(this.Id, this.Quality))
			{
				return this.ZhuYao;
			}
			return -1;
		}

		// Token: 0x060044BD RID: 17597 RVA: 0x001D3F92 File Offset: 0x001D2192
		public int GetFuYaoId()
		{
			if (Tools.instance.getPlayer().GetHasFuYaoShuXin(this.Id, this.Quality))
			{
				return this.FuYao;
			}
			return -1;
		}

		// Token: 0x060044BE RID: 17598 RVA: 0x001D3FB9 File Offset: 0x001D21B9
		public int GetYaoYinId()
		{
			if (Tools.instance.getPlayer().GetHasYaoYinShuXin(this.Id, this.Quality))
			{
				return this.YaoYin;
			}
			return -1;
		}

		// Token: 0x060044BF RID: 17599 RVA: 0x001D3FE0 File Offset: 0x001D21E0
		public override void Use()
		{
			if (_ItemJsonData.DataDict[this.Id].vagueType == 1)
			{
				new item(this.Id).gongneng(delegate
				{
					Tools.instance.getPlayer().removeItem(this.Id, 1);
					MessageMag.Instance.Send(MessageName.MSG_PLAYER_USE_ITEM, null);
				}, false);
			}
		}

		// Token: 0x0400465E RID: 18014
		public int YaoYin;

		// Token: 0x0400465F RID: 18015
		public int ZhuYao;

		// Token: 0x04004660 RID: 18016
		public int FuYao;
	}
}
