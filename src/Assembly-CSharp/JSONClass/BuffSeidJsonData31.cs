using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000B55 RID: 2901
	public class BuffSeidJsonData31 : IJSONClass
	{
		// Token: 0x060048BC RID: 18620 RVA: 0x001EF0F8 File Offset: 0x001ED2F8
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[31].list)
			{
				try
				{
					BuffSeidJsonData31 buffSeidJsonData = new BuffSeidJsonData31();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value2 = jsonobject["value2"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].ToList();
					if (BuffSeidJsonData31.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData31.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData31.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData31.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData31.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData31.OnInitFinishAction != null)
			{
				BuffSeidJsonData31.OnInitFinishAction();
			}
		}

		// Token: 0x060048BD RID: 18621 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x040042BE RID: 17086
		public static int SEIDID = 31;

		// Token: 0x040042BF RID: 17087
		public static Dictionary<int, BuffSeidJsonData31> DataDict = new Dictionary<int, BuffSeidJsonData31>();

		// Token: 0x040042C0 RID: 17088
		public static List<BuffSeidJsonData31> DataList = new List<BuffSeidJsonData31>();

		// Token: 0x040042C1 RID: 17089
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData31.OnInitFinish);

		// Token: 0x040042C2 RID: 17090
		public int id;

		// Token: 0x040042C3 RID: 17091
		public int value2;

		// Token: 0x040042C4 RID: 17092
		public List<int> value1 = new List<int>();
	}
}
