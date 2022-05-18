using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000C93 RID: 3219
	public class SkillSeidJsonData154 : IJSONClass
	{
		// Token: 0x06004DB4 RID: 19892 RVA: 0x0020BFA0 File Offset: 0x0020A1A0
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[154].list)
			{
				try
				{
					SkillSeidJsonData154 skillSeidJsonData = new SkillSeidJsonData154();
					skillSeidJsonData.id = jsonobject["id"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].I;
					skillSeidJsonData.value2 = jsonobject["value2"].I;
					if (SkillSeidJsonData154.DataDict.ContainsKey(skillSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData154.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.id));
					}
					else
					{
						SkillSeidJsonData154.DataDict.Add(skillSeidJsonData.id, skillSeidJsonData);
						SkillSeidJsonData154.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData154.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData154.OnInitFinishAction != null)
			{
				SkillSeidJsonData154.OnInitFinishAction();
			}
		}

		// Token: 0x06004DB5 RID: 19893 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004D4C RID: 19788
		public static int SEIDID = 154;

		// Token: 0x04004D4D RID: 19789
		public static Dictionary<int, SkillSeidJsonData154> DataDict = new Dictionary<int, SkillSeidJsonData154>();

		// Token: 0x04004D4E RID: 19790
		public static List<SkillSeidJsonData154> DataList = new List<SkillSeidJsonData154>();

		// Token: 0x04004D4F RID: 19791
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData154.OnInitFinish);

		// Token: 0x04004D50 RID: 19792
		public int id;

		// Token: 0x04004D51 RID: 19793
		public int value1;

		// Token: 0x04004D52 RID: 19794
		public int value2;
	}
}
