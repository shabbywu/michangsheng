using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000973 RID: 2419
	public class TianJiDaBi : IJSONClass
	{
		// Token: 0x060043DE RID: 17374 RVA: 0x001CE758 File Offset: 0x001CC958
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.TianJiDaBi.list)
			{
				try
				{
					TianJiDaBi tianJiDaBi = new TianJiDaBi();
					tianJiDaBi.id = jsonobject["id"].I;
					tianJiDaBi.YouXian = jsonobject["YouXian"].I;
					tianJiDaBi.LiuPai = jsonobject["LiuPai"].I;
					if (TianJiDaBi.DataDict.ContainsKey(tianJiDaBi.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典TianJiDaBi.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", tianJiDaBi.id));
					}
					else
					{
						TianJiDaBi.DataDict.Add(tianJiDaBi.id, tianJiDaBi);
						TianJiDaBi.DataList.Add(tianJiDaBi);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典TianJiDaBi.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (TianJiDaBi.OnInitFinishAction != null)
			{
				TianJiDaBi.OnInitFinishAction();
			}
		}

		// Token: 0x060043DF RID: 17375 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004517 RID: 17687
		public static Dictionary<int, TianJiDaBi> DataDict = new Dictionary<int, TianJiDaBi>();

		// Token: 0x04004518 RID: 17688
		public static List<TianJiDaBi> DataList = new List<TianJiDaBi>();

		// Token: 0x04004519 RID: 17689
		public static Action OnInitFinishAction = new Action(TianJiDaBi.OnInitFinish);

		// Token: 0x0400451A RID: 17690
		public int id;

		// Token: 0x0400451B RID: 17691
		public int YouXian;

		// Token: 0x0400451C RID: 17692
		public int LiuPai;
	}
}
