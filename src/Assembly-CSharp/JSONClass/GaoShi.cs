using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000842 RID: 2114
	public class GaoShi : IJSONClass
	{
		// Token: 0x06003F1A RID: 16154 RVA: 0x001AF1F8 File Offset: 0x001AD3F8
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.GaoShi.list)
			{
				try
				{
					GaoShi gaoShi = new GaoShi();
					gaoShi.id = jsonobject["id"].I;
					gaoShi.itemid = jsonobject["itemid"].I;
					gaoShi.type = jsonobject["type"].I;
					gaoShi.num = jsonobject["num"].I;
					gaoShi.jiagexishu = jsonobject["jiagexishu"].I;
					gaoShi.shengwangid = jsonobject["shengwangid"].I;
					gaoShi.shengwang = jsonobject["shengwang"].I;
					gaoShi.taskid = jsonobject["taskid"].I;
					gaoShi.menpaihuobi = jsonobject["menpaihuobi"].I;
					if (GaoShi.DataDict.ContainsKey(gaoShi.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典GaoShi.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", gaoShi.id));
					}
					else
					{
						GaoShi.DataDict.Add(gaoShi.id, gaoShi);
						GaoShi.DataList.Add(gaoShi);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典GaoShi.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (GaoShi.OnInitFinishAction != null)
			{
				GaoShi.OnInitFinishAction();
			}
		}

		// Token: 0x06003F1B RID: 16155 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003B0D RID: 15117
		public static Dictionary<int, GaoShi> DataDict = new Dictionary<int, GaoShi>();

		// Token: 0x04003B0E RID: 15118
		public static List<GaoShi> DataList = new List<GaoShi>();

		// Token: 0x04003B0F RID: 15119
		public static Action OnInitFinishAction = new Action(GaoShi.OnInitFinish);

		// Token: 0x04003B10 RID: 15120
		public int id;

		// Token: 0x04003B11 RID: 15121
		public int itemid;

		// Token: 0x04003B12 RID: 15122
		public int type;

		// Token: 0x04003B13 RID: 15123
		public int num;

		// Token: 0x04003B14 RID: 15124
		public int jiagexishu;

		// Token: 0x04003B15 RID: 15125
		public int shengwangid;

		// Token: 0x04003B16 RID: 15126
		public int shengwang;

		// Token: 0x04003B17 RID: 15127
		public int taskid;

		// Token: 0x04003B18 RID: 15128
		public int menpaihuobi;
	}
}
