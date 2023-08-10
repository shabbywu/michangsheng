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

public class MapComponent : BaseMapCompont
{
	private float MoveBaseSpeed = 4f;

	private float MoveBaseSpeedMin = 1.5f;

	public SkeletonRenderer TaskSpine;

	public SkeletonAnimation attackerSpineboy;

	private string AnimationName = "";

	private string oldName = "";

	public int NodeGroup;

	private bool isInit;

	private new void Awake()
	{
		isInit = false;
		base.Awake();
	}

	public override void AvatarMoveToThis()
	{
		//IL_01f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f7: Expected O, but got Unknown
		//IL_022f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0236: Expected O, but got Unknown
		GameObject val = GameObject.Find("MapMoveNode");
		if ((Object)(object)val == (Object)null)
		{
			UIPopTip.Inst.Pop("找不到移动路径跟节点");
			return;
		}
		_ = PlayerEx.Player;
		MapPlayerController playerController = AllMapManage.instance.MapPlayerController;
		playerController.SetSpeed(1);
		AllMapManage.instance.isPlayMove = true;
		MapMoveNode[] componentsInChildren = val.GetComponentsInChildren<MapMoveNode>();
		List<GameObject> Nodes = new List<GameObject>();
		Nodes.Add(((Component)playerController).gameObject);
		List<GameObject> list = new List<GameObject>();
		if (playerController.ShowType != MapPlayerShowType.遁术)
		{
			MapMoveNode[] array = componentsInChildren;
			foreach (MapMoveNode mapMoveNode in array)
			{
				if ((mapMoveNode.StartNode == NodeIndex && ComAvatar.NowMapIndex == mapMoveNode.EndNode) || (mapMoveNode.EndNode == NodeIndex && ComAvatar.NowMapIndex == mapMoveNode.StartNode))
				{
					list.Add(((Component)mapMoveNode).gameObject);
				}
			}
			if (list.Count > 0 && ComAvatar.NowMapIndex == list[0].GetComponent<MapMoveNode>().EndNode)
			{
				list.Reverse();
			}
			foreach (GameObject item3 in list)
			{
				Nodes.Add(item3);
			}
		}
		Transform val2 = ((Component)this).transform.Find("PlayerPosition");
		Nodes.Add(((Object)(object)val2 != (Object)null) ? ((Component)val2).gameObject : ((Component)AllMapManage.instance.mapIndex[NodeIndex]).gameObject);
		_ = Nodes.Count;
		for (int j = 1; j < Nodes.Count; j++)
		{
			int nodeIndex = j;
			Queue<UnityAction> queue = new Queue<UnityAction>();
			UnityAction item = (UnityAction)delegate
			{
				//IL_002e: Unknown result type (might be due to invalid IL or missing references)
				//IL_0033: Unknown result type (might be due to invalid IL or missing references)
				//IL_0053: Unknown result type (might be due to invalid IL or missing references)
				//IL_0058: Unknown result type (might be due to invalid IL or missing references)
				//IL_012b: Unknown result type (might be due to invalid IL or missing references)
				//IL_0160: Unknown result type (might be due to invalid IL or missing references)
				//IL_018a: Unknown result type (might be due to invalid IL or missing references)
				playerController.SetSpeed(1);
				float num = Vector2.Distance(Vector2.op_Implicit(Nodes[nodeIndex - 1].transform.position), Vector2.op_Implicit(Nodes[nodeIndex].transform.position));
				float num2 = ((Tools.instance.getPlayer().dunSu > 100) ? (MoveBaseSpeedMin + MoveBaseSpeed) : (MoveBaseSpeedMin + MoveBaseSpeed * ((float)Tools.instance.getPlayer().dunSu / 100f)));
				if (playerController.ShowType == MapPlayerShowType.遁术)
				{
					num2 *= 2f;
				}
				float num3 = num / num2;
				iTween.MoveTo(((Component)playerController).gameObject, iTween.Hash(new object[12]
				{
					"x",
					Nodes[nodeIndex].transform.position.x,
					"y",
					Nodes[nodeIndex].transform.position.y,
					"z",
					((Component)playerController).transform.position.z,
					"time",
					num3,
					"islocal",
					false,
					"EaseType",
					"linear"
				}));
				WASDMove.waitTime = num3;
				WASDMove.needWait = true;
				((MonoBehaviour)this).Invoke("callContinue", num3);
			};
			queue.Enqueue(item);
			YSFuncList.Ints.AddFunc(queue);
		}
		Queue<UnityAction> queue2 = new Queue<UnityAction>();
		UnityAction item2 = (UnityAction)delegate
		{
			setAvatarNowMapIndex();
			showLuDian();
			playerController.SetSpeed(0);
			AllMapManage.instance.isPlayMove = false;
			YSFuncList.Ints.Continue();
			_ = Tools.instance.getPlayer().NowMapIndex;
			updateSedNode();
			if (MapGetWay.Inst.CurTalk > 0)
			{
				Resources.Load<GameObject>("talkPrefab/TalkPrefab/talk" + MapGetWay.Inst.CurTalk).Inst();
				MapGetWay.Inst.CurTalk = 0;
			}
		};
		queue2.Enqueue(item2);
		YSFuncList.Ints.AddFunc(queue2);
	}

