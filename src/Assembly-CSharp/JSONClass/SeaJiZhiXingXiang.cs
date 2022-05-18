using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000C64 RID: 3172
	public class SeaJiZhiXingXiang : IJSONClass
	{
		// Token: 0x06004CF9 RID: 19705 RVA: 0x00208478 File Offset: 0x00206678
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

		// Token: 0x06004CFA RID: 19706 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004C08 RID: 19464
		public static Dictionary<int, SeaJiZhiXingXiang> DataDict = new Dictionary<int, SeaJiZhiXingXiang>();

		// Token: 0x04004C09 RID: 19465
		public static List<SeaJiZhiXingXiang> DataList = new List<SeaJiZhiXingXiang>();

		// Token: 0x04004C0A RID: 19466
		public static Action OnInitFinishAction = new Action(SeaJiZhiXingXiang.OnInitFinish);

		// Token: 0x04004C0B RID: 19467
		public int id;

		// Token: 0x04004C0C RID: 19468
		public int OffsetX;

		// Token: 0x04004C0D RID: 19469
		public int OffsetY;

		// Token: 0x04004C0E RID: 19470
		public int ScaleX;

		// Token: 0x04004C0F RID: 19471
		public int ScaleY;

		// Token: 0x04004C10 RID: 19472
		public string Skin;

		// Token: 0x04004C11 RID: 19473
		public string Anim;
	}
}
