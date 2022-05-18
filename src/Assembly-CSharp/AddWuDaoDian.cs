using System;
using Fungus;
using script.YarnEditor.Command;
using UnityEngine;

// Token: 0x0200024F RID: 591
[CommandInfo("YSNew/Add", "增加悟道点", "增加悟道点", 0)]
[AddComponentMenu("")]
public class AddWuDaoDian : Command
{
	// Token: 0x06001206 RID: 4614 RVA: 0x000113B7 File Offset: 0x0000F5B7
	public override void OnEnter()
	{
		AddCommand.AddWuDaoDian(this.AddNum.Value);
		this.Continue();
	}

	// Token: 0x06001207 RID: 4615 RVA: 0x000113CF File Offset: 0x0000F5CF
	public override Color GetButtonColor()
	{
		return new Color32(184, 210, 235, byte.MaxValue);
	}

	// Token: 0x06001208 RID: 4616 RVA: 0x000042DD File Offset: 0x000024DD
	public override void OnReset()
	{
	}

	// Token: 0x04000E8F RID: 3727
	[Tooltip("增加悟道点数量")]
	[VariableProperty(new Type[]
	{
		typeof(IntegerVariable)
	})]
	[SerializeField]
	protected IntegerVariable AddNum;
}
