using System;
using System.IO;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MainUIAvatarCell : MonoBehaviour
{
	public Text avatarName;

	public PlayerSetRandomFace playerFace;

	public Text avatarLevel;

	public Image avatarLevelImage;

	public GameObject hasAvatar;

	public GameObject noAvatar;

	public int index;

	public bool isHasAvatar;

	public static string[] saveFileNames = new string[17]
	{
		"Avatar", "AvatarBackpackJsonData", "AvatarInfo", "AvatarRandomJsonData", "AvatarSavetime", "DeathNpcJsonData", "FirstSetAvatarRandomJsonData", "GameVersion", "JieSuanData", "NpcBackpack",
		"NpcJsonData", "OnlyChengHao", "SaveAvatar", "SaveDFAvatar", "EmailDataMag", "StreamData", "IsComplete"
	};

	public void Init(int index, bool isHasAvatar, string name = "", int level = 0, int saveIndex = 0, JSONObject face = null)
	{
		this.index = index;
		this.isHasAvatar = isHasAvatar;
		if (isHasAvatar)
		{
			playerFace.NewSetSelectFace(index, saveIndex, face);
			avatarName.text = name;
			avatarLevel.text = jsonData.instance.LevelUpDataJsonData[level.ToString()]["Name"].Str;
			avatarLevelImage.sprite = ResManager.inst.LoadSprite($"NewUI/Fight/LevelIcon/icon_{level}");
			hasAvatar.SetActive(true);
			noAvatar.SetActive(false);
		}
		else
		{
			noAvatar.SetActive(true);
			hasAvatar.SetActive(false);
		}
	}

	public void Click()
	{
		if (isHasAvatar)
		{
			MainUIMag.inst.selectAvatarPanel.loadPlayerData.Init(index);
			return;
		}
		MainUIMag.inst.createAvatarPanel.curIndex = index;
		MainUIMag.inst.selectAvatarPanel.OpenCreateAvatarPanel();
		YSNewSaveSystem.NowUsingAvatarIndex = index;
		YSNewSaveSystem.NowUsingSlot = 0;
		YSNewSaveSystem.NowAvatarPathPre = YSNewSaveSystem.GetAvatarSavePathPre(index, 0) ?? "";
	}

	public void Delete()
	{
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Expected O, but got Unknown
		if (!isHasAvatar)
		{
			return;
		}
		TySelect.inst.Show("确定要删除此存档吗?", (UnityAction)delegate
		{
			try
			{
				isHasAvatar = false;
				noAvatar.SetActive(true);
				hasAvatar.SetActive(false);
				DeleteOldSave();
				DeleteNewSave();
			}
			catch (Exception ex)
			{
				string text = $"删除存档{index}异常：\n{ex.Message}";
				Debug.LogError((object)text);
				UCheckBox.Show(text);
			}
		});
	}

	private void DeleteOldSave()
	{
		for (int i = 0; i < 6; i++)
		{
			string[] array = saveFileNames;
			foreach (string text in array)
			{
				if (File.Exists(Paths.GetSavePath() + "/" + text + Tools.instance.getSaveID(index, i) + ".sav"))
				{
					File.Delete(Paths.GetSavePath() + "/" + text + Tools.instance.getSaveID(index, i) + ".sav");
				}
			}
		}
		if (File.Exists(Paths.GetSavePath() + "/PlayerAvatarName" + index + ".sav"))
		{
			File.Delete(Paths.GetSavePath() + "/PlayerAvatarName" + index + ".sav");
		}
		if (File.Exists(Paths.GetSavePath() + "/SaveAvatar" + index + ".sav"))
		{
			File.Delete(Paths.GetSavePath() + "/SaveAvatar" + index + ".sav");
		}
	}

	private void DeleteNewSave()
	{
		YSNewSaveSystem.DeleteSave(index);
	}
}
