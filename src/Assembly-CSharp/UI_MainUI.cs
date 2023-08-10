using System.Collections.Generic;
using KBEngine;
using UnityEngine;
using UnityEngine.UI;

public class UI_MainUI : MonoBehaviour
{
	public static UI_MainUI inst;

	public Transform but_skill1;

	public Transform but_skill2;

	public Transform but_skill3;

	public Transform btn_use;

	public Text skill1Text;

	public Text skill2Text;

	public Text skill3Text;

	public Dictionary<int, Transform> btnAll = new Dictionary<int, Transform>();

	public Dictionary<int, Skill> btnAllskill = new Dictionary<int, Skill>();

	private void Awake()
	{
		inst = this;
		skill1Text = ((Component)but_skill1.GetChild(0)).GetComponent<Text>();
		skill2Text = ((Component)but_skill2.GetChild(0)).GetComponent<Text>();
		skill3Text = ((Component)but_skill3.GetChild(0)).GetComponent<Text>();
		((Component)but_skill1).gameObject.SetActive(false);
		((Component)but_skill2).gameObject.SetActive(false);
		((Component)but_skill3).gameObject.SetActive(false);
		btnAll[0] = but_skill1;
		btnAll[1] = but_skill2;
		btnAll[2] = but_skill3;
	}

	public void setSkill1(string name)
	{
		((Component)btnAll[0].GetChild(0)).GetComponent<Text>().text = name;
	}

	public void setSkill()
	{
		for (int i = 0; i < btnAll.Count; i++)
		{
			((Component)btnAll[i]).gameObject.SetActive(false);
		}
		for (int j = 0; j < SkillBox.inst.skills.Count; j++)
		{
			string name = SkillBox.inst.skills[j].name;
			((Component)btnAll[j].GetChild(0)).GetComponent<Text>().text = name;
			((Component)btnAll[j]).gameObject.SetActive(true);
			setCoolTime(btnAll[j], j);
			btnAllskill[j] = SkillBox.inst.skills[j];
		}
	}

	public void setCoolTime(Transform button, int i)
	{
		((Component)button).gameObject.AddComponent<updateCD>();
		((Component)button).gameObject.GetComponent<updateCD>().coolingTimer = SkillBox.inst.skills[i].coolTime;
	}

	private void Update()
	{
	}
}
