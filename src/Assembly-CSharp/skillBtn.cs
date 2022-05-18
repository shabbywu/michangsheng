using System;
using System.Data;
using System.Text.RegularExpressions;
using KBEngine;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200064F RID: 1615
public class skillBtn : MonoBehaviour
{
	// Token: 0x06002826 RID: 10278 RVA: 0x0001F899 File Offset: 0x0001DA99
	private void Start()
	{
		this.panle.SetActive(false);
	}

	// Token: 0x06002827 RID: 10279 RVA: 0x0013AAB0 File Offset: 0x00138CB0
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

	// Token: 0x06002828 RID: 10280 RVA: 0x0001F8A7 File Offset: 0x0001DAA7
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

	// Token: 0x06002829 RID: 10281 RVA: 0x0001F899 File Offset: 0x0001DA99
	public void closeskill()
	{
		this.panle.SetActive(false);
	}

	// Token: 0x040021F5 RID: 8693
	public GameObject panle;

	// Token: 0x040021F6 RID: 8694
	public int state;
}
