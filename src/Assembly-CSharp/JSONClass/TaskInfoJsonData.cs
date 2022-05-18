using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000CF3 RID: 3315
	public class TaskInfoJsonData : IJSONClass
	{
		// Token: 0x06004F34 RID: 20276 RVA: 0x00213910 File Offset: 0x00211B10
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

		// Token: 0x06004F35 RID: 20277 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004FF5 RID: 20469
		public static Dictionary<int, TaskInfoJsonData> DataDict = new Dictionary<int, TaskInfoJsonData>();

		// Token: 0x04004FF6 RID: 20470
		public static List<TaskInfoJsonData> DataList = new List<TaskInfoJsonData>();

		// Token: 0x04004FF7 RID: 20471
		public static Action OnInitFinishAction = new Action(TaskInfoJsonData.OnInitFinish);

		// Token: 0x04004FF8 RID: 20472
		public int id;

		// Token: 0x04004FF9 RID: 20473
		public int TaskID;

		// Token: 0x04004FFA RID: 20474
		public int TaskIndex;

		// Token: 0x04004FFB RID: 20475
		public int mapIndex;

		// Token: 0x04004FFC RID: 20476
		public int IsFinal;

		// Token: 0x04004FFD RID: 20477
		public string Desc;
	}
}
