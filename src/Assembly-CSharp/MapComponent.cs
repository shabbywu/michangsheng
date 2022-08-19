using System;
using System.Collections;
using System.Collections.Generic;
using Fungus;
using GetWay;
using JSONClass;
using KBEngine;
using Spine;
using Spine.Unity;
using UnityEngine;
using UnityEngine.Events;
using YSGame;

// Token: 0x0200018E RID: 398
public class MapComponent : BaseMapCompont
{
	// Token: 0x060010F8 RID: 4344 RVA: 0x0006543E File Offset: 0x0006363E
	private new void Awake()
	{
		this.isInit = false;
		base.Awake();
	}

	// Token: 0x060010F9 RID: 4345 RVA: 0x00065450 File Offset: 0x00063650
	public override void AvatarMoveToThis()
	{
		GameObject gameObject = GameObject.Find("MapMoveNode");
		if (gameObject == null)
		{
			UIPopTip.Inst.Pop("找不到移动路径跟节点", PopTipIconType.叹号);
			return;
		}
		Avatar player = PlayerEx.Player;
		MapPlayerController playerController = AllMapManage.instance.MapPlayerController;
		playerController.SetSpeed(1);
		AllMapManage.instance.isPlayMove = true;
		MapMoveNode[] componentsInChildren = gameObject.GetComponentsInChildren<MapMoveNode>();
		List<GameObject> Nodes = new List<GameObject>();
		Nodes.Add(playerController.gameObject);
		List<GameObject> list = new List<GameObject>();
		if (playerController.ShowType != MapPlayerShowType.遁术)
		{
			foreach (MapMoveNode mapMoveNode in componentsInChildren)
			{
				if ((mapMoveNode.StartNode == this.NodeIndex && this.ComAvatar.NowMapIndex == mapMoveNode.EndNode) || (mapMoveNode.EndNode == this.NodeIndex && this.ComAvatar.NowMapIndex == mapMoveNode.StartNode))
				{
					list.Add(mapMoveNode.gameObject);
				}
			}
			if (list.Count > 0 && this.ComAvatar.NowMapIndex == list[0].GetComponent<MapMoveNode>().EndNode)
			{
				list.Reverse();
			}
			foreach (GameObject item in list)
			{
				Nodes.Add(item);
			}
		}
		Transform transform = base.transform.Find("PlayerPosition");
		Nodes.Add((transform != null) ? transform.gameObject : AllMapManage.instance.mapIndex[this.NodeIndex].gameObject);
		int count = Nodes.Count;
		for (int j = 1; j < Nodes.Count; j++)
		{
			int nodeIndex = j;
			Queue<UnityAction> queue = new Queue<UnityAction>();
			UnityAction item2 = delegate()
			{
				playerController.SetSpeed(1);
				float num = Vector2.Distance(Nodes[nodeIndex - 1].transform.position, Nodes[nodeIndex].transform.position);
				float num2 = (Tools.instance.getPlayer().dunSu > 100) ? (this.MoveBaseSpeedMin + this.MoveBaseSpeed) : (this.MoveBaseSpeedMin + this.MoveBaseSpeed * ((float)Tools.instance.getPlayer().dunSu / 100f));
				if (playerController.ShowType == MapPlayerShowType.遁术)
				{
					num2 *= 2f;
				}
				float num3 = num / num2;
				iTween.MoveTo(playerController.gameObject, iTween.Hash(new object[]
				{
					"x",
					Nodes[nodeIndex].transform.position.x,
					"y",
					Nodes[nodeIndex].transform.position.y,
					"z",
					playerController.transform.position.z,
					"time",
					num3,
					"islocal",
					false,
					"EaseType",
					"linear"
				}));
				WASDMove.waitTime = num3;
				WASDMove.needWait = true;
				this.Invoke("callContinue", num3);
			};
			queue.Enqueue(item2);
			YSFuncList.Ints.AddFunc(queue);
		}
		Queue<UnityAction> queue2 = new Queue<UnityAction>();
		UnityAction item3 = delegate()
		{
			this.setAvatarNowMapIndex();
			this.showLuDian();
			playerController.SetSpeed(0);
			AllMapManage.instance.isPlayMove = false;
			YSFuncList.Ints.Continue();
			int nowMapIndex = Tools.instance.getPlayer().NowMapIndex;
			this.updateSedNode();
			if (MapGetWay.Inst.CurTalk > 0)
			{
				Resources.Load<GameObject>("talkPrefab/TalkPrefab/talk" + MapGetWay.Inst.CurTalk).Inst(null);
				MapGetWay.Inst.CurTalk = 0;
			}
		};
		queue2.Enqueue(item3);
		YSFuncList.Ints.AddFunc(queue2);
	}

