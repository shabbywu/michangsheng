using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x0200038F RID: 911
public class UINPCQingJiao : MonoBehaviour, IESCClose
{
	// Token: 0x060019AD RID: 6573 RVA: 0x000042DD File Offset: 0x000024DD
	private void Start()
	{
	}

	// Token: 0x060019AE RID: 6574 RVA: 0x000161D8 File Offset: 0x000143D8
	private void Update()
	{
		this.AutoHide();
	}

	// Token: 0x060019AF RID: 6575 RVA: 0x000E2284 File Offset: 0x000E0484
	public bool CanShow()
	{
		return !UINPCJiaoHu.AllShouldHide && (!(PanelMamager.inst != null) || !(PanelMamager.inst.UISceneGameObject == null)) && (!(PanelMamager.inst != null) || PanelMamager.inst.nowPanel == PanelMamager.PanelType.空);
	}

	// Token: 0x060019B0 RID: 6576 RVA: 0x000161E0 File Offset: 0x000143E0
	private void AutoHide()
	{
		if (!this.CanShow())
		{
			UINPCJiaoHu.Inst.HideNPCQingJiaoPanel();
		}
	}

	// Token: 0x060019B1 RID: 6577 RVA: 0x000E3B88 File Offset: 0x000E1D88
	public void RefreshUI()
	{
		UINPCQingJiao.<>c__DisplayClass36_0 CS$<>8__locals1 = new UINPCQingJiao.<>c__DisplayClass36_0();
		CS$<>8__locals1.<>4__this = this;
		this.Init();
		CS$<>8__locals1.player = Tools.instance.getPlayer();
		this.npc = UINPCJiaoHu.Inst.NowJiaoHuNPC;
		this.npc.RefreshData();
		CS$<>8__locals1.isShiFu = PlayerEx.IsTheather(this.npc.ID);
		this.FavorShuXi.SetActive(false);
		this.FavorYouShan.SetActive(false);
		this.FavorQinMi.SetActive(false);
		this.QingFenText.text = this.npc.QingFen.ToString();
		this.ShuXiText.color = this.TextColor1;
		this.YouShanText.color = this.TextColor1;
		this.QinMiText.color = this.TextColor1;
		this.ShuXiLockImage.enabled = false;
		this.YouShanLockImage.enabled = false;
		this.QinMiLockImage.enabled = false;
		if (this.npc.FavorLevel >= UINPCQingJiao.FavorDict[3])
		{
			this.FavorQinMi.SetActive(true);
		}
		else if (this.npc.FavorLevel >= UINPCQingJiao.FavorDict[2])
		{
			this.FavorYouShan.SetActive(true);
			this.QinMiText.color = this.TextColor2;
			this.QinMiLockImage.enabled = true;
		}
		else if (this.npc.FavorLevel >= UINPCQingJiao.FavorDict[1])
		{
			this.FavorShuXi.SetActive(true);
			this.QinMiText.color = this.TextColor2;
			this.YouShanText.color = this.TextColor2;
			this.YouShanLockImage.enabled = true;
			this.QinMiLockImage.enabled = true;
		}
		else
		{
			this.ShuXiText.color = this.TextColor2;
			this.QinMiText.color = this.TextColor2;
			this.YouShanText.color = this.TextColor2;
			this.ShuXiLockImage.enabled = true;
			this.YouShanLockImage.enabled = true;
			this.QinMiLockImage.enabled = true;
		}
		List<UINPCQingJiaoSkillData> list = (from d in UINPCQingJiao.SkillDataList
		where d.LiuPai == CS$<>8__locals1.<>4__this.npc.LiuPai && d.NPCLevel <= CS$<>8__locals1.<>4__this.npc.Level
		select d).ToList<UINPCQingJiaoSkillData>();
		foreach (RectTransform t in this.RTList)
		{
			this.ClearChild(t);
		}
		int i = 1;
		while (i <= 3)
		{
			UINPCQingJiao.<>c__DisplayClass36_1 CS$<>8__locals2 = new UINPCQingJiao.<>c__DisplayClass36_1();
			CS$<>8__locals2.CS$<>8__locals1 = CS$<>8__locals1;
			CS$<>8__locals2.level = i;
			if (this.nowType == 0)
			{
				List<UINPCQingJiaoSkillData.SData> list2 = new List<UINPCQingJiaoSkillData.SData>();
				foreach (UINPCQingJiaoSkillData uinpcqingJiaoSkillData in list)
				{
					IEnumerable<UINPCQingJiaoSkillData.SData> staticSkills = uinpcqingJiaoSkillData.StaticSkills;
					Func<UINPCQingJiaoSkillData.SData, bool> predicate;
					if ((predicate = CS$<>8__locals2.<>9__1) == null)
					{
						predicate = (CS$<>8__locals2.<>9__1 = ((UINPCQingJiaoSkillData.SData s) => s.Quality == CS$<>8__locals2.level));
					}
					using (List<UINPCQingJiaoSkillData.SData>.Enumerator enumerator3 = staticSkills.Where(predicate).ToList<UINPCQingJiaoSkillData.SData>().GetEnumerator())
					{
						while (enumerator3.MoveNext())
						{
							UINPCQingJiaoSkillData.SData d = enumerator3.Current;
							if (list2.Find((UINPCQingJiaoSkillData.SData s) => s.SkillID == d.SkillID) != null)
							{
								for (int j = 0; j < list2.Count; j++)
								{
									if (list2[j].SkillID == d.SkillID && list2[j].ID < d.ID)
									{
										list2[j] = d;
									}
								}
							}
							else
							{
								list2.Add(d);
							}
						}
					}
				}
				using (List<UINPCQingJiaoSkillData.SData>.Enumerator enumerator3 = list2.GetEnumerator())
				{
					while (enumerator3.MoveNext())
					{
						UINPCQingJiao.<>c__DisplayClass36_3 CS$<>8__locals4 = new UINPCQingJiao.<>c__DisplayClass36_3();
						CS$<>8__locals4.CS$<>8__locals2 = CS$<>8__locals2;
						CS$<>8__locals4.d = enumerator3.Current;
						UIIconShow icon = Object.Instantiate<GameObject>(this.IconPrefab, this.RTList[CS$<>8__locals4.CS$<>8__locals2.level - 1]).GetComponent<UIIconShow>();
						icon.SetStaticSkill(CS$<>8__locals4.d.ID, true);
						icon.CanDrag = false;
						JSONObject skill = jsonData.instance.StaticSkillJsonData.list.Find((JSONObject s) => s["id"].I == CS$<>8__locals4.d.ID);
						int qingjiaotype = skill["qingjiaotype"].I;
						bool isMiChuan = qingjiaotype == 3 || qingjiaotype == 5;
						if (isMiChuan && this.npc.IsNingZhouNPC && !CS$<>8__locals4.CS$<>8__locals2.CS$<>8__locals1.isShiFu)
						{
							if (!PlayerEx.IsNingZhouMaxShengWangLevel())
							{
								icon.SetBuChuan();
							}
						}
						else if (isMiChuan && !this.npc.IsNingZhouNPC && !CS$<>8__locals4.CS$<>8__locals2.CS$<>8__locals1.isShiFu)
						{
							if (!PlayerEx.IsSeaMaxShengWangLevel())
							{
								icon.SetBuChuan();
							}
						}
						else if (qingjiaotype == 6 && !CS$<>8__locals4.CS$<>8__locals2.CS$<>8__locals1.isShiFu)
						{
							icon.NowJiaoBiao = UIIconShow.JiaoBiaoType.Mo;
						}
						UnityAction action = delegate()
						{
							if (icon.IsLingWu)
							{
								UIPopTip.Inst.Pop("你已经领悟这个功法了", PopTipIconType.叹号);
								return;
							}
							UINPCJiaoHu.Inst.QingJiaoName = skill["name"].Str.RemoveNumber();
							if (isMiChuan && CS$<>8__locals4.CS$<>8__locals2.CS$<>8__locals1.<>4__this.npc.IsNingZhouNPC && !CS$<>8__locals4.CS$<>8__locals2.CS$<>8__locals1.isShiFu)
							{
								if (!PlayerEx.IsNingZhouMaxShengWangLevel())
								{
									UIPopTip.Inst.Pop("你的声望不足以学习此功法", PopTipIconType.叹号);
									UINPCJiaoHu.Inst.IsQingJiaoShiBaiSW = true;
									return;
								}
							}
							else if (isMiChuan && !CS$<>8__locals4.CS$<>8__locals2.CS$<>8__locals1.<>4__this.npc.IsNingZhouNPC && !CS$<>8__locals4.CS$<>8__locals2.CS$<>8__locals1.isShiFu)
							{
								if (!PlayerEx.IsSeaMaxShengWangLevel())
								{
									UIPopTip.Inst.Pop("你的声望不足以学习此功法", PopTipIconType.叹号);
									UINPCJiaoHu.Inst.IsQingJiaoShiBaiSW = true;
									return;
								}
							}
							else if (qingjiaotype == 6 && !CS$<>8__locals4.CS$<>8__locals2.CS$<>8__locals1.isShiFu)
							{
								UIPopTip.Inst.Pop("此乃魔道不传之密", PopTipIconType.叹号);
								UINPCJiaoHu.Inst.IsQingJiaoShiBaiSW = true;
								return;
							}
							if (CS$<>8__locals4.CS$<>8__locals2.CS$<>8__locals1.<>4__this.npc.FavorLevel < UINPCQingJiao.FavorDict[CS$<>8__locals4.CS$<>8__locals2.level])
							{
								UIPopTip.Inst.Pop("你的好感度不足以学习此功法", PopTipIconType.叹号);
								return;
							}
							int qingFenCost = NPCEx.GetQingFenCost(skill, true);
							if (CS$<>8__locals4.CS$<>8__locals2.CS$<>8__locals1.<>4__this.npc.QingFen < qingFenCost)
							{
								UIPopTip.Inst.Pop("你们的情分不足以学习此功法", PopTipIconType.叹号);
								UINPCJiaoHu.Inst.IsQingJiaoShiBaiQF = true;
								return;
							}
							NPCEx.AddQingFen(CS$<>8__locals4.CS$<>8__locals2.CS$<>8__locals1.<>4__this.npc.ID, -qingFenCost, false);
							UINPCJiaoHu.Inst.JiaoHuItemID = UINPCQingJiaoSkillData.GongFaItemDict[skill["Skill_ID"].I];
							CS$<>8__locals4.CS$<>8__locals2.CS$<>8__locals1.player.addItem(UINPCJiaoHu.Inst.JiaoHuItemID, 1, null, true);
							UINPCJiaoHu.Inst.IsQingJiaoChengGong = true;
							NpcJieSuanManager.inst.isUpDateNpcList = true;
						};
						UIIconShow icon3 = icon;
						icon3.OnClick = (UnityAction<PointerEventData>)Delegate.Combine(icon3.OnClick, new UnityAction<PointerEventData>(delegate(PointerEventData b)
						{
							USelectBox.Show("是否确定请教此功法？", action, null);
						}));
					}
					goto IL_8B1;
				}
				goto IL_5D0;
			}
			goto IL_5D0;
			IL_8B1:
			i++;
			continue;
			IL_5D0:
			List<UINPCQingJiaoSkillData.SData> list3 = new List<UINPCQingJiaoSkillData.SData>();
			foreach (UINPCQingJiaoSkillData uinpcqingJiaoSkillData2 in list)
			{
				IEnumerable<UINPCQingJiaoSkillData.SData> skills = uinpcqingJiaoSkillData2.Skills;
				Func<UINPCQingJiaoSkillData.SData, bool> predicate2;
				if ((predicate2 = CS$<>8__locals2.<>9__6) == null)
				{
					predicate2 = (CS$<>8__locals2.<>9__6 = ((UINPCQingJiaoSkillData.SData s) => s.Quality == CS$<>8__locals2.level));
				}
				using (List<UINPCQingJiaoSkillData.SData>.Enumerator enumerator3 = skills.Where(predicate2).ToList<UINPCQingJiaoSkillData.SData>().GetEnumerator())
				{
					while (enumerator3.MoveNext())
					{
						UINPCQingJiaoSkillData.SData d = enumerator3.Current;
						if ((from s in list3
						where s.SkillID == d.SkillID && s.ID >= d.ID
						select s).Count<UINPCQingJiaoSkillData.SData>() <= 0)
						{
							list3.Add(d);
						}
					}
				}
			}
			using (List<UINPCQingJiaoSkillData.SData>.Enumerator enumerator3 = list3.GetEnumerator())
			{
				while (enumerator3.MoveNext())
				{
					UINPCQingJiao.<>c__DisplayClass36_6 CS$<>8__locals7 = new UINPCQingJiao.<>c__DisplayClass36_6();
					CS$<>8__locals7.CS$<>8__locals4 = CS$<>8__locals2;
					CS$<>8__locals7.d = enumerator3.Current;
					UIIconShow icon = Object.Instantiate<GameObject>(this.IconPrefab, this.RTList[CS$<>8__locals7.CS$<>8__locals4.level - 1]).GetComponent<UIIconShow>();
					icon.SetSkill(CS$<>8__locals7.d.ID, true, 1);
					icon.CanDrag = false;
					JSONObject skill = jsonData.instance._skillJsonData.list.Find((JSONObject s) => s["id"].I == CS$<>8__locals7.d.ID);
					int qingjiaotype = skill["qingjiaotype"].I;
					bool isMiChuan = qingjiaotype == 3 || qingjiaotype == 5;
					if (isMiChuan && this.npc.IsNingZhouNPC && !CS$<>8__locals7.CS$<>8__locals4.CS$<>8__locals1.isShiFu)
					{
						if (!PlayerEx.IsNingZhouMaxShengWangLevel())
						{
							icon.SetBuChuan();
						}
					}
					else if (isMiChuan && !this.npc.IsNingZhouNPC && !CS$<>8__locals7.CS$<>8__locals4.CS$<>8__locals1.isShiFu)
					{
						if (!PlayerEx.IsSeaMaxShengWangLevel())
						{
							icon.SetBuChuan();
						}
					}
					else if (qingjiaotype == 6 && !CS$<>8__locals7.CS$<>8__locals4.CS$<>8__locals1.isShiFu)
					{
						icon.NowJiaoBiao = UIIconShow.JiaoBiaoType.Mo;
					}
					UnityAction action = delegate()
					{
						if (icon.IsLingWu)
						{
							UIPopTip.Inst.Pop("你已经领悟这个神通了", PopTipIconType.叹号);
							return;
						}
						UINPCJiaoHu.Inst.QingJiaoName = skill["name"].Str.RemoveNumber();
						if (isMiChuan && CS$<>8__locals7.CS$<>8__locals4.CS$<>8__locals1.<>4__this.npc.IsNingZhouNPC && !CS$<>8__locals7.CS$<>8__locals4.CS$<>8__locals1.isShiFu)
						{
							if (!PlayerEx.IsNingZhouMaxShengWangLevel())
							{
								UIPopTip.Inst.Pop("你的声望不足以学习此神通", PopTipIconType.叹号);
								UINPCJiaoHu.Inst.IsQingJiaoShiBaiSW = true;
								return;
							}
						}
						else if (isMiChuan && !CS$<>8__locals7.CS$<>8__locals4.CS$<>8__locals1.<>4__this.npc.IsNingZhouNPC && !CS$<>8__locals7.CS$<>8__locals4.CS$<>8__locals1.isShiFu)
						{
							if (!PlayerEx.IsSeaMaxShengWangLevel())
							{
								UIPopTip.Inst.Pop("你的声望不足以学习此神通", PopTipIconType.叹号);
								UINPCJiaoHu.Inst.IsQingJiaoShiBaiSW = true;
								return;
							}
						}
						else if (qingjiaotype == 6 && !CS$<>8__locals7.CS$<>8__locals4.CS$<>8__locals1.isShiFu)
						{
							UIPopTip.Inst.Pop("此乃魔道不传之密", PopTipIconType.叹号);
							UINPCJiaoHu.Inst.IsQingJiaoShiBaiSW = true;
							return;
						}
						if (CS$<>8__locals7.CS$<>8__locals4.CS$<>8__locals1.<>4__this.npc.FavorLevel < UINPCQingJiao.FavorDict[CS$<>8__locals7.CS$<>8__locals4.level])
						{
							UIPopTip.Inst.Pop("你的好感度不足以学习此神通", PopTipIconType.叹号);
							return;
						}
						int qingFenCost = NPCEx.GetQingFenCost(skill, false);
						if (CS$<>8__locals7.CS$<>8__locals4.CS$<>8__locals1.<>4__this.npc.QingFen < qingFenCost)
						{
							UIPopTip.Inst.Pop("你们的情分不足以学习此神通", PopTipIconType.叹号);
							UINPCJiaoHu.Inst.IsQingJiaoShiBaiQF = true;
							return;
						}
						NPCEx.AddQingFen(CS$<>8__locals7.CS$<>8__locals4.CS$<>8__locals1.<>4__this.npc.ID, -qingFenCost, false);
						UINPCJiaoHu.Inst.JiaoHuItemID = UINPCQingJiaoSkillData.SkillItemDict[skill["Skill_ID"].I];
						CS$<>8__locals7.CS$<>8__locals4.CS$<>8__locals1.player.addItem(UINPCJiaoHu.Inst.JiaoHuItemID, 1, null, true);
						UINPCJiaoHu.Inst.IsQingJiaoChengGong = true;
						NpcJieSuanManager.inst.isUpDateNpcList = true;
					};
					UIIconShow icon2 = icon;
					icon2.OnClick = (UnityAction<PointerEventData>)Delegate.Combine(icon2.OnClick, new UnityAction<PointerEventData>(delegate(PointerEventData b)
					{
						USelectBox.Show("是否确定请教此神通？", action, null);
					}));
				}
			}
			goto IL_8B1;
		}
	}

