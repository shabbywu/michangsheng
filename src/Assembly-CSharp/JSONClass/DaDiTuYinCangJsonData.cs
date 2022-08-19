using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000829 RID: 2089
	public class DaDiTuYinCangJsonData : IJSONClass
	{
		// Token: 0x06003EB6 RID: 16054 RVA: 0x001ACC0C File Offset: 0x001AAE0C
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.DaDiTuYinCangJsonData.list)
			{
				try
				{
					DaDiTuYinCangJsonData daDiTuYinCangJsonData = new DaDiTuYinCangJsonData();
					daDiTuYinCangJsonData.id = jsonobject["id"].I;
					daDiTuYinCangJsonData.Type = jsonobject["Type"].I;
					daDiTuYinCangJsonData.fuhao = jsonobject["fuhao"].Str;
					daDiTuYinCangJsonData.StartTime = jsonobject["StartTime"].Str;
					daDiTuYinCangJsonData.EndTime = jsonobject["EndTime"].Str;
					daDiTuYinCangJsonData.EventValue = jsonobject["EventValue"].ToList();
					if (DaDiTuYinCangJsonData.DataDict.ContainsKey(daDiTuYinCangJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典DaDiTuYinCangJsonData.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", daDiTuYinCangJsonData.id));
					}
					else
					{
						DaDiTuYinCangJsonData.DataDict.Add(daDiTuYinCangJsonData.id, daDiTuYinCangJsonData);
						DaDiTuYinCangJsonData.DataList.Add(daDiTuYinCangJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典DaDiTuYinCangJsonData.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (DaDiTuYinCangJsonData.OnInitFinishAction != null)
			{
				DaDiTuYinCangJsonData.OnInitFinishAction();
			}
		}

		// Token: 0x06003EB7 RID: 16055 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003A5B RID: 14939
		public static Dictionary<int, DaDiTuYinCangJsonData> DataDict = new Dictionary<int, DaDiTuYinCangJsonData>();

		// Token: 0x04003A5C RID: 14940
		public static List<DaDiTuYinCangJsonData> DataList = new List<DaDiTuYinCangJsonData>();

		// Token: 0x04003A5D RID: 14941
		public static Action OnInitFinishAction = new Action(DaDiTuYinCangJsonData.OnInitFinish);

		// Token: 0x04003A5E RID: 14942
		public int id;

		// Token: 0x04003A5F RID: 14943
		public int Type;

		// Token: 0x04003A60 RID: 14944
		public string fuhao;

		// Token: 0x04003A61 RID: 14945
		public string StartTime;

		// Token: 0x04003A62 RID: 14946
		public string EndTime;

		// Token: 0x04003A63 RID: 14947
		public List<int> EventValue = new List<int>();
	}
}
