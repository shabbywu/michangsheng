using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000B11 RID: 2833
	public class BuffSeidJsonData144 : IJSONClass
	{
		// Token: 0x060047AE RID: 18350 RVA: 0x001E9E8C File Offset: 0x001E808C
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[144].list)
			{
				try
				{
					BuffSeidJsonData144 buffSeidJsonData = new BuffSeidJsonData144();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					buffSeidJsonData.value2 = jsonobject["value2"].I;
					buffSeidJsonData.value3 = jsonobject["value3"].I;
					if (BuffSeidJsonData144.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData144.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData144.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData144.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData144.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData144.OnInitFinishAction != null)
			{
				BuffSeidJsonData144.OnInitFinishAction();
			}
		}

		// Token: 0x060047AF RID: 18351 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x040040FC RID: 16636
		public static int SEIDID = 144;

		// Token: 0x040040FD RID: 16637
		public static Dictionary<int, BuffSeidJsonData144> DataDict = new Dictionary<int, BuffSeidJsonData144>();

		// Token: 0x040040FE RID: 16638
		public static List<BuffSeidJsonData144> DataList = new List<BuffSeidJsonData144>();

		// Token: 0x040040FF RID: 16639
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData144.OnInitFinish);

		// Token: 0x04004100 RID: 16640
		public int id;

		// Token: 0x04004101 RID: 16641
		public int value1;

		// Token: 0x04004102 RID: 16642
		public int value2;

		// Token: 0x04004103 RID: 16643
		public int value3;
	}
}
