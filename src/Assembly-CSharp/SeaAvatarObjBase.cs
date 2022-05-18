using System;
using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using Fungus;
using KBEngine;
using Newtonsoft.Json.Linq;
using Spine.Unity.Examples;
using UnityEngine;

// Token: 0x02000295 RID: 661
public class SeaAvatarObjBase : MonoBehaviour
{
	// Token: 0x1700025D RID: 605
	// (get) Token: 0x0600143F RID: 5183 RVA: 0x00012C86 File Offset: 0x00010E86
	// (set) Token: 0x06001440 RID: 5184 RVA: 0x00012C8E File Offset: 0x00010E8E
	public bool IsCollect
	{
		get
		{
			return this.isCollect;
		}
		set
		{
			this.isCollect = value;
			NpcJieSuanManager.inst.isUpDateNpcList = true;
		}
	}

	// Token: 0x1700025E RID: 606
	// (get) Token: 0x06001441 RID: 5185 RVA: 0x00012CA2 File Offset: 0x00010EA2
	public int MenPai
	{
		get
		{
			return (int)jsonData.instance.EndlessSeaNPCData[string.Concat(this._EventId)]["shiliID"];
		}
	}

	// Token: 0x1700025F RID: 607
	// (get) Token: 0x06001442 RID: 5186 RVA: 0x00012CD2 File Offset: 0x00010ED2
	public int ShenShi
	{
		get
		{
			return (int)this.Json["ShenShi"];
		}
	}

	// Token: 0x17000260 RID: 608
	// (get) Token: 0x06001443 RID: 5187 RVA: 0x00012CE9 File Offset: 0x00010EE9
	public int LV
	{
		get
		{
			return (int)jsonData.instance.EndlessSeaNPCData[string.Concat(this._EventId)]["EventLV"];
		}
	}

	// Token: 0x17000261 RID: 609
	// (get) Token: 0x06001444 RID: 5188 RVA: 0x00012D19 File Offset: 0x00010F19
	public JObject Json
	{
		get
		{
			return (JObject)jsonData.instance.EndlessSeaNPCData[string.Concat(this._EventId)];
		}
	}

	// Token: 0x17000262 RID: 610
	// (get) Token: 0x06001445 RID: 5189 RVA: 0x00012D3F File Offset: 0x00010F3F
	public SeaAvatarObjBase.EventType AIEventType
	{
		get
		{
			return (SeaAvatarObjBase.EventType)((int)jsonData.instance.EndlessSeaAIChuFa[this.ThinkType.ToString()]["type"]);
		}
	}

	// Token: 0x06001446 RID: 5190 RVA: 0x00012D6A File Offset: 0x00010F6A
	private void Start()
	{
		this.LastMoveTime = Tools.instance.getPlayer().worldTimeMag.getNowTime();
		this.InitNearlIsLand();
	}

	// Token: 0x06001447 RID: 5191 RVA: 0x000B9068 File Offset: 0x000B7268
	public void InitNearlIsLand()
	{
		int seaIslandIndex = Tools.instance.getPlayer().seaNodeMag.GetSeaIslandIndex(this.SeaId);
		this.NearlIslandIndex = EndlessSeaMag.GetRealIndex(this.SeaId, seaIslandIndex);
	}

	// Token: 0x06001448 RID: 5192 RVA: 0x00012D8C File Offset: 0x00010F8C
	public void PlayMove(List<int> MoveLine)
	{
		base.StartCoroutine(this.IEplaymove(MoveLine));
	}

	// Token: 0x06001449 RID: 5193 RVA: 0x000B90A4 File Offset: 0x000B72A4
	public void Think()
	{
		if (!this.IsTimeToMove())
		{
			return;
		}
		if (!this.IsInPlayerScope())
		{
			this.LastMoveTime = Tools.instance.getPlayer().worldTimeMag.getNowTime();
			return;
		}
		Avatar player = Tools.instance.getPlayer();
		if (player.ItemSeid27Days() > 0 && (int)this.Json["EventType"] == 1)
		{
			int type = (int)player.ItemBuffList["27"]["AIType"];
			this.ResetAITpe(type);
		}
		this.behaviorTree.EnableBehavior();
	}

	// Token: 0x0600144A RID: 5194 RVA: 0x000B913C File Offset: 0x000B733C
	public void ResetAITpe(int type)
	{
		if (this.ThinkType == type)
		{
			return;
		}
		this.ThinkType = type;
		if (!EndlessSeaMag.Inst.externalBehaviorTrees.ContainsKey(type))
		{
			EndlessSeaMag.Inst.externalBehaviorTrees[type] = (Resources.Load("MapPrefab/SeaAI/SAI" + type) as ExternalBehaviorTree);
		}
		this.behaviorTree.ExternalBehavior = EndlessSeaMag.Inst.externalBehaviorTrees[type];
	}

