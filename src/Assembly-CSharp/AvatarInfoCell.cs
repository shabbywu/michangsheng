using System;
using KBEngine;
using UnityEngine;
using YSGame;

// Token: 0x02000180 RID: 384
public class AvatarInfoCell : MonoBehaviour
{
	// Token: 0x0600104C RID: 4172 RVA: 0x0005FB7B File Offset: 0x0005DD7B
	private void Start()
	{
		AvatarInfoCell.inst = this;
	}

	// Token: 0x0600104D RID: 4173 RVA: 0x0005FB7B File Offset: 0x0005DD7B
	private void Awake()
	{
		AvatarInfoCell.inst = this;
	}

	// Token: 0x0600104E RID: 4174 RVA: 0x0005FB84 File Offset: 0x0005DD84
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

	// Token: 0x0600104F RID: 4175 RVA: 0x0005FC5C File Offset: 0x0005DE5C
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

	// Token: 0x06001050 RID: 4176 RVA: 0x0005FCBC File Offset: 0x0005DEBC
	public void saveGame()
	{
		int @int = PlayerPrefs.GetInt("NowPlayerFileAvatar");
		Tools.instance.playerSaveGame(@int, this.index, null);
	}

	// Token: 0x04000BD6 RID: 3030
	public AvatarInfoCell.FileType fileType;

	// Token: 0x04000BD7 RID: 3031
	public int index;

	// Token: 0x04000BD8 RID: 3032
	public static AvatarInfoCell inst;

	// Token: 0x0200129C RID: 4764
	public enum FileType
	{
		// Token: 0x04006625 RID: 26149
		SAVE,
		// Token: 0x04006626 RID: 26150
		LOAD
	}
}
