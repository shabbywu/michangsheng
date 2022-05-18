using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000BB6 RID: 2998
	public class CyNpcAnswerData : IJSONClass
	{
		// Token: 0x06004A40 RID: 19008 RVA: 0x001F6D50 File Offset: 0x001F4F50
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.CyNpcAnswerData.list)
			{
				try
				{
					CyNpcAnswerData cyNpcAnswerData = new CyNpcAnswerData();
					cyNpcAnswerData.id = jsonobject["id"].I;
					cyNpcAnswerData.NPCActionID = jsonobject["NPCActionID"].I;
					cyNpcAnswerData.AnswerType = jsonobject["AnswerType"].I;
					cyNpcAnswerData.IsPangBai = jsonobject["IsPangBai"].I;
					cyNpcAnswerData.AnswerAction = jsonobject["AnswerAction"].I;
					cyNpcAnswerData.DuiHua = jsonobject["DuiHua"].Str;
					if (CyNpcAnswerData.DataDict.ContainsKey(cyNpcAnswerData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典CyNpcAnswerData.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", cyNpcAnswerData.id));
					}
					else
					{
						CyNpcAnswerData.DataDict.Add(cyNpcAnswerData.id, cyNpcAnswerData);
						CyNpcAnswerData.DataList.Add(cyNpcAnswerData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典CyNpcAnswerData.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (CyNpcAnswerData.OnInitFinishAction != null)
			{
				CyNpcAnswerData.OnInitFinishAction();
			}
		}

		// Token: 0x06004A41 RID: 19009 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004578 RID: 17784
		public static Dictionary<int, CyNpcAnswerData> DataDict = new Dictionary<int, CyNpcAnswerData>();

		// Token: 0x04004579 RID: 17785
		public static List<CyNpcAnswerData> DataList = new List<CyNpcAnswerData>();

		// Token: 0x0400457A RID: 17786
		public static Action OnInitFinishAction = new Action(CyNpcAnswerData.OnInitFinish);

		// Token: 0x0400457B RID: 17787
		public int id;

		// Token: 0x0400457C RID: 17788
		public int NPCActionID;

		// Token: 0x0400457D RID: 17789
		public int AnswerType;

		// Token: 0x0400457E RID: 17790
		public int IsPangBai;

		// Token: 0x0400457F RID: 17791
		public int AnswerAction;

		// Token: 0x04004580 RID: 17792
		public string DuiHua;
	}
}
