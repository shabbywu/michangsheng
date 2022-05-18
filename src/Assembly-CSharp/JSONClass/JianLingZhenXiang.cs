using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000BFA RID: 3066
	public class JianLingZhenXiang : IJSONClass
	{
		// Token: 0x06004B50 RID: 19280 RVA: 0x001FCDBC File Offset: 0x001FAFBC
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.JianLingZhenXiang.list)
			{
				try
				{
					JianLingZhenXiang jianLingZhenXiang = new JianLingZhenXiang();
					jianLingZhenXiang.id = jsonobject["id"].Str;
					jianLingZhenXiang.desc = jsonobject["desc"].Str;
					if (JianLingZhenXiang.DataDict.ContainsKey(jianLingZhenXiang.id))
					{
						PreloadManager.LogException("!!!错误!!!向字典JianLingZhenXiang.DataDict添加数据时出现重复的键，Key:" + jianLingZhenXiang.id + "，已跳过，请检查配表");
					}
					else
					{
						JianLingZhenXiang.DataDict.Add(jianLingZhenXiang.id, jianLingZhenXiang);
						JianLingZhenXiang.DataList.Add(jianLingZhenXiang);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典JianLingZhenXiang.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (JianLingZhenXiang.OnInitFinishAction != null)
			{
				JianLingZhenXiang.OnInitFinishAction();
			}
		}

		// Token: 0x06004B51 RID: 19281 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x040047A4 RID: 18340
		public static Dictionary<string, JianLingZhenXiang> DataDict = new Dictionary<string, JianLingZhenXiang>();

		// Token: 0x040047A5 RID: 18341
		public static List<JianLingZhenXiang> DataList = new List<JianLingZhenXiang>();

		// Token: 0x040047A6 RID: 18342
		public static Action OnInitFinishAction = new Action(JianLingZhenXiang.OnInitFinish);

		// Token: 0x040047A7 RID: 18343
		public string id;

		// Token: 0x040047A8 RID: 18344
		public string desc;
	}
}
