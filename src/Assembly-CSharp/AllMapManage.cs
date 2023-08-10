using System.Collections.Generic;
using JSONClass;
using KBEngine;
using UltimateSurvival;
using UnityEngine;
using YSGame;

public class AllMapManage : MonoBehaviour
{
	public static AllMapManage instance;

	public Dictionary<int, BaseMapCompont> mapIndex = new Dictionary<int, BaseMapCompont>();

	[HideInInspector]
	public MapPlayerController MapPlayerController;

	public GameObject TaskFlag;

	public Attempt backToLastInFuBenScene = new Attempt();

	public bool canLoad = true;

	public bool isPlayMove;

	public GameObject LuXianGroup;

	public GameObject AllNodeGameobjGroup;

	public Dictionary<int, int> RandomFlag = new Dictionary<int, int>();

	private void Awake()
	{
		instance = this;
		GameObject val = Object.Instantiate<GameObject>(Resources.Load<GameObject>("MapPlayer"));
		MapPlayerController = val.GetComponent<MapPlayerController>();
		if (SceneEx.NowSceneName == "AllMaps")
		{
			PlayerEx.Player.AllMapSetNode();
			PlayerEx.Player.nomelTaskMag.restAllTaskType();
		}
	}

	private void Start()
	{
		backToLastInFuBenScene.SetTryer(backFuBen);
		((MonoBehaviour)this).Invoke("RefreshLuDian", 0.1f);
	}

	public void RefreshLuDian()
	{
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		Avatar player = Tools.instance.getPlayer();
		float shenShiArea = player.GetShenShiArea();
		List<int> list = new List<int>();
		foreach (KeyValuePair<int, BaseMapCompont> item in mapIndex)
		{
			MapComponent mapComponent = item.Value as MapComponent;
			if ((Object)(object)mapComponent == (Object)null)
			{
				return;
			}
			if (!AllMapLuDainType.DataDict.ContainsKey(mapComponent.NodeIndex) || AllMapLuDainType.DataDict[mapComponent.NodeIndex].MapType != 1 || mapComponent.NodeGroup == 0)
			{
				continue;
			}
			float num = Vector3.Distance(((Component)MapPlayerController.Inst).transform.position, ((Component)mapComponent).transform.position);
			bool flag = false;
			if (mapIndex.ContainsKey(player.NowMapIndex))
			{
				MapComponent mapComponent2 = (MapComponent)mapIndex[player.NowMapIndex];
				if (mapComponent.NodeGroup == mapComponent2.NodeGroup || num <= shenShiArea)
				{
					flag = true;
					list.Add(mapComponent.NodeGroup);
					((Component)mapComponent).gameObject.SetActive(true);
				}
			}
			if (!flag && ((Component)mapComponent).gameObject.activeSelf)
			{
				((Component)mapComponent).gameObject.SetActive(false);
				iTween.FadeTo(((Component)mapComponent).gameObject, 0f, 1f);
			}
		}
		if (!((Object)(object)LuXianGroup != (Object)null))
		{
			return;
		}
		AllMapsLuXian[] componentsInChildren = LuXianGroup.GetComponentsInChildren<AllMapsLuXian>(true);
		foreach (AllMapsLuXian allMapsLuXian in componentsInChildren)
		{
			bool flag2 = false;
			if (mapIndex.ContainsKey(player.NowMapIndex))
			{
				MapComponent mapComponent3 = (MapComponent)mapIndex[player.NowMapIndex];
				if (allMapsLuXian.NodeGroup == mapComponent3.NodeGroup || list.Contains(allMapsLuXian.NodeGroup))
				{
					flag2 = true;
					((Component)allMapsLuXian).gameObject.SetActive(true);
				}
			}
			if (!flag2 && ((Component)allMapsLuXian).gameObject.activeSelf)
			{
				((Component)allMapsLuXian).gameObject.SetActive(false);
				iTween.FadeTo(((Component)allMapsLuXian).gameObject, 0f, 1f);
			}
		}
	}

	public bool backFuBen()
	{
		if (canLoad)
		{
			Tools.instance.getPlayer().fubenContorl.outFuBen();
			canLoad = false;
		}
		return true;
	}

	private void OnDestroy()
	{
		instance = null;
		YSFuncList.Ints.Clear();
	}
}