	private void callContinue()
	{
		YSFuncList.Ints.Continue();
	}

	public override void BaseAddTime()
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Expected O, but got Unknown
		Queue<UnityAction> queue = new Queue<UnityAction>();
		UnityAction item = (UnityAction)delegate
		{
			JSONObject jSONObject = AllMapCastTimeJsonData.list.Find((JSONObject aa) => (int)aa["dunSu"].n >= ComAvatar.dunSu);
			if (jSONObject != null)
			{
				ComAvatar.AddTime((int)jSONObject["XiaoHao"].n);
			}
			YSFuncList.Ints.Continue();
		};
		queue.Enqueue(item);
		YSFuncList.Ints.AddFunc(queue);
	}

	public override void EventRandom()
	{
		if (Tools.instance.getPlayer().NowMapIndex != NodeIndex)
		{
			if (AllMapManage.instance.MapPlayerController.ShowType == MapPlayerShowType.遁术)
			{
				NewEventRandom();
			}
			else
			{
				((MonoBehaviour)this).StartCoroutine(Move());
			}
		}
	}

	public void NewEventRandom()
	{
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Expected O, but got Unknown
		if (!CanClick())
		{
			return;
		}
		fuBenSetClick();
		NewMovaAvatar();
		if (IsStatic)
		{
			return;
		}
		Queue<UnityAction> queue = new Queue<UnityAction>();
		UnityAction item = (UnityAction)delegate
		{
			Avatar avatar = (Avatar)KBEngineApp.app.player();
			int num = avatar.nomelTaskMag.AutoAllMapPlaceHasNTask(new List<int> { NodeIndex });
			if (num != -1)
			{
				JSONObject jSONObject = avatar.nomelTaskMag.IsNTaskZiXiangInLuJin(num, new List<int> { NodeIndex });
				JSONObject nowChildIDSuiJiJson = avatar.nomelTaskMag.GetNowChildIDSuiJiJson(num);
				if (jSONObject["type"].I == 5)
				{
					avatar.randomFuBenMag.GetInRandomFuBen(NodeIndex);
				}
				else
				{
					GlobalValue.Set(401, nowChildIDSuiJiJson["Value"].I, ((object)this).GetType().Name + ".EventRandom");
					GlobalValue.Set(402, num, ((object)this).GetType().Name + ".EventRandom");
					Object.Instantiate<GameObject>(Resources.Load<GameObject>("talkPrefab/TalkPrefab/talk" + jSONObject["talkID"].str));
				}
				YSFuncList.Ints.Continue();
			}
			else
			{
				int i = avatar.AllMapRandomNode[NodeIndex.ToString()]["EventId"].I;
				int num2 = (int)avatar.AllMapRandomNode[NodeIndex.ToString()]["Type"].n;
				if (num2 == 2 || num2 == 5 || avatar.AllMapRandomNode[NodeIndex.ToString()]["EventId"].I == 0)
				{
					if ((Object)(object)FungusManager.Instance.jieShaBlock == (Object)null)
					{
						GameObject val = Object.Instantiate<GameObject>(Resources.Load<GameObject>("talkPrefab/TalkPrefab/talk4010"));
						FungusManager.Instance.jieShaBlock = val.GetComponentInChildren<Flowchart>();
					}
					else if (GlobalValue.Get(171, ((object)this).GetType().Name + ".EventRandom") == 1)
					{
						GlobalValue.Set(171, 0, ((object)this).GetType().Name + ".EventRandom");
					}
					else
					{
						FungusManager.Instance.jieShaBlock.Reset(resetCommands: false, resetVariables: true);
						FungusManager.Instance.jieShaBlock.ExecuteBlock("Splash");
					}
					ResteAllMapNode();
					Tools.instance.getPlayer().AllMapSetNode();
					YSFuncList.Ints.Continue();
				}
				else
				{
					int i2 = MapRandomJsonData[string.Concat(i)]["EventData"].I;
					int i3 = MapRandomJsonData[string.Concat(i)]["MosterID"].I;
					if (MapRandomJsonData[string.Concat(i)]["once"].I != 0)
					{
						if (!avatar.SuiJiShiJian.HasField(Tools.getScreenName()))
						{
							avatar.SuiJiShiJian.AddField(Tools.getScreenName(), new JSONObject(JSONObject.Type.ARRAY));
						}
						avatar.SuiJiShiJian[Tools.getScreenName()].Add(i);
					}
					switch ((int)MapRandomJsonData[string.Concat(i)]["EventList"].n)
					{
					case 0:
						Object.Instantiate<GameObject>(Resources.Load<GameObject>("talkPrefab/TalkPrefab/talk" + i2));
						break;
					case 1:
						addOption(i2);
						break;
					case 2:
						Tools.instance.MonstarID = i3;
						Object.Instantiate<GameObject>(Resources.Load<GameObject>("talkPrefab/FightPrefab/Fight" + i2));
						break;
					case 3:
						OpenDadituCaiJi();
						break;
					}
					ResteAllMapNode();
					Tools.instance.getPlayer().AllMapSetNode();
					YSFuncList.Ints.Continue();
				}
			}
		};
		queue.Enqueue(item);
		YSFuncList.Ints.AddFunc(queue);
	}

	public void NewMovaAvatar()
	{
		base.movaAvatar();
	}

	public override void movaAvatar()
	{
		if (Tools.instance.getPlayer().NowMapIndex != NodeIndex)
		{
			if (AllMapManage.instance.MapPlayerController.ShowType == MapPlayerShowType.遁术)
			{
				base.movaAvatar();
			}
			else
			{
				((MonoBehaviour)this).StartCoroutine(Move());
			}
		}
	}

	private IEnumerator Move()
	{
		List<int> list = new List<int>();
		if (MapGetWay.Inst.IsNearly(Tools.instance.getPlayer().NowMapIndex, NodeIndex))
		{
			list.Add(NodeIndex);
		}
		else
		{
			list = MapGetWay.Inst.GetBestList(Tools.instance.getPlayer().NowMapIndex, NodeIndex);
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
			for (; index < list.Count; index++)
			{
				int num = list[index];
				if ((Object)(object)AllMapManage.instance != (Object)null && AllMapManage.instance.mapIndex.ContainsKey(num))
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
					yield return (object)new WaitForSeconds(0.2f);
				}
				yield return (object)new WaitForSeconds(0.3f);
				if (!Tools.instance.canClick() || MapGetWay.Inst.IsStop)
				{
					break;
				}
			}
		}
		MapGetWay.Inst.StopAuToMove();
	}

	public override bool YuJianFeiXing()
	{
		if (ComAvatar.NowMapIndex == NodeIndex)
		{
			return false;
		}
		foreach (SkillItem equipStaticSkill in ((Avatar)KBEngineApp.app.player()).equipStaticSkillList)
		{
			if (jsonData.instance.StaticSkillJsonData[string.Concat(Tools.instance.getStaticSkillKeyByID(equipStaticSkill.itemId))]["seid"].list.Find((JSONObject aa) => (int)aa.n == 9) != null)
			{
				return true;
			}
		}
		return false;
	}

	public override void Update()
	{
		if (!isInit)
		{
			isInit = true;
			updateSedNode();
		}
	}

	public void updateSedNode()
	{
		//IL_022d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0207: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c2: Unknown result type (might be due to invalid IL or missing references)
		base.Update();
		Avatar player = Tools.instance.getPlayer();
		int num = int.Parse(((Object)((Component)this).gameObject).name);
		foreach (JSONObject item in jsonData.instance.DaDiTuYinCangJsonData.list)
		{
			if (item["id"].I != num)
			{
				continue;
			}
			bool flag = false;
			bool flag2 = false;
			if (item["StartTime"].str != "")
			{
				if (Tools.instance.IsInTime(player.worldTimeMag.nowTime, item["StartTime"].str, item["EndTime"].str))
				{
					flag = true;
				}
			}
			else
			{
				flag = true;
			}
			if (item["EventValue"].list.Count > 0)
			{
				int num2 = GlobalValue.Get(item["EventValue"][0].I, "MapComponent.updateSedNode 判断地图隐藏点");
				int i = item["EventValue"][1].I;
				string str = item["fuhao"].str;
				switch (str)
				{
				case "=":
					if (num2 == i)
					{
						flag2 = true;
					}
					break;
				case ">":
					if (num2 > i)
					{
						flag2 = true;
					}
					break;
				case "<":
					if (num2 < i)
					{
						flag2 = true;
					}
					break;
				default:
					Debug.LogError((object)("意外的地图隐藏点符号:" + str));
					break;
				}
			}
			else
			{
				flag2 = true;
			}
			if (flag && flag2)
			{
				if (item["Type"].I == 1)
				{
					((Component)this).gameObject.transform.localScale = new Vector3(0.5f, 0.5f, 1f);
				}
				else
				{
					((Component)this).gameObject.transform.localScale = Vector3.zero;
				}
			}
			else if (item["Type"].I == 1)
			{
				((Component)this).gameObject.transform.localScale = Vector3.zero;
			}
			else
			{
				((Component)this).gameObject.transform.localScale = new Vector3(0.5f, 0.5f, 1f);
			}
			break;
		}
		bool flag3 = false;
		int num3 = player.nomelTaskMag.AutoAllMapPlaceHasNTask(new List<int> { NodeIndex });
		if (num3 != -1)
		{
			flag3 = true;
		}
		if (!((Object)(object)TaskSpine != (Object)null) || !player.AllMapRandomNode.HasField(NodeIndex.ToString()))
		{
			return;
		}
		int num4 = (int)player.AllMapRandomNode[NodeIndex.ToString()]["Type"].n;
		if (flag3)
		{
			num4 = 7;
		}
		JSONObject jSONObject = jsonData.instance.AllMapReset[num4.ToString()];
		if (jSONObject["Icon"].str == "")
		{
			TaskSpine.initialSkinName = "default";
			if (oldName != "default")
			{
				oldName = TaskSpine.initialSkinName;
				TaskSpine.Initialize(true);
			}
			return;
		}
		if (jsonData.instance.MapRandomJsonData.HasField(player.AllMapRandomNode[NodeIndex.ToString()]["EventId"].I.ToString()) && jsonData.instance.MapRandomJsonData[player.AllMapRandomNode[NodeIndex.ToString()]["EventId"].I.ToString()]["Icon"].str != "")
		{
			TaskSpine.initialSkinName = jsonData.instance.MapRandomJsonData[player.AllMapRandomNode[NodeIndex.ToString()]["EventId"].I.ToString()]["Icon"].str;
		}
		else
		{
			TaskSpine.initialSkinName = jSONObject["Icon"].str;
			AnimationName = jSONObject["Act"].str;
			if (num3 != -1 && player.nomelTaskMag.IsNTaskZiXiangInLuJin(num3, new List<int> { NodeIndex })["type"].I == 5)
			{
				TaskSpine.initialSkinName = "Icon4";
			}
		}
		if (oldName != TaskSpine.initialSkinName)
		{
			oldName = TaskSpine.initialSkinName;
			TaskSpine.Initialize(true);
			attackerSpineboy.AnimationState.SetAnimation(0, "appear", true);
			attackerSpineboy.AnimationState.AddAnimation(0, (AnimationName == "") ? "Act1" : AnimationName, true, 0.3f);
		}
	}

	public void Complete(TrackEntry trackEntry)
	{
		attackerSpineboy.AnimationState.SetAnimation(0, AnimationName, true);
	}

	public void CompleteTrigger(TrackEntry trackEntry)
	{
		TaskSpine.initialSkinName = "default";
		TaskSpine.Initialize(true);
	}

	public override void showLuDian()
	{
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		Avatar player = Tools.instance.getPlayer();
		float shenShiArea = player.GetShenShiArea();
		List<int> list = new List<int>();
		foreach (KeyValuePair<int, BaseMapCompont> item in AllMapManage.instance.mapIndex)
		{
			MapComponent mapComponent = (MapComponent)item.Value;
			if (AllMapLuDainType.DataDict.ContainsKey(mapComponent.NodeIndex) && AllMapLuDainType.DataDict[mapComponent.NodeIndex].MapType == 1 && mapComponent.NodeGroup != 0)
			{
				float num = Vector3.Distance(((Component)MapPlayerController.Inst).transform.position, ((Component)mapComponent).transform.position);
				if (mapComponent.NodeGroup == ((MapComponent)AllMapManage.instance.mapIndex[player.NowMapIndex]).NodeGroup || num <= shenShiArea)
				{
					list.Add(mapComponent.NodeGroup);
					((Component)mapComponent).gameObject.SetActive(true);
					iTween.FadeTo(((Component)((Component)mapComponent).transform.Find("Level1Move")).gameObject, iTween.Hash(new object[8] { "alpha", 1f, "time", 0.6f, "EaseType", "linear", "includechildren", false }));
				}
				else if (((Component)mapComponent).gameObject.activeSelf)
				{
					((MonoBehaviour)this).StartCoroutine(setGameobjectActive(((Component)mapComponent).gameObject, act: false, 1f));
					iTween.FadeTo(((Component)((Component)mapComponent).transform.Find("Level1Move")).gameObject, iTween.Hash(new object[8] { "alpha", 0f, "time", 0.6f, "EaseType", "linear", "includechildren", false }));
				}
			}
		}
		AllMapsLuXian[] componentsInChildren = AllMapManage.instance.LuXianGroup.GetComponentsInChildren<AllMapsLuXian>(true);
		foreach (AllMapsLuXian allMapsLuXian in componentsInChildren)
		{
			if (allMapsLuXian.NodeGroup == ((MapComponent)AllMapManage.instance.mapIndex[player.NowMapIndex]).NodeGroup || list.Contains(allMapsLuXian.NodeGroup))
			{
				((Component)allMapsLuXian).gameObject.SetActive(true);
				iTween.FadeTo(((Component)allMapsLuXian).gameObject, iTween.Hash(new object[6] { "alpha", 1f, "time", 0.6f, "EaseType", "linear" }));
			}
			else if (((Component)allMapsLuXian).gameObject.activeSelf)
			{
				((MonoBehaviour)this).StartCoroutine(setGameobjectActive(((Component)allMapsLuXian).gameObject, act: false, 1f));
				iTween.FadeTo(((Component)allMapsLuXian).gameObject, iTween.Hash(new object[6] { "alpha", 0f, "time", 0.6f, "EaseType", "linear" }));
			}
		}
	}

	public override void ResteAllMapNode()
	{
		Avatar avatar = (Avatar)KBEngineApp.app.player();
		if (!avatar.AllMapRandomNode.HasField(NodeIndex.ToString()))
		{
			avatar.AllMapSetNode();
		}
		int num = (int)avatar.AllMapRandomNode[NodeIndex.ToString()]["Type"].n;
		if ((num != 2 && num != 5) || avatar.AllMapRandomNode[NodeIndex.ToString()]["EventId"].I != 0)
		{
			if ((int)jsonData.instance.AllMapLuDainType[NodeIndex.ToString()]["MapType"].n == 0)
			{
				avatar.AllMapRandomNode[NodeIndex.ToString()].SetField("Type", 2);
			}
			else
			{
				avatar.AllMapRandomNode[NodeIndex.ToString()].SetField("Type", 5);
			}
			avatar.AllMapRandomNode[NodeIndex.ToString()].SetField("EventId", 0);
			avatar.AllMapRandomNode[NodeIndex.ToString()].SetField("resetTime", avatar.worldTimeMag.nowTime);
		}
	}

	public IEnumerator setGameobjectActive(GameObject obj, bool act, float time)
	{
		yield return (object)new WaitForSeconds(time);
		obj.SetActive(act);
	}

	private void OnDestroy()
	{
		if ((Object)(object)MapMoveTips.Inst != (Object)null)
		{
			MapMoveTips.Hide();
		}
	}

	public override void CloseLuDian()
	{
	}
}
