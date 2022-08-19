using System;
using Fungus;
using script.YarnEditor.Command;
using UnityEngine;

// Token: 0x02000172 RID: 370
[CommandInfo("YSNew/Add", "增加悟道点", "增加悟道点", 0)]
[AddComponentMenu("")]
public class AddWuDaoDian : Command
{
	// Token: 0x06000FA6 RID: 4006 RVA: 0x0005E210 File Offset: 0x0005C410
	public override void OnEnter()
	{
		AddCommand.AddWuDaoDian(this.AddNum.Value);
		this.Continue();
	}

	// Token: 0x06000FA7 RID: 4007 RVA: 0x0005E228 File Offset: 0x0005C428
	public override Color GetButtonColor()
	{
		return new Color32(184, 210, 235, byte.MaxValue);
	}

	// Token: 0x06000FA8 RID: 4008 RVA: 0x00004095 File Offset: 0x00002295
	public override void OnReset()
	{
	}

	// Token: 0x04000BBE RID: 3006
	[Tooltip("增加悟道点数量")]
	[VariableProperty(new Type[]
	{
		typeof(IntegerVariable)
	})]
	[SerializeField]
	protected IntegerVariable AddNum;
}
