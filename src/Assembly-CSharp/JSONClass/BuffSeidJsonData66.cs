using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000B7C RID: 2940
	public class BuffSeidJsonData66 : IJSONClass
	{
		// Token: 0x06004958 RID: 18776 RVA: 0x001F23FC File Offset: 0x001F05FC
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[66].list)
			{
				try
				{
					BuffSeidJsonData66 buffSeidJsonData = new BuffSeidJsonData66();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					buffSeidJsonData.value2 = jsonobject["value2"].I;
					if (BuffSeidJsonData66.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData66.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData66.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData66.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData66.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData66.OnInitFinishAction != null)
			{
				BuffSeidJsonData66.OnInitFinishAction();
			}
		}

		// Token: 0x06004959 RID: 18777 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x040043E3 RID: 17379
		public static int SEIDID = 66;

		// Token: 0x040043E4 RID: 17380
		public static Dictionary<int, BuffSeidJsonData66> DataDict = new Dictionary<int, BuffSeidJsonData66>();

		// Token: 0x040043E5 RID: 17381
		public static List<BuffSeidJsonData66> DataList = new List<BuffSeidJsonData66>();

		// Token: 0x040043E6 RID: 17382
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData66.OnInitFinish);

		// Token: 0x040043E7 RID: 17383
		public int id;

		// Token: 0x040043E8 RID: 17384
		public int value1;

		// Token: 0x040043E9 RID: 17385
		public int value2;
	}
}
