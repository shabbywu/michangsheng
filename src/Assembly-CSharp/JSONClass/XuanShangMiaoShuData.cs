using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000D11 RID: 3345
	public class XuanShangMiaoShuData : IJSONClass
	{
		// Token: 0x06004FAE RID: 20398 RVA: 0x00216700 File Offset: 0x00214900
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.XuanShangMiaoShuData.list)
			{
				try
				{
					XuanShangMiaoShuData xuanShangMiaoShuData = new XuanShangMiaoShuData();
					xuanShangMiaoShuData.id = jsonobject["id"].I;
					xuanShangMiaoShuData.Info = jsonobject["Info"].Str;
					if (XuanShangMiaoShuData.DataDict.ContainsKey(xuanShangMiaoShuData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典XuanShangMiaoShuData.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", xuanShangMiaoShuData.id));
					}
					else
					{
						XuanShangMiaoShuData.DataDict.Add(xuanShangMiaoShuData.id, xuanShangMiaoShuData);
						XuanShangMiaoShuData.DataList.Add(xuanShangMiaoShuData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典XuanShangMiaoShuData.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (XuanShangMiaoShuData.OnInitFinishAction != null)
			{
				XuanShangMiaoShuData.OnInitFinishAction();
			}
		}

		// Token: 0x06004FAF RID: 20399 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x040050F6 RID: 20726
		public static Dictionary<int, XuanShangMiaoShuData> DataDict = new Dictionary<int, XuanShangMiaoShuData>();

		// Token: 0x040050F7 RID: 20727
		public static List<XuanShangMiaoShuData> DataList = new List<XuanShangMiaoShuData>();

		// Token: 0x040050F8 RID: 20728
		public static Action OnInitFinishAction = new Action(XuanShangMiaoShuData.OnInitFinish);

		// Token: 0x040050F9 RID: 20729
		public int id;

		// Token: 0x040050FA RID: 20730
		public string Info;
	}
}
