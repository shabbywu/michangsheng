using System;
using System.Collections.Generic;
using Fungus;
using KBEngine;
using Newtonsoft.Json.Linq;
using UltimateSurvival;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x02000289 RID: 649
public class MapRandomCompent : MapInstComport
{
	// Token: 0x060013D9 RID: 5081 RVA: 0x0001286F File Offset: 0x00010A6F
	protected override void Awake()
	{
		this.AllMapCastTimeJsonData = jsonData.instance.AllMapCastTimeJsonData;
		this.MapRandomJsonData = jsonData.instance.MapRandomJsonData;
	}

	// Token: 0x060013DA RID: 5082 RVA: 0x000B67A8 File Offset: 0x000B49A8
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

	// Token: 0x060013DB RID: 5083 RVA: 0x00012891 File Offset: 0x00010A91
	protected override int GetGrideNum()
	{
		return base.transform.parent.GetComponent<RandomFuBen>().Wide;
	}

	// Token: 0x060013DC RID: 5084 RVA: 0x000042DD File Offset: 0x000024DD
	public override void BaseAddTime()
	{
	}

	// Token: 0x060013DD RID: 5085 RVA: 0x000B6814 File Offset: 0x000B4A14
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

	// Token: 0x060013DE RID: 5086 RVA: 0x000128A8 File Offset: 0x00010AA8
	public static void ShowOutRandomFubenTalk()
	{
		MapRandomCompent.NowRandomOutCompent.OutRandomFuBen();
	}

	// Token: 0x060013DF RID: 5087 RVA: 0x000128B4 File Offset: 0x00010AB4
	public void OutRandomFuBen()
	{
		Tools.instance.getPlayer().randomFuBenMag.OutRandomFuBen();
	}

	// Token: 0x060013E0 RID: 5088 RVA: 0x000B6A38 File Offset: 0x000B4C38
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

	// Token: 0x060013E1 RID: 5089 RVA: 0x000B6BF8 File Offset: 0x000B4DF8
	public bool ISOutNode()
	{
		RandomFuBen component = base.transform.parent.GetComponent<RandomFuBen>();
		List<int> list = new List<int>();
		List<int> list2 = new List<int>();
		component.mapMag.getAllMapIndex(FuBenMap.NodeType.Exit, list, list2);
		return component.mapMag.mapIndex[list[0], list2[0]] == this.NodeIndex;
	}

	// Token: 0x060013E2 RID: 5090 RVA: 0x000B6C58 File Offset: 0x000B4E58
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

	// Token: 0x060013E3 RID: 5091 RVA: 0x000B6CC4 File Offset: 0x000B4EC4
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

	// Token: 0x04000F80 RID: 3968
	public static MapRandomCompent NowRandomOutCompent;
}
