using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000970 RID: 2416
	public class TaskJsonData : IJSONClass
	{
		// Token: 0x060043D2 RID: 17362 RVA: 0x001CE0C4 File Offset: 0x001CC2C4
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.TaskJsonData.list)
			{
				try
				{
					TaskJsonData taskJsonData = new TaskJsonData();
					taskJsonData.id = jsonobject["id"].I;
					taskJsonData.Type = jsonobject["Type"].I;
					taskJsonData.variable = jsonobject["variable"].I;
					taskJsonData.circulation = jsonobject["circulation"].I;
					taskJsonData.mapIndex = jsonobject["mapIndex"].I;
					taskJsonData.continueTime = jsonobject["continueTime"].I;
					taskJsonData.isFinsh = jsonobject["isFinsh"].I;
					taskJsonData.Name = jsonobject["Name"].Str;
					taskJsonData.Title = jsonobject["Title"].Str;
					taskJsonData.Desc = jsonobject["Desc"].Str;
					taskJsonData.StarTime = jsonobject["StarTime"].Str;
					taskJsonData.EndTime = jsonobject["EndTime"].Str;
					if (TaskJsonData.DataDict.ContainsKey(taskJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典TaskJsonData.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", taskJsonData.id));
					}
					else
					{
						TaskJsonData.DataDict.Add(taskJsonData.id, taskJsonData);
						TaskJsonData.DataList.Add(taskJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典TaskJsonData.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (TaskJsonData.OnInitFinishAction != null)
			{
				TaskJsonData.OnInitFinishAction();
			}
		}

		// Token: 0x060043D3 RID: 17363 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x040044EE RID: 17646
		public static Dictionary<int, TaskJsonData> DataDict = new Dictionary<int, TaskJsonData>();

		// Token: 0x040044EF RID: 17647
		public static List<TaskJsonData> DataList = new List<TaskJsonData>();

		// Token: 0x040044F0 RID: 17648
		public static Action OnInitFinishAction = new Action(TaskJsonData.OnInitFinish);

		// Token: 0x040044F1 RID: 17649
		public int id;

		// Token: 0x040044F2 RID: 17650
		public int Type;

		// Token: 0x040044F3 RID: 17651
		public int variable;

		// Token: 0x040044F4 RID: 17652
		public int circulation;

		// Token: 0x040044F5 RID: 17653
		public int mapIndex;

		// Token: 0x040044F6 RID: 17654
		public int continueTime;

		// Token: 0x040044F7 RID: 17655
		public int isFinsh;

		// Token: 0x040044F8 RID: 17656
		public string Name;

		// Token: 0x040044F9 RID: 17657
		public string Title;

		// Token: 0x040044FA RID: 17658
		public string Desc;

		// Token: 0x040044FB RID: 17659
		public string StarTime;

		// Token: 0x040044FC RID: 17660
		public string EndTime;
	}
}