	// Token: 0x060019B2 RID: 6578 RVA: 0x000E4500 File Offset: 0x000E2700
	private void Init()
	{
		if (!this.inited)
		{
			foreach (JSONObject json in jsonData.instance.NPCLeiXingDate.list)
			{
				UINPCQingJiao.SkillDataList.Add(new UINPCQingJiaoSkillData(json));
			}
			foreach (UINPCQingJiaoSkillData uinpcqingJiaoSkillData in UINPCQingJiao.SkillDataList)
			{
				if (!UINPCQingJiao.QJSkillDict.ContainsKey(uinpcqingJiaoSkillData.LiuPai))
				{
					UINPCQingJiao.QJSkillDict.Add(uinpcqingJiaoSkillData.LiuPai, new Dictionary<int, UINPCQingJiaoSkillData>());
				}
				if (!UINPCQingJiao.QJSkillDict[uinpcqingJiaoSkillData.LiuPai].ContainsKey(uinpcqingJiaoSkillData.NPCLevel))
				{
					UINPCQingJiao.QJSkillDict[uinpcqingJiaoSkillData.LiuPai].Add(uinpcqingJiaoSkillData.NPCLevel, uinpcqingJiaoSkillData);
				}
			}
			this.inited = true;
		}
	}

	// Token: 0x060019B3 RID: 6579 RVA: 0x000E4614 File Offset: 0x000E2814
	public void OnShenTongBtnClick()
	{
		if (this.nowType != 1)
		{
			this.nowType = 1;
			this.ShenTongBtnImage.sprite = this.TabBGSprite;
			this.ShenTongBtnIconImage.sprite = this.ShenTongTabSprite;
			this.ShenTongBtnText.color = this.TabColor;
			this.GongFaBtnImage.sprite = this.TabBGLoseSprite;
			this.GongFaBtnIconImage.sprite = this.GongFaLoseSprite;
			this.GongFaBtnText.color = this.TabLoseColor;
			this.RefreshUI();
		}
	}

