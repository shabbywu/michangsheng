using System;
using KBEngine;
using UnityEngine;

// Token: 0x020005EC RID: 1516
public class BiGuanXiuLian : MonoBehaviour
{
	// Token: 0x0600260F RID: 9743 RVA: 0x000042DD File Offset: 0x000024DD
	private void Start()
	{
	}

	// Token: 0x06002610 RID: 9744 RVA: 0x0012D37C File Offset: 0x0012B57C
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

	// Token: 0x04002091 RID: 8337
	public UIBiGuan uibiguan;
}
