using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000CDA RID: 3290
	public class SkillSeidJsonData90 : IJSONClass
	{
		// Token: 0x06004ED0 RID: 20176 RVA: 0x00211650 File Offset: 0x0020F850
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[90].list)
			{
				try
				{
					SkillSeidJsonData90 skillSeidJsonData = new SkillSeidJsonData90();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].I;
					skillSeidJsonData.value2 = jsonobject["value2"].I;
					if (SkillSeidJsonData90.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData90.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData90.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData90.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData90.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData90.OnInitFinishAction != null)
			{
				SkillSeidJsonData90.OnInitFinishAction();
			}
		}

		// Token: 0x06004ED1 RID: 20177 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004F2B RID: 20267
		public static int SEIDID = 90;

		// Token: 0x04004F2C RID: 20268
		public static Dictionary<int, SkillSeidJsonData90> DataDict = new Dictionary<int, SkillSeidJsonData90>();

		// Token: 0x04004F2D RID: 20269
		public static List<SkillSeidJsonData90> DataList = new List<SkillSeidJsonData90>();

		// Token: 0x04004F2E RID: 20270
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData90.OnInitFinish);

		// Token: 0x04004F2F RID: 20271
		public int skillid;

		// Token: 0x04004F30 RID: 20272
		public int value1;

		// Token: 0x04004F31 RID: 20273
		public int value2;
	}
}
