using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000826 RID: 2086
	public class CyShiLiNameData : IJSONClass
	{
		// Token: 0x06003EAA RID: 16042 RVA: 0x001AC810 File Offset: 0x001AAA10
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.CyShiLiNameData.list)
			{
				try
				{
					CyShiLiNameData cyShiLiNameData = new CyShiLiNameData();
					cyShiLiNameData.id = jsonobject["id"].I;
					cyShiLiNameData.name = jsonobject["name"].Str;
					if (CyShiLiNameData.DataDict.ContainsKey(cyShiLiNameData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典CyShiLiNameData.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", cyShiLiNameData.id));
					}
					else
					{
						CyShiLiNameData.DataDict.Add(cyShiLiNameData.id, cyShiLiNameData);
						CyShiLiNameData.DataList.Add(cyShiLiNameData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典CyShiLiNameData.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (CyShiLiNameData.OnInitFinishAction != null)
			{
				CyShiLiNameData.OnInitFinishAction();
			}
		}

		// Token: 0x06003EAB RID: 16043 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003A4B RID: 14923
		public static Dictionary<int, CyShiLiNameData> DataDict = new Dictionary<int, CyShiLiNameData>();

		// Token: 0x04003A4C RID: 14924
		public static List<CyShiLiNameData> DataList = new List<CyShiLiNameData>();

		// Token: 0x04003A4D RID: 14925
		public static Action OnInitFinishAction = new Action(CyShiLiNameData.OnInitFinish);

		// Token: 0x04003A4E RID: 14926
		public int id;

		// Token: 0x04003A4F RID: 14927
		public string name;
	}
}
