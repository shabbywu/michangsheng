using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000969 RID: 2409
	public class StaticSkillSeidJsonData9 : IJSONClass
	{
		// Token: 0x060043B6 RID: 17334 RVA: 0x001CD36C File Offset: 0x001CB56C
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.StaticSkillSeidJsonData[9].list)
			{
				try
				{
					StaticSkillSeidJsonData9 staticSkillSeidJsonData = new StaticSkillSeidJsonData9();
					staticSkillSeidJsonData.skillid = jsonobject["skillid"].I;
					staticSkillSeidJsonData.Spine = jsonobject["Spine"].Str;
					staticSkillSeidJsonData.OnMoveEnter = jsonobject["OnMoveEnter"].Str;
					staticSkillSeidJsonData.OnMoveExit = jsonobject["OnMoveExit"].Str;
					staticSkillSeidJsonData.OnLoopMoveEnter = jsonobject["OnLoopMoveEnter"].Str;
					staticSkillSeidJsonData.OnLoopMoveExit = jsonobject["OnLoopMoveExit"].Str;
					if (StaticSkillSeidJsonData9.DataDict.ContainsKey(staticSkillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典StaticSkillSeidJsonData9.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", staticSkillSeidJsonData.skillid));
					}
					else
					{
						StaticSkillSeidJsonData9.DataDict.Add(staticSkillSeidJsonData.skillid, staticSkillSeidJsonData);
						StaticSkillSeidJsonData9.DataList.Add(staticSkillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典StaticSkillSeidJsonData9.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (StaticSkillSeidJsonData9.OnInitFinishAction != null)
			{
				StaticSkillSeidJsonData9.OnInitFinishAction();
			}
		}

		// Token: 0x060043B7 RID: 17335 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x040044A3 RID: 17571
		public static int SEIDID = 9;

		// Token: 0x040044A4 RID: 17572
		public static Dictionary<int, StaticSkillSeidJsonData9> DataDict = new Dictionary<int, StaticSkillSeidJsonData9>();

		// Token: 0x040044A5 RID: 17573
		public static List<StaticSkillSeidJsonData9> DataList = new List<StaticSkillSeidJsonData9>();

		// Token: 0x040044A6 RID: 17574
		public static Action OnInitFinishAction = new Action(StaticSkillSeidJsonData9.OnInitFinish);

		// Token: 0x040044A7 RID: 17575
		public int skillid;

		// Token: 0x040044A8 RID: 17576
		public string Spine;

		// Token: 0x040044A9 RID: 17577
		public string OnMoveEnter;

		// Token: 0x040044AA RID: 17578
		public string OnMoveExit;

		// Token: 0x040044AB RID: 17579
		public string OnLoopMoveEnter;

		// Token: 0x040044AC RID: 17580
		public string OnLoopMoveExit;
	}
}
