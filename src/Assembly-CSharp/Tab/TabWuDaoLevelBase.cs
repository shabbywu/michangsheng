using System;
using System.Collections.Generic;
using JSONClass;
using UnityEngine;
using UnityEngine.UI;

namespace Tab
{
	// Token: 0x020006F1 RID: 1777
	[Serializable]
	public class TabWuDaoLevelBase : UIBase
	{
		// Token: 0x0600391E RID: 14622 RVA: 0x00185D94 File Offset: 0x00183F94
		public TabWuDaoLevelBase(GameObject go, int id, int level)
		{
			try
			{
				this._go = go;
				this._active = base.Get("Active", true);
				this._noActive = base.Get("NoActive", true);
				this._level = level;
				base.Get<Text>("Active/Name").text = WuDaoJinJieJson.DataDict[level].Text;
				base.Get<Text>("NoActive/Name").text = WuDaoJinJieJson.DataDict[level].Text;
				base.Get<Text>("Active/Value").text = WuDaoJinJieJson.DataDict[level - 1].Max.ToString();
				base.Get<Text>("NoActive/Value").text = WuDaoJinJieJson.DataDict[level - 1].Max.ToString();
				this._cloud = base.Get<Image>("Active/Cloud");
				this.UpdateUI(id);
			}
			catch (Exception ex)
			{
				Debug.LogError(ex);
			}
		}

		// Token: 0x0600391F RID: 14623 RVA: 0x00185E98 File Offset: 0x00184098
		public void UpdateUI(int id)
		{
			this._id = id;
			bool flag = this.IsCanActive();
			if (flag)
			{
				this._active.SetActive(true);
				this._noActive.SetActive(false);
			}
			else
			{
				this._active.SetActive(false);
				this._noActive.SetActive(true);
			}
			if (this._go.transform.childCount > 2)
			{
				Object.Destroy(this._go.transform.GetChild(2).gameObject);
			}
			List<int> wuDaoSkillCount = this.GetWuDaoSkillCount();
			int count = wuDaoSkillCount.Count;
			this._cloud.sprite = SingletonMono<TabUIMag>.Instance.WuDaoPanel.WudaoBgImgDict[count.ToString()];
			GameObject gameObject = SingletonMono<TabUIMag>.Instance.WuDaoPanel.WudaoSkillListDict[count].Inst(this._go.transform);
			float y = SingletonMono<TabUIMag>.Instance.WuDaoPanel.WudaoSkillListDict[count].transform.position.y;
			gameObject.transform.SetPostionY(y).SetLocalPositionX(0f);
			gameObject.name = count.ToString();
			for (int i = 0; i < count; i++)
			{
				WuDaoJson wuDaoJson = WuDaoJson.DataDict[wuDaoSkillCount[i]];
				WuDaoSlot wuDaoSlot = new WuDaoSlot(gameObject.transform.GetChild(i).gameObject, wuDaoJson.id);
				if (flag)
				{
					if (Tools.instance.getPlayer().wuDaoMag.IsStudy(wuDaoJson.id))
					{
						wuDaoSlot.SetState(1);
					}
					else
					{
						wuDaoSlot.SetState(2);
					}
				}
				else
				{
					wuDaoSlot.SetState(3);
				}
			}
		}

		// Token: 0x06003920 RID: 14624 RVA: 0x0018603C File Offset: 0x0018423C
		private List<int> GetWuDaoSkillCount()
		{
			List<int> list = new List<int>();
			foreach (WuDaoJson wuDaoJson in WuDaoJson.DataList)
			{
				if (wuDaoJson.Type.Contains(this._id) && wuDaoJson.Lv == this._level)
				{
					list.Add(wuDaoJson.id);
				}
			}
			return list;
		}

		// Token: 0x06003921 RID: 14625 RVA: 0x001860BC File Offset: 0x001842BC
		private bool IsCanActive()
		{
			return Tools.instance.getPlayer().wuDaoMag.getWuDaoLevelByType(this._id) >= this._level;
		}

		// Token: 0x04003125 RID: 12581
		private int _id;

		// Token: 0x04003126 RID: 12582
		private int _level;

		// Token: 0x04003127 RID: 12583
		private GameObject _active;

		// Token: 0x04003128 RID: 12584
		private GameObject _noActive;

		// Token: 0x04003129 RID: 12585
		private Image _cloud;
	}
}
