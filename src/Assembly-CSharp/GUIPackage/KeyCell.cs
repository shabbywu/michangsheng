using System;
using KBEngine;
using UnityEngine;
using UnityEngine.UI;

namespace GUIPackage;

public class KeyCell : MonoBehaviour
{
	public GameObject Icon;

	public GameObject Num;

	public item keyItem;

	public Skill keySkill;

	public int KeyItemID;

	public int KeyCallType;

	public Text SkillName;

	public Image IconImage;

	private ItemDatebase itemDatebase;

	private Avatar avatar;

	public ITEM_INFO wepen;

	public bool isFight;

	public bool hasKeyCell = true;

	public float StartPressTime;

	public bool IsPress;

	private UISprite cdFill;

	private bool isException;

	public void longPress()
	{
		if (IsPress)
		{
			if (keySkill.skill_ID != -1 && RealTime.time - StartPressTime > 0.8f)
			{
				OnHover(isOver: true);
			}
		}
		else
		{
			OnHover(isOver: false);
		}
	}

	private void Start()
	{
		itemDatebase = ((Component)jsonData.instance).gameObject.GetComponent<ItemDatebase>();
		avatar = Tools.instance.getPlayer();
		if (isFight)
		{
			((Component)Icon.transform.GetChild(2)).gameObject.SetActive(false);
		}
		KeyItemID = -1;
		cdFill = ((Component)Icon.transform.GetChild(0)).GetComponent<UISprite>();
	}

	private void Update()
	{
		try
		{
			if (!isException)
			{
				Show_Date();
				if (RoundManager.KeyHideCD < 0f)
				{
					UesKey();
				}
				Icon_CoolDown();
			}
		}
		catch (Exception ex)
		{
			isException = true;
			Debug.LogError((object)("KeyCell.Update出现异常:" + ex.Message + "\n" + ex.StackTrace));
		}
	}

	protected void Icon_CoolDown()
	{
		if (keySkill.skill_ID != -1)
		{
			if (keySkill.CurCD != 0f)
			{
				if (wepen != null && wepen.uuid != "" && wepen.uuid != null && RoundManager.instance.WeaponSkillList.ContainsKey(wepen.uuid))
				{
					cdFill.fillAmount = keySkill.CurCD / keySkill.CoolDown;
					if (RoundManager.instance.WeaponSkillList[wepen.uuid] > 0)
					{
						((Component)Icon.transform.GetChild(0).GetChild(0)).GetComponent<UILabel>().text = string.Concat(RoundManager.instance.WeaponSkillList[wepen.uuid]);
					}
				}
				else
				{
					cdFill.fillAmount = keySkill.CurCD / keySkill.CoolDown;
					if (avatar.SkillSeidFlag.ContainsKey(29) && avatar.SkillSeidFlag[29].ContainsKey(keySkill.skill_ID) && avatar.SkillSeidFlag[29][keySkill.skill_ID] > 0)
					{
						((Component)Icon.transform.GetChild(0).GetChild(0)).GetComponent<UILabel>().text = string.Concat(avatar.SkillSeidFlag[29][keySkill.skill_ID]);
					}
				}
			}
			else
			{
				Transform val = Icon.transform.Find("Sprite/Label");
				if ((Object)(object)val != (Object)null && (Object)(object)((Component)val).GetComponent<UILabel>() != (Object)null)
				{
					((Component)val).GetComponent<UILabel>().text = "";
				}
				cdFill.fillAmount = 0f;
			}
			SkillCanUseType skillCanUseType = keySkill.CanUse(PlayerEx.Player, PlayerEx.Player, showError: false);
			if (skillCanUseType != SkillCanUseType.可以使用 && skillCanUseType != SkillCanUseType.尚未冷却不能使用)
			{
				cdFill.fillAmount = 1f;
			}
		}
		else
		{
			cdFill.fillAmount = 0f;
		}
		if (isFight)
		{
			if (RoundManager.instance.ChoiceSkill == keySkill)
			{
				((Component)Icon.transform.GetChild(1)).gameObject.SetActive(true);
			}
			else
			{
				((Component)Icon.transform.GetChild(1)).gameObject.SetActive(false);
			}
			showLianJiHightLight();
		}
	}

