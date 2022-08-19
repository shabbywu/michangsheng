using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x0200096F RID: 2415
	public class TaskInfoJsonData : IJSONClass
	{
		// Token: 0x060043CE RID: 17358 RVA: 0x001CDF08 File Offset: 0x001CC108
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.TaskInfoJsonData.list)
			{
				try
				{
					TaskInfoJsonData taskInfoJsonData = new TaskInfoJsonData();
					taskInfoJsonData.id = jsonobject["id"].I;
					taskInfoJsonData.TaskID = jsonobject["TaskID"].I;
					taskInfoJsonData.TaskIndex = jsonobject["TaskIndex"].I;
					taskInfoJsonData.mapIndex = jsonobject["mapIndex"].I;
					taskInfoJsonData.IsFinal = jsonobject["IsFinal"].I;
					taskInfoJsonData.Desc = jsonobject["Desc"].Str;
					if (TaskInfoJsonData.DataDict.ContainsKey(taskInfoJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典TaskInfoJsonData.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", taskInfoJsonData.id));
					}
					else
					{
						TaskInfoJsonData.DataDict.Add(taskInfoJsonData.id, taskInfoJsonData);
						TaskInfoJsonData.DataList.Add(taskInfoJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典TaskInfoJsonData.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (TaskInfoJsonData.OnInitFinishAction != null)
			{
				TaskInfoJsonData.OnInitFinishAction();
			}
		}

		// Token: 0x060043CF RID: 17359 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x040044E5 RID: 17637
		public static Dictionary<int, TaskInfoJsonData> DataDict = new Dictionary<int, TaskInfoJsonData>();

		// Token: 0x040044E6 RID: 17638
		public static List<TaskInfoJsonData> DataList = new List<TaskInfoJsonData>();

		// Token: 0x040044E7 RID: 17639
		public static Action OnInitFinishAction = new Action(TaskInfoJsonData.OnInitFinish);

		// Token: 0x040044E8 RID: 17640
		public int id;

		// Token: 0x040044E9 RID: 17641
		public int TaskID;

		// Token: 0x040044EA RID: 17642
		public int TaskIndex;

		// Token: 0x040044EB RID: 17643
		public int mapIndex;

		// Token: 0x040044EC RID: 17644
		public int IsFinal;

		// Token: 0x040044ED RID: 17645
		public string Desc;
	}
}
