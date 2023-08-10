using UnityEngine;
using UnityEngine.Events;

namespace GUIPackage;

public class SkillCell : MonoBehaviour
{
	public GameObject Icon;

	public GameObject Level;

	public GameObject Name;

	public GameObject PingZhi;

	public int skillID;

	public GameObject KeyName;

	public UILabel NameLabel;

	public bool showName;

	private float refreshCD;

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
			PingZhi.GetComponent<UITexture>().mainTexture = null;
			if ((Object)(object)KeyName != (Object)null)
			{
				KeyName.SetActive(false);
			}
			return;
		}
		Icon.GetComponent<UITexture>().mainTexture = (Texture)(object)Singleton.skillUI.skill[skillID].skill_Icon;
		Level.GetComponent<UILabel>().text = "Lv" + Singleton.skillUI.skill[skillID].skill_level + "/" + Singleton.skillUI.skill[skillID].Max_level;
		Name.GetComponent<UILabel>().text = Singleton.skillUI.skill[skillID].skill_Name;
		PingZhi.GetComponent<UITexture>().mainTexture = (Texture)(object)Singleton.skillUI.skill[skillID].SkillPingZhi;
		if (!((Object)(object)KeyName == (Object)null))
		{
			if (Tools.instance.getPlayer().showSkillName == 0 && showName)
			{
				KeyName.SetActive(true);
				NameLabel.text = Tools.instance.getSkillName(Singleton.skillUI.skill[skillID].skill_ID, includecolor: true);
			}
			else
			{
				KeyName.SetActive(false);
			}
		}
	}

	private void OnPress()
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
		Singleton.skillUI.showTooltip = false;
		if (Input.GetMouseButton(0) && !Singleton.inventory.draggingItem && !Singleton.key.draggingKey && Singleton.skillUI.skill[skillID].CoolDown != 0f)
		{
			Singleton.skillUI.draggingSkill = true;
			Singleton.skillUI.dragedSkill = Singleton.skillUI.skill[skillID];
		}
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
				Singleton.skillUI.Show_Tooltip(Singleton.skillUI.skill[skillID]);
				Singleton.skillUI.showTooltip = true;
			}
			else
			{
				Singleton.skillUI.showTooltip = false;
			}
		}
	}
}
