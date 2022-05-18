using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000D13 RID: 3347
	public class YuanYingBiao : IJSONClass
	{
		// Token: 0x06004FB6 RID: 20406 RVA: 0x002169A4 File Offset: 0x00214BA4
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

		// Token: 0x06004FB7 RID: 20407 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04005103 RID: 20739
		public static Dictionary<int, YuanYingBiao> DataDict = new Dictionary<int, YuanYingBiao>();

		// Token: 0x04005104 RID: 20740
		public static List<YuanYingBiao> DataList = new List<YuanYingBiao>();

		// Token: 0x04005105 RID: 20741
		public static Action OnInitFinishAction = new Action(YuanYingBiao.OnInitFinish);

		// Token: 0x04005106 RID: 20742
		public int id;

		// Token: 0x04005107 RID: 20743
		public int value1;

		// Token: 0x04005108 RID: 20744
		public int value2;

		// Token: 0x04005109 RID: 20745
		public int target;

		// Token: 0x0400510A RID: 20746
		public string desc;

		// Token: 0x0400510B RID: 20747
		public List<int> value3 = new List<int>();

		// Token: 0x0400510C RID: 20748
		public List<int> value4 = new List<int>();
	}
}