	public void showLianJiHightLight()
	{
		if (keySkill.skill_ID == -1)
		{
			return;
		}
		bool active = false;
		if (jsonData.instance.skillJsonData[string.Concat(keySkill.skill_ID)]["seid"].list.Find((JSONObject aa) => jsonData.instance.hightLightSkillID.Contains((int)aa.n)) != null)
		{
			Avatar player = Tools.instance.getPlayer();
			if (player == null || player.OtherAvatar == null)
			{
				return;
			}
			if (keySkill.CanUse(player, player, showError: false) == SkillCanUseType.可以使用)
			{
				Avatar receiver = player;
				if (jsonData.instance.skillJsonData[string.Concat(keySkill.skill_ID)]["script"].str == "SkillAttack")
				{
					receiver = player.OtherAvatar;
				}
				if (keySkill.CanRealizeSeid(player, receiver))
				{
					active = true;
				}
			}
		}
		((Component)Icon.transform.GetChild(2)).gameObject.SetActive(active);
	}

	protected void UesKey()
	{
		if (hasKeyCell && Input.GetKeyDown(((Object)this).name.ToLower()) && Tools.instance.getPlayer().state == 3)
		{
			if (keyItem.itemID != -1)
			{
				Singleton.inventory.UseItem(KeyItemID);
			}
			else if (keySkill.skill_ID != -1 && !(((Object)this).name == "Tab"))
			{
				RoundManager.instance.SetChoiceSkill(ref keySkill);
			}
		}
	}

	protected void Show_Date()
	{
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_014b: Unknown result type (might be due to invalid IL or missing references)
		if ((Object)(object)keyItem.itemIcon != (Object)null)
		{
			Icon.GetComponent<UITexture>().mainTexture = (Texture)(object)keyItem.itemIcon;
		}
		else if ((Object)(object)keySkill.skill_Icon != (Object)null)
		{
			if (KeyCallType == 1 && (Object)(object)IconImage.sprite.texture != (Object)(object)itemDatebase.items[wepen.itemId].itemIcon)
			{
				IconImage.sprite = Sprite.Create(itemDatebase.items[wepen.itemId].itemIcon, new Rect(0f, 0f, 128f, 128f), new Vector2(128f, 128f));
			}
			else
			{
				Icon.GetComponent<UITexture>().mainTexture = (Texture)(object)keySkill.skill_Icon;
			}
			if (KeyCallType == 1 && keySkill.ItemAddSeid != null)
			{
				IconImage.sprite = Sprite.Create(keySkill.skill_Icon, new Rect(0f, 0f, 128f, 128f), new Vector2(128f, 128f));
			}
		}
		else
		{
			Icon.GetComponent<UITexture>().mainTexture = null;
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

	protected void OnHover(bool isOver)
	{
		if (keySkill.skill_ID == -1)
		{
			return;
		}
		if (isOver)
		{
			RoundManager.instance.ToolitpSkill.clearItem();
			if (KeyCallType == 1)
			{
				RoundManager.instance.ItemToolitpSkill.setItemText(keySkill.skill_ID, wepen);
				RoundManager.instance.ItemToolitpSkill.showTooltip = true;
			}
			else
			{
				RoundManager.instance.ToolitpSkill.setText(keySkill.skill_ID, keySkill);
				RoundManager.instance.ToolitpSkill.showTooltip = true;
			}
		}
		else
		{
			RoundManager.instance.ItemToolitpSkill.showTooltip = false;
			RoundManager.instance.ToolitpSkill.showTooltip = false;
		}
	}

	protected void OnPress()
	{
		if (Input.GetMouseButtonDown(0) && Tools.instance.getPlayer().state == 3 && keySkill.skill_ID != -1)
		{
			StartPressTime = RealTime.time;
			IsPress = true;
			RoundManager.instance.SetChoiceSkill(ref keySkill);
			OnHover(isOver: true);
		}
	}
}
