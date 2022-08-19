using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x020008D4 RID: 2260
	public class SeaHaiYuJiZhiShuaXin : IJSONClass
	{
		// Token: 0x06004163 RID: 16739 RVA: 0x001BFBB4 File Offset: 0x001BDDB4
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

		// Token: 0x06004164 RID: 16740 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x040040A7 RID: 16551
		public static Dictionary<int, SeaHaiYuJiZhiShuaXin> DataDict = new Dictionary<int, SeaHaiYuJiZhiShuaXin>();

		// Token: 0x040040A8 RID: 16552
		public static List<SeaHaiYuJiZhiShuaXin> DataList = new List<SeaHaiYuJiZhiShuaXin>();

		// Token: 0x040040A9 RID: 16553
		public static Action OnInitFinishAction = new Action(SeaHaiYuJiZhiShuaXin.OnInitFinish);

		// Token: 0x040040AA RID: 16554
		public int id;

		// Token: 0x040040AB RID: 16555
		public List<int> ID = new List<int>();

		// Token: 0x040040AC RID: 16556
		public List<int> CD = new List<int>();

		// Token: 0x040040AD RID: 16557
		public List<int> WeiZhi = new List<int>();
	}
}
