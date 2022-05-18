using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000C20 RID: 3104
	public class NewTaskMagData : IJSONClass
	{
		// Token: 0x06004BE9 RID: 19433 RVA: 0x00200A94 File Offset: 0x001FEC94
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

		// Token: 0x06004BEA RID: 19434 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x0400490A RID: 18698
		public static Dictionary<int, NewTaskMagData> DataDict = new Dictionary<int, NewTaskMagData>();

		// Token: 0x0400490B RID: 18699
		public static List<NewTaskMagData> DataList = new List<NewTaskMagData>();

		// Token: 0x0400490C RID: 18700
		public static Action OnInitFinishAction = new Action(NewTaskMagData.OnInitFinish);

		// Token: 0x0400490D RID: 18701
		public int id;

		// Token: 0x0400490E RID: 18702
		public int ShiBaiLevel;

		// Token: 0x0400490F RID: 18703
		public int continueTime;

		// Token: 0x04004910 RID: 18704
		public string EndTime;

		// Token: 0x04004911 RID: 18705
		public List<int> ShiBaiType = new List<int>();
	}
}
