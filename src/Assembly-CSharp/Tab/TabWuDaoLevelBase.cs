using System;
using System.Collections.Generic;
using JSONClass;
using UnityEngine;
using UnityEngine.UI;

namespace Tab
{
	// Token: 0x02000A32 RID: 2610
	[Serializable]
	public class TabWuDaoLevelBase : UIBase
	{
		// Token: 0x06004389 RID: 17289 RVA: 0x001CD760 File Offset: 0x001CB960
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

		// Token: 0x0600438A RID: 17290 RVA: 0x001CD864 File Offset: 0x001CBA64
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

		// Token: 0x0600438B RID: 17291 RVA: 0x001CDA08 File Offset: 0x001CBC08
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

		// Token: 0x0600438C RID: 17292 RVA: 0x0003049F File Offset: 0x0002E69F
		private bool IsCanActive()
		{
			return Tools.instance.getPlayer().wuDaoMag.getWuDaoLevelByType(this._id) >= this._level;
		}

		// Token: 0x04003B8A RID: 15242
		private int _id;

		// Token: 0x04003B8B RID: 15243
		private int _level;

		// Token: 0x04003B8C RID: 15244
		private GameObject _active;

		// Token: 0x04003B8D RID: 15245
		private GameObject _noActive;

		// Token: 0x04003B8E RID: 15246
		private Image _cloud;
	}
}
