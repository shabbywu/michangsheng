using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000BFD RID: 3069
	public class JieDanSeidJsonData1 : IJSONClass
	{
		// Token: 0x06004B5D RID: 19293 RVA: 0x001FD2C0 File Offset: 0x001FB4C0
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.JieDanSeidJsonData[1].list)
			{
				try
				{
					JieDanSeidJsonData1 jieDanSeidJsonData = new JieDanSeidJsonData1();
					jieDanSeidJsonData.skillid = jsonobject["skillid"].I;
					jieDanSeidJsonData.target = jsonobject["target"].I;
					jieDanSeidJsonData.value1 = jsonobject["value1"].ToList();
					jieDanSeidJsonData.value2 = jsonobject["value2"].ToList();
					if (JieDanSeidJsonData1.DataDict.ContainsKey(jieDanSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典JieDanSeidJsonData1.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", jieDanSeidJsonData.skillid));
					}
					else
					{
						JieDanSeidJsonData1.DataDict.Add(jieDanSeidJsonData.skillid, jieDanSeidJsonData);
						JieDanSeidJsonData1.DataList.Add(jieDanSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典JieDanSeidJsonData1.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (JieDanSeidJsonData1.OnInitFinishAction != null)
			{
				JieDanSeidJsonData1.OnInitFinishAction();
			}
		}

		// Token: 0x06004B5E RID: 19294 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x040047BF RID: 18367
		public static int SEIDID = 1;

		// Token: 0x040047C0 RID: 18368
		public static Dictionary<int, JieDanSeidJsonData1> DataDict = new Dictionary<int, JieDanSeidJsonData1>();

		// Token: 0x040047C1 RID: 18369
		public static List<JieDanSeidJsonData1> DataList = new List<JieDanSeidJsonData1>();

		// Token: 0x040047C2 RID: 18370
		public static Action OnInitFinishAction = new Action(JieDanSeidJsonData1.OnInitFinish);

		// Token: 0x040047C3 RID: 18371
		public int skillid;

		// Token: 0x040047C4 RID: 18372
		public int target;

		// Token: 0x040047C5 RID: 18373
		public List<int> value1 = new List<int>();

		// Token: 0x040047C6 RID: 18374
		public List<int> value2 = new List<int>();
	}
}
