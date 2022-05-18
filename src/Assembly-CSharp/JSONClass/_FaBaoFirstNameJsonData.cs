using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000AD9 RID: 2777
	public class _FaBaoFirstNameJsonData : IJSONClass
	{
		// Token: 0x060046CE RID: 18126 RVA: 0x001E4B2C File Offset: 0x001E2D2C
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance._FaBaoFirstNameJsonData.list)
			{
				try
				{
					_FaBaoFirstNameJsonData faBaoFirstNameJsonData = new _FaBaoFirstNameJsonData();
					faBaoFirstNameJsonData.id = jsonobject["id"].I;
					faBaoFirstNameJsonData.FirstName = jsonobject["FirstName"].Str;
					faBaoFirstNameJsonData.Type = jsonobject["Type"].ToList();
					faBaoFirstNameJsonData.quality = jsonobject["quality"].ToList();
					if (_FaBaoFirstNameJsonData.DataDict.ContainsKey(faBaoFirstNameJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典_FaBaoFirstNameJsonData.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", faBaoFirstNameJsonData.id));
					}
					else
					{
						_FaBaoFirstNameJsonData.DataDict.Add(faBaoFirstNameJsonData.id, faBaoFirstNameJsonData);
						_FaBaoFirstNameJsonData.DataList.Add(faBaoFirstNameJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典_FaBaoFirstNameJsonData.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (_FaBaoFirstNameJsonData.OnInitFinishAction != null)
			{
				_FaBaoFirstNameJsonData.OnInitFinishAction();
			}
		}

		// Token: 0x060046CF RID: 18127 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003EF6 RID: 16118
		public static Dictionary<int, _FaBaoFirstNameJsonData> DataDict = new Dictionary<int, _FaBaoFirstNameJsonData>();

		// Token: 0x04003EF7 RID: 16119
		public static List<_FaBaoFirstNameJsonData> DataList = new List<_FaBaoFirstNameJsonData>();

		// Token: 0x04003EF8 RID: 16120
		public static Action OnInitFinishAction = new Action(_FaBaoFirstNameJsonData.OnInitFinish);

		// Token: 0x04003EF9 RID: 16121
		public int id;

		// Token: 0x04003EFA RID: 16122
		public string FirstName;

		// Token: 0x04003EFB RID: 16123
		public List<int> Type = new List<int>();

		// Token: 0x04003EFC RID: 16124
		public List<int> quality = new List<int>();
	}
}
