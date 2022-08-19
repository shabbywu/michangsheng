using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000804 RID: 2052
	public class CaiYaoShoYi : IJSONClass
	{
		// Token: 0x06003E22 RID: 15906 RVA: 0x001A8F60 File Offset: 0x001A7160
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.CaiYaoShoYi.list)
			{
				try
				{
					CaiYaoShoYi caiYaoShoYi = new CaiYaoShoYi();
					caiYaoShoYi.id = jsonobject["id"].I;
					caiYaoShoYi.name = jsonobject["name"].Str;
					caiYaoShoYi.QuanZhon = jsonobject["QuanZhon"].ToList();
					if (CaiYaoShoYi.DataDict.ContainsKey(caiYaoShoYi.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典CaiYaoShoYi.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", caiYaoShoYi.id));
					}
					else
					{
						CaiYaoShoYi.DataDict.Add(caiYaoShoYi.id, caiYaoShoYi);
						CaiYaoShoYi.DataList.Add(caiYaoShoYi);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典CaiYaoShoYi.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (CaiYaoShoYi.OnInitFinishAction != null)
			{
				CaiYaoShoYi.OnInitFinishAction();
			}
		}

		// Token: 0x06003E23 RID: 15907 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003927 RID: 14631
		public static Dictionary<int, CaiYaoShoYi> DataDict = new Dictionary<int, CaiYaoShoYi>();

		// Token: 0x04003928 RID: 14632
		public static List<CaiYaoShoYi> DataList = new List<CaiYaoShoYi>();

		// Token: 0x04003929 RID: 14633
		public static Action OnInitFinishAction = new Action(CaiYaoShoYi.OnInitFinish);

		// Token: 0x0400392A RID: 14634
		public int id;

		// Token: 0x0400392B RID: 14635
		public string name;

		// Token: 0x0400392C RID: 14636
		public List<int> QuanZhon = new List<int>();
	}
}
