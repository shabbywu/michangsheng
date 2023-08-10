using System.Collections.Generic;
using KBEngine;
using UnityEngine;
using UnityEngine.Events;

namespace GUIPackage;

public class SkillStaticCell : MonoBehaviour
{
	public GameObject Icon;

	public GameObject Level;

	public GameObject Name;

	public GameObject PingZhi;

	public int skillID;

	public Skill_UIST skill_UIST;

	public GameObject KeyName;

	public UILabel NameLabel;

	public bool showName;

	private Avatar avatar;

	public GameObject Dengji;

	public UITexture uITexture;

	public List<Texture> sprites;

	public bool showDengji;

	private float refreshCD;

	private void Start()
	{
		avatar = Tools.instance.getPlayer();
	}

	private void Update()
	{
		if (refreshCD < 0f)
		{
			UpdateRefresh();
			refreshCD = 0.2f;
		}
		else
		{
			refreshCD -= Time.deltaTime;
		}
	}

	public void UpdateRefresh()
	{
		if (skillID == -1)
		{
			Icon.GetComponent<UITexture>().mainTexture = null;
			Level.GetComponent<UILabel>().text = "";
			Name.GetComponent<UILabel>().text = "";
			if ((Object)(object)PingZhi != (Object)null)
			{
				PingZhi.GetComponent<UITexture>().mainTexture = null;
			}
			if ((Object)(object)KeyName != (Object)null)
			{
				KeyName.SetActive(false);
			}
			return;
		}
		Icon.GetComponent<UITexture>().mainTexture = (Texture)(object)skill_UIST.skill[skillID].skill_Icon;
		Level.GetComponent<UILabel>().text = "Lv" + getSkillLevel(skill_UIST.skill[skillID].skill_ID) + "/" + Singleton.skillUI2.skill[skillID].Max_level;
		Name.GetComponent<UILabel>().text = skill_UIST.skill[skillID].skill_Name;
		if ((Object)(object)PingZhi != (Object)null)
		{
			PingZhi.GetComponent<UITexture>().mainTexture = (Texture)(object)skill_UIST.skill[skillID].SkillPingZhi;
		}
		if (!((Object)(object)KeyName == (Object)null))
		{
			if (Tools.instance.getPlayer().showSkillName == 0 && showName)
			{
				KeyName.SetActive(true);
				NameLabel.text = Tools.instance.getStaticSkillName(skill_UIST.skill[skillID].skill_ID);
			}
			else
			{
				KeyName.SetActive(false);
			}
			Dengji.SetActive(false);
		}
	}

	public int getSkillLevel(int SkillID)
	{
		int staticSkillIDByKey = Tools.instance.getStaticSkillIDByKey(SkillID);
		foreach (SkillItem hasStaticSkill in avatar.hasStaticSkillList)
		{
			if (staticSkillIDByKey == hasStaticSkill.itemId)
			{
				return hasStaticSkill.level;
			}
		}
		return 0;
	}

	protected virtual void OnPress()
	{
		if (skillID != -1)
		{
			PCOnPress();
		}
	}

	public void MobilePress()
	{
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Expected O, but got Unknown
		PCOnHover(isOver: true);
		Singleton.ToolTipsBackGround.openTooltips();
		TooltipsBackgroundi toolTipsBackGround = Singleton.ToolTipsBackGround;
		toolTipsBackGround.CloseAction = (UnityAction)delegate
		{
			PCOnHover(isOver: false);
		};
		((Component)toolTipsBackGround.use).gameObject.SetActive(false);
	}

	public void PCOnPress()
	{
		skill_UIST.showTooltip = false;
		if (Input.GetMouseButton(0) && !Singleton.inventory.draggingItem && !Singleton.key.draggingKey && skill_UIST.skill[skillID].CoolDown != 0f)
		{
			skill_UIST.draggingSkill = true;
			skill_UIST.dragedSkill = skill_UIST.skill[skillID];
		}
	}

	public virtual void SetShow_Tooltip()
	{
		skill_UIST.Show_Tooltip(skill_UIST.skill[skillID]);
	}

	private void OnHover(bool isOver)
	{
		PCOnHover(isOver);
	}

	public void PCOnHover(bool isOver)
	{
		if (skillID != -1)
		{
			if (isOver)
			{
				SetShow_Tooltip();
				skill_UIST.showTooltip = true;
			}
			else
			{
				skill_UIST.showTooltip = false;
			}
		}
	}

	public void skillUPCell()
	{
		skill_UIST.SkillUP(skillID);
	}
}
