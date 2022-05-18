using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000C1B RID: 3099
	public class MenPaiFengLuBiao : IJSONClass
	{
		// Token: 0x06004BD5 RID: 19413 RVA: 0x00200254 File Offset: 0x001FE454
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.MenPaiFengLuBiao.list)
			{
				try
				{
					MenPaiFengLuBiao menPaiFengLuBiao = new MenPaiFengLuBiao();
					menPaiFengLuBiao.id = jsonobject["id"].I;
					menPaiFengLuBiao.MenKan = jsonobject["MenKan"].I;
					menPaiFengLuBiao.CD = jsonobject["CD"].I;
					menPaiFengLuBiao.money = jsonobject["money"].I;
					menPaiFengLuBiao.Name = jsonobject["Name"].Str;
					menPaiFengLuBiao.RenWu = jsonobject["RenWu"].ToList();
					menPaiFengLuBiao.haogandu = jsonobject["haogandu"].ToList();
					menPaiFengLuBiao.addMoney = jsonobject["addMoney"].ToList();
					if (MenPaiFengLuBiao.DataDict.ContainsKey(menPaiFengLuBiao.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典MenPaiFengLuBiao.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", menPaiFengLuBiao.id));
					}
					else
					{
						MenPaiFengLuBiao.DataDict.Add(menPaiFengLuBiao.id, menPaiFengLuBiao);
						MenPaiFengLuBiao.DataList.Add(menPaiFengLuBiao);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典MenPaiFengLuBiao.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (MenPaiFengLuBiao.OnInitFinishAction != null)
			{
				MenPaiFengLuBiao.OnInitFinishAction();
			}
		}

		// Token: 0x06004BD6 RID: 19414 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x040048D8 RID: 18648
		public static Dictionary<int, MenPaiFengLuBiao> DataDict = new Dictionary<int, MenPaiFengLuBiao>();

		// Token: 0x040048D9 RID: 18649
		public static List<MenPaiFengLuBiao> DataList = new List<MenPaiFengLuBiao>();

		// Token: 0x040048DA RID: 18650
		public static Action OnInitFinishAction = new Action(MenPaiFengLuBiao.OnInitFinish);

		// Token: 0x040048DB RID: 18651
		public int id;

		// Token: 0x040048DC RID: 18652
		public int MenKan;

		// Token: 0x040048DD RID: 18653
		public int CD;

		// Token: 0x040048DE RID: 18654
		public int money;

		// Token: 0x040048DF RID: 18655
		public string Name;

		// Token: 0x040048E0 RID: 18656
		public List<int> RenWu = new List<int>();

		// Token: 0x040048E1 RID: 18657
		public List<int> haogandu = new List<int>();

		// Token: 0x040048E2 RID: 18658
		public List<int> addMoney = new List<int>();
	}
}
