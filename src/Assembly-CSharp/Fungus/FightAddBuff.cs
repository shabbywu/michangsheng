using KBEngine;
using UnityEngine;

namespace Fungus;

[CommandInfo("YSFight", "FightAddBuff", "增加Buff", 0)]
[AddComponentMenu("")]
public class FightAddBuff : Command
{
	[Tooltip("增加的角色(0主角,1敌人)")]
	[SerializeField]
	protected int type;

	[Tooltip("增加的BuffID")]
	[SerializeField]
	protected int BuffID;

	[Tooltip("增加的Buff层数")]
	[SerializeField]
	protected int BuffSum;

	public override void OnEnter()
	{
		Avatar player = Tools.instance.getPlayer();
		if (type == 1)
		{
			player.OtherAvatar.spell.addDBuff(BuffID, BuffSum);
		}
		else
		{
			player.spell.addDBuff(BuffID, BuffSum);
		}
		Continue();
	}

	public override Color GetButtonColor()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		return Color32.op_Implicit(new Color32((byte)184, (byte)210, (byte)235, byte.MaxValue));
	}

	public override void OnReset()
	{
	}
}
