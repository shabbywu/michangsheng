using Fungus;
using JSONClass;
using KBEngine;
using UnityEngine;

[CommandInfo("YSPlayer", "设置玩家化神领域", "设置玩家化神领域", 0)]
[AddComponentMenu("")]
public class CmdSetHuaShenLingYuSkill : Command
{
	[SerializeField]
	protected WuDaoType WuDao;

	public override void OnEnter()
	{
		Do(WuDao);
		Continue();
	}

	public static void Do(WuDaoType wuDao)
	{
		HuaShenData huaShenData = HuaShenData.DataDict[(int)wuDao];
		Avatar player = PlayerEx.Player;
		player.HuaShenLingYuSkill = new JSONObject(huaShenData.Skill);
		player.HuaShenWuDao = new JSONObject((int)wuDao);
	}
}
