using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000C7C RID: 3196
	public class SkillSeidJsonData112 : IJSONClass
	{
		// Token: 0x06004D59 RID: 19801 RVA: 0x0020A304 File Offset: 0x00208504
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[112].list)
			{
				try
				{
					SkillSeidJsonData112 skillSeidJsonData = new SkillSeidJsonData112();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].ToList();
					skillSeidJsonData.value2 = jsonobject["value2"].ToList();
					if (SkillSeidJsonData112.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData112.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData112.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData112.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData112.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData112.OnInitFinishAction != null)
			{
				SkillSeidJsonData112.OnInitFinishAction();
			}
		}

		// Token: 0x06004D5A RID: 19802 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004CAB RID: 19627
		public static int SEIDID = 112;

		// Token: 0x04004CAC RID: 19628
		public static Dictionary<int, SkillSeidJsonData112> DataDict = new Dictionary<int, SkillSeidJsonData112>();

		// Token: 0x04004CAD RID: 19629
		public static List<SkillSeidJsonData112> DataList = new List<SkillSeidJsonData112>();

		// Token: 0x04004CAE RID: 19630
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData112.OnInitFinish);

		// Token: 0x04004CAF RID: 19631
		public int skillid;

		// Token: 0x04004CB0 RID: 19632
		public List<int> value1 = new List<int>();

		// Token: 0x04004CB1 RID: 19633
		public List<int> value2 = new List<int>();
	}
}
