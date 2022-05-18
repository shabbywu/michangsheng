using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000CA6 RID: 3238
	public class SkillSeidJsonData35 : IJSONClass
	{
		// Token: 0x06004E00 RID: 19968 RVA: 0x0020D704 File Offset: 0x0020B904
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[35].list)
			{
				try
				{
					SkillSeidJsonData35 skillSeidJsonData = new SkillSeidJsonData35();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].I;
					if (SkillSeidJsonData35.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData35.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData35.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData35.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData35.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData35.OnInitFinishAction != null)
			{
				SkillSeidJsonData35.OnInitFinishAction();
			}
		}

		// Token: 0x06004E01 RID: 19969 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004DCD RID: 19917
		public static int SEIDID = 35;

		// Token: 0x04004DCE RID: 19918
		public static Dictionary<int, SkillSeidJsonData35> DataDict = new Dictionary<int, SkillSeidJsonData35>();

		// Token: 0x04004DCF RID: 19919
		public static List<SkillSeidJsonData35> DataList = new List<SkillSeidJsonData35>();

		// Token: 0x04004DD0 RID: 19920
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData35.OnInitFinish);

		// Token: 0x04004DD1 RID: 19921
		public int skillid;

		// Token: 0x04004DD2 RID: 19922
		public int value1;
	}
}
