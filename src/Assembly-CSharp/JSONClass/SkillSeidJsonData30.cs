using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000CA1 RID: 3233
	public class SkillSeidJsonData30 : IJSONClass
	{
		// Token: 0x06004DEC RID: 19948 RVA: 0x0020D0EC File Offset: 0x0020B2EC
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[30].list)
			{
				try
				{
					SkillSeidJsonData30 skillSeidJsonData = new SkillSeidJsonData30();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].I;
					skillSeidJsonData.value2 = jsonobject["value2"].I;
					if (SkillSeidJsonData30.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData30.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData30.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData30.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData30.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData30.OnInitFinishAction != null)
			{
				SkillSeidJsonData30.OnInitFinishAction();
			}
		}

		// Token: 0x06004DED RID: 19949 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004DAB RID: 19883
		public static int SEIDID = 30;

		// Token: 0x04004DAC RID: 19884
		public static Dictionary<int, SkillSeidJsonData30> DataDict = new Dictionary<int, SkillSeidJsonData30>();

		// Token: 0x04004DAD RID: 19885
		public static List<SkillSeidJsonData30> DataList = new List<SkillSeidJsonData30>();

		// Token: 0x04004DAE RID: 19886
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData30.OnInitFinish);

		// Token: 0x04004DAF RID: 19887
		public int skillid;

		// Token: 0x04004DB0 RID: 19888
		public int value1;

		// Token: 0x04004DB1 RID: 19889
		public int value2;
	}
}
