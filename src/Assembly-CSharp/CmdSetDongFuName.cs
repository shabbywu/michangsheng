using System;
using Fungus;
using UnityEngine;

// Token: 0x02000315 RID: 789
[CommandInfo("YSDongFu", "让玩家设置洞府名字", "让玩家设置洞府名字", 0)]
[AddComponentMenu("")]
public class CmdSetDongFuName : Command
{
	// Token: 0x0600175D RID: 5981 RVA: 0x000149F6 File Offset: 0x00012BF6
	public override void OnEnter()
	{
		this.OpenInputBox();
		this.Continue();
	}

	// Token: 0x0600175E RID: 5982 RVA: 0x00014A04 File Offset: 0x00012C04
	public void OpenInputBox()
	{
		UInputBox.Show("为洞府命名", delegate(string s)
		{
			if (string.IsNullOrWhiteSpace(s))
			{
				this.OpenInputBox();
				return;
			}
			if (s.Length > 6)
			{
				UIPopTip.Inst.Pop("名字太长了", PopTipIconType.叹号);
				this.OpenInputBox();
				return;
			}
			DongFuManager.SetDongFuName(this.dongFuID, s);
		});
	}

	// Token: 0x040012B9 RID: 4793
	[Tooltip("洞府ID")]
	[SerializeField]
	protected int dongFuID;
}
