using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x0200086F RID: 2159
	public class JieDanSeidJsonData1 : IJSONClass
	{
		// Token: 0x06003FCF RID: 16335 RVA: 0x001B3820 File Offset: 0x001B1A20
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

		// Token: 0x06003FD0 RID: 16336 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003C66 RID: 15462
		public static int SEIDID = 1;

		// Token: 0x04003C67 RID: 15463
		public static Dictionary<int, JieDanSeidJsonData1> DataDict = new Dictionary<int, JieDanSeidJsonData1>();

		// Token: 0x04003C68 RID: 15464
		public static List<JieDanSeidJsonData1> DataList = new List<JieDanSeidJsonData1>();

		// Token: 0x04003C69 RID: 15465
		public static Action OnInitFinishAction = new Action(JieDanSeidJsonData1.OnInitFinish);

		// Token: 0x04003C6A RID: 15466
		public int skillid;

		// Token: 0x04003C6B RID: 15467
		public int target;

		// Token: 0x04003C6C RID: 15468
		public List<int> value1 = new List<int>();

		// Token: 0x04003C6D RID: 15469
		public List<int> value2 = new List<int>();
	}
}
