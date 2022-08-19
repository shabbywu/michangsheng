using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000821 RID: 2081
	public class CyNpcDuiBaiData : IJSONClass
	{
		// Token: 0x06003E96 RID: 16022 RVA: 0x001ABB4C File Offset: 0x001A9D4C
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.CyNpcDuiBaiData.list)
			{
				try
				{
					CyNpcDuiBaiData cyNpcDuiBaiData = new CyNpcDuiBaiData();
					cyNpcDuiBaiData.id = jsonobject["id"].I;
					cyNpcDuiBaiData.Type = jsonobject["Type"].I;
					cyNpcDuiBaiData.XingGe = jsonobject["XingGe"].I;
					cyNpcDuiBaiData.dir1 = jsonobject["dir1"].Str;
					cyNpcDuiBaiData.dir2 = jsonobject["dir2"].Str;
					cyNpcDuiBaiData.dir3 = jsonobject["dir3"].Str;
					if (CyNpcDuiBaiData.DataDict.ContainsKey(cyNpcDuiBaiData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典CyNpcDuiBaiData.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", cyNpcDuiBaiData.id));
					}
					else
					{
						CyNpcDuiBaiData.DataDict.Add(cyNpcDuiBaiData.id, cyNpcDuiBaiData);
						CyNpcDuiBaiData.DataList.Add(cyNpcDuiBaiData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典CyNpcDuiBaiData.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (CyNpcDuiBaiData.OnInitFinishAction != null)
			{
				CyNpcDuiBaiData.OnInitFinishAction();
			}
		}

		// Token: 0x06003E97 RID: 16023 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x040039F9 RID: 14841
		public static Dictionary<int, CyNpcDuiBaiData> DataDict = new Dictionary<int, CyNpcDuiBaiData>();

		// Token: 0x040039FA RID: 14842
		public static List<CyNpcDuiBaiData> DataList = new List<CyNpcDuiBaiData>();

		// Token: 0x040039FB RID: 14843
		public static Action OnInitFinishAction = new Action(CyNpcDuiBaiData.OnInitFinish);

		// Token: 0x040039FC RID: 14844
		public int id;

		// Token: 0x040039FD RID: 14845
		public int Type;

		// Token: 0x040039FE RID: 14846
		public int XingGe;

		// Token: 0x040039FF RID: 14847
		public string dir1;

		// Token: 0x04003A00 RID: 14848
		public string dir2;

		// Token: 0x04003A01 RID: 14849
		public string dir3;
	}
}
