using System;
using System.Collections.Generic;

// Token: 0x020003D9 RID: 985
[Serializable]
public class EmailData
{
	// Token: 0x06001AF5 RID: 6901 RVA: 0x000EEA40 File Offset: 0x000ECC40
	public EmailData(int npcId, bool isOut, bool isComplete, List<int> content, int actionId, List<int> item, int outTime, int addHaoGanDu, string sendTime)
	{
		this.npcId = npcId;
		this.isComplete = isComplete;
		this.isOut = isOut;
		this.content = content;
		this.actionId = actionId;
		this.item = item;
		this.outTime = outTime;
		this.addHaoGanDu = addHaoGanDu;
		this.sendTime = sendTime;
	}

	// Token: 0x06001AF6 RID: 6902 RVA: 0x000EEAE8 File Offset: 0x000ECCE8
	public EmailData(int npcId, bool isOld, int oldId, string sendTime)
	{
		this.sendTime = sendTime;
		this.npcId = npcId;
		this.isOld = isOld;
		this.oldId = oldId;
	}

	// Token: 0x06001AF7 RID: 6903 RVA: 0x000EEB68 File Offset: 0x000ECD68
	public EmailData(int npcId, int questionId, bool isPlayer, string sendTime)
	{
		this.sendTime = sendTime;
		this.npcId = npcId;
		this.questionId = questionId;
		this.isPlayer = isPlayer;
	}

	// Token: 0x06001AF8 RID: 6904 RVA: 0x000EEBE8 File Offset: 0x000ECDE8
	public EmailData(int npcId, int answerId, bool isAnswer, bool isPangBai, string sendTime)
	{
		this.sendTime = sendTime;
		this.npcId = npcId;
		this.isAnswer = isAnswer;
		this.isPangBai = isPangBai;
		this.answerId = answerId;
	}

	// Token: 0x06001AF9 RID: 6905 RVA: 0x000EEC70 File Offset: 0x000ECE70
	public bool CheckIsOut()
	{
		DateTime nowTime = Tools.instance.getPlayer().worldTimeMag.getNowTime();
		DateTime t = DateTime.Parse(this.sendTime).AddMonths(this.outTime);
		return nowTime > t;
	}

	// Token: 0x06001AFA RID: 6906 RVA: 0x000EECB8 File Offset: 0x000ECEB8
	public string TimeToString()
	{
		DateTime dateTime = DateTime.Parse(this.sendTime);
		return string.Format("{0}年{1}月{2}日", dateTime.Year, dateTime.Month, dateTime.Day);
	}

	// Token: 0x04001684 RID: 5764
	public int npcId;

	// Token: 0x04001685 RID: 5765
	public int questionId;

	// Token: 0x04001686 RID: 5766
	public bool isPlayer;

	// Token: 0x04001687 RID: 5767
	public bool isOld;

	// Token: 0x04001688 RID: 5768
	public bool isPangBai;

	// Token: 0x04001689 RID: 5769
	public bool isAnswer;

	// Token: 0x0400168A RID: 5770
	public string npcName = "";

	// Token: 0x0400168B RID: 5771
	public string sceneName = "null";

	// Token: 0x0400168C RID: 5772
	public int answerId;

	// Token: 0x0400168D RID: 5773
	public int oldId;

	// Token: 0x0400168E RID: 5774
	public bool isOut;

	// Token: 0x0400168F RID: 5775
	public bool isComplete;

	// Token: 0x04001690 RID: 5776
	public string sendTime;

	// Token: 0x04001691 RID: 5777
	public int DongFuId;

	// Token: 0x04001692 RID: 5778
	public string daoYaoStr = "";

	// Token: 0x04001693 RID: 5779
	public string xiaoYaoStr = "";

	// Token: 0x04001694 RID: 5780
	public List<int> content = new List<int>();

	// Token: 0x04001695 RID: 5781
	public List<int> contentKey = new List<int>();

	// Token: 0x04001696 RID: 5782
	public int actionId;

	// Token: 0x04001697 RID: 5783
	public List<int> item = new List<int>();

	// Token: 0x04001698 RID: 5784
	public int outTime;

	// Token: 0x04001699 RID: 5785
	public int addHaoGanDu;

	// Token: 0x0400169A RID: 5786
	public CyPaiMaiInfo PaiMaiInfo;

	// Token: 0x0400169B RID: 5787
	public RandomTask RandomTask;
}
