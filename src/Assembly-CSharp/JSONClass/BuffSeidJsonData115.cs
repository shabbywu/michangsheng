using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000AFB RID: 2811
	public class BuffSeidJsonData115 : IJSONClass
	{
		// Token: 0x06004756 RID: 18262 RVA: 0x001E8310 File Offset: 0x001E6510
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[115].list)
			{
				try
				{
					BuffSeidJsonData115 buffSeidJsonData = new BuffSeidJsonData115();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					if (BuffSeidJsonData115.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData115.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData115.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData115.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData115.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData115.OnInitFinishAction != null)
			{
				BuffSeidJsonData115.OnInitFinishAction();
			}
		}

		// Token: 0x06004757 RID: 18263 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004062 RID: 16482
		public static int SEIDID = 115;

		// Token: 0x04004063 RID: 16483
		public static Dictionary<int, BuffSeidJsonData115> DataDict = new Dictionary<int, BuffSeidJsonData115>();

		// Token: 0x04004064 RID: 16484
		public static List<BuffSeidJsonData115> DataList = new List<BuffSeidJsonData115>();

		// Token: 0x04004065 RID: 16485
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData115.OnInitFinish);

		// Token: 0x04004066 RID: 16486
		public int id;

		// Token: 0x04004067 RID: 16487
		public int value1;
	}
}
