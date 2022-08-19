using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x0200086C RID: 2156
	public class JianLingZhenXiang : IJSONClass
	{
		// Token: 0x06003FC2 RID: 16322 RVA: 0x001B3270 File Offset: 0x001B1470
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.JianLingZhenXiang.list)
			{
				try
				{
					JianLingZhenXiang jianLingZhenXiang = new JianLingZhenXiang();
					jianLingZhenXiang.id = jsonobject["id"].Str;
					jianLingZhenXiang.desc = jsonobject["desc"].Str;
					if (JianLingZhenXiang.DataDict.ContainsKey(jianLingZhenXiang.id))
					{
						PreloadManager.LogException("!!!错误!!!向字典JianLingZhenXiang.DataDict添加数据时出现重复的键，Key:" + jianLingZhenXiang.id + "，已跳过，请检查配表");
					}
					else
					{
						JianLingZhenXiang.DataDict.Add(jianLingZhenXiang.id, jianLingZhenXiang);
						JianLingZhenXiang.DataList.Add(jianLingZhenXiang);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典JianLingZhenXiang.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (JianLingZhenXiang.OnInitFinishAction != null)
			{
				JianLingZhenXiang.OnInitFinishAction();
			}
		}

		// Token: 0x06003FC3 RID: 16323 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003C4B RID: 15435
		public static Dictionary<string, JianLingZhenXiang> DataDict = new Dictionary<string, JianLingZhenXiang>();

		// Token: 0x04003C4C RID: 15436
		public static List<JianLingZhenXiang> DataList = new List<JianLingZhenXiang>();

		// Token: 0x04003C4D RID: 15437
		public static Action OnInitFinishAction = new Action(JianLingZhenXiang.OnInitFinish);

		// Token: 0x04003C4E RID: 15438
		public string id;

		// Token: 0x04003C4F RID: 15439
		public string desc;
	}
}
