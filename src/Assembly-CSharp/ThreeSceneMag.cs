using System;
using System.Collections;
using System.Collections.Generic;
using Fungus;
using GUIPackage;
using KBEngine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Token: 0x020003D3 RID: 979
public class ThreeSceneMag : MonoBehaviour
{
	// Token: 0x17000273 RID: 627
	// (get) Token: 0x06001FCC RID: 8140 RVA: 0x000E0080 File Offset: 0x000DE280
	public int getNowAvatar
	{
		get
		{
			return this.nowAvatar;
		}
	}

	// Token: 0x06001FCD RID: 8141 RVA: 0x000E0088 File Offset: 0x000DE288
	public void init()
	{
		UINPCData.ThreeSceneNPCTalkCache.Clear();
		UINPCData.ThreeSceneZhongYaoNPCTalkCache.Clear();
		UINPCJiaoHu.Inst.TNPCIDList.Clear();
		this.NPCChoice.SetActive(false);
		int num = -1;
		try
		{
			num = int.Parse(SceneManager.GetActiveScene().name.Replace("S", ""));
		}
		catch (Exception)
		{
		}
		this.avatar = Tools.instance.getPlayer();
		if (this.CraftingList != null && this.CraftingList.transform.childCount > 0)
		{
			Tools.ClearObj(this.CraftingList.transform.GetChild(0));
		}
		foreach (JSONObject jsonobject in jsonData.instance.ThreeSenceJsonData.list)
		{
			if (jsonobject["SceneID"].I == num)
			{
				this.addTalkAvatar(jsonobject["id"].I);
			}
		}
		int num2 = this.avatar.nomelTaskMag.AutoThreeSceneHasNTask();
		if (num2 != -1)
		{
			JSONObject nowChildIDSuiJiJson = this.avatar.nomelTaskMag.GetNowChildIDSuiJiJson(num2);
			int index = this.avatar.nomelTaskMag.nowChildNTask(num2);
			JSONObject TalkJson = this.avatar.nomelTaskMag.GetXiangXi(num2, index);
			int avatarID = NPCEx.NPCIDToNew(nowChildIDSuiJiJson["Value"].I);
			this.AddAvatarObj(avatarID, delegate
			{
				GlobalValue.Set(400, avatarID, "ThreeSceneMag.init");
				this.nowAvatar = avatarID;
				JSONObject talkJson = TalkJson;
				Object.Instantiate<GameObject>(Resources.Load<GameObject>("talkPrefab/TalkPrefab/Talk" + talkJson["talkID"].str));
			});
		}
		NpcJieSuanManager.inst.isUpDateNpcList = true;
	}

	// Token: 0x06001FCE RID: 8142 RVA: 0x000E0260 File Offset: 0x000DE460
	public void xiaLaBtn()
	{
		this.CraftingList.transform.parent.parent.GetComponent<ScrollRect>().verticalNormalizedPosition = 0f;
	}

	// Token: 0x06001FCF RID: 8143 RVA: 0x000E0288 File Offset: 0x000DE488
	public void addTalkAvatar(int key)
	{
		JSONObject jsonobject = jsonData.instance.ThreeSenceJsonData[string.Concat(key)];
		bool flag = true;
		if (jsonData.instance.MonstarIsDeath(jsonobject["AvatarID"].I))
		{
			flag = false;
		}
		if (jsonobject["Level"].Count > 0 && ((int)jsonobject["Level"][0].n > (int)this.avatar.level || (int)this.avatar.level > (int)jsonobject["Level"][1].n))
		{
			flag = false;
		}
		if (jsonobject["StarTime"].str != "" && jsonobject["EndTime"].str != "")
		{
			DateTime t = DateTime.Parse(jsonobject["StarTime"].str);
			DateTime t2 = DateTime.Parse(jsonobject["EndTime"].str);
			DateTime nowTime = this.avatar.worldTimeMag.getNowTime();
			if ((int)jsonobject["circulation"].n > 0 && nowTime.Year % (int)jsonobject["circulation"].n == t.Year % (int)jsonobject["circulation"].n)
			{
				nowTime = new DateTime(t.Year, nowTime.Month, nowTime.Day);
			}
			if (!(t <= nowTime) || !(nowTime <= t2))
			{
				flag = false;
			}
		}
		for (int i = 0; i < jsonobject["SaticValue"].list.Count; i++)
		{
			if (jsonobject["SaticValue"][i].n > 0f)
			{
				int num = 0;
				if (jsonobject.HasField("SaticValueX") && jsonobject["SaticValueX"].Count > i)
				{
					num = (int)jsonobject["SaticValueX"][i].n;
				}
				if (GlobalValue.Get(jsonobject["SaticValue"][i].I, "ThreeSceneMag.addTalkAvatar") == num)
				{
					flag = false;
				}
			}
		}
		if (flag)
		{
			int _TempID = NPCEx.NPCIDToNew(jsonobject["AvatarID"].I);
			this.AddAvatarObj(_TempID, delegate
			{
				GlobalValue.Set(400, _TempID, "ThreeSceneMag.addTalkAvatar");
				this.nowAvatar = key;
				this.openChoic();
			});
		}
	}

