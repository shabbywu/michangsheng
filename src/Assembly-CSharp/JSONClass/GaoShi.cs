using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000BD3 RID: 3027
	public class GaoShi : IJSONClass
	{
		// Token: 0x06004AB4 RID: 19124 RVA: 0x001F99E4 File Offset: 0x001F7BE4
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

		// Token: 0x06004AB5 RID: 19125 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x0400467B RID: 18043
		public static Dictionary<int, GaoShi> DataDict = new Dictionary<int, GaoShi>();

		// Token: 0x0400467C RID: 18044
		public static List<GaoShi> DataList = new List<GaoShi>();

		// Token: 0x0400467D RID: 18045
		public static Action OnInitFinishAction = new Action(GaoShi.OnInitFinish);

		// Token: 0x0400467E RID: 18046
		public int id;

		// Token: 0x0400467F RID: 18047
		public int itemid;

		// Token: 0x04004680 RID: 18048
		public int type;

		// Token: 0x04004681 RID: 18049
		public int num;

		// Token: 0x04004682 RID: 18050
		public int jiagexishu;

		// Token: 0x04004683 RID: 18051
		public int shengwangid;

		// Token: 0x04004684 RID: 18052
		public int shengwang;

		// Token: 0x04004685 RID: 18053
		public int taskid;

		// Token: 0x04004686 RID: 18054
		public int menpaihuobi;
	}
}
