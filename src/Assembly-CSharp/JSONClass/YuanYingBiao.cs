using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000993 RID: 2451
	public class YuanYingBiao : IJSONClass
	{
		// Token: 0x06004460 RID: 17504 RVA: 0x001D1BDC File Offset: 0x001CFDDC
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.YuanYingBiao.list)
			{
				try
				{
					YuanYingBiao yuanYingBiao = new YuanYingBiao();
					yuanYingBiao.id = jsonobject["id"].I;
					yuanYingBiao.value1 = jsonobject["value1"].I;
					yuanYingBiao.value2 = jsonobject["value2"].I;
					yuanYingBiao.target = jsonobject["target"].I;
					yuanYingBiao.desc = jsonobject["desc"].Str;
					yuanYingBiao.value3 = jsonobject["value3"].ToList();
					yuanYingBiao.value4 = jsonobject["value4"].ToList();
					if (YuanYingBiao.DataDict.ContainsKey(yuanYingBiao.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典YuanYingBiao.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", yuanYingBiao.id));
					}
					else
					{
						YuanYingBiao.DataDict.Add(yuanYingBiao.id, yuanYingBiao);
						YuanYingBiao.DataList.Add(yuanYingBiao);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典YuanYingBiao.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (YuanYingBiao.OnInitFinishAction != null)
			{
				YuanYingBiao.OnInitFinishAction();
			}
		}

		// Token: 0x06004461 RID: 17505 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x0400460F RID: 17935
		public static Dictionary<int, YuanYingBiao> DataDict = new Dictionary<int, YuanYingBiao>();

		// Token: 0x04004610 RID: 17936
		public static List<YuanYingBiao> DataList = new List<YuanYingBiao>();

		// Token: 0x04004611 RID: 17937
		public static Action OnInitFinishAction = new Action(YuanYingBiao.OnInitFinish);

		// Token: 0x04004612 RID: 17938
		public int id;

		// Token: 0x04004613 RID: 17939
		public int value1;

		// Token: 0x04004614 RID: 17940
		public int value2;

		// Token: 0x04004615 RID: 17941
		public int target;

		// Token: 0x04004616 RID: 17942
		public string desc;

		// Token: 0x04004617 RID: 17943
		public List<int> value3 = new List<int>();

		// Token: 0x04004618 RID: 17944
		public List<int> value4 = new List<int>();
	}
}
