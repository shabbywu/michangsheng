using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000CF5 RID: 3317
	public class ThreeSenceJsonData : IJSONClass
	{
		// Token: 0x06004F3C RID: 20284 RVA: 0x00213CBC File Offset: 0x00211EBC
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.ThreeSenceJsonData.list)
			{
				try
				{
					ThreeSenceJsonData threeSenceJsonData = new ThreeSenceJsonData();
					threeSenceJsonData.id = jsonobject["id"].I;
					threeSenceJsonData.SceneID = jsonobject["SceneID"].I;
					threeSenceJsonData.AvatarID = jsonobject["AvatarID"].I;
					threeSenceJsonData.TalkID = jsonobject["TalkID"].I;
					threeSenceJsonData.circulation = jsonobject["circulation"].I;
					threeSenceJsonData.transaction = jsonobject["transaction"].I;
					threeSenceJsonData.transaction2 = jsonobject["transaction2"].I;
					threeSenceJsonData.transaction3 = jsonobject["transaction3"].I;
					threeSenceJsonData.qiecuoLv = jsonobject["qiecuoLv"].I;
					threeSenceJsonData.StarTime = jsonobject["StarTime"].Str;
					threeSenceJsonData.EndTime = jsonobject["EndTime"].Str;
					threeSenceJsonData.FirstSay = jsonobject["FirstSay"].Str;
					threeSenceJsonData.Level = jsonobject["Level"].ToList();
					threeSenceJsonData.SaticValue = jsonobject["SaticValue"].ToList();
					threeSenceJsonData.SaticValueX = jsonobject["SaticValueX"].ToList();
					threeSenceJsonData.TaskIconValue = jsonobject["TaskIconValue"].ToList();
					threeSenceJsonData.TaskIconValueX = jsonobject["TaskIconValueX"].ToList();
					if (ThreeSenceJsonData.DataDict.ContainsKey(threeSenceJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典ThreeSenceJsonData.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", threeSenceJsonData.id));
					}
					else
					{
						ThreeSenceJsonData.DataDict.Add(threeSenceJsonData.id, threeSenceJsonData);
						ThreeSenceJsonData.DataList.Add(threeSenceJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典ThreeSenceJsonData.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (ThreeSenceJsonData.OnInitFinishAction != null)
			{
				ThreeSenceJsonData.OnInitFinishAction();
			}
		}

		// Token: 0x06004F3D RID: 20285 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x0400500D RID: 20493
		public static Dictionary<int, ThreeSenceJsonData> DataDict = new Dictionary<int, ThreeSenceJsonData>();

		// Token: 0x0400500E RID: 20494
		public static List<ThreeSenceJsonData> DataList = new List<ThreeSenceJsonData>();

		// Token: 0x0400500F RID: 20495
		public static Action OnInitFinishAction = new Action(ThreeSenceJsonData.OnInitFinish);

		// Token: 0x04005010 RID: 20496
		public int id;

		// Token: 0x04005011 RID: 20497
		public int SceneID;

		// Token: 0x04005012 RID: 20498
		public int AvatarID;

		// Token: 0x04005013 RID: 20499
		public int TalkID;

		// Token: 0x04005014 RID: 20500
		public int circulation;

		// Token: 0x04005015 RID: 20501
		public int transaction;

		// Token: 0x04005016 RID: 20502
		public int transaction2;

		// Token: 0x04005017 RID: 20503
		public int transaction3;

		// Token: 0x04005018 RID: 20504
		public int qiecuoLv;

		// Token: 0x04005019 RID: 20505
		public string StarTime;

		// Token: 0x0400501A RID: 20506
		public string EndTime;

		// Token: 0x0400501B RID: 20507
		public string FirstSay;

		// Token: 0x0400501C RID: 20508
		public List<int> Level = new List<int>();

		// Token: 0x0400501D RID: 20509
		public List<int> SaticValue = new List<int>();

		// Token: 0x0400501E RID: 20510
		public List<int> SaticValueX = new List<int>();

		// Token: 0x0400501F RID: 20511
		public List<int> TaskIconValue = new List<int>();

		// Token: 0x04005020 RID: 20512
		public List<int> TaskIconValueX = new List<int>();
	}
}
