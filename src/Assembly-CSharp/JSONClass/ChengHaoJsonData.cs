using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000B9D RID: 2973
	public class ChengHaoJsonData : IJSONClass
	{
		// Token: 0x060049DC RID: 18908 RVA: 0x001F4D08 File Offset: 0x001F2F08
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

		// Token: 0x060049DD RID: 18909 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x040044C5 RID: 17605
		public static Dictionary<int, ChengHaoJsonData> DataDict = new Dictionary<int, ChengHaoJsonData>();

		// Token: 0x040044C6 RID: 17606
		public static List<ChengHaoJsonData> DataList = new List<ChengHaoJsonData>();

		// Token: 0x040044C7 RID: 17607
		public static Action OnInitFinishAction = new Action(ChengHaoJsonData.OnInitFinish);

		// Token: 0x040044C8 RID: 17608
		public int id;

		// Token: 0x040044C9 RID: 17609
		public string Name;
	}
}
