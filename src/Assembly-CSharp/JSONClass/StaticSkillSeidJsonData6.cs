using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000CEA RID: 3306
	public class StaticSkillSeidJsonData6 : IJSONClass
	{
		// Token: 0x06004F10 RID: 20240 RVA: 0x00212B18 File Offset: 0x00210D18
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.StaticSkillSeidJsonData[6].list)
			{
				try
				{
					StaticSkillSeidJsonData6 staticSkillSeidJsonData = new StaticSkillSeidJsonData6();
					staticSkillSeidJsonData.skillid = jsonobject["skillid"].I;
					staticSkillSeidJsonData.value1 = jsonobject["value1"].I;
					if (StaticSkillSeidJsonData6.DataDict.ContainsKey(staticSkillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典StaticSkillSeidJsonData6.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", staticSkillSeidJsonData.skillid));
					}
					else
					{
						StaticSkillSeidJsonData6.DataDict.Add(staticSkillSeidJsonData.skillid, staticSkillSeidJsonData);
						StaticSkillSeidJsonData6.DataList.Add(staticSkillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典StaticSkillSeidJsonData6.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (StaticSkillSeidJsonData6.OnInitFinishAction != null)
			{
				StaticSkillSeidJsonData6.OnInitFinishAction();
			}
		}

		// Token: 0x06004F11 RID: 20241 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004FA1 RID: 20385
		public static int SEIDID = 6;

		// Token: 0x04004FA2 RID: 20386
		public static Dictionary<int, StaticSkillSeidJsonData6> DataDict = new Dictionary<int, StaticSkillSeidJsonData6>();

		// Token: 0x04004FA3 RID: 20387
		public static List<StaticSkillSeidJsonData6> DataList = new List<StaticSkillSeidJsonData6>();

		// Token: 0x04004FA4 RID: 20388
		public static Action OnInitFinishAction = new Action(StaticSkillSeidJsonData6.OnInitFinish);

		// Token: 0x04004FA5 RID: 20389
		public int skillid;

		// Token: 0x04004FA6 RID: 20390
		public int value1;
	}
}
