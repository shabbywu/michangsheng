using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000CF7 RID: 3319
	public class TianJiDaBi : IJSONClass
	{
		// Token: 0x06004F44 RID: 20292 RVA: 0x00214080 File Offset: 0x00212280
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

		// Token: 0x06004F45 RID: 20293 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04005027 RID: 20519
		public static Dictionary<int, TianJiDaBi> DataDict = new Dictionary<int, TianJiDaBi>();

		// Token: 0x04005028 RID: 20520
		public static List<TianJiDaBi> DataList = new List<TianJiDaBi>();

		// Token: 0x04005029 RID: 20521
		public static Action OnInitFinishAction = new Action(TianJiDaBi.OnInitFinish);

		// Token: 0x0400502A RID: 20522
		public int id;

		// Token: 0x0400502B RID: 20523
		public int YouXian;

		// Token: 0x0400502C RID: 20524
		public int LiuPai;
	}
}
