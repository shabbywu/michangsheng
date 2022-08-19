using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000965 RID: 2405
	public class StaticSkillSeidJsonData5 : IJSONClass
	{
		// Token: 0x060043A6 RID: 17318 RVA: 0x001CCE0C File Offset: 0x001CB00C
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.StaticSkillSeidJsonData[5].list)
			{
				try
				{
					StaticSkillSeidJsonData5 staticSkillSeidJsonData = new StaticSkillSeidJsonData5();
					staticSkillSeidJsonData.skillid = jsonobject["skillid"].I;
					staticSkillSeidJsonData.value1 = jsonobject["value1"].I;
					if (StaticSkillSeidJsonData5.DataDict.ContainsKey(staticSkillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典StaticSkillSeidJsonData5.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", staticSkillSeidJsonData.skillid));
					}
					else
					{
						StaticSkillSeidJsonData5.DataDict.Add(staticSkillSeidJsonData.skillid, staticSkillSeidJsonData);
						StaticSkillSeidJsonData5.DataList.Add(staticSkillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典StaticSkillSeidJsonData5.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (StaticSkillSeidJsonData5.OnInitFinishAction != null)
			{
				StaticSkillSeidJsonData5.OnInitFinishAction();
			}
		}

		// Token: 0x060043A7 RID: 17319 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x0400448B RID: 17547
		public static int SEIDID = 5;

		// Token: 0x0400448C RID: 17548
		public static Dictionary<int, StaticSkillSeidJsonData5> DataDict = new Dictionary<int, StaticSkillSeidJsonData5>();

		// Token: 0x0400448D RID: 17549
		public static List<StaticSkillSeidJsonData5> DataList = new List<StaticSkillSeidJsonData5>();

		// Token: 0x0400448E RID: 17550
		public static Action OnInitFinishAction = new Action(StaticSkillSeidJsonData5.OnInitFinish);

		// Token: 0x0400448F RID: 17551
		public int skillid;

		// Token: 0x04004490 RID: 17552
		public int value1;
	}
}
