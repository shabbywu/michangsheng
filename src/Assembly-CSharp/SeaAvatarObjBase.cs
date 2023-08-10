using System;
using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using Fungus;
using KBEngine;
using Newtonsoft.Json.Linq;
using Spine.Unity.Examples;
using UnityEngine;

public class SeaAvatarObjBase : MonoBehaviour
{
	public enum EventType
	{
		initiative,
		passivity
	}

	public enum Directon
	{
		UP,
		Down,
		Left,
		Right
	}

	public enum AIState
	{
		WaitStop = 1,
		ToPlayer,
		ToPlayerNotGongJi,
		GouAway,
		ToNeralIsLand
	}

	public int speed = 20;

	public int _EventId;

	public int ThinkType = 1;

	public DateTime LastMoveTime;

	public List<int> LuXian = new List<int>();

	public int NowMapIndex;

	public bool NeedStop;

	public string NearWaitTime = "0001-01-01";

	public int NearlIslandIndex;

	private bool isCollect;

	public BehaviorTree behaviorTree;

	public bool ISNTaskMonstar;

	public GameObject NtaskSpine;

	public GameObject objBase;

	public RuntimeAnimatorController HaiGuaiAnimCtl;

	public RuntimeAnimatorController ChuanAnimCtl;

	public List<SkeletonAnimationHandleExample.StateNameToAnimationReference> ChuanStatesAndAnimations = new List<SkeletonAnimationHandleExample.StateNameToAnimationReference>();

	private bool IsShow = true;

	public int SeaId;

	public string UUID;

	private bool CanEventTrager = true;

	public bool IsCollect
	{
		get
		{
			return isCollect;
		}
		set
		{
			isCollect = value;
			NpcJieSuanManager.inst.isUpDateNpcList = true;
		}
	}

	public int MenPai => (int)jsonData.instance.EndlessSeaNPCData[string.Concat(_EventId)][(object)"shiliID"];

	public int ShenShi => (int)Json["ShenShi"];

	public int LV => (int)jsonData.instance.EndlessSeaNPCData[string.Concat(_EventId)][(object)"EventLV"];

	public JObject Json => (JObject)jsonData.instance.EndlessSeaNPCData[string.Concat(_EventId)];

	public EventType AIEventType => (EventType)(int)jsonData.instance.EndlessSeaAIChuFa[ThinkType.ToString()][(object)"type"];

	private void Start()
	{
		LastMoveTime = Tools.instance.getPlayer().worldTimeMag.getNowTime();
		InitNearlIsLand();
	}

	public void InitNearlIsLand()
	{
		int seaIslandIndex = Tools.instance.getPlayer().seaNodeMag.GetSeaIslandIndex(SeaId);
		NearlIslandIndex = EndlessSeaMag.GetRealIndex(SeaId, seaIslandIndex);
	}

	public void PlayMove(List<int> MoveLine)
	{
		((MonoBehaviour)this).StartCoroutine(IEplaymove(MoveLine));
	}

	public void Think()
	{
		if (!IsTimeToMove())
		{
			return;
		}
		if (!IsInPlayerScope())
		{
			LastMoveTime = Tools.instance.getPlayer().worldTimeMag.getNowTime();
			return;
		}
		Avatar player = Tools.instance.getPlayer();
		if (player.ItemSeid27Days() > 0 && (int)Json["EventType"] == 1)
		{
			int type = (int)player.ItemBuffList["27"][(object)"AIType"];
			ResetAITpe(type);
		}
		((Behavior)behaviorTree).EnableBehavior();
	}

	public void ResetAITpe(int type)
	{
		if (ThinkType != type)
		{
			ThinkType = type;
			if (!EndlessSeaMag.Inst.externalBehaviorTrees.ContainsKey(type))
			{
				EndlessSeaMag.Inst.externalBehaviorTrees[type] = Resources.Load("MapPrefab/SeaAI/SAI" + type) as ExternalBehaviorTree;
			}
			((Behavior)behaviorTree).ExternalBehavior = (ExternalBehavior)(object)EndlessSeaMag.Inst.externalBehaviorTrees[type];
		}
	}

	public void MonstarMoveToPlayer()
	{
		int nowIndex = PlayerEx.Player.fubenContorl[Tools.getScreenName()].NowIndex;
		List<int> roadXian = EndlessSeaMag.Inst.GetRoadXian(NowMapIndex, nowIndex);
		if (roadXian.Count > 0)
		{
			int num = roadXian[0];
			if (!((MapSeaCompent)AllMapManage.instance.mapIndex[num]).NodeHasIsLand())
			{
				MonstarMoveToPositon(num);
			}
		}
	}

