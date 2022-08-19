using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000961 RID: 2401
	public class StaticSkillSeidJsonData1 : IJSONClass
	{
		// Token: 0x06004396 RID: 17302 RVA: 0x001CC864 File Offset: 0x001CAA64
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.StaticSkillSeidJsonData[1].list)
			{
				try
				{
					StaticSkillSeidJsonData1 staticSkillSeidJsonData = new StaticSkillSeidJsonData1();
					staticSkillSeidJsonData.skillid = jsonobject["skillid"].I;
					staticSkillSeidJsonData.target = jsonobject["target"].I;
					staticSkillSeidJsonData.value1 = jsonobject["value1"].ToList();
					staticSkillSeidJsonData.value2 = jsonobject["value2"].ToList();
					if (StaticSkillSeidJsonData1.DataDict.ContainsKey(staticSkillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典StaticSkillSeidJsonData1.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", staticSkillSeidJsonData.skillid));
					}
					else
					{
						StaticSkillSeidJsonData1.DataDict.Add(staticSkillSeidJsonData.skillid, staticSkillSeidJsonData);
						StaticSkillSeidJsonData1.DataList.Add(staticSkillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典StaticSkillSeidJsonData1.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (StaticSkillSeidJsonData1.OnInitFinishAction != null)
			{
				StaticSkillSeidJsonData1.OnInitFinishAction();
			}
		}

		// Token: 0x06004397 RID: 17303 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004471 RID: 17521
		public static int SEIDID = 1;

		// Token: 0x04004472 RID: 17522
		public static Dictionary<int, StaticSkillSeidJsonData1> DataDict = new Dictionary<int, StaticSkillSeidJsonData1>();

		// Token: 0x04004473 RID: 17523
		public static List<StaticSkillSeidJsonData1> DataList = new List<StaticSkillSeidJsonData1>();

		// Token: 0x04004474 RID: 17524
		public static Action OnInitFinishAction = new Action(StaticSkillSeidJsonData1.OnInitFinish);

		// Token: 0x04004475 RID: 17525
		public int skillid;

		// Token: 0x04004476 RID: 17526
		public int target;

		// Token: 0x04004477 RID: 17527
		public List<int> value1 = new List<int>();

		// Token: 0x04004478 RID: 17528
		public List<int> value2 = new List<int>();
	}
}
