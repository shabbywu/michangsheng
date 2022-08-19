using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x0200039D RID: 925
public class TySelect : MonoBehaviour
{
	// Token: 0x1700025C RID: 604
	// (get) Token: 0x06001E45 RID: 7749 RVA: 0x000D5685 File Offset: 0x000D3885
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

	// Token: 0x06001E46 RID: 7750 RVA: 0x000D56B8 File Offset: 0x000D38B8
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

	// Token: 0x06001E47 RID: 7751 RVA: 0x0005C928 File Offset: 0x0005AB28
	public void Close()
	{
		Object.Destroy(base.gameObject);
	}

	// Token: 0x040018D5 RID: 6357
	public Text desc;

	// Token: 0x040018D6 RID: 6358
	public FpBtn okBtn;

	// Token: 0x040018D7 RID: 6359
	public FpBtn returnBtn;

	// Token: 0x040018D8 RID: 6360
	private static TySelect _inst;
}
