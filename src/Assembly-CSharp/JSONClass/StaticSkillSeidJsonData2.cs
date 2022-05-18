using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000CE6 RID: 3302
	public class StaticSkillSeidJsonData2 : IJSONClass
	{
		// Token: 0x06004F00 RID: 20224 RVA: 0x00212678 File Offset: 0x00210878
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.StaticSkillSeidJsonData[2].list)
			{
				try
				{
					StaticSkillSeidJsonData2 staticSkillSeidJsonData = new StaticSkillSeidJsonData2();
					staticSkillSeidJsonData.skillid = jsonobject["skillid"].I;
					staticSkillSeidJsonData.value1 = jsonobject["value1"].I;
					if (StaticSkillSeidJsonData2.DataDict.ContainsKey(staticSkillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典StaticSkillSeidJsonData2.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", staticSkillSeidJsonData.skillid));
					}
					else
					{
						StaticSkillSeidJsonData2.DataDict.Add(staticSkillSeidJsonData.skillid, staticSkillSeidJsonData);
						StaticSkillSeidJsonData2.DataList.Add(staticSkillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典StaticSkillSeidJsonData2.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (StaticSkillSeidJsonData2.OnInitFinishAction != null)
			{
				StaticSkillSeidJsonData2.OnInitFinishAction();
			}
		}

		// Token: 0x06004F01 RID: 20225 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004F89 RID: 20361
		public static int SEIDID = 2;

		// Token: 0x04004F8A RID: 20362
		public static Dictionary<int, StaticSkillSeidJsonData2> DataDict = new Dictionary<int, StaticSkillSeidJsonData2>();

		// Token: 0x04004F8B RID: 20363
		public static List<StaticSkillSeidJsonData2> DataList = new List<StaticSkillSeidJsonData2>();

		// Token: 0x04004F8C RID: 20364
		public static Action OnInitFinishAction = new Action(StaticSkillSeidJsonData2.OnInitFinish);

		// Token: 0x04004F8D RID: 20365
		public int skillid;

		// Token: 0x04004F8E RID: 20366
		public int value1;
	}
}
