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

// Token: 0x02000275 RID: 629
public class MapComponent : BaseMapCompont
{
	// Token: 0x06001362 RID: 4962 RVA: 0x00012317 File Offset: 0x00010517
	private new void Awake()
	{
		this.isInit = false;
		base.Awake();
	}

	// Token: 0x06001363 RID: 4963 RVA: 0x000B3C14 File Offset: 0x000B1E14
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

	// Token: 0x06001364 RID: 4964 RVA: 0x000112BB File Offset: 0x0000F4BB
	private void callContinue()
	{
		YSFuncList.Ints.Continue();
	}

	// Token: 0x06001365 RID: 4965 RVA: 0x000B3E7C File Offset: 0x000B207C
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

	// Token: 0x06001366 RID: 4966 RVA: 0x00012326 File Offset: 0x00010526
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

	// Token: 0x06001367 RID: 4967 RVA: 0x000B3EB0 File Offset: 0x000B20B0
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
			int num2 = (int)avatar.AllMapRandomNode[this.NodeIndex.ToString()]["EventId"].n;
			int num3 = (int)avatar.AllMapRandomNode[this.NodeIndex.ToString()]["Type"].n;
			if (num3 == 2 || num3 == 5 || (int)avatar.AllMapRandomNode[this.NodeIndex.ToString()]["EventId"].n == 0)
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
			int num4 = (int)this.MapRandomJsonData[string.Concat(num2)]["EventData"].n;
			int monstarID = (int)this.MapRandomJsonData[string.Concat(num2)]["MosterID"].n;
			if ((int)this.MapRandomJsonData[string.Concat(num2)]["once"].n != 0)
			{
				if (!avatar.SuiJiShiJian.HasField(Tools.getScreenName()))
				{
					avatar.SuiJiShiJian.AddField(Tools.getScreenName(), new JSONObject(JSONObject.Type.ARRAY));
				}
				avatar.SuiJiShiJian[Tools.getScreenName()].Add(num2);
			}
			switch ((int)this.MapRandomJsonData[string.Concat(num2)]["EventList"].n)
			{
			case 0:
				Object.Instantiate<GameObject>(Resources.Load<GameObject>("talkPrefab/TalkPrefab/talk" + num4));
				break;
			case 1:
				this.addOption(num4);
				break;
			case 2:
				Tools.instance.MonstarID = monstarID;
				Object.Instantiate<GameObject>(Resources.Load<GameObject>("talkPrefab/FightPrefab/Fight" + num4));
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

	// Token: 0x06001368 RID: 4968 RVA: 0x00012366 File Offset: 0x00010566
	public void NewMovaAvatar()
	{
		base.movaAvatar();
	}

	// Token: 0x06001369 RID: 4969 RVA: 0x0001236E File Offset: 0x0001056E
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

	// Token: 0x0600136A RID: 4970 RVA: 0x000123AE File Offset: 0x000105AE
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

	// Token: 0x0600136B RID: 4971 RVA: 0x000B3F00 File Offset: 0x000B2100
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

	// Token: 0x0600136C RID: 4972 RVA: 0x000123BD File Offset: 0x000105BD
	public override void Update()
	{
		if (!this.isInit)
		{
			this.isInit = true;
			this.updateSedNode();
		}
	}

	// Token: 0x0600136D RID: 4973 RVA: 0x000B3FD4 File Offset: 0x000B21D4
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

	// Token: 0x0600136E RID: 4974 RVA: 0x000123D4 File Offset: 0x000105D4
	public void Complete(TrackEntry trackEntry)
	{
		this.attackerSpineboy.AnimationState.SetAnimation(0, this.AnimationName, true);
	}

	// Token: 0x0600136F RID: 4975 RVA: 0x000123EF File Offset: 0x000105EF
	public void CompleteTrigger(TrackEntry trackEntry)
	{
		this.TaskSpine.initialSkinName = "default";
		this.TaskSpine.Initialize(true);
	}

	// Token: 0x06001370 RID: 4976 RVA: 0x000B4534 File Offset: 0x000B2734
	public override void showLuDian()
	{
		Avatar player = Tools.instance.getPlayer();
		foreach (KeyValuePair<int, BaseMapCompont> keyValuePair in AllMapManage.instance.mapIndex)
		{
			MapComponent mapComponent = (MapComponent)keyValuePair.Value;
			if (AllMapLuDainType.DataDict.ContainsKey(mapComponent.NodeIndex) && AllMapLuDainType.DataDict[mapComponent.NodeIndex].MapType == 1 && mapComponent.NodeGroup != 0)
			{
				if (mapComponent.NodeGroup == ((MapComponent)AllMapManage.instance.mapIndex[player.NowMapIndex]).NodeGroup)
				{
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
			if (allMapsLuXian.NodeGroup == ((MapComponent)AllMapManage.instance.mapIndex[player.NowMapIndex]).NodeGroup)
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

	// Token: 0x06001371 RID: 4977 RVA: 0x000B4860 File Offset: 0x000B2A60
	public override void ResteAllMapNode()
	{
		Avatar avatar = (Avatar)KBEngineApp.app.player();
		if (!avatar.AllMapRandomNode.HasField(this.NodeIndex.ToString()))
		{
			avatar.AllMapSetNode();
		}
		int num = (int)avatar.AllMapRandomNode[this.NodeIndex.ToString()]["Type"].n;
		if ((num == 2 || num == 5) && (int)avatar.AllMapRandomNode[this.NodeIndex.ToString()]["EventId"].n == 0)
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

	// Token: 0x06001372 RID: 4978 RVA: 0x0001240D File Offset: 0x0001060D
	public IEnumerator setGameobjectActive(GameObject obj, bool act, float time)
	{
		yield return new WaitForSeconds(time);
		obj.SetActive(act);
		yield break;
	}

	// Token: 0x06001373 RID: 4979 RVA: 0x0001242A File Offset: 0x0001062A
	private void OnDestroy()
	{
		if (MapMoveTips.Inst != null)
		{
			MapMoveTips.Hide();
		}
	}

	// Token: 0x06001374 RID: 4980 RVA: 0x000042DD File Offset: 0x000024DD
	public override void CloseLuDian()
	{
	}

	// Token: 0x04000F16 RID: 3862
	private float MoveBaseSpeed = 4f;

	// Token: 0x04000F17 RID: 3863
	private float MoveBaseSpeedMin = 1.5f;

	// Token: 0x04000F18 RID: 3864
	public SkeletonRenderer TaskSpine;

	// Token: 0x04000F19 RID: 3865
	public SkeletonAnimation attackerSpineboy;

	// Token: 0x04000F1A RID: 3866
	private string AnimationName = "";

	// Token: 0x04000F1B RID: 3867
	private string oldName = "";

	// Token: 0x04000F1C RID: 3868
	public int NodeGroup;

	// Token: 0x04000F1D RID: 3869
	private bool isInit;
}
