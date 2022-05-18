using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000B53 RID: 2899
	public class BuffSeidJsonData3 : IJSONClass
	{
		// Token: 0x060048B4 RID: 18612 RVA: 0x001EEE94 File Offset: 0x001ED094
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[3].list)
			{
				try
				{
					BuffSeidJsonData3 buffSeidJsonData = new BuffSeidJsonData3();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					if (BuffSeidJsonData3.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData3.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData3.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData3.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData3.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData3.OnInitFinishAction != null)
			{
				BuffSeidJsonData3.OnInitFinishAction();
			}
		}

		// Token: 0x060048B5 RID: 18613 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x040042B1 RID: 17073
		public static int SEIDID = 3;

		// Token: 0x040042B2 RID: 17074
		public static Dictionary<int, BuffSeidJsonData3> DataDict = new Dictionary<int, BuffSeidJsonData3>();

		// Token: 0x040042B3 RID: 17075
		public static List<BuffSeidJsonData3> DataList = new List<BuffSeidJsonData3>();

		// Token: 0x040042B4 RID: 17076
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData3.OnInitFinish);

		// Token: 0x040042B5 RID: 17077
		public int id;

		// Token: 0x040042B6 RID: 17078
		public int value1;
	}
}
