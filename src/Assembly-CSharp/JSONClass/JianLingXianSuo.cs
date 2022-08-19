using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x0200086B RID: 2155
	public class JianLingXianSuo : IJSONClass
	{
		// Token: 0x06003FBE RID: 16318 RVA: 0x001B309C File Offset: 0x001B129C
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.JianLingXianSuo.list)
			{
				try
				{
					JianLingXianSuo jianLingXianSuo = new JianLingXianSuo();
					jianLingXianSuo.Type = jsonobject["Type"].I;
					jianLingXianSuo.JiYi = jsonobject["JiYi"].I;
					jianLingXianSuo.XianSuoLV = jsonobject["XianSuoLV"].I;
					jianLingXianSuo.id = jsonobject["id"].Str;
					jianLingXianSuo.desc = jsonobject["desc"].Str;
					jianLingXianSuo.XianSuoDuiHua1 = jsonobject["XianSuoDuiHua1"].Str;
					jianLingXianSuo.XianSuoDuiHua2 = jsonobject["XianSuoDuiHua2"].Str;
					if (JianLingXianSuo.DataDict.ContainsKey(jianLingXianSuo.id))
					{
						PreloadManager.LogException("!!!错误!!!向字典JianLingXianSuo.DataDict添加数据时出现重复的键，Key:" + jianLingXianSuo.id + "，已跳过，请检查配表");
					}
					else
					{
						JianLingXianSuo.DataDict.Add(jianLingXianSuo.id, jianLingXianSuo);
						JianLingXianSuo.DataList.Add(jianLingXianSuo);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典JianLingXianSuo.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (JianLingXianSuo.OnInitFinishAction != null)
			{
				JianLingXianSuo.OnInitFinishAction();
			}
		}

		// Token: 0x06003FBF RID: 16319 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003C41 RID: 15425
		public static Dictionary<string, JianLingXianSuo> DataDict = new Dictionary<string, JianLingXianSuo>();

		// Token: 0x04003C42 RID: 15426
		public static List<JianLingXianSuo> DataList = new List<JianLingXianSuo>();

		// Token: 0x04003C43 RID: 15427
		public static Action OnInitFinishAction = new Action(JianLingXianSuo.OnInitFinish);

		// Token: 0x04003C44 RID: 15428
		public int Type;

		// Token: 0x04003C45 RID: 15429
		public int JiYi;

		// Token: 0x04003C46 RID: 15430
		public int XianSuoLV;

		// Token: 0x04003C47 RID: 15431
		public string id;

		// Token: 0x04003C48 RID: 15432
		public string desc;

		// Token: 0x04003C49 RID: 15433
		public string XianSuoDuiHua1;

		// Token: 0x04003C4A RID: 15434
		public string XianSuoDuiHua2;
	}
}
