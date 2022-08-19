using System;
using System.Collections.Generic;
using Fungus;
using KBEngine;
using Newtonsoft.Json.Linq;
using UltimateSurvival;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x02000197 RID: 407
public class MapRandomCompent : MapInstComport
{
	// Token: 0x0600114E RID: 4430 RVA: 0x00068253 File Offset: 0x00066453
	protected override void Awake()
	{
		this.AllMapCastTimeJsonData = jsonData.instance.AllMapCastTimeJsonData;
		this.MapRandomJsonData = jsonData.instance.MapRandomJsonData;
	}

	// Token: 0x0600114F RID: 4431 RVA: 0x00068278 File Offset: 0x00066478
	protected override void Start()
	{
		this.NodeIndex = int.Parse(base.name);
		this.SetState.SetTryer(new Attempt<MapInstComport.NodeType>.GenericTryerDelegate(base.TryChange_State));
		this.State.AddChangeListener(new Action(base.chengeState));
		this.State.Set(MapInstComport.NodeType.Disable);
		this.StartSeting();
		if (this.ISOutNode())
		{
			MapRandomCompent.NowRandomOutCompent = this;
		}
	}

	// Token: 0x06001150 RID: 4432 RVA: 0x000682E4 File Offset: 0x000664E4
	protected override int GetGrideNum()
	{
		return base.transform.parent.GetComponent<RandomFuBen>().Wide;
	}

	// Token: 0x06001151 RID: 4433 RVA: 0x00004095 File Offset: 0x00002295
	public override void BaseAddTime()
	{
	}

	// Token: 0x06001152 RID: 4434 RVA: 0x000682FC File Offset: 0x000664FC
	public override void EventRandom()
	{
		Avatar player = Tools.instance.getPlayer();
		if (!base.CanClick())
		{
			return;
		}
		if (WASDMove.Inst != null)
		{
			WASDMove.Inst.IsMoved = true;
		}
		this.fuBenSetClick();
		this.movaAvatar();
		if (this.ISOutNode())
		{
			USelectBox.Show("是否离开当前副本？", new UnityAction(this.OutRandomFuBen), delegate
			{
				if (AllMapManage.instance != null && AllMapManage.instance.mapIndex.ContainsKey(Tools.instance.fubenLastIndex))
				{
					AllMapManage.instance.mapIndex[Tools.instance.fubenLastIndex].AvatarMoveToThis();
				}
			});
			return;
		}
		int taskTalkID = this.GetTaskTalkID();
		if (taskTalkID != -1 && taskTalkID != 0)
		{
			Object.Instantiate<GameObject>(Resources.Load<GameObject>("talkPrefab/TalkPrefab/talk" + taskTalkID));
			int nowRandomFuBenID = player.NowRandomFuBenID;
			JToken jtoken = player.RandomFuBenList[nowRandomFuBenID.ToString()];
			jtoken["ShouldReset"] = true;
			jtoken["TaskTalkID"] = -1;
			this.ResetIndex();
			return;
		}
		int suiIndex = this.getSuiIndex();
		if (suiIndex == -1)
		{
			return;
		}
		JToken jtoken2 = jsonData.instance.RandomMapEventList[suiIndex.ToString()];
		int num = 0;
		foreach (JToken jtoken3 in ((JArray)jtoken2["valueID"]))
		{
			GlobalValue.Set((int)jtoken3, (int)jtoken2["value"][num], "MapRandomComponent.EventRandom 随机副本事件全局变量");
			num++;
		}
		if (!(AllMapManage.instance != null) || !AllMapManage.instance.RandomFlag.ContainsKey(suiIndex))
		{
			string text = string.Format("talkPrefab/TalkPrefab/talk{0}", (int)jtoken2["talk"]);
			GameObject gameObject = Resources.Load<GameObject>(text);
			if (gameObject != null)
			{
				Flowchart componentInChildren = Object.Instantiate<GameObject>(gameObject).GetComponentInChildren<Flowchart>();
				if (componentInChildren.HasVariable("FBEventID"))
				{
					componentInChildren.SetIntegerVariable("FBEventID", suiIndex);
				}
			}
			else
			{
				Debug.LogError(text + "不存在，无法实例化talk，请检查");
			}
		}
		this.ResetIndex();
	}

