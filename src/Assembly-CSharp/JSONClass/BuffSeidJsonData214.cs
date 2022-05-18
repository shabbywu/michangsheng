using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000B4A RID: 2890
	public class BuffSeidJsonData214 : IJSONClass
	{
		// Token: 0x06004890 RID: 18576 RVA: 0x001EE364 File Offset: 0x001EC564
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

		// Token: 0x06004891 RID: 18577 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004273 RID: 17011
		public static int SEIDID = 214;

		// Token: 0x04004274 RID: 17012
		public static Dictionary<int, BuffSeidJsonData214> DataDict = new Dictionary<int, BuffSeidJsonData214>();

		// Token: 0x04004275 RID: 17013
		public static List<BuffSeidJsonData214> DataList = new List<BuffSeidJsonData214>();

		// Token: 0x04004276 RID: 17014
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData214.OnInitFinish);

		// Token: 0x04004277 RID: 17015
		public int id;

		// Token: 0x04004278 RID: 17016
		public int value1;

		// Token: 0x04004279 RID: 17017
		public string panduan;
	}
}
