using KBEngine;
using UnityEngine;
using UnityEngine.Events;

namespace GUIPackage;

public class KeyCellMapSkill : MonoBehaviour
{
	public GameObject Icon;

	public GameObject Num;

	public item keyItem;

	public Skill keySkill;

	public GameObject PingZhi;

	public int KeyItemID;

	public bool CanClick = true;

	public GameObject KeyName;

	public UILabel NameLabel;

	public bool showName;

	private void Start()
	{
		Icon = ((Component)((Component)this).transform.Find("Icon")).gameObject;
		Num = ((Component)((Component)this).transform.Find("num")).gameObject;
		KeyItemID = -1;
		((MonoBehaviour)this).Invoke("loadKey", 1f);
	}

	public void loadKey()
	{
	}

	private void Update()
	{
		Show_Date();
		Icon_CoolDown();
		if (!((Object)(object)KeyName == (Object)null))
		{
			if (Tools.instance.getPlayer().showSkillName == 0 && showName && keySkill.skill_ID != -1)
			{
				KeyName.SetActive(true);
				NameLabel.text = Tools.instance.getSkillName(keySkill.skill_ID, includecolor: true);
			}
			else
			{
				KeyName.SetActive(false);
			}
		}
	}

	private void Icon_CoolDown()
	{
		if (keySkill.skill_ID != -1)
		{
			if (keySkill.CurCD != 0f)
			{
				Icon.GetComponentInChildren<UISprite>().fillAmount = keySkill.CurCD / keySkill.CoolDown;
			}
			else
			{
				Icon.GetComponentInChildren<UISprite>().fillAmount = 0f;
			}
		}
		else
		{
			Icon.GetComponentInChildren<UISprite>().fillAmount = 0f;
		}
	}

	private void UesKey()
	{
	}

	private void OnHover(bool isOver)
	{
		PCOnHover(isOver);
	}

	public void PCOnHover(bool isOver)
	{
		if (keySkill.skill_ID != -1)
		{
			if (isOver)
			{
				Singleton.skillUI.Show_Tooltip(keySkill);
				Singleton.skillUI.showTooltip = true;
			}
			else
			{
				Singleton.skillUI.showTooltip = false;
			}
		}
	}

	private void OnDrop(GameObject obj)
	{
		if (Input.GetMouseButtonUp(0) && CanClick && Singleton.skillUI.draggingSkill)
		{
			chengeSkill();
		}
	}

	private void Show_Date()
	{
		if ((Object)(object)keyItem.itemIcon != (Object)null)
		{
			Icon.GetComponent<UITexture>().mainTexture = (Texture)(object)keyItem.itemIcon;
		}
		else if ((Object)(object)keySkill.skill_Icon != (Object)null)
		{
			Icon.GetComponent<UITexture>().mainTexture = (Texture)(object)keySkill.skill_Icon;
		}
		else
		{
			Icon.GetComponent<UITexture>().mainTexture = null;
		}
		if ((Object)(object)PingZhi != (Object)null)
		{
			PingZhi.GetComponent<UITexture>().mainTexture = (Texture)(object)keySkill.SkillPingZhi;
		}
		if (keyItem.itemNum != 0)
		{
			Num.GetComponent<UILabel>().text = keyItem.itemNum.ToString();
		}
		else
		{
			Num.GetComponent<UILabel>().text = null;
		}
	}

	private void chengeSkill()
	{
		if (Singleton.skillUI.draggingSkill)
		{
			Avatar avatar = (Avatar)KBEngineApp.app.player();
			if (jsonData.instance.skillJsonData[Singleton.skillUI.dragedSkill.skill_ID.ToString()]["AttackType"].list.Find((JSONObject aaa) => (int)aaa.n == 13) != null)
			{
				UIPopTip.Inst.Pop("秘术仅在结丹时自动生效");
				Singleton.skillUI.Clear_Draged();
				return;
			}
			if (jsonData.instance.skillJsonData[Singleton.skillUI.dragedSkill.skill_ID.ToString()]["AttackType"].list.Find((JSONObject aaa) => (int)aaa.n == 14 || (int)aaa.n == 15 || (int)aaa.n == 16) != null)
			{
				UIPopTip.Inst.Pop("秘术仅在结婴时自动生效");
				Singleton.skillUI.Clear_Draged();
				return;
			}
			if (jsonData.instance.skillJsonData[Singleton.skillUI.dragedSkill.skill_ID.ToString()]["AttackType"].list.Find((JSONObject aaa) => (int)aaa.n == 18) != null)
			{
				UIPopTip.Inst.Pop("秘术仅在化神时自动生效");
				Singleton.skillUI.Clear_Draged();
				return;
			}
			Singleton.key.Clear_MapSkillKye(Singleton.skillUI.dragedSkill);
			if (keySkill.skill_ID != -1)
			{
				avatar.UnEquipSkill(Tools.instance.getSkillIDByKey(keySkill.skill_ID));
			}
			keySkill = Singleton.skillUI.dragedSkill;
			Singleton.skillUI.Clear_Draged();
			avatar.equipSkill(Tools.instance.getSkillIDByKey(keySkill.skill_ID), int.Parse(((Object)((Component)this).transform).name));
			KeyItemID = -1;
			keyItem = new item();
		}
		else if (Singleton.key.draggingKey)
		{
			keyItem = Singleton.inventory.dragedItem;
			KeyItemID = Singleton.inventory.dragedID;
			Singleton.inventory.Clear_dragedItem();
			keySkill = new Skill();
		}
		else if (keySkill.skill_ID != -1)
		{
			((Avatar)KBEngineApp.app.player()).UnEquipSkill(Tools.instance.getSkillIDByKey(keySkill.skill_ID));
			Singleton.skillUI.dragedSkill = keySkill;
			Singleton.skillUI.draggingSkill = true;
			keySkill = new Skill();
		}
	}

	private void OnPress()
	{
		if (CanClick)
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
		if (Input.GetMouseButton(0))
		{
			chengeSkill();
		}
	}
}
