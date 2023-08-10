using System;
using System.Collections.Generic;
using GUIPackage;
using JSONClass;
using KBEngine;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;
using YSGame;

public class UIBiGuanLingWuPanel : TabPanelBase
{
	public RectTransform SVContent;

	public Dropdown Fillter1;

	public Dropdown Filter2;

	public Text RightTitle;

	public Text LingWuTiaoJianText;

	public Text LingWuXiaoHaoText;

	public Text LingWuShuoMingText;

	public Image BtnImage1;

	public Image BtnImage2;

	public Material GreyMat;

	private UIIconShow tmpIcon;

	public override void OnPanelShow()
	{
		base.OnPanelShow();
		RefreshUI();
	}

	public void RefreshUI()
	{
		RefreshInventory();
		SetNull();
		UIBiGuanPanel.Inst.RefreshKeFangTime();
	}

	public void RefreshInventory()
	{
		((Transform)(object)SVContent).DestoryAllChild();
		Avatar player = PlayerEx.Player;
		int value = Fillter1.value;
		int value2 = Filter2.value;
		foreach (ITEM_INFO value3 in player.itemList.values)
		{
			if (player.FindEquipItemByUUID(value3.uuid) != null)
			{
				continue;
			}
			if (!_ItemJsonData.DataDict.ContainsKey(value3.itemId))
			{
				Debug.LogError((object)("找不到物品" + value3.itemId));
				continue;
			}
			_ItemJsonData itemJsonData = _ItemJsonData.DataDict[value3.itemId];
			JSONObject info = jsonData.instance.ItemJsonData[value3.itemId.ToString()];
			int num = itemJsonData.quality;
			if (value3.Seid != null && value3.Seid.HasField("quality"))
			{
				num = value3.Seid["quality"].I;
			}
			if ((value2 != 0 && num != value2) || value3.itemCount == 0)
			{
				continue;
			}
			bool flag = false;
			int type = itemJsonData.type;
			if (type == 3)
			{
				int num2 = Mathf.RoundToInt(float.Parse(itemJsonData.desc));
				if (SkillDatebase.instence.Dict.ContainsKey(num2))
				{
					if (SkillDatebase.instence.Dict[num2].ContainsKey(1))
					{
						GUIPackage.Skill skill = SkillDatebase.instence.Dict[num2][1];
						if (value == 0)
						{
							flag = true;
						}
						else
						{
							List<int> attackType = _skillJsonData.DataDict[skill.skill_ID].AttackType;
							if (value == 6)
							{
								foreach (int item in attackType)
								{
									if (item > 4)
									{
										flag = true;
									}
								}
							}
							else if (attackType.Contains(value - 1))
							{
								flag = true;
							}
						}
					}
					else
					{
						Debug.LogError((object)$"刷新背包时出错，id为{value3.itemId}的神通书籍出现绑定异常，Skill_ID为{num2}的神通没有第1层，无法获取数据，请检查配表");
					}
				}
				else
				{
					Debug.LogError((object)$"刷新背包时出错，id为{value3.itemId}的神通书籍出现绑定异常，没有Skill_ID为{num2}的神通，请检查配表");
				}
			}
			if (type == 4)
			{
				int num3 = Mathf.RoundToInt(float.Parse(itemJsonData.desc));
				if (SkillStaticDatebase.instence.Dict.ContainsKey(num3))
				{
					if (SkillStaticDatebase.instence.Dict[num3].ContainsKey(1))
					{
						GUIPackage.Skill skill2 = SkillStaticDatebase.instence.Dict[num3][1];
						if (value == 0)
						{
							flag = true;
						}
						else
						{
							int attackType2 = StaticSkillJsonData.DataDict[skill2.skill_ID].AttackType;
							if (attackType2 < 5)
							{
								if (attackType2 == value - 1)
								{
									flag = true;
								}
							}
							else if (value == 6)
							{
								flag = true;
							}
						}
					}
					else
					{
						Debug.LogError((object)$"刷新背包时出错，id为{value3.itemId}的功法书籍出现绑定异常，Skill_ID为{num3}的功法没有第1层，无法获取数据，请检查配表");
					}
				}
				else
				{
					Debug.LogError((object)$"刷新背包时出错，id为{value3.itemId}的功法书籍出现绑定异常，没有Skill_ID为{num3}的功法，请检查配表");
				}
			}
			if (type == 13)
			{
				if (value == 0)
				{
					flag = true;
				}
				else
				{
					List<int> wuDao = itemJsonData.wuDao;
					if (value == 6)
					{
						for (int i = 0; i < wuDao.Count; i += 2)
						{
							if (wuDao[i] > 5)
							{
								flag = true;
							}
						}
					}
					else
					{
						for (int j = 0; j < wuDao.Count; j += 2)
						{
							if (wuDao[j] == value)
							{
								flag = true;
							}
						}
					}
				}
				if (itemJsonData.name == "情报玉简" || itemJsonData.name == "天机阁情报")
				{
					flag = false;
				}
			}
			if (!flag)
			{
				continue;
			}
			UIIconShow show = Object.Instantiate<GameObject>(UIIconShow.Prefab, (Transform)(object)SVContent).GetComponent<UIIconShow>();
			show.ShowPrice = UIIconShow.ShowPriceType.None;
			show.isShowStudy = true;
			show.SetItem(value3);
			show.Count = (int)value3.itemCount;
			show.CanDrag = false;
			int day = Tools.CalcLingWuTime(value3.itemId);
			string castTimeText = Tools.getStr("xiaohaoshijian").Replace("{Y}", string.Concat(Tools.DayToYear(day))).Replace("{M}", string.Concat(Tools.DayToMonth(day)))
				.Replace("{D}", string.Concat(Tools.DayToDay(day)))
				.Replace("消耗时间：", "");
			List<int> wudaoTypeList = new List<int>();
			List<int> wudaoLvList = new List<int>();
			GUIPackage.item.GetWuDaoType(show.tmpItem.itemID, wudaoTypeList, wudaoLvList);
			string wudaotianjiantext = GUIPackage.item.StudyTiaoJian(wudaoTypeList, wudaoLvList);
			wudaotianjiantext = wudaotianjiantext.Replace("。", "");
			wudaotianjiantext = wudaotianjiantext.Replace(";", "\n");
			string shuoming = Inventory2.getSkillBookDesc(info).ToCN();
			shuoming = shuoming.Replace("[FF00FF]", "<color=#FF00FF>");
			shuoming = shuoming.Replace("[-]", "</color>");
			UIIconShow uIIconShow = show;
			uIIconShow.OnClick = (UnityAction<PointerEventData>)(object)Delegate.Combine((Delegate?)(object)uIIconShow.OnClick, (Delegate?)(object)(UnityAction<PointerEventData>)delegate
			{
				SetLingWu(show, wudaotianjiantext, castTimeText, shuoming);
				SetAllIconUnSelected();
				show.SelectedImage.SetActive(true);
				if (Input.GetMouseButton(0))
				{
					Debug.Log((object)"通过鼠标左键触发");
				}
				if (Input.GetMouseButton(1))
				{
					Debug.Log((object)"通过鼠标右键触发");
				}
			});
		}
	}

