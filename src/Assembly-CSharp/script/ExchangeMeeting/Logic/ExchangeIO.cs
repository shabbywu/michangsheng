using System;
using System.Collections.Generic;
using Bag;
using Boo.Lang.Runtime;
using JSONClass;
using script.ExchangeMeeting.Logic.Interface;
using UnityEngine;

namespace script.ExchangeMeeting.Logic
{
	// Token: 0x02000A3E RID: 2622
	public class ExchangeIO : IExchangeIO
	{
		// Token: 0x06004803 RID: 18435 RVA: 0x001E6DCC File Offset: 0x001E4FCC
		public override void Add(IExchangeData data)
		{
			if (data is PlayerExchangeData)
			{
				base.source.playerList.Add(data);
				return;
			}
			if (data is SysExchangeData)
			{
				base.source.sysList.Add(data);
			}
		}

		// Token: 0x06004804 RID: 18436 RVA: 0x001E6E01 File Offset: 0x001E5001
		public override void Remove(IExchangeData data)
		{
			if (data is PlayerExchangeData)
			{
				base.source.playerList.Remove(data);
				return;
			}
			if (data is SysExchangeData)
			{
				base.source.sysList.Remove(data);
			}
		}

		// Token: 0x06004805 RID: 18437 RVA: 0x001E6E38 File Offset: 0x001E5038
		public override bool NeedUpdateNpcExchange()
		{
			return base.source.NextUpdate <= PlayerEx.Player.worldTimeMag.getNowTime();
		}

		// Token: 0x06004806 RID: 18438 RVA: 0x001E6E5C File Offset: 0x001E505C
		public override List<int> GetGuDingList()
		{
			List<int> list = new List<int>();
			foreach (GuDingExchangeData guDingExchangeData in GuDingExchangeData.DataList)
			{
				if (!base.source.guDingList.Contains(guDingExchangeData.id))
				{
					if (guDingExchangeData.EventValue.Count == 2)
					{
						int value = PlayerEx.Player.StaticValue.Value[guDingExchangeData.EventValue[0]];
						int value2 = guDingExchangeData.EventValue[1];
						if (!ToolsEx.IsMatching(value, value2, guDingExchangeData.fuhao))
						{
							continue;
						}
					}
					else if (guDingExchangeData.EventValue.Count > 0)
					{
						Debug.LogError("判断数目异常已跳过,固定交易事件id:" + guDingExchangeData.id);
						continue;
					}
					list.Add(guDingExchangeData.id);
				}
			}
			return list;
		}

		// Token: 0x06004807 RID: 18439 RVA: 0x001E6F4C File Offset: 0x001E514C
		public override void CreateNpcExchange()
		{
			if (base.source.sysList == null)
			{
				base.source.sysList = new List<IExchangeData>();
				Debug.LogError("sysList is null");
			}
			else
			{
				base.source.sysList.Clear();
			}
			List<int> guDingList = this.GetGuDingList();
			int num = 0;
			if (guDingList.Count > 0)
			{
				num += guDingList.Count;
				foreach (int id in guDingList)
				{
					base.source.sysList.Add(IExchangeMag.Inst.ExchangeDataFactory.Create(id, 2));
				}
			}
			if (num >= 5)
			{
				num = this.minRandomNum;
			}
			else
			{
				num = this.minTotalNum - num;
			}
			using (List<int>.Enumerator enumerator = this.GetRandomExchangeList(num).GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					int id2 = enumerator.Current;
					base.source.sysList.Add(IExchangeMag.Inst.ExchangeDataFactory.Create(id2, 1));
				}
				goto IL_125;
			}
			IL_108:
			base.source.NextUpdate = base.source.NextUpdate.AddYears(50);
			IL_125:
			if (!(base.source.NextUpdate <= PlayerEx.Player.worldTimeMag.getNowTime()))
			{
				return;
			}
			goto IL_108;
		}

		// Token: 0x06004808 RID: 18440 RVA: 0x001E70BC File Offset: 0x001E52BC
		public override void CreatePlayerExchange(List<BaseItem> needs, List<BaseItem> gets)
		{
			this.Add(IExchangeMag.Inst.ExchangeDataFactory.Create(needs, gets));
		}

		// Token: 0x06004809 RID: 18441 RVA: 0x001E70D8 File Offset: 0x001E52D8
		protected override List<int> GetRandomExchangeList(int count)
		{
			Dictionary<int, int> dictionary = new Dictionary<int, int>();
			foreach (RandomExchangeData randomExchangeData in RandomExchangeData.DataList)
			{
				dictionary.Add(randomExchangeData.id, randomExchangeData.percent);
			}
			List<int> list = new List<int>();
			for (int i = 0; i < count; i++)
			{
				list.Add(this.GetRandomId(dictionary));
				dictionary.Remove(list[i]);
			}
			if (list.Count != count)
			{
				throw new RuntimeException(string.Format("随机指定数量系统交易事件失败,需求数量为:{0},实际数量为:{1},", count, list.Count) + "请检测GetRandomExchangeList方法");
			}
			return list;
		}

		// Token: 0x0600480A RID: 18442 RVA: 0x001E71A4 File Offset: 0x001E53A4
		public override List<IExchangeData> GetPlayerList()
		{
			return base.source.playerList;
		}

		// Token: 0x0600480B RID: 18443 RVA: 0x001E71B1 File Offset: 0x001E53B1
		public override List<IExchangeData> GetSystemList()
		{
			return base.source.sysList;
		}

		// Token: 0x0600480C RID: 18444 RVA: 0x001E71BE File Offset: 0x001E53BE
		public override void SaveGuDingId(int id)
		{
			base.source.guDingList.Add(id);
		}

		// Token: 0x0600480D RID: 18445 RVA: 0x001E71D4 File Offset: 0x001E53D4
		private int GetRandomId(Dictionary<int, int> dict)
		{
			int num = 0;
			foreach (int num2 in dict.Values)
			{
				num += num2;
			}
			int num3 = this.random.Next(0, num + 1);
			int num4 = 0;
			foreach (int num5 in dict.Keys)
			{
				num4 += dict[num5];
				if (num4 >= num3)
				{
					return num5;
				}
			}
			throw new RuntimeException("GetRandomId出错，权重随机方法异常");
		}
	}
}
