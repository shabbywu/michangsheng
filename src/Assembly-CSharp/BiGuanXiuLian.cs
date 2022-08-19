using System;
using KBEngine;
using UnityEngine;

// Token: 0x02000435 RID: 1077
public class BiGuanXiuLian : MonoBehaviour
{
	// Token: 0x06002250 RID: 8784 RVA: 0x00004095 File Offset: 0x00002295
	private void Start()
	{
	}

	// Token: 0x06002251 RID: 8785 RVA: 0x000EC304 File Offset: 0x000EA504
	private void Update()
	{
		Avatar player = Tools.instance.getPlayer();
		string screenName = Tools.getScreenName();
		DateTime residueTime = player.zulinContorl.getResidueTime(screenName);
		int num = (int)(((ulong)jsonData.instance.LevelUpDataJsonData[player.level.ToString()]["MaxExp"].n - player.exp) / (this.uibiguan.getBiguanSpeed() + player.getTimeExpSpeed())) + 1;
		int timeSum = ZulinContorl.GetTimeSum(residueTime);
		int maxTime = Mathf.Min(Mathf.Min(240, timeSum), num);
		this.uibiguan.MaxTime = maxTime;
		this.uibiguan.InputOnChenge();
	}

	// Token: 0x04001BC5 RID: 7109
	public UIBiGuan uibiguan;
}
