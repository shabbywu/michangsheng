using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000BBC RID: 3004
	public class CyShiLiNameData : IJSONClass
	{
		// Token: 0x06004A58 RID: 19032 RVA: 0x001F7AA4 File Offset: 0x001F5CA4
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

		// Token: 0x06004A59 RID: 19033 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x040045D3 RID: 17875
		public static Dictionary<int, CyShiLiNameData> DataDict = new Dictionary<int, CyShiLiNameData>();

		// Token: 0x040045D4 RID: 17876
		public static List<CyShiLiNameData> DataList = new List<CyShiLiNameData>();

		// Token: 0x040045D5 RID: 17877
		public static Action OnInitFinishAction = new Action(CyShiLiNameData.OnInitFinish);

		// Token: 0x040045D6 RID: 17878
		public int id;

		// Token: 0x040045D7 RID: 17879
		public string name;
	}
}
