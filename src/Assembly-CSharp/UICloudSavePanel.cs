using System;
using Steamworks;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200036E RID: 878
public class UICloudSavePanel : MonoBehaviour
{
	// Token: 0x06001D6A RID: 7530 RVA: 0x000D00A4 File Offset: 0x000CE2A4
	public void OpenPanel()
	{
		base.gameObject.SetActive(true);
		try
		{
			if (SteamAPI.IsSteamRunning())
			{
				if (SteamRemoteStorage.IsCloudEnabledForApp() && SteamRemoteStorage.IsCloudEnabledForAccount())
				{
					this.RefreshCloudSave();
				}
				else
				{
					this.TipText.text = "未启用Steam云，请先在Steam中启用云存档功能。";
					this.TipText.gameObject.SetActive(true);
				}
			}
			else
			{
				this.TipText.text = "Steam未运行，请从Steam启动游戏。";
				this.TipText.gameObject.SetActive(true);
			}
		}
		catch (Exception arg)
		{
			this.TipText.text = string.Format("Steam异常，请检查Steam状态：\n{0}", arg);
			this.TipText.gameObject.SetActive(true);
		}
	}

	// Token: 0x06001D6B RID: 7531 RVA: 0x000B5E62 File Offset: 0x000B4062
	public void Close()
	{
		base.gameObject.SetActive(false);
	}

	// Token: 0x06001D6C RID: 7532 RVA: 0x000D015C File Offset: 0x000CE35C
	public void RefreshCloudSave()
	{
		this.TipText.gameObject.SetActive(false);
		this.SlotRoot.DestoryAllChild();
		try
		{
			foreach (CloudSaveFileData data in YSNewSaveSystem.GetCloudSaveData())
			{
				UICloudSaveSlot component = Object.Instantiate<GameObject>(this.SaveSlotPrefab, this.SlotRoot).GetComponent<UICloudSaveSlot>();
				component.SetData(data);
				component.UICloudSavePanel = this;
			}
		}
		catch (Exception arg)
		{
			this.SlotRoot.DestoryAllChild();
			this.TipText.text = string.Format("获取云存档时出现异常:{0}", arg);
			this.TipText.gameObject.SetActive(true);
		}
	}

	// Token: 0x04001802 RID: 6146
	public Text TipText;

	// Token: 0x04001803 RID: 6147
	public GameObject SaveSlotPrefab;

	// Token: 0x04001804 RID: 6148
	public RectTransform SlotRoot;
}
