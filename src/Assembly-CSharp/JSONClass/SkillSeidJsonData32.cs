using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000CA3 RID: 3235
	public class SkillSeidJsonData32 : IJSONClass
	{
		// Token: 0x06004DF4 RID: 19956 RVA: 0x0020D364 File Offset: 0x0020B564
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[32].list)
			{
				try
				{
					SkillSeidJsonData32 skillSeidJsonData = new SkillSeidJsonData32();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].I;
					skillSeidJsonData.value2 = jsonobject["value2"].I;
					if (SkillSeidJsonData32.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData32.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData32.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData32.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData32.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData32.OnInitFinishAction != null)
			{
				SkillSeidJsonData32.OnInitFinishAction();
			}
		}

		// Token: 0x06004DF5 RID: 19957 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004DB9 RID: 19897
		public static int SEIDID = 32;

		// Token: 0x04004DBA RID: 19898
		public static Dictionary<int, SkillSeidJsonData32> DataDict = new Dictionary<int, SkillSeidJsonData32>();

		// Token: 0x04004DBB RID: 19899
		public static List<SkillSeidJsonData32> DataList = new List<SkillSeidJsonData32>();

		// Token: 0x04004DBC RID: 19900
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData32.OnInitFinish);

		// Token: 0x04004DBD RID: 19901
		public int skillid;

		// Token: 0x04004DBE RID: 19902
		public int value1;

		// Token: 0x04004DBF RID: 19903
		public int value2;
	}
}
