using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000968 RID: 2408
	public class StaticSkillSeidJsonData8 : IJSONClass
	{
		// Token: 0x060043B2 RID: 17330 RVA: 0x001CD214 File Offset: 0x001CB414
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.StaticSkillSeidJsonData[8].list)
			{
				try
				{
					StaticSkillSeidJsonData8 staticSkillSeidJsonData = new StaticSkillSeidJsonData8();
					staticSkillSeidJsonData.skillid = jsonobject["skillid"].I;
					staticSkillSeidJsonData.value1 = jsonobject["value1"].I;
					if (StaticSkillSeidJsonData8.DataDict.ContainsKey(staticSkillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典StaticSkillSeidJsonData8.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", staticSkillSeidJsonData.skillid));
					}
					else
					{
						StaticSkillSeidJsonData8.DataDict.Add(staticSkillSeidJsonData.skillid, staticSkillSeidJsonData);
						StaticSkillSeidJsonData8.DataList.Add(staticSkillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典StaticSkillSeidJsonData8.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (StaticSkillSeidJsonData8.OnInitFinishAction != null)
			{
				StaticSkillSeidJsonData8.OnInitFinishAction();
			}
		}

		// Token: 0x060043B3 RID: 17331 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x0400449D RID: 17565
		public static int SEIDID = 8;

		// Token: 0x0400449E RID: 17566
		public static Dictionary<int, StaticSkillSeidJsonData8> DataDict = new Dictionary<int, StaticSkillSeidJsonData8>();

		// Token: 0x0400449F RID: 17567
		public static List<StaticSkillSeidJsonData8> DataList = new List<StaticSkillSeidJsonData8>();

		// Token: 0x040044A0 RID: 17568
		public static Action OnInitFinishAction = new Action(StaticSkillSeidJsonData8.OnInitFinish);

		// Token: 0x040044A1 RID: 17569
		public int skillid;

		// Token: 0x040044A2 RID: 17570
		public int value1;
	}
}
