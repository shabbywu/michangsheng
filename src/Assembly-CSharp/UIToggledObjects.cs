using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000AC RID: 172
[AddComponentMenu("NGUI/Interaction/Toggled Objects")]
public class UIToggledObjects : MonoBehaviour
{
	// Token: 0x06000671 RID: 1649 RVA: 0x00077B80 File Offset: 0x00075D80
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

	// Token: 0x06000672 RID: 1650 RVA: 0x00077C08 File Offset: 0x00075E08
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

	// Token: 0x06000673 RID: 1651 RVA: 0x00009B89 File Offset: 0x00007D89
	private void Set(GameObject go, bool state)
	{
		if (go != null)
		{
			NGUITools.SetActive(go, state);
		}
	}

	// Token: 0x040004DF RID: 1247
	public List<GameObject> activate;

	// Token: 0x040004E0 RID: 1248
	public List<GameObject> deactivate;

	// Token: 0x040004E1 RID: 1249
	[HideInInspector]
	[SerializeField]
	private GameObject target;

	// Token: 0x040004E2 RID: 1250
	[HideInInspector]
	[SerializeField]
	private bool inverse;
}
