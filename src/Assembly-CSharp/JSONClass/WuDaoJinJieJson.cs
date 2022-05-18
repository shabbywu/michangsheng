using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000D00 RID: 3328
	public class WuDaoJinJieJson : IJSONClass
	{
		// Token: 0x06004F6A RID: 20330 RVA: 0x00215148 File Offset: 0x00213348
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.WuDaoJinJieJson.list)
			{
				try
				{
					WuDaoJinJieJson wuDaoJinJieJson = new WuDaoJinJieJson();
					wuDaoJinJieJson.id = jsonobject["id"].I;
					wuDaoJinJieJson.LV = jsonobject["LV"].I;
					wuDaoJinJieJson.Max = jsonobject["Max"].I;
					wuDaoJinJieJson.JiaCheng = jsonobject["JiaCheng"].I;
					wuDaoJinJieJson.LianDan = jsonobject["LianDan"].I;
					wuDaoJinJieJson.LianQi = jsonobject["LianQi"].I;
					wuDaoJinJieJson.Text = jsonobject["Text"].Str;
					if (WuDaoJinJieJson.DataDict.ContainsKey(wuDaoJinJieJson.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典WuDaoJinJieJson.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", wuDaoJinJieJson.id));
					}
					else
					{
						WuDaoJinJieJson.DataDict.Add(wuDaoJinJieJson.id, wuDaoJinJieJson);
						WuDaoJinJieJson.DataList.Add(wuDaoJinJieJson);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典WuDaoJinJieJson.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (WuDaoJinJieJson.OnInitFinishAction != null)
			{
				WuDaoJinJieJson.OnInitFinishAction();
			}
		}

		// Token: 0x06004F6B RID: 20331 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04005083 RID: 20611
		public static Dictionary<int, WuDaoJinJieJson> DataDict = new Dictionary<int, WuDaoJinJieJson>();

		// Token: 0x04005084 RID: 20612
		public static List<WuDaoJinJieJson> DataList = new List<WuDaoJinJieJson>();

		// Token: 0x04005085 RID: 20613
		public static Action OnInitFinishAction = new Action(WuDaoJinJieJson.OnInitFinish);

		// Token: 0x04005086 RID: 20614
		public int id;

		// Token: 0x04005087 RID: 20615
		public int LV;

		// Token: 0x04005088 RID: 20616
		public int Max;

		// Token: 0x04005089 RID: 20617
		public int JiaCheng;

		// Token: 0x0400508A RID: 20618
		public int LianDan;

		// Token: 0x0400508B RID: 20619
		public int LianQi;

		// Token: 0x0400508C RID: 20620
		public string Text;
	}
}
