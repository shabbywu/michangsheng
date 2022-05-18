using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000C26 RID: 3110
	public class NpcBiaoBaiTiWenData : IJSONClass
	{
		// Token: 0x06004C01 RID: 19457 RVA: 0x002014AC File Offset: 0x001FF6AC
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

		// Token: 0x06004C02 RID: 19458 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004947 RID: 18759
		public static Dictionary<int, NpcBiaoBaiTiWenData> DataDict = new Dictionary<int, NpcBiaoBaiTiWenData>();

		// Token: 0x04004948 RID: 18760
		public static List<NpcBiaoBaiTiWenData> DataList = new List<NpcBiaoBaiTiWenData>();

		// Token: 0x04004949 RID: 18761
		public static Action OnInitFinishAction = new Action(NpcBiaoBaiTiWenData.OnInitFinish);

		// Token: 0x0400494A RID: 18762
		public int id;

		// Token: 0x0400494B RID: 18763
		public int TiWen;

		// Token: 0x0400494C RID: 18764
		public int XingGe;

		// Token: 0x0400494D RID: 18765
		public int BiaoQian;

		// Token: 0x0400494E RID: 18766
		public int optionDesc1;

		// Token: 0x0400494F RID: 18767
		public int optionDesc2;

		// Token: 0x04004950 RID: 18768
		public int optionDesc3;
	}
}
