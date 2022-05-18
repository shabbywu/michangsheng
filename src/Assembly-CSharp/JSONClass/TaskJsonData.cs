using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000CF4 RID: 3316
	public class TaskJsonData : IJSONClass
	{
		// Token: 0x06004F38 RID: 20280 RVA: 0x00213AA4 File Offset: 0x00211CA4
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

		// Token: 0x06004F39 RID: 20281 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004FFE RID: 20478
		public static Dictionary<int, TaskJsonData> DataDict = new Dictionary<int, TaskJsonData>();

		// Token: 0x04004FFF RID: 20479
		public static List<TaskJsonData> DataList = new List<TaskJsonData>();

		// Token: 0x04005000 RID: 20480
		public static Action OnInitFinishAction = new Action(TaskJsonData.OnInitFinish);

		// Token: 0x04005001 RID: 20481
		public int id;

		// Token: 0x04005002 RID: 20482
		public int Type;

		// Token: 0x04005003 RID: 20483
		public int variable;

		// Token: 0x04005004 RID: 20484
		public int circulation;

		// Token: 0x04005005 RID: 20485
		public int mapIndex;

		// Token: 0x04005006 RID: 20486
		public int continueTime;

		// Token: 0x04005007 RID: 20487
		public int isFinsh;

		// Token: 0x04005008 RID: 20488
		public string Name;

		// Token: 0x04005009 RID: 20489
		public string Title;

		// Token: 0x0400500A RID: 20490
		public string Desc;

		// Token: 0x0400500B RID: 20491
		public string StarTime;

		// Token: 0x0400500C RID: 20492
		public string EndTime;
	}
}