	// Token: 0x060010FA RID: 4346 RVA: 0x000656B8 File Offset: 0x000638B8
	private void callContinue()
	{
		YSFuncList.Ints.Continue();
	}

	// Token: 0x060010FB RID: 4347 RVA: 0x000656C4 File Offset: 0x000638C4
	public override void BaseAddTime()
	{
		Queue<UnityAction> queue = new Queue<UnityAction>();
		UnityAction item = delegate()
		{
			JSONObject jsonobject = this.AllMapCastTimeJsonData.list.Find((JSONObject aa) => (int)aa["dunSu"].n >= this.ComAvatar.dunSu);
			if (jsonobject != null)
			{
				this.ComAvatar.AddTime((int)jsonobject["XiaoHao"].n, 0, 0);
			}
			YSFuncList.Ints.Continue();
		};
		queue.Enqueue(item);
		YSFuncList.Ints.AddFunc(queue);
	}

	// Token: 0x060010FC RID: 4348 RVA: 0x000656F6 File Offset: 0x000638F6
	public override void EventRandom()
	{
		if (Tools.instance.getPlayer().NowMapIndex == this.NodeIndex)
		{
			return;
		}
		if (AllMapManage.instance.MapPlayerController.ShowType == MapPlayerShowType.遁术)
		{
			this.NewEventRandom();
			return;
		}
		base.StartCoroutine(this.Move());
	}

