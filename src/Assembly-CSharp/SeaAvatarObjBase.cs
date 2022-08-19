using System;
using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using Fungus;
using KBEngine;
using Newtonsoft.Json.Linq;
using Spine.Unity.Examples;
using UnityEngine;

// Token: 0x0200019D RID: 413
public class SeaAvatarObjBase : MonoBehaviour
{
	// Token: 0x17000217 RID: 535
	// (get) Token: 0x0600119E RID: 4510 RVA: 0x0006AD12 File Offset: 0x00068F12
	// (set) Token: 0x0600119F RID: 4511 RVA: 0x0006AD1A File Offset: 0x00068F1A
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

	// Token: 0x17000218 RID: 536
	// (get) Token: 0x060011A0 RID: 4512 RVA: 0x0006AD2E File Offset: 0x00068F2E
	public int MenPai
	{
		get
		{
			return (int)jsonData.instance.EndlessSeaNPCData[string.Concat(this._EventId)]["shiliID"];
		}
	}

	// Token: 0x17000219 RID: 537
	// (get) Token: 0x060011A1 RID: 4513 RVA: 0x0006AD5E File Offset: 0x00068F5E
	public int ShenShi
	{
		get
		{
			return (int)this.Json["ShenShi"];
		}
	}

	// Token: 0x1700021A RID: 538
	// (get) Token: 0x060011A2 RID: 4514 RVA: 0x0006AD75 File Offset: 0x00068F75
	public int LV
	{
		get
		{
			return (int)jsonData.instance.EndlessSeaNPCData[string.Concat(this._EventId)]["EventLV"];
		}
	}

	// Token: 0x1700021B RID: 539
	// (get) Token: 0x060011A3 RID: 4515 RVA: 0x0006ADA5 File Offset: 0x00068FA5
	public JObject Json
	{
		get
		{
			return (JObject)jsonData.instance.EndlessSeaNPCData[string.Concat(this._EventId)];
		}
	}

	// Token: 0x1700021C RID: 540
	// (get) Token: 0x060011A4 RID: 4516 RVA: 0x0006ADCB File Offset: 0x00068FCB
	public SeaAvatarObjBase.EventType AIEventType
	{
		get
		{
			return (SeaAvatarObjBase.EventType)((int)jsonData.instance.EndlessSeaAIChuFa[this.ThinkType.ToString()]["type"]);
		}
	}

	// Token: 0x060011A5 RID: 4517 RVA: 0x0006ADF6 File Offset: 0x00068FF6
	private void Start()
	{
		this.LastMoveTime = Tools.instance.getPlayer().worldTimeMag.getNowTime();
		this.InitNearlIsLand();
	}

	// Token: 0x060011A6 RID: 4518 RVA: 0x0006AE18 File Offset: 0x00069018
	public void InitNearlIsLand()
	{
		int seaIslandIndex = Tools.instance.getPlayer().seaNodeMag.GetSeaIslandIndex(this.SeaId);
		this.NearlIslandIndex = EndlessSeaMag.GetRealIndex(this.SeaId, seaIslandIndex);
	}

	// Token: 0x060011A7 RID: 4519 RVA: 0x0006AE52 File Offset: 0x00069052
	public void PlayMove(List<int> MoveLine)
	{
		base.StartCoroutine(this.IEplaymove(MoveLine));
	}

	// Token: 0x060011A8 RID: 4520 RVA: 0x0006AE64 File Offset: 0x00069064
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

	// Token: 0x060011A9 RID: 4521 RVA: 0x0006AEFC File Offset: 0x000690FC
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

	// Token: 0x060011AA RID: 4522 RVA: 0x0006AF74 File Offset: 0x00069174
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

	// Token: 0x060011AB RID: 4523 RVA: 0x0006AFE4 File Offset: 0x000691E4
	public void moveToNearlIsland()
	{
		int nearlIsLand = this.GetNearlIsLand();
		if (nearlIsLand > 0)
		{
			this.MonstarMoveToPositon(nearlIsLand);
		}
	}

	// Token: 0x060011AC RID: 4524 RVA: 0x0006B004 File Offset: 0x00069204
	public void moveAwayFromPositon()
	{
		int awayFromPositon = this.GetAwayFromPositon(true);
		this.MonstarMoveToPositon(awayFromPositon);
	}

	// Token: 0x060011AD RID: 4525 RVA: 0x0006B020 File Offset: 0x00069220
	public void ResetBehavirTree(int type)
	{
		this.behaviorTree.ExternalBehavior = EndlessSeaMag.Inst.externalBehaviorTrees[type];
		BehaviorManager.instance.RestartBehavior(this.behaviorTree);
	}

	// Token: 0x060011AE RID: 4526 RVA: 0x0006B050 File Offset: 0x00069250
	public void MonstarMoveToPositon(int endIndex)
	{
		Avatar player = PlayerEx.Player;
		int days = (player.worldTimeMag.getNowTime() - this.LastMoveTime).Days;
		this.MonstarMove(endIndex, days, this.speed);
		this.LastMoveTime = player.worldTimeMag.getNowTime();
	}

	// Token: 0x060011AF RID: 4527 RVA: 0x0006B0A4 File Offset: 0x000692A4
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

	// Token: 0x060011B0 RID: 4528 RVA: 0x0006B1C8 File Offset: 0x000693C8
	public bool WhetherCanMove()
	{
		return this.IsInPlayerScope() && this.IsTimeToMove();
	}

	// Token: 0x060011B1 RID: 4529 RVA: 0x0006B1DD File Offset: 0x000693DD
	public int GetNearlIsLand()
	{
		if (this.NowMapIndex == this.NearlIslandIndex)
		{
			this.ResetNearlIsland();
		}
		return this.NearlIslandIndex;
	}

