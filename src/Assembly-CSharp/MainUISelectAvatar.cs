using System;
using UnityEngine;
using YSGame;

// Token: 0x020004A9 RID: 1193
public class MainUISelectAvatar : MonoBehaviour, IESCClose
{
	// Token: 0x06001FAF RID: 8111 RVA: 0x00110318 File Offset: 0x0010E518
	public void Init()
	{
		if (!this.isInit)
		{
			int i = 0;
			while (i < this.maxNum)
			{
				GameObject gameObject = Object.Instantiate<GameObject>(this.avatarTemp, this.avatarList);
				MainUIAvatarCell component = gameObject.GetComponent<MainUIAvatarCell>();
				try
				{
					bool flag = false;
					for (int j = 0; j < 6; j++)
					{
						if (YSSaveGame.HasFile(Paths.GetSavePath(), "AvatarInfo" + Tools.instance.getSaveID(i, j)))
						{
							try
							{
								JSONObject jsonObject = YSSaveGame.GetJsonObject("AvatarInfo" + Tools.instance.getSaveID(i, j), null);
								component.Init(i, true, jsonObject["firstName"].Str + jsonObject["lastName"].Str, jsonObject["avatarLevel"].I, j);
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
						component.Init(i, false, "", 0, 0);
					}
				}
				catch (Exception)
				{
					component.Init(i, false, "", 0, 0);
					goto IL_FB;
				}
				goto IL_F4;
				IL_FB:
				i++;
				continue;
				IL_F4:
				gameObject.SetActive(true);
				goto IL_FB;
			}
			this.isInit = true;
		}
		else
		{
			base.gameObject.SetActive(true);
		}
		if (this.loadPlayerData.gameObject.activeSelf)
		{
			this.loadPlayerData.gameObject.SetActive(false);
		}
		ESCCloseManager.Inst.RegisterClose(this);
	}

	// Token: 0x06001FB0 RID: 8112 RVA: 0x00110490 File Offset: 0x0010E690
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

	// Token: 0x06001FB1 RID: 8113 RVA: 0x0001A1A2 File Offset: 0x000183A2
	public void OpenCreateAvatarPanel()
	{
		base.gameObject.SetActive(false);
		MainUIMag.inst.createAvatarPanel.Init();
	}

	// Token: 0x06001FB2 RID: 8114 RVA: 0x0001A1BF File Offset: 0x000183BF
	public bool TryEscClose()
	{
		this.ReturnMainPanel();
		return true;
	}

	// Token: 0x04001B18 RID: 6936
	private bool isInit;

	// Token: 0x04001B19 RID: 6937
	public int maxNum;

	// Token: 0x04001B1A RID: 6938
	public GameObject avatarTemp;

	// Token: 0x04001B1B RID: 6939
	public Transform avatarList;

	// Token: 0x04001B1C RID: 6940
	public MainUILoadData loadPlayerData;
}