	// Token: 0x060019B4 RID: 6580 RVA: 0x000E46A0 File Offset: 0x000E28A0
	public void OnGongFaBtnClick()
	{
		if (this.nowType != 0)
		{
			this.nowType = 0;
			this.ShenTongBtnImage.sprite = this.TabBGLoseSprite;
			this.ShenTongBtnIconImage.sprite = this.ShenTongLoseSprite;
			this.ShenTongBtnText.color = this.TabLoseColor;
			this.GongFaBtnImage.sprite = this.TabBGSprite;
			this.GongFaBtnIconImage.sprite = this.GongFaTabSprite;
			this.GongFaBtnText.color = this.TabColor;
			this.RefreshUI();
		}
	}

	// Token: 0x060019B5 RID: 6581 RVA: 0x000E4728 File Offset: 0x000E2928
	public void ClearChild(RectTransform t)
	{
		List<GameObject> list = new List<GameObject>();
		for (int i = 0; i < t.childCount; i++)
		{
			GameObject gameObject = t.GetChild(i).gameObject;
			if (gameObject.name != "Lock")
			{
				list.Add(gameObject);
			}
		}
		foreach (GameObject gameObject2 in list)
		{
			Object.Destroy(gameObject2);
		}
	}

	// Token: 0x060019B6 RID: 6582 RVA: 0x000161F4 File Offset: 0x000143F4
	public bool TryEscClose()
	{
		UINPCJiaoHu.Inst.HideNPCQingJiaoPanel();
		return true;
	}

