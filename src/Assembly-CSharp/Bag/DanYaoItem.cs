using System;
using GUIPackage;
using JSONClass;

namespace Bag
{
	// Token: 0x02000D25 RID: 3365
	[Serializable]
	public class DanYaoItem : BaseItem
	{
		// Token: 0x0600501E RID: 20510 RVA: 0x002188D4 File Offset: 0x00216AD4
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

		// Token: 0x0600501F RID: 20511 RVA: 0x00039BFA File Offset: 0x00037DFA
		public override JiaoBiaoType GetJiaoBiaoType()
		{
			if (Tools.getJsonobject(Tools.instance.getPlayer().NaiYaoXin, string.Concat(this.Id)) >= this.CanUse && this.Type == 5)
			{
				return JiaoBiaoType.耐;
			}
			return base.GetJiaoBiaoType();
		}

		// Token: 0x06005020 RID: 20512 RVA: 0x0021893C File Offset: 0x00216B3C
		public int GetDanDu()
		{
			int num = _ItemJsonData.DataDict[this.Id].DanDu;
			if (_ItemJsonData.DataDict[this.Id].seid.Contains(29) && (int)Tools.instance.getPlayer().level < ItemsSeidJsonData29.DataDict[this.Id].value1)
			{
				num += ItemsSeidJsonData29.DataDict[this.Id].value2;
			}
			return num;
		}

		// Token: 0x06005021 RID: 20513 RVA: 0x00039C39 File Offset: 0x00037E39
		public int GetHasUse()
		{
			return Tools.getJsonobject(Tools.instance.getPlayer().NaiYaoXin, this.Id.ToString());
		}

		// Token: 0x06005022 RID: 20514 RVA: 0x00039C5A File Offset: 0x00037E5A
		public int GetMaxUseNum()
		{
			return item.GetItemCanUseNum(this.Id);
		}

		// Token: 0x06005023 RID: 20515 RVA: 0x00039C67 File Offset: 0x00037E67
		public override void Use()
		{
			new item(this.Id).gongneng(delegate
			{
				Tools.instance.getPlayer().removeItem(this.Id, 1);
				MessageMag.Instance.Send(MessageName.MSG_PLAYER_USE_ITEM, null);
			}, false);
		}

		// Token: 0x0400515F RID: 20831
		public int CanUse;

		// Token: 0x04005160 RID: 20832
		public int DanDu;
	}
}