	// Token: 0x060011B2 RID: 4530 RVA: 0x0006B1FC File Offset: 0x000693FC
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

	// Token: 0x060011B3 RID: 4531 RVA: 0x0006B280 File Offset: 0x00069480
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

	// Token: 0x060011B4 RID: 4532 RVA: 0x0006B3B8 File Offset: 0x000695B8
	public bool IsInPlayerScope()
	{
		return EndlessSeaMag.Inst.IsInSeeType(this.ShenShi, this.NowMapIndex, EndlessSeaMag.Inst.getAvatarNowMapIndex());
	}

	// Token: 0x060011B5 RID: 4533 RVA: 0x0006B3E0 File Offset: 0x000695E0
	public bool IsTimeToMove()
	{
		return (Tools.instance.getPlayer().worldTimeMag.getNowTime() - this.LastMoveTime).Days >= this.speed;
	}

	// Token: 0x060011B6 RID: 4534 RVA: 0x0006B420 File Offset: 0x00069620
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

	// Token: 0x060011B7 RID: 4535 RVA: 0x0006B488 File Offset: 0x00069688
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

	// Token: 0x060011B8 RID: 4536 RVA: 0x0006B518 File Offset: 0x00069718
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

	// Token: 0x060011B9 RID: 4537 RVA: 0x0006B5A8 File Offset: 0x000697A8
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

	// Token: 0x060011BA RID: 4538 RVA: 0x0006B6A8 File Offset: 0x000698A8
	public void addOption(int talkID)
	{
		int key = int.Parse(Tools.getScreenName().Replace("F", ""));
		new AddOption
		{
			AllMapShiJianOptionJsonData = jsonData.instance.FuBenJsonData[key][1],
			AllMapOptionJsonData = jsonData.instance.FuBenJsonData[key][2]
		}.addOption(talkID);
	}

	// Token: 0x060011BB RID: 4539 RVA: 0x0006B712 File Offset: 0x00069912
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

	// Token: 0x060011BC RID: 4540 RVA: 0x0006B728 File Offset: 0x00069928
	private void Update()
	{
		if (Vector3.Distance(AllMapManage.instance.MapPlayerController.transform.position, base.transform.position) <= 2f)
		{
			if (EndlessSeaMag.Inst.NeedRefresh)
			{
				EndlessSeaMag.Inst.flagMonstarTarget = true;
				this.CanEventTrager = true;
				EndlessSeaMag.Inst.NeedRefresh = false;
				this.NeedStop = true;
			}
			if (this.NeedStop)
			{
				return;
			}
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
		else
		{
			if (this.NeedStop)
			{
				this.NeedStop = false;
			}
			if (this.IsCollect)
			{
				this.IsCollect = false;
			}
		}
	}

	// Token: 0x04000CA0 RID: 3232
	public int speed = 20;

	// Token: 0x04000CA1 RID: 3233
	public int _EventId;

	// Token: 0x04000CA2 RID: 3234
	public int ThinkType = 1;

	// Token: 0x04000CA3 RID: 3235
	public DateTime LastMoveTime;

	// Token: 0x04000CA4 RID: 3236
	public List<int> LuXian = new List<int>();

	// Token: 0x04000CA5 RID: 3237
	public int NowMapIndex;

	// Token: 0x04000CA6 RID: 3238
	public bool NeedStop;

	// Token: 0x04000CA7 RID: 3239
	public string NearWaitTime = "0001-01-01";

	// Token: 0x04000CA8 RID: 3240
	public int NearlIslandIndex;

	// Token: 0x04000CA9 RID: 3241
	private bool isCollect;

	// Token: 0x04000CAA RID: 3242
	public BehaviorTree behaviorTree;

	// Token: 0x04000CAB RID: 3243
	public bool ISNTaskMonstar;

	// Token: 0x04000CAC RID: 3244
	public GameObject NtaskSpine;

	// Token: 0x04000CAD RID: 3245
	public GameObject objBase;

	// Token: 0x04000CAE RID: 3246
	public RuntimeAnimatorController HaiGuaiAnimCtl;

	// Token: 0x04000CAF RID: 3247
	public RuntimeAnimatorController ChuanAnimCtl;

	// Token: 0x04000CB0 RID: 3248
	public List<SkeletonAnimationHandleExample.StateNameToAnimationReference> ChuanStatesAndAnimations = new List<SkeletonAnimationHandleExample.StateNameToAnimationReference>();

	// Token: 0x04000CB1 RID: 3249
	private bool IsShow = true;

	// Token: 0x04000CB2 RID: 3250
	public int SeaId;

	// Token: 0x04000CB3 RID: 3251
	public string UUID;

	// Token: 0x04000CB4 RID: 3252
	private bool CanEventTrager = true;

	// Token: 0x020012B8 RID: 4792
	public enum EventType
	{
		// Token: 0x0400666F RID: 26223
		initiative,
		// Token: 0x04006670 RID: 26224
		passivity
	}

	// Token: 0x020012B9 RID: 4793
	public enum Directon
	{
		// Token: 0x04006672 RID: 26226
		UP,
		// Token: 0x04006673 RID: 26227
		Down,
		// Token: 0x04006674 RID: 26228
		Left,
		// Token: 0x04006675 RID: 26229
		Right
	}

	// Token: 0x020012BA RID: 4794
	public enum AIState
	{
		// Token: 0x04006677 RID: 26231
		WaitStop = 1,
		// Token: 0x04006678 RID: 26232
		ToPlayer,
		// Token: 0x04006679 RID: 26233
		ToPlayerNotGongJi,
		// Token: 0x0400667A RID: 26234
		GouAway,
		// Token: 0x0400667B RID: 26235
		ToNeralIsLand
	}
}
