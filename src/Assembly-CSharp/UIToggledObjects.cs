using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000079 RID: 121
[AddComponentMenu("NGUI/Interaction/Toggled Objects")]
public class UIToggledObjects : MonoBehaviour
{
	// Token: 0x06000605 RID: 1541 RVA: 0x000220CC File Offset: 0x000202CC
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

	// Token: 0x06000606 RID: 1542 RVA: 0x00022154 File Offset: 0x00020354
	public void Toggle()
	{
		bool value = UIToggle.current.value;
		if (base.enabled)
		{
			for (int i = 0; i < this.activate.Count; i++)
			{
				this.Set(this.activate[i], value);
			}
			for (int j = 0; j < this.deactivate.Count; j++)
			{
				this.Set(this.deactivate[j], !value);
			}
		}
	}

	// Token: 0x06000607 RID: 1543 RVA: 0x000221C9 File Offset: 0x000203C9
	private void Set(GameObject go, bool state)
	{
		if (go != null)
		{
			NGUITools.SetActive(go, state);
		}
	}

	// Token: 0x0400040F RID: 1039
	public List<GameObject> activate;

	// Token: 0x04000410 RID: 1040
	public List<GameObject> deactivate;

	// Token: 0x04000411 RID: 1041
	[HideInInspector]
	[SerializeField]
	private GameObject target;

	// Token: 0x04000412 RID: 1042
	[HideInInspector]
	[SerializeField]
	private bool inverse;
}
