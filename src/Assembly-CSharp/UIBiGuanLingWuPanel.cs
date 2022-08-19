using System;
using System.Collections.Generic;
using GUIPackage;
using JSONClass;
using KBEngine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using YSGame;

// Token: 0x02000288 RID: 648
public class UIBiGuanLingWuPanel : TabPanelBase
{
	// Token: 0x06001769 RID: 5993 RVA: 0x000A06BF File Offset: 0x0009E8BF
	public override void OnPanelShow()
	{
		base.OnPanelShow();
		this.RefreshUI();
	}

	// Token: 0x0600176A RID: 5994 RVA: 0x000A06CD File Offset: 0x0009E8CD
	public void RefreshUI()
	{
		this.RefreshInventory();
		this.SetNull();
		UIBiGuanPanel.Inst.RefreshKeFangTime();
	}

	// Token: 0x0600176B RID: 5995 RVA: 0x000A06E8 File Offset: 0x0009E8E8
	public void RefreshInventory()
	{
		this.SVContent.DestoryAllChild();
		Avatar player = PlayerEx.Player;
		int value = this.Fillter1.value;
		int value2 = this.Filter2.value;
		foreach (ITEM_INFO item_INFO in player.itemList.values)
		{
			if (player.FindEquipItemByUUID(item_INFO.uuid) == null)
			{
				if (!_ItemJsonData.DataDict.ContainsKey(item_INFO.itemId))
				{
					Debug.LogError("找不到物品" + item_INFO.itemId);
				}
				else
				{
					_ItemJsonData itemJsonData = _ItemJsonData.DataDict[item_INFO.itemId];
					JSONObject info = jsonData.instance.ItemJsonData[item_INFO.itemId.ToString()];
					int num = itemJsonData.quality;
					if (item_INFO.Seid != null && item_INFO.Seid.HasField("quality"))
					{
						num = item_INFO.Seid["quality"].I;
					}
					if ((value2 == 0 || num == value2) && item_INFO.itemCount > 0U)
					{
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
											using (List<int>.Enumerator enumerator2 = attackType.GetEnumerator())
											{
												while (enumerator2.MoveNext())
												{
													if (enumerator2.Current > 4)
													{
														flag = true;
													}
												}
												goto IL_233;
											}
										}
										if (attackType.Contains(value - 1))
										{
											flag = true;
										}
									}
								}
								else
								{
									Debug.LogError(string.Format("刷新背包时出错，id为{0}的神通书籍出现绑定异常，Skill_ID为{1}的神通没有第1层，无法获取数据，请检查配表", item_INFO.itemId, num2));
								}
							}
							else
							{
								Debug.LogError(string.Format("刷新背包时出错，id为{0}的神通书籍出现绑定异常，没有Skill_ID为{1}的神通，请检查配表", item_INFO.itemId, num2));
							}
						}
						IL_233:
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
									Debug.LogError(string.Format("刷新背包时出错，id为{0}的功法书籍出现绑定异常，Skill_ID为{1}的功法没有第1层，无法获取数据，请检查配表", item_INFO.itemId, num3));
								}
							}
							else
							{
								Debug.LogError(string.Format("刷新背包时出错，id为{0}的功法书籍出现绑定异常，没有Skill_ID为{1}的功法，请检查配表", item_INFO.itemId, num3));
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
						if (flag)
						{
							UIIconShow show = Object.Instantiate<GameObject>(UIIconShow.Prefab, this.SVContent).GetComponent<UIIconShow>();
							show.ShowPrice = UIIconShow.ShowPriceType.None;
							show.isShowStudy = true;
							show.SetItem(item_INFO);
							show.Count = (int)item_INFO.itemCount;
							show.CanDrag = false;
							int day = Tools.CalcLingWuTime(item_INFO.itemId);
							string castTimeText = Tools.getStr("xiaohaoshijian").Replace("{Y}", string.Concat(Tools.DayToYear(day))).Replace("{M}", string.Concat(Tools.DayToMonth(day))).Replace("{D}", string.Concat(Tools.DayToDay(day))).Replace("消耗时间：", "");
							List<int> wudaoTypeList = new List<int>();
							List<int> wudaoLvList = new List<int>();
							item.GetWuDaoType(show.tmpItem.itemID, wudaoTypeList, wudaoLvList);
							string wudaotianjiantext = item.StudyTiaoJian(wudaoTypeList, wudaoLvList);
							wudaotianjiantext = wudaotianjiantext.Replace("。", "");
							wudaotianjiantext = wudaotianjiantext.Replace(";", "\n");
							string shuoming = Inventory2.getSkillBookDesc(info).ToCN();
							shuoming = shuoming.Replace("[FF00FF]", "<color=#FF00FF>");
							shuoming = shuoming.Replace("[-]", "</color>");
							UIIconShow show2 = show;
							show2.OnClick = (UnityAction<PointerEventData>)Delegate.Combine(show2.OnClick, new UnityAction<PointerEventData>(delegate(PointerEventData b)
							{
								this.SetLingWu(show, wudaotianjiantext, castTimeText, shuoming);
								this.SetAllIconUnSelected();
								show.SelectedImage.SetActive(true);
								if (Input.GetMouseButton(0))
								{
									Debug.Log("通过鼠标左键触发");
								}
								if (Input.GetMouseButton(1))
								{
									Debug.Log("通过鼠标右键触发");
								}
							}));
						}
					}
				}
			}
		}
	}

	// Token: 0x0600176C RID: 5996 RVA: 0x000A0CD0 File Offset: 0x0009EED0
	public void SetAllIconUnSelected()
	{
		int childCount = this.SVContent.childCount;
		for (int i = 0; i < childCount; i++)
		{
			this.SVContent.GetChild(i).GetComponent<UIIconShow>().SelectedImage.SetActive(false);
		}
	}

	// Token: 0x0600176D RID: 5997 RVA: 0x000A0D14 File Offset: 0x0009EF14
	public void SetNull()
	{
		this.tmpIcon = null;
		this.RightTitle.text = "领悟";
		this.LingWuTiaoJianText.text = "";
		this.LingWuShuoMingText.text = "";
		this.LingWuXiaoHaoText.text = "";
		this.BtnImage1.material = this.GreyMat;
		this.BtnImage2.material = this.GreyMat;
	}

	// Token: 0x0600176E RID: 5998 RVA: 0x000A0D8C File Offset: 0x0009EF8C
	public void SetLingWu(UIIconShow iconShow, string tiaojian, string castTime, string shuoming)
	{
		this.tmpIcon = iconShow;
		this.BtnImage1.material = null;
		this.BtnImage2.material = null;
		this.RightTitle.text = iconShow.tmpItem.itemName;
		this.LingWuTiaoJianText.text = tiaojian;
		this.LingWuXiaoHaoText.text = castTime;
		this.LingWuShuoMingText.text = shuoming;
	}

	// Token: 0x0600176F RID: 5999 RVA: 0x000A0DF3 File Offset: 0x0009EFF3
	public void OnLingWuButtonClick()
	{
		this.ReadBook();
	}

	// Token: 0x06001770 RID: 6000 RVA: 0x000A0DFB File Offset: 0x0009EFFB
	public void OnFilterChanged()
	{
		this.RefreshUI();
	}

	// Token: 0x06001771 RID: 6001 RVA: 0x000A0E04 File Offset: 0x0009F004
	public void ReadBook()
	{
		if (this.tmpIcon == null)
		{
			return;
		}
		int itemID = this.tmpIcon.tmpItem.itemID;
		string screenName = Tools.getScreenName();
		DateTime residueTime = Tools.instance.getPlayer().zulinContorl.getResidueTime(screenName);
		int num = Tools.CalcLingWuTime(itemID);
		if ((residueTime.Year - 1) * 365 + (residueTime.Month - 1) * 30 + residueTime.Day < num)
		{
			UIPopTip.Inst.Pop("房间剩余时间不足，无法学习", PopTipIconType.叹号);
			return;
		}
		this.tmpIcon.tmpItem.gongneng(delegate
		{
			Avatar player = PlayerEx.Player;
			player.removeItem(itemID);
			player.HP = player.HP_Max;
			MusicMag.instance.PlayEffectMusic(12, 1f);
			this.Invoke("RefreshUI", 0.1f);
		}, true);
	}

	// Token: 0x04001222 RID: 4642
	public RectTransform SVContent;

	// Token: 0x04001223 RID: 4643
	public Dropdown Fillter1;

	// Token: 0x04001224 RID: 4644
	public Dropdown Filter2;

	// Token: 0x04001225 RID: 4645
	public Text RightTitle;

	// Token: 0x04001226 RID: 4646
	public Text LingWuTiaoJianText;

	// Token: 0x04001227 RID: 4647
	public Text LingWuXiaoHaoText;

	// Token: 0x04001228 RID: 4648
	public Text LingWuShuoMingText;

	// Token: 0x04001229 RID: 4649
	public Image BtnImage1;

	// Token: 0x0400122A RID: 4650
	public Image BtnImage2;

	// Token: 0x0400122B RID: 4651
	public Material GreyMat;

	// Token: 0x0400122C RID: 4652
	private UIIconShow tmpIcon;
}
