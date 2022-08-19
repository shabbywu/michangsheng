using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x0200083C RID: 2108
	public class EquipSeidJsonData8 : IJSONClass
	{
		// Token: 0x06003F02 RID: 16130 RVA: 0x001AE80C File Offset: 0x001ACA0C
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.EquipSeidJsonData[8].list)
			{
				try
				{
					EquipSeidJsonData8 equipSeidJsonData = new EquipSeidJsonData8();
					equipSeidJsonData.id = jsonobject["id"].I;
					equipSeidJsonData.value1 = jsonobject["value1"].I;
					if (EquipSeidJsonData8.DataDict.ContainsKey(equipSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典EquipSeidJsonData8.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", equipSeidJsonData.id));
					}
					else
					{
						EquipSeidJsonData8.DataDict.Add(equipSeidJsonData.id, equipSeidJsonData);
						EquipSeidJsonData8.DataList.Add(equipSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典EquipSeidJsonData8.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (EquipSeidJsonData8.OnInitFinishAction != null)
			{
				EquipSeidJsonData8.OnInitFinishAction();
			}
		}

		// Token: 0x06003F03 RID: 16131 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003ADB RID: 15067
		public static int SEIDID = 8;

		// Token: 0x04003ADC RID: 15068
		public static Dictionary<int, EquipSeidJsonData8> DataDict = new Dictionary<int, EquipSeidJsonData8>();

		// Token: 0x04003ADD RID: 15069
		public static List<EquipSeidJsonData8> DataList = new List<EquipSeidJsonData8>();

		// Token: 0x04003ADE RID: 15070
		public static Action OnInitFinishAction = new Action(EquipSeidJsonData8.OnInitFinish);

		// Token: 0x04003ADF RID: 15071
		public int id;

		// Token: 0x04003AE0 RID: 15072
		public int value1;
	}
}