	// Token: 0x0600144B RID: 5195 RVA: 0x000B91B4 File Offset: 0x000B73B4
	public void MonstarMoveToPlayer()
	{
		int nowIndex = PlayerEx.Player.fubenContorl[Tools.getScreenName()].NowIndex;
		List<int> roadXian = EndlessSeaMag.Inst.GetRoadXian(this.NowMapIndex, nowIndex);
		if (roadXian.Count > 0)
		{
			int num = roadXian[0];
			if (!((MapSeaCompent)AllMapManage.instance.mapIndex[num]).NodeHasIsLand())
			{
				this.MonstarMoveToPositon(num);
			}
		}
	}

	// Token: 0x0600144C RID: 5196 RVA: 0x000B9224 File Offset: 0x000B7424
	public void moveToNearlIsland()
	{
		int nearlIsLand = this.GetNearlIsLand();
		if (nearlIsLand > 0)
		{
			this.MonstarMoveToPositon(nearlIsLand);
		}
	}

	// Token: 0x0600144D RID: 5197 RVA: 0x000B9244 File Offset: 0x000B7444
	public void moveAwayFromPositon()
	{
		int awayFromPositon = this.GetAwayFromPositon(true);
		this.MonstarMoveToPositon(awayFromPositon);
	}

	// Token: 0x0600144E RID: 5198 RVA: 0x00012D9C File Offset: 0x00010F9C
	public void ResetBehavirTree(int type)
	{
		this.behaviorTree.ExternalBehavior = EndlessSeaMag.Inst.externalBehaviorTrees[type];
		BehaviorManager.instance.RestartBehavior(this.behaviorTree);
	}

	// Token: 0x0600144F RID: 5199 RVA: 0x000B9260 File Offset: 0x000B7460
	public void MonstarMoveToPositon(int endIndex)
	{
		Avatar player = PlayerEx.Player;
		int days = (player.worldTimeMag.getNowTime() - this.LastMoveTime).Days;
		this.MonstarMove(endIndex, days, this.speed);
		this.LastMoveTime = player.worldTimeMag.getNowTime();
	}

	// Token: 0x06001450 RID: 5200 RVA: 0x000B92B4 File Offset: 0x000B74B4
	public int GetAwayFromPositon(bool ignoreIsland = false)
	{
		List<int> nextIndex = ((MapSeaCompent)AllMapManage.instance.mapIndex[this.NowMapIndex]).nextIndex;
		int nowIndex = Tools.instance.getPlayer().fubenContorl[Tools.getScreenName()].NowIndex;
		MapSeaCompent mapSeaCompent = (MapSeaCompent)AllMapManage.instance.mapIndex[nowIndex];
		int result = 0;
		float num = 0f;
		foreach (int num2 in nextIndex)
		{
			MapSeaCompent mapSeaCompent2 = (MapSeaCompent)AllMapManage.instance.mapIndex[num2];
			if (!(mapSeaCompent2.transform.GetComponent<SeaToOherScene>() != null) && (!ignoreIsland || !mapSeaCompent2.NodeHasIsLand()))
			{
				float num3 = Vector3.Distance(mapSeaCompent.transform.position, mapSeaCompent2.transform.position);
				if (num == num3 && jsonData.instance.QuikeGetRandom() % 2 == 0)
				{
					result = num2;
				}
				if (num < num3)
				{
					result = num2;
					num = num3;
				}
			}
		}
		return result;
	}

	// Token: 0x06001451 RID: 5201 RVA: 0x00012DC9 File Offset: 0x00010FC9
	public bool WhetherCanMove()
	{
		return this.IsInPlayerScope() && this.IsTimeToMove();
	}

	// Token: 0x06001452 RID: 5202 RVA: 0x00012DDE File Offset: 0x00010FDE
	public int GetNearlIsLand()
	{
		if (this.NowMapIndex == this.NearlIslandIndex)
		{
			this.ResetNearlIsland();
		}
		return this.NearlIslandIndex;
	}

	// Token: 0x06001453 RID: 5203 RVA: 0x000B93D8 File Offset: 0x000B75D8
	public void ResetNearlIsland()
	{
		DateTime d = DateTime.Parse(this.NearWaitTime);
		DateTime nowTime = Tools.instance.getPlayer().worldTimeMag.getNowTime();
		if ((nowTime - d).Days > 120)
		{
			this.NearWaitTime = Tools.instance.getPlayer().worldTimeMag.nowTime;
			d = DateTime.Parse(this.NearWaitTime);
		}
		if ((nowTime - d).Days > 60)
		{
			this.NearlIslandIndex = this.GetAtherNearlIsLand();
		}
	}

