using System.IO;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UICloudSaveSlot : MonoBehaviour
{
	public Text SlotIDText;

	public GameObject NoDataObj;

	public GameObject HasDataObj;

	public Text UploadTimeText;

	public Text FileSizeText;

	public Text DescText;

	public CloudSaveFileData Data;

	[HideInInspector]
	public UICloudSavePanel UICloudSavePanel;

	public void SetData(CloudSaveFileData data)
	{
		Data = data;
		SlotIDText.text = $"{data.Slot + 1}";
		if (data.HasFile)
		{
			HasDataObj.SetActive(true);
			NoDataObj.SetActive(false);
			UploadTimeText.text = $"上传时间：{data.FileTime}";
			FileSizeText.text = $"文件大小：{data.FileSize / 1024}kb";
			DescText.text = "备注：" + data.FileDesc;
		}
		else
		{
			HasDataObj.SetActive(false);
			NoDataObj.SetActive(true);
		}
	}

	public void OnUploadBtnClick()
	{
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Expected O, but got Unknown
		if (Data.HasFile)
		{
			USelectBox.Show("是否覆盖此槽位?", (UnityAction)delegate
			{
				UInputBox.Show("填写存档备注", delegate(string s)
				{
					YSNewSaveSystem.ZipAndUploadCloudSaveData(Data.Slot, s);
					UIPopTip.Inst.Pop("上传完毕");
					UICloudSavePanel.Close();
				});
			});
		}
		else
		{
			UInputBox.Show("填写存档备注", delegate(string s)
			{
				YSNewSaveSystem.ZipAndUploadCloudSaveData(Data.Slot, s);
				UIPopTip.Inst.Pop("上传完毕");
				UICloudSavePanel.Close();
			});
		}
	}

	public void OnDownloadBtnClick()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Expected O, but got Unknown
		USelectBox.Show("是否下载并覆盖本地存档?\n此操作会删除本地存档！", (UnityAction)delegate
		{
			YSNewSaveSystem.DownloadCloudSave(Data.Slot);
			YSNewSaveSystem.DownloadCloudSaveDesc(Data.Slot);
			if (Directory.Exists(Paths.GetNewSavePath()))
			{
				Directory.Delete(Paths.GetNewSavePath(), recursive: true);
			}
			YSZip.UnZipFile($"{Paths.GetCloudSavePath()}/CloudSave_{Data.Slot}.zip", Paths.GetNewSavePath() ?? "");
			UICloudSavePanel.RefreshCloudSave();
			MainUIMag.inst.RefreshSave();
			UIPopTip.Inst.Pop("云存档下载完成");
		});
	}

	public void OnDeleteBtnClick()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Expected O, but got Unknown
		USelectBox.Show("是否删除此槽位云存档？", (UnityAction)delegate
		{
			YSNewSaveSystem.DeleteCloudSave(Data.Slot);
			UICloudSavePanel.RefreshCloudSave();
		});
	}
}
