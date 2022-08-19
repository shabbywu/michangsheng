using System;
using System.Collections.Generic;
using KBEngine;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200040F RID: 1039
public class UI_MainUI : MonoBehaviour
{
	// Token: 0x0600218F RID: 8591 RVA: 0x000E92B4 File Offset: 0x000E74B4
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

	// Token: 0x06002190 RID: 8592 RVA: 0x000E9375 File Offset: 0x000E7575
	public void setSkill1(string name)
	{
		this.btnAll[0].GetChild(0).GetComponent<Text>().text = name;
	}

	// Token: 0x06002191 RID: 8593 RVA: 0x000E9394 File Offset: 0x000E7594
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

	// Token: 0x06002192 RID: 8594 RVA: 0x000E9464 File Offset: 0x000E7664
	public void setCoolTime(Transform button, int i)
	{
		button.gameObject.AddComponent<updateCD>();
		button.gameObject.GetComponent<updateCD>().coolingTimer = SkillBox.inst.skills[i].coolTime;
	}

	// Token: 0x06002193 RID: 8595 RVA: 0x00004095 File Offset: 0x00002295
	private void Update()
	{
	}

	// Token: 0x04001B07 RID: 6919
	public static UI_MainUI inst;

	// Token: 0x04001B08 RID: 6920
	public Transform but_skill1;

	// Token: 0x04001B09 RID: 6921
	public Transform but_skill2;

	// Token: 0x04001B0A RID: 6922
	public Transform but_skill3;

	// Token: 0x04001B0B RID: 6923
	public Transform btn_use;

	// Token: 0x04001B0C RID: 6924
	public Text skill1Text;

	// Token: 0x04001B0D RID: 6925
	public Text skill2Text;

	// Token: 0x04001B0E RID: 6926
	public Text skill3Text;

	// Token: 0x04001B0F RID: 6927
	public Dictionary<int, Transform> btnAll = new Dictionary<int, Transform>();

	// Token: 0x04001B10 RID: 6928
	public Dictionary<int, Skill> btnAllskill = new Dictionary<int, Skill>();
}
