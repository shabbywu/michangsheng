using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000CF0 RID: 3312
	public class StrTextJsonData : IJSONClass
	{
		// Token: 0x06004F28 RID: 20264 RVA: 0x0021329C File Offset: 0x0021149C
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.StrTextJsonData.list)
			{
				try
				{
					StrTextJsonData strTextJsonData = new StrTextJsonData();
					strTextJsonData.StrID = jsonobject["StrID"].Str;
					strTextJsonData.ChinaText = jsonobject["ChinaText"].Str;
					if (StrTextJsonData.DataDict.ContainsKey(strTextJsonData.StrID))
					{
						PreloadManager.LogException("!!!错误!!!向字典StrTextJsonData.DataDict添加数据时出现重复的键，Key:" + strTextJsonData.StrID + "，已跳过，请检查配表");
					}
					else
					{
						StrTextJsonData.DataDict.Add(strTextJsonData.StrID, strTextJsonData);
						StrTextJsonData.DataList.Add(strTextJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典StrTextJsonData.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (StrTextJsonData.OnInitFinishAction != null)
			{
				StrTextJsonData.OnInitFinishAction();
			}
		}

		// Token: 0x06004F29 RID: 20265 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004FC9 RID: 20425
		public static Dictionary<string, StrTextJsonData> DataDict = new Dictionary<string, StrTextJsonData>();

		// Token: 0x04004FCA RID: 20426
		public static List<StrTextJsonData> DataList = new List<StrTextJsonData>();

		// Token: 0x04004FCB RID: 20427
		public static Action OnInitFinishAction = new Action(StrTextJsonData.OnInitFinish);

		// Token: 0x04004FCC RID: 20428
		public string StrID;

		// Token: 0x04004FCD RID: 20429
		public string ChinaText;
	}
}
