using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000CF1 RID: 3313
	public class SuiJiTouXiangGeShuJsonData : IJSONClass
	{
		// Token: 0x06004F2C RID: 20268 RVA: 0x002133C0 File Offset: 0x002115C0
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

		// Token: 0x06004F2D RID: 20269 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004FCE RID: 20430
		public static Dictionary<string, SuiJiTouXiangGeShuJsonData> DataDict = new Dictionary<string, SuiJiTouXiangGeShuJsonData>();

		// Token: 0x04004FCF RID: 20431
		public static List<SuiJiTouXiangGeShuJsonData> DataList = new List<SuiJiTouXiangGeShuJsonData>();

		// Token: 0x04004FD0 RID: 20432
		public static Action OnInitFinishAction = new Action(SuiJiTouXiangGeShuJsonData.OnInitFinish);

		// Token: 0x04004FD1 RID: 20433
		public int All;

		// Token: 0x04004FD2 RID: 20434
		public string StrID;

		// Token: 0x04004FD3 RID: 20435
		public string colorset;

		// Token: 0x04004FD4 RID: 20436
		public string Type;

		// Token: 0x04004FD5 RID: 20437
		public string ChildType;

		// Token: 0x04004FD6 RID: 20438
		public string ImageName;

		// Token: 0x04004FD7 RID: 20439
		public List<int> AvatarSex1 = new List<int>();

		// Token: 0x04004FD8 RID: 20440
		public List<int> SuiJiSex1 = new List<int>();

		// Token: 0x04004FD9 RID: 20441
		public List<int> Sex1 = new List<int>();

		// Token: 0x04004FDA RID: 20442
		public List<int> AvatarSex2 = new List<int>();

		// Token: 0x04004FDB RID: 20443
		public List<int> SuiJiSex2 = new List<int>();

		// Token: 0x04004FDC RID: 20444
		public List<int> Sex2 = new List<int>();
	}
}
