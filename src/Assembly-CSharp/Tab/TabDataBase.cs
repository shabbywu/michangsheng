using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Tab;

public class TabDataBase : UIBase
{
	public int Id;

	public int Index;

	public readonly GameObject HasDataPanel;

	public readonly GameObject NoDataPanel;

	public readonly Image Icon;

	public readonly Text JingJie;

	public readonly Text GameTime;

	public readonly Text RealTime;

	public SaveSlotData data;

	public TabDataBase(GameObject go, int type = 0)
	{
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Expected O, but got Unknown
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Expected O, but got Unknown
		_go = go;
		HasDataPanel = Get("HasData");
		NoDataPanel = Get("NoData");
		Icon = Get<Image>("HasData/境界/Img");
		JingJie = Get<Text>("HasData/境界/Value");
		GameTime = Get<Text>("HasData/游戏时间/Value");
		RealTime = Get<Text>("HasData/现实时间/Value");
		if (type == 0)
		{
			_go.GetComponent<FpBtn>().mouseUpEvent.AddListener(new UnityAction(Save));
		}
		else
		{
			_go.GetComponent<FpBtn>().mouseUpEvent.AddListener(new UnityAction(Load));
		}
		Id = PlayerPrefs.GetInt("NowPlayerFileAvatar");
		Index = int.Parse(((Object)_go).name);
	}

	public void UpdateDate()
	{
		data = YSNewSaveSystem.GetAvatarSaveData(Id, Index);
		if (data.HasSave)
		{
			JingJie.SetText(data.AvatarLevelText);
			Icon.sprite = data.AvatarLevelSprite;
			GameTime.SetText(data.GameTime);
			RealTime.SetText(data.RealSaveTime);
			if (!data.IsNewSaveSystem)
			{
				RealTime.text += " <color=red><size=30>旧</size></color>";
			}
			NoDataPanel.SetActive(false);
			HasDataPanel.SetActive(true);
		}
		else
		{
			NoDataPanel.SetActive(true);
			HasDataPanel.SetActive(false);
		}
	}

	private void Save()
	{
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Expected O, but got Unknown
		if (Index == 0)
		{
			UIPopTip.Inst.Pop("不能覆盖自动存档", PopTipIconType.感悟);
		}
		else if (data.HasSave)
		{
			TySelect.inst.Show("是否覆盖当前存档", (UnityAction)delegate
			{
				if ((Object)(object)SingletonMono<TabUIMag>.Instance != (Object)null)
				{
					SingletonMono<TabUIMag>.Instance.TryEscClose();
				}
				YSNewSaveSystem.SaveGame(Id, Index);
			});
		}
		else
		{
			if ((Object)(object)SingletonMono<TabUIMag>.Instance != (Object)null)
			{
				SingletonMono<TabUIMag>.Instance.TryEscClose();
			}
			YSNewSaveSystem.SaveGame(Id, Index);
		}
	}

	private void Load()
	{
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Expected O, but got Unknown
		if (!data.HasSave)
		{
			return;
		}
		TySelect.inst.Show("是否读取当前存档", (UnityAction)delegate
		{
			try
			{
				if (data.IsNewSaveSystem)
				{
					YSNewSaveSystem.LoadSave(Id, Index);
				}
				else
				{
					YSNewSaveSystem.LoadOldSave(Id, Index);
				}
			}
			catch (Exception ex)
			{
				Debug.LogError((object)"读档失败");
				Debug.LogError((object)ex);
				UCheckBox.Show("存档读取失败，可能已损坏。如果订阅了模组，请检查是否有模组错误。");
			}
		}, null, isDestorySelf: false);
	}
}
