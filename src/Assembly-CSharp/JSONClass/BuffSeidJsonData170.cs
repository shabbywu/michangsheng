using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000B26 RID: 2854
	public class BuffSeidJsonData170 : IJSONClass
	{
		// Token: 0x06004800 RID: 18432 RVA: 0x001EB7A4 File Offset: 0x001E99A4
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[170].list)
			{
				try
				{
					BuffSeidJsonData170 buffSeidJsonData = new BuffSeidJsonData170();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					if (BuffSeidJsonData170.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData170.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData170.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData170.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData170.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData170.OnInitFinishAction != null)
			{
				BuffSeidJsonData170.OnInitFinishAction();
			}
		}

		// Token: 0x06004801 RID: 18433 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004188 RID: 16776
		public static int SEIDID = 170;

		// Token: 0x04004189 RID: 16777
		public static Dictionary<int, BuffSeidJsonData170> DataDict = new Dictionary<int, BuffSeidJsonData170>();

		// Token: 0x0400418A RID: 16778
		public static List<BuffSeidJsonData170> DataList = new List<BuffSeidJsonData170>();

		// Token: 0x0400418B RID: 16779
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData170.OnInitFinish);

		// Token: 0x0400418C RID: 16780
		public int id;

		// Token: 0x0400418D RID: 16781
		public int value1;
	}
}
