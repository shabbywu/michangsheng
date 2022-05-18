using System;
using System.Collections.Generic;
using JSONClass;
using KBEngine;
using UltimateSurvival;
using UnityEngine;
using YSGame;

// Token: 0x02000259 RID: 601
public class AllMapManage : MonoBehaviour
{
	// Token: 0x0600128B RID: 4747 RVA: 0x000AE888 File Offset: 0x000ACA88
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

	// Token: 0x0600128C RID: 4748 RVA: 0x000119FD File Offset: 0x0000FBFD
	private void Start()
	{
		this.backToLastInFuBenScene.SetTryer(new TryerDelegate(this.backFuBen));
		base.Invoke("RefreshLuDian", 0.1f);
	}

	// Token: 0x0600128D RID: 4749 RVA: 0x000AE8E4 File Offset: 0x000ACAE4
	public void RefreshLuDian()
	{
		Avatar player = Tools.instance.getPlayer();
		foreach (KeyValuePair<int, BaseMapCompont> keyValuePair in this.mapIndex)
		{
			MapComponent mapComponent = keyValuePair.Value as MapComponent;
			if (mapComponent == null)
			{
				return;
			}
			if (AllMapLuDainType.DataDict.ContainsKey(mapComponent.NodeIndex) && AllMapLuDainType.DataDict[mapComponent.NodeIndex].MapType == 1 && mapComponent.NodeGroup != 0)
			{
				bool flag = false;
				if (this.mapIndex.ContainsKey(player.NowMapIndex))
				{
					MapComponent mapComponent2 = (MapComponent)this.mapIndex[player.NowMapIndex];
					if (mapComponent.NodeGroup == mapComponent2.NodeGroup)
					{
						flag = true;
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
					if (allMapsLuXian.NodeGroup == mapComponent3.NodeGroup)
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

	// Token: 0x0600128E RID: 4750 RVA: 0x00011A26 File Offset: 0x0000FC26
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

	// Token: 0x0600128F RID: 4751 RVA: 0x00011A4D File Offset: 0x0000FC4D
	private void OnDestroy()
	{
		AllMapManage.instance = null;
		YSFuncList.Ints.Clear();
	}

	// Token: 0x04000E99 RID: 3737
	public static AllMapManage instance;

	// Token: 0x04000E9A RID: 3738
	public Dictionary<int, BaseMapCompont> mapIndex = new Dictionary<int, BaseMapCompont>();

	// Token: 0x04000E9B RID: 3739
	[HideInInspector]
	public MapPlayerController MapPlayerController;

	// Token: 0x04000E9C RID: 3740
	public GameObject TaskFlag;

	// Token: 0x04000E9D RID: 3741
	public Attempt backToLastInFuBenScene = new Attempt();

	// Token: 0x04000E9E RID: 3742
	public bool canLoad = true;

	// Token: 0x04000E9F RID: 3743
	public bool isPlayMove;

	// Token: 0x04000EA0 RID: 3744
	public GameObject LuXianGroup;

	// Token: 0x04000EA1 RID: 3745
	public GameObject AllNodeGameobjGroup;

	// Token: 0x04000EA2 RID: 3746
	public Dictionary<int, int> RandomFlag = new Dictionary<int, int>();
}
