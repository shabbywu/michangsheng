using System;
using System.Collections.Generic;
using System.Linq;
using KBEngine;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;

public class UINPCQingJiao : MonoBehaviour, IESCClose
{
	private UINPCData npc;

	private bool inited;

	public List<RectTransform> RTList = new List<RectTransform>();

	public Image ShenTongBtnImage;

	public Image GongFaBtnImage;

	public Image ShenTongBtnIconImage;

	public Image GongFaBtnIconImage;

	public Text ShenTongBtnText;

	public Text GongFaBtnText;

	public Sprite ShenTongTabSprite;

	public Sprite ShenTongLoseSprite;

	public Sprite GongFaTabSprite;

	public Sprite GongFaLoseSprite;

	public Sprite TabBGSprite;

	public Sprite TabBGLoseSprite;

	public Text ShuXiText;

	public Text YouShanText;

	public Text QinMiText;

	public Text QingFenText;

	public Image ShuXiLockImage;

	public Image YouShanLockImage;

	public Image QinMiLockImage;

	private Color TabColor = new Color(0.972549f, 72f / 85f, 0.6392157f);

	private Color TabLoseColor = new Color(24f / 85f, 0.58431375f, 29f / 51f);

	private Color TextColor1 = new Color(42f / 85f, 0.88235295f, 0.8509804f);

	private Color TextColor2 = new Color(0.47843137f, 20f / 51f, 0.2901961f);

	public GameObject FavorShuXi;

	public GameObject FavorYouShan;

	public GameObject FavorQinMi;

	public GameObject IconPrefab;

	private int nowType;

	private static Dictionary<int, int> FavorDict = new Dictionary<int, int>
	{
		{ 1, 5 },
		{ 2, 6 },
		{ 3, 8 }
	};

	private static List<UINPCQingJiaoSkillData> SkillDataList = new List<UINPCQingJiaoSkillData>();

	private static Dictionary<int, Dictionary<int, UINPCQingJiaoSkillData>> QJSkillDict = new Dictionary<int, Dictionary<int, UINPCQingJiaoSkillData>>();

	private void Start()
	{
	}

	private void Update()
	{
		AutoHide();
	}

	public bool CanShow()
	{
		if (UINPCJiaoHu.AllShouldHide)
		{
			return false;
		}
		if ((Object)(object)PanelMamager.inst != (Object)null && (Object)(object)PanelMamager.inst.UISceneGameObject == (Object)null)
		{
			return false;
		}
		if ((Object)(object)PanelMamager.inst != (Object)null && PanelMamager.inst.nowPanel != PanelMamager.PanelType.空)
		{
			return false;
		}
		return true;
	}

	private void AutoHide()
	{
		if (!CanShow())
		{
			UINPCJiaoHu.Inst.HideNPCQingJiaoPanel();
		}
	}

