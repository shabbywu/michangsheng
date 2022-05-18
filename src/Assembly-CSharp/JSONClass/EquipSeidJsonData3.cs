using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000BCB RID: 3019
	public class EquipSeidJsonData3 : IJSONClass
	{
		// Token: 0x06004A94 RID: 19092 RVA: 0x001F8EC8 File Offset: 0x001F70C8
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

		// Token: 0x06004A95 RID: 19093 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x0400463D RID: 17981
		public static int SEIDID = 3;

		// Token: 0x0400463E RID: 17982
		public static Dictionary<int, EquipSeidJsonData3> DataDict = new Dictionary<int, EquipSeidJsonData3>();

		// Token: 0x0400463F RID: 17983
		public static List<EquipSeidJsonData3> DataList = new List<EquipSeidJsonData3>();

		// Token: 0x04004640 RID: 17984
		public static Action OnInitFinishAction = new Action(EquipSeidJsonData3.OnInitFinish);

		// Token: 0x04004641 RID: 17985
		public int id;

		// Token: 0x04004642 RID: 17986
		public int value1;
	}
}
