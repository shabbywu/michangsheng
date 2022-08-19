using System;
using System.Collections.Generic;
using JSONClass;
using script.ItemSource.Interface;

namespace script.ItemSource
{
	// Token: 0x02000A19 RID: 2585
	public class ItemSourceMag : ABItemSourceMag
	{
		// Token: 0x06004778 RID: 18296 RVA: 0x001E3CEB File Offset: 0x001E1EEB
		public ItemSourceMag()
		{
			this.Init();
		}

		// Token: 0x06004779 RID: 18297 RVA: 0x001E3CFC File Offset: 0x001E1EFC
		private void Init()
		{
			this.Update = new ItemSourceUpdate();
			this.IO = new ItemSourceIO();
			Dictionary<int, ABItemSourceData> itemSourceDataDic = ABItemSource.Get().ItemSourceDataDic;
			foreach (_ItemJsonData itemJsonData in _ItemJsonData.DataList)
			{
				if (itemJsonData.id >= jsonData.QingJiaoItemIDSegment)
				{
					break;
				}
				if (itemJsonData.ShuaXin >= 1)
				{
					if (itemSourceDataDic.ContainsKey(itemJsonData.id))
					{
						itemSourceDataDic[itemJsonData.id].UpdateTime = itemJsonData.ShuaXin;
					}
					else
					{
						itemSourceDataDic.Add(itemJsonData.id, new ItemSourceData
						{
							Id = itemJsonData.id,
							UpdateTime = itemJsonData.ShuaXin,
							HasCostTime = 0
						});
					}
				}
			}
		}
	}
}
