using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020004A8 RID: 1192
public class MainUIAvatarCell : MonoBehaviour
{
	// Token: 0x06001FA9 RID: 8105 RVA: 0x0010FF50 File Offset: 0x0010E150
	public void Init(int index, bool isHasAvatar, string name = "", int level = 0, int saveIndex = 0)
	{
		this.index = index;
		this.isHasAvatar = isHasAvatar;
		if (isHasAvatar)
		{
			this.playerFace.SetSelectFace(index, saveIndex);
			this.avatarName.text = name;
			this.avatarLevel.text = jsonData.instance.LevelUpDataJsonData[level.ToString()]["Name"].Str;
			this.avatarLevelImage.sprite = ResManager.inst.LoadSprite(string.Format("NewUI/Fight/LevelIcon/icon_{0}", level));
			this.hasAvatar.SetActive(true);
			this.noAvatar.SetActive(false);
			return;
		}
		this.noAvatar.SetActive(true);
		this.hasAvatar.SetActive(false);
	}

	// Token: 0x06001FAA RID: 8106 RVA: 0x00110014 File Offset: 0x0010E214
	public void Click()
	{
		if (this.isHasAvatar)
		{
			MainUIMag.inst.selectAvatarPanel.loadPlayerData.Init(this.index);
			return;
		}
		MainUIMag.inst.createAvatarPanel.curIndex = this.index;
		MainUIMag.inst.selectAvatarPanel.OpenCreateAvatarPanel();
	}

	// Token: 0x06001FAB RID: 8107 RVA: 0x0001A17B File Offset: 0x0001837B
	public void Delete()
	{
		if (this.isHasAvatar)
		{
			TySelect.inst.Show("确定要删除此存档吗?", delegate
			{
				try
				{
					this.isHasAvatar = false;
					this.noAvatar.SetActive(true);
					this.hasAvatar.SetActive(false);
					for (int i = 0; i < 6; i++)
					{
						foreach (string text in MainUIAvatarCell.saveFileNames)
						{
							if (File.Exists(string.Concat(new string[]
							{
								Paths.GetSavePath(),
								"/",
								text,
								Tools.instance.getSaveID(this.index, i),
								".sav"
							})))
							{
								File.Delete(string.Concat(new string[]
								{
									Paths.GetSavePath(),
									"/",
									text,
									Tools.instance.getSaveID(this.index, i),
									".sav"
								}));
							}
						}
					}
					if (File.Exists(string.Concat(new object[]
					{
						Paths.GetSavePath(),
						"/PlayerAvatarName",
						this.index,
						".sav"
					})))
					{
						File.Delete(string.Concat(new object[]
						{
							Paths.GetSavePath(),
							"/PlayerAvatarName",
							this.index,
							".sav"
						}));
					}
					if (File.Exists(string.Concat(new object[]
					{
						Paths.GetSavePath(),
						"/SaveAvatar",
						this.index,
						".sav"
					})))
					{
						File.Delete(string.Concat(new object[]
						{
							Paths.GetSavePath(),
							"/SaveAvatar",
							this.index,
							".sav"
						}));
					}
				}
				catch (Exception ex)
				{
					string text2 = string.Format("删除存档{0}异常：\n{1}", this.index, ex.Message);
					Debug.LogError(text2);
					UCheckBox.Show(text2, null);
				}
			}, null, true);
		}
	}

	// Token: 0x04001B0F RID: 6927
	public Text avatarName;

	// Token: 0x04001B10 RID: 6928
	public PlayerSetRandomFace playerFace;

	// Token: 0x04001B11 RID: 6929
	public Text avatarLevel;

	// Token: 0x04001B12 RID: 6930
	public Image avatarLevelImage;

	// Token: 0x04001B13 RID: 6931
	public GameObject hasAvatar;

	// Token: 0x04001B14 RID: 6932
	public GameObject noAvatar;

	// Token: 0x04001B15 RID: 6933
	public int index;

	// Token: 0x04001B16 RID: 6934
	public bool isHasAvatar;

	// Token: 0x04001B17 RID: 6935
	public static string[] saveFileNames = new string[]
	{
		"Avatar",
		"AvatarBackpackJsonData",
		"AvatarInfo",
		"AvatarRandomJsonData",
		"AvatarSavetime",
		"DeathNpcJsonData",
		"FirstSetAvatarRandomJsonData",
		"GameVersion",
		"JieSuanData",
		"NpcBackpack",
		"NpcJsonData",
		"OnlyChengHao",
		"SaveAvatar",
		"SaveDFAvatar",
		"EmailDataMag",
		"StreamData",
		"IsComplete"
	};
}
