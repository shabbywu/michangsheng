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

// Token: 0x02000268 RID: 616
public class EndlessSeaMag : MonoBehaviour
{
	// Token: 0x060012F6 RID: 4854 RVA: 0x000B095C File Offset: 0x000AEB5C
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

	// Token: 0x060012F7 RID: 4855 RVA: 0x00011ED2 File Offset: 0x000100D2
	private void Start()
	{
		base.Invoke("autoCreatAllMonstar", 0.01f);
	}

	// Token: 0x060012F8 RID: 4856 RVA: 0x000B0B30 File Offset: 0x000AED30
	public int GetHaiYuLvIndex(int _seaid)
	{
		Avatar player = Tools.instance.getPlayer();
		int num = (int)((JArray)player.EndlessSea["SafeLv"])[_seaid - 1];
		JToken jtoken = jsonData.instance.EndlessSeaLuanLiuRandom[num.ToString()];
		return (int)player.EndlessSea["LuanLiuId"][_seaid - 1] % ((JArray)jtoken).Count;
	}

	// Token: 0x060012F9 RID: 4857 RVA: 0x000B0BB0 File Offset: 0x000AEDB0
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

	// Token: 0x060012FA RID: 4858 RVA: 0x000B0C64 File Offset: 0x000AEE64
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

	// Token: 0x060012FB RID: 4859 RVA: 0x000B0D1C File Offset: 0x000AEF1C
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

	// Token: 0x060012FC RID: 4860 RVA: 0x000B0DD4 File Offset: 0x000AEFD4
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

	// Token: 0x060012FD RID: 4861 RVA: 0x00011EE4 File Offset: 0x000100E4
	public static bool IsOutMap(int x, int y)
	{
		return x < 0 || x > EndlessSeaMag.MapWide || y < 0 || y > 70;
	}

	// Token: 0x060012FE RID: 4862 RVA: 0x000B0EC0 File Offset: 0x000AF0C0
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

	// Token: 0x060012FF RID: 4863 RVA: 0x000B0F40 File Offset: 0x000AF140
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

	// Token: 0x06001300 RID: 4864 RVA: 0x000B1048 File Offset: 0x000AF248
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

	// Token: 0x06001301 RID: 4865 RVA: 0x000B11C4 File Offset: 0x000AF3C4
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

	// Token: 0x06001302 RID: 4866 RVA: 0x000B136C File Offset: 0x000AF56C
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

	// Token: 0x06001303 RID: 4867 RVA: 0x00011EFE File Offset: 0x000100FE
	public void AddLuXianDian(int index)
	{
		this.LuXian.Add(index);
		this.ResetLuXianDian();
	}

	// Token: 0x06001304 RID: 4868 RVA: 0x000B13BC File Offset: 0x000AF5BC
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

	// Token: 0x06001305 RID: 4869 RVA: 0x000B14C8 File Offset: 0x000AF6C8
	public static void AddSeeIsland(int sea)
	{
		JArray jarray = (JArray)Tools.instance.getPlayer().EndlessSeaAvatarSeeIsland["Island"];
		if (!Tools.ContensInt(jarray, sea))
		{
			jarray.Add(sea);
		}
	}

	// Token: 0x06001306 RID: 4870 RVA: 0x000B150C File Offset: 0x000AF70C
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

	// Token: 0x06001307 RID: 4871 RVA: 0x00011F12 File Offset: 0x00010112
	public bool IsInSeeType(int shenshi, int AvatarIndex, int TargetIndex)
	{
		return this.GetInSeeIndex(shenshi, AvatarIndex).Contains(TargetIndex);
	}

	// Token: 0x06001308 RID: 4872 RVA: 0x000042DD File Offset: 0x000024DD
	public void MoveToSeaPositon(int start, int end)
	{
	}

	// Token: 0x06001309 RID: 4873 RVA: 0x00011F27 File Offset: 0x00010127
	public int getAvatarNowMapIndex()
	{
		return Tools.instance.getPlayer().fubenContorl[Tools.getScreenName()].NowIndex;
	}

	// Token: 0x0600130A RID: 4874 RVA: 0x000B165C File Offset: 0x000AF85C
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

	// Token: 0x0600130B RID: 4875 RVA: 0x00011F47 File Offset: 0x00010147
	public void RemoveAllLuXian()
	{
		this.LuXian.Clear();
		this.LuXianDian.Clear();
		this.autoRestLuXianUIID();
		this.RestLuXianDianSprite();
	}

	// Token: 0x0600130C RID: 4876 RVA: 0x000B16F4 File Offset: 0x000AF8F4
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

	// Token: 0x0600130D RID: 4877 RVA: 0x00011F6B File Offset: 0x0001016B
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

	// Token: 0x0600130E RID: 4878 RVA: 0x000B18A0 File Offset: 0x000AFAA0
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
				EndlessSeaMag.<>c__DisplayClass45_0 CS$<>8__locals1 = new EndlessSeaMag.<>c__DisplayClass45_0();
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

	// Token: 0x0600130F RID: 4879 RVA: 0x000B1980 File Offset: 0x000AFB80
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

