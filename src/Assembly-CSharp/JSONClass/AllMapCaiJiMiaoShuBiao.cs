using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000AE2 RID: 2786
	public class AllMapCaiJiMiaoShuBiao : IJSONClass
	{
		// Token: 0x060046F2 RID: 18162 RVA: 0x001E5B9C File Offset: 0x001E3D9C
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.AllMapCaiJiMiaoShuBiao.list)
			{
				try
				{
					AllMapCaiJiMiaoShuBiao allMapCaiJiMiaoShuBiao = new AllMapCaiJiMiaoShuBiao();
					allMapCaiJiMiaoShuBiao.ID = jsonobject["ID"].I;
					allMapCaiJiMiaoShuBiao.desc1 = jsonobject["desc1"].Str;
					allMapCaiJiMiaoShuBiao.desc2 = jsonobject["desc2"].Str;
					allMapCaiJiMiaoShuBiao.desc3 = jsonobject["desc3"].Str;
					allMapCaiJiMiaoShuBiao.desc4 = jsonobject["desc4"].Str;
					allMapCaiJiMiaoShuBiao.desc5 = jsonobject["desc5"].Str;
					allMapCaiJiMiaoShuBiao.desc6 = jsonobject["desc6"].Str;
					allMapCaiJiMiaoShuBiao.desc7 = jsonobject["desc7"].Str;
					if (AllMapCaiJiMiaoShuBiao.DataDict.ContainsKey(allMapCaiJiMiaoShuBiao.ID))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典AllMapCaiJiMiaoShuBiao.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", allMapCaiJiMiaoShuBiao.ID));
					}
					else
					{
						AllMapCaiJiMiaoShuBiao.DataDict.Add(allMapCaiJiMiaoShuBiao.ID, allMapCaiJiMiaoShuBiao);
						AllMapCaiJiMiaoShuBiao.DataList.Add(allMapCaiJiMiaoShuBiao);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典AllMapCaiJiMiaoShuBiao.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (AllMapCaiJiMiaoShuBiao.OnInitFinishAction != null)
			{
				AllMapCaiJiMiaoShuBiao.OnInitFinishAction();
			}
		}

		// Token: 0x060046F3 RID: 18163 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003F63 RID: 16227
		public static Dictionary<int, AllMapCaiJiMiaoShuBiao> DataDict = new Dictionary<int, AllMapCaiJiMiaoShuBiao>();

		// Token: 0x04003F64 RID: 16228
		public static List<AllMapCaiJiMiaoShuBiao> DataList = new List<AllMapCaiJiMiaoShuBiao>();

		// Token: 0x04003F65 RID: 16229
		public static Action OnInitFinishAction = new Action(AllMapCaiJiMiaoShuBiao.OnInitFinish);

		// Token: 0x04003F66 RID: 16230
		public int ID;

		// Token: 0x04003F67 RID: 16231
		public string desc1;

		// Token: 0x04003F68 RID: 16232
		public string desc2;

		// Token: 0x04003F69 RID: 16233
		public string desc3;

		// Token: 0x04003F6A RID: 16234
		public string desc4;

		// Token: 0x04003F6B RID: 16235
		public string desc5;

		// Token: 0x04003F6C RID: 16236
		public string desc6;

		// Token: 0x04003F6D RID: 16237
		public string desc7;
	}
}
