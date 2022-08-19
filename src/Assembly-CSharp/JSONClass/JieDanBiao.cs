using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x0200086E RID: 2158
	public class JieDanBiao : IJSONClass
	{
		// Token: 0x06003FCB RID: 16331 RVA: 0x001B35D8 File Offset: 0x001B17D8
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

		// Token: 0x06003FCC RID: 16332 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003C59 RID: 15449
		public static Dictionary<int, JieDanBiao> DataDict = new Dictionary<int, JieDanBiao>();

		// Token: 0x04003C5A RID: 15450
		public static List<JieDanBiao> DataList = new List<JieDanBiao>();

		// Token: 0x04003C5B RID: 15451
		public static Action OnInitFinishAction = new Action(JieDanBiao.OnInitFinish);

		// Token: 0x04003C5C RID: 15452
		public int id;

		// Token: 0x04003C5D RID: 15453
		public int JinDanQuality;

		// Token: 0x04003C5E RID: 15454
		public int HP;

		// Token: 0x04003C5F RID: 15455
		public int EXP;

		// Token: 0x04003C60 RID: 15456
		public string name;

		// Token: 0x04003C61 RID: 15457
		public string desc;

		// Token: 0x04003C62 RID: 15458
		public List<int> JinDanType = new List<int>();

		// Token: 0x04003C63 RID: 15459
		public List<int> LinGengType = new List<int>();

		// Token: 0x04003C64 RID: 15460
		public List<int> LinGengZongShu = new List<int>();

		// Token: 0x04003C65 RID: 15461
		public List<int> seid = new List<int>();
	}
}
