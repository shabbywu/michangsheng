using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000843 RID: 2115
	public class GaoShiLeiXing : IJSONClass
	{
		// Token: 0x06003F1E RID: 16158 RVA: 0x001AF3F8 File Offset: 0x001AD5F8
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

		// Token: 0x06003F1F RID: 16159 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003B19 RID: 15129
		public static Dictionary<string, GaoShiLeiXing> DataDict = new Dictionary<string, GaoShiLeiXing>();

		// Token: 0x04003B1A RID: 15130
		public static List<GaoShiLeiXing> DataList = new List<GaoShiLeiXing>();

		// Token: 0x04003B1B RID: 15131
		public static Action OnInitFinishAction = new Action(GaoShiLeiXing.OnInitFinish);

		// Token: 0x04003B1C RID: 15132
		public int cd;

		// Token: 0x04003B1D RID: 15133
		public string id;

		// Token: 0x04003B1E RID: 15134
		public string name;

		// Token: 0x04003B1F RID: 15135
		public List<int> num = new List<int>();

		// Token: 0x04003B20 RID: 15136
		public List<int> qujian = new List<int>();
	}
}
