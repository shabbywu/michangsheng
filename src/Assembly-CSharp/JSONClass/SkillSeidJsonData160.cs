using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000C96 RID: 3222
	public class SkillSeidJsonData160 : IJSONClass
	{
		// Token: 0x06004DC0 RID: 19904 RVA: 0x0020C3A4 File Offset: 0x0020A5A4
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[160].list)
			{
				try
				{
					SkillSeidJsonData160 skillSeidJsonData = new SkillSeidJsonData160();
					skillSeidJsonData.id = jsonobject["id"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].I;
					if (SkillSeidJsonData160.DataDict.ContainsKey(skillSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData160.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.id));
					}
					else
					{
						SkillSeidJsonData160.DataDict.Add(skillSeidJsonData.id, skillSeidJsonData);
						SkillSeidJsonData160.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData160.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData160.OnInitFinishAction != null)
			{
				SkillSeidJsonData160.OnInitFinishAction();
			}
		}

		// Token: 0x06004DC1 RID: 19905 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004D63 RID: 19811
		public static int SEIDID = 160;

		// Token: 0x04004D64 RID: 19812
		public static Dictionary<int, SkillSeidJsonData160> DataDict = new Dictionary<int, SkillSeidJsonData160>();

		// Token: 0x04004D65 RID: 19813
		public static List<SkillSeidJsonData160> DataList = new List<SkillSeidJsonData160>();

		// Token: 0x04004D66 RID: 19814
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData160.OnInitFinish);

		// Token: 0x04004D67 RID: 19815
		public int id;

		// Token: 0x04004D68 RID: 19816
		public int value1;
	}
}
