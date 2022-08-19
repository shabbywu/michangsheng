using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000828 RID: 2088
	public class CyZiDuanData : IJSONClass
	{
		// Token: 0x06003EB2 RID: 16050 RVA: 0x001ACAC0 File Offset: 0x001AACC0
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.CyZiDuanData.list)
			{
				try
				{
					CyZiDuanData cyZiDuanData = new CyZiDuanData();
					cyZiDuanData.id = jsonobject["id"].I;
					cyZiDuanData.name = jsonobject["name"].Str;
					if (CyZiDuanData.DataDict.ContainsKey(cyZiDuanData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典CyZiDuanData.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", cyZiDuanData.id));
					}
					else
					{
						CyZiDuanData.DataDict.Add(cyZiDuanData.id, cyZiDuanData);
						CyZiDuanData.DataList.Add(cyZiDuanData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典CyZiDuanData.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (CyZiDuanData.OnInitFinishAction != null)
			{
				CyZiDuanData.OnInitFinishAction();
			}
		}

		// Token: 0x06003EB3 RID: 16051 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003A56 RID: 14934
		public static Dictionary<int, CyZiDuanData> DataDict = new Dictionary<int, CyZiDuanData>();

		// Token: 0x04003A57 RID: 14935
		public static List<CyZiDuanData> DataList = new List<CyZiDuanData>();

		// Token: 0x04003A58 RID: 14936
		public static Action OnInitFinishAction = new Action(CyZiDuanData.OnInitFinish);

		// Token: 0x04003A59 RID: 14937
		public int id;

		// Token: 0x04003A5A RID: 14938
		public string name;
	}
}
