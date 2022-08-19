using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000820 RID: 2080
	public class CyNpcAnswerData : IJSONClass
	{
		// Token: 0x06003E92 RID: 16018 RVA: 0x001AB990 File Offset: 0x001A9B90
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

		// Token: 0x06003E93 RID: 16019 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x040039F0 RID: 14832
		public static Dictionary<int, CyNpcAnswerData> DataDict = new Dictionary<int, CyNpcAnswerData>();

		// Token: 0x040039F1 RID: 14833
		public static List<CyNpcAnswerData> DataList = new List<CyNpcAnswerData>();

		// Token: 0x040039F2 RID: 14834
		public static Action OnInitFinishAction = new Action(CyNpcAnswerData.OnInitFinish);

		// Token: 0x040039F3 RID: 14835
		public int id;

		// Token: 0x040039F4 RID: 14836
		public int NPCActionID;

		// Token: 0x040039F5 RID: 14837
		public int AnswerType;

		// Token: 0x040039F6 RID: 14838
		public int IsPangBai;

		// Token: 0x040039F7 RID: 14839
		public int AnswerAction;

		// Token: 0x040039F8 RID: 14840
		public string DuiHua;
	}
}
