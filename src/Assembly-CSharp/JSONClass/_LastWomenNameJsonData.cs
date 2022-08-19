using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000746 RID: 1862
	public class _LastWomenNameJsonData : IJSONClass
	{
		// Token: 0x06003B2C RID: 15148 RVA: 0x00197088 File Offset: 0x00195288
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance._LastWomenNameJsonData.list)
			{
				try
				{
					_LastWomenNameJsonData lastWomenNameJsonData = new _LastWomenNameJsonData();
					lastWomenNameJsonData.id = jsonobject["id"].I;
					lastWomenNameJsonData.Name = jsonobject["Name"].Str;
					if (_LastWomenNameJsonData.DataDict.ContainsKey(lastWomenNameJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典_LastWomenNameJsonData.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", lastWomenNameJsonData.id));
					}
					else
					{
						_LastWomenNameJsonData.DataDict.Add(lastWomenNameJsonData.id, lastWomenNameJsonData);
						_LastWomenNameJsonData.DataList.Add(lastWomenNameJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典_LastWomenNameJsonData.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (_LastWomenNameJsonData.OnInitFinishAction != null)
			{
				_LastWomenNameJsonData.OnInitFinishAction();
			}
		}

		// Token: 0x06003B2D RID: 15149 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003394 RID: 13204
		public static Dictionary<int, _LastWomenNameJsonData> DataDict = new Dictionary<int, _LastWomenNameJsonData>();

		// Token: 0x04003395 RID: 13205
		public static List<_LastWomenNameJsonData> DataList = new List<_LastWomenNameJsonData>();

		// Token: 0x04003396 RID: 13206
		public static Action OnInitFinishAction = new Action(_LastWomenNameJsonData.OnInitFinish);

		// Token: 0x04003397 RID: 13207
		public int id;

		// Token: 0x04003398 RID: 13208
		public string Name;
	}
}