	// Token: 0x06001310 RID: 4880 RVA: 0x000B1BE8 File Offset: 0x000AFDE8
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

	// Token: 0x06001311 RID: 4881 RVA: 0x00011F98 File Offset: 0x00010198
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

	// Token: 0x06001312 RID: 4882 RVA: 0x000B1C68 File Offset: 0x000AFE68
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

	// Token: 0x06001313 RID: 4883 RVA: 0x000B1CE4 File Offset: 0x000AFEE4
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

	// Token: 0x06001314 RID: 4884 RVA: 0x000B1D80 File Offset: 0x000AFF80
	public void RemoveLuXianDian(int index)
	{
		int index2 = this.LuXian.IndexOf(index);
		this.LuXian.RemoveAt(index2);
		this.ResetLuXianDian();
		this.autoRestLuXianUIID();
	}

	// Token: 0x06001315 RID: 4885 RVA: 0x000B1DB4 File Offset: 0x000AFFB4
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

	// Token: 0x06001316 RID: 4886 RVA: 0x000B1E98 File Offset: 0x000B0098
	public List<int> GetRoadXian(int startIndex, int endIndex)
	{
		List<int> list = new List<int>();
		this.GetIndexList(startIndex, endIndex, list);
		return list;
	}

	// Token: 0x06001317 RID: 4887 RVA: 0x000B1EB8 File Offset: 0x000B00B8
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

	// Token: 0x06001318 RID: 4888 RVA: 0x000B1F08 File Offset: 0x000B0108
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

	// Token: 0x06001319 RID: 4889 RVA: 0x000B1F68 File Offset: 0x000B0168
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

	// Token: 0x0600131A RID: 4890 RVA: 0x00011FB5 File Offset: 0x000101B5
	private void OnDestroy()
	{
		EndlessSeaMag.Inst = null;
	}

	// Token: 0x0600131B RID: 4891 RVA: 0x000042DD File Offset: 0x000024DD
	public void RemoveIndex()
	{
	}

	// Token: 0x0600131C RID: 4892 RVA: 0x000B2124 File Offset: 0x000B0324
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

	// Token: 0x0600131D RID: 4893 RVA: 0x000B22AC File Offset: 0x000B04AC
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

	// Token: 0x0600131E RID: 4894 RVA: 0x000B2360 File Offset: 0x000B0560
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

	// Token: 0x0600131F RID: 4895 RVA: 0x000B2408 File Offset: 0x000B0608
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

	// Token: 0x06001320 RID: 4896 RVA: 0x000B2484 File Offset: 0x000B0684
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

	// Token: 0x04000ED4 RID: 3796
	public static EndlessSeaMag Inst;

	// Token: 0x04000ED5 RID: 3797
	public static int MapWide = 133;

	// Token: 0x04000ED6 RID: 3798
	public static bool StopMove = false;

	// Token: 0x04000ED7 RID: 3799
	[HideInInspector]
	public List<int> LuXian = new List<int>();

	// Token: 0x04000ED8 RID: 3800
	private List<List<int>> LuXianDian = new List<List<int>>();

	// Token: 0x04000ED9 RID: 3801
	public GameObject mapNodeUI;

	// Token: 0x04000EDA RID: 3802
	public GameObject LuXianUI;

	// Token: 0x04000EDB RID: 3803
	public GameObject LineUIBase;

	// Token: 0x04000EDC RID: 3804
	public GameObject MonstarObject;

	// Token: 0x04000EDD RID: 3805
	[HideInInspector]
	public List<SeaAvatarObjBase> MonstarList = new List<SeaAvatarObjBase>();

	// Token: 0x04000EDE RID: 3806
	public SeaGrid seaGrid;

	// Token: 0x04000EDF RID: 3807
	[HideInInspector]
	public List<SeaAvatarObjBase> RoundEventList = new List<SeaAvatarObjBase>();

	// Token: 0x04000EE0 RID: 3808
	public bool flagMonstarTarget = true;

	// Token: 0x04000EE1 RID: 3809
	public Dictionary<int, ExternalBehaviorTree> externalBehaviorTrees;

	// Token: 0x04000EE2 RID: 3810
	[HideInInspector]
	public GameObject LangHua;

	// Token: 0x04000EE3 RID: 3811
	[HideInInspector]
	public EndlessFengBao fengBao;

	// Token: 0x04000EE4 RID: 3812
	private GameObject FengBaoObjList;

	// Token: 0x04000EE5 RID: 3813
	public Dictionary<int, int> oldFengBaoindex = new Dictionary<int, int>();

	// Token: 0x04000EE6 RID: 3814
	private Dictionary<string, SkeletonDataAsset> skeletonmonstar = new Dictionary<string, SkeletonDataAsset>();

	// Token: 0x04000EE7 RID: 3815
	private SeaTargetUI targetUI;

	// Token: 0x04000EE8 RID: 3816
	public SeaAvatarObjBase.Directon PlayerDirecton = SeaAvatarObjBase.Directon.Left;
}
