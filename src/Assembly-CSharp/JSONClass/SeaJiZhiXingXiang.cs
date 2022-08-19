using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x020008D7 RID: 2263
	public class SeaJiZhiXingXiang : IJSONClass
	{
		// Token: 0x0600416F RID: 16751 RVA: 0x001C009C File Offset: 0x001BE29C
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SeaJiZhiXingXiang.list)
			{
				try
				{
					SeaJiZhiXingXiang seaJiZhiXingXiang = new SeaJiZhiXingXiang();
					seaJiZhiXingXiang.id = jsonobject["id"].I;
					seaJiZhiXingXiang.OffsetX = jsonobject["OffsetX"].I;
					seaJiZhiXingXiang.OffsetY = jsonobject["OffsetY"].I;
					seaJiZhiXingXiang.ScaleX = jsonobject["ScaleX"].I;
					seaJiZhiXingXiang.ScaleY = jsonobject["ScaleY"].I;
					seaJiZhiXingXiang.Skin = jsonobject["Skin"].Str;
					seaJiZhiXingXiang.Anim = jsonobject["Anim"].Str;
					if (SeaJiZhiXingXiang.DataDict.ContainsKey(seaJiZhiXingXiang.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SeaJiZhiXingXiang.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", seaJiZhiXingXiang.id));
					}
					else
					{
						SeaJiZhiXingXiang.DataDict.Add(seaJiZhiXingXiang.id, seaJiZhiXingXiang);
						SeaJiZhiXingXiang.DataList.Add(seaJiZhiXingXiang);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SeaJiZhiXingXiang.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SeaJiZhiXingXiang.OnInitFinishAction != null)
			{
				SeaJiZhiXingXiang.OnInitFinishAction();
			}
		}

		// Token: 0x06004170 RID: 16752 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x040040BE RID: 16574
		public static Dictionary<int, SeaJiZhiXingXiang> DataDict = new Dictionary<int, SeaJiZhiXingXiang>();

		// Token: 0x040040BF RID: 16575
		public static List<SeaJiZhiXingXiang> DataList = new List<SeaJiZhiXingXiang>();

		// Token: 0x040040C0 RID: 16576
		public static Action OnInitFinishAction = new Action(SeaJiZhiXingXiang.OnInitFinish);

		// Token: 0x040040C1 RID: 16577
		public int id;

		// Token: 0x040040C2 RID: 16578
		public int OffsetX;

		// Token: 0x040040C3 RID: 16579
		public int OffsetY;

		// Token: 0x040040C4 RID: 16580
		public int ScaleX;

		// Token: 0x040040C5 RID: 16581
		public int ScaleY;

		// Token: 0x040040C6 RID: 16582
		public string Skin;

		// Token: 0x040040C7 RID: 16583
		public string Anim;
	}
}
