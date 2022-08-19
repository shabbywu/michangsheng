using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x020007B2 RID: 1970
	public class BuffSeidJsonData214 : IJSONClass
	{
		// Token: 0x06003CDA RID: 15578 RVA: 0x001A16D0 File Offset: 0x0019F8D0
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[214].list)
			{
				try
				{
					BuffSeidJsonData214 buffSeidJsonData = new BuffSeidJsonData214();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					buffSeidJsonData.panduan = jsonobject["panduan"].Str;
					if (BuffSeidJsonData214.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData214.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData214.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData214.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData214.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData214.OnInitFinishAction != null)
			{
				BuffSeidJsonData214.OnInitFinishAction();
			}
		}

		// Token: 0x06003CDB RID: 15579 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x040036DA RID: 14042
		public static int SEIDID = 214;

		// Token: 0x040036DB RID: 14043
		public static Dictionary<int, BuffSeidJsonData214> DataDict = new Dictionary<int, BuffSeidJsonData214>();

		// Token: 0x040036DC RID: 14044
		public static List<BuffSeidJsonData214> DataList = new List<BuffSeidJsonData214>();

		// Token: 0x040036DD RID: 14045
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData214.OnInitFinish);

		// Token: 0x040036DE RID: 14046
		public int id;

		// Token: 0x040036DF RID: 14047
		public int value1;

		// Token: 0x040036E0 RID: 14048
		public string panduan;
	}
}
