using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000CFB RID: 3323
	public class TuJianChunWenBen : IJSONClass
	{
		// Token: 0x06004F56 RID: 20310 RVA: 0x002149A0 File Offset: 0x00212BA0
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.TuJianChunWenBen.list)
			{
				try
				{
					TuJianChunWenBen tuJianChunWenBen = new TuJianChunWenBen();
					tuJianChunWenBen.id = jsonobject["id"].I;
					tuJianChunWenBen.typenum = jsonobject["typenum"].I;
					tuJianChunWenBen.type = jsonobject["type"].I;
					tuJianChunWenBen.name1 = jsonobject["name1"].Str;
					tuJianChunWenBen.name2 = jsonobject["name2"].Str;
					tuJianChunWenBen.descr = jsonobject["descr"].Str;
					if (TuJianChunWenBen.DataDict.ContainsKey(tuJianChunWenBen.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典TuJianChunWenBen.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", tuJianChunWenBen.id));
					}
					else
					{
						TuJianChunWenBen.DataDict.Add(tuJianChunWenBen.id, tuJianChunWenBen);
						TuJianChunWenBen.DataList.Add(tuJianChunWenBen);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典TuJianChunWenBen.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (TuJianChunWenBen.OnInitFinishAction != null)
			{
				TuJianChunWenBen.OnInitFinishAction();
			}
		}

		// Token: 0x06004F57 RID: 20311 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04005056 RID: 20566
		public static Dictionary<int, TuJianChunWenBen> DataDict = new Dictionary<int, TuJianChunWenBen>();

		// Token: 0x04005057 RID: 20567
		public static List<TuJianChunWenBen> DataList = new List<TuJianChunWenBen>();

		// Token: 0x04005058 RID: 20568
		public static Action OnInitFinishAction = new Action(TuJianChunWenBen.OnInitFinish);

		// Token: 0x04005059 RID: 20569
		public int id;

		// Token: 0x0400505A RID: 20570
		public int typenum;

		// Token: 0x0400505B RID: 20571
		public int type;

		// Token: 0x0400505C RID: 20572
		public string name1;

		// Token: 0x0400505D RID: 20573
		public string name2;

		// Token: 0x0400505E RID: 20574
		public string descr;
	}
}
