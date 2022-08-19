using System;
using GUIPackage;
using JSONClass;

namespace Bag
{
	// Token: 0x020009A0 RID: 2464
	[Serializable]
	public class DanYaoItem : BaseItem
	{
		// Token: 0x060044C2 RID: 17602 RVA: 0x001D4040 File Offset: 0x001D2240
		public override void SetItem(int id, int count)
		{
			base.SetItem(id, count);
			this.CanUse = _ItemJsonData.DataDict[id].CanUse;
			if (Tools.instance.getPlayer().checkHasStudyWuDaoSkillByID(2131))
			{
				this.CanUse *= 2;
			}
			this.DanDu = _ItemJsonData.DataDict[id].DanDu;
		}

		// Token: 0x060044C3 RID: 17603 RVA: 0x001D40A5 File Offset: 0x001D22A5
		public override JiaoBiaoType GetJiaoBiaoType()
		{
			if (Tools.getJsonobject(Tools.instance.getPlayer().NaiYaoXin, string.Concat(this.Id)) >= this.CanUse && this.Type == 5)
			{
				return JiaoBiaoType.耐;
			}
			return base.GetJiaoBiaoType();
		}

		// Token: 0x060044C4 RID: 17604 RVA: 0x001D40E4 File Offset: 0x001D22E4
		public int GetDanDu()
		{
			int num = _ItemJsonData.DataDict[this.Id].DanDu;
			if (_ItemJsonData.DataDict[this.Id].seid.Contains(29) && (int)Tools.instance.getPlayer().level < ItemsSeidJsonData29.DataDict[this.Id].value1)
			{
				num += ItemsSeidJsonData29.DataDict[this.Id].value2;
			}
			return num;
		}

		// Token: 0x060044C5 RID: 17605 RVA: 0x001D4164 File Offset: 0x001D2364
		public int GetHasUse()
		{
			return Tools.getJsonobject(Tools.instance.getPlayer().NaiYaoXin, this.Id.ToString());
		}

		// Token: 0x060044C6 RID: 17606 RVA: 0x001D4185 File Offset: 0x001D2385
		public int GetMaxUseNum()
		{
			return item.GetItemCanUseNum(this.Id);
		}

		// Token: 0x060044C7 RID: 17607 RVA: 0x001D4192 File Offset: 0x001D2392
		public override void Use()
		{
			new item(this.Id).gongneng(delegate
			{
				Tools.instance.getPlayer().removeItem(this.Id, 1);
				MessageMag.Instance.Send(MessageName.MSG_PLAYER_USE_ITEM, null);
			}, false);
		}

		// Token: 0x04004661 RID: 18017
		public int CanUse;

		// Token: 0x04004662 RID: 18018
		public int DanDu;
	}
}
