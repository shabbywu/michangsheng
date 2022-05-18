using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x02000526 RID: 1318
public class TySelect : MonoBehaviour
{
	// Token: 0x170002AE RID: 686
	// (get) Token: 0x060021C6 RID: 8646 RVA: 0x0001BC0B File Offset: 0x00019E0B
	public static TySelect inst
	{
		get
		{
			if (TySelect._inst == null)
			{
				TySelect._inst = Object.Instantiate<GameObject>(ResManager.inst.LoadPrefab("TySelect")).GetComponent<TySelect>();
			}
			return TySelect._inst;
		}
	}

	// Token: 0x060021C7 RID: 8647 RVA: 0x00119024 File Offset: 0x00117224
	public void Show(string mag, UnityAction ok = null, UnityAction quit = null, bool isDestorySelf = true)
	{
		this.desc.text = mag;
		if (ok != null)
		{
			this.okBtn.mouseUpEvent.AddListener(ok);
		}
		if (isDestorySelf)
		{
			this.okBtn.mouseUpEvent.AddListener(new UnityAction(this.Close));
		}
		if (quit != null)
		{
			this.returnBtn.mouseUpEvent.AddListener(quit);
		}
		this.returnBtn.mouseUpEvent.AddListener(new UnityAction(this.Close));
	}

	// Token: 0x060021C8 RID: 8648 RVA: 0x000111B3 File Offset: 0x0000F3B3
	public void Close()
	{
		Object.Destroy(base.gameObject);
	}

	// Token: 0x04001D3E RID: 7486
	public Text desc;

	// Token: 0x04001D3F RID: 7487
	public FpBtn okBtn;

	// Token: 0x04001D40 RID: 7488
	public FpBtn returnBtn;

	// Token: 0x04001D41 RID: 7489
	private static TySelect _inst;
}
