using System;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x02000370 RID: 880
public class AtrCallBack : MonoBehaviour
{
	// Token: 0x06001D78 RID: 7544 RVA: 0x000D046D File Offset: 0x000CE66D
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

	// Token: 0x06001D79 RID: 7545 RVA: 0x000D04A7 File Offset: 0x000CE6A7
	public void SetAction(UnityAction action)
	{
		this._action = action;
	}

	// Token: 0x0400180D RID: 6157
	private bool _isInit;

	// Token: 0x0400180E RID: 6158
	private UnityAction _action;
}
