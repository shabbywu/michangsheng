using System;
using System.Collections.Generic;
using KBEngine;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020007A1 RID: 1953
public class wuDaoUICell : MonoBehaviour
{
	// Token: 0x060031A6 RID: 12710 RVA: 0x000042DD File Offset: 0x000024DD
	private void Start()
	{
	}

	// Token: 0x060031A7 RID: 12711 RVA: 0x0002457F File Offset: 0x0002277F
	private void FixedUpdate()
	{
		this.postion = base.transform.position;
	}

	// Token: 0x060031A8 RID: 12712 RVA: 0x00024592 File Offset: 0x00022792
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

	// Token: 0x060031A9 RID: 12713 RVA: 0x0018B430 File Offset: 0x00189630
	public void studyWuDao()
	{
		Avatar player = Tools.instance.getPlayer();
		JSONObject wuDaoJson = this.WuDaoJson;
		foreach (JSONObject jsonobject in wuDaoJson["Type"].list)
		{
			player.wuDaoMag.addWuDaoSkill(jsonobject.I, wuDaoJson["id"].I);
		}
	}

	// Token: 0x060031AA RID: 12714 RVA: 0x0018B4BC File Offset: 0x001896BC
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

	// Token: 0x060031AB RID: 12715 RVA: 0x0018B568 File Offset: 0x00189768
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

	// Token: 0x060031AC RID: 12716 RVA: 0x0018B5D8 File Offset: 0x001897D8
	public bool CanEx(int WuDaoType)
	{
		int wuDaoLevelByType = Tools.instance.getPlayer().wuDaoMag.getWuDaoLevelByType(WuDaoType);
		int i = this.WuDaoJson["Lv"].I;
		return wuDaoLevelByType >= i;
	}

	// Token: 0x060031AD RID: 12717 RVA: 0x0018B618 File Offset: 0x00189818
	public bool CanWuDaoDian()
	{
		Avatar player = Tools.instance.getPlayer();
		JSONObject wuDaoJson = this.WuDaoJson;
		return player.wuDaoMag.GetNowWuDaoDian() >= wuDaoJson["Cast"].I;
	}

	// Token: 0x060031AE RID: 12718 RVA: 0x0018B658 File Offset: 0x00189858
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

	// Token: 0x17000457 RID: 1111
	// (get) Token: 0x060031AF RID: 12719 RVA: 0x000245CA File Offset: 0x000227CA
	public JSONObject WuDaoJson
	{
		get
		{
			return jsonData.instance.WuDaoJson[this.ID.ToString()];
		}
	}

	// Token: 0x060031B0 RID: 12720 RVA: 0x000042DD File Offset: 0x000024DD
	private void Update()
	{
	}

	// Token: 0x04002DE2 RID: 11746
	public int ID;

	// Token: 0x04002DE3 RID: 11747
	public Image bg;

	// Token: 0x04002DE4 RID: 11748
	public Image icon;

	// Token: 0x04002DE5 RID: 11749
	public Text castNum;

	// Token: 0x04002DE6 RID: 11750
	public Text wuDaoName;

	// Token: 0x04002DE7 RID: 11751
	public Vector3 postion;
}
