using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000BB7 RID: 2999
	public class CyNpcDuiBaiData : IJSONClass
	{
		// Token: 0x06004A44 RID: 19012 RVA: 0x001F6EE4 File Offset: 0x001F50E4
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

		// Token: 0x06004A45 RID: 19013 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004581 RID: 17793
		public static Dictionary<int, CyNpcDuiBaiData> DataDict = new Dictionary<int, CyNpcDuiBaiData>();

		// Token: 0x04004582 RID: 17794
		public static List<CyNpcDuiBaiData> DataList = new List<CyNpcDuiBaiData>();

		// Token: 0x04004583 RID: 17795
		public static Action OnInitFinishAction = new Action(CyNpcDuiBaiData.OnInitFinish);

		// Token: 0x04004584 RID: 17796
		public int id;

		// Token: 0x04004585 RID: 17797
		public int Type;

		// Token: 0x04004586 RID: 17798
		public int XingGe;

		// Token: 0x04004587 RID: 17799
		public string dir1;

		// Token: 0x04004588 RID: 17800
		public string dir2;

		// Token: 0x04004589 RID: 17801
		public string dir3;
	}
}
