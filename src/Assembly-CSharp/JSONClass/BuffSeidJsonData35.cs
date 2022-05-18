using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000B5D RID: 2909
	public class BuffSeidJsonData35 : IJSONClass
	{
		// Token: 0x060048DC RID: 18652 RVA: 0x001EFB74 File Offset: 0x001EDD74
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[35].list)
			{
				try
				{
					BuffSeidJsonData35 buffSeidJsonData = new BuffSeidJsonData35();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					buffSeidJsonData.value2 = jsonobject["value2"].I;
					buffSeidJsonData.value3 = jsonobject["value3"].I;
					buffSeidJsonData.value4 = jsonobject["value4"].I;
					if (BuffSeidJsonData35.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData35.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData35.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData35.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData35.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData35.OnInitFinishAction != null)
			{
				BuffSeidJsonData35.OnInitFinishAction();
			}
		}

		// Token: 0x060048DD RID: 18653 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x040042FB RID: 17147
		public static int SEIDID = 35;

		// Token: 0x040042FC RID: 17148
		public static Dictionary<int, BuffSeidJsonData35> DataDict = new Dictionary<int, BuffSeidJsonData35>();

		// Token: 0x040042FD RID: 17149
		public static List<BuffSeidJsonData35> DataList = new List<BuffSeidJsonData35>();

		// Token: 0x040042FE RID: 17150
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData35.OnInitFinish);

		// Token: 0x040042FF RID: 17151
		public int id;

		// Token: 0x04004300 RID: 17152
		public int value1;

		// Token: 0x04004301 RID: 17153
		public int value2;

		// Token: 0x04004302 RID: 17154
		public int value3;

		// Token: 0x04004303 RID: 17155
		public int value4;
	}
}
