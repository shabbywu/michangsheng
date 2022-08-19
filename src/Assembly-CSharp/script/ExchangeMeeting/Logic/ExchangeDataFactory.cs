using System;
using System.Collections.Generic;
using Bag;
using Boo.Lang.Runtime;
using JSONClass;
using script.ExchangeMeeting.Logic.Interface;
using UnityEngine;

namespace script.ExchangeMeeting.Logic
{
	// Token: 0x02000A3D RID: 2621
	public class ExchangeDataFactory : IExchangeDataFactory
	{
		// Token: 0x060047FE RID: 18430 RVA: 0x001E6928 File Offset: 0x001E4B28
		public override IExchangeData Create(int id, int type)
		{
			List<BaseItem> list = new List<BaseItem>();
			Dictionary<int, int> dictionary = new Dictionary<int, int>();
			List<BaseItem> list2 = new List<BaseItem>();
			Dictionary<int, Dictionary<int, int>> conditionDict;
			if (type != 1)
			{
				if (type != 2)
				{
					throw new RuntimeException(string.Format("type类型错误：{0}", type));
				}
				if (!GuDingExchangeData.DataDict.ContainsKey(id))
				{
					throw new RuntimeException(string.Format("在随机易物表中不存在Id：{0}", id));
				}
				GuDingExchangeData guDingExchangeData = GuDingExchangeData.DataDict[id];
				if (guDingExchangeData.YiWuFlag.Count != guDingExchangeData.NumFlag.Count)
				{
					throw new RuntimeException(string.Format("YiWuFlag和NumFlag数量不一致，请检查固定易物表Id为：{0}", id));
				}
				if (guDingExchangeData.YiWuItem.Count != guDingExchangeData.NumItem.Count)
				{
					throw new RuntimeException(string.Format("YiWuItem和NumItem数量不一致，请检查固定易物表Id为：{0}", id));
				}
				conditionDict = this.GetConditionDict(guDingExchangeData.YiWuFlag, guDingExchangeData.NumFlag, guDingExchangeData.YiWuItem, guDingExchangeData.NumItem);
				list2.Add(BaseItem.Create(guDingExchangeData.ItemID, 1, Tools.getUUID(), Tools.CreateItemSeid(guDingExchangeData.ItemID)));
			}
			else
			{
				if (!RandomExchangeData.DataDict.ContainsKey(id))
				{
					throw new RuntimeException(string.Format("在随机易物表中不存在Id：{0}", id));
				}
				RandomExchangeData randomExchangeData = RandomExchangeData.DataDict[id];
				if (randomExchangeData.YiWuFlag.Count != randomExchangeData.NumFlag.Count)
				{
					throw new RuntimeException(string.Format("YiWuFlag和NumFlag数量不一致，请检查随机易物表Id为：{0}", id));
				}
				if (randomExchangeData.YiWuItem.Count != randomExchangeData.NumItem.Count)
				{
					throw new RuntimeException(string.Format("YiWuItem和NumItem数量不一致，请检查随机易物表Id为：{0}", id));
				}
				conditionDict = this.GetConditionDict(randomExchangeData.YiWuFlag, randomExchangeData.NumFlag, randomExchangeData.YiWuItem, randomExchangeData.NumItem);
				list2.Add(BaseItem.Create(randomExchangeData.ItemID, 1, Tools.getUUID(), Tools.CreateItemSeid(randomExchangeData.ItemID)));
			}
			foreach (int num in conditionDict.Keys)
			{
				if (num == 1)
				{
					using (Dictionary<int, int>.KeyCollection.Enumerator enumerator2 = conditionDict[num].Keys.GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							int key = enumerator2.Current;
							dictionary.Add(key, conditionDict[num][key]);
						}
						continue;
					}
				}
				foreach (int num2 in conditionDict[num].Keys)
				{
					list.Add(BaseItem.Create(num2, conditionDict[num][num2], Tools.getUUID(), Tools.CreateItemSeid(num2)));
				}
			}
			return new SysExchangeData(list, dictionary, list2, id, type == 2);
		}

		// Token: 0x060047FF RID: 18431 RVA: 0x001E6C44 File Offset: 0x001E4E44
		public override IExchangeData Create(List<BaseItem> needItems, List<BaseItem> giveItems)
		{
			return new PlayerExchangeData(needItems, giveItems);
		}

		// Token: 0x06004800 RID: 18432 RVA: 0x001E6C4D File Offset: 0x001E4E4D
		protected override int GetConditionCount()
		{
			return this.random.Next(2, 4);
		}

		// Token: 0x06004801 RID: 18433 RVA: 0x001E6C5C File Offset: 0x001E4E5C
		protected override Dictionary<int, Dictionary<int, int>> GetConditionDict(List<int> tags1, List<int> nums1, List<int> items1, List<int> nums2)
		{
			Dictionary<int, Dictionary<int, int>> dictionary = new Dictionary<int, Dictionary<int, int>>();
			List<int> list = new List<int>(tags1);
			List<int> list2 = new List<int>(nums1);
			List<int> list3 = new List<int>(items1);
			List<int> list4 = new List<int>(nums2);
			dictionary.Add(1, new Dictionary<int, int>());
			dictionary.Add(2, new Dictionary<int, int>());
			int num = tags1.Count + items1.Count;
			int num2 = this.GetConditionCount();
			if (num2 > num)
			{
				num2 = num;
			}
			try
			{
				for (int i = 0; i < num2; i++)
				{
					int num3;
					if (list.Count > 0 && list3.Count > 0)
					{
						if (num2 == 2)
						{
							num3 = 1;
						}
						else
						{
							num3 = 2;
						}
					}
					else if (list.Count > 0)
					{
						num3 = 1;
					}
					else
					{
						num3 = 2;
					}
					if (num3 == 1)
					{
						int index = this.random.Next(0, list.Count);
						dictionary[num3].Add(list[index], list2[index]);
						list.RemoveAt(index);
						list2.RemoveAt(index);
					}
					else
					{
						int index = this.random.Next(0, list3.Count);
						dictionary[num3].Add(list3[index], list4[index]);
						list3.RemoveAt(index);
						list4.RemoveAt(index);
					}
				}
			}
			catch (Exception ex)
			{
				Debug.LogError(ex);
				throw new RuntimeException("GetConditionDict运行出错");
			}
			return dictionary;
		}
	}
}
