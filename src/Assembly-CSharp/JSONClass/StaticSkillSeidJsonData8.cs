using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000CEC RID: 3308
	public class StaticSkillSeidJsonData8 : IJSONClass
	{
		// Token: 0x06004F18 RID: 20248 RVA: 0x00212D68 File Offset: 0x00210F68
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

		// Token: 0x06004F19 RID: 20249 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004FAD RID: 20397
		public static int SEIDID = 8;

		// Token: 0x04004FAE RID: 20398
		public static Dictionary<int, StaticSkillSeidJsonData8> DataDict = new Dictionary<int, StaticSkillSeidJsonData8>();

		// Token: 0x04004FAF RID: 20399
		public static List<StaticSkillSeidJsonData8> DataList = new List<StaticSkillSeidJsonData8>();

		// Token: 0x04004FB0 RID: 20400
		public static Action OnInitFinishAction = new Action(StaticSkillSeidJsonData8.OnInitFinish);

		// Token: 0x04004FB1 RID: 20401
		public int skillid;

		// Token: 0x04004FB2 RID: 20402
		public int value1;
	}
}
