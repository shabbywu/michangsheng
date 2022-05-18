using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000CA9 RID: 3241
	public class SkillSeidJsonData38 : IJSONClass
	{
		// Token: 0x06004E0C RID: 19980 RVA: 0x0020DA90 File Offset: 0x0020BC90
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[38].list)
			{
				try
				{
					SkillSeidJsonData38 skillSeidJsonData = new SkillSeidJsonData38();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].I;
					skillSeidJsonData.value2 = jsonobject["value2"].I;
					if (SkillSeidJsonData38.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData38.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData38.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData38.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData38.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData38.OnInitFinishAction != null)
			{
				SkillSeidJsonData38.OnInitFinishAction();
			}
		}

		// Token: 0x06004E0D RID: 19981 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004DE0 RID: 19936
		public static int SEIDID = 38;

		// Token: 0x04004DE1 RID: 19937
		public static Dictionary<int, SkillSeidJsonData38> DataDict = new Dictionary<int, SkillSeidJsonData38>();

		// Token: 0x04004DE2 RID: 19938
		public static List<SkillSeidJsonData38> DataList = new List<SkillSeidJsonData38>();

		// Token: 0x04004DE3 RID: 19939
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData38.OnInitFinish);

		// Token: 0x04004DE4 RID: 19940
		public int skillid;

		// Token: 0x04004DE5 RID: 19941
		public int value1;

		// Token: 0x04004DE6 RID: 19942
		public int value2;
	}
}
