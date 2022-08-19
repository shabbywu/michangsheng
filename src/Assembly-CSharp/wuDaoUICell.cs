using System;
using System.Collections.Generic;
using KBEngine;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200050F RID: 1295
public class wuDaoUICell : MonoBehaviour
{
	// Token: 0x0600299F RID: 10655 RVA: 0x00004095 File Offset: 0x00002295
	private void Start()
	{
	}

	// Token: 0x060029A0 RID: 10656 RVA: 0x0013E26E File Offset: 0x0013C46E
	private void FixedUpdate()
	{
		this.postion = base.transform.position;
	}

	// Token: 0x060029A1 RID: 10657 RVA: 0x0013E281 File Offset: 0x0013C481
	public void Click()
	{
		WuDaoUIMag.inst.wuDaoCellTooltip.open(this.ID, this.icon);
		WuDaoUIMag.inst.wuDaoCellTooltip.action = delegate()
		{
			if (this.CanStudyWuDao())
			{
				this.studyWuDao();
				WuDaoUIMag.inst.upWuDaoDate();
			}
			else if (this.IsStudy())
			{
				UIPopTip.Inst.Pop("已经领悟过该大道", PopTipIconType.叹号);
			}
			else
			{
				UIPopTip.Inst.Pop("未达到领悟条件", PopTipIconType.叹号);
			}
			WuDaoUIMag.inst.ResetCellButton();
		};
	}

	// Token: 0x060029A2 RID: 10658 RVA: 0x0013E2BC File Offset: 0x0013C4BC
	public void studyWuDao()
	{
		Avatar player = Tools.instance.getPlayer();
		JSONObject wuDaoJson = this.WuDaoJson;
		foreach (JSONObject jsonobject in wuDaoJson["Type"].list)
		{
			player.wuDaoMag.addWuDaoSkill(jsonobject.I, wuDaoJson["id"].I);
		}
	}

	// Token: 0x060029A3 RID: 10659 RVA: 0x0013E348 File Offset: 0x0013C548
	public bool CanStudyWuDao()
	{
		JSONObject wuDaoJson = this.WuDaoJson;
		Tools.instance.getPlayer();
		if (this.IsStudy())
		{
			return false;
		}
		bool flag = true;
		foreach (JSONObject jsonobject in wuDaoJson["Type"].list)
		{
			if (!this.CanEx(jsonobject.I))
			{
				return false;
			}
			if (this.CanLastWuDao(jsonobject.I))
			{
				flag = false;
			}
		}
		return !flag && this.CanWuDaoDian();
	}

	// Token: 0x060029A4 RID: 10660 RVA: 0x0013E3F4 File Offset: 0x0013C5F4
	public bool IsStudy()
	{
		using (List<SkillItem>.Enumerator enumerator = Tools.instance.getPlayer().wuDaoMag.GetAllWuDaoSkills().GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.itemId == this.ID)
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x060029A5 RID: 10661 RVA: 0x0013E464 File Offset: 0x0013C664
	public bool CanEx(int WuDaoType)
	{
		int wuDaoLevelByType = Tools.instance.getPlayer().wuDaoMag.getWuDaoLevelByType(WuDaoType);
		int i = this.WuDaoJson["Lv"].I;
		return wuDaoLevelByType >= i;
	}

	// Token: 0x060029A6 RID: 10662 RVA: 0x0013E4A4 File Offset: 0x0013C6A4
	public bool CanWuDaoDian()
	{
		Avatar player = Tools.instance.getPlayer();
		JSONObject wuDaoJson = this.WuDaoJson;
		return player.wuDaoMag.GetNowWuDaoDian() >= wuDaoJson["Cast"].I;
	}

	// Token: 0x060029A7 RID: 10663 RVA: 0x0013E4E4 File Offset: 0x0013C6E4
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

	// Token: 0x170002BC RID: 700
	// (get) Token: 0x060029A8 RID: 10664 RVA: 0x0013E63C File Offset: 0x0013C83C
	public JSONObject WuDaoJson
	{
		get
		{
			return jsonData.instance.WuDaoJson[this.ID.ToString()];
		}
	}

	// Token: 0x060029A9 RID: 10665 RVA: 0x00004095 File Offset: 0x00002295
	private void Update()
	{
	}

	// Token: 0x040025F9 RID: 9721
	public int ID;

	// Token: 0x040025FA RID: 9722
	public Image bg;

	// Token: 0x040025FB RID: 9723
	public Image icon;

	// Token: 0x040025FC RID: 9724
	public Text castNum;

	// Token: 0x040025FD RID: 9725
	public Text wuDaoName;

	// Token: 0x040025FE RID: 9726
	public Vector3 postion;
}
