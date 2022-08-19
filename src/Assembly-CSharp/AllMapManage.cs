using System;
using System.Collections.Generic;
using JSONClass;
using KBEngine;
using UltimateSurvival;
using UnityEngine;
using YSGame;

// Token: 0x0200017C RID: 380
public class AllMapManage : MonoBehaviour
{
	// Token: 0x06001039 RID: 4153 RVA: 0x0005F6F8 File Offset: 0x0005D8F8
	private void Awake()
	{
		AllMapManage.instance = this;
		GameObject gameObject = Object.Instantiate<GameObject>(Resources.Load<GameObject>("MapPlayer"));
		this.MapPlayerController = gameObject.GetComponent<MapPlayerController>();
		if (SceneEx.NowSceneName == "AllMaps")
		{
			PlayerEx.Player.AllMapSetNode();
			PlayerEx.Player.nomelTaskMag.restAllTaskType();
		}
	}

	// Token: 0x0600103A RID: 4154 RVA: 0x0005F751 File Offset: 0x0005D951
	private void Start()
	{
		this.backToLastInFuBenScene.SetTryer(new TryerDelegate(this.backFuBen));
		base.Invoke("RefreshLuDian", 0.1f);
	}

	// Token: 0x0600103B RID: 4155 RVA: 0x0005F77C File Offset: 0x0005D97C
	public void RefreshLuDian()
	{
		Avatar player = Tools.instance.getPlayer();
		float shenShiArea = player.GetShenShiArea();
		List<int> list = new List<int>();
		foreach (KeyValuePair<int, BaseMapCompont> keyValuePair in this.mapIndex)
		{
			MapComponent mapComponent = keyValuePair.Value as MapComponent;
			if (mapComponent == null)
			{
				return;
			}
			if (AllMapLuDainType.DataDict.ContainsKey(mapComponent.NodeIndex) && AllMapLuDainType.DataDict[mapComponent.NodeIndex].MapType == 1 && mapComponent.NodeGroup != 0)
			{
				float num = Vector3.Distance(MapPlayerController.Inst.transform.position, mapComponent.transform.position);
				bool flag = false;
				if (this.mapIndex.ContainsKey(player.NowMapIndex))
				{
					MapComponent mapComponent2 = (MapComponent)this.mapIndex[player.NowMapIndex];
					if (mapComponent.NodeGroup == mapComponent2.NodeGroup || num <= shenShiArea)
					{
						flag = true;
						list.Add(mapComponent.NodeGroup);
						mapComponent.gameObject.SetActive(true);
					}
				}
				if (!flag && mapComponent.gameObject.activeSelf)
				{
					mapComponent.gameObject.SetActive(false);
					iTween.FadeTo(mapComponent.gameObject, 0f, 1f);
				}
			}
		}
		if (this.LuXianGroup != null)
		{
			foreach (AllMapsLuXian allMapsLuXian in this.LuXianGroup.GetComponentsInChildren<AllMapsLuXian>(true))
			{
				bool flag2 = false;
				if (this.mapIndex.ContainsKey(player.NowMapIndex))
				{
					MapComponent mapComponent3 = (MapComponent)this.mapIndex[player.NowMapIndex];
					if (allMapsLuXian.NodeGroup == mapComponent3.NodeGroup || list.Contains(allMapsLuXian.NodeGroup))
					{
						flag2 = true;
						allMapsLuXian.gameObject.SetActive(true);
					}
				}
				if (!flag2 && allMapsLuXian.gameObject.activeSelf)
				{
					allMapsLuXian.gameObject.SetActive(false);
					iTween.FadeTo(allMapsLuXian.gameObject, 0f, 1f);
				}
			}
		}
	}

	// Token: 0x0600103C RID: 4156 RVA: 0x0005F9D8 File Offset: 0x0005DBD8
	public bool backFuBen()
	{
		bool result = true;
		if (this.canLoad)
		{
			Tools.instance.getPlayer().fubenContorl.outFuBen(true);
			this.canLoad = false;
		}
		return result;
	}

	// Token: 0x0600103D RID: 4157 RVA: 0x0005F9FF File Offset: 0x0005DBFF
	private void OnDestroy()
	{
		AllMapManage.instance = null;
		YSFuncList.Ints.Clear();
	}

	// Token: 0x04000BC7 RID: 3015
	public static AllMapManage instance;

	// Token: 0x04000BC8 RID: 3016
	public Dictionary<int, BaseMapCompont> mapIndex = new Dictionary<int, BaseMapCompont>();

	// Token: 0x04000BC9 RID: 3017
	[HideInInspector]
	public MapPlayerController MapPlayerController;

	// Token: 0x04000BCA RID: 3018
	public GameObject TaskFlag;

	// Token: 0x04000BCB RID: 3019
	public Attempt backToLastInFuBenScene = new Attempt();

	// Token: 0x04000BCC RID: 3020
	public bool canLoad = true;

	// Token: 0x04000BCD RID: 3021
	public bool isPlayMove;

	// Token: 0x04000BCE RID: 3022
	public GameObject LuXianGroup;

	// Token: 0x04000BCF RID: 3023
	public GameObject AllNodeGameobjGroup;

	// Token: 0x04000BD0 RID: 3024
	public Dictionary<int, int> RandomFlag = new Dictionary<int, int>();
}
