using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000C5C RID: 3164
	public class RunawayJsonData : IJSONClass
	{
		// Token: 0x06004CD9 RID: 19673 RVA: 0x00207938 File Offset: 0x00205B38
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.RunawayJsonData.list)
			{
				try
				{
					RunawayJsonData runawayJsonData = new RunawayJsonData();
					runawayJsonData.id = jsonobject["id"].I;
					runawayJsonData.RunCha = jsonobject["RunCha"].I;
					runawayJsonData.RunTime = jsonobject["RunTime"].I;
					runawayJsonData.RunDistance = jsonobject["RunDistance"].I;
					runawayJsonData.Text = jsonobject["Text"].Str;
					if (RunawayJsonData.DataDict.ContainsKey(runawayJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典RunawayJsonData.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", runawayJsonData.id));
					}
					else
					{
						RunawayJsonData.DataDict.Add(runawayJsonData.id, runawayJsonData);
						RunawayJsonData.DataList.Add(runawayJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典RunawayJsonData.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (RunawayJsonData.OnInitFinishAction != null)
			{
				RunawayJsonData.OnInitFinishAction();
			}
		}

		// Token: 0x06004CDA RID: 19674 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004BCC RID: 19404
		public static Dictionary<int, RunawayJsonData> DataDict = new Dictionary<int, RunawayJsonData>();

		// Token: 0x04004BCD RID: 19405
		public static List<RunawayJsonData> DataList = new List<RunawayJsonData>();

		// Token: 0x04004BCE RID: 19406
		public static Action OnInitFinishAction = new Action(RunawayJsonData.OnInitFinish);

		// Token: 0x04004BCF RID: 19407
		public int id;

		// Token: 0x04004BD0 RID: 19408
		public int RunCha;

		// Token: 0x04004BD1 RID: 19409
		public int RunTime;

		// Token: 0x04004BD2 RID: 19410
		public int RunDistance;

		// Token: 0x04004BD3 RID: 19411
		public string Text;
	}
}