	public void RefreshUI()
	{
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0184: Unknown result type (might be due to invalid IL or missing references)
		//IL_0195: Unknown result type (might be due to invalid IL or missing references)
		//IL_0916: Unknown result type (might be due to invalid IL or missing references)
		//IL_0920: Expected O, but got Unknown
		//IL_05d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e2: Expected O, but got Unknown
		Init();
		Avatar player = Tools.instance.getPlayer();
		npc = UINPCJiaoHu.Inst.NowJiaoHuNPC;
		npc.RefreshData();
		bool isShiFu = PlayerEx.IsTheather(npc.ID);
		FavorShuXi.SetActive(false);
		FavorYouShan.SetActive(false);
		FavorQinMi.SetActive(false);
		QingFenText.text = npc.QingFen.ToString();
		((Graphic)ShuXiText).color = TextColor1;
		((Graphic)YouShanText).color = TextColor1;
		((Graphic)QinMiText).color = TextColor1;
		((Behaviour)ShuXiLockImage).enabled = false;
		((Behaviour)YouShanLockImage).enabled = false;
		((Behaviour)QinMiLockImage).enabled = false;
		if (npc.FavorLevel >= FavorDict[3])
		{
			FavorQinMi.SetActive(true);
		}
		else if (npc.FavorLevel >= FavorDict[2])
		{
			FavorYouShan.SetActive(true);
			((Graphic)QinMiText).color = TextColor2;
			((Behaviour)QinMiLockImage).enabled = true;
		}
		else if (npc.FavorLevel >= FavorDict[1])
		{
			FavorShuXi.SetActive(true);
			((Graphic)QinMiText).color = TextColor2;
			((Graphic)YouShanText).color = TextColor2;
			((Behaviour)YouShanLockImage).enabled = true;
			((Behaviour)QinMiLockImage).enabled = true;
		}
		else
		{
			((Graphic)ShuXiText).color = TextColor2;
			((Graphic)QinMiText).color = TextColor2;
			((Graphic)YouShanText).color = TextColor2;
			((Behaviour)ShuXiLockImage).enabled = true;
			((Behaviour)YouShanLockImage).enabled = true;
			((Behaviour)QinMiLockImage).enabled = true;
		}
		List<UINPCQingJiaoSkillData> list = SkillDataList.Where((UINPCQingJiaoSkillData d) => d.LiuPai == npc.LiuPai && d.NPCLevel <= npc.Level).ToList();
		foreach (RectTransform rT in RTList)
		{
			ClearChild(rT);
		}
		for (int i = 1; i <= 3; i++)
		{
			int level = i;
			if (nowType == 0)
			{
				List<UINPCQingJiaoSkillData.SData> list2 = new List<UINPCQingJiaoSkillData.SData>();
				foreach (UINPCQingJiaoSkillData item in list)
				{
					foreach (UINPCQingJiaoSkillData.SData d5 in item.StaticSkills.Where((UINPCQingJiaoSkillData.SData s) => s.Quality == level).ToList())
					{
						if (list2.Find((UINPCQingJiaoSkillData.SData s) => s.SkillID == d5.SkillID) != null)
						{
							for (int j = 0; j < list2.Count; j++)
							{
								if (list2[j].SkillID == d5.SkillID && list2[j].ID < d5.ID)
								{
									list2[j] = d5;
								}
							}
						}
						else
						{
							list2.Add(d5);
						}
					}
				}
				foreach (UINPCQingJiaoSkillData.SData exQingJiaoStaticSkill in npc.ExQingJiaoStaticSkills)
				{
					if (!list2.Contains(exQingJiaoStaticSkill) && exQingJiaoStaticSkill.Quality == level)
					{
						list2.Add(exQingJiaoStaticSkill);
					}
				}
				foreach (UINPCQingJiaoSkillData.SData d4 in list2)
				{
					UIIconShow icon2 = Object.Instantiate<GameObject>(IconPrefab, (Transform)(object)RTList[level - 1]).GetComponent<UIIconShow>();
					icon2.SetStaticSkill(d4.ID, showStudy: true);
					icon2.CanDrag = false;
					JSONObject skill2 = jsonData.instance.StaticSkillJsonData.list.Find((JSONObject s) => s["id"].I == d4.ID);
					int qingjiaotype2 = skill2["qingjiaotype"].I;
					bool isMiChuan2 = qingjiaotype2 == 3 || qingjiaotype2 == 5;
					if (isMiChuan2 && npc.IsNingZhouNPC && !isShiFu)
					{
						if (!PlayerEx.IsNingZhouMaxShengWangLevel())
						{
							icon2.SetBuChuan();
						}
					}
					else if (isMiChuan2 && !npc.IsNingZhouNPC && !isShiFu)
					{
						if (!PlayerEx.IsSeaMaxShengWangLevel())
						{
							icon2.SetBuChuan();
						}
					}
					else if (qingjiaotype2 == 6 && !isShiFu)
					{
						icon2.NowJiaoBiao = UIIconShow.JiaoBiaoType.Mo;
					}
					UnityAction action2 = (UnityAction)delegate
					{
						if (icon2.IsLingWu)
						{
							UIPopTip.Inst.Pop("你已经领悟这个功法了");
						}
						else
						{
							UINPCJiaoHu.Inst.QingJiaoName = skill2["name"].Str.RemoveNumber();
							if (isMiChuan2 && npc.IsNingZhouNPC && !isShiFu)
							{
								if (!PlayerEx.IsNingZhouMaxShengWangLevel())
								{
									UIPopTip.Inst.Pop("你的声望不足以学习此功法");
									UINPCJiaoHu.Inst.IsQingJiaoShiBaiSW = true;
									return;
								}
							}
							else if (isMiChuan2 && !npc.IsNingZhouNPC && !isShiFu)
							{
								if (!PlayerEx.IsSeaMaxShengWangLevel())
								{
									UIPopTip.Inst.Pop("你的声望不足以学习此功法");
									UINPCJiaoHu.Inst.IsQingJiaoShiBaiSW = true;
									return;
								}
							}
							else if (qingjiaotype2 == 6 && !isShiFu)
							{
								UIPopTip.Inst.Pop("此乃魔道不传之密");
								UINPCJiaoHu.Inst.IsQingJiaoShiBaiSW = true;
								return;
							}
							if (npc.FavorLevel < FavorDict[level])
							{
								UIPopTip.Inst.Pop("你的好感度不足以学习此功法");
							}
							else
							{
								int qingFenCost2 = NPCEx.GetQingFenCost(skill2, isGongFa: true);
								if (npc.QingFen < qingFenCost2)
								{
									UIPopTip.Inst.Pop("你们的情分不足以学习此功法");
									UINPCJiaoHu.Inst.IsQingJiaoShiBaiQF = true;
								}
								else
								{
									NPCEx.AddQingFen(npc.ID, -qingFenCost2);
									UINPCJiaoHu.Inst.JiaoHuItemID = UINPCQingJiaoSkillData.GongFaItemDict[skill2["Skill_ID"].I];
									player.addItem(UINPCJiaoHu.Inst.JiaoHuItemID, 1, null, ShowText: true);
									UINPCJiaoHu.Inst.IsQingJiaoChengGong = true;
									NpcJieSuanManager.inst.isUpDateNpcList = true;
								}
							}
						}
					};
					UIIconShow uIIconShow = icon2;
					uIIconShow.OnClick = (UnityAction<PointerEventData>)(object)Delegate.Combine((Delegate?)(object)uIIconShow.OnClick, (Delegate?)(object)(UnityAction<PointerEventData>)delegate
					{
						USelectBox.Show("是否确定请教此功法？", action2);
					});
				}
				continue;
			}
			List<UINPCQingJiaoSkillData.SData> list3 = new List<UINPCQingJiaoSkillData.SData>();
			foreach (UINPCQingJiaoSkillData item2 in list)
			{
				foreach (UINPCQingJiaoSkillData.SData d3 in item2.Skills.Where((UINPCQingJiaoSkillData.SData s) => s.Quality == level).ToList())
				{
					if (list3.Where((UINPCQingJiaoSkillData.SData s) => s.SkillID == d3.SkillID && s.ID >= d3.ID).Count() <= 0)
					{
						list3.Add(d3);
					}
				}
			}
			foreach (UINPCQingJiaoSkillData.SData exQingJiaoSkill in npc.ExQingJiaoSkills)
			{
				if (!list3.Contains(exQingJiaoSkill) && exQingJiaoSkill.Quality == level)
				{
					list3.Add(exQingJiaoSkill);
				}
			}
			foreach (UINPCQingJiaoSkillData.SData d2 in list3)
			{
				UIIconShow icon = Object.Instantiate<GameObject>(IconPrefab, (Transform)(object)RTList[level - 1]).GetComponent<UIIconShow>();
				icon.SetSkill(d2.ID, showStudy: true);
				icon.CanDrag = false;
				JSONObject skill = jsonData.instance._skillJsonData.list.Find((JSONObject s) => s["id"].I == d2.ID);
				int qingjiaotype = skill["qingjiaotype"].I;
				bool isMiChuan = qingjiaotype == 3 || qingjiaotype == 5;
				if (isMiChuan && npc.IsNingZhouNPC && !isShiFu)
				{
					if (!PlayerEx.IsNingZhouMaxShengWangLevel())
					{
						icon.SetBuChuan();
					}
				}
				else if (isMiChuan && !npc.IsNingZhouNPC && !isShiFu)
				{
					if (!PlayerEx.IsSeaMaxShengWangLevel())
					{
						icon.SetBuChuan();
					}
				}
				else if (qingjiaotype == 6 && !isShiFu)
				{
					icon.NowJiaoBiao = UIIconShow.JiaoBiaoType.Mo;
				}
				UnityAction action = (UnityAction)delegate
				{
					if (icon.IsLingWu)
					{
						UIPopTip.Inst.Pop("你已经领悟这个神通了");
					}
					else
					{
						UINPCJiaoHu.Inst.QingJiaoName = skill["name"].Str.RemoveNumber();
						if (isMiChuan && npc.IsNingZhouNPC && !isShiFu)
						{
							if (!PlayerEx.IsNingZhouMaxShengWangLevel())
							{
								UIPopTip.Inst.Pop("你的声望不足以学习此神通");
								UINPCJiaoHu.Inst.IsQingJiaoShiBaiSW = true;
								return;
							}
						}
						else if (isMiChuan && !npc.IsNingZhouNPC && !isShiFu)
						{
							if (!PlayerEx.IsSeaMaxShengWangLevel())
							{
								UIPopTip.Inst.Pop("你的声望不足以学习此神通");
								UINPCJiaoHu.Inst.IsQingJiaoShiBaiSW = true;
								return;
							}
						}
						else if (qingjiaotype == 6 && !isShiFu)
						{
							UIPopTip.Inst.Pop("此乃魔道不传之密");
							UINPCJiaoHu.Inst.IsQingJiaoShiBaiSW = true;
							return;
						}
						if (npc.FavorLevel < FavorDict[level])
						{
							UIPopTip.Inst.Pop("你的好感度不足以学习此神通");
						}
						else
						{
							int qingFenCost = NPCEx.GetQingFenCost(skill, isGongFa: false);
							if (npc.QingFen < qingFenCost)
							{
								UIPopTip.Inst.Pop("你们的情分不足以学习此神通");
								UINPCJiaoHu.Inst.IsQingJiaoShiBaiQF = true;
							}
							else
							{
								NPCEx.AddQingFen(npc.ID, -qingFenCost);
								UINPCJiaoHu.Inst.JiaoHuItemID = UINPCQingJiaoSkillData.SkillItemDict[skill["Skill_ID"].I];
								player.addItem(UINPCJiaoHu.Inst.JiaoHuItemID, 1, null, ShowText: true);
								UINPCJiaoHu.Inst.IsQingJiaoChengGong = true;
								NpcJieSuanManager.inst.isUpDateNpcList = true;
							}
						}
					}
				};
				UIIconShow uIIconShow2 = icon;
				uIIconShow2.OnClick = (UnityAction<PointerEventData>)(object)Delegate.Combine((Delegate?)(object)uIIconShow2.OnClick, (Delegate?)(object)(UnityAction<PointerEventData>)delegate
				{
					USelectBox.Show("是否确定请教此神通？", action);
				});
			}
		}
	}

