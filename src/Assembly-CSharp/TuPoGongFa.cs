using System;
using System.Collections.Generic;
using GUIPackage;
using KBEngine;
using UnityEngine;

// Token: 0x020005F3 RID: 1523
public class TuPoGongFa : MonoBehaviour
{
	// Token: 0x06002637 RID: 9783 RVA: 0x0001E7DB File Offset: 0x0001C9DB
	private void Start()
	{
		this.keyCell = base.GetComponent<KeyCell>();
		this.avatar = Tools.instance.getPlayer();
		this.skilldatabase = SkillStaticDatebase.instence.dicSkills;
	}

	// Token: 0x06002638 RID: 9784 RVA: 0x0001E809 File Offset: 0x0001CA09
	public void setFirstCell()
	{
		if (this.skill_UIST.skill[0].skill_ID != -1)
		{
			this.NowIndex = 0;
			this.keyCell.keySkill = this.skill_UIST.skill[0];
		}
	}

	// Token: 0x06002639 RID: 9785 RVA: 0x0012E0A0 File Offset: 0x0012C2A0
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

	// Token: 0x0600263A RID: 9786 RVA: 0x0001E847 File Offset: 0x0001CA47
	private void setFlagSwitchTrue()
	{
		this.flagSwitch = true;
	}

	// Token: 0x0600263B RID: 9787 RVA: 0x000042DD File Offset: 0x000024DD
	public void TuPo()
	{
	}

	// Token: 0x0600263C RID: 9788 RVA: 0x0001E850 File Offset: 0x0001CA50
	public void resetNowIndex()
	{
		this.keyCell.keySkill = this.skill_UIST.skill[this.NowIndex];
	}

	// Token: 0x0600263D RID: 9789 RVA: 0x000042DD File Offset: 0x000024DD
	private void Update()
	{
	}

	// Token: 0x040020A7 RID: 8359
	public UILabel Name;

	// Token: 0x040020A8 RID: 8360
	public GameObject desc;

	// Token: 0x040020A9 RID: 8361
	public UILabel Time;

	// Token: 0x040020AA RID: 8362
	public GameObject grid;

	// Token: 0x040020AB RID: 8363
	public int NowIndex;

	// Token: 0x040020AC RID: 8364
	public Skill_UIST skill_UIST;

	// Token: 0x040020AD RID: 8365
	private KeyCell keyCell;

	// Token: 0x040020AE RID: 8366
	private Avatar avatar;

	// Token: 0x040020AF RID: 8367
	private Dictionary<int, GUIPackage.Skill> skilldatabase;

	// Token: 0x040020B0 RID: 8368
	private bool flagSwitch = true;
}
