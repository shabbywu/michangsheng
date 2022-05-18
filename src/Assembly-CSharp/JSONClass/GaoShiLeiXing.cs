using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000BD4 RID: 3028
	public class GaoShiLeiXing : IJSONClass
	{
		// Token: 0x06004AB8 RID: 19128 RVA: 0x001F9BBC File Offset: 0x001F7DBC
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.GaoShiLeiXing.list)
			{
				try
				{
					GaoShiLeiXing gaoShiLeiXing = new GaoShiLeiXing();
					gaoShiLeiXing.cd = jsonobject["cd"].I;
					gaoShiLeiXing.id = jsonobject["id"].Str;
					gaoShiLeiXing.name = jsonobject["name"].Str;
					gaoShiLeiXing.num = jsonobject["num"].ToList();
					gaoShiLeiXing.qujian = jsonobject["qujian"].ToList();
					if (GaoShiLeiXing.DataDict.ContainsKey(gaoShiLeiXing.id))
					{
						PreloadManager.LogException("!!!错误!!!向字典GaoShiLeiXing.DataDict添加数据时出现重复的键，Key:" + gaoShiLeiXing.id + "，已跳过，请检查配表");
					}
					else
					{
						GaoShiLeiXing.DataDict.Add(gaoShiLeiXing.id, gaoShiLeiXing);
						GaoShiLeiXing.DataList.Add(gaoShiLeiXing);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典GaoShiLeiXing.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (GaoShiLeiXing.OnInitFinishAction != null)
			{
				GaoShiLeiXing.OnInitFinishAction();
			}
		}

		// Token: 0x06004AB9 RID: 19129 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004687 RID: 18055
		public static Dictionary<string, GaoShiLeiXing> DataDict = new Dictionary<string, GaoShiLeiXing>();

		// Token: 0x04004688 RID: 18056
		public static List<GaoShiLeiXing> DataList = new List<GaoShiLeiXing>();

		// Token: 0x04004689 RID: 18057
		public static Action OnInitFinishAction = new Action(GaoShiLeiXing.OnInitFinish);

		// Token: 0x0400468A RID: 18058
		public int cd;

		// Token: 0x0400468B RID: 18059
		public string id;

		// Token: 0x0400468C RID: 18060
		public string name;

		// Token: 0x0400468D RID: 18061
		public List<int> num = new List<int>();

		// Token: 0x0400468E RID: 18062
		public List<int> qujian = new List<int>();
	}
}
