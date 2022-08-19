using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000802 RID: 2050
	public class CaiLiaoNengLiangBiao : IJSONClass
	{
		// Token: 0x06003E1A RID: 15898 RVA: 0x001A8BA8 File Offset: 0x001A6DA8
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

		// Token: 0x06003E1B RID: 15899 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003911 RID: 14609
		public static Dictionary<int, CaiLiaoNengLiangBiao> DataDict = new Dictionary<int, CaiLiaoNengLiangBiao>();

		// Token: 0x04003912 RID: 14610
		public static List<CaiLiaoNengLiangBiao> DataList = new List<CaiLiaoNengLiangBiao>();

		// Token: 0x04003913 RID: 14611
		public static Action OnInitFinishAction = new Action(CaiLiaoNengLiangBiao.OnInitFinish);

		// Token: 0x04003914 RID: 14612
		public int id;

		// Token: 0x04003915 RID: 14613
		public int value1;
	}
}