	// Token: 0x060010FD RID: 4349 RVA: 0x00065738 File Offset: 0x00063938
	public void NewEventRandom()
	{
		if (!base.CanClick())
		{
			return;
		}
		this.fuBenSetClick();
		this.NewMovaAvatar();
		if (this.IsStatic)
		{
			return;
		}
		Queue<UnityAction> queue = new Queue<UnityAction>();
		UnityAction item = delegate()
		{
			Avatar avatar = (Avatar)KBEngineApp.app.player();
			int num = avatar.nomelTaskMag.AutoAllMapPlaceHasNTask(new List<int>
			{
				this.NodeIndex
			});
			if (num != -1)
			{
				JSONObject jsonobject = avatar.nomelTaskMag.IsNTaskZiXiangInLuJin(num, new List<int>
				{
					this.NodeIndex
				});
				JSONObject nowChildIDSuiJiJson = avatar.nomelTaskMag.GetNowChildIDSuiJiJson(num);
				if (jsonobject["type"].I == 5)
				{
					avatar.randomFuBenMag.GetInRandomFuBen(this.NodeIndex, -1);
				}
				else
				{
					GlobalValue.Set(401, nowChildIDSuiJiJson["Value"].I, base.GetType().Name + ".EventRandom");
					GlobalValue.Set(402, num, base.GetType().Name + ".EventRandom");
					Object.Instantiate<GameObject>(Resources.Load<GameObject>("talkPrefab/TalkPrefab/talk" + jsonobject["talkID"].str));
				}
				YSFuncList.Ints.Continue();
				return;
			}
			int i = avatar.AllMapRandomNode[this.NodeIndex.ToString()]["EventId"].I;
			int num2 = (int)avatar.AllMapRandomNode[this.NodeIndex.ToString()]["Type"].n;
			if (num2 == 2 || num2 == 5 || avatar.AllMapRandomNode[this.NodeIndex.ToString()]["EventId"].I == 0)
			{
				if (FungusManager.Instance.jieShaBlock == null)
				{
					GameObject gameObject = Object.Instantiate<GameObject>(Resources.Load<GameObject>("talkPrefab/TalkPrefab/talk4010"));
					FungusManager.Instance.jieShaBlock = gameObject.GetComponentInChildren<Flowchart>();
				}
				else if (GlobalValue.Get(171, base.GetType().Name + ".EventRandom") == 1)
				{
					GlobalValue.Set(171, 0, base.GetType().Name + ".EventRandom");
				}
				else
				{
					FungusManager.Instance.jieShaBlock.Reset(false, true);
					FungusManager.Instance.jieShaBlock.ExecuteBlock("Splash");
				}
				this.ResteAllMapNode();
				Tools.instance.getPlayer().AllMapSetNode();
				YSFuncList.Ints.Continue();
				return;
			}
			int i2 = this.MapRandomJsonData[string.Concat(i)]["EventData"].I;
			int i3 = this.MapRandomJsonData[string.Concat(i)]["MosterID"].I;
			if (this.MapRandomJsonData[string.Concat(i)]["once"].I != 0)
			{
				if (!avatar.SuiJiShiJian.HasField(Tools.getScreenName()))
				{
					avatar.SuiJiShiJian.AddField(Tools.getScreenName(), new JSONObject(JSONObject.Type.ARRAY));
				}
				avatar.SuiJiShiJian[Tools.getScreenName()].Add(i);
			}
			switch ((int)this.MapRandomJsonData[string.Concat(i)]["EventList"].n)
			{
			case 0:
				Object.Instantiate<GameObject>(Resources.Load<GameObject>("talkPrefab/TalkPrefab/talk" + i2));
				break;
			case 1:
				this.addOption(i2);
				break;
			case 2:
				Tools.instance.MonstarID = i3;
				Object.Instantiate<GameObject>(Resources.Load<GameObject>("talkPrefab/FightPrefab/Fight" + i2));
				break;
			case 3:
				base.OpenDadituCaiJi();
				break;
			}
			this.ResteAllMapNode();
			Tools.instance.getPlayer().AllMapSetNode();
			YSFuncList.Ints.Continue();
		};
		queue.Enqueue(item);
		YSFuncList.Ints.AddFunc(queue);
	}

	// Token: 0x060010FE RID: 4350 RVA: 0x00065788 File Offset: 0x00063988
	public void NewMovaAvatar()
	{
		base.movaAvatar();
	}

	// Token: 0x060010FF RID: 4351 RVA: 0x00065790 File Offset: 0x00063990
	public override void movaAvatar()
	{
		if (Tools.instance.getPlayer().NowMapIndex == this.NodeIndex)
		{
			return;
		}
		if (AllMapManage.instance.MapPlayerController.ShowType == MapPlayerShowType.遁术)
		{
			base.movaAvatar();
			return;
		}
		base.StartCoroutine(this.Move());
	}

	// Token: 0x06001100 RID: 4352 RVA: 0x000657D0 File Offset: 0x000639D0
	private IEnumerator Move()
	{
		List<int> list = new List<int>();
		if (MapGetWay.Inst.IsNearly(Tools.instance.getPlayer().NowMapIndex, this.NodeIndex))
		{
			list.Add(this.NodeIndex);
		}
		else
		{
			list = MapGetWay.Inst.GetBestList(Tools.instance.getPlayer().NowMapIndex, this.NodeIndex);
		}
		if (list != null)
		{
			int index = 0;
			MapGetWay.Inst.IsStop = false;
			MapGetWay.Inst.CurTalk = 0;
			if (list.Count > 1)
			{
				MapMoveTips.Show();
			}
			while (index < list.Count)
			{
				int num = list[index];
				if (AllMapManage.instance != null && AllMapManage.instance.mapIndex.ContainsKey(num))
				{
					MapGetWay.Inst.CurTalk = 0;
					if (BigMapLoadTalk.DataDict.ContainsKey(num) && BigMapLoadTalk.DataDict[num].Talk > 0)
					{
						MapGetWay.Inst.CurTalk = BigMapLoadTalk.DataDict[num].Talk;
						(AllMapManage.instance.mapIndex[num] as MapComponent).NewMovaAvatar();
					}
					else if (num < 100 || num == 101)
					{
						(AllMapManage.instance.mapIndex[num] as MapComponent).NewMovaAvatar();
					}
					else
					{
						(AllMapManage.instance.mapIndex[num] as MapComponent).NewEventRandom();
					}
				}
				while (AllMapManage.instance.isPlayMove)
				{
					yield return new WaitForSeconds(0.2f);
				}
				yield return new WaitForSeconds(0.3f);
				if (!Tools.instance.canClick(false, true) || MapGetWay.Inst.IsStop)
				{
					break;
				}
				int num2 = index;
				index = num2 + 1;
			}
		}
		MapGetWay.Inst.StopAuToMove();
		yield break;
	}

