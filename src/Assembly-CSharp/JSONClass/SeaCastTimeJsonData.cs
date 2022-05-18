using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000C60 RID: 3168
	public class SeaCastTimeJsonData : IJSONClass
	{
		// Token: 0x06004CE9 RID: 19689 RVA: 0x00207F08 File Offset: 0x00206108
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

		// Token: 0x06004CEA RID: 19690 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004BEB RID: 19435
		public static Dictionary<int, SeaCastTimeJsonData> DataDict = new Dictionary<int, SeaCastTimeJsonData>();

		// Token: 0x04004BEC RID: 19436
		public static List<SeaCastTimeJsonData> DataList = new List<SeaCastTimeJsonData>();

		// Token: 0x04004BED RID: 19437
		public static Action OnInitFinishAction = new Action(SeaCastTimeJsonData.OnInitFinish);

		// Token: 0x04004BEE RID: 19438
		public int id;

		// Token: 0x04004BEF RID: 19439
		public int dunSu;

		// Token: 0x04004BF0 RID: 19440
		public int XiaoHao;
	}
}
