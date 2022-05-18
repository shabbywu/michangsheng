using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000C43 RID: 3139
	public class NPCWuDaoJson : IJSONClass
	{
		// Token: 0x06004C75 RID: 19573 RVA: 0x00205090 File Offset: 0x00203290
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.NPCWuDaoJson.list)
			{
				try
				{
					NPCWuDaoJson npcwuDaoJson = new NPCWuDaoJson();
					npcwuDaoJson.id = jsonobject["id"].I;
					npcwuDaoJson.Type = jsonobject["Type"].I;
					npcwuDaoJson.lv = jsonobject["lv"].I;
					npcwuDaoJson.value1 = jsonobject["value1"].I;
					npcwuDaoJson.value2 = jsonobject["value2"].I;
					npcwuDaoJson.value3 = jsonobject["value3"].I;
					npcwuDaoJson.value4 = jsonobject["value4"].I;
					npcwuDaoJson.value5 = jsonobject["value5"].I;
					npcwuDaoJson.value6 = jsonobject["value6"].I;
					npcwuDaoJson.value7 = jsonobject["value7"].I;
					npcwuDaoJson.value8 = jsonobject["value8"].I;
					npcwuDaoJson.value9 = jsonobject["value9"].I;
					npcwuDaoJson.value10 = jsonobject["value10"].I;
					npcwuDaoJson.value11 = jsonobject["value11"].I;
					npcwuDaoJson.value12 = jsonobject["value12"].I;
					npcwuDaoJson.wudaoID = jsonobject["wudaoID"].ToList();
					if (NPCWuDaoJson.DataDict.ContainsKey(npcwuDaoJson.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典NPCWuDaoJson.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", npcwuDaoJson.id));
					}
					else
					{
						NPCWuDaoJson.DataDict.Add(npcwuDaoJson.id, npcwuDaoJson);
						NPCWuDaoJson.DataList.Add(npcwuDaoJson);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典NPCWuDaoJson.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (NPCWuDaoJson.OnInitFinishAction != null)
			{
				NPCWuDaoJson.OnInitFinishAction();
			}
		}

		// Token: 0x06004C76 RID: 19574 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004AD9 RID: 19161
		public static Dictionary<int, NPCWuDaoJson> DataDict = new Dictionary<int, NPCWuDaoJson>();

		// Token: 0x04004ADA RID: 19162
		public static List<NPCWuDaoJson> DataList = new List<NPCWuDaoJson>();

		// Token: 0x04004ADB RID: 19163
		public static Action OnInitFinishAction = new Action(NPCWuDaoJson.OnInitFinish);

		// Token: 0x04004ADC RID: 19164
		public int id;

		// Token: 0x04004ADD RID: 19165
		public int Type;

		// Token: 0x04004ADE RID: 19166
		public int lv;

		// Token: 0x04004ADF RID: 19167
		public int value1;

		// Token: 0x04004AE0 RID: 19168
		public int value2;

		// Token: 0x04004AE1 RID: 19169
		public int value3;

		// Token: 0x04004AE2 RID: 19170
		public int value4;

		// Token: 0x04004AE3 RID: 19171
		public int value5;

		// Token: 0x04004AE4 RID: 19172
		public int value6;

		// Token: 0x04004AE5 RID: 19173
		public int value7;

		// Token: 0x04004AE6 RID: 19174
		public int value8;

		// Token: 0x04004AE7 RID: 19175
		public int value9;

		// Token: 0x04004AE8 RID: 19176
		public int value10;

		// Token: 0x04004AE9 RID: 19177
		public int value11;

		// Token: 0x04004AEA RID: 19178
		public int value12;

		// Token: 0x04004AEB RID: 19179
		public List<int> wudaoID = new List<int>();
	}
}
