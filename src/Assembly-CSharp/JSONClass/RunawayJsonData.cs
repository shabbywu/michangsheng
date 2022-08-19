using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x020008CF RID: 2255
	public class RunawayJsonData : IJSONClass
	{
		// Token: 0x0600414F RID: 16719 RVA: 0x001BF3C0 File Offset: 0x001BD5C0
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

		// Token: 0x06004150 RID: 16720 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004082 RID: 16514
		public static Dictionary<int, RunawayJsonData> DataDict = new Dictionary<int, RunawayJsonData>();

		// Token: 0x04004083 RID: 16515
		public static List<RunawayJsonData> DataList = new List<RunawayJsonData>();

		// Token: 0x04004084 RID: 16516
		public static Action OnInitFinishAction = new Action(RunawayJsonData.OnInitFinish);

		// Token: 0x04004085 RID: 16517
		public int id;

		// Token: 0x04004086 RID: 16518
		public int RunCha;

		// Token: 0x04004087 RID: 16519
		public int RunTime;

		// Token: 0x04004088 RID: 16520
		public int RunDistance;

		// Token: 0x04004089 RID: 16521
		public string Text;
	}
}
