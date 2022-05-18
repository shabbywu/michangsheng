using System;
using System.Collections.Generic;
using KBEngine;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020005C1 RID: 1473
public class UI_MainUI : MonoBehaviour
{
	// Token: 0x06002547 RID: 9543 RVA: 0x0012AB4C File Offset: 0x00128D4C
	private void Awake()
	{
		UI_MainUI.inst = this;
		this.skill1Text = this.but_skill1.GetChild(0).GetComponent<Text>();
		this.skill2Text = this.but_skill2.GetChild(0).GetComponent<Text>();
		this.skill3Text = this.but_skill3.GetChild(0).GetComponent<Text>();
		this.but_skill1.gameObject.SetActive(false);
		this.but_skill2.gameObject.SetActive(false);
		this.but_skill3.gameObject.SetActive(false);
		this.btnAll[0] = this.but_skill1;
		this.btnAll[1] = this.but_skill2;
		this.btnAll[2] = this.but_skill3;
	}

	// Token: 0x06002548 RID: 9544 RVA: 0x0001DE4A File Offset: 0x0001C04A
	public void setSkill1(string name)
	{
		this.btnAll[0].GetChild(0).GetComponent<Text>().text = name;
	}

	// Token: 0x06002549 RID: 9545 RVA: 0x0012AC10 File Offset: 0x00128E10
	public void setSkill()
	{
		for (int i = 0; i < this.btnAll.Count; i++)
		{
			this.btnAll[i].gameObject.SetActive(false);
		}
		for (int j = 0; j < SkillBox.inst.skills.Count; j++)
		{
			string name = SkillBox.inst.skills[j].name;
			this.btnAll[j].GetChild(0).GetComponent<Text>().text = name;
			this.btnAll[j].gameObject.SetActive(true);
			this.setCoolTime(this.btnAll[j], j);
			this.btnAllskill[j] = SkillBox.inst.skills[j];
		}
	}

	// Token: 0x0600254A RID: 9546 RVA: 0x0001DE69 File Offset: 0x0001C069
	public void setCoolTime(Transform button, int i)
	{
		button.gameObject.AddComponent<updateCD>();
		button.gameObject.GetComponent<updateCD>().coolingTimer = SkillBox.inst.skills[i].coolTime;
	}

	// Token: 0x0600254B RID: 9547 RVA: 0x000042DD File Offset: 0x000024DD
	private void Update()
	{
	}

	// Token: 0x04001FC6 RID: 8134
	public static UI_MainUI inst;

	// Token: 0x04001FC7 RID: 8135
	public Transform but_skill1;

	// Token: 0x04001FC8 RID: 8136
	public Transform but_skill2;

	// Token: 0x04001FC9 RID: 8137
	public Transform but_skill3;

	// Token: 0x04001FCA RID: 8138
	public Transform btn_use;

	// Token: 0x04001FCB RID: 8139
	public Text skill1Text;

	// Token: 0x04001FCC RID: 8140
	public Text skill2Text;

	// Token: 0x04001FCD RID: 8141
	public Text skill3Text;

	// Token: 0x04001FCE RID: 8142
	public Dictionary<int, Transform> btnAll = new Dictionary<int, Transform>();

	// Token: 0x04001FCF RID: 8143
	public Dictionary<int, Skill> btnAllskill = new Dictionary<int, Skill>();
}
