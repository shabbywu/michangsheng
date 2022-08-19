using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x020007E5 RID: 2021
	public class BuffSeidJsonData66 : IJSONClass
	{
		// Token: 0x06003DA6 RID: 15782 RVA: 0x001A6300 File Offset: 0x001A4500
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[66].list)
			{
				try
				{
					BuffSeidJsonData66 buffSeidJsonData = new BuffSeidJsonData66();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					buffSeidJsonData.value2 = jsonobject["value2"].I;
					if (BuffSeidJsonData66.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData66.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData66.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData66.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData66.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData66.OnInitFinishAction != null)
			{
				BuffSeidJsonData66.OnInitFinishAction();
			}
		}

		// Token: 0x06003DA7 RID: 15783 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003853 RID: 14419
		public static int SEIDID = 66;

		// Token: 0x04003854 RID: 14420
		public static Dictionary<int, BuffSeidJsonData66> DataDict = new Dictionary<int, BuffSeidJsonData66>();

		// Token: 0x04003855 RID: 14421
		public static List<BuffSeidJsonData66> DataList = new List<BuffSeidJsonData66>();

		// Token: 0x04003856 RID: 14422
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData66.OnInitFinish);

		// Token: 0x04003857 RID: 14423
		public int id;

		// Token: 0x04003858 RID: 14424
		public int value1;

		// Token: 0x04003859 RID: 14425
		public int value2;
	}
}
