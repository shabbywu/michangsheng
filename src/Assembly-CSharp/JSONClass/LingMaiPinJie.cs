using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000883 RID: 2179
	public class LingMaiPinJie : IJSONClass
	{
		// Token: 0x0600401F RID: 16415 RVA: 0x001B5B50 File Offset: 0x001B3D50
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

		// Token: 0x06004020 RID: 16416 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003D21 RID: 15649
		public static Dictionary<int, LingMaiPinJie> DataDict = new Dictionary<int, LingMaiPinJie>();

		// Token: 0x04003D22 RID: 15650
		public static List<LingMaiPinJie> DataList = new List<LingMaiPinJie>();

		// Token: 0x04003D23 RID: 15651
		public static Action OnInitFinishAction = new Action(LingMaiPinJie.OnInitFinish);

		// Token: 0x04003D24 RID: 15652
		public int Id;

		// Token: 0x04003D25 RID: 15653
		public int ShouYiLv;

		// Token: 0x04003D26 RID: 15654
		public int LingHeLv;

		// Token: 0x04003D27 RID: 15655
		public int ShengShiLv;

		// Token: 0x04003D28 RID: 15656
		public string ShouYiDesc;

		// Token: 0x04003D29 RID: 15657
		public string LingHeDesc;

		// Token: 0x04003D2A RID: 15658
		public string ShengShiDesc;
	}
}
