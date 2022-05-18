using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000CC8 RID: 3272
	public class SkillSeidJsonData71 : IJSONClass
	{
		// Token: 0x06004E88 RID: 20104 RVA: 0x002100A0 File Offset: 0x0020E2A0
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[71].list)
			{
				try
				{
					SkillSeidJsonData71 skillSeidJsonData = new SkillSeidJsonData71();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].I;
					skillSeidJsonData.value2 = jsonobject["value2"].I;
					skillSeidJsonData.value3 = jsonobject["value3"].I;
					if (SkillSeidJsonData71.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData71.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData71.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData71.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData71.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData71.OnInitFinishAction != null)
			{
				SkillSeidJsonData71.OnInitFinishAction();
			}
		}

		// Token: 0x06004E89 RID: 20105 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004EB4 RID: 20148
		public static int SEIDID = 71;

		// Token: 0x04004EB5 RID: 20149
		public static Dictionary<int, SkillSeidJsonData71> DataDict = new Dictionary<int, SkillSeidJsonData71>();

		// Token: 0x04004EB6 RID: 20150
		public static List<SkillSeidJsonData71> DataList = new List<SkillSeidJsonData71>();

		// Token: 0x04004EB7 RID: 20151
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData71.OnInitFinish);

		// Token: 0x04004EB8 RID: 20152
		public int skillid;

		// Token: 0x04004EB9 RID: 20153
		public int value1;

		// Token: 0x04004EBA RID: 20154
		public int value2;

		// Token: 0x04004EBB RID: 20155
		public int value3;
	}
}
