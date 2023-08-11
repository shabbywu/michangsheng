using System.Collections.Generic;
using Steamworks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace script.Steam.Utils;

public class UploadModProgress : MonoBehaviour
{
	public bool IsUploading;

	private UGCUpdateHandle_t handle;

	public ulong cur;

	public ulong total;

	private Text content;

	private Dictionary<string, string> dict;

	public UnityAction Action;

	private void Awake()
	{
		dict = new Dictionary<string, string>();
		dict.Add("k_EItemUpdateStatusInvalid", "上传成功");
		dict.Add("k_EItemUpdateStatusPreparingConfig", "正在处理配置数据。");
		dict.Add("k_EItemUpdateStatusPreparingContent", "正在读取和处理内容文件。");
		dict.Add("k_EItemUpdateStatusUploadingContent", "正在将内容更改上传到 Steam。");
		dict.Add("k_EItemUpdateStatusUploadingPreviewFile", "正在上传新的预览文件图像。");
		dict.Add("k_EItemUpdateStatusCommittingChanges", "正在提交所有更改。");
	}

	public void StartProgress(UGCUpdateHandle_t updateHandleT, Text text, UnityAction successCallBack)
	{
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		Action = successCallBack;
		handle = updateHandleT;
		content = text;
		IsUploading = true;
	}

	public void Stop()
	{
		IsUploading = false;
		Action = null;
	}

	private void LateUpdate()
	{
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		if (!IsUploading)
		{
			return;
		}
		Text obj = content;
		Dictionary<string, string> dictionary = dict;
		EItemUpdateStatus itemUpdateProgress = SteamUGC.GetItemUpdateProgress(handle, ref cur, ref total);
		obj.text = dictionary[((object)(EItemUpdateStatus)(ref itemUpdateProgress)).ToString()] ?? "";
		if (content.text.Contains("上传成功"))
		{
			UnityAction action = Action;
			if (action != null)
			{
				action.Invoke();
			}
			content.text = "";
			Stop();
		}
	}
}
