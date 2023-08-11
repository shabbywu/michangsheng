using System.Data;
using System.Text.RegularExpressions;
using KBEngine;
using UnityEngine;
using UnityEngine.UI;

public class skillBtn : MonoBehaviour
{
	public GameObject panle;

	public int state;

	private void Start()
	{
		panle.SetActive(false);
	}

	public void openskill()
	{
		panle.SetActive(true);
		for (int i = 1; i <= 4; i++)
		{
			Text component = ((Component)panle.transform.Find("Text " + i)).GetComponent<Text>();
			Text component2 = ((Component)panle.transform.Find("skilltext (" + i + ")")).GetComponent<Text>();
			if (UI_MainUI.inst.btnAllskill.TryGetValue(i - 1, out var value))
			{
				component.text = value.name;
				string text = Regex.Unescape(jsonData.instance.skillJsonData[string.Concat(value.id)]["descr"].str);
				if (text.IndexOf("（") > 0)
				{
					string text2 = text.Substring(0, text.IndexOf("（") + 1);
					string text3 = text.Substring(text.IndexOf("）"), text.Length - text.IndexOf("）"));
					int length = text.Length - text3.Length - text2.Length;
					string text4 = text.Substring(text.IndexOf("（") + 1, length);
					int attack_Max = ((Avatar)KBEngineApp.app.player()).attack_Max;
					string expression = text4.Replace("attack", string.Concat(attack_Max));
					object obj = new DataTable().Compute(expression, "");
					component2.text = string.Concat(text2, obj, text3);
				}
				else
				{
					component2.text = text;
				}
			}
		}
	}

	public void button()
	{
		if (state == 0)
		{
			state = 1;
			openskill();
		}
		else
		{
			state = 0;
			closeskill();
		}
	}

	public void closeskill()
	{
		panle.SetActive(false);
	}
}
