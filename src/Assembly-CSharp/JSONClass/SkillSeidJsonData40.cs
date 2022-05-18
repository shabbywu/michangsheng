using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000CAC RID: 3244
	public class SkillSeidJsonData40 : IJSONClass
	{
		// Token: 0x06004E18 RID: 19992 RVA: 0x0020DE30 File Offset: 0x0020C030
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[40].list)
			{
				try
				{
					SkillSeidJsonData40 skillSeidJsonData = new SkillSeidJsonData40();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].I;
					skillSeidJsonData.value2 = jsonobject["value2"].I;
					if (SkillSeidJsonData40.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData40.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData40.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData40.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData40.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData40.OnInitFinishAction != null)
			{
				SkillSeidJsonData40.OnInitFinishAction();
			}
		}

		// Token: 0x06004E19 RID: 19993 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004DF4 RID: 19956
		public static int SEIDID = 40;

		// Token: 0x04004DF5 RID: 19957
		public static Dictionary<int, SkillSeidJsonData40> DataDict = new Dictionary<int, SkillSeidJsonData40>();

		// Token: 0x04004DF6 RID: 19958
		public static List<SkillSeidJsonData40> DataList = new List<SkillSeidJsonData40>();

		// Token: 0x04004DF7 RID: 19959
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData40.OnInitFinish);

		// Token: 0x04004DF8 RID: 19960
		public int skillid;

		// Token: 0x04004DF9 RID: 19961
		public int value1;

		// Token: 0x04004DFA RID: 19962
		public int value2;
	}
}
