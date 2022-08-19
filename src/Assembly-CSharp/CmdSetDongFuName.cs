using System;
using Fungus;
using UnityEngine;

// Token: 0x02000200 RID: 512
[CommandInfo("YSDongFu", "让玩家设置洞府名字", "让玩家设置洞府名字", 0)]
[AddComponentMenu("")]
public class CmdSetDongFuName : Command
{
	// Token: 0x060014B3 RID: 5299 RVA: 0x00084C8F File Offset: 0x00082E8F
	public override void OnEnter()
	{
		this.OpenInputBox();
		this.Continue();
	}

	// Token: 0x060014B4 RID: 5300 RVA: 0x00084C9D File Offset: 0x00082E9D
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

	// Token: 0x04000F73 RID: 3955
	[Tooltip("洞府ID")]
	[SerializeField]
	protected int dongFuID;
}
