using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000BB9 RID: 3001
	public class CyPlayeQuestionData : IJSONClass
	{
		// Token: 0x06004A4C RID: 19020 RVA: 0x001F736C File Offset: 0x001F556C
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

		// Token: 0x06004A4D RID: 19021 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x040045A3 RID: 17827
		public static Dictionary<int, CyPlayeQuestionData> DataDict = new Dictionary<int, CyPlayeQuestionData>();

		// Token: 0x040045A4 RID: 17828
		public static List<CyPlayeQuestionData> DataList = new List<CyPlayeQuestionData>();

		// Token: 0x040045A5 RID: 17829
		public static Action OnInitFinishAction = new Action(CyPlayeQuestionData.OnInitFinish);

		// Token: 0x040045A6 RID: 17830
		public int id;

		// Token: 0x040045A7 RID: 17831
		public int SendAction;

		// Token: 0x040045A8 RID: 17832
		public string WenTi;
	}
}
