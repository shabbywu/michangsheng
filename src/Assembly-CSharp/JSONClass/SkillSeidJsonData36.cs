using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000CA7 RID: 3239
	public class SkillSeidJsonData36 : IJSONClass
	{
		// Token: 0x06004E04 RID: 19972 RVA: 0x0020D82C File Offset: 0x0020BA2C
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[36].list)
			{
				try
				{
					SkillSeidJsonData36 skillSeidJsonData = new SkillSeidJsonData36();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].I;
					skillSeidJsonData.value2 = jsonobject["value2"].I;
					if (SkillSeidJsonData36.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData36.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData36.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData36.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData36.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData36.OnInitFinishAction != null)
			{
				SkillSeidJsonData36.OnInitFinishAction();
			}
		}

		// Token: 0x06004E05 RID: 19973 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004DD3 RID: 19923
		public static int SEIDID = 36;

		// Token: 0x04004DD4 RID: 19924
		public static Dictionary<int, SkillSeidJsonData36> DataDict = new Dictionary<int, SkillSeidJsonData36>();

		// Token: 0x04004DD5 RID: 19925
		public static List<SkillSeidJsonData36> DataList = new List<SkillSeidJsonData36>();

		// Token: 0x04004DD6 RID: 19926
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData36.OnInitFinish);

		// Token: 0x04004DD7 RID: 19927
		public int skillid;

		// Token: 0x04004DD8 RID: 19928
		public int value1;

		// Token: 0x04004DD9 RID: 19929
		public int value2;
	}
}
