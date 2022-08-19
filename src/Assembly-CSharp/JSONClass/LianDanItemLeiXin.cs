using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000873 RID: 2163
	public class LianDanItemLeiXin : IJSONClass
	{
		// Token: 0x06003FDF RID: 16351 RVA: 0x001B3FA4 File Offset: 0x001B21A4
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.LianDanItemLeiXin.list)
			{
				try
				{
					LianDanItemLeiXin lianDanItemLeiXin = new LianDanItemLeiXin();
					lianDanItemLeiXin.id = jsonobject["id"].I;
					lianDanItemLeiXin.name = jsonobject["name"].Str;
					lianDanItemLeiXin.desc = jsonobject["desc"].Str;
					if (LianDanItemLeiXin.DataDict.ContainsKey(lianDanItemLeiXin.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典LianDanItemLeiXin.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", lianDanItemLeiXin.id));
					}
					else
					{
						LianDanItemLeiXin.DataDict.Add(lianDanItemLeiXin.id, lianDanItemLeiXin);
						LianDanItemLeiXin.DataList.Add(lianDanItemLeiXin);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典LianDanItemLeiXin.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (LianDanItemLeiXin.OnInitFinishAction != null)
			{
				LianDanItemLeiXin.OnInitFinishAction();
			}
		}

		// Token: 0x06003FE0 RID: 16352 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003C91 RID: 15505
		public static Dictionary<int, LianDanItemLeiXin> DataDict = new Dictionary<int, LianDanItemLeiXin>();

		// Token: 0x04003C92 RID: 15506
		public static List<LianDanItemLeiXin> DataList = new List<LianDanItemLeiXin>();

		// Token: 0x04003C93 RID: 15507
		public static Action OnInitFinishAction = new Action(LianDanItemLeiXin.OnInitFinish);

		// Token: 0x04003C94 RID: 15508
		public int id;

		// Token: 0x04003C95 RID: 15509
		public string name;

		// Token: 0x04003C96 RID: 15510
		public string desc;
	}
}