	private void Init()
	{
		if (inited)
		{
			return;
		}
		foreach (JSONObject item in jsonData.instance.NPCLeiXingDate.list)
		{
			SkillDataList.Add(new UINPCQingJiaoSkillData(item));
		}
		foreach (UINPCQingJiaoSkillData skillData in SkillDataList)
		{
			if (!QJSkillDict.ContainsKey(skillData.LiuPai))
			{
				QJSkillDict.Add(skillData.LiuPai, new Dictionary<int, UINPCQingJiaoSkillData>());
			}
			if (!QJSkillDict[skillData.LiuPai].ContainsKey(skillData.NPCLevel))
			{
				QJSkillDict[skillData.LiuPai].Add(skillData.NPCLevel, skillData);
			}
		}
		inited = true;
	}

	public void OnShenTongBtnClick()
	{
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		if (nowType != 1)
		{
			nowType = 1;
			ShenTongBtnImage.sprite = TabBGSprite;
			ShenTongBtnIconImage.sprite = ShenTongTabSprite;
			((Graphic)ShenTongBtnText).color = TabColor;
			GongFaBtnImage.sprite = TabBGLoseSprite;
			GongFaBtnIconImage.sprite = GongFaLoseSprite;
			((Graphic)GongFaBtnText).color = TabLoseColor;
			RefreshUI();
		}
	}

	public void OnGongFaBtnClick()
	{
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		if (nowType != 0)
		{
			nowType = 0;
			ShenTongBtnImage.sprite = TabBGLoseSprite;
			ShenTongBtnIconImage.sprite = ShenTongLoseSprite;
			((Graphic)ShenTongBtnText).color = TabLoseColor;
			GongFaBtnImage.sprite = TabBGSprite;
			GongFaBtnIconImage.sprite = GongFaTabSprite;
			((Graphic)GongFaBtnText).color = TabColor;
			RefreshUI();
		}
	}

	public void ClearChild(RectTransform t)
	{
		List<GameObject> list = new List<GameObject>();
		for (int i = 0; i < ((Transform)t).childCount; i++)
		{
			GameObject gameObject = ((Component)((Transform)t).GetChild(i)).gameObject;
			if (((Object)gameObject).name != "Lock")
			{
				list.Add(gameObject);
			}
		}
		foreach (GameObject item in list)
		{
			Object.Destroy((Object)(object)item);
		}
	}

	public bool TryEscClose()
	{
		UINPCJiaoHu.Inst.HideNPCQingJiaoPanel();
		return true;
	}
}
