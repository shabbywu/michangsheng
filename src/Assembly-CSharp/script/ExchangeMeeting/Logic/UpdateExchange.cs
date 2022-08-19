using System;
using System.Collections.Generic;
using script.ExchangeMeeting.Logic.Interface;
using script.ItemSource.Interface;
using UnityEngine;

namespace script.ExchangeMeeting.Logic
{
	// Token: 0x02000A3F RID: 2623
	public class UpdateExchange : IUpdateExchange
	{
		// Token: 0x0600480F RID: 18447 RVA: 0x001E72A4 File Offset: 0x001E54A4
		public override void UpdateProcess(int times)
		{
			List<IExchangeData> playerList = IExchangeMag.Inst.ExchangeIO.GetPlayerList();
			for (int i = playerList.Count - 1; i >= 0; i--)
			{
				PlayerExchangeData playerExchangeData = (PlayerExchangeData)playerList[i];
				if (playerExchangeData == null)
				{
					Debug.LogError("playerData is null 跳过");
				}
				else if (playerExchangeData.NeedUpdate)
				{
					playerExchangeData.HasCostTime += times;
					if (playerExchangeData.HasCostTime >= playerExchangeData.NeedTime)
					{
						if (ABItemSourceMag.Inst.IO.NeedCheckCount(playerExchangeData.NeedItems[0].Id) && !ABItemSourceMag.Inst.IO.Get(playerExchangeData.NeedItems[0].Id))
						{
							return;
						}
						this.SuccessExchange(playerExchangeData);
						IExchangeMag.Inst.ExchangeIO.Remove(playerExchangeData);
					}
				}
			}
		}

		// Token: 0x06004810 RID: 18448 RVA: 0x001E7379 File Offset: 0x001E5579
		protected override void SuccessExchange(IExchangeData data)
		{
			PlayerEx.Player.chuanYingManager.AddCyByExchange(2082912, data.NeedItems[0].Id);
		}
	}
}
