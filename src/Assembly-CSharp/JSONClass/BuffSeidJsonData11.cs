using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000AF6 RID: 2806
	public class BuffSeidJsonData11 : IJSONClass
	{
		// Token: 0x06004742 RID: 18242 RVA: 0x001E7D48 File Offset: 0x001E5F48
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[11].list)
			{
				try
				{
					BuffSeidJsonData11 buffSeidJsonData = new BuffSeidJsonData11();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					if (BuffSeidJsonData11.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData11.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData11.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData11.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData11.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData11.OnInitFinishAction != null)
			{
				BuffSeidJsonData11.OnInitFinishAction();
			}
		}

		// Token: 0x06004743 RID: 18243 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004044 RID: 16452
		public static int SEIDID = 11;

		// Token: 0x04004045 RID: 16453
		public static Dictionary<int, BuffSeidJsonData11> DataDict = new Dictionary<int, BuffSeidJsonData11>();

		// Token: 0x04004046 RID: 16454
		public static List<BuffSeidJsonData11> DataList = new List<BuffSeidJsonData11>();

		// Token: 0x04004047 RID: 16455
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData11.OnInitFinish);

		// Token: 0x04004048 RID: 16456
		public int id;

		// Token: 0x04004049 RID: 16457
		public int value1;
	}
}
