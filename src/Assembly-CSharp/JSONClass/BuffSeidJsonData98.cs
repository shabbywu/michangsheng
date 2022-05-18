using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000B98 RID: 2968
	public class BuffSeidJsonData98 : IJSONClass
	{
		// Token: 0x060049C8 RID: 18888 RVA: 0x001F45A8 File Offset: 0x001F27A8
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[98].list)
			{
				try
				{
					BuffSeidJsonData98 buffSeidJsonData = new BuffSeidJsonData98();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					buffSeidJsonData.value2 = jsonobject["value2"].I;
					if (BuffSeidJsonData98.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData98.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData98.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData98.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData98.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData98.OnInitFinishAction != null)
			{
				BuffSeidJsonData98.OnInitFinishAction();
			}
		}

		// Token: 0x060049C9 RID: 18889 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x0400449A RID: 17562
		public static int SEIDID = 98;

		// Token: 0x0400449B RID: 17563
		public static Dictionary<int, BuffSeidJsonData98> DataDict = new Dictionary<int, BuffSeidJsonData98>();

		// Token: 0x0400449C RID: 17564
		public static List<BuffSeidJsonData98> DataList = new List<BuffSeidJsonData98>();

		// Token: 0x0400449D RID: 17565
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData98.OnInitFinish);

		// Token: 0x0400449E RID: 17566
		public int id;

		// Token: 0x0400449F RID: 17567
		public int value1;

		// Token: 0x040044A0 RID: 17568
		public int value2;
	}
}
