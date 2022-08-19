using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x0200096D RID: 2413
	public class SuiJiTouXiangGeShuJsonData : IJSONClass
	{
		// Token: 0x060043C6 RID: 17350 RVA: 0x001CD944 File Offset: 0x001CBB44
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SuiJiTouXiangGeShuJsonData.list)
			{
				try
				{
					SuiJiTouXiangGeShuJsonData suiJiTouXiangGeShuJsonData = new SuiJiTouXiangGeShuJsonData();
					suiJiTouXiangGeShuJsonData.All = jsonobject["All"].I;
					suiJiTouXiangGeShuJsonData.StrID = jsonobject["StrID"].Str;
					suiJiTouXiangGeShuJsonData.colorset = jsonobject["colorset"].Str;
					suiJiTouXiangGeShuJsonData.Type = jsonobject["Type"].Str;
					suiJiTouXiangGeShuJsonData.ChildType = jsonobject["ChildType"].Str;
					suiJiTouXiangGeShuJsonData.ImageName = jsonobject["ImageName"].Str;
					suiJiTouXiangGeShuJsonData.AvatarSex1 = jsonobject["AvatarSex1"].ToList();
					suiJiTouXiangGeShuJsonData.SuiJiSex1 = jsonobject["SuiJiSex1"].ToList();
					suiJiTouXiangGeShuJsonData.Sex1 = jsonobject["Sex1"].ToList();
					suiJiTouXiangGeShuJsonData.AvatarSex2 = jsonobject["AvatarSex2"].ToList();
					suiJiTouXiangGeShuJsonData.SuiJiSex2 = jsonobject["SuiJiSex2"].ToList();
					suiJiTouXiangGeShuJsonData.Sex2 = jsonobject["Sex2"].ToList();
					if (SuiJiTouXiangGeShuJsonData.DataDict.ContainsKey(suiJiTouXiangGeShuJsonData.StrID))
					{
						PreloadManager.LogException("!!!错误!!!向字典SuiJiTouXiangGeShuJsonData.DataDict添加数据时出现重复的键，Key:" + suiJiTouXiangGeShuJsonData.StrID + "，已跳过，请检查配表");
					}
					else
					{
						SuiJiTouXiangGeShuJsonData.DataDict.Add(suiJiTouXiangGeShuJsonData.StrID, suiJiTouXiangGeShuJsonData);
						SuiJiTouXiangGeShuJsonData.DataList.Add(suiJiTouXiangGeShuJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SuiJiTouXiangGeShuJsonData.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SuiJiTouXiangGeShuJsonData.OnInitFinishAction != null)
			{
				SuiJiTouXiangGeShuJsonData.OnInitFinishAction();
			}
		}

		// Token: 0x060043C7 RID: 17351 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x040044BE RID: 17598
		public static Dictionary<string, SuiJiTouXiangGeShuJsonData> DataDict = new Dictionary<string, SuiJiTouXiangGeShuJsonData>();

		// Token: 0x040044BF RID: 17599
		public static List<SuiJiTouXiangGeShuJsonData> DataList = new List<SuiJiTouXiangGeShuJsonData>();

		// Token: 0x040044C0 RID: 17600
		public static Action OnInitFinishAction = new Action(SuiJiTouXiangGeShuJsonData.OnInitFinish);

		// Token: 0x040044C1 RID: 17601
		public int All;

		// Token: 0x040044C2 RID: 17602
		public string StrID;

		// Token: 0x040044C3 RID: 17603
		public string colorset;

		// Token: 0x040044C4 RID: 17604
		public string Type;

		// Token: 0x040044C5 RID: 17605
		public string ChildType;

		// Token: 0x040044C6 RID: 17606
		public string ImageName;

		// Token: 0x040044C7 RID: 17607
		public List<int> AvatarSex1 = new List<int>();

		// Token: 0x040044C8 RID: 17608
		public List<int> SuiJiSex1 = new List<int>();

		// Token: 0x040044C9 RID: 17609
		public List<int> Sex1 = new List<int>();

		// Token: 0x040044CA RID: 17610
		public List<int> AvatarSex2 = new List<int>();

		// Token: 0x040044CB RID: 17611
		public List<int> SuiJiSex2 = new List<int>();

		// Token: 0x040044CC RID: 17612
		public List<int> Sex2 = new List<int>();
	}
}
