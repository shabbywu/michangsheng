using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x020008BC RID: 2236
	public class NvZuJianBiao : IJSONClass
	{
		// Token: 0x06004103 RID: 16643 RVA: 0x001BD24C File Offset: 0x001BB44C
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.NvZuJianBiao.list)
			{
				try
				{
					NvZuJianBiao nvZuJianBiao = new NvZuJianBiao();
					nvZuJianBiao.id = jsonobject["id"].I;
					nvZuJianBiao.StrID = jsonobject["StrID"].Str;
					if (NvZuJianBiao.DataDict.ContainsKey(nvZuJianBiao.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典NvZuJianBiao.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", nvZuJianBiao.id));
					}
					else
					{
						NvZuJianBiao.DataDict.Add(nvZuJianBiao.id, nvZuJianBiao);
						NvZuJianBiao.DataList.Add(nvZuJianBiao);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典NvZuJianBiao.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (NvZuJianBiao.OnInitFinishAction != null)
			{
				NvZuJianBiao.OnInitFinishAction();
			}
		}

		// Token: 0x06004104 RID: 16644 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003FDB RID: 16347
		public static Dictionary<int, NvZuJianBiao> DataDict = new Dictionary<int, NvZuJianBiao>();

		// Token: 0x04003FDC RID: 16348
		public static List<NvZuJianBiao> DataList = new List<NvZuJianBiao>();

		// Token: 0x04003FDD RID: 16349
		public static Action OnInitFinishAction = new Action(NvZuJianBiao.OnInitFinish);

		// Token: 0x04003FDE RID: 16350
		public int id;

		// Token: 0x04003FDF RID: 16351
		public string StrID;
	}
}
