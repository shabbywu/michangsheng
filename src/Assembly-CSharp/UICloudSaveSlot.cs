using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200036F RID: 879
public class UICloudSaveSlot : MonoBehaviour
{
	// Token: 0x06001D6E RID: 7534 RVA: 0x000D022C File Offset: 0x000CE42C
	public void SetData(CloudSaveFileData data)
	{
		this.Data = data;
		this.SlotIDText.text = string.Format("{0}", data.Slot + 1);
		if (data.HasFile)
		{
			this.HasDataObj.SetActive(true);
			this.NoDataObj.SetActive(false);
			this.UploadTimeText.text = string.Format("上传时间：{0}", data.FileTime);
			this.FileSizeText.text = string.Format("文件大小：{0}kb", data.FileSize / 1024);
			this.DescText.text = "备注：" + data.FileDesc;
			return;
		}
		this.HasDataObj.SetActive(false);
		this.NoDataObj.SetActive(true);
	}

	// Token: 0x06001D6F RID: 7535 RVA: 0x000D02FC File Offset: 0x000CE4FC
	public void OnUploadBtnClick()
	{
		if (this.Data.HasFile)
		{
			USelectBox.Show("是否覆盖此槽位?", delegate
			{
				UInputBox.Show("填写存档备注", delegate(string s)
				{
					YSNewSaveSystem.ZipAndUploadCloudSaveData(this.Data.Slot, s);
					UIPopTip.Inst.Pop("上传完毕", PopTipIconType.叹号);
					this.UICloudSavePanel.Close();
				});
			}, null);
			return;
		}
		UInputBox.Show("填写存档备注", delegate(string s)
		{
			YSNewSaveSystem.ZipAndUploadCloudSaveData(this.Data.Slot, s);
			UIPopTip.Inst.Pop("上传完毕", PopTipIconType.叹号);
			this.UICloudSavePanel.Close();
		});
	}

	// Token: 0x06001D70 RID: 7536 RVA: 0x000D0339 File Offset: 0x000CE539
	public void OnDownloadBtnClick()
	{
		USelectBox.Show("是否下载并覆盖本地存档?\n此操作会删除本地存档！", delegate
		{
			YSNewSaveSystem.DownloadCloudSave(this.Data.Slot);
			YSNewSaveSystem.DownloadCloudSaveDesc(this.Data.Slot);
			if (Directory.Exists(Paths.GetNewSavePath()))
			{
				Directory.Delete(Paths.GetNewSavePath(), true);
			}
			YSZip.UnZipFile(string.Format("{0}/CloudSave_{1}.zip", Paths.GetCloudSavePath(), this.Data.Slot), Paths.GetNewSavePath() ?? "");
			this.UICloudSavePanel.RefreshCloudSave();
			MainUIMag.inst.RefreshSave();
			UIPopTip.Inst.Pop("云存档下载完成", PopTipIconType.叹号);
		}, null);
	}

	// Token: 0x06001D71 RID: 7537 RVA: 0x000D0352 File Offset: 0x000CE552
	public void OnDeleteBtnClick()
	{
		USelectBox.Show("是否删除此槽位云存档？", delegate
		{
			YSNewSaveSystem.DeleteCloudSave(this.Data.Slot);
			this.UICloudSavePanel.RefreshCloudSave();
		}, null);
	}

	// Token: 0x04001805 RID: 6149
	public Text SlotIDText;

	// Token: 0x04001806 RID: 6150
	public GameObject NoDataObj;

	// Token: 0x04001807 RID: 6151
	public GameObject HasDataObj;

	// Token: 0x04001808 RID: 6152
	public Text UploadTimeText;

	// Token: 0x04001809 RID: 6153
	public Text FileSizeText;

	// Token: 0x0400180A RID: 6154
	public Text DescText;

	// Token: 0x0400180B RID: 6155
	public CloudSaveFileData Data;

	// Token: 0x0400180C RID: 6156
	[HideInInspector]
	public UICloudSavePanel UICloudSavePanel;
}
