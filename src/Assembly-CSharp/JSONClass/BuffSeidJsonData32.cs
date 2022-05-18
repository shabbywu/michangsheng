using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000B5A RID: 2906
	public class BuffSeidJsonData32 : IJSONClass
	{
		// Token: 0x060048D0 RID: 18640 RVA: 0x001EF74C File Offset: 0x001ED94C
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[32].list)
			{
				try
				{
					BuffSeidJsonData32 buffSeidJsonData = new BuffSeidJsonData32();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					buffSeidJsonData.value2 = jsonobject["value2"].I;
					buffSeidJsonData.value3 = jsonobject["value3"].I;
					if (BuffSeidJsonData32.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData32.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData32.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData32.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData32.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData32.OnInitFinishAction != null)
			{
				BuffSeidJsonData32.OnInitFinishAction();
			}
		}

		// Token: 0x060048D1 RID: 18641 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x040042E2 RID: 17122
		public static int SEIDID = 32;

		// Token: 0x040042E3 RID: 17123
		public static Dictionary<int, BuffSeidJsonData32> DataDict = new Dictionary<int, BuffSeidJsonData32>();

		// Token: 0x040042E4 RID: 17124
		public static List<BuffSeidJsonData32> DataList = new List<BuffSeidJsonData32>();

		// Token: 0x040042E5 RID: 17125
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData32.OnInitFinish);

		// Token: 0x040042E6 RID: 17126
		public int id;

		// Token: 0x040042E7 RID: 17127
		public int value1;

		// Token: 0x040042E8 RID: 17128
		public int value2;

		// Token: 0x040042E9 RID: 17129
		public int value3;
	}
}
