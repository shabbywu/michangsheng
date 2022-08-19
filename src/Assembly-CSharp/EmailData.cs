using System;
using System.Collections.Generic;

// Token: 0x020002A0 RID: 672
[Serializable]
public class EmailData
{
	// Token: 0x06001803 RID: 6147 RVA: 0x000A7804 File Offset: 0x000A5A04
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

	// Token: 0x06001804 RID: 6148 RVA: 0x000A78AC File Offset: 0x000A5AAC
	public EmailData(int npcId, bool isOld, int oldId, string sendTime)
	{
		this.sendTime = sendTime;
		this.npcId = npcId;
		this.isOld = isOld;
		this.oldId = oldId;
	}

	// Token: 0x06001805 RID: 6149 RVA: 0x000A792C File Offset: 0x000A5B2C
	public EmailData(int npcId, int questionId, bool isPlayer, string sendTime)
	{
		this.sendTime = sendTime;
		this.npcId = npcId;
		this.questionId = questionId;
		this.isPlayer = isPlayer;
	}

	// Token: 0x06001806 RID: 6150 RVA: 0x000A79AC File Offset: 0x000A5BAC
	public EmailData(int npcId, int answerId, bool isAnswer, bool isPangBai, string sendTime)
	{
		this.sendTime = sendTime;
		this.npcId = npcId;
		this.isAnswer = isAnswer;
		this.isPangBai = isPangBai;
		this.answerId = answerId;
	}

	// Token: 0x06001807 RID: 6151 RVA: 0x000A7A34 File Offset: 0x000A5C34
	public bool CheckIsOut()
	{
		DateTime nowTime = Tools.instance.getPlayer().worldTimeMag.getNowTime();
		DateTime t = DateTime.Parse(this.sendTime).AddMonths(this.outTime);
		return nowTime > t;
	}

	// Token: 0x06001808 RID: 6152 RVA: 0x000A7A7C File Offset: 0x000A5C7C
	public string TimeToString()
	{
		DateTime dateTime = DateTime.Parse(this.sendTime);
		return string.Format("{0}年{1}月{2}日", dateTime.Year, dateTime.Month, dateTime.Day);
	}

	// Token: 0x040012E8 RID: 4840
	public int npcId;

	// Token: 0x040012E9 RID: 4841
	public int questionId;

	// Token: 0x040012EA RID: 4842
	public bool isPlayer;

	// Token: 0x040012EB RID: 4843
	public bool isOld;

	// Token: 0x040012EC RID: 4844
	public bool isPangBai;

	// Token: 0x040012ED RID: 4845
	public bool isAnswer;

	// Token: 0x040012EE RID: 4846
	public string npcName = "";

	// Token: 0x040012EF RID: 4847
	public string sceneName = "null";

	// Token: 0x040012F0 RID: 4848
	public int answerId;

	// Token: 0x040012F1 RID: 4849
	public int oldId;

	// Token: 0x040012F2 RID: 4850
	public bool isOut;

	// Token: 0x040012F3 RID: 4851
	public bool isComplete;

	// Token: 0x040012F4 RID: 4852
	public string sendTime;

	// Token: 0x040012F5 RID: 4853
	public int DongFuId;

	// Token: 0x040012F6 RID: 4854
	public string daoYaoStr = "";

	// Token: 0x040012F7 RID: 4855
	public string xiaoYaoStr = "";

	// Token: 0x040012F8 RID: 4856
	public List<int> content = new List<int>();

	// Token: 0x040012F9 RID: 4857
	public List<int> contentKey = new List<int>();

	// Token: 0x040012FA RID: 4858
	public int actionId;

	// Token: 0x040012FB RID: 4859
	public List<int> item = new List<int>();

	// Token: 0x040012FC RID: 4860
	public int outTime;

	// Token: 0x040012FD RID: 4861
	public int addHaoGanDu;

	// Token: 0x040012FE RID: 4862
	public CyPaiMaiInfo PaiMaiInfo;

	// Token: 0x040012FF RID: 4863
	public RandomTask RandomTask;
}
