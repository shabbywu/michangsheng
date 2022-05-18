using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000BF9 RID: 3065
	public class JianLingXianSuo : IJSONClass
	{
		// Token: 0x06004B4C RID: 19276 RVA: 0x001FCC10 File Offset: 0x001FAE10
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

		// Token: 0x06004B4D RID: 19277 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x0400479A RID: 18330
		public static Dictionary<string, JianLingXianSuo> DataDict = new Dictionary<string, JianLingXianSuo>();

		// Token: 0x0400479B RID: 18331
		public static List<JianLingXianSuo> DataList = new List<JianLingXianSuo>();

		// Token: 0x0400479C RID: 18332
		public static Action OnInitFinishAction = new Action(JianLingXianSuo.OnInitFinish);

		// Token: 0x0400479D RID: 18333
		public int Type;

		// Token: 0x0400479E RID: 18334
		public int JiYi;

		// Token: 0x0400479F RID: 18335
		public int XianSuoLV;

		// Token: 0x040047A0 RID: 18336
		public string id;

		// Token: 0x040047A1 RID: 18337
		public string desc;

		// Token: 0x040047A2 RID: 18338
		public string XianSuoDuiHua1;

		// Token: 0x040047A3 RID: 18339
		public string XianSuoDuiHua2;
	}
}