	// Token: 0x06001FD0 RID: 8144 RVA: 0x000E0534 File Offset: 0x000DE734
	public void AddAvatarObj(int AvatarID, UnityAction Next)
	{
		if (NPCEx.IsDeath(AvatarID))
		{
			return;
		}
		UINPCJiaoHu.Inst.TNPCIDList.Add(AvatarID);
		int key;
		if (NPCEx.IsZhongYaoNPC(AvatarID, out key))
		{
			UINPCData.ThreeSceneZhongYaoNPCTalkCache.Add(key, Next);
			return;
		}
		UINPCData.ThreeSceneNPCTalkCache.Add(AvatarID, Next);
	}

	// Token: 0x06001FD1 RID: 8145 RVA: 0x000E0580 File Offset: 0x000DE780
	public void openChoic()
	{
		this.NPCChoice.gameObject.SetActive(true);
		JSONObject jsonobject = jsonData.instance.ThreeSenceJsonData[string.Concat(this.nowAvatar)];
		if ((int)jsonobject["transaction"].n == 1)
		{
			this.NPCChoice.transform.Find("UIGrid/exchange").gameObject.SetActive(true);
		}
		else
		{
			this.NPCChoice.transform.Find("UIGrid/exchange").gameObject.SetActive(false);
		}
		if ((int)jsonobject["transaction2"].n == 1)
		{
			this.NPCChoice.transform.Find("UIGrid/GongFa").gameObject.SetActive(true);
		}
		else
		{
			this.NPCChoice.transform.Find("UIGrid/GongFa").gameObject.SetActive(false);
		}
		int i = jsonobject["AvatarID"].I;
		if ((int)jsonobject["transaction3"].n == 1 && (int)jsonData.instance.AvatarRandomJsonData[string.Concat(i)]["HaoGanDu"].n >= 40 && (int)Tools.instance.getPlayer().level >= jsonobject["qiecuoLv"].I)
		{
			this.NPCChoice.transform.Find("UIGrid/qiecuo").gameObject.SetActive(true);
		}
		else
		{
			this.NPCChoice.transform.Find("UIGrid/qiecuo").gameObject.SetActive(false);
		}
		Transform transform = this.NPCChoice.transform.Find("UIGrid");
		transform.GetComponent<UIGrid>().repositionNow = true;
		int num = 0;
		using (IEnumerator enumerator = transform.transform.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (((Transform)enumerator.Current).gameObject.activeSelf)
				{
					num++;
				}
			}
		}
		this.tempSay = null;
		if (num <= 2)
		{
			this.NPCChoice.gameObject.SetActive(false);
			Object.Instantiate<GameObject>(Resources.Load<GameObject>("talkPrefab/TalkPrefab/talk" + jsonobject["TalkID"].I));
			return;
		}
		if (jsonobject["FirstSay"].str != "")
		{
			Say say = (Say)Object.Instantiate<GameObject>(Resources.Load<GameObject>("talkPrefab/BasePrefab/NPCTalk")).transform.Find("Flowchart").GetComponent<Flowchart>().FindBlock("Splash").CommandList[0];
			say.pubAvatarIntID = jsonobject["AvatarID"].I;
			say.SetStandardText(Tools.Code64(jsonobject["FirstSay"].str));
			this.tempSay = say;
		}
	}

	// Token: 0x06001FD2 RID: 8146 RVA: 0x000E0878 File Offset: 0x000DEA78
	public void SayNext()
	{
		if (this.tempSay != null)
		{
			this.tempSay.Continue();
		}
	}

	// Token: 0x06001FD3 RID: 8147 RVA: 0x000E0894 File Offset: 0x000DEA94
	public void statrTalk()
	{
		JSONObject jsonobject = jsonData.instance.ThreeSenceJsonData[string.Concat(this.nowAvatar)];
		Object.Instantiate<GameObject>(Resources.Load<GameObject>("talkPrefab/TalkPrefab/talk" + jsonobject["TalkID"].I));
		this.close();
	}

	// Token: 0x06001FD4 RID: 8148 RVA: 0x000E08F4 File Offset: 0x000DEAF4
	public void exchengeItem()
	{
		Object.Instantiate<GameObject>(ResManager.inst.LoadPrefab("JiaoYiPanel"), UI_Manager.inst.gameObject.transform);
		this.exchangeUI = Singleton.ints.exchengePlan.gameObject;
		JSONObject jsonobject = jsonData.instance.ThreeSenceJsonData[string.Concat(this.nowAvatar)];
		this.exchangeUI.transform.parent = UI_Manager.inst.gameObject.transform;
		this.exchangeUI.transform.localPosition = Vector3.zero;
		this.exchangeUI.transform.localScale = new Vector3(0.752f, 0.752f, 1f);
		this.exchangeUI.SetActive(true);
		this.exchangeUI.GetComponent<ExchangePlan>().MonstarID = jsonobject["AvatarID"].I;
		this.exchangeUI.GetComponent<ExchangePlan>().initPlan();
		UIButton component = this.exchangeUI.transform.Find("Panel/close").GetComponent<UIButton>();
		component.onClick.Clear();
		component.onClick.Add(new EventDelegate(delegate()
		{
			Object.Destroy(this.exchangeUI.gameObject);
		}));
		this.close();
	}

	// Token: 0x06001FD5 RID: 8149 RVA: 0x000E0A34 File Offset: 0x000DEC34
	public int getMonstarIndex(List<int> list)
	{
		Avatar player = Tools.instance.getPlayer();
		int num = 0;
		bool flag = false;
		bool flag2 = false;
		bool flag3 = false;
		foreach (JSONObject jsonobject in jsonData.instance.FavorabilityInfoJsonData.list)
		{
			foreach (int num2 in list)
			{
				if (jsonobject["AvatarID"].I == num2 && NPCEx.GetFavor(num2) >= (int)jsonobject["HaoGanDu"].n)
				{
					if (num == 0 || (int)jsonobject["JinDu"].n > (int)jsonData.instance.FavorabilityInfoJsonData[string.Concat(num)]["JinDu"].n)
					{
						num = jsonobject["id"].I;
					}
					if (!player.AvatarGotChuanGong.HasField(jsonobject["id"].I.ToString()))
					{
						flag = true;
						bool flag4 = false;
						bool flag5 = false;
						if ((int)jsonobject["AvatarLevel"].n == 0)
						{
							flag4 = true;
							flag2 = true;
						}
						else if ((int)jsonobject["AvatarLevel"].n <= (int)player.level)
						{
							flag4 = true;
							flag2 = true;
						}
						if ((int)jsonobject["Time"].n == 0)
						{
							flag5 = true;
							flag3 = true;
						}
						else
						{
							DateTime t = new DateTime(Mathf.Clamp((int)jsonobject["Time"].n, 1, 9999), 1, 1);
							DateTime nowTime = player.worldTimeMag.getNowTime();
							if (t <= nowTime)
							{
								flag5 = true;
								flag3 = true;
							}
						}
						if (flag4 && flag5)
						{
							return jsonobject["id"].I;
						}
					}
				}
			}
		}
		JSONObject jsonobject2 = jsonData.instance.FavorabilityInfoJsonData[num.ToString()];
		if (num != 0 && jsonobject2["no"].str != "" && player.AvatarGotChuanGong.HasField(num.ToString()))
		{
			Tools.Say(Tools.instance.Code64ToString(jsonobject2["no"].str), UINPCJiaoHu.Inst.NowJiaoHuNPC.ID);
		}
		else if (num == 0)
		{
			UIPopTip.Inst.Pop("好感度不足", PopTipIconType.叹号);
		}
		else if (!flag)
		{
			UIPopTip.Inst.Pop("好感度不足", PopTipIconType.叹号);
		}
		else
		{
			if (!flag2)
			{
				UIPopTip.Inst.Pop("境界不足", PopTipIconType.叹号);
			}
			if (!flag3)
			{
				UIPopTip.Inst.Pop("时间未到", PopTipIconType.叹号);
			}
		}
		return 0;
	}

	// Token: 0x06001FD6 RID: 8150 RVA: 0x000E0D40 File Offset: 0x000DEF40
	public void qingJiaoGongFanom(int _AvatarID)
	{
		List<int> haoGanDUGuanLian = jsonData.instance.getHaoGanDUGuanLian(_AvatarID);
		int monstarIndex = this.getMonstarIndex(haoGanDUGuanLian);
		Avatar player = Tools.instance.getPlayer();
		if (monstarIndex > 0)
		{
			player.AvatarGotChuanGong.AddField(monstarIndex.ToString(), 1);
			JSONObject jsonobject = jsonData.instance.FavorabilityInfoJsonData[monstarIndex.ToString()];
			Tools.Say(Tools.instance.Code64ToString(jsonobject["yes"].str), _AvatarID);
			player.addItem(jsonobject["ItemID"].I, 1, Tools.CreateItemSeid(jsonobject["ItemID"].I), true);
		}
		this.close();
	}

	// Token: 0x06001FD7 RID: 8151 RVA: 0x000E0DF4 File Offset: 0x000DEFF4
	public void qingJiaoGongFa()
	{
		JSONObject jsonobject = jsonData.instance.ThreeSenceJsonData[string.Concat(this.nowAvatar)];
		this.qingJiaoGongFanom(jsonobject["AvatarID"].I);
	}

	// Token: 0x06001FD8 RID: 8152 RVA: 0x000E0E38 File Offset: 0x000DF038
	public void qiecuo()
	{
		JSONObject jsonobject = jsonData.instance.ThreeSenceJsonData[string.Concat(this.nowAvatar)];
		int monstarId = jsonobject["AvatarID"].I;
		Block block = Object.Instantiate<GameObject>(Resources.Load("uiPrefab/fungus/qiecuostart") as GameObject).GetComponent<Flowchart>().FindBlock("statr");
		Say say = (Say)block.CommandList[0];
		say.pubAvatarIntID = monstarId;
		JSONObject jsonobject2 = jsonData.instance.QieCuoJsonData.list.Find((JSONObject aa) => aa["AvatarID"].I == monstarId);
		say.SetStandardText(Tools.instance.Code64ToString(jsonobject2["jieshou"].str));
		((StartFight)block.CommandList[2]).MonstarID = monstarId;
		this.close();
	}

	// Token: 0x06001FD9 RID: 8153 RVA: 0x000E0F22 File Offset: 0x000DF122
	public void close()
	{
		this.NPCChoice.SetActive(false);
	}

	// Token: 0x06001FDA RID: 8154 RVA: 0x000E0F30 File Offset: 0x000DF130
	private void OnDestroy()
	{
		ThreeSceneMag.inst = null;
	}

	// Token: 0x06001FDB RID: 8155 RVA: 0x000E0F38 File Offset: 0x000DF138
	private void Update()
	{
		if (this.NPCChoice != null && PanelMamager.inst != null)
		{
			if (this.NPCChoice.activeSelf)
			{
				PanelMamager.inst.canOpenPanel = false;
				return;
			}
			PanelMamager.inst.canOpenPanel = true;
		}
	}

	// Token: 0x040019E0 RID: 6624
	public GameObject CraftingList;

	// Token: 0x040019E1 RID: 6625
	public GameObject TempRecipe;

	// Token: 0x040019E2 RID: 6626
	public GameObject NPCChoice;

	// Token: 0x040019E3 RID: 6627
	public GameObject exchangeUI;

	// Token: 0x040019E4 RID: 6628
	public Avatar avatar;

	// Token: 0x040019E5 RID: 6629
	public GameObject xiala;

	// Token: 0x040019E6 RID: 6630
	public Say tempSay;

	// Token: 0x040019E7 RID: 6631
	public static ThreeSceneMag inst;

	// Token: 0x040019E8 RID: 6632
	private int nowAvatar;

	// Token: 0x040019E9 RID: 6633
	public GameObject face0;
}
