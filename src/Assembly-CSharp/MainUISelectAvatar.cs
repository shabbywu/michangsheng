using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using YSGame;

// Token: 0x02000338 RID: 824
public class MainUISelectAvatar : MonoBehaviour, IESCClose
{
	// Token: 0x06001C63 RID: 7267 RVA: 0x000CB618 File Offset: 0x000C9818
	public void Init()
	{
		if (!this.isInit)
		{
			ESCCloseManager.Inst.RegisterClose(this);
			this.isInit = true;
		}
		if (this.loadPlayerData.gameObject.activeSelf)
		{
			this.loadPlayerData.gameObject.SetActive(false);
		}
		base.gameObject.SetActive(true);
	}

	// Token: 0x06001C64 RID: 7268 RVA: 0x000CB670 File Offset: 0x000C9870
	public void RefreshSaveSlot()
	{
		for (int i = this.cellList.Count - 1; i >= 0; i--)
		{
			Object.Destroy(this.cellList[i]);
		}
		this.cellList.Clear();
		int j = 0;
		while (j < this.maxNum)
		{
			GameObject gameObject = Object.Instantiate<GameObject>(this.avatarTemp, this.avatarList);
			this.cellList.Add(gameObject);
			MainUIAvatarCell component = gameObject.GetComponent<MainUIAvatarCell>();
			try
			{
				Stopwatch stopwatch = new Stopwatch();
				stopwatch.Start();
				bool flag = false;
				for (int k = 0; k < 6; k++)
				{
					if (YSNewSaveSystem.HasFile(Paths.GetNewSavePath() + "/" + YSNewSaveSystem.GetAvatarSavePathPre(j, k) + "/AvatarInfo.json"))
					{
						try
						{
							JSONObject jsonObject = YSNewSaveSystem.GetJsonObject(YSNewSaveSystem.GetAvatarSavePathPre(j, k) + "/AvatarInfo.json", null);
							JSONObject face = null;
							if (jsonObject.HasField("face"))
							{
								face = jsonObject["face"];
							}
							component.Init(j, true, jsonObject["firstName"].Str + jsonObject["lastName"].Str, jsonObject["avatarLevel"].I, k, face);
							flag = true;
							break;
						}
						catch
						{
						}
					}
				}
				if (!flag)
				{
					for (int l = 0; l < 6; l++)
					{
						if (YSSaveGame.HasFile(Paths.GetSavePath(), "AvatarInfo" + Tools.instance.getSaveID(j, l)))
						{
							try
							{
								JSONObject jsonObject2 = YSSaveGame.GetJsonObject("AvatarInfo" + Tools.instance.getSaveID(j, l), null);
								component.Init(j, true, jsonObject2["firstName"].Str + jsonObject2["lastName"].Str, jsonObject2["avatarLevel"].I, l, null);
								flag = true;
								break;
							}
							catch
							{
							}
						}
					}
				}
				if (!flag)
				{
					component.Init(j, false, "", 0, 0, null);
				}
				stopwatch.Stop();
				Debug.Log(string.Format("刷新存档信息 {0} 耗时{1}ms，是否存在角色:{2}", j, stopwatch.ElapsedMilliseconds, flag));
			}
			catch (Exception)
			{
				component.Init(j, false, "", 0, 0, null);
				goto IL_23B;
			}
			goto IL_234;
			IL_23B:
			j++;
			continue;
			IL_234:
			gameObject.SetActive(true);
			goto IL_23B;
		}
	}

	// Token: 0x06001C65 RID: 7269 RVA: 0x000CB914 File Offset: 0x000C9B14
	public void ReturnMainPanel()
	{
		if (this.loadPlayerData.gameObject.activeSelf)
		{
			this.loadPlayerData.gameObject.SetActive(false);
			return;
		}
		base.gameObject.SetActive(false);
		MainUIMag.inst.mainPanel.SetActive(true);
		ESCCloseManager.Inst.UnRegisterClose(this);
	}

	// Token: 0x06001C66 RID: 7270 RVA: 0x000CB96C File Offset: 0x000C9B6C
	public void OpenCreateAvatarPanel()
	{
		base.gameObject.SetActive(false);
		MainUIMag.inst.createAvatarPanel.Init();
	}

	// Token: 0x06001C67 RID: 7271 RVA: 0x000CB989 File Offset: 0x000C9B89
	public bool TryEscClose()
	{
		this.ReturnMainPanel();
		return true;
	}

	// Token: 0x040016DC RID: 5852
	private bool isInit;

	// Token: 0x040016DD RID: 5853
	public int maxNum;

	// Token: 0x040016DE RID: 5854
	public GameObject avatarTemp;

	// Token: 0x040016DF RID: 5855
	public Transform avatarList;

	// Token: 0x040016E0 RID: 5856
	public MainUILoadData loadPlayerData;

	// Token: 0x040016E1 RID: 5857
	private List<GameObject> cellList = new List<GameObject>();
}
