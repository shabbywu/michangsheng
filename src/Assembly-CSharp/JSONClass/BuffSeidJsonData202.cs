using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000B3F RID: 2879
	public class BuffSeidJsonData202 : IJSONClass
	{
		// Token: 0x06004864 RID: 18532 RVA: 0x001ED630 File Offset: 0x001EB830
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[202].list)
			{
				try
				{
					BuffSeidJsonData202 buffSeidJsonData = new BuffSeidJsonData202();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					if (BuffSeidJsonData202.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData202.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData202.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData202.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData202.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData202.OnInitFinishAction != null)
			{
				BuffSeidJsonData202.OnInitFinishAction();
			}
		}

		// Token: 0x06004865 RID: 18533 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x0400422D RID: 16941
		public static int SEIDID = 202;

		// Token: 0x0400422E RID: 16942
		public static Dictionary<int, BuffSeidJsonData202> DataDict = new Dictionary<int, BuffSeidJsonData202>();

		// Token: 0x0400422F RID: 16943
		public static List<BuffSeidJsonData202> DataList = new List<BuffSeidJsonData202>();

		// Token: 0x04004230 RID: 16944
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData202.OnInitFinish);

		// Token: 0x04004231 RID: 16945
		public int id;

		// Token: 0x04004232 RID: 16946
		public int value1;
	}
}
