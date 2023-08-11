using KBEngine;
using UnityEngine;
using UnityEngine.Events;

namespace GUIPackage;

public class KeyCellMapPassSkill : MonoBehaviour
{
	public GameObject Icon;

	public GameObject Num;

	public item keyItem;

	public Skill keySkill;

	public GameObject PingZhi;

	public int KeyItemID;

	public GameObject KeyName;

	public UILabel NameLabel;

	public bool CanClick = true;

	public bool IsDunsu;

	public bool showName;

	private void Start()
	{
		Icon = ((Component)((Component)this).transform.Find("Icon")).gameObject;
		Num = ((Component)((Component)this).transform.Find("num")).gameObject;
		KeyItemID = -1;
		((MonoBehaviour)this).Invoke("loadKey", 0.5f);
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
				NameLabel.text = Tools.instance.getStaticSkillName(keySkill.skill_ID);
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
				Singleton.skillUI2.Show_Tooltip(keySkill);
				Singleton.skillUI2.showTooltip = true;
			}
			else
			{
				Singleton.skillUI2.showTooltip = false;
			}
		}
		if (isOver && Input.GetMouseButtonUp(0) && CanClick && Singleton.skillUI2.draggingSkill)
		{
			chengeSkill();
		}
	}

	private void OnDrop(GameObject obj)
	{
		if (Input.GetMouseButtonUp(0) && CanClick && Singleton.skillUI2.draggingSkill)
		{
			chengeSkill();
		}
	}

	private void chengeSkill()
	{
		if (Singleton.skillUI2.draggingSkill)
		{
			if (IsDunsu)
			{
				if (jsonData.instance.StaticSkillJsonData[Singleton.skillUI2.dragedSkill.skill_ID.ToString()]["AttackType"].n != 6f)
				{
					UIPopTip.Inst.Pop(Tools.getStr("bushidunshu"));
					return;
				}
			}
			else if (jsonData.instance.StaticSkillJsonData[Singleton.skillUI2.dragedSkill.skill_ID.ToString()]["AttackType"].n == 6f)
			{
				UIPopTip.Inst.Pop(Tools.getStr("dunsuleijineng"));
				return;
			}
			Avatar avatar = (Avatar)KBEngineApp.app.player();
			if (keySkill.skill_ID != -1)
			{
				avatar.UnEquipStaticSkill(Tools.instance.getStaticSkillKeyByID(keySkill.skill_ID));
			}
			Singleton.key2.Clear_MapSkillPassKye(Singleton.skillUI2.dragedSkill);
			keySkill = Singleton.skillUI2.dragedSkill;
			Singleton.skillUI2.Clear_Draged();
			avatar.equipStaticSkill(Tools.instance.getStaticSkillIDByKey(keySkill.skill_ID), int.Parse(((Object)((Component)this).transform).name));
			KeyItemID = -1;
			keyItem = new item();
		}
		else if (Singleton.key2.draggingKey)
		{
			keyItem = Singleton.inventory.dragedItem;
			KeyItemID = Singleton.inventory.dragedID;
			Singleton.inventory.Clear_dragedItem();
			keySkill = new Skill();
		}
		else if (keySkill.skill_ID != -1)
		{
			((Avatar)KBEngineApp.app.player()).UnEquipStaticSkill(Tools.instance.getStaticSkillIDByKey(keySkill.skill_ID));
			Singleton.skillUI2.dragedSkill = keySkill;
			Singleton.skillUI2.draggingSkill = true;
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
