using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x0200081E RID: 2078
	public class CreateAvatarJsonData : IJSONClass
	{
		// Token: 0x06003E8A RID: 16010 RVA: 0x001AB604 File Offset: 0x001A9804
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.CreateAvatarJsonData.list)
			{
				try
				{
					CreateAvatarJsonData createAvatarJsonData = new CreateAvatarJsonData();
					createAvatarJsonData.id = jsonobject["id"].I;
					createAvatarJsonData.fenZu = jsonobject["fenZu"].I;
					createAvatarJsonData.feiYong = jsonobject["feiYong"].I;
					createAvatarJsonData.fenLeiGuanLian = jsonobject["fenLeiGuanLian"].I;
					createAvatarJsonData.jiesuo = jsonobject["jiesuo"].I;
					createAvatarJsonData.Title = jsonobject["Title"].Str;
					createAvatarJsonData.fenLei = jsonobject["fenLei"].Str;
					createAvatarJsonData.Desc = jsonobject["Desc"].Str;
					createAvatarJsonData.Info = jsonobject["Info"].Str;
					createAvatarJsonData.seid = jsonobject["seid"].ToList();
					if (CreateAvatarJsonData.DataDict.ContainsKey(createAvatarJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典CreateAvatarJsonData.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", createAvatarJsonData.id));
					}
					else
					{
						CreateAvatarJsonData.DataDict.Add(createAvatarJsonData.id, createAvatarJsonData);
						CreateAvatarJsonData.DataList.Add(createAvatarJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典CreateAvatarJsonData.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (CreateAvatarJsonData.OnInitFinishAction != null)
			{
				CreateAvatarJsonData.OnInitFinishAction();
			}
		}

		// Token: 0x06003E8B RID: 16011 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x040039DD RID: 14813
		public static Dictionary<int, CreateAvatarJsonData> DataDict = new Dictionary<int, CreateAvatarJsonData>();

		// Token: 0x040039DE RID: 14814
		public static List<CreateAvatarJsonData> DataList = new List<CreateAvatarJsonData>();

		// Token: 0x040039DF RID: 14815
		public static Action OnInitFinishAction = new Action(CreateAvatarJsonData.OnInitFinish);

		// Token: 0x040039E0 RID: 14816
		public int id;

		// Token: 0x040039E1 RID: 14817
		public int fenZu;

		// Token: 0x040039E2 RID: 14818
		public int feiYong;

		// Token: 0x040039E3 RID: 14819
		public int fenLeiGuanLian;

		// Token: 0x040039E4 RID: 14820
		public int jiesuo;

		// Token: 0x040039E5 RID: 14821
		public string Title;

		// Token: 0x040039E6 RID: 14822
		public string fenLei;

		// Token: 0x040039E7 RID: 14823
		public string Desc;

		// Token: 0x040039E8 RID: 14824
		public string Info;

		// Token: 0x040039E9 RID: 14825
		public List<int> seid = new List<int>();
	}
}
