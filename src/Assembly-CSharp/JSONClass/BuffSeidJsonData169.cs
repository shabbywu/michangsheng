using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000B24 RID: 2852
	public class BuffSeidJsonData169 : IJSONClass
	{
		// Token: 0x060047F8 RID: 18424 RVA: 0x001EB53C File Offset: 0x001E973C
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[169].list)
			{
				try
				{
					BuffSeidJsonData169 buffSeidJsonData = new BuffSeidJsonData169();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					if (BuffSeidJsonData169.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData169.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData169.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData169.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData169.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData169.OnInitFinishAction != null)
			{
				BuffSeidJsonData169.OnInitFinishAction();
			}
		}

		// Token: 0x060047F9 RID: 18425 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x0400417B RID: 16763
		public static int SEIDID = 169;

		// Token: 0x0400417C RID: 16764
		public static Dictionary<int, BuffSeidJsonData169> DataDict = new Dictionary<int, BuffSeidJsonData169>();

		// Token: 0x0400417D RID: 16765
		public static List<BuffSeidJsonData169> DataList = new List<BuffSeidJsonData169>();

		// Token: 0x0400417E RID: 16766
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData169.OnInitFinish);

		// Token: 0x0400417F RID: 16767
		public int id;

		// Token: 0x04004180 RID: 16768
		public int value1;
	}
}
