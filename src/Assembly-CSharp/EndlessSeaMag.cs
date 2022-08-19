using System;
using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using KBEngine;
using Newtonsoft.Json.Linq;
using Spine.Unity;
using Spine.Unity.Examples;
using UnityEngine;
using UnityEngine.Events;
using YSGame;

// Token: 0x02000187 RID: 391
public class EndlessSeaMag : MonoBehaviour
{
	// Token: 0x0600109D RID: 4253 RVA: 0x00061E4C File Offset: 0x0006004C
	private void Awake()
	{
		EndlessSeaMag.Inst = this;
		this.externalBehaviorTrees = new Dictionary<int, ExternalBehaviorTree>();
		foreach (KeyValuePair<string, JToken> keyValuePair in jsonData.instance.EndlessSeaNPCData)
		{
			if (!this.externalBehaviorTrees.ContainsKey((int)keyValuePair.Value["AIType"]))
			{
				this.externalBehaviorTrees[(int)keyValuePair.Value["AIType"]] = (Resources.Load("MapPrefab/SeaAI/SAI" + (int)keyValuePair.Value["AIType"]) as ExternalBehaviorTree);
			}
		}
		GameObject gameObject = Resources.Load("MapPrefab/SeaAI/fengbao/fengbaoBase") as GameObject;
		if (gameObject != null)
		{
			this.fengBao = gameObject.transform.GetComponent<EndlessFengBao>();
		}
		if (this.FengBaoObjList == null)
		{
			this.FengBaoObjList = new GameObject("FengBaoList");
		}
		if (this.targetUI == null)
		{
			GameObject gameObject2 = Resources.Load("MapPrefab/SeaAI/SeaTargetUI") as GameObject;
			this.targetUI = Object.Instantiate<GameObject>(gameObject2).GetComponent<SeaTargetUI>();
			this.targetUI.GetComponent<Canvas>().worldCamera = Camera.main;
			this.targetUI.GetComponent<Canvas>().planeDistance = 6.4f;
		}
		if (this.LangHua == null)
		{
			this.LangHua = (Resources.Load("MapPrefab/SeaAI/fengbao/LangHua") as GameObject);
		}
		this.skeletonmonstar["hai_guai_SkeletonData"] = (Resources.Load("MapPrefab/SeaAI/Monstar/haishou_SkeletonData") as SkeletonDataAsset);
		this.skeletonmonstar["RandomEvent_SkeletonData"] = (Resources.Load("MapPrefab/SeaAI/shijian/haishangqiyu_SkeletonData") as SkeletonDataAsset);
	}

	// Token: 0x0600109E RID: 4254 RVA: 0x00062020 File Offset: 0x00060220
	private void Start()
	{
		base.Invoke("autoCreatAllMonstar", 0.01f);
	}

	// Token: 0x0600109F RID: 4255 RVA: 0x00062034 File Offset: 0x00060234
	public int GetHaiYuLvIndex(int _seaid)
	{
		Avatar player = Tools.instance.getPlayer();
		int num = (int)((JArray)player.EndlessSea["SafeLv"])[_seaid - 1];
		JToken jtoken = jsonData.instance.EndlessSeaLuanLiuRandom[num.ToString()];
		return (int)player.EndlessSea["LuanLiuId"][_seaid - 1] % ((JArray)jtoken).Count;
	}

	// Token: 0x060010A0 RID: 4256 RVA: 0x000620B4 File Offset: 0x000602B4
	public void autoCreatFengBao()
	{
		Avatar player = Tools.instance.getPlayer();
		foreach (int num in this.seaGrid.HaiYuIDList)
		{
			this.oldFengBaoindex[num] = this.GetHaiYuLvIndex(num);
			foreach (JToken temp in player.seaNodeMag.GetFengBaoIndexList(num))
			{
				this.CreateFengBao(temp, num);
			}
		}
	}

	// Token: 0x060010A1 RID: 4257 RVA: 0x00062168 File Offset: 0x00060368
	public void CreateFengBao(JToken temp, int seaid)
	{
		int realIndex = EndlessSeaMag.GetRealIndex(seaid, (int)temp["index"]);
		EndlessFengBao endlessFengBao = Object.Instantiate<EndlessFengBao>(this.fengBao);
		endlessFengBao.transform.parent = this.FengBaoObjList.transform;
		endlessFengBao.transform.position = AllMapManage.instance.mapIndex[realIndex].transform.position;
		endlessFengBao.id = (int)temp["id"];
		endlessFengBao.index = (int)temp["index"];
		endlessFengBao.lv = (int)temp["lv"];
		endlessFengBao.seaid = seaid;
		endlessFengBao.Show();
	}

