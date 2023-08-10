using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Fungus;
using KBEngine;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.Events;

public class MapRandomCompent : MapInstComport
{
	[Serializable]
	[CompilerGenerated]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9 = new _003C_003Ec();

		public static UnityAction _003C_003E9__5_0;

		internal void _003CEventRandom_003Eb__5_0()
		{
			if ((Object)(object)AllMapManage.instance != (Object)null && AllMapManage.instance.mapIndex.ContainsKey(Tools.instance.fubenLastIndex))
			{
				AllMapManage.instance.mapIndex[Tools.instance.fubenLastIndex].AvatarMoveToThis();
			}
		}
	}

	public static MapRandomCompent NowRandomOutCompent;

	protected override void Awake()
	{
		AllMapCastTimeJsonData = jsonData.instance.AllMapCastTimeJsonData;
		MapRandomJsonData = jsonData.instance.MapRandomJsonData;
	}

	protected override void Start()
	{
		NodeIndex = int.Parse(((Object)this).name);
		SetState.SetTryer(base.TryChange_State);
		State.AddChangeListener(base.chengeState);
		State.Set(NodeType.Disable);
		StartSeting();
		if (ISOutNode())
		{
			NowRandomOutCompent = this;
		}
	}

	protected override int GetGrideNum()
	{
		return ((Component)((Component)this).transform.parent).GetComponent<RandomFuBen>().Wide;
	}

	public override void BaseAddTime()
	{
	}

	public override void EventRandom()
	{
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Expected O, but got Unknown
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Expected O, but got Unknown
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		Avatar player = Tools.instance.getPlayer();
		if (!CanClick())
		{
			return;
		}
		if ((Object)(object)WASDMove.Inst != (Object)null)
		{
			WASDMove.Inst.IsMoved = true;
		}
		fuBenSetClick();
		movaAvatar();
		if (ISOutNode())
		{
			UnityAction val = OutRandomFuBen;
			object obj = _003C_003Ec._003C_003E9__5_0;
			if (obj == null)
			{
				UnityAction val2 = delegate
				{
					if ((Object)(object)AllMapManage.instance != (Object)null && AllMapManage.instance.mapIndex.ContainsKey(Tools.instance.fubenLastIndex))
					{
						AllMapManage.instance.mapIndex[Tools.instance.fubenLastIndex].AvatarMoveToThis();
					}
				};
				_003C_003Ec._003C_003E9__5_0 = val2;
				obj = (object)val2;
			}
			USelectBox.Show("是否离开当前副本？", val, (UnityAction)obj);
			return;
		}
		int taskTalkID = GetTaskTalkID();
		if (taskTalkID != -1 && taskTalkID != 0)
		{
			Object.Instantiate<GameObject>(Resources.Load<GameObject>("talkPrefab/TalkPrefab/talk" + taskTalkID));
			int nowRandomFuBenID = player.NowRandomFuBenID;
			JToken obj2 = player.RandomFuBenList[nowRandomFuBenID.ToString()];
			obj2[(object)"ShouldReset"] = JToken.op_Implicit(true);
			obj2[(object)"TaskTalkID"] = JToken.op_Implicit(-1);
			ResetIndex();
			return;
		}
		int suiIndex = getSuiIndex();
		if (suiIndex == -1)
		{
			return;
		}
		JToken val3 = jsonData.instance.RandomMapEventList[suiIndex.ToString()];
		int num = 0;
		foreach (JToken item in (JArray)val3[(object)"valueID"])
		{
			GlobalValue.Set((int)item, (int)val3[(object)"value"][(object)num], "MapRandomComponent.EventRandom 随机副本事件全局变量");
			num++;
		}
		if (!((Object)(object)AllMapManage.instance != (Object)null) || !AllMapManage.instance.RandomFlag.ContainsKey(suiIndex))
		{
			string text = string.Format("talkPrefab/TalkPrefab/talk{0}", (int)val3[(object)"talk"]);
			GameObject val4 = Resources.Load<GameObject>(text);
			if ((Object)(object)val4 != (Object)null)
			{
				Flowchart componentInChildren = Object.Instantiate<GameObject>(val4).GetComponentInChildren<Flowchart>();
				if (componentInChildren.HasVariable("FBEventID"))
				{
					componentInChildren.SetIntegerVariable("FBEventID", suiIndex);
				}
			}
			else
			{
				Debug.LogError((object)(text + "不存在，无法实例化talk，请检查"));
			}
		}
		ResetIndex();
	}

	public static void ShowOutRandomFubenTalk()
	{
		NowRandomOutCompent.OutRandomFuBen();
	}

	public void OutRandomFuBen()
	{
		Tools.instance.getPlayer().randomFuBenMag.OutRandomFuBen();
	}

	public void ResetIndex()
	{
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Expected O, but got Unknown
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Expected O, but got Unknown
		Avatar player = Tools.instance.getPlayer();
		int nowRandomFuBenID = player.NowRandomFuBenID;
		JToken val = player.RandomFuBenList[nowRandomFuBenID.ToString()];
		foreach (JObject item in (JArray)val[(object)"Award"])
		{
			JObject val2 = item;
			if ((int)val2["Index"] == NodeIndex)
			{
				if ((int)val2["ID"] != -1 && (int)jsonData.instance.RandomMapEventList[((int)val2["ID"]).ToString()][(object)"chufaduoci"] == 1)
				{
					val2["ID"] = JToken.op_Implicit(-1);
				}
				break;
			}
		}
		foreach (JObject item2 in (JArray)val[(object)"Event"])
		{
			JObject val3 = item2;
			if ((int)val3["Index"] == NodeIndex)
			{
				if ((int)val3["ID"] != -1 && (int)jsonData.instance.RandomMapEventList[((int)val3["ID"]).ToString()][(object)"chufaduoci"] == 1)
				{
					val3["ID"] = JToken.op_Implicit(-1);
				}
				break;
			}
		}
	}

	public bool ISOutNode()
	{
		RandomFuBen component = ((Component)((Component)this).transform.parent).GetComponent<RandomFuBen>();
		List<int> list = new List<int>();
		List<int> list2 = new List<int>();
		component.mapMag.getAllMapIndex(FuBenMap.NodeType.Exit, list, list2);
		if (component.mapMag.mapIndex[list[0], list2[0]] == NodeIndex)
		{
			return true;
		}
		return false;
	}

	public int GetTaskTalkID()
	{
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Expected O, but got Unknown
		Avatar player = Tools.instance.getPlayer();
		int nowRandomFuBenID = player.NowRandomFuBenID;
		JObject val = (JObject)player.RandomFuBenList[nowRandomFuBenID.ToString()];
		if (val.ContainsKey("TaskIndex") && (int)val["TaskIndex"] == NodeIndex)
		{
			return (int)val["TaskTalkID"];
		}
		return -1;
	}

	public int getSuiIndex()
	{
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Expected O, but got Unknown
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Expected O, but got Unknown
		Avatar player = Tools.instance.getPlayer();
		int nowRandomFuBenID = player.NowRandomFuBenID;
		JToken val = player.RandomFuBenList[nowRandomFuBenID.ToString()];
		int result = -1;
		foreach (JObject item in (JArray)val[(object)"Award"])
		{
			JObject val2 = item;
			if ((int)val2["Index"] == NodeIndex)
			{
				result = (int)val2["ID"];
			}
		}
		foreach (JObject item2 in (JArray)val[(object)"Event"])
		{
			JObject val3 = item2;
			if ((int)val3["Index"] == NodeIndex)
			{
				result = (int)val3["ID"];
			}
		}
		return result;
	}
}
