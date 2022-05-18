using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000B6B RID: 2923
	public class BuffSeidJsonData5 : IJSONClass
	{
		// Token: 0x06004914 RID: 18708 RVA: 0x001F0F14 File Offset: 0x001EF114
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[5].list)
			{
				try
				{
					BuffSeidJsonData5 buffSeidJsonData = new BuffSeidJsonData5();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].ToList();
					buffSeidJsonData.value2 = jsonobject["value2"].ToList();
					if (BuffSeidJsonData5.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData5.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData5.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData5.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData5.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData5.OnInitFinishAction != null)
			{
				BuffSeidJsonData5.OnInitFinishAction();
			}
		}

		// Token: 0x06004915 RID: 18709 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004370 RID: 17264
		public static int SEIDID = 5;

		// Token: 0x04004371 RID: 17265
		public static Dictionary<int, BuffSeidJsonData5> DataDict = new Dictionary<int, BuffSeidJsonData5>();

		// Token: 0x04004372 RID: 17266
		public static List<BuffSeidJsonData5> DataList = new List<BuffSeidJsonData5>();

		// Token: 0x04004373 RID: 17267
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData5.OnInitFinish);

		// Token: 0x04004374 RID: 17268
		public int id;

		// Token: 0x04004375 RID: 17269
		public List<int> value1 = new List<int>();

		// Token: 0x04004376 RID: 17270
		public List<int> value2 = new List<int>();
	}
}
