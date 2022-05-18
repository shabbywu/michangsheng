using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000C3F RID: 3135
	public class NpcTalkShouCiJiaoTanData : IJSONClass
	{
		// Token: 0x06004C65 RID: 19557 RVA: 0x00204794 File Offset: 0x00202994
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.NpcTalkShouCiJiaoTanData.list)
			{
				try
				{
					NpcTalkShouCiJiaoTanData npcTalkShouCiJiaoTanData = new NpcTalkShouCiJiaoTanData();
					npcTalkShouCiJiaoTanData.id = jsonobject["id"].I;
					npcTalkShouCiJiaoTanData.JingJie = jsonobject["JingJie"].I;
					npcTalkShouCiJiaoTanData.ShengWang = jsonobject["ShengWang"].I;
					npcTalkShouCiJiaoTanData.XingGe = jsonobject["XingGe"].I;
					npcTalkShouCiJiaoTanData.HaoGanDu = jsonobject["HaoGanDu"].I;
					npcTalkShouCiJiaoTanData.FirstTalk = jsonobject["FirstTalk"].Str;
					if (NpcTalkShouCiJiaoTanData.DataDict.ContainsKey(npcTalkShouCiJiaoTanData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典NpcTalkShouCiJiaoTanData.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", npcTalkShouCiJiaoTanData.id));
					}
					else
					{
						NpcTalkShouCiJiaoTanData.DataDict.Add(npcTalkShouCiJiaoTanData.id, npcTalkShouCiJiaoTanData);
						NpcTalkShouCiJiaoTanData.DataList.Add(npcTalkShouCiJiaoTanData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典NpcTalkShouCiJiaoTanData.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (NpcTalkShouCiJiaoTanData.OnInitFinishAction != null)
			{
				NpcTalkShouCiJiaoTanData.OnInitFinishAction();
			}
		}

		// Token: 0x06004C66 RID: 19558 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004AA3 RID: 19107
		public static Dictionary<int, NpcTalkShouCiJiaoTanData> DataDict = new Dictionary<int, NpcTalkShouCiJiaoTanData>();

		// Token: 0x04004AA4 RID: 19108
		public static List<NpcTalkShouCiJiaoTanData> DataList = new List<NpcTalkShouCiJiaoTanData>();

		// Token: 0x04004AA5 RID: 19109
		public static Action OnInitFinishAction = new Action(NpcTalkShouCiJiaoTanData.OnInitFinish);

		// Token: 0x04004AA6 RID: 19110
		public int id;

		// Token: 0x04004AA7 RID: 19111
		public int JingJie;

		// Token: 0x04004AA8 RID: 19112
		public int ShengWang;

		// Token: 0x04004AA9 RID: 19113
		public int XingGe;

		// Token: 0x04004AAA RID: 19114
		public int HaoGanDu;

		// Token: 0x04004AAB RID: 19115
		public string FirstTalk;
	}
}
