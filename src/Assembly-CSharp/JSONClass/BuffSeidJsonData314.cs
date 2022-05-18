using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000B58 RID: 2904
	public class BuffSeidJsonData314 : IJSONClass
	{
		// Token: 0x060048C8 RID: 18632 RVA: 0x001EF4B4 File Offset: 0x001ED6B4
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[314].list)
			{
				try
				{
					BuffSeidJsonData314 buffSeidJsonData = new BuffSeidJsonData314();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.target = jsonobject["target"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					buffSeidJsonData.value2 = jsonobject["value2"].I;
					if (BuffSeidJsonData314.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData314.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData314.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData314.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData314.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData314.OnInitFinishAction != null)
			{
				BuffSeidJsonData314.OnInitFinishAction();
			}
		}

		// Token: 0x060048C9 RID: 18633 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x040042D3 RID: 17107
		public static int SEIDID = 314;

		// Token: 0x040042D4 RID: 17108
		public static Dictionary<int, BuffSeidJsonData314> DataDict = new Dictionary<int, BuffSeidJsonData314>();

		// Token: 0x040042D5 RID: 17109
		public static List<BuffSeidJsonData314> DataList = new List<BuffSeidJsonData314>();

		// Token: 0x040042D6 RID: 17110
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData314.OnInitFinish);

		// Token: 0x040042D7 RID: 17111
		public int id;

		// Token: 0x040042D8 RID: 17112
		public int target;

		// Token: 0x040042D9 RID: 17113
		public int value1;

		// Token: 0x040042DA RID: 17114
		public int value2;
	}
}