	// Token: 0x060010A2 RID: 4258 RVA: 0x00062220 File Offset: 0x00060420
	public void autoResetFengBao()
	{
		Avatar player = Tools.instance.getPlayer();
		foreach (int num in this.seaGrid.HaiYuIDList)
		{
			if (this.oldFengBaoindex[num] != this.GetHaiYuLvIndex(num))
			{
				foreach (JToken temp in player.seaNodeMag.GetFengBaoIndexList(num))
				{
					this.ResetFengBao(temp, num);
				}
			}
		}
	}

	// Token: 0x060010A3 RID: 4259 RVA: 0x000622D8 File Offset: 0x000604D8
	public void ResetFengBao(JToken temp, int seaid)
	{
		int realIndex = EndlessSeaMag.GetRealIndex(seaid, (int)temp["index"]);
		foreach (object obj in this.FengBaoObjList.transform)
		{
			EndlessFengBao component = ((Transform)obj).GetComponent<EndlessFengBao>();
			if (component.id == (int)temp["id"] && component.lv == (int)temp["lv"] && component.seaid == seaid && component.index != (int)temp["index"])
			{
				component.Move(AllMapManage.instance.mapIndex[realIndex].transform.position);
				break;
			}
		}
	}

	// Token: 0x060010A4 RID: 4260 RVA: 0x000623C4 File Offset: 0x000605C4
	public static bool IsOutMap(int x, int y)
	{
		return x < 0 || x > EndlessSeaMag.MapWide || y < 0 || y > 70;
	}

	// Token: 0x060010A5 RID: 4261 RVA: 0x000623E0 File Offset: 0x000605E0
	public static List<int> GetAroundIndexList(int avatarindex, int round, bool shizi = false)
	{
		List<int> list = new List<int>();
		int indexX = FuBenMap.getIndexX(avatarindex, EndlessSeaMag.MapWide);
		int indexY = FuBenMap.getIndexY(avatarindex, EndlessSeaMag.MapWide);
		for (int i = -round; i <= round; i++)
		{
			for (int j = -round; j <= round; j++)
			{
				if ((!shizi || i == 0 || j == 0) && !EndlessSeaMag.IsOutMap(indexX + i, indexY + j))
				{
					int index = FuBenMap.getIndex(indexX + i, indexY + j, EndlessSeaMag.MapWide);
					list.Add(index);
				}
			}
		}
		return list;
	}

	// Token: 0x060010A6 RID: 4262 RVA: 0x00062460 File Offset: 0x00060660
	public List<SeaAvatarObjBase> GetAroundEventList(int round, int eventType)
	{
		int nowIndex = Tools.instance.getPlayer().fubenContorl[Tools.getScreenName()].NowIndex;
		int indexX = FuBenMap.getIndexX(nowIndex, EndlessSeaMag.MapWide);
		int indexY = FuBenMap.getIndexY(nowIndex, EndlessSeaMag.MapWide);
		List<SeaAvatarObjBase> list = new List<SeaAvatarObjBase>();
		for (int i = -round; i <= round; i++)
		{
			for (int j = -round; j <= round; j++)
			{
				if (!EndlessSeaMag.IsOutMap(indexX + i, indexY + j))
				{
					int index = FuBenMap.getIndex(indexX + i, indexY + j, EndlessSeaMag.MapWide);
					foreach (SeaAvatarObjBase seaAvatarObjBase in this.MonstarList)
					{
						if (seaAvatarObjBase.NowMapIndex == index && (int)seaAvatarObjBase.Json["EventType"] == eventType)
						{
							list.Add(seaAvatarObjBase);
						}
					}
				}
			}
		}
		this.RoundEventList = list;
		return list;
	}

	// Token: 0x060010A7 RID: 4263 RVA: 0x00062568 File Offset: 0x00060768
	public void autoCreatAllMonstar()
	{
		Avatar player = Tools.instance.getPlayer();
		foreach (int seaID in this.seaGrid.HaiYuIDList)
		{
			foreach (JToken jtoken in player.EndlessSeaRandomNode[seaID.ToString()]["Monstar"])
			{
				int id = (int)jtoken["monstarId"];
				int index;
				if ((int)jtoken["index"] <= 49)
				{
					index = EndlessSeaMag.GetRealIndex(seaID, (int)jtoken["index"]);
				}
				else
				{
					index = (int)jtoken["index"];
				}
				string uuid = (string)jtoken["uuid"];
				this.CreateMonstar(id, index, uuid, seaID, false);
			}
		}
		GameObject gameObject = GameObject.Find("ThreeSceneNpcCanvas");
		if (gameObject != null)
		{
			gameObject.gameObject.SetActive(false);
		}
		try
		{
			this.NTaskCreateMonstar();
		}
		catch (Exception ex)
		{
			Debug.LogError(ex);
		}
		this.autoCreatFengBao();
		this.SetCanSeeMonstar();
	}

