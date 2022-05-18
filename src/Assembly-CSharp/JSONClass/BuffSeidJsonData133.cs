using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000B07 RID: 2823
	public class BuffSeidJsonData133 : IJSONClass
	{
		// Token: 0x06004786 RID: 18310 RVA: 0x001E91D0 File Offset: 0x001E73D0
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[133].list)
			{
				try
				{
					BuffSeidJsonData133 buffSeidJsonData = new BuffSeidJsonData133();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.target = jsonobject["target"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					buffSeidJsonData.value3 = jsonobject["value3"].I;
					if (BuffSeidJsonData133.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData133.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData133.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData133.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData133.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData133.OnInitFinishAction != null)
			{
				BuffSeidJsonData133.OnInitFinishAction();
			}
		}

		// Token: 0x06004787 RID: 18311 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x040040B4 RID: 16564
		public static int SEIDID = 133;

		// Token: 0x040040B5 RID: 16565
		public static Dictionary<int, BuffSeidJsonData133> DataDict = new Dictionary<int, BuffSeidJsonData133>();

		// Token: 0x040040B6 RID: 16566
		public static List<BuffSeidJsonData133> DataList = new List<BuffSeidJsonData133>();

		// Token: 0x040040B7 RID: 16567
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData133.OnInitFinish);

		// Token: 0x040040B8 RID: 16568
		public int id;

		// Token: 0x040040B9 RID: 16569
		public int target;

		// Token: 0x040040BA RID: 16570
		public int value1;

		// Token: 0x040040BB RID: 16571
		public int value3;
	}
}
