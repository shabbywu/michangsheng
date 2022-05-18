using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000B89 RID: 2953
	public class BuffSeidJsonData81 : IJSONClass
	{
		// Token: 0x0600498C RID: 18828 RVA: 0x001F33D8 File Offset: 0x001F15D8
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[81].list)
			{
				try
				{
					BuffSeidJsonData81 buffSeidJsonData = new BuffSeidJsonData81();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					if (BuffSeidJsonData81.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData81.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData81.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData81.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData81.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData81.OnInitFinishAction != null)
			{
				BuffSeidJsonData81.OnInitFinishAction();
			}
		}

		// Token: 0x0600498D RID: 18829 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x0400443A RID: 17466
		public static int SEIDID = 81;

		// Token: 0x0400443B RID: 17467
		public static Dictionary<int, BuffSeidJsonData81> DataDict = new Dictionary<int, BuffSeidJsonData81>();

		// Token: 0x0400443C RID: 17468
		public static List<BuffSeidJsonData81> DataList = new List<BuffSeidJsonData81>();

		// Token: 0x0400443D RID: 17469
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData81.OnInitFinish);

		// Token: 0x0400443E RID: 17470
		public int id;

		// Token: 0x0400443F RID: 17471
		public int value1;
	}
}
