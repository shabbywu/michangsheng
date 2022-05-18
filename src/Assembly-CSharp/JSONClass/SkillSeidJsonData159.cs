using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000C95 RID: 3221
	public class SkillSeidJsonData159 : IJSONClass
	{
		// Token: 0x06004DBC RID: 19900 RVA: 0x0020C264 File Offset: 0x0020A464
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[159].list)
			{
				try
				{
					SkillSeidJsonData159 skillSeidJsonData = new SkillSeidJsonData159();
					skillSeidJsonData.id = jsonobject["id"].I;
					skillSeidJsonData.target = jsonobject["target"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].I;
					if (SkillSeidJsonData159.DataDict.ContainsKey(skillSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData159.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.id));
					}
					else
					{
						SkillSeidJsonData159.DataDict.Add(skillSeidJsonData.id, skillSeidJsonData);
						SkillSeidJsonData159.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData159.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData159.OnInitFinishAction != null)
			{
				SkillSeidJsonData159.OnInitFinishAction();
			}
		}

		// Token: 0x06004DBD RID: 19901 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004D5C RID: 19804
		public static int SEIDID = 159;

		// Token: 0x04004D5D RID: 19805
		public static Dictionary<int, SkillSeidJsonData159> DataDict = new Dictionary<int, SkillSeidJsonData159>();

		// Token: 0x04004D5E RID: 19806
		public static List<SkillSeidJsonData159> DataList = new List<SkillSeidJsonData159>();

		// Token: 0x04004D5F RID: 19807
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData159.OnInitFinish);

		// Token: 0x04004D60 RID: 19808
		public int id;

		// Token: 0x04004D61 RID: 19809
		public int target;

		// Token: 0x04004D62 RID: 19810
		public int value1;
	}
}
