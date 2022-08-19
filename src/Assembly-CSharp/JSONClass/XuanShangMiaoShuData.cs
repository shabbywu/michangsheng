using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000991 RID: 2449
	public class XuanShangMiaoShuData : IJSONClass
	{
		// Token: 0x06004458 RID: 17496 RVA: 0x001D18E8 File Offset: 0x001CFAE8
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

		// Token: 0x06004459 RID: 17497 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004602 RID: 17922
		public static Dictionary<int, XuanShangMiaoShuData> DataDict = new Dictionary<int, XuanShangMiaoShuData>();

		// Token: 0x04004603 RID: 17923
		public static List<XuanShangMiaoShuData> DataList = new List<XuanShangMiaoShuData>();

		// Token: 0x04004604 RID: 17924
		public static Action OnInitFinishAction = new Action(XuanShangMiaoShuData.OnInitFinish);

		// Token: 0x04004605 RID: 17925
		public int id;

		// Token: 0x04004606 RID: 17926
		public string Info;
	}
}
