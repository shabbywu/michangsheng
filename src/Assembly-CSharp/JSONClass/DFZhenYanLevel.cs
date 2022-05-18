using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000BC3 RID: 3011
	public class DFZhenYanLevel : IJSONClass
	{
		// Token: 0x06004A74 RID: 19060 RVA: 0x001F836C File Offset: 0x001F656C
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

		// Token: 0x06004A75 RID: 19061 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x040045FE RID: 17918
		public static Dictionary<int, DFZhenYanLevel> DataDict = new Dictionary<int, DFZhenYanLevel>();

		// Token: 0x040045FF RID: 17919
		public static List<DFZhenYanLevel> DataList = new List<DFZhenYanLevel>();

		// Token: 0x04004600 RID: 17920
		public static Action OnInitFinishAction = new Action(DFZhenYanLevel.OnInitFinish);

		// Token: 0x04004601 RID: 17921
		public int id;

		// Token: 0x04004602 RID: 17922
		public int zhenpanlevel;

		// Token: 0x04004603 RID: 17923
		public int wudaolevel;

		// Token: 0x04004604 RID: 17924
		public int buzhenxiaohao;

		// Token: 0x04004605 RID: 17925
		public int xiuliansudu;

		// Token: 0x04004606 RID: 17926
		public int lingtiansudu;

		// Token: 0x04004607 RID: 17927
		public int lingtiancuishengsudu;

		// Token: 0x04004608 RID: 17928
		public string name;
	}
}
