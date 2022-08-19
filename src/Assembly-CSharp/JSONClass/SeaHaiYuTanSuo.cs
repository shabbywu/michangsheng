using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x020008D5 RID: 2261
	public class SeaHaiYuTanSuo : IJSONClass
	{
		// Token: 0x06004167 RID: 16743 RVA: 0x001BFD54 File Offset: 0x001BDF54
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SeaHaiYuTanSuo.list)
			{
				try
				{
					SeaHaiYuTanSuo seaHaiYuTanSuo = new SeaHaiYuTanSuo();
					seaHaiYuTanSuo.id = jsonobject["id"].I;
					seaHaiYuTanSuo.IsTanSuo = jsonobject["IsTanSuo"].I;
					seaHaiYuTanSuo.Value = jsonobject["Value"].I;
					seaHaiYuTanSuo.ZuoBiao = jsonobject["ZuoBiao"].I;
					if (SeaHaiYuTanSuo.DataDict.ContainsKey(seaHaiYuTanSuo.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SeaHaiYuTanSuo.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", seaHaiYuTanSuo.id));
					}
					else
					{
						SeaHaiYuTanSuo.DataDict.Add(seaHaiYuTanSuo.id, seaHaiYuTanSuo);
						SeaHaiYuTanSuo.DataList.Add(seaHaiYuTanSuo);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SeaHaiYuTanSuo.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SeaHaiYuTanSuo.OnInitFinishAction != null)
			{
				SeaHaiYuTanSuo.OnInitFinishAction();
			}
		}

		// Token: 0x06004168 RID: 16744 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x040040AE RID: 16558
		public static Dictionary<int, SeaHaiYuTanSuo> DataDict = new Dictionary<int, SeaHaiYuTanSuo>();

		// Token: 0x040040AF RID: 16559
		public static List<SeaHaiYuTanSuo> DataList = new List<SeaHaiYuTanSuo>();

		// Token: 0x040040B0 RID: 16560
		public static Action OnInitFinishAction = new Action(SeaHaiYuTanSuo.OnInitFinish);

		// Token: 0x040040B1 RID: 16561
		public int id;

		// Token: 0x040040B2 RID: 16562
		public int IsTanSuo;

		// Token: 0x040040B3 RID: 16563
		public int Value;

		// Token: 0x040040B4 RID: 16564
		public int ZuoBiao;
	}
}