	// Token: 0x06001101 RID: 4353 RVA: 0x000657E0 File Offset: 0x000639E0
	public override bool YuJianFeiXing()
	{
		if (this.ComAvatar.NowMapIndex == this.NodeIndex)
		{
			return false;
		}
		foreach (SkillItem skillItem in ((Avatar)KBEngineApp.app.player()).equipStaticSkillList)
		{
			if (jsonData.instance.StaticSkillJsonData[string.Concat(Tools.instance.getStaticSkillKeyByID(skillItem.itemId))]["seid"].list.Find((JSONObject aa) => (int)aa.n == 9) != null)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06001102 RID: 4354 RVA: 0x000658B4 File Offset: 0x00063AB4
	public override void Update()
	{
		if (!this.isInit)
		{
			this.isInit = true;
			this.updateSedNode();
		}
	}

	// Token: 0x06001103 RID: 4355 RVA: 0x000658CC File Offset: 0x00063ACC
	public void updateSedNode()
	{
		base.Update();
		Avatar player = Tools.instance.getPlayer();
		int num = int.Parse(base.gameObject.name);
		foreach (JSONObject jsonobject in jsonData.instance.DaDiTuYinCangJsonData.list)
		{
			if (jsonobject["id"].I == num)
			{
				bool flag = false;
				bool flag2 = false;
				if (jsonobject["StartTime"].str != "")
				{
					if (Tools.instance.IsInTime(player.worldTimeMag.nowTime, jsonobject["StartTime"].str, jsonobject["EndTime"].str))
					{
						flag = true;
					}
				}
				else
				{
					flag = true;
				}
				if (jsonobject["EventValue"].list.Count > 0)
				{
					int num2 = GlobalValue.Get(jsonobject["EventValue"][0].I, "MapComponent.updateSedNode 判断地图隐藏点");
					int i = jsonobject["EventValue"][1].I;
					string str = jsonobject["fuhao"].str;
					if (str == "=")
					{
						if (num2 == i)
						{
							flag2 = true;
						}
					}
					else if (str == ">")
					{
						if (num2 > i)
						{
							flag2 = true;
						}
					}
					else if (str == "<")
					{
						if (num2 < i)
						{
							flag2 = true;
						}
					}
					else
					{
						Debug.LogError("意外的地图隐藏点符号:" + str);
					}
				}
				else
				{
					flag2 = true;
				}
				if (flag && flag2)
				{
					if (jsonobject["Type"].I == 1)
					{
						base.gameObject.transform.localScale = new Vector3(0.5f, 0.5f, 1f);
						break;
					}
					base.gameObject.transform.localScale = Vector3.zero;
					break;
				}
				else
				{
					if (jsonobject["Type"].I == 1)
					{
						base.gameObject.transform.localScale = Vector3.zero;
						break;
					}
					base.gameObject.transform.localScale = new Vector3(0.5f, 0.5f, 1f);
					break;
				}
			}
		}
		bool flag3 = false;
		int num3 = player.nomelTaskMag.AutoAllMapPlaceHasNTask(new List<int>
		{
			this.NodeIndex
		});
		if (num3 != -1)
		{
			flag3 = true;
		}
		if (this.TaskSpine != null && player.AllMapRandomNode.HasField(this.NodeIndex.ToString()))
		{
			int num4 = (int)player.AllMapRandomNode[this.NodeIndex.ToString()]["Type"].n;
			if (flag3)
			{
				num4 = 7;
			}
			JSONObject jsonobject2 = jsonData.instance.AllMapReset[num4.ToString()];
			if (jsonobject2["Icon"].str == "")
			{
				this.TaskSpine.initialSkinName = "default";
				if (this.oldName != "default")
				{
					this.oldName = this.TaskSpine.initialSkinName;
					this.TaskSpine.Initialize(true);
				}
				return;
			}
			if (jsonData.instance.MapRandomJsonData.HasField(player.AllMapRandomNode[this.NodeIndex.ToString()]["EventId"].I.ToString()) && jsonData.instance.MapRandomJsonData[player.AllMapRandomNode[this.NodeIndex.ToString()]["EventId"].I.ToString()]["Icon"].str != "")
			{
				this.TaskSpine.initialSkinName = jsonData.instance.MapRandomJsonData[player.AllMapRandomNode[this.NodeIndex.ToString()]["EventId"].I.ToString()]["Icon"].str;
			}
			else
			{
				this.TaskSpine.initialSkinName = jsonobject2["Icon"].str;
				this.AnimationName = jsonobject2["Act"].str;
				if (num3 != -1 && player.nomelTaskMag.IsNTaskZiXiangInLuJin(num3, new List<int>
				{
					this.NodeIndex
				})["type"].I == 5)
				{
					this.TaskSpine.initialSkinName = "Icon4";
				}
			}
			if (this.oldName != this.TaskSpine.initialSkinName)
			{
				this.oldName = this.TaskSpine.initialSkinName;
				this.TaskSpine.Initialize(true);
				this.attackerSpineboy.AnimationState.SetAnimation(0, "appear", true);
				this.attackerSpineboy.AnimationState.AddAnimation(0, (this.AnimationName == "") ? "Act1" : this.AnimationName, true, 0.3f);
			}
		}
	}

	// Token: 0x06001104 RID: 4356 RVA: 0x00065E2C File Offset: 0x0006402C
	public void Complete(TrackEntry trackEntry)
	{
		this.attackerSpineboy.AnimationState.SetAnimation(0, this.AnimationName, true);
	}

	// Token: 0x06001105 RID: 4357 RVA: 0x00065E47 File Offset: 0x00064047
	public void CompleteTrigger(TrackEntry trackEntry)
	{
		this.TaskSpine.initialSkinName = "default";
		this.TaskSpine.Initialize(true);
	}

	// Token: 0x06001106 RID: 4358 RVA: 0x00065E68 File Offset: 0x00064068
	public override void showLuDian()
	{
		Avatar player = Tools.instance.getPlayer();
		float shenShiArea = player.GetShenShiArea();
		List<int> list = new List<int>();
		foreach (KeyValuePair<int, BaseMapCompont> keyValuePair in AllMapManage.instance.mapIndex)
		{
			MapComponent mapComponent = (MapComponent)keyValuePair.Value;
			if (AllMapLuDainType.DataDict.ContainsKey(mapComponent.NodeIndex) && AllMapLuDainType.DataDict[mapComponent.NodeIndex].MapType == 1 && mapComponent.NodeGroup != 0)
			{
				float num = Vector3.Distance(MapPlayerController.Inst.transform.position, mapComponent.transform.position);
				if (mapComponent.NodeGroup == ((MapComponent)AllMapManage.instance.mapIndex[player.NowMapIndex]).NodeGroup || num <= shenShiArea)
				{
					list.Add(mapComponent.NodeGroup);
					mapComponent.gameObject.SetActive(true);
					iTween.FadeTo(mapComponent.transform.Find("Level1Move").gameObject, iTween.Hash(new object[]
					{
						"alpha",
						1f,
						"time",
						0.6f,
						"EaseType",
						"linear",
						"includechildren",
						false
					}));
				}
				else if (mapComponent.gameObject.activeSelf)
				{
					base.StartCoroutine(this.setGameobjectActive(mapComponent.gameObject, false, 1f));
					iTween.FadeTo(mapComponent.transform.Find("Level1Move").gameObject, iTween.Hash(new object[]
					{
						"alpha",
						0f,
						"time",
						0.6f,
						"EaseType",
						"linear",
						"includechildren",
						false
					}));
				}
			}
		}
		foreach (AllMapsLuXian allMapsLuXian in AllMapManage.instance.LuXianGroup.GetComponentsInChildren<AllMapsLuXian>(true))
		{
			if (allMapsLuXian.NodeGroup == ((MapComponent)AllMapManage.instance.mapIndex[player.NowMapIndex]).NodeGroup || list.Contains(allMapsLuXian.NodeGroup))
			{
				allMapsLuXian.gameObject.SetActive(true);
				iTween.FadeTo(allMapsLuXian.gameObject, iTween.Hash(new object[]
				{
					"alpha",
					1f,
					"time",
					0.6f,
					"EaseType",
					"linear"
				}));
			}
			else if (allMapsLuXian.gameObject.activeSelf)
			{
				base.StartCoroutine(this.setGameobjectActive(allMapsLuXian.gameObject, false, 1f));
				iTween.FadeTo(allMapsLuXian.gameObject, iTween.Hash(new object[]
				{
					"alpha",
					0f,
					"time",
					0.6f,
					"EaseType",
					"linear"
				}));
			}
		}
	}

	// Token: 0x06001107 RID: 4359 RVA: 0x000661F0 File Offset: 0x000643F0
	public override void ResteAllMapNode()
	{
		Avatar avatar = (Avatar)KBEngineApp.app.player();
		if (!avatar.AllMapRandomNode.HasField(this.NodeIndex.ToString()))
		{
			avatar.AllMapSetNode();
		}
		int num = (int)avatar.AllMapRandomNode[this.NodeIndex.ToString()]["Type"].n;
		if ((num == 2 || num == 5) && avatar.AllMapRandomNode[this.NodeIndex.ToString()]["EventId"].I == 0)
		{
			return;
		}
		if ((int)jsonData.instance.AllMapLuDainType[this.NodeIndex.ToString()]["MapType"].n == 0)
		{
			avatar.AllMapRandomNode[this.NodeIndex.ToString()].SetField("Type", 2);
		}
		else
		{
			avatar.AllMapRandomNode[this.NodeIndex.ToString()].SetField("Type", 5);
		}
		avatar.AllMapRandomNode[this.NodeIndex.ToString()].SetField("EventId", 0);
		avatar.AllMapRandomNode[this.NodeIndex.ToString()].SetField("resetTime", avatar.worldTimeMag.nowTime);
	}

	// Token: 0x06001108 RID: 4360 RVA: 0x0006633E File Offset: 0x0006453E
	public IEnumerator setGameobjectActive(GameObject obj, bool act, float time)
	{
		yield return new WaitForSeconds(time);
		obj.SetActive(act);
		yield break;
	}

	// Token: 0x06001109 RID: 4361 RVA: 0x0006635B File Offset: 0x0006455B
	private void OnDestroy()
	{
		if (MapMoveTips.Inst != null)
		{
			MapMoveTips.Hide();
		}
	}

	// Token: 0x0600110A RID: 4362 RVA: 0x00004095 File Offset: 0x00002295
	public override void CloseLuDian()
	{
	}

	// Token: 0x04000C2F RID: 3119
	private float MoveBaseSpeed = 4f;

	// Token: 0x04000C30 RID: 3120
	private float MoveBaseSpeedMin = 1.5f;

	// Token: 0x04000C31 RID: 3121
	public SkeletonRenderer TaskSpine;

	// Token: 0x04000C32 RID: 3122
	public SkeletonAnimation attackerSpineboy;

	// Token: 0x04000C33 RID: 3123
	private string AnimationName = "";

	// Token: 0x04000C34 RID: 3124
	private string oldName = "";

	// Token: 0x04000C35 RID: 3125
	public int NodeGroup;

	// Token: 0x04000C36 RID: 3126
	private bool isInit;
}
