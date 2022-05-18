using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000CA8 RID: 3240
	public class SkillSeidJsonData37 : IJSONClass
	{
		// Token: 0x06004E08 RID: 19976 RVA: 0x0020D968 File Offset: 0x0020BB68
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[37].list)
			{
				try
				{
					SkillSeidJsonData37 skillSeidJsonData = new SkillSeidJsonData37();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].I;
					if (SkillSeidJsonData37.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData37.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData37.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData37.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData37.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData37.OnInitFinishAction != null)
			{
				SkillSeidJsonData37.OnInitFinishAction();
			}
		}

		// Token: 0x06004E09 RID: 19977 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004DDA RID: 19930
		public static int SEIDID = 37;

		// Token: 0x04004DDB RID: 19931
		public static Dictionary<int, SkillSeidJsonData37> DataDict = new Dictionary<int, SkillSeidJsonData37>();

		// Token: 0x04004DDC RID: 19932
		public static List<SkillSeidJsonData37> DataList = new List<SkillSeidJsonData37>();

		// Token: 0x04004DDD RID: 19933
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData37.OnInitFinish);

		// Token: 0x04004DDE RID: 19934
		public int skillid;

		// Token: 0x04004DDF RID: 19935
		public int value1;
	}
}
