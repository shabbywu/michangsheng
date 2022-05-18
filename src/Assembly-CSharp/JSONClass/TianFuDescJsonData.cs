using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000CF6 RID: 3318
	public class TianFuDescJsonData : IJSONClass
	{
		// Token: 0x06004F40 RID: 20288 RVA: 0x00213F44 File Offset: 0x00212144
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.TianFuDescJsonData.list)
			{
				try
				{
					TianFuDescJsonData tianFuDescJsonData = new TianFuDescJsonData();
					tianFuDescJsonData.id = jsonobject["id"].I;
					tianFuDescJsonData.Title = jsonobject["Title"].Str;
					tianFuDescJsonData.Desc = jsonobject["Desc"].Str;
					if (TianFuDescJsonData.DataDict.ContainsKey(tianFuDescJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典TianFuDescJsonData.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", tianFuDescJsonData.id));
					}
					else
					{
						TianFuDescJsonData.DataDict.Add(tianFuDescJsonData.id, tianFuDescJsonData);
						TianFuDescJsonData.DataList.Add(tianFuDescJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典TianFuDescJsonData.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (TianFuDescJsonData.OnInitFinishAction != null)
			{
				TianFuDescJsonData.OnInitFinishAction();
			}
		}

		// Token: 0x06004F41 RID: 20289 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04005021 RID: 20513
		public static Dictionary<int, TianFuDescJsonData> DataDict = new Dictionary<int, TianFuDescJsonData>();

		// Token: 0x04005022 RID: 20514
		public static List<TianFuDescJsonData> DataList = new List<TianFuDescJsonData>();

		// Token: 0x04005023 RID: 20515
		public static Action OnInitFinishAction = new Action(TianFuDescJsonData.OnInitFinish);

		// Token: 0x04005024 RID: 20516
		public int id;

		// Token: 0x04005025 RID: 20517
		public string Title;

		// Token: 0x04005026 RID: 20518
		public string Desc;
	}
}
