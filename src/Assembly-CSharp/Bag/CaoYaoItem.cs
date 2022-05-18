using System;
using GUIPackage;
using JSONClass;

namespace Bag
{
	// Token: 0x02000D24 RID: 3364
	[Serializable]
	public class CaoYaoItem : BaseItem
	{
		// Token: 0x06005014 RID: 20500 RVA: 0x0021887C File Offset: 0x00216A7C
		public override void SetItem(int id, int count)
		{
			base.SetItem(id, count);
			this.YaoYin = _ItemJsonData.DataDict[id].yaoZhi1;
			this.ZhuYao = _ItemJsonData.DataDict[id].yaoZhi2;
			this.FuYao = _ItemJsonData.DataDict[id].yaoZhi3;
		}

		// Token: 0x06005015 RID: 20501 RVA: 0x00039A96 File Offset: 0x00037C96
		public string GetZhuYao()
		{
			if (Tools.instance.getPlayer().GetHasZhuYaoShuXin(this.Id, this.Quality))
			{
				return Tools.getLiDanLeiXinStr(this.ZhuYao);
			}
			return "未知";
		}

		// Token: 0x06005016 RID: 20502 RVA: 0x00039AC6 File Offset: 0x00037CC6
		public string GetFuYao()
		{
			if (Tools.instance.getPlayer().GetHasFuYaoShuXin(this.Id, this.Quality))
			{
				return Tools.getLiDanLeiXinStr(this.FuYao);
			}
			return "未知";
		}

		// Token: 0x06005017 RID: 20503 RVA: 0x00039AF6 File Offset: 0x00037CF6
		public string GetYaoYin()
		{
			if (Tools.instance.getPlayer().GetHasYaoYinShuXin(this.Id, this.Quality))
			{
				return Tools.getLiDanLeiXinStr(this.YaoYin);
			}
			return "未知";
		}

		// Token: 0x06005018 RID: 20504 RVA: 0x00039B26 File Offset: 0x00037D26
		public int GetZhuYaoId()
		{
			if (Tools.instance.getPlayer().GetHasZhuYaoShuXin(this.Id, this.Quality))
			{
				return this.ZhuYao;
			}
			return -1;
		}

		// Token: 0x06005019 RID: 20505 RVA: 0x00039B4D File Offset: 0x00037D4D
		public int GetFuYaoId()
		{
			if (Tools.instance.getPlayer().GetHasFuYaoShuXin(this.Id, this.Quality))
			{
				return this.FuYao;
			}
			return -1;
		}

		// Token: 0x0600501A RID: 20506 RVA: 0x00039B74 File Offset: 0x00037D74
		public int GetYaoYinId()
		{
			if (Tools.instance.getPlayer().GetHasYaoYinShuXin(this.Id, this.Quality))
			{
				return this.YaoYin;
			}
			return -1;
		}

		// Token: 0x0600501B RID: 20507 RVA: 0x00039B9B File Offset: 0x00037D9B
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

		// Token: 0x0400515C RID: 20828
		public int YaoYin;

		// Token: 0x0400515D RID: 20829
		public int ZhuYao;

		// Token: 0x0400515E RID: 20830
		public int FuYao;
	}
}