	public void moveToNearlIsland()
	{
		int nearlIsLand = GetNearlIsLand();
		if (nearlIsLand > 0)
		{
			MonstarMoveToPositon(nearlIsLand);
		}
	}

	public void moveAwayFromPositon()
	{
		int awayFromPositon = GetAwayFromPositon(ignoreIsland: true);
		MonstarMoveToPositon(awayFromPositon);
	}

	public void ResetBehavirTree(int type)
	{
		((Behavior)behaviorTree).ExternalBehavior = (ExternalBehavior)(object)EndlessSeaMag.Inst.externalBehaviorTrees[type];
		BehaviorManager.instance.RestartBehavior((Behavior)(object)behaviorTree);
	}

	public void MonstarMoveToPositon(int endIndex)
	{
		Avatar player = PlayerEx.Player;
		int days = (player.worldTimeMag.getNowTime() - LastMoveTime).Days;
		MonstarMove(endIndex, days, speed);
		LastMoveTime = player.worldTimeMag.getNowTime();
	}

	public int GetAwayFromPositon(bool ignoreIsland = false)
	{
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		List<int> nextIndex = ((MapSeaCompent)AllMapManage.instance.mapIndex[NowMapIndex]).nextIndex;
		int nowIndex = Tools.instance.getPlayer().fubenContorl[Tools.getScreenName()].NowIndex;
		MapSeaCompent mapSeaCompent = (MapSeaCompent)AllMapManage.instance.mapIndex[nowIndex];
		int result = 0;
		float num = 0f;
		foreach (int item in nextIndex)
		{
			MapSeaCompent mapSeaCompent2 = (MapSeaCompent)AllMapManage.instance.mapIndex[item];
			if (!((Object)(object)((Component)((Component)mapSeaCompent2).transform).GetComponent<SeaToOherScene>() != (Object)null) && (!ignoreIsland || !mapSeaCompent2.NodeHasIsLand()))
			{
				float num2 = Vector3.Distance(((Component)mapSeaCompent).transform.position, ((Component)mapSeaCompent2).transform.position);
				if (num == num2 && jsonData.instance.QuikeGetRandom() % 2 == 0)
				{
					result = item;
				}
				if (num < num2)
				{
					result = item;
					num = num2;
				}
			}
		}
		return result;
	}

	public bool WhetherCanMove()
	{
		if (IsInPlayerScope() && IsTimeToMove())
		{
			return true;
		}
		return false;
	}

	public int GetNearlIsLand()
	{
		if (NowMapIndex == NearlIslandIndex)
		{
			ResetNearlIsland();
		}
		return NearlIslandIndex;
	}

	public void ResetNearlIsland()
	{
		DateTime dateTime = DateTime.Parse(NearWaitTime);
		DateTime nowTime = Tools.instance.getPlayer().worldTimeMag.getNowTime();
		if ((nowTime - dateTime).Days > 120)
		{
			NearWaitTime = Tools.instance.getPlayer().worldTimeMag.nowTime;
			dateTime = DateTime.Parse(NearWaitTime);
		}
		if ((nowTime - dateTime).Days > 60)
		{
			NearlIslandIndex = GetAtherNearlIsLand();
		}
	}

	public int GetAtherNearlIsLand()
	{
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		Dictionary<int, BaseMapCompont> mapIndex = AllMapManage.instance.mapIndex;
		List<int> list = new List<int> { -1, -1, -1 };
		List<float> list2 = new List<float> { 9999f, 9999f, 9999f };
		foreach (KeyValuePair<int, BaseMapCompont> item in mapIndex)
		{
			MapSeaCompent mapSeaCompent = (MapSeaCompent)item.Value;
			if (!mapSeaCompent.NodeHasIsLand())
			{
				continue;
			}
			float num = Vector3.Distance(((Component)mapSeaCompent).transform.position, ((Component)this).transform.position);
			for (int i = 0; i < list2.Count; i++)
			{
				if (num < list2[i] && mapSeaCompent.NodeIndex != NearlIslandIndex)
				{
					list2[i] = num;
					list[i] = mapSeaCompent.NodeIndex;
					break;
				}
			}
		}
		if (list.Count > 0)
		{
			return list[jsonData.GetRandom() % list.Count];
		}
		return -1;
	}

