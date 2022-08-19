using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000971 RID: 2417
	public class ThreeSenceJsonData : IJSONClass
	{
		// Token: 0x060043D6 RID: 17366 RVA: 0x001CE304 File Offset: 0x001CC504
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

		// Token: 0x060043D7 RID: 17367 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x040044FD RID: 17661
		public static Dictionary<int, ThreeSenceJsonData> DataDict = new Dictionary<int, ThreeSenceJsonData>();

		// Token: 0x040044FE RID: 17662
		public static List<ThreeSenceJsonData> DataList = new List<ThreeSenceJsonData>();

		// Token: 0x040044FF RID: 17663
		public static Action OnInitFinishAction = new Action(ThreeSenceJsonData.OnInitFinish);

		// Token: 0x04004500 RID: 17664
		public int id;

		// Token: 0x04004501 RID: 17665
		public int SceneID;

		// Token: 0x04004502 RID: 17666
		public int AvatarID;

		// Token: 0x04004503 RID: 17667
		public int TalkID;

		// Token: 0x04004504 RID: 17668
		public int circulation;

		// Token: 0x04004505 RID: 17669
		public int transaction;

		// Token: 0x04004506 RID: 17670
		public int transaction2;

		// Token: 0x04004507 RID: 17671
		public int transaction3;

		// Token: 0x04004508 RID: 17672
		public int qiecuoLv;

		// Token: 0x04004509 RID: 17673
		public string StarTime;

		// Token: 0x0400450A RID: 17674
		public string EndTime;

		// Token: 0x0400450B RID: 17675
		public string FirstSay;

		// Token: 0x0400450C RID: 17676
		public List<int> Level = new List<int>();

		// Token: 0x0400450D RID: 17677
		public List<int> SaticValue = new List<int>();

		// Token: 0x0400450E RID: 17678
		public List<int> SaticValueX = new List<int>();

		// Token: 0x0400450F RID: 17679
		public List<int> TaskIconValue = new List<int>();

		// Token: 0x04004510 RID: 17680
		public List<int> TaskIconValueX = new List<int>();
	}
}