	// Token: 0x060010A8 RID: 4264 RVA: 0x000626E4 File Offset: 0x000608E4
	public void NTaskCreateMonstar()
	{
		Avatar player = Tools.instance.getPlayer();
		foreach (JSONObject jsonobject in player.nomelTaskMag.GetNowNTask())
		{
			int i = jsonobject["id"].I;
			List<JSONObject> ntaskXiangXiList = player.nomelTaskMag.GetNTaskXiangXiList(i);
			int num = 0;
			using (List<JSONObject>.Enumerator enumerator2 = ntaskXiangXiList.GetEnumerator())
			{
				while (enumerator2.MoveNext())
				{
					if (enumerator2.Current["type"].I == 7)
					{
						int chilidID = player.nomelTaskMag.getChilidID(i, num);
						player.nomelTaskMag.getWhereChilidID(i, num);
						JSONObject jsonobject2 = jsonData.instance.NTaskSuiJI[chilidID.ToString()];
						JSONObject jsonobject3 = jsonData.instance.NTaskSuiJI[chilidID.ToString()];
						if (jsonobject3["StrValue"].str == Tools.getScreenName())
						{
							int inSeaID = player.seaNodeMag.GetInSeaID(jsonobject3["Value"].I, EndlessSeaMag.MapWide);
							this.CreateMonstar(jsonobject2["Value"].I, jsonobject3["Value"].I, Tools.getUUID(), inSeaID, true);
						}
					}
					num++;
				}
			}
		}
	}

	// Token: 0x060010A9 RID: 4265 RVA: 0x0006288C File Offset: 0x00060A8C
	public static int GetRealIndex(int seaID, int index)
	{
		int indexX = FuBenMap.getIndexX(seaID, EndlessSeaMag.MapWide / 7);
		int indexY = FuBenMap.getIndexY(seaID, EndlessSeaMag.MapWide / 7);
		int indexX2 = FuBenMap.getIndexX(index, 7);
		int indexY2 = FuBenMap.getIndexY(index, 7);
		int x = indexX * 7 + indexX2;
		int y = indexY * 7 + indexY2;
		return FuBenMap.getIndex(x, y, EndlessSeaMag.MapWide);
	}

	// Token: 0x060010AA RID: 4266 RVA: 0x000628D9 File Offset: 0x00060AD9
	public void AddLuXianDian(int index)
	{
		this.LuXian.Add(index);
		this.ResetLuXianDian();
	}

	// Token: 0x060010AB RID: 4267 RVA: 0x000628F0 File Offset: 0x00060AF0
	public void SetCanSeeMonstar()
	{
		Avatar player = Tools.instance.getPlayer();
		List<int> inSeeIndex = this.GetInSeeIndex(player.shengShi, this.getAvatarNowMapIndex());
		foreach (SeaAvatarObjBase seaAvatarObjBase in this.MonstarList)
		{
			if (inSeeIndex.Contains(seaAvatarObjBase.NowMapIndex) || seaAvatarObjBase.ISNTaskMonstar)
			{
				seaAvatarObjBase.ShowMonstarObj();
			}
			else
			{
				seaAvatarObjBase.HideMonstarObj();
			}
		}
		foreach (int num in inSeeIndex)
		{
			if (AllMapManage.instance.mapIndex.ContainsKey(num))
			{
				MapSeaCompent mapSeaCompent = (MapSeaCompent)AllMapManage.instance.mapIndex[num];
				if (mapSeaCompent.NodeHasIsLand())
				{
					EndlessSeaMag.AddSeeIsland(num);
				}
				if (mapSeaCompent.WhetherHasJiZhi)
				{
					EndlessSeaMag.AddSeeIsland(num);
				}
			}
		}
	}

	// Token: 0x060010AC RID: 4268 RVA: 0x000629FC File Offset: 0x00060BFC
	public static void AddSeeIsland(int sea)
	{
		JArray jarray = (JArray)Tools.instance.getPlayer().EndlessSeaAvatarSeeIsland["Island"];
		if (!Tools.ContensInt(jarray, sea))
		{
			jarray.Add(sea);
		}
	}

