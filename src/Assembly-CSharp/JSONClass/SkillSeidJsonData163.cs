using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000C98 RID: 3224
	public class SkillSeidJsonData163 : IJSONClass
	{
		// Token: 0x06004DC8 RID: 19912 RVA: 0x0020C628 File Offset: 0x0020A828
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[163].list)
			{
				try
				{
					SkillSeidJsonData163 skillSeidJsonData = new SkillSeidJsonData163();
					skillSeidJsonData.id = jsonobject["id"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].I;
					if (SkillSeidJsonData163.DataDict.ContainsKey(skillSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData163.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.id));
					}
					else
					{
						SkillSeidJsonData163.DataDict.Add(skillSeidJsonData.id, skillSeidJsonData);
						SkillSeidJsonData163.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData163.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData163.OnInitFinishAction != null)
			{
				SkillSeidJsonData163.OnInitFinishAction();
			}
		}

		// Token: 0x06004DC9 RID: 19913 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004D71 RID: 19825
		public static int SEIDID = 163;

		// Token: 0x04004D72 RID: 19826
		public static Dictionary<int, SkillSeidJsonData163> DataDict = new Dictionary<int, SkillSeidJsonData163>();

		// Token: 0x04004D73 RID: 19827
		public static List<SkillSeidJsonData163> DataList = new List<SkillSeidJsonData163>();

		// Token: 0x04004D74 RID: 19828
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData163.OnInitFinish);

		// Token: 0x04004D75 RID: 19829
		public int id;

		// Token: 0x04004D76 RID: 19830
		public int value1;
	}
}
