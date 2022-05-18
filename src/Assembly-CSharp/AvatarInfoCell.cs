using System;
using KBEngine;
using UnityEngine;
using YSGame;

// Token: 0x0200025D RID: 605
public class AvatarInfoCell : MonoBehaviour
{
	// Token: 0x0600129E RID: 4766 RVA: 0x00011B38 File Offset: 0x0000FD38
	private void Start()
	{
		AvatarInfoCell.inst = this;
	}

	// Token: 0x0600129F RID: 4767 RVA: 0x00011B38 File Offset: 0x0000FD38
	private void Awake()
	{
		AvatarInfoCell.inst = this;
	}

	// Token: 0x060012A0 RID: 4768 RVA: 0x000AEB64 File Offset: 0x000ACD64
	public void click()
	{
		int @int = PlayerPrefs.GetInt("NowPlayerFileAvatar");
		if (this.fileType != AvatarInfoCell.FileType.SAVE)
		{
			if (this.fileType == AvatarInfoCell.FileType.LOAD && YSSaveGame.HasFile(Paths.GetSavePath(), "AvatarInfo" + Tools.instance.getSaveID(@int, this.index)))
			{
				selectBox.instence.setChoice("是否读取当前存档", new EventDelegate(delegate()
				{
					if (jsonData.instance.saveState == 1)
					{
						UIPopTip.Inst.Pop("正在存档,请存完后再读取", PopTipIconType.叹号);
						return;
					}
					if (!NpcJieSuanManager.inst.isCanJieSuan)
					{
						UIPopTip.Inst.Pop("正在结算中,请稍后读档", PopTipIconType.叹号);
						UIPopTip.Inst.Pop("如果一直提示,请向官方报备", PopTipIconType.叹号);
						return;
					}
					if (FpUIMag.inst != null)
					{
						Object.Destroy(FpUIMag.inst.gameObject);
					}
					if (TpUIMag.inst != null)
					{
						Object.Destroy(TpUIMag.inst.gameObject);
					}
					if (PanelMamager.inst.UISceneGameObject != null)
					{
						PanelMamager.inst.UISceneGameObject.SetActive(false);
					}
					this.loadGame();
				}), null);
			}
			return;
		}
		if (this.index == 0)
		{
			UIPopTip.Inst.Pop("不能覆盖自动存档", PopTipIconType.叹号);
			return;
		}
		if (YSSaveGame.GetInt("SaveAvatar" + Tools.instance.getSaveID(@int, this.index), 0) == 1)
		{
			selectBox.instence.setChoice("是否覆盖当前存档", new EventDelegate(new EventDelegate.Callback(this.saveGame)), null);
			return;
		}
		this.saveGame();
	}

	// Token: 0x060012A1 RID: 4769 RVA: 0x000AEC3C File Offset: 0x000ACE3C
	public void loadGame()
	{
		int @int = PlayerPrefs.GetInt("NowPlayerFileAvatar");
		YSSaveGame.Reset();
		KBEngineApp.app.entities[10] = null;
		KBEngineApp.app.entities.Remove(10);
		GameObject gameObject = new GameObject();
		gameObject.AddComponent<StartGame>();
		gameObject.GetComponent<StartGame>().startGame(@int, this.index, -1);
	}

	// Token: 0x060012A2 RID: 4770 RVA: 0x000AEC9C File Offset: 0x000ACE9C
	public void saveGame()
	{
		int @int = PlayerPrefs.GetInt("NowPlayerFileAvatar");
		Tools.instance.playerSaveGame(@int, this.index, null);
	}

	// Token: 0x04000EA8 RID: 3752
	public AvatarInfoCell.FileType fileType;

	// Token: 0x04000EA9 RID: 3753
	public int index;

	// Token: 0x04000EAA RID: 3754
	public static AvatarInfoCell inst;

	// Token: 0x0200025E RID: 606
	public enum FileType
	{
		// Token: 0x04000EAC RID: 3756
		SAVE,
		// Token: 0x04000EAD RID: 3757
		LOAD
	}
}
