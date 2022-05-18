using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000CB8 RID: 3256
	public class SkillSeidJsonData52 : IJSONClass
	{
		// Token: 0x06004E48 RID: 20040 RVA: 0x0020ED78 File Offset: 0x0020CF78
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[52].list)
			{
				try
				{
					SkillSeidJsonData52 skillSeidJsonData = new SkillSeidJsonData52();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].I;
					if (SkillSeidJsonData52.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData52.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData52.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData52.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData52.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData52.OnInitFinishAction != null)
			{
				SkillSeidJsonData52.OnInitFinishAction();
			}
		}

		// Token: 0x06004E49 RID: 20041 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004E4D RID: 20045
		public static int SEIDID = 52;

		// Token: 0x04004E4E RID: 20046
		public static Dictionary<int, SkillSeidJsonData52> DataDict = new Dictionary<int, SkillSeidJsonData52>();

		// Token: 0x04004E4F RID: 20047
		public static List<SkillSeidJsonData52> DataList = new List<SkillSeidJsonData52>();

		// Token: 0x04004E50 RID: 20048
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData52.OnInitFinish);

		// Token: 0x04004E51 RID: 20049
		public int skillid;

		// Token: 0x04004E52 RID: 20050
		public int value1;
	}
}