	// Token: 0x06001153 RID: 4435 RVA: 0x00068520 File Offset: 0x00066720
	public static void ShowOutRandomFubenTalk()
	{
		MapRandomCompent.NowRandomOutCompent.OutRandomFuBen();
	}

	// Token: 0x06001154 RID: 4436 RVA: 0x0006852C File Offset: 0x0006672C
	public void OutRandomFuBen()
	{
		Tools.instance.getPlayer().randomFuBenMag.OutRandomFuBen();
	}

	// Token: 0x06001155 RID: 4437 RVA: 0x00068544 File Offset: 0x00066744
	public void ResetIndex()
	{
		Avatar player = Tools.instance.getPlayer();
		int nowRandomFuBenID = player.NowRandomFuBenID;
		JToken jtoken = player.RandomFuBenList[nowRandomFuBenID.ToString()];
		foreach (JToken jtoken2 in ((JArray)jtoken["Award"]))
		{
			JObject jobject = (JObject)jtoken2;
			if ((int)jobject["Index"] == this.NodeIndex)
			{
				if ((int)jobject["ID"] != -1 && (int)jsonData.instance.RandomMapEventList[((int)jobject["ID"]).ToString()]["chufaduoci"] == 1)
				{
					jobject["ID"] = -1;
					break;
				}
				break;
			}
		}
		foreach (JToken jtoken3 in ((JArray)jtoken["Event"]))
		{
			JObject jobject2 = (JObject)jtoken3;
			if ((int)jobject2["Index"] == this.NodeIndex)
			{
				if ((int)jobject2["ID"] != -1 && (int)jsonData.instance.RandomMapEventList[((int)jobject2["ID"]).ToString()]["chufaduoci"] == 1)
				{
					jobject2["ID"] = -1;
					break;
				}
				break;
			}
		}
	}

	// Token: 0x06001156 RID: 4438 RVA: 0x00068704 File Offset: 0x00066904
	public bool ISOutNode()
	{
		RandomFuBen component = base.transform.parent.GetComponent<RandomFuBen>();
		List<int> list = new List<int>();
		List<int> list2 = new List<int>();
		component.mapMag.getAllMapIndex(FuBenMap.NodeType.Exit, list, list2);
		return component.mapMag.mapIndex[list[0], list2[0]] == this.NodeIndex;
	}

	// Token: 0x06001157 RID: 4439 RVA: 0x00068764 File Offset: 0x00066964
	public int GetTaskTalkID()
	{
		Avatar player = Tools.instance.getPlayer();
		int nowRandomFuBenID = player.NowRandomFuBenID;
		JObject jobject = (JObject)player.RandomFuBenList[nowRandomFuBenID.ToString()];
		if (jobject.ContainsKey("TaskIndex") && (int)jobject["TaskIndex"] == this.NodeIndex)
		{
			return (int)jobject["TaskTalkID"];
		}
		return -1;
	}

	// Token: 0x06001158 RID: 4440 RVA: 0x000687D0 File Offset: 0x000669D0
	public int getSuiIndex()
	{
		Avatar player = Tools.instance.getPlayer();
		int nowRandomFuBenID = player.NowRandomFuBenID;
		JToken jtoken = player.RandomFuBenList[nowRandomFuBenID.ToString()];
		int result = -1;
		foreach (JToken jtoken2 in ((JArray)jtoken["Award"]))
		{
			JObject jobject = (JObject)jtoken2;
			if ((int)jobject["Index"] == this.NodeIndex)
			{
				result = (int)jobject["ID"];
			}
		}
		foreach (JToken jtoken3 in ((JArray)jtoken["Event"]))
		{
			JObject jobject2 = (JObject)jtoken3;
			if ((int)jobject2["Index"] == this.NodeIndex)
			{
				result = (int)jobject2["ID"];
			}
		}
		return result;
	}

	// Token: 0x04000C7B RID: 3195
	public static MapRandomCompent NowRandomOutCompent;
}
