using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000C38 RID: 3128
	public class NpcQiYuDate : IJSONClass
	{
		// Token: 0x06004C49 RID: 19529 RVA: 0x002037B0 File Offset: 0x002019B0
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

		// Token: 0x06004C4A RID: 19530 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004A2A RID: 18986
		public static Dictionary<int, NpcQiYuDate> DataDict = new Dictionary<int, NpcQiYuDate>();

		// Token: 0x04004A2B RID: 18987
		public static List<NpcQiYuDate> DataList = new List<NpcQiYuDate>();

		// Token: 0x04004A2C RID: 18988
		public static Action OnInitFinishAction = new Action(NpcQiYuDate.OnInitFinish);

		// Token: 0x04004A2D RID: 18989
		public int id;

		// Token: 0x04004A2E RID: 18990
		public int quanzhong;

		// Token: 0x04004A2F RID: 18991
		public int ZhuangTai;

		// Token: 0x04004A30 RID: 18992
		public int Itemnum;

		// Token: 0x04004A31 RID: 18993
		public int XiuWei;

		// Token: 0x04004A32 RID: 18994
		public int XueLiang;

		// Token: 0x04004A33 RID: 18995
		public string QiYuInfo;

		// Token: 0x04004A34 RID: 18996
		public List<int> JingJie = new List<int>();

		// Token: 0x04004A35 RID: 18997
		public List<int> Item = new List<int>();
	}
}
