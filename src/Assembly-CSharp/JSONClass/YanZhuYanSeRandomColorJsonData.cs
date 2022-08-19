using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000992 RID: 2450
	public class YanZhuYanSeRandomColorJsonData : IJSONClass
	{
		// Token: 0x0600445C RID: 17500 RVA: 0x001D1A34 File Offset: 0x001CFC34
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.YanZhuYanSeRandomColorJsonData.list)
			{
				try
				{
					YanZhuYanSeRandomColorJsonData yanZhuYanSeRandomColorJsonData = new YanZhuYanSeRandomColorJsonData();
					yanZhuYanSeRandomColorJsonData.id = jsonobject["id"].I;
					yanZhuYanSeRandomColorJsonData.R = jsonobject["R"].I;
					yanZhuYanSeRandomColorJsonData.G = jsonobject["G"].I;
					yanZhuYanSeRandomColorJsonData.B = jsonobject["B"].I;
					yanZhuYanSeRandomColorJsonData.beizhu = jsonobject["beizhu"].Str;
					if (YanZhuYanSeRandomColorJsonData.DataDict.ContainsKey(yanZhuYanSeRandomColorJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典YanZhuYanSeRandomColorJsonData.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", yanZhuYanSeRandomColorJsonData.id));
					}
					else
					{
						YanZhuYanSeRandomColorJsonData.DataDict.Add(yanZhuYanSeRandomColorJsonData.id, yanZhuYanSeRandomColorJsonData);
						YanZhuYanSeRandomColorJsonData.DataList.Add(yanZhuYanSeRandomColorJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典YanZhuYanSeRandomColorJsonData.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (YanZhuYanSeRandomColorJsonData.OnInitFinishAction != null)
			{
				YanZhuYanSeRandomColorJsonData.OnInitFinishAction();
			}
		}

		// Token: 0x0600445D RID: 17501 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004607 RID: 17927
		public static Dictionary<int, YanZhuYanSeRandomColorJsonData> DataDict = new Dictionary<int, YanZhuYanSeRandomColorJsonData>();

		// Token: 0x04004608 RID: 17928
		public static List<YanZhuYanSeRandomColorJsonData> DataList = new List<YanZhuYanSeRandomColorJsonData>();

		// Token: 0x04004609 RID: 17929
		public static Action OnInitFinishAction = new Action(YanZhuYanSeRandomColorJsonData.OnInitFinish);

		// Token: 0x0400460A RID: 17930
		public int id;

		// Token: 0x0400460B RID: 17931
		public int R;

		// Token: 0x0400460C RID: 17932
		public int G;

		// Token: 0x0400460D RID: 17933
		public int B;

		// Token: 0x0400460E RID: 17934
		public string beizhu;
	}
}
