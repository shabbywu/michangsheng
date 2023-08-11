using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using BehaviorDesigner.Runtime;
using KBEngine;
using Newtonsoft.Json.Linq;
using Spine.Unity;
using Spine.Unity.Examples;
using UnityEngine;
using UnityEngine.Events;
using YSGame;

public class EndlessSeaMag : MonoBehaviour
{
	[Serializable]
	[CompilerGenerated]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9 = new _003C_003Ec();

		public static UnityAction _003C_003E9__46_0;

		internal void _003CStartMove_003Eb__46_0()
		{
			AllMapManage.instance.MapPlayerController.SetSpeed(1);
			YSFuncList.Ints.Continue();
		}
	}

	public static EndlessSeaMag Inst;

	public static int MapWide = 133;

	public static bool StopMove = false;

	[HideInInspector]
	public List<int> LuXian = new List<int>();

	private List<List<int>> LuXianDian = new List<List<int>>();

	public GameObject mapNodeUI;

	public GameObject LuXianUI;

	public GameObject LineUIBase;

	public GameObject MonstarObject;

	[HideInInspector]
	public List<SeaAvatarObjBase> MonstarList = new List<SeaAvatarObjBase>();

	public SeaGrid seaGrid;

	[HideInInspector]
	public List<SeaAvatarObjBase> RoundEventList = new List<SeaAvatarObjBase>();

	public bool flagMonstarTarget = true;

	public bool NeedRefresh;

	public Dictionary<int, ExternalBehaviorTree> externalBehaviorTrees;

	[HideInInspector]
	public GameObject LangHua;

	[HideInInspector]
	public EndlessFengBao fengBao;

	private GameObject FengBaoObjList;

	public Dictionary<int, int> oldFengBaoindex = new Dictionary<int, int>();

	private Dictionary<string, SkeletonDataAsset> skeletonmonstar = new Dictionary<string, SkeletonDataAsset>();

	private SeaTargetUI targetUI;

	public SeaAvatarObjBase.Directon PlayerDirecton = SeaAvatarObjBase.Directon.Left;

	private void Awake()
	{
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Expected O, but got Unknown
		Inst = this;
		externalBehaviorTrees = new Dictionary<int, ExternalBehaviorTree>();
		foreach (KeyValuePair<string, JToken> endlessSeaNPCDatum in jsonData.instance.EndlessSeaNPCData)
		{
			if (!externalBehaviorTrees.ContainsKey((int)endlessSeaNPCDatum.Value[(object)"AIType"]))
			{
				externalBehaviorTrees[(int)endlessSeaNPCDatum.Value[(object)"AIType"]] = Resources.Load("MapPrefab/SeaAI/SAI" + (int)endlessSeaNPCDatum.Value[(object)"AIType"]) as ExternalBehaviorTree;
			}
		}
		Object obj = Resources.Load("MapPrefab/SeaAI/fengbao/fengbaoBase");
		GameObject val = (GameObject)(object)((obj is GameObject) ? obj : null);
		if ((Object)(object)val != (Object)null)
		{
			fengBao = ((Component)val.transform).GetComponent<EndlessFengBao>();
		}
		if ((Object)(object)FengBaoObjList == (Object)null)
		{
			FengBaoObjList = new GameObject("FengBaoList");
		}
		if ((Object)(object)targetUI == (Object)null)
		{
			Object obj2 = Resources.Load("MapPrefab/SeaAI/SeaTargetUI");
			GameObject val2 = (GameObject)(object)((obj2 is GameObject) ? obj2 : null);
			targetUI = Object.Instantiate<GameObject>(val2).GetComponent<SeaTargetUI>();
			((Component)targetUI).GetComponent<Canvas>().worldCamera = Camera.main;
			((Component)targetUI).GetComponent<Canvas>().planeDistance = 6.4f;
		}
		if ((Object)(object)LangHua == (Object)null)
		{
			ref GameObject langHua = ref LangHua;
			Object obj3 = Resources.Load("MapPrefab/SeaAI/fengbao/LangHua");
			langHua = (GameObject)(object)((obj3 is GameObject) ? obj3 : null);
		}
		Dictionary<string, SkeletonDataAsset> dictionary = skeletonmonstar;
		Object obj4 = Resources.Load("MapPrefab/SeaAI/Monstar/haishou_SkeletonData");
		dictionary["hai_guai_SkeletonData"] = (SkeletonDataAsset)(object)((obj4 is SkeletonDataAsset) ? obj4 : null);
		Dictionary<string, SkeletonDataAsset> dictionary2 = skeletonmonstar;
		Object obj5 = Resources.Load("MapPrefab/SeaAI/shijian/haishangqiyu_SkeletonData");
		dictionary2["RandomEvent_SkeletonData"] = (SkeletonDataAsset)(object)((obj5 is SkeletonDataAsset) ? obj5 : null);
	}

	private void Start()
	{
		((MonoBehaviour)this).Invoke("autoCreatAllMonstar", 0.01f);
	}

	public int GetHaiYuLvIndex(int _seaid)
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		Avatar player = Tools.instance.getPlayer();
		int num = (int)((JArray)player.EndlessSea["SafeLv"])[_seaid - 1];
		JToken val = jsonData.instance.EndlessSeaLuanLiuRandom[num.ToString()];
		return (int)player.EndlessSea["LuanLiuId"][(object)(_seaid - 1)] % ((JContainer)(JArray)val).Count;
	}

	public void autoCreatFengBao()
	{
		Avatar player = Tools.instance.getPlayer();
		foreach (int haiYuID in seaGrid.HaiYuIDList)
		{
			oldFengBaoindex[haiYuID] = GetHaiYuLvIndex(haiYuID);
			foreach (JToken item in (IEnumerable<JToken>)player.seaNodeMag.GetFengBaoIndexList(haiYuID))
			{
				CreateFengBao(item, haiYuID);
			}
		}
	}

	public void CreateFengBao(JToken temp, int seaid)
	{
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		int realIndex = GetRealIndex(seaid, (int)temp[(object)"index"]);
		EndlessFengBao endlessFengBao = Object.Instantiate<EndlessFengBao>(fengBao);
		((Component)endlessFengBao).transform.parent = FengBaoObjList.transform;
		((Component)endlessFengBao).transform.position = ((Component)AllMapManage.instance.mapIndex[realIndex]).transform.position;
		endlessFengBao.id = (int)temp[(object)"id"];
		endlessFengBao.index = (int)temp[(object)"index"];
		endlessFengBao.lv = (int)temp[(object)"lv"];
		endlessFengBao.seaid = seaid;
		endlessFengBao.Show();
	}

	public void autoResetFengBao()
	{
		Avatar player = Tools.instance.getPlayer();
		foreach (int haiYuID in seaGrid.HaiYuIDList)
		{
			if (oldFengBaoindex[haiYuID] == GetHaiYuLvIndex(haiYuID))
			{
				continue;
			}
			foreach (JToken item in (IEnumerable<JToken>)player.seaNodeMag.GetFengBaoIndexList(haiYuID))
			{
				ResetFengBao(item, haiYuID);
			}
		}
	}

	public void ResetFengBao(JToken temp, int seaid)
	{
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		int realIndex = GetRealIndex(seaid, (int)temp[(object)"index"]);
		foreach (Transform item in FengBaoObjList.transform)
		{
			EndlessFengBao component = ((Component)item).GetComponent<EndlessFengBao>();
			if (component.id == (int)temp[(object)"id"] && component.lv == (int)temp[(object)"lv"] && component.seaid == seaid && component.index != (int)temp[(object)"index"])
			{
				component.Move(((Component)AllMapManage.instance.mapIndex[realIndex]).transform.position);
				break;
			}
		}
	}

	public static bool IsOutMap(int x, int y)
	{
		if (x < 0 || x > MapWide || y < 0 || y > 70)
		{
			return true;
		}
		return false;
	}

	public static List<int> GetAroundIndexList(int avatarindex, int round, bool shizi = false)
	{
		List<int> list = new List<int>();
		int indexX = FuBenMap.getIndexX(avatarindex, MapWide);
		int indexY = FuBenMap.getIndexY(avatarindex, MapWide);
		for (int i = -round; i <= round; i++)
		{
			for (int j = -round; j <= round; j++)
			{
				if ((!shizi || i == 0 || j == 0) && !IsOutMap(indexX + i, indexY + j))
				{
					int index = FuBenMap.getIndex(indexX + i, indexY + j, MapWide);
					list.Add(index);
				}
			}
		}
		return list;
	}

	public List<SeaAvatarObjBase> GetAroundEventList(int round, int eventType)
	{
		int nowIndex = Tools.instance.getPlayer().fubenContorl[Tools.getScreenName()].NowIndex;
		int indexX = FuBenMap.getIndexX(nowIndex, MapWide);
		int indexY = FuBenMap.getIndexY(nowIndex, MapWide);
		List<SeaAvatarObjBase> list = new List<SeaAvatarObjBase>();
		for (int i = -round; i <= round; i++)
		{
			for (int j = -round; j <= round; j++)
			{
				if (IsOutMap(indexX + i, indexY + j))
				{
					continue;
				}
				int index = FuBenMap.getIndex(indexX + i, indexY + j, MapWide);
				foreach (SeaAvatarObjBase monstar in MonstarList)
				{
					if (monstar.NowMapIndex == index && (int)monstar.Json["EventType"] == eventType)
					{
						list.Add(monstar);
					}
				}
			}
		}
		RoundEventList = list;
		return list;
	}

	public void autoCreatAllMonstar()
	{
		Avatar player = Tools.instance.getPlayer();
		foreach (int haiYuID in seaGrid.HaiYuIDList)
		{
			foreach (JToken item in (IEnumerable<JToken>)player.EndlessSeaRandomNode[haiYuID.ToString()][(object)"Monstar"])
			{
				int iD = (int)item[(object)"monstarId"];
				int num = 0;
				num = (((int)item[(object)"index"] > 49) ? ((int)item[(object)"index"]) : GetRealIndex(haiYuID, (int)item[(object)"index"]));
				string uuid = (string)item[(object)"uuid"];
				CreateMonstar(iD, num, uuid, haiYuID);
			}
		}
		GameObject val = GameObject.Find("ThreeSceneNpcCanvas");
		if ((Object)(object)val != (Object)null)
		{
			val.gameObject.SetActive(false);
		}
		try
		{
			NTaskCreateMonstar();
		}
		catch (Exception ex)
		{
			Debug.LogError((object)ex);
		}
		autoCreatFengBao();
		SetCanSeeMonstar();
	}

	public void NTaskCreateMonstar()
	{
		Avatar player = Tools.instance.getPlayer();
		foreach (JSONObject item in player.nomelTaskMag.GetNowNTask())
		{
			int i = item["id"].I;
			List<JSONObject> nTaskXiangXiList = player.nomelTaskMag.GetNTaskXiangXiList(i);
			int num = 0;
			foreach (JSONObject item2 in nTaskXiangXiList)
			{
				if (item2["type"].I == 7)
				{
					int chilidID = player.nomelTaskMag.getChilidID(i, num);
					player.nomelTaskMag.getWhereChilidID(i, num);
					JSONObject jSONObject = jsonData.instance.NTaskSuiJI[chilidID.ToString()];
					JSONObject jSONObject2 = jsonData.instance.NTaskSuiJI[chilidID.ToString()];
					if (jSONObject2["StrValue"].str == Tools.getScreenName())
					{
						int inSeaID = player.seaNodeMag.GetInSeaID(jSONObject2["Value"].I, MapWide);
						CreateMonstar(jSONObject["Value"].I, jSONObject2["Value"].I, Tools.getUUID(), inSeaID, isNTaskMonstar: true);
					}
				}
				num++;
			}
		}
	}

	public static int GetRealIndex(int seaID, int index)
	{
		int indexX = FuBenMap.getIndexX(seaID, MapWide / 7);
		int indexY = FuBenMap.getIndexY(seaID, MapWide / 7);
		int indexX2 = FuBenMap.getIndexX(index, 7);
		int indexY2 = FuBenMap.getIndexY(index, 7);
		int x = indexX * 7 + indexX2;
		int y = indexY * 7 + indexY2;
		return FuBenMap.getIndex(x, y, MapWide);
	}

	public void AddLuXianDian(int index)
	{
		LuXian.Add(index);
		ResetLuXianDian();
	}

	public void SetCanSeeMonstar()
	{
		Avatar player = Tools.instance.getPlayer();
		List<int> inSeeIndex = GetInSeeIndex(player.shengShi, getAvatarNowMapIndex());
		foreach (SeaAvatarObjBase monstar in MonstarList)
		{
			if (inSeeIndex.Contains(monstar.NowMapIndex) || monstar.ISNTaskMonstar)
			{
				monstar.ShowMonstarObj();
			}
			else
			{
				monstar.HideMonstarObj();
			}
		}
		foreach (int item in inSeeIndex)
		{
			if (AllMapManage.instance.mapIndex.ContainsKey(item))
			{
				MapSeaCompent obj = (MapSeaCompent)AllMapManage.instance.mapIndex[item];
				if (obj.NodeHasIsLand())
				{
					AddSeeIsland(item);
				}
				if (obj.WhetherHasJiZhi)
				{
					AddSeeIsland(item);
				}
			}
		}
	}

	public static void AddSeeIsland(int sea)
	{
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Expected O, but got Unknown
		JArray val = (JArray)Tools.instance.getPlayer().EndlessSeaAvatarSeeIsland["Island"];
		if (!Tools.ContensInt(val, sea))
		{
			val.Add(JToken.op_Implicit(sea));
		}
	}

	public List<int> GetInSeeIndex(int shenshi, int AvatarIndex)
	{
		JToken obj = Tools.FindJTokens((JToken)(object)jsonData.instance.EndlessSeaShiYe, (JToken aa) => ((int)aa[(object)"shenshi"] >= shenshi) ? true : false);
		int indexX = FuBenMap.getIndexX(AvatarIndex, MapWide);
		int indexY = FuBenMap.getIndexY(AvatarIndex, MapWide);
		List<int> list = new List<int>();
		List<int> list2 = new List<int>();
		List<int> list3 = new List<int>();
		int num = 0;
		foreach (JToken item in (IEnumerable<JToken>)obj[(object)"xinzhuang"])
		{
			if (num % 2 == 0)
			{
				list.Add((int)item);
			}
			else
			{
				list2.Add((int)item);
			}
			num++;
		}
		int num2 = 0;
		foreach (int item2 in list2)
		{
			_ = item2;
			int x = indexX + list[num2];
			int y = indexY + list2[num2];
			if (!IsOutMap(x, y))
			{
				int index = FuBenMap.getIndex(x, y, MapWide);
				list3.Add(index);
				num2++;
			}
		}
		return list3;
	}

	public bool IsInSeeType(int shenshi, int AvatarIndex, int TargetIndex)
	{
		if (GetInSeeIndex(shenshi, AvatarIndex).Contains(TargetIndex))
		{
			return true;
		}
		return false;
	}

	public void MoveToSeaPositon(int start, int end)
	{
	}

	public int getAvatarNowMapIndex()
	{
		return Tools.instance.getPlayer().fubenContorl[Tools.getScreenName()].NowIndex;
	}

	public void ResetLuXianDian()
	{
		int num = 1;
		int avatarNowMapIndex = getAvatarNowMapIndex();
		LuXianDian.Clear();
		foreach (int item in LuXian)
		{
			List<int> roadXian = GetRoadXian((num > 1) ? LuXian[num - 2] : avatarNowMapIndex, item);
			LuXianDian.Add(roadXian);
			num++;
		}
		RestLuXianDianSprite();
	}

	public void RemoveAllLuXian()
	{
		LuXian.Clear();
		LuXianDian.Clear();
		autoRestLuXianUIID();
		RestLuXianDianSprite();
	}

	public void RestLuXianDianSprite()
	{
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		foreach (Transform item in LineUIBase.transform)
		{
			((Component)item).transform.position = new Vector3(0f, 100000f, 0f);
		}
		MapSeaCompent mapSeaCompent = (MapSeaCompent)AllMapManage.instance.mapIndex[getAvatarNowMapIndex()];
		int num = 0;
		foreach (List<int> item2 in LuXianDian)
		{
			foreach (int item3 in item2)
			{
				MapSeaCompent mapSeaCompent2 = (MapSeaCompent)AllMapManage.instance.mapIndex[item3];
				if ((Object)(object)mapSeaCompent != (Object)null)
				{
					Transform val = null;
					if (LineUIBase.transform.childCount > num)
					{
						val = LineUIBase.transform.GetChild(num);
					}
					else
					{
						val = Object.Instantiate<GameObject>(LuXianUI).transform;
						val.parent = LineUIBase.transform;
					}
					DrawLine(((Component)val).GetComponent<SpriteRenderer>(), ((Component)mapSeaCompent).transform.position, ((Component)mapSeaCompent2).transform.position);
					num++;
				}
				mapSeaCompent = mapSeaCompent2;
			}
		}
	}

	public string GetDirectonName(SeaAvatarObjBase.Directon direction)
	{
		switch (direction)
		{
		case SeaAvatarObjBase.Directon.UP:
			return "B";
		case SeaAvatarObjBase.Directon.Down:
			return "Z";
		case SeaAvatarObjBase.Directon.Left:
		case SeaAvatarObjBase.Directon.Right:
			return "C";
		default:
			return null;
		}
	}

	public void StartMove()
	{
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Expected O, but got Unknown
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Expected O, but got Unknown
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Expected O, but got Unknown
		int num = 0;
		Queue<UnityAction> queue = new Queue<UnityAction>();
		object obj = _003C_003Ec._003C_003E9__46_0;
		if (obj == null)
		{
			UnityAction val = delegate
			{
				AllMapManage.instance.MapPlayerController.SetSpeed(1);
				YSFuncList.Ints.Continue();
			};
			_003C_003Ec._003C_003E9__46_0 = val;
			obj = (object)val;
		}
		UnityAction item = (UnityAction)obj;
		queue.Enqueue(item);
		foreach (int temp in LuXian)
		{
			int _tindex = num;
			UnityAction item2 = (UnityAction)delegate
			{
				Move(temp, _tindex);
			};
			queue.Enqueue(item2);
			num++;
		}
		UnityAction item3 = (UnityAction)delegate
		{
			RemoveAllLuXian();
			AllMapManage.instance.MapPlayerController.SetSpeed(0);
			YSFuncList.Ints.Continue();
		};
		queue.Enqueue(item3);
		YSFuncList.Ints.AddFunc(queue);
	}

	public void CreateMonstar(int ID, int index, string uuid, int seaID, bool isNTaskMonstar = false)
	{
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		JToken val = jsonData.instance.EndlessSeaNPCData[ID.ToString()];
		GameObject obj = Object.Instantiate<GameObject>(MonstarObject);
		SeaAvatarObjBase component = obj.GetComponent<SeaAvatarObjBase>();
		int type = (int)val[(object)"AIType"];
		component.ResetAITpe(type);
		component.speed = (int)val[(object)"speed"];
		component._EventId = ID;
		component.NowMapIndex = index;
		component.UUID = uuid;
		component.SeaId = seaID;
		component.ISNTaskMonstar = isNTaskMonstar;
		obj.transform.position = ((Component)AllMapManage.instance.mapIndex[index]).transform.position;
		MonstarList.Add(component);
		if ((int)val[(object)"EventType"] == 1)
		{
			SkeletonAnimation componentInChildren = component.objBase.GetComponentInChildren<SkeletonAnimation>();
			((SkeletonRenderer)componentInChildren).skeletonDataAsset = skeletonmonstar["hai_guai_SkeletonData"];
			if (SceneEx.NowSceneName == "Sea2")
			{
				((SkeletonRenderer)componentInChildren).initialSkinName = "qian";
			}
			else
			{
				((SkeletonRenderer)componentInChildren).initialSkinName = "shen";
			}
			((SkeletonRenderer)componentInChildren).Initialize(true);
		}
		if (isNTaskMonstar)
		{
			component.NtaskSpine.gameObject.SetActive(true);
			component.objBase.SetActive(false);
			((Component)component).GetComponent<Animator>().runtimeAnimatorController = component.HaiGuaiAnimCtl;
			return;
		}
		component.NtaskSpine.gameObject.SetActive(false);
		component.objBase.SetActive(true);
		int num = (int)jsonData.instance.EndlessSeaNPCData[ID.ToString()][(object)"stvalue"][(object)0];
		if (num < 2000)
		{
			UINPCData uINPCData = new UINPCData(NPCEx.GetSeaNPCIDByEventID(ID));
			uINPCData.RefreshData();
			((Component)component).GetComponent<Animator>().runtimeAnimatorController = component.ChuanAnimCtl;
			((Component)component).GetComponent<SkeletonAnimationHandleExample>().statesAndAnimations = component.ChuanStatesAndAnimations;
			SkeletonAnimation componentInChildren2 = component.objBase.GetComponentInChildren<SkeletonAnimation>();
			((SkeletonRenderer)componentInChildren2).initialSkinName = $"boat_{uINPCData.BigLevel + 1}";
			((SkeletonRenderer)componentInChildren2).Initialize(true);
		}
		if (num > 10000)
		{
			Debug.Log((object)$"在{index}生成了随机事件");
			SkeletonAnimation componentInChildren3 = component.objBase.GetComponentInChildren<SkeletonAnimation>();
			((SkeletonRenderer)componentInChildren3).skeletonDataAsset = skeletonmonstar["RandomEvent_SkeletonData"];
			((SkeletonRenderer)componentInChildren3).initialSkinName = "default";
			componentInChildren3.AnimationName = "Act1";
			((SkeletonRenderer)componentInChildren3).Initialize(true);
		}
	}

	private void Move(int endPositon, int index)
	{
		int avatarNowMapIndex = getAvatarNowMapIndex();
		List<int> roadXian = GetRoadXian(avatarNowMapIndex, endPositon);
		LuXianDian[index] = roadXian;
		RestLuXianDianSprite();
		if (roadXian.Count > 0)
		{
			if (((MapSeaCompent)AllMapManage.instance.mapIndex[roadXian[0]]).SatrtMove())
			{
				((MonoBehaviour)this).StartCoroutine(DieDaiMove(endPositon, index));
			}
			else
			{
				YSFuncList.Ints.Continue();
			}
		}
		else
		{
			YSFuncList.Ints.Continue();
		}
	}

	public IEnumerator DieDaiMove(int endPositon, int index)
	{
		yield return (object)new WaitForSeconds(1f);
		if (StopMove)
		{
			StopMove = false;
			YSFuncList.Ints.Continue();
		}
		else
		{
			Move(endPositon, index);
		}
	}

	public void StopAllContens()
	{
		foreach (SeaAvatarObjBase monstar in MonstarList)
		{
			if ((Object)(object)monstar != (Object)null)
			{
				((MonoBehaviour)monstar).StopAllCoroutines();
			}
		}
		AllMapManage.instance.MapPlayerController.SetSpeed(0);
		StopMove = true;
		YSFuncList.Ints.ClearQueue();
	}

	public void DrawLine(SpriteRenderer sprite, Vector3 StartPositon, Vector3 EndPosition)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		((Component)sprite).transform.position = StartPositon;
		float num = Vector3.Distance(StartPositon, EndPosition);
		sprite.size = new Vector2(num / ((Component)sprite).transform.localScale.x, sprite.size.y);
		Vector3 val = EndPosition - StartPositon;
		float num2 = Vector3.Angle(new Vector3(1f, 0f, 0f), val);
		if (val.y < 0f)
		{
			num2 = 360f - num2;
		}
		((Component)sprite).transform.localRotation = Quaternion.Euler(0f, 0f, num2);
	}

	public void RemoveLuXianDian(int index)
	{
		int index2 = LuXian.IndexOf(index);
		LuXian.RemoveAt(index2);
		ResetLuXianDian();
		autoRestLuXianUIID();
	}

	public void autoRestLuXianUIID()
	{
		foreach (KeyValuePair<int, BaseMapCompont> item in AllMapManage.instance.mapIndex)
		{
			MapSeaCompent mapSeaCompent = (MapSeaCompent)item.Value;
			if ((Object)(object)mapSeaCompent.MoveFlagUI != (Object)null && !LuXian.Contains(mapSeaCompent.NodeIndex))
			{
				mapSeaCompent.MoveFlagUI.SetActive(false);
			}
		}
		foreach (int item2 in LuXian)
		{
			((MapSeaCompent)AllMapManage.instance.mapIndex[item2]).RestNodeLuXianIndex();
		}
	}

	public List<int> GetRoadXian(int startIndex, int endIndex)
	{
		List<int> list = new List<int>();
		GetIndexList(startIndex, endIndex, list);
		return list;
	}

	public void GetIndexList(int startIndex, int endIndex, List<int> NodeIndexList)
	{
		if (startIndex != endIndex)
		{
			FindPath(startIndex, endIndex);
			MapSeaCompent start = (MapSeaCompent)AllMapManage.instance.mapIndex[startIndex];
			MapSeaCompent end = (MapSeaCompent)AllMapManage.instance.mapIndex[endIndex];
			ShowPath(end, start, NodeIndexList);
		}
	}

	public void ShowPath(MapSeaCompent end, MapSeaCompent start, List<int> NodeIndexList)
	{
		List<MapSeaCompent> list = new List<MapSeaCompent>();
		list.Add(end);
		MapSeaCompent seaParent = end.SeaParent;
		while ((Object)(object)seaParent != (Object)(object)start)
		{
			list.Add(seaParent);
			seaParent = seaParent.SeaParent;
		}
		for (int num = list.Count - 1; num >= 0; num--)
		{
			NodeIndexList.Add(list[num].NodeIndex);
		}
	}

	public void dieDaiAddIndex(List<int> NodeIndexList, int startIndex, int endIndex, Dictionary<int, int> closeList, List<int> openList)
	{
		if (startIndex == endIndex)
		{
			return;
		}
		MapSeaCompent obj = (MapSeaCompent)AllMapManage.instance.mapIndex[startIndex];
		MapSeaCompent mapSeaCompent = (MapSeaCompent)AllMapManage.instance.mapIndex[endIndex];
		List<int> nextIndex = obj.nextIndex;
		int num = 0;
		int num2 = -1;
		float num3 = 1E+13f;
		Avatar player = Tools.instance.getPlayer();
		int indexX = FuBenMap.getIndexX(endIndex, MapWide);
		int indexY = FuBenMap.getIndexY(endIndex, MapWide);
		foreach (int item in nextIndex)
		{
			if (openList.Contains(item))
			{
				openList.Add(item);
			}
		}
		foreach (int item2 in nextIndex)
		{
			if (endIndex == item2)
			{
				NodeIndexList.Add(item2);
				return;
			}
			if (!closeList.ContainsKey(item2))
			{
				int indexX2 = FuBenMap.getIndexX(item2, MapWide);
				int indexY2 = FuBenMap.getIndexY(item2, MapWide);
				int num4 = Mathf.Abs(indexX - indexX2) + Mathf.Abs(indexY - indexY2) * 10;
				int num5 = player.seaNodeMag.GetIndexFengBaoLv(item2, MapWide) * 10;
				int num6 = 10 + num5;
				int num7 = num4 + num6;
				if ((float)num7 < num3 && mapSeaCompent.CheckCanGetIn(item2))
				{
					num3 = num7;
					num2 = item2;
				}
				num++;
			}
		}
		if (num2 != -1 && num2 != endIndex)
		{
			closeList[num2] = 1;
			NodeIndexList.Add(num2);
			dieDaiAddIndex(NodeIndexList, num2, endIndex, closeList, openList);
		}
	}

	private void OnDestroy()
	{
		Inst = null;
	}

	public void RemoveIndex()
	{
	}

	public void FindPath(int startIndex, int endIndex)
	{
		List<MapSeaCompent> list = new List<MapSeaCompent>();
		List<MapSeaCompent> list2 = new List<MapSeaCompent>();
		MapSeaCompent mapSeaCompent = (MapSeaCompent)AllMapManage.instance.mapIndex[startIndex];
		MapSeaCompent mapSeaCompent2 = (MapSeaCompent)AllMapManage.instance.mapIndex[endIndex];
		mapSeaCompent.F = 0;
		mapSeaCompent.G = 0;
		mapSeaCompent.H = 0;
		mapSeaCompent.SeaParent = null;
		list.Add(mapSeaCompent);
		while (list.Count > 0)
		{
			MapSeaCompent minFOfList = GetMinFOfList(list);
			list.Remove(minFOfList);
			list2.Add(minFOfList);
			List<MapSeaCompent> surroundPoint = GetSurroundPoint(minFOfList.NodeIndex);
			foreach (MapSeaCompent item in list2)
			{
				if (surroundPoint.Contains(item))
				{
					surroundPoint.Remove(item);
				}
			}
			foreach (MapSeaCompent item2 in surroundPoint)
			{
				if (list.Contains(item2))
				{
					int num = 10 + GetQuanZhon(item2) + minFOfList.G;
					if (num < item2.G)
					{
						item2.SetParent(minFOfList, num);
					}
				}
				else
				{
					item2.SeaParent = minFOfList;
					GetF(item2, mapSeaCompent2);
					list.Add(item2);
				}
			}
			if (list.Contains(mapSeaCompent2))
			{
				break;
			}
		}
	}

	public List<MapSeaCompent> GetSurroundPoint(int index)
	{
		List<MapSeaCompent> list = new List<MapSeaCompent>();
		foreach (int item2 in ((MapSeaCompent)AllMapManage.instance.mapIndex[index]).nextIndex)
		{
			if (AllMapManage.instance.mapIndex.ContainsKey(item2) && ((Component)AllMapManage.instance.mapIndex[item2]).gameObject.activeSelf)
			{
				MapSeaCompent item = (MapSeaCompent)AllMapManage.instance.mapIndex[item2];
				list.Add(item);
			}
		}
		return list;
	}

	public int GetQuanZhon(MapSeaCompent index)
	{
		int num = Tools.instance.getPlayer().seaNodeMag.GetIndexFengBaoLv(index.NodeIndex, MapWide);
		Avatar player = Tools.instance.getPlayer();
		if (num != 0)
		{
			JToken nowLingZhouShuXinJson = player.GetNowLingZhouShuXinJson();
			if (nowLingZhouShuXinJson != null && Mathf.Clamp((int)jsonData.instance.EndlessSeaLinQiSafeLvData[num.ToString()][(object)"chuandemage"] - (int)nowLingZhouShuXinJson[(object)"Defense"], 0, 99999) <= 0)
			{
				num = 0;
			}
		}
		if ((Object)(object)((Component)((Component)index).transform).GetComponent<SeaToOherScene>() != (Object)null)
		{
			num += 100;
		}
		return num * 30;
	}

	public void GetF(MapSeaCompent point, MapSeaCompent end)
	{
		int num = 0;
		int num2 = Mathf.Abs(end.X - point.X) * 10 + Mathf.Abs(end.Y - point.Y) * 10;
		if ((Object)(object)point.SeaParent != (Object)null)
		{
			int quanZhon = GetQuanZhon(point);
			num = 10 + quanZhon + point.SeaParent.G;
		}
		int f = num2 + num;
		point.H = num2;
		point.G = num;
		point.F = f;
	}

	public MapSeaCompent GetMinFOfList(List<MapSeaCompent> list)
	{
		int num = int.MaxValue;
		MapSeaCompent result = null;
		foreach (MapSeaCompent item in list)
		{
			if (item.F < num)
			{
				num = item.F;
				result = item;
			}
		}
		return result;
	}
}
