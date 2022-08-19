using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x02000276 RID: 630
public class UINPCQingJiao : MonoBehaviour, IESCClose
{
	// Token: 0x060016FB RID: 5883 RVA: 0x00004095 File Offset: 0x00002295
	private void Start()
	{
	}

	// Token: 0x060016FC RID: 5884 RVA: 0x0009C52B File Offset: 0x0009A72B
	private void Update()
	{
		this.AutoHide();
	}

	// Token: 0x060016FD RID: 5885 RVA: 0x0009C534 File Offset: 0x0009A734
	public bool CanShow()
	{
		return !UINPCJiaoHu.AllShouldHide && (!(PanelMamager.inst != null) || !(PanelMamager.inst.UISceneGameObject == null)) && (!(PanelMamager.inst != null) || PanelMamager.inst.nowPanel == PanelMamager.PanelType.空);
	}

	// Token: 0x060016FE RID: 5886 RVA: 0x0009C588 File Offset: 0x0009A788
	private void AutoHide()
	{
		if (!this.CanShow())
		{
			UINPCJiaoHu.Inst.HideNPCQingJiaoPanel();
		}
	}

	// Token: 0x060016FF RID: 5887 RVA: 0x0009C59C File Offset: 0x0009A79C
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
				foreach (UINPCQingJiaoSkillData.SData sdata in this.npc.ExQingJiaoStaticSkills)
				{
					if (!list2.Contains(sdata) && sdata.Quality == CS$<>8__locals2.level)
					{
						list2.Add(sdata);
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
					goto IL_965;
				}
				goto IL_62A;
			}
			goto IL_62A;
			IL_965:
			i++;
			continue;
			IL_62A:
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
			foreach (UINPCQingJiaoSkillData.SData sdata2 in this.npc.ExQingJiaoSkills)
			{
				if (!list3.Contains(sdata2) && sdata2.Quality == CS$<>8__locals2.level)
				{
					list3.Add(sdata2);
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
			goto IL_965;
		}
	}

	// Token: 0x06001700 RID: 5888 RVA: 0x0009CFF8 File Offset: 0x0009B1F8
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

	// Token: 0x06001701 RID: 5889 RVA: 0x0009D10C File Offset: 0x0009B30C
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

	// Token: 0x06001702 RID: 5890 RVA: 0x0009D198 File Offset: 0x0009B398
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

	// Token: 0x06001703 RID: 5891 RVA: 0x0009D220 File Offset: 0x0009B420
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

	// Token: 0x06001704 RID: 5892 RVA: 0x0009D2A8 File Offset: 0x0009B4A8
	public bool TryEscClose()
	{
		UINPCJiaoHu.Inst.HideNPCQingJiaoPanel();
		return true;
	}

	// Token: 0x04001189 RID: 4489
	private UINPCData npc;

	// Token: 0x0400118A RID: 4490
	private bool inited;

	// Token: 0x0400118B RID: 4491
	public List<RectTransform> RTList = new List<RectTransform>();

	// Token: 0x0400118C RID: 4492
	public Image ShenTongBtnImage;

	// Token: 0x0400118D RID: 4493
	public Image GongFaBtnImage;

	// Token: 0x0400118E RID: 4494
	public Image ShenTongBtnIconImage;

	// Token: 0x0400118F RID: 4495
	public Image GongFaBtnIconImage;

	// Token: 0x04001190 RID: 4496
	public Text ShenTongBtnText;

	// Token: 0x04001191 RID: 4497
	public Text GongFaBtnText;

	// Token: 0x04001192 RID: 4498
	public Sprite ShenTongTabSprite;

	// Token: 0x04001193 RID: 4499
	public Sprite ShenTongLoseSprite;

	// Token: 0x04001194 RID: 4500
	public Sprite GongFaTabSprite;

	// Token: 0x04001195 RID: 4501
	public Sprite GongFaLoseSprite;

	// Token: 0x04001196 RID: 4502
	public Sprite TabBGSprite;

	// Token: 0x04001197 RID: 4503
	public Sprite TabBGLoseSprite;

	// Token: 0x04001198 RID: 4504
	public Text ShuXiText;

	// Token: 0x04001199 RID: 4505
	public Text YouShanText;

	// Token: 0x0400119A RID: 4506
	public Text QinMiText;

	// Token: 0x0400119B RID: 4507
	public Text QingFenText;

	// Token: 0x0400119C RID: 4508
	public Image ShuXiLockImage;

	// Token: 0x0400119D RID: 4509
	public Image YouShanLockImage;

	// Token: 0x0400119E RID: 4510
	public Image QinMiLockImage;

	// Token: 0x0400119F RID: 4511
	private Color TabColor = new Color(0.972549f, 0.84705883f, 0.6392157f);

	// Token: 0x040011A0 RID: 4512
	private Color TabLoseColor = new Color(0.28235295f, 0.58431375f, 0.5686275f);

	// Token: 0x040011A1 RID: 4513
	private Color TextColor1 = new Color(0.49411765f, 0.88235295f, 0.8509804f);

	// Token: 0x040011A2 RID: 4514
	private Color TextColor2 = new Color(0.47843137f, 0.39215687f, 0.2901961f);

	// Token: 0x040011A3 RID: 4515
	public GameObject FavorShuXi;

	// Token: 0x040011A4 RID: 4516
	public GameObject FavorYouShan;

	// Token: 0x040011A5 RID: 4517
	public GameObject FavorQinMi;

	// Token: 0x040011A6 RID: 4518
	public GameObject IconPrefab;

	// Token: 0x040011A7 RID: 4519
	private int nowType;

	// Token: 0x040011A8 RID: 4520
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

	// Token: 0x040011A9 RID: 4521
	private static List<UINPCQingJiaoSkillData> SkillDataList = new List<UINPCQingJiaoSkillData>();

	// Token: 0x040011AA RID: 4522
	private static Dictionary<int, Dictionary<int, UINPCQingJiaoSkillData>> QJSkillDict = new Dictionary<int, Dictionary<int, UINPCQingJiaoSkillData>>();
}
