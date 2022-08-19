using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000892 RID: 2194
	public class NewTaskMagData : IJSONClass
	{
		// Token: 0x0600405B RID: 16475 RVA: 0x001B7610 File Offset: 0x001B5810
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.NewTaskMagData.list)
			{
				try
				{
					NewTaskMagData newTaskMagData = new NewTaskMagData();
					newTaskMagData.id = jsonobject["id"].I;
					newTaskMagData.ShiBaiLevel = jsonobject["ShiBaiLevel"].I;
					newTaskMagData.continueTime = jsonobject["continueTime"].I;
					newTaskMagData.EndTime = jsonobject["EndTime"].Str;
					newTaskMagData.ShiBaiType = jsonobject["ShiBaiType"].ToList();
					if (NewTaskMagData.DataDict.ContainsKey(newTaskMagData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典NewTaskMagData.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", newTaskMagData.id));
					}
					else
					{
						NewTaskMagData.DataDict.Add(newTaskMagData.id, newTaskMagData);
						NewTaskMagData.DataList.Add(newTaskMagData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典NewTaskMagData.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (NewTaskMagData.OnInitFinishAction != null)
			{
				NewTaskMagData.OnInitFinishAction();
			}
		}

		// Token: 0x0600405C RID: 16476 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003DB1 RID: 15793
		public static Dictionary<int, NewTaskMagData> DataDict = new Dictionary<int, NewTaskMagData>();

		// Token: 0x04003DB2 RID: 15794
		public static List<NewTaskMagData> DataList = new List<NewTaskMagData>();

		// Token: 0x04003DB3 RID: 15795
		public static Action OnInitFinishAction = new Action(NewTaskMagData.OnInitFinish);

		// Token: 0x04003DB4 RID: 15796
		public int id;

		// Token: 0x04003DB5 RID: 15797
		public int ShiBaiLevel;

		// Token: 0x04003DB6 RID: 15798
		public int continueTime;

		// Token: 0x04003DB7 RID: 15799
		public string EndTime;

		// Token: 0x04003DB8 RID: 15800
		public List<int> ShiBaiType = new List<int>();
	}
}
