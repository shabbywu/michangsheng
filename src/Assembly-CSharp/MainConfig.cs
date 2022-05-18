using System;
using Tab;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x020004A4 RID: 1188
public class MainConfig : TabSetPanel, IESCClose
{
	// Token: 0x06001F8B RID: 8075 RVA: 0x0001A094 File Offset: 0x00018294
	public MainConfig(GameObject go) : base(go)
	{
	}

	// Token: 0x06001F8C RID: 8076 RVA: 0x0001A09D File Offset: 0x0001829D
	protected override void Init()
	{
		base.Init();
		base.Get<FpBtn>("关闭").mouseUpEvent.AddListener(new UnityAction(this.Hide));
	}

	// Token: 0x06001F8D RID: 8077 RVA: 0x0001A0C7 File Offset: 0x000182C7
	public override void Show()
	{
		ESCCloseManager.Inst.RegisterClose(this);
		base.Show();
	}

	// Token: 0x06001F8E RID: 8078 RVA: 0x0001A0DA File Offset: 0x000182DA
	public bool TryEscClose()
	{
		this.Hide();
		ESCCloseManager.Inst.UnRegisterClose(this);
		return true;
	}
}
