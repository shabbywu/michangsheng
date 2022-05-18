using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000BBF RID: 3007
	public class DaDiTuYinCangJsonData : IJSONClass
	{
		// Token: 0x06004A64 RID: 19044 RVA: 0x001F7E28 File Offset: 0x001F6028
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

		// Token: 0x06004A65 RID: 19045 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x040045E3 RID: 17891
		public static Dictionary<int, DaDiTuYinCangJsonData> DataDict = new Dictionary<int, DaDiTuYinCangJsonData>();

		// Token: 0x040045E4 RID: 17892
		public static List<DaDiTuYinCangJsonData> DataList = new List<DaDiTuYinCangJsonData>();

		// Token: 0x040045E5 RID: 17893
		public static Action OnInitFinishAction = new Action(DaDiTuYinCangJsonData.OnInitFinish);

		// Token: 0x040045E6 RID: 17894
		public int id;

		// Token: 0x040045E7 RID: 17895
		public int Type;

		// Token: 0x040045E8 RID: 17896
		public string fuhao;

		// Token: 0x040045E9 RID: 17897
		public string StartTime;

		// Token: 0x040045EA RID: 17898
		public string EndTime;

		// Token: 0x040045EB RID: 17899
		public List<int> EventValue = new List<int>();
	}
}
