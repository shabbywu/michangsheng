using System;
using System.Collections.Generic;
using Steamworks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace script.Steam.Utils
{
	// Token: 0x020009E0 RID: 2528
	public class UploadModProgress : MonoBehaviour
	{
		// Token: 0x0600460C RID: 17932 RVA: 0x001DA3FC File Offset: 0x001D85FC
		private void Awake()
		{
			this.dict = new Dictionary<string, string>();
			this.dict.Add("k_EItemUpdateStatusInvalid", "上传成功");
			this.dict.Add("k_EItemUpdateStatusPreparingConfig", "正在处理配置数据。");
			this.dict.Add("k_EItemUpdateStatusPreparingContent", "正在读取和处理内容文件。");
			this.dict.Add("k_EItemUpdateStatusUploadingContent", "正在将内容更改上传到 Steam。");
			this.dict.Add("k_EItemUpdateStatusUploadingPreviewFile", "正在上传新的预览文件图像。");
			this.dict.Add("k_EItemUpdateStatusCommittingChanges", "正在提交所有更改。");
		}

		// Token: 0x0600460D RID: 17933 RVA: 0x001DA492 File Offset: 0x001D8692
		public void StartProgress(UGCUpdateHandle_t updateHandleT, Text text, UnityAction successCallBack)
		{
			this.Action = successCallBack;
			this.handle = updateHandleT;
			this.content = text;
			this.IsUploading = true;
		}

		// Token: 0x0600460E RID: 17934 RVA: 0x001DA4B0 File Offset: 0x001D86B0
		public void Stop()
		{
			this.IsUploading = false;
			this.Action = null;
		}

		// Token: 0x0600460F RID: 17935 RVA: 0x001DA4C0 File Offset: 0x001D86C0
		private void LateUpdate()
		{
			if (!this.IsUploading)
			{
				return;
			}
			this.content.text = (this.dict[SteamUGC.GetItemUpdateProgress(this.handle, ref this.cur, ref this.total).ToString()] ?? "");
			if (this.content.text.Contains("上传成功"))
			{
				UnityAction action = this.Action;
				if (action != null)
				{
					action.Invoke();
				}
				this.content.text = "";
				this.Stop();
			}
		}

		// Token: 0x04004799 RID: 18329
		public bool IsUploading;

		// Token: 0x0400479A RID: 18330
		private UGCUpdateHandle_t handle;

		// Token: 0x0400479B RID: 18331
		public ulong cur;

		// Token: 0x0400479C RID: 18332
		public ulong total;

		// Token: 0x0400479D RID: 18333
		private Text content;

		// Token: 0x0400479E RID: 18334
		private Dictionary<string, string> dict;

		// Token: 0x0400479F RID: 18335
		public UnityAction Action;
	}
}
