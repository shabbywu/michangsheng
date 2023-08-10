using System;
using System.Collections.Generic;
using Fungus;
using GUIPackage;
using KBEngine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ThreeSceneMag : MonoBehaviour
{
	public GameObject CraftingList;

	public GameObject TempRecipe;

	public GameObject NPCChoice;

	public GameObject exchangeUI;

	public Avatar avatar;

	public GameObject xiala;

	public Say tempSay;

	public static ThreeSceneMag inst;

	private int nowAvatar;

	public GameObject face0;

	public int getNowAvatar => nowAvatar;

	public void init()
	{
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_0197: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a1: Expected O, but got Unknown
		UINPCData.ThreeSceneNPCTalkCache.Clear();
		UINPCData.ThreeSceneZhongYaoNPCTalkCache.Clear();
		UINPCJiaoHu.Inst.TNPCIDList.Clear();
		NPCChoice.SetActive(false);
		int num = -1;
		try
		{
			Scene activeScene = SceneManager.GetActiveScene();
			num = int.Parse(((Scene)(ref activeScene)).name.Replace("S", ""));
		}
		catch (Exception)
		{
		}
		avatar = Tools.instance.getPlayer();
		if ((Object)(object)CraftingList != (Object)null && CraftingList.transform.childCount > 0)
		{
			Tools.ClearObj(CraftingList.transform.GetChild(0));
		}
		foreach (JSONObject item in jsonData.instance.ThreeSenceJsonData.list)
		{
			if (item["SceneID"].I == num)
			{
				addTalkAvatar(item["id"].I);
			}
		}
		int num2 = avatar.nomelTaskMag.AutoThreeSceneHasNTask();
		if (num2 != -1)
		{
			JSONObject nowChildIDSuiJiJson = avatar.nomelTaskMag.GetNowChildIDSuiJiJson(num2);
			int index = avatar.nomelTaskMag.nowChildNTask(num2);
			JSONObject TalkJson = avatar.nomelTaskMag.GetXiangXi(num2, index);
			int avatarID = NPCEx.NPCIDToNew(nowChildIDSuiJiJson["Value"].I);
			AddAvatarObj(avatarID, (UnityAction)delegate
			{
				GlobalValue.Set(400, avatarID, "ThreeSceneMag.init");
				nowAvatar = avatarID;
				JSONObject jSONObject = TalkJson;
				Object.Instantiate<GameObject>(Resources.Load<GameObject>("talkPrefab/TalkPrefab/Talk" + jSONObject["talkID"].str));
			});
		}
		NpcJieSuanManager.inst.isUpDateNpcList = true;
	}

	public void xiaLaBtn()
	{
		((Component)CraftingList.transform.parent.parent).GetComponent<ScrollRect>().verticalNormalizedPosition = 0f;
	}

	public void addTalkAvatar(int key)
	{
		//IL_0294: Unknown result type (might be due to invalid IL or missing references)
		//IL_029e: Expected O, but got Unknown
		JSONObject jSONObject = jsonData.instance.ThreeSenceJsonData[string.Concat(key)];
		bool flag = true;
		if (jsonData.instance.MonstarIsDeath(jSONObject["AvatarID"].I))
		{
			flag = false;
		}
		if (jSONObject["Level"].Count > 0 && ((int)jSONObject["Level"][0].n > avatar.level || avatar.level > (int)jSONObject["Level"][1].n))
		{
			flag = false;
		}
		if (jSONObject["StarTime"].str != "" && jSONObject["EndTime"].str != "")
		{
			DateTime dateTime = DateTime.Parse(jSONObject["StarTime"].str);
			DateTime dateTime2 = DateTime.Parse(jSONObject["EndTime"].str);
			DateTime dateTime3 = avatar.worldTimeMag.getNowTime();
			if ((int)jSONObject["circulation"].n > 0 && dateTime3.Year % (int)jSONObject["circulation"].n == dateTime.Year % (int)jSONObject["circulation"].n)
			{
				dateTime3 = new DateTime(dateTime.Year, dateTime3.Month, dateTime3.Day);
			}
			if (!(dateTime <= dateTime3) || !(dateTime3 <= dateTime2))
			{
				flag = false;
			}
		}
		for (int i = 0; i < jSONObject["SaticValue"].list.Count; i++)
		{
			if (jSONObject["SaticValue"][i].n > 0f)
			{
				int num = 0;
				if (jSONObject.HasField("SaticValueX") && jSONObject["SaticValueX"].Count > i)
				{
					num = (int)jSONObject["SaticValueX"][i].n;
				}
				if (GlobalValue.Get(jSONObject["SaticValue"][i].I, "ThreeSceneMag.addTalkAvatar") == num)
				{
					flag = false;
				}
			}
		}
		if (flag)
		{
			int _TempID = NPCEx.NPCIDToNew(jSONObject["AvatarID"].I);
			AddAvatarObj(_TempID, (UnityAction)delegate
			{
				GlobalValue.Set(400, _TempID, "ThreeSceneMag.addTalkAvatar");
				nowAvatar = key;
				openChoic();
			});
		}
	}

	public void AddAvatarObj(int AvatarID, UnityAction Next)
	{
		if (!NPCEx.IsDeath(AvatarID))
		{
			UINPCJiaoHu.Inst.TNPCIDList.Add(AvatarID);
			if (NPCEx.IsZhongYaoNPC(AvatarID, out var oldid))
			{
				UINPCData.ThreeSceneZhongYaoNPCTalkCache.Add(oldid, Next);
			}
			else
			{
				UINPCData.ThreeSceneNPCTalkCache.Add(AvatarID, Next);
			}
		}
	}

	public void openChoic()
	{
		//IL_01c9: Unknown result type (might be due to invalid IL or missing references)
		NPCChoice.gameObject.SetActive(true);
		JSONObject jSONObject = jsonData.instance.ThreeSenceJsonData[string.Concat(nowAvatar)];
		if ((int)jSONObject["transaction"].n == 1)
		{
			((Component)NPCChoice.transform.Find("UIGrid/exchange")).gameObject.SetActive(true);
		}
		else
		{
			((Component)NPCChoice.transform.Find("UIGrid/exchange")).gameObject.SetActive(false);
		}
		if ((int)jSONObject["transaction2"].n == 1)
		{
			((Component)NPCChoice.transform.Find("UIGrid/GongFa")).gameObject.SetActive(true);
		}
		else
		{
			((Component)NPCChoice.transform.Find("UIGrid/GongFa")).gameObject.SetActive(false);
		}
		int i = jSONObject["AvatarID"].I;
		if ((int)jSONObject["transaction3"].n == 1 && (int)jsonData.instance.AvatarRandomJsonData[string.Concat(i)]["HaoGanDu"].n >= 40 && Tools.instance.getPlayer().level >= jSONObject["qiecuoLv"].I)
		{
			((Component)NPCChoice.transform.Find("UIGrid/qiecuo")).gameObject.SetActive(true);
		}
		else
		{
			((Component)NPCChoice.transform.Find("UIGrid/qiecuo")).gameObject.SetActive(false);
		}
		Transform obj = NPCChoice.transform.Find("UIGrid");
		((Component)obj).GetComponent<UIGrid>().repositionNow = true;
		int num = 0;
		foreach (Transform item in ((Component)obj).transform)
		{
			if (((Component)item).gameObject.activeSelf)
			{
				num++;
			}
		}
		tempSay = null;
		if (num <= 2)
		{
			NPCChoice.gameObject.SetActive(false);
			Object.Instantiate<GameObject>(Resources.Load<GameObject>("talkPrefab/TalkPrefab/talk" + jSONObject["TalkID"].I));
		}
		else if (jSONObject["FirstSay"].str != "")
		{
			Say say = (Say)((Component)Object.Instantiate<GameObject>(Resources.Load<GameObject>("talkPrefab/BasePrefab/NPCTalk")).transform.Find("Flowchart")).GetComponent<Flowchart>().FindBlock("Splash").CommandList[0];
			say.pubAvatarIntID = jSONObject["AvatarID"].I;
			say.SetStandardText(Tools.Code64(jSONObject["FirstSay"].str));
			tempSay = say;
		}
	}

	public void SayNext()
	{
		if ((Object)(object)tempSay != (Object)null)
		{
			tempSay.Continue();
		}
	}

	public void statrTalk()
	{
		JSONObject jSONObject = jsonData.instance.ThreeSenceJsonData[string.Concat(nowAvatar)];
		Object.Instantiate<GameObject>(Resources.Load<GameObject>("talkPrefab/TalkPrefab/talk" + jSONObject["TalkID"].I));
		close();
	}

	public void exchengeItem()
	{
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		Object.Instantiate<GameObject>(ResManager.inst.LoadPrefab("JiaoYiPanel"), ((Component)UI_Manager.inst).gameObject.transform);
		exchangeUI = ((Component)Singleton.ints.exchengePlan).gameObject;
		JSONObject jSONObject = jsonData.instance.ThreeSenceJsonData[string.Concat(nowAvatar)];
		exchangeUI.transform.parent = ((Component)UI_Manager.inst).gameObject.transform;
		exchangeUI.transform.localPosition = Vector3.zero;
		exchangeUI.transform.localScale = new Vector3(0.752f, 0.752f, 1f);
		exchangeUI.SetActive(true);
		exchangeUI.GetComponent<ExchangePlan>().MonstarID = jSONObject["AvatarID"].I;
		exchangeUI.GetComponent<ExchangePlan>().initPlan();
		UIButton component = ((Component)exchangeUI.transform.Find("Panel/close")).GetComponent<UIButton>();
		component.onClick.Clear();
		component.onClick.Add(new EventDelegate(delegate
		{
			Object.Destroy((Object)(object)exchangeUI.gameObject);
		}));
		close();
	}

	public int getMonstarIndex(List<int> list)
	{
		Avatar player = Tools.instance.getPlayer();
		int num = 0;
		bool flag = false;
		bool flag2 = false;
		bool flag3 = false;
		foreach (JSONObject item in jsonData.instance.FavorabilityInfoJsonData.list)
		{
			foreach (int item2 in list)
			{
				if (item["AvatarID"].I != item2 || NPCEx.GetFavor(item2) < (int)item["HaoGanDu"].n)
				{
					continue;
				}
				if (num == 0 || (int)item["JinDu"].n > (int)jsonData.instance.FavorabilityInfoJsonData[string.Concat(num)]["JinDu"].n)
				{
					num = item["id"].I;
				}
				if (player.AvatarGotChuanGong.HasField(item["id"].I.ToString()))
				{
					continue;
				}
				flag = true;
				bool flag4 = false;
				bool flag5 = false;
				if ((int)item["AvatarLevel"].n == 0)
				{
					flag4 = true;
					flag2 = true;
				}
				else if ((int)item["AvatarLevel"].n <= player.level)
				{
					flag4 = true;
					flag2 = true;
				}
				if ((int)item["Time"].n == 0)
				{
					flag5 = true;
					flag3 = true;
				}
				else
				{
					DateTime dateTime = new DateTime(Mathf.Clamp((int)item["Time"].n, 1, 9999), 1, 1);
					DateTime nowTime = player.worldTimeMag.getNowTime();
					if (dateTime <= nowTime)
					{
						flag5 = true;
						flag3 = true;
					}
				}
				if (flag4 && flag5)
				{
					return item["id"].I;
				}
			}
		}
		JSONObject jSONObject = jsonData.instance.FavorabilityInfoJsonData[num.ToString()];
		if (num != 0 && jSONObject["no"].str != "" && player.AvatarGotChuanGong.HasField(num.ToString()))
		{
			Tools.Say(Tools.instance.Code64ToString(jSONObject["no"].str), UINPCJiaoHu.Inst.NowJiaoHuNPC.ID);
		}
		else if (num == 0)
		{
			UIPopTip.Inst.Pop("好感度不足");
		}
		else if (!flag)
		{
			UIPopTip.Inst.Pop("好感度不足");
		}
		else
		{
			if (!flag2)
			{
				UIPopTip.Inst.Pop("境界不足");
			}
			if (!flag3)
			{
				UIPopTip.Inst.Pop("时间未到");
			}
		}
		return 0;
	}

	public void qingJiaoGongFanom(int _AvatarID)
	{
		List<int> haoGanDUGuanLian = jsonData.instance.getHaoGanDUGuanLian(_AvatarID);
		int monstarIndex = getMonstarIndex(haoGanDUGuanLian);
		Avatar player = Tools.instance.getPlayer();
		if (monstarIndex > 0)
		{
			player.AvatarGotChuanGong.AddField(monstarIndex.ToString(), 1);
			JSONObject jSONObject = jsonData.instance.FavorabilityInfoJsonData[monstarIndex.ToString()];
			Tools.Say(Tools.instance.Code64ToString(jSONObject["yes"].str), _AvatarID);
			player.addItem(jSONObject["ItemID"].I, 1, Tools.CreateItemSeid(jSONObject["ItemID"].I), ShowText: true);
		}
		close();
	}

	public void qingJiaoGongFa()
	{
		JSONObject jSONObject = jsonData.instance.ThreeSenceJsonData[string.Concat(nowAvatar)];
		qingJiaoGongFanom(jSONObject["AvatarID"].I);
	}

	public void qiecuo()
	{
		JSONObject jSONObject = jsonData.instance.ThreeSenceJsonData[string.Concat(nowAvatar)];
		int monstarId = jSONObject["AvatarID"].I;
		Object obj = Resources.Load("uiPrefab/fungus/qiecuostart");
		Block block = Object.Instantiate<GameObject>((GameObject)(object)((obj is GameObject) ? obj : null)).GetComponent<Flowchart>().FindBlock("statr");
		Say obj2 = (Say)block.CommandList[0];
		obj2.pubAvatarIntID = monstarId;
		JSONObject jSONObject2 = jsonData.instance.QieCuoJsonData.list.Find((JSONObject aa) => aa["AvatarID"].I == monstarId);
		obj2.SetStandardText(Tools.instance.Code64ToString(jSONObject2["jieshou"].str));
		((StartFight)block.CommandList[2]).MonstarID = monstarId;
		close();
	}

	public void close()
	{
		NPCChoice.SetActive(false);
	}

	private void OnDestroy()
	{
		inst = null;
	}

	private void Update()
	{
		if ((Object)(object)NPCChoice != (Object)null && (Object)(object)PanelMamager.inst != (Object)null)
		{
			if (NPCChoice.activeSelf)
			{
				PanelMamager.inst.canOpenPanel = false;
			}
			else
			{
				PanelMamager.inst.canOpenPanel = true;
			}
		}
	}
}
