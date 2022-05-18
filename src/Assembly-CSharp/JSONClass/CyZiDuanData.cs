using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000BBE RID: 3006
	public class CyZiDuanData : IJSONClass
	{
		// Token: 0x06004A60 RID: 19040 RVA: 0x001F7D04 File Offset: 0x001F5F04
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

		// Token: 0x06004A61 RID: 19041 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x040045DE RID: 17886
		public static Dictionary<int, CyZiDuanData> DataDict = new Dictionary<int, CyZiDuanData>();

		// Token: 0x040045DF RID: 17887
		public static List<CyZiDuanData> DataList = new List<CyZiDuanData>();

		// Token: 0x040045E0 RID: 17888
		public static Action OnInitFinishAction = new Action(CyZiDuanData.OnInitFinish);

		// Token: 0x040045E1 RID: 17889
		public int id;

		// Token: 0x040045E2 RID: 17890
		public string name;
	}
}
