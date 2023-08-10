using System.Collections.Generic;
using GUIPackage;
using KBEngine;
using UnityEngine;

public class TuPoGongFa : MonoBehaviour
{
	public UILabel Name;

	public GameObject desc;

	public UILabel Time;

	public GameObject grid;

	public int NowIndex;

	public Skill_UIST skill_UIST;

	private KeyCell keyCell;

	private Avatar avatar;

	private Dictionary<int, GUIPackage.Skill> skilldatabase;

	private bool flagSwitch = true;

	private void Start()
	{
		keyCell = ((Component)this).GetComponent<KeyCell>();
		avatar = Tools.instance.getPlayer();
		skilldatabase = SkillStaticDatebase.instence.dicSkills;
	}

	public void setFirstCell()
	{
		if (skill_UIST.skill[0].skill_ID != -1)
		{
			NowIndex = 0;
			keyCell.keySkill = skill_UIST.skill[0];
		}
	}

	public void clearCell()
	{
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		Name.text = "";
		Time.text = "";
		keyCell.keySkill = new GUIPackage.Skill();
		int num = 0;
		foreach (Transform item in grid.transform)
		{
			((Component)item).GetComponent<KeyCellMapPassSkill>().keySkill = new GUIPackage.Skill();
			((Component)desc.transform.GetChild(num)).GetComponent<UILabel>().text = "";
			((Component)((Component)item).transform.Find("Background/Label")).GetComponent<UILabel>().color = new Color(0.54f, 0.54f, 0.54f, 0.7f);
			num++;
		}
	}

	private void setFlagSwitchTrue()
	{
		flagSwitch = true;
	}

	public void TuPo()
	{
	}

	public void resetNowIndex()
	{
		keyCell.keySkill = skill_UIST.skill[NowIndex];
	}

	private void Update()
	{
	}
}