	// Token: 0x040014DA RID: 5338
	private UINPCData npc;

	// Token: 0x040014DB RID: 5339
	private bool inited;

	// Token: 0x040014DC RID: 5340
	public List<RectTransform> RTList = new List<RectTransform>();

	// Token: 0x040014DD RID: 5341
	public Image ShenTongBtnImage;

	// Token: 0x040014DE RID: 5342
	public Image GongFaBtnImage;

	// Token: 0x040014DF RID: 5343
	public Image ShenTongBtnIconImage;

	// Token: 0x040014E0 RID: 5344
	public Image GongFaBtnIconImage;

	// Token: 0x040014E1 RID: 5345
	public Text ShenTongBtnText;

	// Token: 0x040014E2 RID: 5346
	public Text GongFaBtnText;

	// Token: 0x040014E3 RID: 5347
	public Sprite ShenTongTabSprite;

	// Token: 0x040014E4 RID: 5348
	public Sprite ShenTongLoseSprite;

	// Token: 0x040014E5 RID: 5349
	public Sprite GongFaTabSprite;

	// Token: 0x040014E6 RID: 5350
	public Sprite GongFaLoseSprite;

	// Token: 0x040014E7 RID: 5351
	public Sprite TabBGSprite;

	// Token: 0x040014E8 RID: 5352
	public Sprite TabBGLoseSprite;

