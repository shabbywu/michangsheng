using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000839 RID: 2105
	public class EquipSeidJsonData2 : IJSONClass
	{
		// Token: 0x06003EF6 RID: 16118 RVA: 0x001AE404 File Offset: 0x001AC604
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.EquipSeidJsonData[2].list)
			{
				try
				{
					EquipSeidJsonData2 equipSeidJsonData = new EquipSeidJsonData2();
					equipSeidJsonData.id = jsonobject["id"].I;
					equipSeidJsonData.value1 = jsonobject["value1"].I;
					if (EquipSeidJsonData2.DataDict.ContainsKey(equipSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典EquipSeidJsonData2.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", equipSeidJsonData.id));
					}
					else
					{
						EquipSeidJsonData2.DataDict.Add(equipSeidJsonData.id, equipSeidJsonData);
						EquipSeidJsonData2.DataList.Add(equipSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典EquipSeidJsonData2.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (EquipSeidJsonData2.OnInitFinishAction != null)
			{
				EquipSeidJsonData2.OnInitFinishAction();
			}
		}

		// Token: 0x06003EF7 RID: 16119 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003AC9 RID: 15049
		public static int SEIDID = 2;

		// Token: 0x04003ACA RID: 15050
		public static Dictionary<int, EquipSeidJsonData2> DataDict = new Dictionary<int, EquipSeidJsonData2>();

		// Token: 0x04003ACB RID: 15051
		public static List<EquipSeidJsonData2> DataList = new List<EquipSeidJsonData2>();

		// Token: 0x04003ACC RID: 15052
		public static Action OnInitFinishAction = new Action(EquipSeidJsonData2.OnInitFinish);

		// Token: 0x04003ACD RID: 15053
		public int id;

		// Token: 0x04003ACE RID: 15054
		public int value1;
	}
}
