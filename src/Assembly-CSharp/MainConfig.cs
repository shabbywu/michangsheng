using System;
using Tab;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x02000334 RID: 820
public class MainConfig : TabSetPanel, IESCClose
{
	// Token: 0x06001C3B RID: 7227 RVA: 0x000CA4AA File Offset: 0x000C86AA
	public MainConfig(GameObject go) : base(go)
	{
	}

	// Token: 0x06001C3C RID: 7228 RVA: 0x000CA4B3 File Offset: 0x000C86B3
	protected override void Init()
	{
		base.Init();
		base.Get<FpBtn>("关闭").mouseUpEvent.AddListener(new UnityAction(this.Hide));
	}

	// Token: 0x06001C3D RID: 7229 RVA: 0x000CA4DD File Offset: 0x000C86DD
	public override void Show()
	{
		ESCCloseManager.Inst.RegisterClose(this);
		base.Show();
	}

	// Token: 0x06001C3E RID: 7230 RVA: 0x000CA4F0 File Offset: 0x000C86F0
	public bool TryEscClose()
	{
		this.Hide();
		ESCCloseManager.Inst.UnRegisterClose(this);
		return true;
	}
}
