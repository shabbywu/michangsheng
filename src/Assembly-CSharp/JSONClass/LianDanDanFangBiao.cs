using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000872 RID: 2162
	public class LianDanDanFangBiao : IJSONClass
	{
		// Token: 0x06003FDB RID: 16347 RVA: 0x001B3D38 File Offset: 0x001B1F38
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

		// Token: 0x06003FDC RID: 16348 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003C80 RID: 15488
		public static Dictionary<int, LianDanDanFangBiao> DataDict = new Dictionary<int, LianDanDanFangBiao>();

		// Token: 0x04003C81 RID: 15489
		public static List<LianDanDanFangBiao> DataList = new List<LianDanDanFangBiao>();

		// Token: 0x04003C82 RID: 15490
		public static Action OnInitFinishAction = new Action(LianDanDanFangBiao.OnInitFinish);

		// Token: 0x04003C83 RID: 15491
		public int id;

		// Token: 0x04003C84 RID: 15492
		public int ItemID;

		// Token: 0x04003C85 RID: 15493
		public int value1;

		// Token: 0x04003C86 RID: 15494
		public int num1;

		// Token: 0x04003C87 RID: 15495
		public int value2;

		// Token: 0x04003C88 RID: 15496
		public int num2;

		// Token: 0x04003C89 RID: 15497
		public int value3;

		// Token: 0x04003C8A RID: 15498
		public int num3;

		// Token: 0x04003C8B RID: 15499
		public int value4;

		// Token: 0x04003C8C RID: 15500
		public int num4;

		// Token: 0x04003C8D RID: 15501
		public int value5;

		// Token: 0x04003C8E RID: 15502
		public int num5;

		// Token: 0x04003C8F RID: 15503
		public int castTime;

		// Token: 0x04003C90 RID: 15504
		public string name;
	}
}
