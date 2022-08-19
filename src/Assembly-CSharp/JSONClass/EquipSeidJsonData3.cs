using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x0200083A RID: 2106
	public class EquipSeidJsonData3 : IJSONClass
	{
		// Token: 0x06003EFA RID: 16122 RVA: 0x001AE55C File Offset: 0x001AC75C
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.EquipSeidJsonData[3].list)
			{
				try
				{
					EquipSeidJsonData3 equipSeidJsonData = new EquipSeidJsonData3();
					equipSeidJsonData.id = jsonobject["id"].I;
					equipSeidJsonData.value1 = jsonobject["value1"].I;
					if (EquipSeidJsonData3.DataDict.ContainsKey(equipSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典EquipSeidJsonData3.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", equipSeidJsonData.id));
					}
					else
					{
						EquipSeidJsonData3.DataDict.Add(equipSeidJsonData.id, equipSeidJsonData);
						EquipSeidJsonData3.DataList.Add(equipSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典EquipSeidJsonData3.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (EquipSeidJsonData3.OnInitFinishAction != null)
			{
				EquipSeidJsonData3.OnInitFinishAction();
			}
		}

		// Token: 0x06003EFB RID: 16123 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003ACF RID: 15055
		public static int SEIDID = 3;

		// Token: 0x04003AD0 RID: 15056
		public static Dictionary<int, EquipSeidJsonData3> DataDict = new Dictionary<int, EquipSeidJsonData3>();

		// Token: 0x04003AD1 RID: 15057
		public static List<EquipSeidJsonData3> DataList = new List<EquipSeidJsonData3>();

		// Token: 0x04003AD2 RID: 15058
		public static Action OnInitFinishAction = new Action(EquipSeidJsonData3.OnInitFinish);

		// Token: 0x04003AD3 RID: 15059
		public int id;

		// Token: 0x04003AD4 RID: 15060
		public int value1;
	}
}
