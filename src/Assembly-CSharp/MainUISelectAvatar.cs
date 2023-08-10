using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using YSGame;

public class MainUISelectAvatar : MonoBehaviour, IESCClose
{
	private bool isInit;

	public int maxNum;

	public GameObject avatarTemp;

	public Transform avatarList;

	public MainUILoadData loadPlayerData;

	private List<GameObject> cellList = new List<GameObject>();

	public void Init()
	{
		if (!isInit)
		{
			ESCCloseManager.Inst.RegisterClose(this);
			isInit = true;
		}
		if (((Component)loadPlayerData).gameObject.activeSelf)
		{
			((Component)loadPlayerData).gameObject.SetActive(false);
		}
		((Component)this).gameObject.SetActive(true);
	}

	public void RefreshSaveSlot()
	{
		for (int num = cellList.Count - 1; num >= 0; num--)
		{
			Object.Destroy((Object)(object)cellList[num]);
		}
		cellList.Clear();
		for (int i = 0; i < maxNum; i++)
		{
			GameObject val = Object.Instantiate<GameObject>(avatarTemp, avatarList);
			cellList.Add(val);
			MainUIAvatarCell component = val.GetComponent<MainUIAvatarCell>();
			try
			{
				Stopwatch stopwatch = new Stopwatch();
				stopwatch.Start();
				bool flag = false;
				for (int j = 0; j < 6; j++)
				{
					if (!YSNewSaveSystem.HasFile(Paths.GetNewSavePath() + "/" + YSNewSaveSystem.GetAvatarSavePathPre(i, j) + "/AvatarInfo.json"))
					{
						continue;
					}
					try
					{
						JSONObject jsonObject = YSNewSaveSystem.GetJsonObject(YSNewSaveSystem.GetAvatarSavePathPre(i, j) + "/AvatarInfo.json");
						JSONObject face = null;
						if (jsonObject.HasField("face"))
						{
							face = jsonObject["face"];
						}
						component.Init(i, isHasAvatar: true, jsonObject["firstName"].Str + jsonObject["lastName"].Str, jsonObject["avatarLevel"].I, j, face);
						flag = true;
					}
					catch
					{
						continue;
					}
					break;
				}
				if (!flag)
				{
					for (int k = 0; k < 6; k++)
					{
						if (YSSaveGame.HasFile(Paths.GetSavePath(), "AvatarInfo" + Tools.instance.getSaveID(i, k)))
						{
							try
							{
								JSONObject jsonObject2 = YSSaveGame.GetJsonObject("AvatarInfo" + Tools.instance.getSaveID(i, k));
								component.Init(i, isHasAvatar: true, jsonObject2["firstName"].Str + jsonObject2["lastName"].Str, jsonObject2["avatarLevel"].I, k);
								flag = true;
							}
							catch
							{
								continue;
							}
							break;
						}
					}
				}
				if (!flag)
				{
					component.Init(i, isHasAvatar: false);
				}
				stopwatch.Stop();
				Debug.Log((object)$"刷新存档信息 {i} 耗时{stopwatch.ElapsedMilliseconds}ms，是否存在角色:{flag}");
			}
			catch (Exception)
			{
				component.Init(i, isHasAvatar: false);
				continue;
			}
			val.SetActive(true);
		}
	}

	public void ReturnMainPanel()
	{
		if (((Component)loadPlayerData).gameObject.activeSelf)
		{
			((Component)loadPlayerData).gameObject.SetActive(false);
			return;
		}
		((Component)this).gameObject.SetActive(false);
		MainUIMag.inst.mainPanel.SetActive(true);
		ESCCloseManager.Inst.UnRegisterClose(this);
	}

	public void OpenCreateAvatarPanel()
	{
		((Component)this).gameObject.SetActive(false);
		MainUIMag.inst.createAvatarPanel.Init();
	}

	public bool TryEscClose()
	{
		ReturnMainPanel();
		return true;
	}
}
