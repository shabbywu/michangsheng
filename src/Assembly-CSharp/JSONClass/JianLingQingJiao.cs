using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000BF8 RID: 3064
	public class JianLingQingJiao : IJSONClass
	{
		// Token: 0x06004B48 RID: 19272 RVA: 0x001FCA64 File Offset: 0x001FAC64
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

		// Token: 0x06004B49 RID: 19273 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004790 RID: 18320
		public static Dictionary<string, JianLingQingJiao> DataDict = new Dictionary<string, JianLingQingJiao>();

		// Token: 0x04004791 RID: 18321
		public static List<JianLingQingJiao> DataList = new List<JianLingQingJiao>();

		// Token: 0x04004792 RID: 18322
		public static Action OnInitFinishAction = new Action(JianLingQingJiao.OnInitFinish);

		// Token: 0x04004793 RID: 18323
		public int JiYi;

		// Token: 0x04004794 RID: 18324
		public int SkillID;

		// Token: 0x04004795 RID: 18325
		public int StaticSkillID;

		// Token: 0x04004796 RID: 18326
		public string id;

		// Token: 0x04004797 RID: 18327
		public string QingJiaoDuiHuaQian;

		// Token: 0x04004798 RID: 18328
		public string QingJiaoDuiHuaZhong;

		// Token: 0x04004799 RID: 18329
		public string QingJiaoDuiHuaHou;
	}
}