	public void SetAllIconUnSelected()
	{
		int childCount = ((Transform)SVContent).childCount;
		for (int i = 0; i < childCount; i++)
		{
			((Component)((Transform)SVContent).GetChild(i)).GetComponent<UIIconShow>().SelectedImage.SetActive(false);
		}
	}

	public void SetNull()
	{
		tmpIcon = null;
		RightTitle.text = "领悟";
		LingWuTiaoJianText.text = "";
		LingWuShuoMingText.text = "";
		LingWuXiaoHaoText.text = "";
		((Graphic)BtnImage1).material = GreyMat;
		((Graphic)BtnImage2).material = GreyMat;
	}

	public void SetLingWu(UIIconShow iconShow, string tiaojian, string castTime, string shuoming)
	{
		tmpIcon = iconShow;
		((Graphic)BtnImage1).material = null;
		((Graphic)BtnImage2).material = null;
		RightTitle.text = iconShow.tmpItem.itemName;
		LingWuTiaoJianText.text = tiaojian;
		LingWuXiaoHaoText.text = castTime;
		LingWuShuoMingText.text = shuoming;
	}

	public void OnLingWuButtonClick()
	{
		ReadBook();
	}

	public void OnFilterChanged()
	{
		RefreshUI();
	}

	public void ReadBook()
	{
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Expected O, but got Unknown
		if ((Object)(object)tmpIcon == (Object)null)
		{
			return;
		}
		int itemID = tmpIcon.tmpItem.itemID;
		string screenName = Tools.getScreenName();
		DateTime residueTime = Tools.instance.getPlayer().zulinContorl.getResidueTime(screenName);
		int num = Tools.CalcLingWuTime(itemID);
		if ((residueTime.Year - 1) * 365 + (residueTime.Month - 1) * 30 + residueTime.Day < num)
		{
			UIPopTip.Inst.Pop("房间剩余时间不足，无法学习");
			return;
		}
		tmpIcon.tmpItem.gongneng((UnityAction)delegate
		{
			Avatar player = PlayerEx.Player;
			player.removeItem(itemID);
			player.HP = player.HP_Max;
			MusicMag.instance.PlayEffectMusic(12);
			((MonoBehaviour)this).Invoke("RefreshUI", 0.1f);
		}, isTuPo: true);
	}
}