	// Token: 0x060010AD RID: 4269 RVA: 0x00062A40 File Offset: 0x00060C40
	public List<int> GetInSeeIndex(int shenshi, int AvatarIndex)
	{
		JToken jtoken = Tools.FindJTokens(jsonData.instance.EndlessSeaShiYe, (JToken aa) => (int)aa["shenshi"] >= shenshi);
		int indexX = FuBenMap.getIndexX(AvatarIndex, EndlessSeaMag.MapWide);
		int indexY = FuBenMap.getIndexY(AvatarIndex, EndlessSeaMag.MapWide);
		List<int> list = new List<int>();
		List<int> list2 = new List<int>();
		List<int> list3 = new List<int>();
		int num = 0;
		foreach (JToken jtoken2 in jtoken["xinzhuang"])
		{
			if (num % 2 == 0)
			{
				list.Add((int)jtoken2);
			}
			else
			{
				list2.Add((int)jtoken2);
			}
			num++;
		}
		int num2 = 0;
		foreach (int num3 in list2)
		{
			int x = indexX + list[num2];
			int y = indexY + list2[num2];
			if (!EndlessSeaMag.IsOutMap(x, y))
			{
				int index = FuBenMap.getIndex(x, y, EndlessSeaMag.MapWide);
				list3.Add(index);
				num2++;
			}
		}
		return list3;
	}

	// Token: 0x060010AE RID: 4270 RVA: 0x00062B90 File Offset: 0x00060D90
	public bool IsInSeeType(int shenshi, int AvatarIndex, int TargetIndex)
	{
		return this.GetInSeeIndex(shenshi, AvatarIndex).Contains(TargetIndex);
	}

	// Token: 0x060010AF RID: 4271 RVA: 0x00004095 File Offset: 0x00002295
	public void MoveToSeaPositon(int start, int end)
	{
	}

	// Token: 0x060010B0 RID: 4272 RVA: 0x00062BA5 File Offset: 0x00060DA5
	public int getAvatarNowMapIndex()
	{
		return Tools.instance.getPlayer().fubenContorl[Tools.getScreenName()].NowIndex;
	}

	// Token: 0x060010B1 RID: 4273 RVA: 0x00062BC8 File Offset: 0x00060DC8
	public void ResetLuXianDian()
	{
		int num = 1;
		int avatarNowMapIndex = this.getAvatarNowMapIndex();
		this.LuXianDian.Clear();
		foreach (int endIndex in this.LuXian)
		{
			List<int> roadXian = this.GetRoadXian((num > 1) ? this.LuXian[num - 2] : avatarNowMapIndex, endIndex);
			this.LuXianDian.Add(roadXian);
			num++;
		}
		this.RestLuXianDianSprite();
	}

	// Token: 0x060010B2 RID: 4274 RVA: 0x00062C60 File Offset: 0x00060E60
	public void RemoveAllLuXian()
	{
		this.LuXian.Clear();
		this.LuXianDian.Clear();
		this.autoRestLuXianUIID();
		this.RestLuXianDianSprite();
	}

	// Token: 0x060010B3 RID: 4275 RVA: 0x00062C84 File Offset: 0x00060E84
	public void RestLuXianDianSprite()
	{
		foreach (object obj in this.LineUIBase.transform)
		{
			((Transform)obj).transform.position = new Vector3(0f, 100000f, 0f);
		}
		MapSeaCompent mapSeaCompent = (MapSeaCompent)AllMapManage.instance.mapIndex[this.getAvatarNowMapIndex()];
		int num = 0;
		foreach (List<int> list in this.LuXianDian)
		{
			foreach (int key in list)
			{
				MapSeaCompent mapSeaCompent2 = (MapSeaCompent)AllMapManage.instance.mapIndex[key];
				if (mapSeaCompent != null)
				{
					Transform transform;
					if (this.LineUIBase.transform.childCount > num)
					{
						transform = this.LineUIBase.transform.GetChild(num);
					}
					else
					{
						transform = Object.Instantiate<GameObject>(this.LuXianUI).transform;
						transform.parent = this.LineUIBase.transform;
					}
					this.DrawLine(transform.GetComponent<SpriteRenderer>(), mapSeaCompent.transform.position, mapSeaCompent2.transform.position);
					num++;
				}
				mapSeaCompent = mapSeaCompent2;
			}
		}
	}

	// Token: 0x060010B4 RID: 4276 RVA: 0x00062E30 File Offset: 0x00061030
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

