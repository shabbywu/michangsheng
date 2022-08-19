using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000337 RID: 823
public class MainUIAvatarCell : MonoBehaviour
{
	// Token: 0x06001C5B RID: 7259 RVA: 0x000CB1E4 File Offset: 0x000C93E4
	public void Init(int index, bool isHasAvatar, string name = "", int level = 0, int saveIndex = 0, JSONObject face = null)
	{
		this.index = index;
		this.isHasAvatar = isHasAvatar;
		if (isHasAvatar)
		{
			this.playerFace.NewSetSelectFace(index, saveIndex, face);
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

	// Token: 0x06001C5C RID: 7260 RVA: 0x000CB2A8 File Offset: 0x000C94A8
	public void Click()
	{
		if (this.isHasAvatar)
		{
			MainUIMag.inst.selectAvatarPanel.loadPlayerData.Init(this.index);
			return;
		}
		MainUIMag.inst.createAvatarPanel.curIndex = this.index;
		MainUIMag.inst.selectAvatarPanel.OpenCreateAvatarPanel();
		YSNewSaveSystem.NowUsingAvatarIndex = this.index;
		YSNewSaveSystem.NowUsingSlot = 0;
		YSNewSaveSystem.NowAvatarPathPre = (YSNewSaveSystem.GetAvatarSavePathPre(this.index, 0) ?? "");
	}

	// Token: 0x06001C5D RID: 7261 RVA: 0x000CB327 File Offset: 0x000C9527
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
					this.DeleteOldSave();
					this.DeleteNewSave();
				}
				catch (Exception ex)
				{
					string text = string.Format("删除存档{0}异常：\n{1}", this.index, ex.Message);
					Debug.LogError(text);
					UCheckBox.Show(text, null);
				}
			}, null, true);
		}
	}

	// Token: 0x06001C5E RID: 7262 RVA: 0x000CB350 File Offset: 0x000C9550
	private void DeleteOldSave()
	{
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

	// Token: 0x06001C5F RID: 7263 RVA: 0x000CB4EB File Offset: 0x000C96EB
	private void DeleteNewSave()
	{
		YSNewSaveSystem.DeleteSave(this.index);
	}

	// Token: 0x040016D3 RID: 5843
	public Text avatarName;

	// Token: 0x040016D4 RID: 5844
	public PlayerSetRandomFace playerFace;

	// Token: 0x040016D5 RID: 5845
	public Text avatarLevel;

	// Token: 0x040016D6 RID: 5846
	public Image avatarLevelImage;

	// Token: 0x040016D7 RID: 5847
	public GameObject hasAvatar;

	// Token: 0x040016D8 RID: 5848
	public GameObject noAvatar;

	// Token: 0x040016D9 RID: 5849
	public int index;

	// Token: 0x040016DA RID: 5850
	public bool isHasAvatar;

	// Token: 0x040016DB RID: 5851
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
