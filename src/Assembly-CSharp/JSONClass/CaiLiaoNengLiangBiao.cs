using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000B99 RID: 2969
	public class CaiLiaoNengLiangBiao : IJSONClass
	{
		// Token: 0x060049CC RID: 18892 RVA: 0x001F46E4 File Offset: 0x001F28E4
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.CaiLiaoNengLiangBiao.list)
			{
				try
				{
					CaiLiaoNengLiangBiao caiLiaoNengLiangBiao = new CaiLiaoNengLiangBiao();
					caiLiaoNengLiangBiao.id = jsonobject["id"].I;
					caiLiaoNengLiangBiao.value1 = jsonobject["value1"].I;
					if (CaiLiaoNengLiangBiao.DataDict.ContainsKey(caiLiaoNengLiangBiao.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典CaiLiaoNengLiangBiao.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", caiLiaoNengLiangBiao.id));
					}
					else
					{
						CaiLiaoNengLiangBiao.DataDict.Add(caiLiaoNengLiangBiao.id, caiLiaoNengLiangBiao);
						CaiLiaoNengLiangBiao.DataList.Add(caiLiaoNengLiangBiao);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典CaiLiaoNengLiangBiao.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (CaiLiaoNengLiangBiao.OnInitFinishAction != null)
			{
				CaiLiaoNengLiangBiao.OnInitFinishAction();
			}
		}

		// Token: 0x060049CD RID: 18893 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x040044A1 RID: 17569
		public static Dictionary<int, CaiLiaoNengLiangBiao> DataDict = new Dictionary<int, CaiLiaoNengLiangBiao>();

		// Token: 0x040044A2 RID: 17570
		public static List<CaiLiaoNengLiangBiao> DataList = new List<CaiLiaoNengLiangBiao>();

		// Token: 0x040044A3 RID: 17571
		public static Action OnInitFinishAction = new Action(CaiLiaoNengLiangBiao.OnInitFinish);

		// Token: 0x040044A4 RID: 17572
		public int id;

		// Token: 0x040044A5 RID: 17573
		public int value1;
	}
}
