using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000BFC RID: 3068
	public class JieDanBiao : IJSONClass
	{
		// Token: 0x06004B59 RID: 19289 RVA: 0x001FD0D4 File Offset: 0x001FB2D4
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.JieDanBiao.list)
			{
				try
				{
					JieDanBiao jieDanBiao = new JieDanBiao();
					jieDanBiao.id = jsonobject["id"].I;
					jieDanBiao.JinDanQuality = jsonobject["JinDanQuality"].I;
					jieDanBiao.HP = jsonobject["HP"].I;
					jieDanBiao.EXP = jsonobject["EXP"].I;
					jieDanBiao.name = jsonobject["name"].Str;
					jieDanBiao.desc = jsonobject["desc"].Str;
					jieDanBiao.JinDanType = jsonobject["JinDanType"].ToList();
					jieDanBiao.LinGengType = jsonobject["LinGengType"].ToList();
					jieDanBiao.LinGengZongShu = jsonobject["LinGengZongShu"].ToList();
					jieDanBiao.seid = jsonobject["seid"].ToList();
					if (JieDanBiao.DataDict.ContainsKey(jieDanBiao.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典JieDanBiao.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", jieDanBiao.id));
					}
					else
					{
						JieDanBiao.DataDict.Add(jieDanBiao.id, jieDanBiao);
						JieDanBiao.DataList.Add(jieDanBiao);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典JieDanBiao.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (JieDanBiao.OnInitFinishAction != null)
			{
				JieDanBiao.OnInitFinishAction();
			}
		}

		// Token: 0x06004B5A RID: 19290 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x040047B2 RID: 18354
		public static Dictionary<int, JieDanBiao> DataDict = new Dictionary<int, JieDanBiao>();

		// Token: 0x040047B3 RID: 18355
		public static List<JieDanBiao> DataList = new List<JieDanBiao>();

		// Token: 0x040047B4 RID: 18356
		public static Action OnInitFinishAction = new Action(JieDanBiao.OnInitFinish);

		// Token: 0x040047B5 RID: 18357
		public int id;

		// Token: 0x040047B6 RID: 18358
		public int JinDanQuality;

		// Token: 0x040047B7 RID: 18359
		public int HP;

		// Token: 0x040047B8 RID: 18360
		public int EXP;

		// Token: 0x040047B9 RID: 18361
		public string name;

		// Token: 0x040047BA RID: 18362
		public string desc;

		// Token: 0x040047BB RID: 18363
		public List<int> JinDanType = new List<int>();

		// Token: 0x040047BC RID: 18364
		public List<int> LinGengType = new List<int>();

		// Token: 0x040047BD RID: 18365
		public List<int> LinGengZongShu = new List<int>();

		// Token: 0x040047BE RID: 18366
		public List<int> seid = new List<int>();
	}
}
