using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000898 RID: 2200
	public class NpcBiaoBaiTiWenData : IJSONClass
	{
		// Token: 0x06004073 RID: 16499 RVA: 0x001B8188 File Offset: 0x001B6388
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.NpcBiaoBaiTiWenData.list)
			{
				try
				{
					NpcBiaoBaiTiWenData npcBiaoBaiTiWenData = new NpcBiaoBaiTiWenData();
					npcBiaoBaiTiWenData.id = jsonobject["id"].I;
					npcBiaoBaiTiWenData.TiWen = jsonobject["TiWen"].I;
					npcBiaoBaiTiWenData.XingGe = jsonobject["XingGe"].I;
					npcBiaoBaiTiWenData.BiaoQian = jsonobject["BiaoQian"].I;
					npcBiaoBaiTiWenData.optionDesc1 = jsonobject["optionDesc1"].I;
					npcBiaoBaiTiWenData.optionDesc2 = jsonobject["optionDesc2"].I;
					npcBiaoBaiTiWenData.optionDesc3 = jsonobject["optionDesc3"].I;
					if (NpcBiaoBaiTiWenData.DataDict.ContainsKey(npcBiaoBaiTiWenData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典NpcBiaoBaiTiWenData.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", npcBiaoBaiTiWenData.id));
					}
					else
					{
						NpcBiaoBaiTiWenData.DataDict.Add(npcBiaoBaiTiWenData.id, npcBiaoBaiTiWenData);
						NpcBiaoBaiTiWenData.DataList.Add(npcBiaoBaiTiWenData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典NpcBiaoBaiTiWenData.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (NpcBiaoBaiTiWenData.OnInitFinishAction != null)
			{
				NpcBiaoBaiTiWenData.OnInitFinishAction();
			}
		}

		// Token: 0x06004074 RID: 16500 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003DEE RID: 15854
		public static Dictionary<int, NpcBiaoBaiTiWenData> DataDict = new Dictionary<int, NpcBiaoBaiTiWenData>();

		// Token: 0x04003DEF RID: 15855
		public static List<NpcBiaoBaiTiWenData> DataList = new List<NpcBiaoBaiTiWenData>();

		// Token: 0x04003DF0 RID: 15856
		public static Action OnInitFinishAction = new Action(NpcBiaoBaiTiWenData.OnInitFinish);

		// Token: 0x04003DF1 RID: 15857
		public int id;

		// Token: 0x04003DF2 RID: 15858
		public int TiWen;

		// Token: 0x04003DF3 RID: 15859
		public int XingGe;

		// Token: 0x04003DF4 RID: 15860
		public int BiaoQian;

		// Token: 0x04003DF5 RID: 15861
		public int optionDesc1;

		// Token: 0x04003DF6 RID: 15862
		public int optionDesc2;

		// Token: 0x04003DF7 RID: 15863
		public int optionDesc3;
	}
}
