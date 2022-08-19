using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000823 RID: 2083
	public class CyPlayeQuestionData : IJSONClass
	{
		// Token: 0x06003E9E RID: 16030 RVA: 0x001AC064 File Offset: 0x001AA264
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.CyPlayeQuestionData.list)
			{
				try
				{
					CyPlayeQuestionData cyPlayeQuestionData = new CyPlayeQuestionData();
					cyPlayeQuestionData.id = jsonobject["id"].I;
					cyPlayeQuestionData.SendAction = jsonobject["SendAction"].I;
					cyPlayeQuestionData.WenTi = jsonobject["WenTi"].Str;
					if (CyPlayeQuestionData.DataDict.ContainsKey(cyPlayeQuestionData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典CyPlayeQuestionData.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", cyPlayeQuestionData.id));
					}
					else
					{
						CyPlayeQuestionData.DataDict.Add(cyPlayeQuestionData.id, cyPlayeQuestionData);
						CyPlayeQuestionData.DataList.Add(cyPlayeQuestionData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典CyPlayeQuestionData.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (CyPlayeQuestionData.OnInitFinishAction != null)
			{
				CyPlayeQuestionData.OnInitFinishAction();
			}
		}

		// Token: 0x06003E9F RID: 16031 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003A1B RID: 14875
		public static Dictionary<int, CyPlayeQuestionData> DataDict = new Dictionary<int, CyPlayeQuestionData>();

		// Token: 0x04003A1C RID: 14876
		public static List<CyPlayeQuestionData> DataList = new List<CyPlayeQuestionData>();

		// Token: 0x04003A1D RID: 14877
		public static Action OnInitFinishAction = new Action(CyPlayeQuestionData.OnInitFinish);

		// Token: 0x04003A1E RID: 14878
		public int id;

		// Token: 0x04003A1F RID: 14879
		public int SendAction;

		// Token: 0x04003A20 RID: 14880
		public string WenTi;
	}
}
