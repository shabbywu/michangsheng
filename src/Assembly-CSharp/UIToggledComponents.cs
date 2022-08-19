using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000078 RID: 120
[ExecuteInEditMode]
[RequireComponent(typeof(UIToggle))]
[AddComponentMenu("NGUI/Interaction/Toggled Components")]
public class UIToggledComponents : MonoBehaviour
{
	// Token: 0x06000602 RID: 1538 RVA: 0x00021FC8 File Offset: 0x000201C8
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

	// Token: 0x06000603 RID: 1539 RVA: 0x00022050 File Offset: 0x00020250
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

	// Token: 0x0400040B RID: 1035
	public List<MonoBehaviour> activate;

	// Token: 0x0400040C RID: 1036
	public List<MonoBehaviour> deactivate;

	// Token: 0x0400040D RID: 1037
	[HideInInspector]
	[SerializeField]
	private MonoBehaviour target;

	// Token: 0x0400040E RID: 1038
	[HideInInspector]
	[SerializeField]
	private bool inverse;
}
