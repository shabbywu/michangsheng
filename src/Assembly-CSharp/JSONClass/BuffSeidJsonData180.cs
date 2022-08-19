using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000797 RID: 1943
	public class BuffSeidJsonData180 : IJSONClass
	{
		// Token: 0x06003C6E RID: 15470 RVA: 0x0019F030 File Offset: 0x0019D230
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[180].list)
			{
				try
				{
					BuffSeidJsonData180 buffSeidJsonData = new BuffSeidJsonData180();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					buffSeidJsonData.panduan = jsonobject["panduan"].Str;
					if (BuffSeidJsonData180.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData180.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData180.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData180.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData180.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData180.OnInitFinishAction != null)
			{
				BuffSeidJsonData180.OnInitFinishAction();
			}
		}

		// Token: 0x06003C6F RID: 15471 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003629 RID: 13865
		public static int SEIDID = 180;

		// Token: 0x0400362A RID: 13866
		public static Dictionary<int, BuffSeidJsonData180> DataDict = new Dictionary<int, BuffSeidJsonData180>();

		// Token: 0x0400362B RID: 13867
		public static List<BuffSeidJsonData180> DataList = new List<BuffSeidJsonData180>();

		// Token: 0x0400362C RID: 13868
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData180.OnInitFinish);

		// Token: 0x0400362D RID: 13869
		public int id;

		// Token: 0x0400362E RID: 13870
		public int value1;

		// Token: 0x0400362F RID: 13871
		public string panduan;
	}
}
