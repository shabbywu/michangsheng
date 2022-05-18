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

// Token: 0x0200056A RID: 1386
public class ThreeSceneMag : MonoBehaviour
{
	// Token: 0x170002BB RID: 699
	// (get) Token: 0x06002345 RID: 9029 RVA: 0x0001C993 File Offset: 0x0001AB93
	public int getNowAvatar
	{
		get
		{
			return this.nowAvatar;
		}
	}

	// Token: 0x06002346 RID: 9030 RVA: 0x00122B64 File Offset: 0x00120D64
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
			if ((int)jsonobject["SceneID"].n == num)
			{
				this.addTalkAvatar((int)jsonobject["id"].n);
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

	// Token: 0x06002347 RID: 9031 RVA: 0x0001C99B File Offset: 0x0001AB9B
	public void xiaLaBtn()
	{
		this.CraftingList.transform.parent.parent.GetComponent<ScrollRect>().verticalNormalizedPosition = 0f;
	}

	// Token: 0x06002348 RID: 9032 RVA: 0x00122D3C File Offset: 0x00120F3C
	public void addTalkAvatar(int key)
	{
		JSONObject jsonobject = jsonData.instance.ThreeSenceJsonData[string.Concat(key)];
		bool flag = true;
		if (jsonData.instance.MonstarIsDeath((int)jsonobject["AvatarID"].n))
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

	// Token: 0x06002349 RID: 9033 RVA: 0x00122FE8 File Offset: 0x001211E8
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

	// Token: 0x0600234A RID: 9034 RVA: 0x00123034 File Offset: 0x00121234
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
		int num = (int)jsonobject["AvatarID"].n;
		if ((int)jsonobject["transaction3"].n == 1 && (int)jsonData.instance.AvatarRandomJsonData[string.Concat(num)]["HaoGanDu"].n >= 40 && (int)Tools.instance.getPlayer().level >= jsonobject["qiecuoLv"].I)
		{
			this.NPCChoice.transform.Find("UIGrid/qiecuo").gameObject.SetActive(true);
		}
		else
		{
			this.NPCChoice.transform.Find("UIGrid/qiecuo").gameObject.SetActive(false);
		}
		Transform transform = this.NPCChoice.transform.Find("UIGrid");
		transform.GetComponent<UIGrid>().repositionNow = true;
		int num2 = 0;
		using (IEnumerator enumerator = transform.transform.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (((Transform)enumerator.Current).gameObject.activeSelf)
				{
					num2++;
				}
			}
		}
		this.tempSay = null;
		if (num2 <= 2)
		{
			this.NPCChoice.gameObject.SetActive(false);
			Object.Instantiate<GameObject>(Resources.Load<GameObject>("talkPrefab/TalkPrefab/talk" + (int)jsonobject["TalkID"].n));
			return;
		}
		if (jsonobject["FirstSay"].str != "")
		{
			Say say = (Say)Object.Instantiate<GameObject>(Resources.Load<GameObject>("talkPrefab/BasePrefab/NPCTalk")).transform.Find("Flowchart").GetComponent<Flowchart>().FindBlock("Splash").CommandList[0];
			say.pubAvatarIntID = (int)jsonobject["AvatarID"].n;
			say.SetStandardText(Tools.Code64(jsonobject["FirstSay"].str));
			this.tempSay = say;
		}
	}

	// Token: 0x0600234B RID: 9035 RVA: 0x0001C9C1 File Offset: 0x0001ABC1
	public void SayNext()
	{
		if (this.tempSay != null)
		{
			this.tempSay.Continue();
		}
	}

	// Token: 0x0600234C RID: 9036 RVA: 0x00123330 File Offset: 0x00121530
	public void statrTalk()
	{
		JSONObject jsonobject = jsonData.instance.ThreeSenceJsonData[string.Concat(this.nowAvatar)];
		Object.Instantiate<GameObject>(Resources.Load<GameObject>("talkPrefab/TalkPrefab/talk" + (int)jsonobject["TalkID"].n));
		this.close();
	}

