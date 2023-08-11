using System;
using KBEngine;
using UnityEngine;

public class BiGuanXiuLian : MonoBehaviour
{
	public UIBiGuan uibiguan;

	private void Start()
	{
	}

	private void Update()
	{
		Avatar player = Tools.instance.getPlayer();
		string screenName = Tools.getScreenName();
		DateTime residueTime = player.zulinContorl.getResidueTime(screenName);
		int num = (int)((float)((ulong)jsonData.instance.LevelUpDataJsonData[player.level.ToString()]["MaxExp"].n - player.exp) / (uibiguan.getBiguanSpeed() + player.getTimeExpSpeed())) + 1;
		int timeSum = ZulinContorl.GetTimeSum(residueTime);
		int maxTime = Mathf.Min(Mathf.Min(240, timeSum), num);
		uibiguan.MaxTime = maxTime;
		uibiguan.InputOnChenge();
	}
}
