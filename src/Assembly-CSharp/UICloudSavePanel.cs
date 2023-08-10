using System;
using Steamworks;
using UnityEngine;
using UnityEngine.UI;

public class UICloudSavePanel : MonoBehaviour
{
	public Text TipText;

	public GameObject SaveSlotPrefab;

	public RectTransform SlotRoot;

	public void OpenPanel()
	{
		((Component)this).gameObject.SetActive(true);
		try
		{
			if (SteamAPI.IsSteamRunning())
			{
				if (SteamRemoteStorage.IsCloudEnabledForApp() && SteamRemoteStorage.IsCloudEnabledForAccount())
				{
					RefreshCloudSave();
					return;
				}
				TipText.text = "未启用Steam云，请先在Steam中启用云存档功能。";
				((Component)TipText).gameObject.SetActive(true);
			}
			else
			{
				TipText.text = "Steam未运行，请从Steam启动游戏。";
				((Component)TipText).gameObject.SetActive(true);
			}
		}
		catch (Exception arg)
		{
			TipText.text = $"Steam异常，请检查Steam状态：\n{arg}";
			((Component)TipText).gameObject.SetActive(true);
		}
	}

	public void Close()
	{
		((Component)this).gameObject.SetActive(false);
	}

	public void RefreshCloudSave()
	{
		((Component)TipText).gameObject.SetActive(false);
		((Transform)(object)SlotRoot).DestoryAllChild();
		try
		{
			foreach (CloudSaveFileData cloudSaveDatum in YSNewSaveSystem.GetCloudSaveData())
			{
				UICloudSaveSlot component = Object.Instantiate<GameObject>(SaveSlotPrefab, (Transform)(object)SlotRoot).GetComponent<UICloudSaveSlot>();
				component.SetData(cloudSaveDatum);
				component.UICloudSavePanel = this;
			}
		}
		catch (Exception arg)
		{
			((Transform)(object)SlotRoot).DestoryAllChild();
			TipText.text = $"获取云存档时出现异常:{arg}";
			((Component)TipText).gameObject.SetActive(true);
		}
	}
}