	public bool IsInPlayerScope()
	{
		if (EndlessSeaMag.Inst.IsInSeeType(ShenShi, NowMapIndex, EndlessSeaMag.Inst.getAvatarNowMapIndex()))
		{
			return true;
		}
		return false;
	}

	public bool IsTimeToMove()
	{
		if ((Tools.instance.getPlayer().worldTimeMag.getNowTime() - LastMoveTime).Days >= speed)
		{
			return true;
		}
		return false;
	}

	public void MonstarMove(int EndPositon, int timeDay, int speed)
	{
		LuXian = EndlessSeaMag.Inst.GetRoadXian(NowMapIndex, EndPositon);
		int num = timeDay / speed;
		List<int> list = new List<int>();
		for (int i = 0; i < num && i < LuXian.Count; i++)
		{
			list.Add(LuXian[i]);
		}
		((Component)this).GetComponent<SeaAvatarObjBase>().PlayMove(list);
	}

	public void ShowMonstarObj()
	{
		if (!IsShow)
		{
			IsShow = true;
			iTween.ScaleTo(objBase, iTween.Hash(new object[10] { "x", 1, "y", 1, "z", 1, "time", 1f, "islocal", true }));
		}
	}

	public void HideMonstarObj()
	{
		if (IsShow)
		{
			IsShow = false;
			iTween.ScaleTo(objBase, iTween.Hash(new object[10] { "x", 0, "y", 0, "z", 0, "time", 1f, "islocal", true }));
		}
	}

	public void EventShiJian()
	{
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		if (_EventId == 0)
		{
			return;
		}
		int eventId = _EventId;
		JToken val = jsonData.instance.EndlessSeaNPCData[string.Concat(eventId)];
		int num = (int)val[(object)"EventList"];
		_ = (int)val[(object)"once"];
		int num2 = 0;
		foreach (JToken item in (JArray)val[(object)"stvalueid"])
		{
			GlobalValue.Set((int)item, (int)val[(object)"stvalue"][(object)num2], "SeaAvatarObjBase.EventShiJian 海上事件变量");
			num2++;
		}
		Object.Instantiate<GameObject>(Resources.Load<GameObject>("talkPrefab/TalkPrefab/talk" + num)).GetComponentInChildren<Flowchart>().SetStringVariable("uuid", UUID);
	}

	public void addOption(int talkID)
	{
		int key = int.Parse(Tools.getScreenName().Replace("F", ""));
		AddOption obj = new AddOption();
		obj.AllMapShiJianOptionJsonData = jsonData.instance.FuBenJsonData[key][1];
		obj.AllMapOptionJsonData = jsonData.instance.FuBenJsonData[key][2];
		obj.addOption(talkID);
	}

	public IEnumerator IEplaymove(List<int> MoveLine)
	{
		if (MoveLine.Count > 0)
		{
			((MapSeaCompent)AllMapManage.instance.mapIndex[MoveLine[0]]).MonstarMoveToThis(this);
			yield return (object)new WaitForSeconds(1f);
			MoveLine.RemoveAt(0);
			((MonoBehaviour)this).StartCoroutine(IEplaymove(MoveLine));
		}
		else
		{
			((Component)this).GetComponent<Animator>().SetInteger("speed", 0);
		}
	}

	private void Update()
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		if (Vector3.Distance(((Component)AllMapManage.instance.MapPlayerController).transform.position, ((Component)this).transform.position) <= 2f)
		{
			if (EndlessSeaMag.Inst.NeedRefresh)
			{
				EndlessSeaMag.Inst.flagMonstarTarget = true;
				CanEventTrager = true;
				EndlessSeaMag.Inst.NeedRefresh = false;
				NeedStop = true;
			}
			if (!NeedStop)
			{
				if (AIEventType == EventType.initiative && CanEventTrager && EndlessSeaMag.Inst.flagMonstarTarget)
				{
					EndlessSeaMag.Inst.flagMonstarTarget = false;
					CanEventTrager = false;
					EndlessSeaMag.Inst.StopAllContens();
					EventShiJian();
				}
				else if (!IsCollect)
				{
					IsCollect = true;
				}
				if (!EndlessSeaMag.Inst.flagMonstarTarget && IsCollect)
				{
					IsCollect = false;
				}
			}
		}
		else
		{
			if (NeedStop)
			{
				NeedStop = false;
			}
			if (IsCollect)
			{
				IsCollect = false;
			}
		}
	}
}
