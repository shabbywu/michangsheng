using System;
using System.Collections.Generic;

[Serializable]
public class EmailData
{
	public int npcId;

	public int questionId;

	public bool isPlayer;

	public bool isOld;

	public bool isPangBai;

	public bool isAnswer;

	public string npcName = "";

	public string sceneName = "null";

	public int answerId;

	public int oldId;

	public bool isOut;

	public bool isComplete;

	public string sendTime;

	public int DongFuId;

	public string daoYaoStr = "";

	public string xiaoYaoStr = "";

	public List<int> content = new List<int>();

	public List<int> contentKey = new List<int>();

	public int actionId;

	public List<int> item = new List<int>();

	public int outTime;

	public int addHaoGanDu;

	public CyPaiMaiInfo PaiMaiInfo;

	public RandomTask RandomTask;

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

	public EmailData(int npcId, bool isOld, int oldId, string sendTime)
	{
		this.sendTime = sendTime;
		this.npcId = npcId;
		this.isOld = isOld;
		this.oldId = oldId;
	}

	public EmailData(int npcId, int questionId, bool isPlayer, string sendTime)
	{
		this.sendTime = sendTime;
		this.npcId = npcId;
		this.questionId = questionId;
		this.isPlayer = isPlayer;
	}

	public EmailData(int npcId, int answerId, bool isAnswer, bool isPangBai, string sendTime)
	{
		this.sendTime = sendTime;
		this.npcId = npcId;
		this.isAnswer = isAnswer;
		this.isPangBai = isPangBai;
		this.answerId = answerId;
	}

	public bool CheckIsOut()
	{
		DateTime nowTime = Tools.instance.getPlayer().worldTimeMag.getNowTime();
		DateTime dateTime = DateTime.Parse(sendTime).AddMonths(outTime);
		if (nowTime > dateTime)
		{
			return true;
		}
		return false;
	}

	public string TimeToString()
	{
		DateTime dateTime = DateTime.Parse(sendTime);
		return $"{dateTime.Year}年{dateTime.Month}月{dateTime.Day}日";
	}
}
