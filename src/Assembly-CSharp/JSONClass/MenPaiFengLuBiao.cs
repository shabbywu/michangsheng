using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x0200088D RID: 2189
	public class MenPaiFengLuBiao : IJSONClass
	{
		// Token: 0x06004047 RID: 16455 RVA: 0x001B6CE0 File Offset: 0x001B4EE0
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

		// Token: 0x06004048 RID: 16456 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003D7F RID: 15743
		public static Dictionary<int, MenPaiFengLuBiao> DataDict = new Dictionary<int, MenPaiFengLuBiao>();

		// Token: 0x04003D80 RID: 15744
		public static List<MenPaiFengLuBiao> DataList = new List<MenPaiFengLuBiao>();

		// Token: 0x04003D81 RID: 15745
		public static Action OnInitFinishAction = new Action(MenPaiFengLuBiao.OnInitFinish);

		// Token: 0x04003D82 RID: 15746
		public int id;

		// Token: 0x04003D83 RID: 15747
		public int MenKan;

		// Token: 0x04003D84 RID: 15748
		public int CD;

		// Token: 0x04003D85 RID: 15749
		public int money;

		// Token: 0x04003D86 RID: 15750
		public string Name;

		// Token: 0x04003D87 RID: 15751
		public List<int> RenWu = new List<int>();

		// Token: 0x04003D88 RID: 15752
		public List<int> haogandu = new List<int>();

		// Token: 0x04003D89 RID: 15753
		public List<int> addMoney = new List<int>();
	}
}