	// Token: 0x060010B5 RID: 4277 RVA: 0x00062E60 File Offset: 0x00061060
	public void StartMove()
	{
		int num = 0;
		Queue<UnityAction> queue = new Queue<UnityAction>();
		UnityAction item = delegate()
		{
			AllMapManage.instance.MapPlayerController.SetSpeed(1);
			YSFuncList.Ints.Continue();
		};
		queue.Enqueue(item);
		using (List<int>.Enumerator enumerator = this.LuXian.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				EndlessSeaMag.<>c__DisplayClass46_0 CS$<>8__locals1 = new EndlessSeaMag.<>c__DisplayClass46_0();
				CS$<>8__locals1.<>4__this = this;
				CS$<>8__locals1.temp = enumerator.Current;
				int _tindex = num;
				UnityAction item2 = delegate()
				{
					CS$<>8__locals1.<>4__this.Move(CS$<>8__locals1.temp, _tindex);
				};
				queue.Enqueue(item2);
				num++;
			}
		}
		UnityAction item3 = delegate()
		{
			this.RemoveAllLuXian();
			AllMapManage.instance.MapPlayerController.SetSpeed(0);
			YSFuncList.Ints.Continue();
		};
		queue.Enqueue(item3);
		YSFuncList.Ints.AddFunc(queue);
	}

	// Token: 0x060010B6 RID: 4278 RVA: 0x00062F40 File Offset: 0x00061140
	public void CreateMonstar(int ID, int index, string uuid, int seaID, bool isNTaskMonstar = false)
	{
		JToken jtoken = jsonData.instance.EndlessSeaNPCData[ID.ToString()];
		GameObject gameObject = Object.Instantiate<GameObject>(this.MonstarObject);
		SeaAvatarObjBase component = gameObject.GetComponent<SeaAvatarObjBase>();
		int type = (int)jtoken["AIType"];
		component.ResetAITpe(type);
		component.speed = (int)jtoken["speed"];
		component._EventId = ID;
		component.NowMapIndex = index;
		component.UUID = uuid;
		component.SeaId = seaID;
		component.ISNTaskMonstar = isNTaskMonstar;
		gameObject.transform.position = AllMapManage.instance.mapIndex[index].transform.position;
		this.MonstarList.Add(component);
		if ((int)jtoken["EventType"] == 1)
		{
			SkeletonAnimation componentInChildren = component.objBase.GetComponentInChildren<SkeletonAnimation>();
			componentInChildren.skeletonDataAsset = this.skeletonmonstar["hai_guai_SkeletonData"];
			if (SceneEx.NowSceneName == "Sea2")
			{
				componentInChildren.initialSkinName = "qian";
			}
			else
			{
				componentInChildren.initialSkinName = "shen";
			}
			componentInChildren.Initialize(true);
		}
		if (isNTaskMonstar)
		{
			component.NtaskSpine.gameObject.SetActive(true);
			component.objBase.SetActive(false);
			component.GetComponent<Animator>().runtimeAnimatorController = component.HaiGuaiAnimCtl;
			return;
		}
		component.NtaskSpine.gameObject.SetActive(false);
		component.objBase.SetActive(true);
		int num = (int)jsonData.instance.EndlessSeaNPCData[ID.ToString()]["stvalue"][0];
		if (num < 2000)
		{
			UINPCData uinpcdata = new UINPCData(NPCEx.GetSeaNPCIDByEventID(ID), false);
			uinpcdata.RefreshData();
			component.GetComponent<Animator>().runtimeAnimatorController = component.ChuanAnimCtl;
			component.GetComponent<SkeletonAnimationHandleExample>().statesAndAnimations = component.ChuanStatesAndAnimations;
			SkeletonAnimation componentInChildren2 = component.objBase.GetComponentInChildren<SkeletonAnimation>();
			componentInChildren2.initialSkinName = string.Format("boat_{0}", uinpcdata.BigLevel + 1);
			componentInChildren2.Initialize(true);
		}
		if (num > 10000)
		{
			Debug.Log(string.Format("在{0}生成了随机事件", index));
			SkeletonAnimation componentInChildren3 = component.objBase.GetComponentInChildren<SkeletonAnimation>();
			componentInChildren3.skeletonDataAsset = this.skeletonmonstar["RandomEvent_SkeletonData"];
			componentInChildren3.initialSkinName = "default";
			componentInChildren3.AnimationName = "Act1";
			componentInChildren3.Initialize(true);
		}
	}

	// Token: 0x060010B7 RID: 4279 RVA: 0x000631A8 File Offset: 0x000613A8
	private void Move(int endPositon, int index)
	{
		int avatarNowMapIndex = this.getAvatarNowMapIndex();
		List<int> roadXian = this.GetRoadXian(avatarNowMapIndex, endPositon);
		this.LuXianDian[index] = roadXian;
		this.RestLuXianDianSprite();
		if (roadXian.Count <= 0)
		{
			YSFuncList.Ints.Continue();
			return;
		}
		if (((MapSeaCompent)AllMapManage.instance.mapIndex[roadXian[0]]).SatrtMove())
		{
			base.StartCoroutine(this.DieDaiMove(endPositon, index));
			return;
		}
		YSFuncList.Ints.Continue();
	}

	// Token: 0x060010B8 RID: 4280 RVA: 0x00063228 File Offset: 0x00061428
	public IEnumerator DieDaiMove(int endPositon, int index)
	{
		yield return new WaitForSeconds(1f);
		if (EndlessSeaMag.StopMove)
		{
			EndlessSeaMag.StopMove = false;
			YSFuncList.Ints.Continue();
		}
		else
		{
			this.Move(endPositon, index);
		}
		yield break;
	}

	// Token: 0x060010B9 RID: 4281 RVA: 0x00063248 File Offset: 0x00061448
	public void StopAllContens()
	{
		foreach (SeaAvatarObjBase seaAvatarObjBase in this.MonstarList)
		{
			if (seaAvatarObjBase != null)
			{
				seaAvatarObjBase.StopAllCoroutines();
			}
		}
		AllMapManage.instance.MapPlayerController.SetSpeed(0);
		EndlessSeaMag.StopMove = true;
		YSFuncList.Ints.ClearQueue();
	}

	// Token: 0x060010BA RID: 4282 RVA: 0x000632C4 File Offset: 0x000614C4
	public void DrawLine(SpriteRenderer sprite, Vector3 StartPositon, Vector3 EndPosition)
	{
		sprite.transform.position = StartPositon;
		float num = Vector3.Distance(StartPositon, EndPosition);
		sprite.size = new Vector2(num / sprite.transform.localScale.x, sprite.size.y);
		Vector3 vector = EndPosition - StartPositon;
		float num2 = Vector3.Angle(new Vector3(1f, 0f, 0f), vector);
		if (vector.y < 0f)
		{
			num2 = 360f - num2;
		}
		sprite.transform.localRotation = Quaternion.Euler(0f, 0f, num2);
	}

	// Token: 0x060010BB RID: 4283 RVA: 0x00063360 File Offset: 0x00061560
	public void RemoveLuXianDian(int index)
	{
		int index2 = this.LuXian.IndexOf(index);
		this.LuXian.RemoveAt(index2);
		this.ResetLuXianDian();
		this.autoRestLuXianUIID();
	}

	// Token: 0x060010BC RID: 4284 RVA: 0x00063394 File Offset: 0x00061594
	public void autoRestLuXianUIID()
	{
		foreach (KeyValuePair<int, BaseMapCompont> keyValuePair in AllMapManage.instance.mapIndex)
		{
			MapSeaCompent mapSeaCompent = (MapSeaCompent)keyValuePair.Value;
			if (mapSeaCompent.MoveFlagUI != null && !this.LuXian.Contains(mapSeaCompent.NodeIndex))
			{
				mapSeaCompent.MoveFlagUI.SetActive(false);
			}
		}
		foreach (int key in this.LuXian)
		{
			((MapSeaCompent)AllMapManage.instance.mapIndex[key]).RestNodeLuXianIndex();
		}
	}

	// Token: 0x060010BD RID: 4285 RVA: 0x00063478 File Offset: 0x00061678
	public List<int> GetRoadXian(int startIndex, int endIndex)
	{
		List<int> list = new List<int>();
		this.GetIndexList(startIndex, endIndex, list);
		return list;
	}

	// Token: 0x060010BE RID: 4286 RVA: 0x00063498 File Offset: 0x00061698
	public void GetIndexList(int startIndex, int endIndex, List<int> NodeIndexList)
	{
		if (startIndex == endIndex)
		{
			return;
		}
		this.FindPath(startIndex, endIndex);
		MapSeaCompent start = (MapSeaCompent)AllMapManage.instance.mapIndex[startIndex];
		MapSeaCompent end = (MapSeaCompent)AllMapManage.instance.mapIndex[endIndex];
		this.ShowPath(end, start, NodeIndexList);
	}

	// Token: 0x060010BF RID: 4287 RVA: 0x000634E8 File Offset: 0x000616E8
	public void ShowPath(MapSeaCompent end, MapSeaCompent start, List<int> NodeIndexList)
	{
		List<MapSeaCompent> list = new List<MapSeaCompent>();
		list.Add(end);
		MapSeaCompent seaParent = end.SeaParent;
		while (seaParent != start)
		{
			list.Add(seaParent);
			seaParent = seaParent.SeaParent;
		}
		for (int i = list.Count - 1; i >= 0; i--)
		{
			NodeIndexList.Add(list[i].NodeIndex);
		}
	}

	// Token: 0x060010C0 RID: 4288 RVA: 0x00063548 File Offset: 0x00061748
	public void dieDaiAddIndex(List<int> NodeIndexList, int startIndex, int endIndex, Dictionary<int, int> closeList, List<int> openList)
	{
		if (startIndex == endIndex)
		{
			return;
		}
		BaseMapCompont baseMapCompont = (MapSeaCompent)AllMapManage.instance.mapIndex[startIndex];
		MapSeaCompent mapSeaCompent = (MapSeaCompent)AllMapManage.instance.mapIndex[endIndex];
		List<int> nextIndex = baseMapCompont.nextIndex;
		int num = 0;
		int num2 = -1;
		float num3 = 1E+13f;
		Avatar player = Tools.instance.getPlayer();
		int indexX = FuBenMap.getIndexX(endIndex, EndlessSeaMag.MapWide);
		int indexY = FuBenMap.getIndexY(endIndex, EndlessSeaMag.MapWide);
		foreach (int item in nextIndex)
		{
			if (openList.Contains(item))
			{
				openList.Add(item);
			}
		}
		foreach (int num4 in nextIndex)
		{
			if (endIndex == num4)
			{
				NodeIndexList.Add(num4);
				return;
			}
			if (!closeList.ContainsKey(num4))
			{
				int indexX2 = FuBenMap.getIndexX(num4, EndlessSeaMag.MapWide);
				int indexY2 = FuBenMap.getIndexY(num4, EndlessSeaMag.MapWide);
				int num5 = Mathf.Abs(indexX - indexX2) + Mathf.Abs(indexY - indexY2) * 10;
				int num6 = player.seaNodeMag.GetIndexFengBaoLv(num4, EndlessSeaMag.MapWide) * 10;
				int num7 = 10 + num6;
				int num8 = num5 + num7;
				if ((float)num8 < num3 && mapSeaCompent.CheckCanGetIn(num4))
				{
					num3 = (float)num8;
					num2 = num4;
				}
				num++;
			}
		}
		if (num2 != -1 && num2 != endIndex)
		{
			closeList[num2] = 1;
			NodeIndexList.Add(num2);
			this.dieDaiAddIndex(NodeIndexList, num2, endIndex, closeList, openList);
		}
	}

	// Token: 0x060010C1 RID: 4289 RVA: 0x00063704 File Offset: 0x00061904
	private void OnDestroy()
	{
		EndlessSeaMag.Inst = null;
	}

	// Token: 0x060010C2 RID: 4290 RVA: 0x00004095 File Offset: 0x00002295
	public void RemoveIndex()
	{
	}

	// Token: 0x060010C3 RID: 4291 RVA: 0x0006370C File Offset: 0x0006190C
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
			MapSeaCompent minFOfList = this.GetMinFOfList(list);
			list.Remove(minFOfList);
			list2.Add(minFOfList);
			List<MapSeaCompent> surroundPoint = this.GetSurroundPoint(minFOfList.NodeIndex);
			foreach (MapSeaCompent item in list2)
			{
				if (surroundPoint.Contains(item))
				{
					surroundPoint.Remove(item);
				}
			}
			foreach (MapSeaCompent mapSeaCompent3 in surroundPoint)
			{
				if (list.Contains(mapSeaCompent3))
				{
					int num = 10 + this.GetQuanZhon(mapSeaCompent3) + minFOfList.G;
					if (num < mapSeaCompent3.G)
					{
						mapSeaCompent3.SetParent(minFOfList, num);
					}
				}
				else
				{
					mapSeaCompent3.SeaParent = minFOfList;
					this.GetF(mapSeaCompent3, mapSeaCompent2);
					list.Add(mapSeaCompent3);
				}
			}
			if (list.Contains(mapSeaCompent2))
			{
				break;
			}
		}
	}

	// Token: 0x060010C4 RID: 4292 RVA: 0x00063894 File Offset: 0x00061A94
	public List<MapSeaCompent> GetSurroundPoint(int index)
	{
		List<MapSeaCompent> list = new List<MapSeaCompent>();
		foreach (int key in ((MapSeaCompent)AllMapManage.instance.mapIndex[index]).nextIndex)
		{
			if (AllMapManage.instance.mapIndex.ContainsKey(key) && AllMapManage.instance.mapIndex[key].gameObject.activeSelf)
			{
				MapSeaCompent item = (MapSeaCompent)AllMapManage.instance.mapIndex[key];
				list.Add(item);
			}
		}
		return list;
	}

	// Token: 0x060010C5 RID: 4293 RVA: 0x00063948 File Offset: 0x00061B48
	public int GetQuanZhon(MapSeaCompent index)
	{
		int num = Tools.instance.getPlayer().seaNodeMag.GetIndexFengBaoLv(index.NodeIndex, EndlessSeaMag.MapWide);
		Avatar player = Tools.instance.getPlayer();
		if (num != 0)
		{
			JToken nowLingZhouShuXinJson = player.GetNowLingZhouShuXinJson();
			if (nowLingZhouShuXinJson != null && Mathf.Clamp((int)jsonData.instance.EndlessSeaLinQiSafeLvData[num.ToString()]["chuandemage"] - (int)nowLingZhouShuXinJson["Defense"], 0, 99999) <= 0)
			{
				num = 0;
			}
		}
		if (index.transform.GetComponent<SeaToOherScene>() != null)
		{
			num += 100;
		}
		return num * 30;
	}

	// Token: 0x060010C6 RID: 4294 RVA: 0x000639F0 File Offset: 0x00061BF0
	public void GetF(MapSeaCompent point, MapSeaCompent end)
	{
		int num = 0;
		int num2 = Mathf.Abs(end.X - point.X) * 10 + Mathf.Abs(end.Y - point.Y) * 10;
		if (point.SeaParent != null)
		{
			int quanZhon = this.GetQuanZhon(point);
			num = 10 + quanZhon + point.SeaParent.G;
		}
		int f = num2 + num;
		point.H = num2;
		point.G = num;
		point.F = f;
	}

	// Token: 0x060010C7 RID: 4295 RVA: 0x00063A6C File Offset: 0x00061C6C
	public MapSeaCompent GetMinFOfList(List<MapSeaCompent> list)
	{
		int num = int.MaxValue;
		MapSeaCompent result = null;
		foreach (MapSeaCompent mapSeaCompent in list)
		{
			if (mapSeaCompent.F < num)
			{
				num = mapSeaCompent.F;
				result = mapSeaCompent;
			}
		}
		return result;
	}

	// Token: 0x04000BFA RID: 3066
	public static EndlessSeaMag Inst;

	// Token: 0x04000BFB RID: 3067
	public static int MapWide = 133;

	// Token: 0x04000BFC RID: 3068
	public static bool StopMove = false;

	// Token: 0x04000BFD RID: 3069
	[HideInInspector]
	public List<int> LuXian = new List<int>();

	// Token: 0x04000BFE RID: 3070
	private List<List<int>> LuXianDian = new List<List<int>>();

	// Token: 0x04000BFF RID: 3071
	public GameObject mapNodeUI;

	// Token: 0x04000C00 RID: 3072
	public GameObject LuXianUI;

	// Token: 0x04000C01 RID: 3073
	public GameObject LineUIBase;

	// Token: 0x04000C02 RID: 3074
	public GameObject MonstarObject;

	// Token: 0x04000C03 RID: 3075
	[HideInInspector]
	public List<SeaAvatarObjBase> MonstarList = new List<SeaAvatarObjBase>();

	// Token: 0x04000C04 RID: 3076
	public SeaGrid seaGrid;

	// Token: 0x04000C05 RID: 3077
	[HideInInspector]
	public List<SeaAvatarObjBase> RoundEventList = new List<SeaAvatarObjBase>();

	// Token: 0x04000C06 RID: 3078
	public bool flagMonstarTarget = true;

	// Token: 0x04000C07 RID: 3079
	public bool NeedRefresh;

	// Token: 0x04000C08 RID: 3080
	public Dictionary<int, ExternalBehaviorTree> externalBehaviorTrees;

	// Token: 0x04000C09 RID: 3081
	[HideInInspector]
	public GameObject LangHua;

	// Token: 0x04000C0A RID: 3082
	[HideInInspector]
	public EndlessFengBao fengBao;

	// Token: 0x04000C0B RID: 3083
	private GameObject FengBaoObjList;

	// Token: 0x04000C0C RID: 3084
	public Dictionary<int, int> oldFengBaoindex = new Dictionary<int, int>();

	// Token: 0x04000C0D RID: 3085
	private Dictionary<string, SkeletonDataAsset> skeletonmonstar = new Dictionary<string, SkeletonDataAsset>();

	// Token: 0x04000C0E RID: 3086
	private SeaTargetUI targetUI;

	// Token: 0x04000C0F RID: 3087
	public SeaAvatarObjBase.Directon PlayerDirecton = SeaAvatarObjBase.Directon.Left;
}
