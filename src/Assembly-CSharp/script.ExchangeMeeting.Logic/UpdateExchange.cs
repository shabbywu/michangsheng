using System.Collections.Generic;
using UnityEngine;
using script.ExchangeMeeting.Logic.Interface;
using script.ItemSource.Interface;

namespace script.ExchangeMeeting.Logic;

public class UpdateExchange : IUpdateExchange
{
	public override void UpdateProcess(int times)
	{
		List<IExchangeData> playerList = IExchangeMag.Inst.ExchangeIO.GetPlayerList();
		for (int num = playerList.Count - 1; num >= 0; num--)
		{
			PlayerExchangeData playerExchangeData = (PlayerExchangeData)playerList[num];
			if (playerExchangeData == null)
			{
				Debug.LogError((object)"playerData is null 跳过");
			}
			else if (playerExchangeData.NeedUpdate)
			{
				playerExchangeData.HasCostTime += times;
				if (playerExchangeData.HasCostTime >= playerExchangeData.NeedTime)
				{
					if (ABItemSourceMag.Inst.IO.NeedCheckCount(playerExchangeData.NeedItems[0].Id) && !ABItemSourceMag.Inst.IO.Get(playerExchangeData.NeedItems[0].Id))
					{
						break;
					}
					SuccessExchange(playerExchangeData);
					IExchangeMag.Inst.ExchangeIO.Remove(playerExchangeData);
				}
			}
		}
	}

	protected override void SuccessExchange(IExchangeData data)
	{
		PlayerEx.Player.chuanYingManager.AddCyByExchange(2082912, data.NeedItems[0].Id);
	}
}
