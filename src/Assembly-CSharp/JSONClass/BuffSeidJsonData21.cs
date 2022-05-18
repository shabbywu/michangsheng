using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000B46 RID: 2886
	public class BuffSeidJsonData21 : IJSONClass
	{
		// Token: 0x06004880 RID: 18560 RVA: 0x001EDEA4 File Offset: 0x001EC0A4
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[21].list)
			{
				try
				{
					BuffSeidJsonData21 buffSeidJsonData = new BuffSeidJsonData21();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					if (BuffSeidJsonData21.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData21.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData21.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData21.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData21.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData21.OnInitFinishAction != null)
			{
				BuffSeidJsonData21.OnInitFinishAction();
			}
		}

		// Token: 0x06004881 RID: 18561 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x0400425A RID: 16986
		public static int SEIDID = 21;

		// Token: 0x0400425B RID: 16987
		public static Dictionary<int, BuffSeidJsonData21> DataDict = new Dictionary<int, BuffSeidJsonData21>();

		// Token: 0x0400425C RID: 16988
		public static List<BuffSeidJsonData21> DataList = new List<BuffSeidJsonData21>();

		// Token: 0x0400425D RID: 16989
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData21.OnInitFinish);

		// Token: 0x0400425E RID: 16990
		public int id;

		// Token: 0x0400425F RID: 16991
		public int value1;
	}
}
