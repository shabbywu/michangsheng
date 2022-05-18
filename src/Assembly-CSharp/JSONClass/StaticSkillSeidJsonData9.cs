using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000CED RID: 3309
	public class StaticSkillSeidJsonData9 : IJSONClass
	{
		// Token: 0x06004F1C RID: 20252 RVA: 0x00212E90 File Offset: 0x00211090
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

		// Token: 0x06004F1D RID: 20253 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004FB3 RID: 20403
		public static int SEIDID = 9;

		// Token: 0x04004FB4 RID: 20404
		public static Dictionary<int, StaticSkillSeidJsonData9> DataDict = new Dictionary<int, StaticSkillSeidJsonData9>();

		// Token: 0x04004FB5 RID: 20405
		public static List<StaticSkillSeidJsonData9> DataList = new List<StaticSkillSeidJsonData9>();

		// Token: 0x04004FB6 RID: 20406
		public static Action OnInitFinishAction = new Action(StaticSkillSeidJsonData9.OnInitFinish);

		// Token: 0x04004FB7 RID: 20407
		public int skillid;

		// Token: 0x04004FB8 RID: 20408
		public string Spine;

		// Token: 0x04004FB9 RID: 20409
		public string OnMoveEnter;

		// Token: 0x04004FBA RID: 20410
		public string OnMoveExit;

		// Token: 0x04004FBB RID: 20411
		public string OnLoopMoveEnter;

		// Token: 0x04004FBC RID: 20412
		public string OnLoopMoveExit;
	}
}