	// Token: 0x06001454 RID: 5204 RVA: 0x000B945C File Offset: 0x000B765C
	public int GetAtherNearlIsLand()
	{
		Dictionary<int, BaseMapCompont> mapIndex = AllMapManage.instance.mapIndex;
		List<int> list = new List<int>
		{
			-1,
			-1,
			-1
		};
		List<float> list2 = new List<float>
		{
			9999f,
			9999f,
			9999f
		};
		foreach (KeyValuePair<int, BaseMapCompont> keyValuePair in mapIndex)
		{
			MapSeaCompent mapSeaCompent = (MapSeaCompent)keyValuePair.Value;
			if (mapSeaCompent.NodeHasIsLand())
			{
				float num = Vector3.Distance(mapSeaCompent.transform.position, base.transform.position);
				for (int i = 0; i < list2.Count; i++)
				{
					if (num < list2[i] && mapSeaCompent.NodeIndex != this.NearlIslandIndex)
					{
						list2[i] = num;
						list[i] = mapSeaCompent.NodeIndex;
						break;
					}
				}
			}
		}
		if (list.Count > 0)
		{
			return list[jsonData.GetRandom() % list.Count];
		}
		return -1;
	}

	// Token: 0x06001455 RID: 5205 RVA: 0x00012DFA File Offset: 0x00010FFA
	public bool IsInPlayerScope()
	{
		return EndlessSeaMag.Inst.IsInSeeType(this.ShenShi, this.NowMapIndex, EndlessSeaMag.Inst.getAvatarNowMapIndex());
	}

	// Token: 0x06001456 RID: 5206 RVA: 0x000B9594 File Offset: 0x000B7794
	public bool IsTimeToMove()
	{
		return (Tools.instance.getPlayer().worldTimeMag.getNowTime() - this.LastMoveTime).Days >= this.speed;
	}

	// Token: 0x06001457 RID: 5207 RVA: 0x000B95D4 File Offset: 0x000B77D4
	public void MonstarMove(int EndPositon, int timeDay, int speed)
	{
		this.LuXian = EndlessSeaMag.Inst.GetRoadXian(this.NowMapIndex, EndPositon);
		int num = timeDay / speed;
		List<int> list = new List<int>();
		int num2 = 0;
		while (num2 < num && num2 < this.LuXian.Count)
		{
			list.Add(this.LuXian[num2]);
			num2++;
		}
		base.GetComponent<SeaAvatarObjBase>().PlayMove(list);
	}

	// Token: 0x06001458 RID: 5208 RVA: 0x000B963C File Offset: 0x000B783C
	public void ShowMonstarObj()
	{
		if (!this.IsShow)
		{
			this.IsShow = true;
			iTween.ScaleTo(this.objBase, iTween.Hash(new object[]
			{
				"x",
				1,
				"y",
				1,
				"z",
				1,
				"time",
				1f,
				"islocal",
				true
			}));
		}
	}

	// Token: 0x06001459 RID: 5209 RVA: 0x000B96CC File Offset: 0x000B78CC
	public void HideMonstarObj()
	{
		if (this.IsShow)
		{
			this.IsShow = false;
			iTween.ScaleTo(this.objBase, iTween.Hash(new object[]
			{
				"x",
				0,
				"y",
				0,
				"z",
				0,
				"time",
				1f,
				"islocal",
				true
			}));
		}
	}

	// Token: 0x0600145A RID: 5210 RVA: 0x000B975C File Offset: 0x000B795C
	public void EventShiJian()
	{
		if (this._EventId == 0)
		{
			return;
		}
		int eventId = this._EventId;
		JToken jtoken = jsonData.instance.EndlessSeaNPCData[string.Concat(eventId)];
		int num = (int)jtoken["EventList"];
		(int)jtoken["once"];
		int num2 = 0;
		foreach (JToken jtoken2 in ((JArray)jtoken["stvalueid"]))
		{
			GlobalValue.Set((int)jtoken2, (int)jtoken["stvalue"][num2], "SeaAvatarObjBase.EventShiJian 海上事件变量");
			num2++;
		}
		Object.Instantiate<GameObject>(Resources.Load<GameObject>("talkPrefab/TalkPrefab/talk" + num)).GetComponentInChildren<Flowchart>().SetStringVariable("uuid", this.UUID);
	}

	// Token: 0x0600145B RID: 5211 RVA: 0x000B4FF4 File Offset: 0x000B31F4
	public void addOption(int talkID)
	{
		int key = int.Parse(Tools.getScreenName().Replace("F", ""));
		new AddOption
		{
			AllMapShiJianOptionJsonData = jsonData.instance.FuBenJsonData[key][1],
			AllMapOptionJsonData = jsonData.instance.FuBenJsonData[key][2]
		}.addOption(talkID);
	}

