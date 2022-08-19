using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000806 RID: 2054
	public class ChengHaoJsonData : IJSONClass
	{
		// Token: 0x06003E2A RID: 15914 RVA: 0x001A9280 File Offset: 0x001A7480
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.ChengHaoJsonData.list)
			{
				try
				{
					ChengHaoJsonData chengHaoJsonData = new ChengHaoJsonData();
					chengHaoJsonData.id = jsonobject["id"].I;
					chengHaoJsonData.Name = jsonobject["Name"].Str;
					if (ChengHaoJsonData.DataDict.ContainsKey(chengHaoJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典ChengHaoJsonData.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", chengHaoJsonData.id));
					}
					else
					{
						ChengHaoJsonData.DataDict.Add(chengHaoJsonData.id, chengHaoJsonData);
						ChengHaoJsonData.DataList.Add(chengHaoJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典ChengHaoJsonData.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (ChengHaoJsonData.OnInitFinishAction != null)
			{
				ChengHaoJsonData.OnInitFinishAction();
			}
		}

		// Token: 0x06003E2B RID: 15915 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003935 RID: 14645
		public static Dictionary<int, ChengHaoJsonData> DataDict = new Dictionary<int, ChengHaoJsonData>();

		// Token: 0x04003936 RID: 14646
		public static List<ChengHaoJsonData> DataList = new List<ChengHaoJsonData>();

		// Token: 0x04003937 RID: 14647
		public static Action OnInitFinishAction = new Action(ChengHaoJsonData.OnInitFinish);

		// Token: 0x04003938 RID: 14648
		public int id;

		// Token: 0x04003939 RID: 14649
		public string Name;
	}
}
