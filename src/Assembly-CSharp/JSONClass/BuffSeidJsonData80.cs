using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000B88 RID: 2952
	public class BuffSeidJsonData80 : IJSONClass
	{
		// Token: 0x06004988 RID: 18824 RVA: 0x001F3284 File Offset: 0x001F1484
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[80].list)
			{
				try
				{
					BuffSeidJsonData80 buffSeidJsonData = new BuffSeidJsonData80();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					buffSeidJsonData.value2 = jsonobject["value2"].I;
					buffSeidJsonData.value3 = jsonobject["value3"].I;
					if (BuffSeidJsonData80.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData80.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData80.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData80.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData80.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData80.OnInitFinishAction != null)
			{
				BuffSeidJsonData80.OnInitFinishAction();
			}
		}

		// Token: 0x06004989 RID: 18825 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004432 RID: 17458
		public static int SEIDID = 80;

		// Token: 0x04004433 RID: 17459
		public static Dictionary<int, BuffSeidJsonData80> DataDict = new Dictionary<int, BuffSeidJsonData80>();

		// Token: 0x04004434 RID: 17460
		public static List<BuffSeidJsonData80> DataList = new List<BuffSeidJsonData80>();

		// Token: 0x04004435 RID: 17461
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData80.OnInitFinish);

		// Token: 0x04004436 RID: 17462
		public int id;

		// Token: 0x04004437 RID: 17463
		public int value1;

		// Token: 0x04004438 RID: 17464
		public int value2;

		// Token: 0x04004439 RID: 17465
		public int value3;
	}
}
