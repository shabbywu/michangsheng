using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x020007A0 RID: 1952
	public class BuffSeidJsonData196 : IJSONClass
	{
		// Token: 0x06003C92 RID: 15506 RVA: 0x0019FD84 File Offset: 0x0019DF84
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[196].list)
			{
				try
				{
					BuffSeidJsonData196 buffSeidJsonData = new BuffSeidJsonData196();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					buffSeidJsonData.panduan = jsonobject["panduan"].Str;
					if (BuffSeidJsonData196.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData196.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData196.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData196.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData196.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData196.OnInitFinishAction != null)
			{
				BuffSeidJsonData196.OnInitFinishAction();
			}
		}

		// Token: 0x06003C93 RID: 15507 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003667 RID: 13927
		public static int SEIDID = 196;

		// Token: 0x04003668 RID: 13928
		public static Dictionary<int, BuffSeidJsonData196> DataDict = new Dictionary<int, BuffSeidJsonData196>();

		// Token: 0x04003669 RID: 13929
		public static List<BuffSeidJsonData196> DataList = new List<BuffSeidJsonData196>();

		// Token: 0x0400366A RID: 13930
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData196.OnInitFinish);

		// Token: 0x0400366B RID: 13931
		public int id;

		// Token: 0x0400366C RID: 13932
		public int value1;

		// Token: 0x0400366D RID: 13933
		public string panduan;
	}
}
