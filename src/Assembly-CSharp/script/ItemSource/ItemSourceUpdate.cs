using System;
using System.Collections.Generic;
using script.ItemSource.Interface;

namespace script.ItemSource
{
	// Token: 0x02000A1A RID: 2586
	public class ItemSourceUpdate : ABItemSourceUpdate
	{
		// Token: 0x0600477A RID: 18298 RVA: 0x001E3DD8 File Offset: 0x001E1FD8
		public override void Update(int times)
		{
			Dictionary<int, ABItemSourceData> itemSourceDataDic = ABItemSource.Get().ItemSourceDataDic;
			foreach (int key in itemSourceDataDic.Keys)
			{
				itemSourceDataDic[key].HasCostTime += times;
				if (itemSourceDataDic[key].HasCostTime >= itemSourceDataDic[key].UpdateTime)
				{
					itemSourceDataDic[key].HasCostTime = 0;
					itemSourceDataDic[key].Count = 1;
				}
			}
		}
	}
}
