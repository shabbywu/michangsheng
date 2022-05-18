using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000AB RID: 171
[ExecuteInEditMode]
[RequireComponent(typeof(UIToggle))]
[AddComponentMenu("NGUI/Interaction/Toggled Components")]
public class UIToggledComponents : MonoBehaviour
{
	// Token: 0x0600066E RID: 1646 RVA: 0x00077A7C File Offset: 0x00075C7C
	private void Awake()
	{
		if (this.target != null)
		{
			if (this.activate.Count == 0 && this.deactivate.Count == 0)
			{
				if (this.inverse)
				{
					this.deactivate.Add(this.target);
				}
				else
				{
					this.activate.Add(this.target);
				}
			}
			else
			{
				this.target = null;
			}
		}
		EventDelegate.Add(base.GetComponent<UIToggle>().onChange, new EventDelegate.Callback(this.Toggle));
	}

	// Token: 0x0600066F RID: 1647 RVA: 0x00077B04 File Offset: 0x00075D04
	public void Toggle()
	{
		if (base.enabled)
		{
			for (int i = 0; i < this.activate.Count; i++)
			{
				this.activate[i].enabled = UIToggle.current.value;
			}
			for (int j = 0; j < this.deactivate.Count; j++)
			{
				this.deactivate[j].enabled = !UIToggle.current.value;
			}
		}
	}

	// Token: 0x040004DB RID: 1243
	public List<MonoBehaviour> activate;

	// Token: 0x040004DC RID: 1244
	public List<MonoBehaviour> deactivate;

	// Token: 0x040004DD RID: 1245
	[HideInInspector]
	[SerializeField]
	private MonoBehaviour target;

	// Token: 0x040004DE RID: 1246
	[HideInInspector]
	[SerializeField]
	private bool inverse;
}
