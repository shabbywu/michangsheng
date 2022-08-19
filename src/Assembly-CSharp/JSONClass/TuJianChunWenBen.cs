using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x0200097B RID: 2427
	public class TuJianChunWenBen : IJSONClass
	{
		// Token: 0x06004400 RID: 17408 RVA: 0x001CF76C File Offset: 0x001CD96C
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.TuJianChunWenBen.list)
			{
				try
				{
					TuJianChunWenBen tuJianChunWenBen = new TuJianChunWenBen();
					tuJianChunWenBen.id = jsonobject["id"].I;
					tuJianChunWenBen.typenum = jsonobject["typenum"].I;
					tuJianChunWenBen.type = jsonobject["type"].I;
					tuJianChunWenBen.name1 = jsonobject["name1"].Str;
					tuJianChunWenBen.name2 = jsonobject["name2"].Str;
					tuJianChunWenBen.descr = jsonobject["descr"].Str;
					if (TuJianChunWenBen.DataDict.ContainsKey(tuJianChunWenBen.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典TuJianChunWenBen.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", tuJianChunWenBen.id));
					}
					else
					{
						TuJianChunWenBen.DataDict.Add(tuJianChunWenBen.id, tuJianChunWenBen);
						TuJianChunWenBen.DataList.Add(tuJianChunWenBen);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典TuJianChunWenBen.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (TuJianChunWenBen.OnInitFinishAction != null)
			{
				TuJianChunWenBen.OnInitFinishAction();
			}
		}

		// Token: 0x06004401 RID: 17409 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004562 RID: 17762
		public static Dictionary<int, TuJianChunWenBen> DataDict = new Dictionary<int, TuJianChunWenBen>();

		// Token: 0x04004563 RID: 17763
		public static List<TuJianChunWenBen> DataList = new List<TuJianChunWenBen>();

		// Token: 0x04004564 RID: 17764
		public static Action OnInitFinishAction = new Action(TuJianChunWenBen.OnInitFinish);

		// Token: 0x04004565 RID: 17765
		public int id;

		// Token: 0x04004566 RID: 17766
		public int typenum;

		// Token: 0x04004567 RID: 17767
		public int type;

		// Token: 0x04004568 RID: 17768
		public string name1;

		// Token: 0x04004569 RID: 17769
		public string name2;

		// Token: 0x0400456A RID: 17770
		public string descr;
	}
}
