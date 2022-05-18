using System;
using System.Collections.Generic;
using JSONClass;
using UnityEngine;
using UnityEngine.UI;

namespace Tab
{
	// Token: 0x02000A51 RID: 2641
	[Serializable]
	public class TabWuDaoPanel : ITabPanelBase
	{
		// Token: 0x170007D0 RID: 2000
		// (get) Token: 0x0600441F RID: 17439 RVA: 0x00030CD8 File Offset: 0x0002EED8
		// (set) Token: 0x06004420 RID: 17440 RVA: 0x00030CE0 File Offset: 0x0002EEE0
		public Dictionary<int, GameObject> WudaoSkillListDict { get; private set; }

		// Token: 0x170007D1 RID: 2001
		// (get) Token: 0x06004421 RID: 17441 RVA: 0x00030CE9 File Offset: 0x0002EEE9
		// (set) Token: 0x06004422 RID: 17442 RVA: 0x00030CF1 File Offset: 0x0002EEF1
		public Dictionary<string, Sprite> WudaoBgImgDict { get; private set; }

		// Token: 0x06004423 RID: 17443 RVA: 0x00030CFA File Offset: 0x0002EEFA
		public TabWuDaoPanel(GameObject gameObject)
		{
			this._go = gameObject;
			this._isInit = false;
		}

		// Token: 0x06004424 RID: 17444 RVA: 0x001D1CCC File Offset: 0x001CFECC
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

		// Token: 0x06004425 RID: 17445 RVA: 0x001D1E28 File Offset: 0x001D0028
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

		// Token: 0x06004426 RID: 17446 RVA: 0x001D1EE0 File Offset: 0x001D00E0
		public void UpdateWuDaoDian()
		{
			this._curWuDaoDian.text = Tools.instance.getPlayer().wuDaoMag.GetNowWuDaoDian().ToString();
		}

		// Token: 0x06004427 RID: 17447 RVA: 0x001D1F14 File Offset: 0x001D0114
		public void UpdateUI()
		{
			this.UpdateWuDaoDian();
			this._curSiXuNum.text = Tools.instance.getPlayer().LingGuang.Count.ToString();
			if (this._curSelectType != null)
			{
				this._wuDaoLevel.UpdateUI(this._curSelectType.Id);
			}
		}

		// Token: 0x06004428 RID: 17448 RVA: 0x00030D10 File Offset: 0x0002EF10
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

		// Token: 0x04003C31 RID: 15409
		private bool _isInit;

		// Token: 0x04003C32 RID: 15410
		private List<TabWuDaoToggle> _wudaoSelectList;

		// Token: 0x04003C33 RID: 15411
		private TabWuDaoToggle _curSelectType;

		// Token: 0x04003C34 RID: 15412
		public WuDaoTooltip WuDaoTooltip;

		// Token: 0x04003C35 RID: 15413
		private Dictionary<string, Sprite> _wudaoImgDict;

		// Token: 0x04003C38 RID: 15416
		private TabWuDaoLevel _wuDaoLevel;

		// Token: 0x04003C39 RID: 15417
		private Text _curWuDaoDian;

		// Token: 0x04003C3A RID: 15418
		private Text _curSiXuNum;
	}
}
