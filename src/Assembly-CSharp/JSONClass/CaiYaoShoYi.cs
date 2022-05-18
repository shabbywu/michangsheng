using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000B9B RID: 2971
	public class CaiYaoShoYi : IJSONClass
	{
		// Token: 0x060049D4 RID: 18900 RVA: 0x001F4A4C File Offset: 0x001F2C4C
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

		// Token: 0x060049D5 RID: 18901 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x040044B7 RID: 17591
		public static Dictionary<int, CaiYaoShoYi> DataDict = new Dictionary<int, CaiYaoShoYi>();

		// Token: 0x040044B8 RID: 17592
		public static List<CaiYaoShoYi> DataList = new List<CaiYaoShoYi>();

		// Token: 0x040044B9 RID: 17593
		public static Action OnInitFinishAction = new Action(CaiYaoShoYi.OnInitFinish);

		// Token: 0x040044BA RID: 17594
		public int id;

		// Token: 0x040044BB RID: 17595
		public string name;

		// Token: 0x040044BC RID: 17596
		public List<int> QuanZhon = new List<int>();
	}
}