	// Token: 0x040014E9 RID: 5353
	public Text ShuXiText;

	// Token: 0x040014EA RID: 5354
	public Text YouShanText;

	// Token: 0x040014EB RID: 5355
	public Text QinMiText;

	// Token: 0x040014EC RID: 5356
	public Text QingFenText;

	// Token: 0x040014ED RID: 5357
	public Image ShuXiLockImage;

	// Token: 0x040014EE RID: 5358
	public Image YouShanLockImage;

	// Token: 0x040014EF RID: 5359
	public Image QinMiLockImage;

	// Token: 0x040014F0 RID: 5360
	private Color TabColor = new Color(0.972549f, 0.84705883f, 0.6392157f);

	// Token: 0x040014F1 RID: 5361
	private Color TabLoseColor = new Color(0.28235295f, 0.58431375f, 0.5686275f);

	// Token: 0x040014F2 RID: 5362
	private Color TextColor1 = new Color(0.49411765f, 0.88235295f, 0.8509804f);

	// Token: 0x040014F3 RID: 5363
	private Color TextColor2 = new Color(0.47843137f, 0.39215687f, 0.2901961f);

	// Token: 0x040014F4 RID: 5364
	public GameObject FavorShuXi;

	// Token: 0x040014F5 RID: 5365
	public GameObject FavorYouShan;

	// Token: 0x040014F6 RID: 5366
	public GameObject FavorQinMi;

	// Token: 0x040014F7 RID: 5367
	public GameObject IconPrefab;

	// Token: 0x040014F8 RID: 5368
	private int nowType;

	// Token: 0x040014F9 RID: 5369
	private static Dictionary<int, int> FavorDict = new Dictionary<int, int>
	{
		{
			1,
			5
		},
		{
			2,
			6
		},
		{
			3,
			8
		}
	};

	// Token: 0x040014FA RID: 5370
	private static List<UINPCQingJiaoSkillData> SkillDataList = new List<UINPCQingJiaoSkillData>();

	// Token: 0x040014FB RID: 5371
	private static Dictionary<int, Dictionary<int, UINPCQingJiaoSkillData>> QJSkillDict = new Dictionary<int, Dictionary<int, UINPCQingJiaoSkillData>>();
}
