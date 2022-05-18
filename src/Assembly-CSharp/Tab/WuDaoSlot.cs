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
	// Token: 0x02000A33 RID: 2611
	[Serializable]
	public class WuDaoSlot : UIBase
	{
		// Token: 0x0600438D RID: 17293 RVA: 0x001CDA88 File Offset: 0x001CBC88
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
			this._go.AddComponent<TabListener>().mouseUpEvent.AddListener(delegate()
			{
				SingletonMono<TabUIMag>.Instance.WuDaoPanel.WuDaoTooltip.Show(this._icon.sprite, this.Id, new UnityAction(this.Study));
			});
		}

		// Token: 0x0600438E RID: 17294 RVA: 0x001CDBC8 File Offset: 0x001CBDC8
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

		// Token: 0x0600438F RID: 17295 RVA: 0x001CDD04 File Offset: 0x001CBF04
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

		// Token: 0x06004390 RID: 17296 RVA: 0x001CDE48 File Offset: 0x001CC048
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

		// Token: 0x06004391 RID: 17297 RVA: 0x001CDEE4 File Offset: 0x001CC0E4
		public bool CanEx(int WuDaoType)
		{
			int wuDaoLevelByType = Tools.instance.getPlayer().wuDaoMag.getWuDaoLevelByType(WuDaoType);
			int i = this.WuDaoJson["Lv"].I;
			return wuDaoLevelByType >= i;
		}

		// Token: 0x06004392 RID: 17298 RVA: 0x001CDF24 File Offset: 0x001CC124
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

		// Token: 0x06004393 RID: 17299 RVA: 0x001CDFA8 File Offset: 0x001CC1A8
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

		// Token: 0x04003B8F RID: 15247
		private GameObject _name;

		// Token: 0x04003B90 RID: 15248
		private GameObject _cost;

		// Token: 0x04003B91 RID: 15249
		private GameObject _black;

		// Token: 0x04003B92 RID: 15250
		private Image _icon;

		// Token: 0x04003B93 RID: 15251
		private Image _white;

		// Token: 0x04003B94 RID: 15252
		private UIEffect _iconEffect;

		// Token: 0x04003B95 RID: 15253
		public int Id;

		// Token: 0x04003B96 RID: 15254
		public int State;

		// Token: 0x04003B97 RID: 15255
		public int Cost;

		// Token: 0x04003B98 RID: 15256
		public JSONObject WuDaoJson;
	}
}
