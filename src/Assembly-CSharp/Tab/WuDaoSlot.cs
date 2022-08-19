using System;
using System.Collections.Generic;
using Coffee.UIEffects;
using JSONClass;
using KBEngine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Tab
{
	// Token: 0x020006F2 RID: 1778
	[Serializable]
	public class WuDaoSlot : UIBase
	{
		// Token: 0x06003922 RID: 14626 RVA: 0x001860E4 File Offset: 0x001842E4
		public WuDaoSlot(GameObject go, int id)
		{
			this._go = go;
			this.Id = id;
			WuDaoJson wuDaoJson = JSONClass.WuDaoJson.DataDict[this.Id];
			this._icon = base.Get<Image>("Mask/Icon");
			this._icon.sprite = ResManager.inst.LoadSprite("WuDao Icon/" + wuDaoJson.icon);
			this._iconEffect = base.Get<UIEffect>("Mask/Icon");
			base.Get<Text>("Name/Value").text = wuDaoJson.name;
			this.Cost = wuDaoJson.Cast;
			base.Get<Text>("Cost/Value").text = this.Cost.ToString();
			this._name = base.Get("Name", true);
			this._cost = base.Get("Cost", true);
			this._black = base.Get("Black", true);
			this._white = this._go.GetComponent<Image>();
			this.WuDaoJson = jsonData.instance.WuDaoJson[this.Id.ToString()];
			this._go.AddComponent<UIListener>().mouseUpEvent.AddListener(delegate()
			{
				SingletonMono<TabUIMag>.Instance.WuDaoPanel.WuDaoTooltip.Show(this._icon.sprite, this.Id, new UnityAction(this.Study));
			});
		}

		// Token: 0x06003923 RID: 14627 RVA: 0x00186224 File Offset: 0x00184424
		public void SetState(int state)
		{
			this.State = state;
			switch (this.State)
			{
			case 1:
				this._name.SetActive(false);
				this._cost.SetActive(false);
				this._black.SetActive(false);
				this._white.Show();
				this._iconEffect.enabled = false;
				return;
			case 2:
				this._name.SetActive(true);
				this._cost.SetActive(true);
				this._black.SetActive(false);
				this._white.Show();
				this._iconEffect.enabled = true;
				this._iconEffect.effectFactor = 0.72f;
				this._iconEffect.colorMode = 3;
				this._iconEffect.colorFactor = 0.25f;
				return;
			case 3:
				this._name.SetActive(false);
				this._cost.SetActive(false);
				this._black.SetActive(true);
				this._white.Hide();
				this._iconEffect.enabled = true;
				this._iconEffect.effectFactor = 0.72f;
				this._iconEffect.colorMode = 3;
				this._iconEffect.colorFactor = 0.25f;
				return;
			default:
				return;
			}
		}

		// Token: 0x06003924 RID: 14628 RVA: 0x00186360 File Offset: 0x00184560
		private void Study()
		{
			Avatar player = Tools.instance.getPlayer();
			if (this.State == 1)
			{
				UIPopTip.Inst.Pop("已领悟过该大道", PopTipIconType.叹号);
			}
			else if (this.State == 2)
			{
				if (player.wuDaoMag.GetNowWuDaoDian() >= this.Cost)
				{
					if (this.CanStudyWuDao())
					{
						foreach (int wuDaoType in JSONClass.WuDaoJson.DataDict[this.Id].Type)
						{
							player.wuDaoMag.addWuDaoSkill(wuDaoType, this.Id);
							SingletonMono<TabUIMag>.Instance.WuDaoPanel.UpdateWuDaoDian();
						}
						this.SetState(1);
					}
					else if (this.MoreCheck())
					{
						UIPopTip.Inst.Pop("未达到领悟条件", PopTipIconType.叹号);
					}
					else
					{
						UIPopTip.Inst.Pop("未领悟前置悟道", PopTipIconType.叹号);
					}
				}
				else
				{
					UIPopTip.Inst.Pop("悟道点不足", PopTipIconType.叹号);
				}
			}
			else if (this.State == 3)
			{
				UIPopTip.Inst.Pop("未达到领悟条件", PopTipIconType.叹号);
			}
			SingletonMono<TabUIMag>.Instance.WuDaoPanel.WuDaoTooltip.Close();
		}

		// Token: 0x06003925 RID: 14629 RVA: 0x001864A4 File Offset: 0x001846A4
		public bool CanStudyWuDao()
		{
			JSONObject jsonobject = jsonData.instance.WuDaoJson[this.Id.ToString()];
			bool flag = true;
			foreach (JSONObject jsonobject2 in jsonobject["Type"].list)
			{
				if (!this.CanEx(jsonobject2.I))
				{
					return false;
				}
				if (this.CanLastWuDao(jsonobject2.I))
				{
					flag = false;
				}
			}
			return !flag;
		}

		// Token: 0x06003926 RID: 14630 RVA: 0x00186540 File Offset: 0x00184740
		public bool CanEx(int WuDaoType)
		{
			int wuDaoLevelByType = Tools.instance.getPlayer().wuDaoMag.getWuDaoLevelByType(WuDaoType);
			int i = this.WuDaoJson["Lv"].I;
			return wuDaoLevelByType >= i;
		}

		// Token: 0x06003927 RID: 14631 RVA: 0x00186580 File Offset: 0x00184780
		public bool MoreCheck()
		{
			foreach (JSONObject jsonobject in jsonData.instance.WuDaoJson[this.Id.ToString()]["Type"].list)
			{
				if (!this.CanEx(jsonobject.I))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06003928 RID: 14632 RVA: 0x00186604 File Offset: 0x00184804
		public bool CanLastWuDao(int wudaoType)
		{
			Avatar player = Tools.instance.getPlayer();
			JSONObject wuDaoJson = this.WuDaoJson;
			JSONObject wuDaoStudy = player.wuDaoMag.getWuDaoStudy(wudaoType);
			int i = wuDaoJson["Lv"].I;
			if (i == 1)
			{
				return true;
			}
			Dictionary<int, bool> dictionary = new Dictionary<int, bool>();
			for (int j = 1; j < i; j++)
			{
				dictionary[j] = false;
			}
			foreach (JSONObject jsonobject in wuDaoStudy.list)
			{
				JSONObject jsonobject2 = jsonData.instance.WuDaoJson[jsonobject.I.ToString()];
				if (dictionary.ContainsKey(jsonobject2["Lv"].I) && !dictionary[jsonobject2["Lv"].I])
				{
					dictionary[jsonobject2["Lv"].I] = true;
				}
			}
			foreach (KeyValuePair<int, bool> keyValuePair in dictionary)
			{
				if (!keyValuePair.Value)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0400312A RID: 12586
		private GameObject _name;

		// Token: 0x0400312B RID: 12587
		private GameObject _cost;

		// Token: 0x0400312C RID: 12588
		private GameObject _black;

		// Token: 0x0400312D RID: 12589
		private Image _icon;

		// Token: 0x0400312E RID: 12590
		private Image _white;

		// Token: 0x0400312F RID: 12591
		private UIEffect _iconEffect;

		// Token: 0x04003130 RID: 12592
		public int Id;

		// Token: 0x04003131 RID: 12593
		public int State;

		// Token: 0x04003132 RID: 12594
		public int Cost;

		// Token: 0x04003133 RID: 12595
		public JSONObject WuDaoJson;
	}
}
