using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x020008AA RID: 2218
	public class NpcQiYuDate : IJSONClass
	{
		// Token: 0x060040BB RID: 16571 RVA: 0x001BA884 File Offset: 0x001B8A84
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.NpcQiYuDate.list)
			{
				try
				{
					NpcQiYuDate npcQiYuDate = new NpcQiYuDate();
					npcQiYuDate.id = jsonobject["id"].I;
					npcQiYuDate.quanzhong = jsonobject["quanzhong"].I;
					npcQiYuDate.ZhuangTai = jsonobject["ZhuangTai"].I;
					npcQiYuDate.Itemnum = jsonobject["Itemnum"].I;
					npcQiYuDate.XiuWei = jsonobject["XiuWei"].I;
					npcQiYuDate.XueLiang = jsonobject["XueLiang"].I;
					npcQiYuDate.QiYuInfo = jsonobject["QiYuInfo"].Str;
					npcQiYuDate.JingJie = jsonobject["JingJie"].ToList();
					npcQiYuDate.Item = jsonobject["Item"].ToList();
					if (NpcQiYuDate.DataDict.ContainsKey(npcQiYuDate.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典NpcQiYuDate.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", npcQiYuDate.id));
					}
					else
					{
						NpcQiYuDate.DataDict.Add(npcQiYuDate.id, npcQiYuDate);
						NpcQiYuDate.DataList.Add(npcQiYuDate);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典NpcQiYuDate.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (NpcQiYuDate.OnInitFinishAction != null)
			{
				NpcQiYuDate.OnInitFinishAction();
			}
		}

		// Token: 0x060040BC RID: 16572 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003ED2 RID: 16082
		public static Dictionary<int, NpcQiYuDate> DataDict = new Dictionary<int, NpcQiYuDate>();

		// Token: 0x04003ED3 RID: 16083
		public static List<NpcQiYuDate> DataList = new List<NpcQiYuDate>();

		// Token: 0x04003ED4 RID: 16084
		public static Action OnInitFinishAction = new Action(NpcQiYuDate.OnInitFinish);

		// Token: 0x04003ED5 RID: 16085
		public int id;

		// Token: 0x04003ED6 RID: 16086
		public int quanzhong;

		// Token: 0x04003ED7 RID: 16087
		public int ZhuangTai;

		// Token: 0x04003ED8 RID: 16088
		public int Itemnum;

		// Token: 0x04003ED9 RID: 16089
		public int XiuWei;

		// Token: 0x04003EDA RID: 16090
		public int XueLiang;

		// Token: 0x04003EDB RID: 16091
		public string QiYuInfo;

		// Token: 0x04003EDC RID: 16092
		public List<int> JingJie = new List<int>();

		// Token: 0x04003EDD RID: 16093
		public List<int> Item = new List<int>();
	}
}
