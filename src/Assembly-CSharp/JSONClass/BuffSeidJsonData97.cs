using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000B97 RID: 2967
	public class BuffSeidJsonData97 : IJSONClass
	{
		// Token: 0x060049C4 RID: 18884 RVA: 0x001F4480 File Offset: 0x001F2680
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[97].list)
			{
				try
				{
					BuffSeidJsonData97 buffSeidJsonData = new BuffSeidJsonData97();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					if (BuffSeidJsonData97.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData97.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData97.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData97.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData97.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData97.OnInitFinishAction != null)
			{
				BuffSeidJsonData97.OnInitFinishAction();
			}
		}

		// Token: 0x060049C5 RID: 18885 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004494 RID: 17556
		public static int SEIDID = 97;

		// Token: 0x04004495 RID: 17557
		public static Dictionary<int, BuffSeidJsonData97> DataDict = new Dictionary<int, BuffSeidJsonData97>();

		// Token: 0x04004496 RID: 17558
		public static List<BuffSeidJsonData97> DataList = new List<BuffSeidJsonData97>();

		// Token: 0x04004497 RID: 17559
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData97.OnInitFinish);

		// Token: 0x04004498 RID: 17560
		public int id;

		// Token: 0x04004499 RID: 17561
		public int value1;
	}
}
