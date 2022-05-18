using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000B4C RID: 2892
	public class BuffSeidJsonData216 : IJSONClass
	{
		// Token: 0x06004898 RID: 18584 RVA: 0x001EE628 File Offset: 0x001EC828
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[216].list)
			{
				try
				{
					BuffSeidJsonData216 buffSeidJsonData = new BuffSeidJsonData216();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					if (BuffSeidJsonData216.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData216.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData216.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData216.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData216.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData216.OnInitFinishAction != null)
			{
				BuffSeidJsonData216.OnInitFinishAction();
			}
		}

		// Token: 0x06004899 RID: 18585 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004283 RID: 17027
		public static int SEIDID = 216;

		// Token: 0x04004284 RID: 17028
		public static Dictionary<int, BuffSeidJsonData216> DataDict = new Dictionary<int, BuffSeidJsonData216>();

		// Token: 0x04004285 RID: 17029
		public static List<BuffSeidJsonData216> DataList = new List<BuffSeidJsonData216>();

		// Token: 0x04004286 RID: 17030
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData216.OnInitFinish);

		// Token: 0x04004287 RID: 17031
		public int id;

		// Token: 0x04004288 RID: 17032
		public int value1;
	}
}
