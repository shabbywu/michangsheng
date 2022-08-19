using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x0200082D RID: 2093
	public class DFZhenYanLevel : IJSONClass
	{
		// Token: 0x06003EC6 RID: 16070 RVA: 0x001AD204 File Offset: 0x001AB404
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.DFZhenYanLevel.list)
			{
				try
				{
					DFZhenYanLevel dfzhenYanLevel = new DFZhenYanLevel();
					dfzhenYanLevel.id = jsonobject["id"].I;
					dfzhenYanLevel.zhenpanlevel = jsonobject["zhenpanlevel"].I;
					dfzhenYanLevel.wudaolevel = jsonobject["wudaolevel"].I;
					dfzhenYanLevel.buzhenxiaohao = jsonobject["buzhenxiaohao"].I;
					dfzhenYanLevel.xiuliansudu = jsonobject["xiuliansudu"].I;
					dfzhenYanLevel.lingtiansudu = jsonobject["lingtiansudu"].I;
					dfzhenYanLevel.lingtiancuishengsudu = jsonobject["lingtiancuishengsudu"].I;
					dfzhenYanLevel.name = jsonobject["name"].Str;
					if (DFZhenYanLevel.DataDict.ContainsKey(dfzhenYanLevel.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典DFZhenYanLevel.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", dfzhenYanLevel.id));
					}
					else
					{
						DFZhenYanLevel.DataDict.Add(dfzhenYanLevel.id, dfzhenYanLevel);
						DFZhenYanLevel.DataList.Add(dfzhenYanLevel);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典DFZhenYanLevel.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (DFZhenYanLevel.OnInitFinishAction != null)
			{
				DFZhenYanLevel.OnInitFinishAction();
			}
		}

		// Token: 0x06003EC7 RID: 16071 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003A76 RID: 14966
		public static Dictionary<int, DFZhenYanLevel> DataDict = new Dictionary<int, DFZhenYanLevel>();

		// Token: 0x04003A77 RID: 14967
		public static List<DFZhenYanLevel> DataList = new List<DFZhenYanLevel>();

		// Token: 0x04003A78 RID: 14968
		public static Action OnInitFinishAction = new Action(DFZhenYanLevel.OnInitFinish);

		// Token: 0x04003A79 RID: 14969
		public int id;

		// Token: 0x04003A7A RID: 14970
		public int zhenpanlevel;

		// Token: 0x04003A7B RID: 14971
		public int wudaolevel;

		// Token: 0x04003A7C RID: 14972
		public int buzhenxiaohao;

		// Token: 0x04003A7D RID: 14973
		public int xiuliansudu;

		// Token: 0x04003A7E RID: 14974
		public int lingtiansudu;

		// Token: 0x04003A7F RID: 14975
		public int lingtiancuishengsudu;

		// Token: 0x04003A80 RID: 14976
		public string name;
	}
}
