using System;
using System.Data;
using System.Text.RegularExpressions;
using KBEngine;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000484 RID: 1156
public class skillBtn : MonoBehaviour
{
	// Token: 0x0600244A RID: 9290 RVA: 0x000FAEA0 File Offset: 0x000F90A0
	private void Start()
	{
		this.panle.SetActive(false);
	}

	// Token: 0x0600244B RID: 9291 RVA: 0x000FAEB0 File Offset: 0x000F90B0
	public void openskill()
	{
		this.panle.SetActive(true);
		for (int i = 1; i <= 4; i++)
		{
			Text component = this.panle.transform.Find("Text " + i).GetComponent<Text>();
			Text component2 = this.panle.transform.Find("skilltext (" + i + ")").GetComponent<Text>();
			Skill skill;
			if (UI_MainUI.inst.btnAllskill.TryGetValue(i - 1, out skill))
			{
				component.text = skill.name;
				string text = Regex.Unescape(jsonData.instance.skillJsonData[string.Concat(skill.id)]["descr"].str);
				if (text.IndexOf("（") > 0)
				{
					string text2 = text.Substring(0, text.IndexOf("（") + 1);
					string text3 = text.Substring(text.IndexOf("）"), text.Length - text.IndexOf("）"));
					int length = text.Length - text3.Length - text2.Length;
					string text4 = text.Substring(text.IndexOf("（") + 1, length);
					int attack_Max = ((Avatar)KBEngineApp.app.player()).attack_Max;
					string expression = text4.Replace("attack", string.Concat(attack_Max));
					object arg = new DataTable().Compute(expression, "");
					component2.text = text2 + arg + text3;
				}
				else
				{
					component2.text = text;
				}
			}
		}
	}

	// Token: 0x0600244C RID: 9292 RVA: 0x000FB065 File Offset: 0x000F9265
	public void button()
	{
		if (this.state == 0)
		{
			this.state = 1;
			this.openskill();
			return;
		}
		this.state = 0;
		this.closeskill();
	}

	// Token: 0x0600244D RID: 9293 RVA: 0x000FAEA0 File Offset: 0x000F90A0
	public void closeskill()
	{
		this.panle.SetActive(false);
	}

	// Token: 0x04001D04 RID: 7428
	public GameObject panle;

	// Token: 0x04001D05 RID: 7429
	public int state;
}
