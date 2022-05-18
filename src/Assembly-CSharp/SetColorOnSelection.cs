using System;
using UnityEngine;

// Token: 0x02000062 RID: 98
[ExecuteInEditMode]
[RequireComponent(typeof(UIWidget))]
[AddComponentMenu("NGUI/Examples/Set Color on Selection")]
public class SetColorOnSelection : MonoBehaviour
{
	// Token: 0x060004B2 RID: 1202 RVA: 0x0006F738 File Offset: 0x0006D938
	public void SetSpriteBySelection()
	{
		if (UIPopupList.current == null)
		{
			return;
		}
		if (this.mWidget == null)
		{
			this.mWidget = base.GetComponent<UIWidget>();
		}
		string value = UIPopupList.current.value;
		uint num = <PrivateImplementationDetails>.ComputeStringHash(value);
		if (num <= 2743015548U)
		{
			if (num != 382078856U)
			{
				if (num != 1827351814U)
				{
					if (num != 2743015548U)
					{
						return;
					}
					if (!(value == "Red"))
					{
						return;
					}
					this.mWidget.color = Color.red;
					return;
				}
				else
				{
					if (!(value == "White"))
					{
						return;
					}
					this.mWidget.color = Color.white;
					return;
				}
			}
			else
			{
				if (!(value == "Magenta"))
				{
					return;
				}
				this.mWidget.color = Color.magenta;
				return;
			}
		}
		else if (num <= 3381604954U)
		{
			if (num != 2840840028U)
			{
				if (num != 3381604954U)
				{
					return;
				}
				if (!(value == "Cyan"))
				{
					return;
				}
				this.mWidget.color = Color.cyan;
				return;
			}
			else
			{
				if (!(value == "Green"))
				{
					return;
				}
				this.mWidget.color = Color.green;
				return;
			}
		}
		else if (num != 3654151273U)
		{
			if (num != 3923582957U)
			{
				return;
			}
			if (!(value == "Blue"))
			{
				return;
			}
			this.mWidget.color = Color.blue;
			return;
		}
		else
		{
			if (!(value == "Yellow"))
			{
				return;
			}
			this.mWidget.color = Color.yellow;
			return;
		}
	}

	// Token: 0x04000301 RID: 769
	private UIWidget mWidget;
}
