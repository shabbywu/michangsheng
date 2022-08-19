using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x020007EF RID: 2031
	public class BuffSeidJsonData76 : IJSONClass
	{
		// Token: 0x06003DCE RID: 15822 RVA: 0x001A7140 File Offset: 0x001A5340
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[76].list)
			{
				try
				{
					BuffSeidJsonData76 buffSeidJsonData = new BuffSeidJsonData76();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					if (BuffSeidJsonData76.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData76.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData76.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData76.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData76.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData76.OnInitFinishAction != null)
			{
				BuffSeidJsonData76.OnInitFinishAction();
			}
		}

		// Token: 0x06003DCF RID: 15823 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003896 RID: 14486
		public static int SEIDID = 76;

		// Token: 0x04003897 RID: 14487
		public static Dictionary<int, BuffSeidJsonData76> DataDict = new Dictionary<int, BuffSeidJsonData76>();

		// Token: 0x04003898 RID: 14488
		public static List<BuffSeidJsonData76> DataList = new List<BuffSeidJsonData76>();

		// Token: 0x04003899 RID: 14489
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData76.OnInitFinish);

		// Token: 0x0400389A RID: 14490
		public int id;

		// Token: 0x0400389B RID: 14491
		public int value1;
	}
}
