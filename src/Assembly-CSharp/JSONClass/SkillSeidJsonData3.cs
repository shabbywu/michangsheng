using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000CA0 RID: 3232
	public class SkillSeidJsonData3 : IJSONClass
	{
		// Token: 0x06004DE8 RID: 19944 RVA: 0x0020CFB0 File Offset: 0x0020B1B0
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[3].list)
			{
				try
				{
					SkillSeidJsonData3 skillSeidJsonData = new SkillSeidJsonData3();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].ToList();
					skillSeidJsonData.value2 = jsonobject["value2"].ToList();
					if (SkillSeidJsonData3.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData3.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData3.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData3.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData3.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData3.OnInitFinishAction != null)
			{
				SkillSeidJsonData3.OnInitFinishAction();
			}
		}

		// Token: 0x06004DE9 RID: 19945 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004DA4 RID: 19876
		public static int SEIDID = 3;

		// Token: 0x04004DA5 RID: 19877
		public static Dictionary<int, SkillSeidJsonData3> DataDict = new Dictionary<int, SkillSeidJsonData3>();

		// Token: 0x04004DA6 RID: 19878
		public static List<SkillSeidJsonData3> DataList = new List<SkillSeidJsonData3>();

		// Token: 0x04004DA7 RID: 19879
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData3.OnInitFinish);

		// Token: 0x04004DA8 RID: 19880
		public int skillid;

		// Token: 0x04004DA9 RID: 19881
		public List<int> value1 = new List<int>();

		// Token: 0x04004DAA RID: 19882
		public List<int> value2 = new List<int>();
	}
}
