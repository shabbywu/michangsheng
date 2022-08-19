using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x020007AA RID: 1962
	public class BuffSeidJsonData205 : IJSONClass
	{
		// Token: 0x06003CBA RID: 15546 RVA: 0x001A0B98 File Offset: 0x0019ED98
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[205].list)
			{
				try
				{
					BuffSeidJsonData205 buffSeidJsonData = new BuffSeidJsonData205();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					if (BuffSeidJsonData205.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData205.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData205.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData205.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData205.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData205.OnInitFinishAction != null)
			{
				BuffSeidJsonData205.OnInitFinishAction();
			}
		}

		// Token: 0x06003CBB RID: 15547 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x040036A7 RID: 13991
		public static int SEIDID = 205;

		// Token: 0x040036A8 RID: 13992
		public static Dictionary<int, BuffSeidJsonData205> DataDict = new Dictionary<int, BuffSeidJsonData205>();

		// Token: 0x040036A9 RID: 13993
		public static List<BuffSeidJsonData205> DataList = new List<BuffSeidJsonData205>();

		// Token: 0x040036AA RID: 13994
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData205.OnInitFinish);

		// Token: 0x040036AB RID: 13995
		public int id;

		// Token: 0x040036AC RID: 13996
		public int value1;
	}
}
