using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000C00 RID: 3072
	public class LianDanDanFangBiao : IJSONClass
	{
		// Token: 0x06004B69 RID: 19305 RVA: 0x001FD728 File Offset: 0x001FB928
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.LianDanDanFangBiao.list)
			{
				try
				{
					LianDanDanFangBiao lianDanDanFangBiao = new LianDanDanFangBiao();
					lianDanDanFangBiao.id = jsonobject["id"].I;
					lianDanDanFangBiao.ItemID = jsonobject["ItemID"].I;
					lianDanDanFangBiao.value1 = jsonobject["value1"].I;
					lianDanDanFangBiao.num1 = jsonobject["num1"].I;
					lianDanDanFangBiao.value2 = jsonobject["value2"].I;
					lianDanDanFangBiao.num2 = jsonobject["num2"].I;
					lianDanDanFangBiao.value3 = jsonobject["value3"].I;
					lianDanDanFangBiao.num3 = jsonobject["num3"].I;
					lianDanDanFangBiao.value4 = jsonobject["value4"].I;
					lianDanDanFangBiao.num4 = jsonobject["num4"].I;
					lianDanDanFangBiao.value5 = jsonobject["value5"].I;
					lianDanDanFangBiao.num5 = jsonobject["num5"].I;
					lianDanDanFangBiao.castTime = jsonobject["castTime"].I;
					lianDanDanFangBiao.name = jsonobject["name"].Str;
					if (LianDanDanFangBiao.DataDict.ContainsKey(lianDanDanFangBiao.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典LianDanDanFangBiao.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", lianDanDanFangBiao.id));
					}
					else
					{
						LianDanDanFangBiao.DataDict.Add(lianDanDanFangBiao.id, lianDanDanFangBiao);
						LianDanDanFangBiao.DataList.Add(lianDanDanFangBiao);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典LianDanDanFangBiao.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (LianDanDanFangBiao.OnInitFinishAction != null)
			{
				LianDanDanFangBiao.OnInitFinishAction();
			}
		}

		// Token: 0x06004B6A RID: 19306 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x040047D9 RID: 18393
		public static Dictionary<int, LianDanDanFangBiao> DataDict = new Dictionary<int, LianDanDanFangBiao>();

		// Token: 0x040047DA RID: 18394
		public static List<LianDanDanFangBiao> DataList = new List<LianDanDanFangBiao>();

		// Token: 0x040047DB RID: 18395
		public static Action OnInitFinishAction = new Action(LianDanDanFangBiao.OnInitFinish);

		// Token: 0x040047DC RID: 18396
		public int id;

		// Token: 0x040047DD RID: 18397
		public int ItemID;

		// Token: 0x040047DE RID: 18398
		public int value1;

		// Token: 0x040047DF RID: 18399
		public int num1;

		// Token: 0x040047E0 RID: 18400
		public int value2;

		// Token: 0x040047E1 RID: 18401
		public int num2;

		// Token: 0x040047E2 RID: 18402
		public int value3;

		// Token: 0x040047E3 RID: 18403
		public int num3;

		// Token: 0x040047E4 RID: 18404
		public int value4;

		// Token: 0x040047E5 RID: 18405
		public int num4;

		// Token: 0x040047E6 RID: 18406
		public int value5;

		// Token: 0x040047E7 RID: 18407
		public int num5;

		// Token: 0x040047E8 RID: 18408
		public int castTime;

		// Token: 0x040047E9 RID: 18409
		public string name;
	}
}
