using System;
using System.Collections.Generic;
using GUIPackage;
using KBEngine;
using UnityEngine;

// Token: 0x0200043C RID: 1084
public class TuPoGongFa : MonoBehaviour
{
	// Token: 0x06002278 RID: 8824 RVA: 0x000ED167 File Offset: 0x000EB367
	private void Start()
	{
		this.keyCell = base.GetComponent<KeyCell>();
		this.avatar = Tools.instance.getPlayer();
		this.skilldatabase = SkillStaticDatebase.instence.dicSkills;
	}

	// Token: 0x06002279 RID: 8825 RVA: 0x000ED195 File Offset: 0x000EB395
	public void setFirstCell()
	{
		if (this.skill_UIST.skill[0].skill_ID != -1)
		{
			this.NowIndex = 0;
			this.keyCell.keySkill = this.skill_UIST.skill[0];
		}
	}

	// Token: 0x0600227A RID: 8826 RVA: 0x000ED1D4 File Offset: 0x000EB3D4
	public void clearCell()
	{
		this.Name.text = "";
		this.Time.text = "";
		this.keyCell.keySkill = new GUIPackage.Skill();
		int num = 0;
		foreach (object obj in this.grid.transform)
		{
			Transform transform = (Transform)obj;
			transform.GetComponent<KeyCellMapPassSkill>().keySkill = new GUIPackage.Skill();
			this.desc.transform.GetChild(num).GetComponent<UILabel>().text = "";
			transform.transform.Find("Background/Label").GetComponent<UILabel>().color = new Color(0.54f, 0.54f, 0.54f, 0.7f);
			num++;
		}
	}

	// Token: 0x0600227B RID: 8827 RVA: 0x000ED2C4 File Offset: 0x000EB4C4
	private void setFlagSwitchTrue()
	{
		this.flagSwitch = true;
	}

	// Token: 0x0600227C RID: 8828 RVA: 0x00004095 File Offset: 0x00002295
	public void TuPo()
	{
	}

	// Token: 0x0600227D RID: 8829 RVA: 0x000ED2CD File Offset: 0x000EB4CD
	public void resetNowIndex()
	{
		this.keyCell.keySkill = this.skill_UIST.skill[this.NowIndex];
	}

	// Token: 0x0600227E RID: 8830 RVA: 0x00004095 File Offset: 0x00002295
	private void Update()
	{
	}

	// Token: 0x04001BDB RID: 7131
	public UILabel Name;

	// Token: 0x04001BDC RID: 7132
	public GameObject desc;

	// Token: 0x04001BDD RID: 7133
	public UILabel Time;

	// Token: 0x04001BDE RID: 7134
	public GameObject grid;

	// Token: 0x04001BDF RID: 7135
	public int NowIndex;

	// Token: 0x04001BE0 RID: 7136
	public Skill_UIST skill_UIST;

	// Token: 0x04001BE1 RID: 7137
	private KeyCell keyCell;

	// Token: 0x04001BE2 RID: 7138
	private Avatar avatar;

	// Token: 0x04001BE3 RID: 7139
	private Dictionary<int, GUIPackage.Skill> skilldatabase;

	// Token: 0x04001BE4 RID: 7140
	private bool flagSwitch = true;
}
