using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000AE1 RID: 2785
	public class AllMapCaiJiBiao : IJSONClass
	{
		// Token: 0x060046EE RID: 18158 RVA: 0x001E59DC File Offset: 0x001E3BDC
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.AllMapCaiJiBiao.list)
			{
				try
				{
					AllMapCaiJiBiao allMapCaiJiBiao = new AllMapCaiJiBiao();
					allMapCaiJiBiao.ID = jsonobject["ID"].I;
					allMapCaiJiBiao.Item = jsonobject["Item"].I;
					allMapCaiJiBiao.percent = jsonobject["percent"].I;
					allMapCaiJiBiao.Monstar = jsonobject["Monstar"].I;
					allMapCaiJiBiao.MaiFuTime = jsonobject["MaiFuTime"].I;
					allMapCaiJiBiao.MaiFuMonstar = jsonobject["MaiFuMonstar"].I;
					allMapCaiJiBiao.Num = jsonobject["Num"].ToList();
					allMapCaiJiBiao.Level = jsonobject["Level"].ToList();
					if (AllMapCaiJiBiao.DataDict.ContainsKey(allMapCaiJiBiao.ID))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典AllMapCaiJiBiao.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", allMapCaiJiBiao.ID));
					}
					else
					{
						AllMapCaiJiBiao.DataDict.Add(allMapCaiJiBiao.ID, allMapCaiJiBiao);
						AllMapCaiJiBiao.DataList.Add(allMapCaiJiBiao);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典AllMapCaiJiBiao.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (AllMapCaiJiBiao.OnInitFinishAction != null)
			{
				AllMapCaiJiBiao.OnInitFinishAction();
			}
		}

		// Token: 0x060046EF RID: 18159 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003F58 RID: 16216
		public static Dictionary<int, AllMapCaiJiBiao> DataDict = new Dictionary<int, AllMapCaiJiBiao>();

		// Token: 0x04003F59 RID: 16217
		public static List<AllMapCaiJiBiao> DataList = new List<AllMapCaiJiBiao>();

		// Token: 0x04003F5A RID: 16218
		public static Action OnInitFinishAction = new Action(AllMapCaiJiBiao.OnInitFinish);

		// Token: 0x04003F5B RID: 16219
		public int ID;

		// Token: 0x04003F5C RID: 16220
		public int Item;

		// Token: 0x04003F5D RID: 16221
		public int percent;

		// Token: 0x04003F5E RID: 16222
		public int Monstar;

		// Token: 0x04003F5F RID: 16223
		public int MaiFuTime;

		// Token: 0x04003F60 RID: 16224
		public int MaiFuMonstar;

		// Token: 0x04003F61 RID: 16225
		public List<int> Num = new List<int>();

		// Token: 0x04003F62 RID: 16226
		public List<int> Level = new List<int>();
	}
}