	// Token: 0x0600145C RID: 5212 RVA: 0x00012E21 File Offset: 0x00011021
	public IEnumerator IEplaymove(List<int> MoveLine)
	{
		if (MoveLine.Count > 0)
		{
			((MapSeaCompent)AllMapManage.instance.mapIndex[MoveLine[0]]).MonstarMoveToThis(this);
			yield return new WaitForSeconds(1f);
			MoveLine.RemoveAt(0);
			base.StartCoroutine(this.IEplaymove(MoveLine));
		}
		else
		{
			base.GetComponent<Animator>().SetInteger("speed", 0);
		}
		yield break;
	}

	// Token: 0x0600145D RID: 5213 RVA: 0x000B985C File Offset: 0x000B7A5C
	private void Update()
	{
		if (Vector3.Distance(AllMapManage.instance.MapPlayerController.transform.position, base.transform.position) <= 2f)
		{
			if (this.AIEventType == SeaAvatarObjBase.EventType.initiative && this.CanEventTrager && EndlessSeaMag.Inst.flagMonstarTarget)
			{
				EndlessSeaMag.Inst.flagMonstarTarget = false;
				this.CanEventTrager = false;
				EndlessSeaMag.Inst.StopAllContens();
				this.EventShiJian();
			}
			else if (!this.IsCollect)
			{
				this.IsCollect = true;
			}
			if (!EndlessSeaMag.Inst.flagMonstarTarget && this.IsCollect)
			{
				this.IsCollect = false;
				return;
			}
		}
		else if (this.IsCollect)
		{
			this.IsCollect = false;
		}
	}

	// Token: 0x04000FB3 RID: 4019
	public int speed = 20;

	// Token: 0x04000FB4 RID: 4020
	public int _EventId;

	// Token: 0x04000FB5 RID: 4021
	public int ThinkType = 1;

	// Token: 0x04000FB6 RID: 4022
	public DateTime LastMoveTime;

	// Token: 0x04000FB7 RID: 4023
	public List<int> LuXian = new List<int>();

	// Token: 0x04000FB8 RID: 4024
	public int NowMapIndex;

	// Token: 0x04000FB9 RID: 4025
	public string NearWaitTime = "0001-01-01";

	// Token: 0x04000FBA RID: 4026
	public int NearlIslandIndex;

	// Token: 0x04000FBB RID: 4027
	private bool isCollect;

	// Token: 0x04000FBC RID: 4028
	public BehaviorTree behaviorTree;

	// Token: 0x04000FBD RID: 4029
	public bool ISNTaskMonstar;

	// Token: 0x04000FBE RID: 4030
	public GameObject NtaskSpine;

	// Token: 0x04000FBF RID: 4031
	public GameObject objBase;

	// Token: 0x04000FC0 RID: 4032
	public RuntimeAnimatorController HaiGuaiAnimCtl;

	// Token: 0x04000FC1 RID: 4033
	public RuntimeAnimatorController ChuanAnimCtl;

	// Token: 0x04000FC2 RID: 4034
	public List<SkeletonAnimationHandleExample.StateNameToAnimationReference> ChuanStatesAndAnimations = new List<SkeletonAnimationHandleExample.StateNameToAnimationReference>();

	// Token: 0x04000FC3 RID: 4035
	private bool IsShow = true;

	// Token: 0x04000FC4 RID: 4036
	public int SeaId;

	// Token: 0x04000FC5 RID: 4037
	public string UUID;

	// Token: 0x04000FC6 RID: 4038
	private bool CanEventTrager = true;

	// Token: 0x02000296 RID: 662
	public enum EventType
	{
		// Token: 0x04000FC8 RID: 4040
		initiative,
		// Token: 0x04000FC9 RID: 4041
		passivity
	}

	// Token: 0x02000297 RID: 663
	public enum Directon
	{
		// Token: 0x04000FCB RID: 4043
		UP,
		// Token: 0x04000FCC RID: 4044
		Down,
		// Token: 0x04000FCD RID: 4045
		Left,
		// Token: 0x04000FCE RID: 4046
		Right
	}

	// Token: 0x02000298 RID: 664
	public enum AIState
	{
		// Token: 0x04000FD0 RID: 4048
		WaitStop = 1,
		// Token: 0x04000FD1 RID: 4049
		ToPlayer,
		// Token: 0x04000FD2 RID: 4050
		ToPlayerNotGongJi,
		// Token: 0x04000FD3 RID: 4051
		GouAway,
		// Token: 0x04000FD4 RID: 4052
		ToNeralIsLand
	}
}
