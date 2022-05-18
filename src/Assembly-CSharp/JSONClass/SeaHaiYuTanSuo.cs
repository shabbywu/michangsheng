using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000C62 RID: 3170
	public class SeaHaiYuTanSuo : IJSONClass
	{
		// Token: 0x06004CF1 RID: 19697 RVA: 0x00208194 File Offset: 0x00206394
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

		// Token: 0x06004CF2 RID: 19698 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004BF8 RID: 19448
		public static Dictionary<int, SeaHaiYuTanSuo> DataDict = new Dictionary<int, SeaHaiYuTanSuo>();

		// Token: 0x04004BF9 RID: 19449
		public static List<SeaHaiYuTanSuo> DataList = new List<SeaHaiYuTanSuo>();

		// Token: 0x04004BFA RID: 19450
		public static Action OnInitFinishAction = new Action(SeaHaiYuTanSuo.OnInitFinish);

		// Token: 0x04004BFB RID: 19451
		public int id;

		// Token: 0x04004BFC RID: 19452
		public int IsTanSuo;

		// Token: 0x04004BFD RID: 19453
		public int Value;

		// Token: 0x04004BFE RID: 19454
		public int ZuoBiao;
	}
}
