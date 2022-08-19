using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000830 RID: 2096
	public class DongTaiChuanWenBaio : IJSONClass
	{
		// Token: 0x06003ED2 RID: 16082 RVA: 0x001AD6CC File Offset: 0x001AB8CC
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.DongTaiChuanWenBaio.list)
			{
				try
				{
					DongTaiChuanWenBaio dongTaiChuanWenBaio = new DongTaiChuanWenBaio();
					dongTaiChuanWenBaio.id = jsonobject["id"].I;
					dongTaiChuanWenBaio.cunZaiShiJian = jsonobject["cunZaiShiJian"].I;
					dongTaiChuanWenBaio.isshili = jsonobject["isshili"].I;
					dongTaiChuanWenBaio.text = jsonobject["text"].Str;
					if (DongTaiChuanWenBaio.DataDict.ContainsKey(dongTaiChuanWenBaio.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典DongTaiChuanWenBaio.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", dongTaiChuanWenBaio.id));
					}
					else
					{
						DongTaiChuanWenBaio.DataDict.Add(dongTaiChuanWenBaio.id, dongTaiChuanWenBaio);
						DongTaiChuanWenBaio.DataList.Add(dongTaiChuanWenBaio);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典DongTaiChuanWenBaio.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (DongTaiChuanWenBaio.OnInitFinishAction != null)
			{
				DongTaiChuanWenBaio.OnInitFinishAction();
			}
		}

		// Token: 0x06003ED3 RID: 16083 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003A8D RID: 14989
		public static Dictionary<int, DongTaiChuanWenBaio> DataDict = new Dictionary<int, DongTaiChuanWenBaio>();

		// Token: 0x04003A8E RID: 14990
		public static List<DongTaiChuanWenBaio> DataList = new List<DongTaiChuanWenBaio>();

		// Token: 0x04003A8F RID: 14991
		public static Action OnInitFinishAction = new Action(DongTaiChuanWenBaio.OnInitFinish);

		// Token: 0x04003A90 RID: 14992
		public int id;

		// Token: 0x04003A91 RID: 14993
		public int cunZaiShiJian;

		// Token: 0x04003A92 RID: 14994
		public int isshili;

		// Token: 0x04003A93 RID: 14995
		public string text;
	}
}
