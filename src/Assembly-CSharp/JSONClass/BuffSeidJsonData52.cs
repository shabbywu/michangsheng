using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000B6E RID: 2926
	public class BuffSeidJsonData52 : IJSONClass
	{
		// Token: 0x06004920 RID: 18720 RVA: 0x001F12C8 File Offset: 0x001EF4C8
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[52].list)
			{
				try
				{
					BuffSeidJsonData52 buffSeidJsonData = new BuffSeidJsonData52();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					buffSeidJsonData.value2 = jsonobject["value2"].I;
					buffSeidJsonData.value3 = jsonobject["value3"].I;
					if (BuffSeidJsonData52.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData52.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData52.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData52.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData52.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData52.OnInitFinishAction != null)
			{
				BuffSeidJsonData52.OnInitFinishAction();
			}
		}

		// Token: 0x06004921 RID: 18721 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004385 RID: 17285
		public static int SEIDID = 52;

		// Token: 0x04004386 RID: 17286
		public static Dictionary<int, BuffSeidJsonData52> DataDict = new Dictionary<int, BuffSeidJsonData52>();

		// Token: 0x04004387 RID: 17287
		public static List<BuffSeidJsonData52> DataList = new List<BuffSeidJsonData52>();

		// Token: 0x04004388 RID: 17288
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData52.OnInitFinish);

		// Token: 0x04004389 RID: 17289
		public int id;

		// Token: 0x0400438A RID: 17290
		public int value1;

		// Token: 0x0400438B RID: 17291
		public int value2;

		// Token: 0x0400438C RID: 17292
		public int value3;
	}
}
