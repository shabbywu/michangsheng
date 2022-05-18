using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000B20 RID: 2848
	public class BuffSeidJsonData165 : IJSONClass
	{
		// Token: 0x060047E8 RID: 18408 RVA: 0x001EB020 File Offset: 0x001E9220
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[165].list)
			{
				try
				{
					BuffSeidJsonData165 buffSeidJsonData = new BuffSeidJsonData165();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					if (BuffSeidJsonData165.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData165.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData165.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData165.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData165.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData165.OnInitFinishAction != null)
			{
				BuffSeidJsonData165.OnInitFinishAction();
			}
		}

		// Token: 0x060047E9 RID: 18409 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x0400415F RID: 16735
		public static int SEIDID = 165;

		// Token: 0x04004160 RID: 16736
		public static Dictionary<int, BuffSeidJsonData165> DataDict = new Dictionary<int, BuffSeidJsonData165>();

		// Token: 0x04004161 RID: 16737
		public static List<BuffSeidJsonData165> DataList = new List<BuffSeidJsonData165>();

		// Token: 0x04004162 RID: 16738
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData165.OnInitFinish);

		// Token: 0x04004163 RID: 16739
		public int id;

		// Token: 0x04004164 RID: 16740
		public int value1;
	}
}
