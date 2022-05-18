using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000C11 RID: 3089
	public class LingMaiPinJie : IJSONClass
	{
		// Token: 0x06004BAD RID: 19373 RVA: 0x001FF288 File Offset: 0x001FD488
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.LingMaiPinJie.list)
			{
				try
				{
					LingMaiPinJie lingMaiPinJie = new LingMaiPinJie();
					lingMaiPinJie.Id = jsonobject["Id"].I;
					lingMaiPinJie.ShouYiLv = jsonobject["ShouYiLv"].I;
					lingMaiPinJie.LingHeLv = jsonobject["LingHeLv"].I;
					lingMaiPinJie.ShengShiLv = jsonobject["ShengShiLv"].I;
					lingMaiPinJie.ShouYiDesc = jsonobject["ShouYiDesc"].Str;
					lingMaiPinJie.LingHeDesc = jsonobject["LingHeDesc"].Str;
					lingMaiPinJie.ShengShiDesc = jsonobject["ShengShiDesc"].Str;
					if (LingMaiPinJie.DataDict.ContainsKey(lingMaiPinJie.Id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典LingMaiPinJie.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", lingMaiPinJie.Id));
					}
					else
					{
						LingMaiPinJie.DataDict.Add(lingMaiPinJie.Id, lingMaiPinJie);
						LingMaiPinJie.DataList.Add(lingMaiPinJie);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典LingMaiPinJie.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (LingMaiPinJie.OnInitFinishAction != null)
			{
				LingMaiPinJie.OnInitFinishAction();
			}
		}

		// Token: 0x06004BAE RID: 19374 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x0400487A RID: 18554
		public static Dictionary<int, LingMaiPinJie> DataDict = new Dictionary<int, LingMaiPinJie>();

		// Token: 0x0400487B RID: 18555
		public static List<LingMaiPinJie> DataList = new List<LingMaiPinJie>();

		// Token: 0x0400487C RID: 18556
		public static Action OnInitFinishAction = new Action(LingMaiPinJie.OnInitFinish);

		// Token: 0x0400487D RID: 18557
		public int Id;

		// Token: 0x0400487E RID: 18558
		public int ShouYiLv;

		// Token: 0x0400487F RID: 18559
		public int LingHeLv;

		// Token: 0x04004880 RID: 18560
		public int ShengShiLv;

		// Token: 0x04004881 RID: 18561
		public string ShouYiDesc;

		// Token: 0x04004882 RID: 18562
		public string LingHeDesc;

		// Token: 0x04004883 RID: 18563
		public string ShengShiDesc;
	}
}
