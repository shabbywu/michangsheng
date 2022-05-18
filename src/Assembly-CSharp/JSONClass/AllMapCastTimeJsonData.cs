using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000AE3 RID: 2787
	public class AllMapCastTimeJsonData : IJSONClass
	{
		// Token: 0x060046F6 RID: 18166 RVA: 0x001E5D5C File Offset: 0x001E3F5C
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.AllMapCastTimeJsonData.list)
			{
				try
				{
					AllMapCastTimeJsonData allMapCastTimeJsonData = new AllMapCastTimeJsonData();
					allMapCastTimeJsonData.id = jsonobject["id"].I;
					allMapCastTimeJsonData.dunSu = jsonobject["dunSu"].I;
					allMapCastTimeJsonData.XiaoHao = jsonobject["XiaoHao"].I;
					if (AllMapCastTimeJsonData.DataDict.ContainsKey(allMapCastTimeJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典AllMapCastTimeJsonData.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", allMapCastTimeJsonData.id));
					}
					else
					{
						AllMapCastTimeJsonData.DataDict.Add(allMapCastTimeJsonData.id, allMapCastTimeJsonData);
						AllMapCastTimeJsonData.DataList.Add(allMapCastTimeJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典AllMapCastTimeJsonData.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (AllMapCastTimeJsonData.OnInitFinishAction != null)
			{
				AllMapCastTimeJsonData.OnInitFinishAction();
			}
		}

		// Token: 0x060046F7 RID: 18167 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003F6E RID: 16238
		public static Dictionary<int, AllMapCastTimeJsonData> DataDict = new Dictionary<int, AllMapCastTimeJsonData>();

		// Token: 0x04003F6F RID: 16239
		public static List<AllMapCastTimeJsonData> DataList = new List<AllMapCastTimeJsonData>();

		// Token: 0x04003F70 RID: 16240
		public static Action OnInitFinishAction = new Action(AllMapCastTimeJsonData.OnInitFinish);

		// Token: 0x04003F71 RID: 16241
		public int id;

		// Token: 0x04003F72 RID: 16242
		public int dunSu;

		// Token: 0x04003F73 RID: 16243
		public int XiaoHao;
	}
}
