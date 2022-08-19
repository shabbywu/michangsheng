using System;
using System.Collections.Generic;
using JSONClass;
using UnityEngine;
using UnityEngine.UI;

namespace Tab
{
	// Token: 0x02000704 RID: 1796
	[Serializable]
	public class TabWuDaoPanel : ITabPanelBase
	{
		// Token: 0x1700059E RID: 1438
		// (get) Token: 0x0600399B RID: 14747 RVA: 0x0018A4CE File Offset: 0x001886CE
		// (set) Token: 0x0600399C RID: 14748 RVA: 0x0018A4D6 File Offset: 0x001886D6
		public Dictionary<int, GameObject> WudaoSkillListDict { get; private set; }

		// Token: 0x1700059F RID: 1439
		// (get) Token: 0x0600399D RID: 14749 RVA: 0x0018A4DF File Offset: 0x001886DF
		// (set) Token: 0x0600399E RID: 14750 RVA: 0x0018A4E7 File Offset: 0x001886E7
		public Dictionary<string, Sprite> WudaoBgImgDict { get; private set; }

		// Token: 0x0600399F RID: 14751 RVA: 0x0018A4F0 File Offset: 0x001886F0
		public TabWuDaoPanel(GameObject gameObject)
		{
			this._go = gameObject;
			this._isInit = false;
		}

		// Token: 0x060039A0 RID: 14752 RVA: 0x0018A508 File Offset: 0x00188708
		private void Init()
		{
			this._wudaoImgDict = ResManager.inst.LoadSpriteAtlas("NewTab/WuDaoType");
			this.WudaoBgImgDict = ResManager.inst.LoadSpriteAtlas("NewTab/WuDaoBgImg");
			this.WudaoSkillListDict = new Dictionary<int, GameObject>();
			this.WuDaoTooltip = new WuDaoTooltip(base.Get("WuDaoSkillTooltip", true));
			this._curWuDaoDian = base.Get<Text>("Clouds/悟道点/wudaodian");
			this._curSiXuNum = base.Get<Text>("Clouds/思绪/sixu");
			for (int i = 1; i <= 4; i++)
			{
				this.WudaoSkillListDict.Add(i, base.Get(string.Format("WuDaoSkill/{0}", i), true));
			}
			GameObject obj = base.Get("WuDaoSelect/List/ViewPort/Content/WuDaoToggle", true);
			Transform transform = base.Get("WuDaoSelect/List/ViewPort/Content", true).transform;
			this._wudaoSelectList = new List<TabWuDaoToggle>();
			foreach (WuDaoAllTypeJson wuDaoAllTypeJson in WuDaoAllTypeJson.DataList)
			{
				this._wudaoSelectList.Add(new TabWuDaoToggle(obj.Inst(transform), wuDaoAllTypeJson.id, this._wudaoImgDict[wuDaoAllTypeJson.id.ToString()]));
			}
			this.SelectTypeCallBack(this._wudaoSelectList[0]);
		}

		// Token: 0x060039A1 RID: 14753 RVA: 0x0018A664 File Offset: 0x00188864
		public void SelectTypeCallBack(TabWuDaoToggle toggle)
		{
			if (this._curSelectType != null)
			{
				this._curSelectType.SetIsActive(false);
			}
			this._curSelectType = toggle;
			this._curSelectType.SetIsActive(true);
			foreach (TabWuDaoToggle tabWuDaoToggle in this._wudaoSelectList)
			{
				tabWuDaoToggle.UpdateUI();
			}
			if (this._wuDaoLevel == null)
			{
				this._wuDaoLevel = new TabWuDaoLevel(base.Get("WuDaoLevel", true), this._curSelectType.Id);
				return;
			}
			this._wuDaoLevel.UpdateUI(this._curSelectType.Id);
		}

		// Token: 0x060039A2 RID: 14754 RVA: 0x0018A71C File Offset: 0x0018891C
		public void UpdateWuDaoDian()
		{
			this._curWuDaoDian.text = Tools.instance.getPlayer().wuDaoMag.GetNowWuDaoDian().ToString();
		}

		// Token: 0x060039A3 RID: 14755 RVA: 0x0018A750 File Offset: 0x00188950
		public void UpdateUI()
		{
			this.UpdateWuDaoDian();
			this._curSiXuNum.text = Tools.instance.getPlayer().LingGuang.Count.ToString();
			if (this._curSelectType != null)
			{
				this._wuDaoLevel.UpdateUI(this._curSelectType.Id);
			}
		}

		// Token: 0x060039A4 RID: 14756 RVA: 0x0018A7A8 File Offset: 0x001889A8
		public override void Show()
		{
			if (!this._isInit)
			{
				this.Init();
				this._isInit = true;
			}
			this.UpdateUI();
			this._go.SetActive(true);
		}

		// Token: 0x040031B5 RID: 12725
		private bool _isInit;

		// Token: 0x040031B6 RID: 12726
		private List<TabWuDaoToggle> _wudaoSelectList;

		// Token: 0x040031B7 RID: 12727
		private TabWuDaoToggle _curSelectType;

		// Token: 0x040031B8 RID: 12728
		public WuDaoTooltip WuDaoTooltip;

		// Token: 0x040031B9 RID: 12729
		private Dictionary<string, Sprite> _wudaoImgDict;

		// Token: 0x040031BC RID: 12732
		private TabWuDaoLevel _wuDaoLevel;

		// Token: 0x040031BD RID: 12733
		private Text _curWuDaoDian;

		// Token: 0x040031BE RID: 12734
		private Text _curSiXuNum;
	}
}
