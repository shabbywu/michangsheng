using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000B1F RID: 2847
	public class BuffSeidJsonData164 : IJSONClass
	{
		// Token: 0x060047E4 RID: 18404 RVA: 0x001EAEF4 File Offset: 0x001E90F4
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[164].list)
			{
				try
				{
					BuffSeidJsonData164 buffSeidJsonData = new BuffSeidJsonData164();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					if (BuffSeidJsonData164.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData164.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData164.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData164.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData164.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData164.OnInitFinishAction != null)
			{
				BuffSeidJsonData164.OnInitFinishAction();
			}
		}

		// Token: 0x060047E5 RID: 18405 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004159 RID: 16729
		public static int SEIDID = 164;

		// Token: 0x0400415A RID: 16730
		public static Dictionary<int, BuffSeidJsonData164> DataDict = new Dictionary<int, BuffSeidJsonData164>();

		// Token: 0x0400415B RID: 16731
		public static List<BuffSeidJsonData164> DataList = new List<BuffSeidJsonData164>();

		// Token: 0x0400415C RID: 16732
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData164.OnInitFinish);

		// Token: 0x0400415D RID: 16733
		public int id;

		// Token: 0x0400415E RID: 16734
		public int value1;
	}
}
