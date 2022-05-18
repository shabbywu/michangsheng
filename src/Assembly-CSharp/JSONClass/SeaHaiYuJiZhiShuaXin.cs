using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000C61 RID: 3169
	public class SeaHaiYuJiZhiShuaXin : IJSONClass
	{
		// Token: 0x06004CED RID: 19693 RVA: 0x00208044 File Offset: 0x00206244
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SeaHaiYuJiZhiShuaXin.list)
			{
				try
				{
					SeaHaiYuJiZhiShuaXin seaHaiYuJiZhiShuaXin = new SeaHaiYuJiZhiShuaXin();
					seaHaiYuJiZhiShuaXin.id = jsonobject["id"].I;
					seaHaiYuJiZhiShuaXin.ID = jsonobject["ID"].ToList();
					seaHaiYuJiZhiShuaXin.CD = jsonobject["CD"].ToList();
					seaHaiYuJiZhiShuaXin.WeiZhi = jsonobject["WeiZhi"].ToList();
					if (SeaHaiYuJiZhiShuaXin.DataDict.ContainsKey(seaHaiYuJiZhiShuaXin.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SeaHaiYuJiZhiShuaXin.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", seaHaiYuJiZhiShuaXin.id));
					}
					else
					{
						SeaHaiYuJiZhiShuaXin.DataDict.Add(seaHaiYuJiZhiShuaXin.id, seaHaiYuJiZhiShuaXin);
						SeaHaiYuJiZhiShuaXin.DataList.Add(seaHaiYuJiZhiShuaXin);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SeaHaiYuJiZhiShuaXin.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SeaHaiYuJiZhiShuaXin.OnInitFinishAction != null)
			{
				SeaHaiYuJiZhiShuaXin.OnInitFinishAction();
			}
		}

		// Token: 0x06004CEE RID: 19694 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004BF1 RID: 19441
		public static Dictionary<int, SeaHaiYuJiZhiShuaXin> DataDict = new Dictionary<int, SeaHaiYuJiZhiShuaXin>();

		// Token: 0x04004BF2 RID: 19442
		public static List<SeaHaiYuJiZhiShuaXin> DataList = new List<SeaHaiYuJiZhiShuaXin>();

		// Token: 0x04004BF3 RID: 19443
		public static Action OnInitFinishAction = new Action(SeaHaiYuJiZhiShuaXin.OnInitFinish);

		// Token: 0x04004BF4 RID: 19444
		public int id;

		// Token: 0x04004BF5 RID: 19445
		public List<int> ID = new List<int>();

		// Token: 0x04004BF6 RID: 19446
		public List<int> CD = new List<int>();

		// Token: 0x04004BF7 RID: 19447
		public List<int> WeiZhi = new List<int>();
	}
}