	// Token: 0x0600234D RID: 9037 RVA: 0x00123390 File Offset: 0x00121590
	public void exchengeItem()
	{
		Object.Instantiate<GameObject>(ResManager.inst.LoadPrefab("JiaoYiPanel"), UI_Manager.inst.gameObject.transform);
		this.exchangeUI = Singleton.ints.exchengePlan.gameObject;
		JSONObject jsonobject = jsonData.instance.ThreeSenceJsonData[string.Concat(this.nowAvatar)];
		this.exchangeUI.transform.parent = UI_Manager.inst.gameObject.transform;
		this.exchangeUI.transform.localPosition = Vector3.zero;
		this.exchangeUI.transform.localScale = new Vector3(0.752f, 0.752f, 1f);
		this.exchangeUI.SetActive(true);
		this.exchangeUI.GetComponent<ExchangePlan>().MonstarID = (int)jsonobject["AvatarID"].n;
		this.exchangeUI.GetComponent<ExchangePlan>().initPlan();
		UIButton component = this.exchangeUI.transform.Find("Panel/close").GetComponent<UIButton>();
		component.onClick.Clear();
		component.onClick.Add(new EventDelegate(delegate()
		{
			Object.Destroy(this.exchangeUI.gameObject);
		}));
		this.close();
	}

	// Token: 0x0600234E RID: 9038 RVA: 0x001234D4 File Offset: 0x001216D4
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
						num = (int)jsonobject["id"].n;
					}
					if (!player.AvatarGotChuanGong.HasField(((int)jsonobject["id"].n).ToString()))
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
							return (int)jsonobject["id"].n;
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

	// Token: 0x0600234F RID: 9039 RVA: 0x001237E0 File Offset: 0x001219E0
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
			player.addItem((int)jsonobject["ItemID"].n, 1, Tools.CreateItemSeid((int)jsonobject["ItemID"].n), true);
		}
		this.close();
	}

	// Token: 0x06002350 RID: 9040 RVA: 0x00123898 File Offset: 0x00121A98
	public void qingJiaoGongFa()
	{
		JSONObject jsonobject = jsonData.instance.ThreeSenceJsonData[string.Concat(this.nowAvatar)];
		this.qingJiaoGongFanom((int)jsonobject["AvatarID"].n);
	}

	// Token: 0x06002351 RID: 9041 RVA: 0x001238DC File Offset: 0x00121ADC
	public void qiecuo()
	{
		JSONObject jsonobject = jsonData.instance.ThreeSenceJsonData[string.Concat(this.nowAvatar)];
		int monstarId = (int)jsonobject["AvatarID"].n;
		Block block = Object.Instantiate<GameObject>(Resources.Load("uiPrefab/fungus/qiecuostart") as GameObject).GetComponent<Flowchart>().FindBlock("statr");
		Say say = (Say)block.CommandList[0];
		say.pubAvatarIntID = monstarId;
		JSONObject jsonobject2 = jsonData.instance.QieCuoJsonData.list.Find((JSONObject aa) => (int)aa["AvatarID"].n == monstarId);
		say.SetStandardText(Tools.instance.Code64ToString(jsonobject2["jieshou"].str));
		((StartFight)block.CommandList[2]).MonstarID = monstarId;
		this.close();
	}

	// Token: 0x06002352 RID: 9042 RVA: 0x0001C9DC File Offset: 0x0001ABDC
	public void close()
	{
		this.NPCChoice.SetActive(false);
	}

	// Token: 0x06002353 RID: 9043 RVA: 0x0001C9EA File Offset: 0x0001ABEA
	private void OnDestroy()
	{
		ThreeSceneMag.inst = null;
	}

	// Token: 0x06002354 RID: 9044 RVA: 0x001239C8 File Offset: 0x00121BC8
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

	// Token: 0x04001E6A RID: 7786
	public GameObject CraftingList;

	// Token: 0x04001E6B RID: 7787
	public GameObject TempRecipe;

	// Token: 0x04001E6C RID: 7788
	public GameObject NPCChoice;

	// Token: 0x04001E6D RID: 7789
	public GameObject exchangeUI;

	// Token: 0x04001E6E RID: 7790
	public Avatar avatar;

	// Token: 0x04001E6F RID: 7791
	public GameObject xiala;

	// Token: 0x04001E70 RID: 7792
	public Say tempSay;

	// Token: 0x04001E71 RID: 7793
	public static ThreeSceneMag inst;

	// Token: 0x04001E72 RID: 7794
	private int nowAvatar;

	// Token: 0x04001E73 RID: 7795
	public GameObject face0;
}
