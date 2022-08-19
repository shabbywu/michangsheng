using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x020008D3 RID: 2259
	public class SeaCastTimeJsonData : IJSONClass
	{
		// Token: 0x0600415F RID: 16735 RVA: 0x001BFA50 File Offset: 0x001BDC50
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SeaCastTimeJsonData.list)
			{
				try
				{
					SeaCastTimeJsonData seaCastTimeJsonData = new SeaCastTimeJsonData();
					seaCastTimeJsonData.id = jsonobject["id"].I;
					seaCastTimeJsonData.dunSu = jsonobject["dunSu"].I;
					seaCastTimeJsonData.XiaoHao = jsonobject["XiaoHao"].I;
					if (SeaCastTimeJsonData.DataDict.ContainsKey(seaCastTimeJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SeaCastTimeJsonData.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", seaCastTimeJsonData.id));
					}
					else
					{
						SeaCastTimeJsonData.DataDict.Add(seaCastTimeJsonData.id, seaCastTimeJsonData);
						SeaCastTimeJsonData.DataList.Add(seaCastTimeJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SeaCastTimeJsonData.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SeaCastTimeJsonData.OnInitFinishAction != null)
			{
				SeaCastTimeJsonData.OnInitFinishAction();
			}
		}

		// Token: 0x06004160 RID: 16736 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x040040A1 RID: 16545
		public static Dictionary<int, SeaCastTimeJsonData> DataDict = new Dictionary<int, SeaCastTimeJsonData>();

		// Token: 0x040040A2 RID: 16546
		public static List<SeaCastTimeJsonData> DataList = new List<SeaCastTimeJsonData>();

		// Token: 0x040040A3 RID: 16547
		public static Action OnInitFinishAction = new Action(SeaCastTimeJsonData.OnInitFinish);

		// Token: 0x040040A4 RID: 16548
		public int id;

		// Token: 0x040040A5 RID: 16549
		public int dunSu;

		// Token: 0x040040A6 RID: 16550
		public int XiaoHao;
	}
}
