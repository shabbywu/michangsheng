using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x020007BF RID: 1983
	public class BuffSeidJsonData312 : IJSONClass
	{
		// Token: 0x06003D0E RID: 15630 RVA: 0x001A29C8 File Offset: 0x001A0BC8
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[312].list)
			{
				try
				{
					BuffSeidJsonData312 buffSeidJsonData = new BuffSeidJsonData312();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					buffSeidJsonData.panduan = jsonobject["panduan"].Str;
					if (BuffSeidJsonData312.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData312.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData312.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData312.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData312.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData312.OnInitFinishAction != null)
			{
				BuffSeidJsonData312.OnInitFinishAction();
			}
		}

		// Token: 0x06003D0F RID: 15631 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003735 RID: 14133
		public static int SEIDID = 312;

		// Token: 0x04003736 RID: 14134
		public static Dictionary<int, BuffSeidJsonData312> DataDict = new Dictionary<int, BuffSeidJsonData312>();

		// Token: 0x04003737 RID: 14135
		public static List<BuffSeidJsonData312> DataList = new List<BuffSeidJsonData312>();

		// Token: 0x04003738 RID: 14136
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData312.OnInitFinish);

		// Token: 0x04003739 RID: 14137
		public int id;

		// Token: 0x0400373A RID: 14138
		public int value1;

		// Token: 0x0400373B RID: 14139
		public string panduan;
	}
}
