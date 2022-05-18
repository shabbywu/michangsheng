using System;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x020004EC RID: 1260
public class AtrCallBack : MonoBehaviour
{
	// Token: 0x060020D9 RID: 8409 RVA: 0x0001B15C File Offset: 0x0001935C
	private void OnEnable()
	{
		if (!this._isInit)
		{
			this._isInit = true;
			base.enabled = false;
			return;
		}
		if (this._action != null)
		{
			this._action.Invoke();
			base.enabled = false;
			this._action = null;
		}
	}

	// Token: 0x060020DA RID: 8410 RVA: 0x0001B196 File Offset: 0x00019396
	public void SetAction(UnityAction action)
	{
		this._action = action;
	}

	// Token: 0x04001C58 RID: 7256
	private bool _isInit;

	// Token: 0x04001C59 RID: 7257
	private UnityAction _action;
}
