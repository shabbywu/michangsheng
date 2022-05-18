using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000C5B RID: 3163
	public class RenWuDaLeiYouXianJi : IJSONClass
	{
		// Token: 0x06004CD5 RID: 19669 RVA: 0x00207814 File Offset: 0x00205A14
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.RenWuDaLeiYouXianJi.list)
			{
				try
				{
					RenWuDaLeiYouXianJi renWuDaLeiYouXianJi = new RenWuDaLeiYouXianJi();
					renWuDaLeiYouXianJi.Id = jsonobject["Id"].I;
					renWuDaLeiYouXianJi.QuJian = jsonobject["QuJian"].ToList();
					if (RenWuDaLeiYouXianJi.DataDict.ContainsKey(renWuDaLeiYouXianJi.Id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典RenWuDaLeiYouXianJi.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", renWuDaLeiYouXianJi.Id));
					}
					else
					{
						RenWuDaLeiYouXianJi.DataDict.Add(renWuDaLeiYouXianJi.Id, renWuDaLeiYouXianJi);
						RenWuDaLeiYouXianJi.DataList.Add(renWuDaLeiYouXianJi);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典RenWuDaLeiYouXianJi.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (RenWuDaLeiYouXianJi.OnInitFinishAction != null)
			{
				RenWuDaLeiYouXianJi.OnInitFinishAction();
			}
		}

		// Token: 0x06004CD6 RID: 19670 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004BC7 RID: 19399
		public static Dictionary<int, RenWuDaLeiYouXianJi> DataDict = new Dictionary<int, RenWuDaLeiYouXianJi>();

		// Token: 0x04004BC8 RID: 19400
		public static List<RenWuDaLeiYouXianJi> DataList = new List<RenWuDaLeiYouXianJi>();

		// Token: 0x04004BC9 RID: 19401
		public static Action OnInitFinishAction = new Action(RenWuDaLeiYouXianJi.OnInitFinish);

		// Token: 0x04004BCA RID: 19402
		public int Id;

		// Token: 0x04004BCB RID: 19403
		public List<int> QuJian = new List<int>();
	}
}
