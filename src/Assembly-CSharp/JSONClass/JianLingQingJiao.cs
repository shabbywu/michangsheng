using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x0200086A RID: 2154
	public class JianLingQingJiao : IJSONClass
	{
		// Token: 0x06003FBA RID: 16314 RVA: 0x001B2EC8 File Offset: 0x001B10C8
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.JianLingQingJiao.list)
			{
				try
				{
					JianLingQingJiao jianLingQingJiao = new JianLingQingJiao();
					jianLingQingJiao.JiYi = jsonobject["JiYi"].I;
					jianLingQingJiao.SkillID = jsonobject["SkillID"].I;
					jianLingQingJiao.StaticSkillID = jsonobject["StaticSkillID"].I;
					jianLingQingJiao.id = jsonobject["id"].Str;
					jianLingQingJiao.QingJiaoDuiHuaQian = jsonobject["QingJiaoDuiHuaQian"].Str;
					jianLingQingJiao.QingJiaoDuiHuaZhong = jsonobject["QingJiaoDuiHuaZhong"].Str;
					jianLingQingJiao.QingJiaoDuiHuaHou = jsonobject["QingJiaoDuiHuaHou"].Str;
					if (JianLingQingJiao.DataDict.ContainsKey(jianLingQingJiao.id))
					{
						PreloadManager.LogException("!!!错误!!!向字典JianLingQingJiao.DataDict添加数据时出现重复的键，Key:" + jianLingQingJiao.id + "，已跳过，请检查配表");
					}
					else
					{
						JianLingQingJiao.DataDict.Add(jianLingQingJiao.id, jianLingQingJiao);
						JianLingQingJiao.DataList.Add(jianLingQingJiao);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典JianLingQingJiao.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (JianLingQingJiao.OnInitFinishAction != null)
			{
				JianLingQingJiao.OnInitFinishAction();
			}
		}

		// Token: 0x06003FBB RID: 16315 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003C37 RID: 15415
		public static Dictionary<string, JianLingQingJiao> DataDict = new Dictionary<string, JianLingQingJiao>();

		// Token: 0x04003C38 RID: 15416
		public static List<JianLingQingJiao> DataList = new List<JianLingQingJiao>();

		// Token: 0x04003C39 RID: 15417
		public static Action OnInitFinishAction = new Action(JianLingQingJiao.OnInitFinish);

		// Token: 0x04003C3A RID: 15418
		public int JiYi;

		// Token: 0x04003C3B RID: 15419
		public int SkillID;

		// Token: 0x04003C3C RID: 15420
		public int StaticSkillID;

		// Token: 0x04003C3D RID: 15421
		public string id;

		// Token: 0x04003C3E RID: 15422
		public string QingJiaoDuiHuaQian;

		// Token: 0x04003C3F RID: 15423
		public string QingJiaoDuiHuaZhong;

		// Token: 0x04003C40 RID: 15424
		public string QingJiaoDuiHuaHou;
	}
}
