using System;
using UnityEngine;

// Token: 0x0200008E RID: 142
[AddComponentMenu("NGUI/Interaction/Key Navigation")]
public class UIKeyNavigation : MonoBehaviour
{
	// Token: 0x06000593 RID: 1427 RVA: 0x0000908A File Offset: 0x0000728A
	protected virtual void OnEnable()
	{
		UIKeyNavigation.list.Add(this);
		if (this.startsSelected && (UICamera.selectedObject == null || !NGUITools.GetActive(UICamera.selectedObject)))
		{
			UICamera.currentScheme = UICamera.ControlScheme.Controller;
			UICamera.selectedObject = base.gameObject;
		}
	}

	// Token: 0x06000594 RID: 1428 RVA: 0x000090C9 File Offset: 0x000072C9
	protected virtual void OnDisable()
	{
		UIKeyNavigation.list.Remove(this);
	}

	// Token: 0x06000595 RID: 1429 RVA: 0x000090D7 File Offset: 0x000072D7
	protected GameObject GetLeft()
	{
		if (NGUITools.GetActive(this.onLeft))
		{
			return this.onLeft;
		}
		if (this.constraint == UIKeyNavigation.Constraint.Vertical || this.constraint == UIKeyNavigation.Constraint.Explicit)
		{
			return null;
		}
		return this.Get(Vector3.left, true);
	}

	// Token: 0x06000596 RID: 1430 RVA: 0x0000910D File Offset: 0x0000730D
	private GameObject GetRight()
	{
		if (NGUITools.GetActive(this.onRight))
		{
			return this.onRight;
		}
		if (this.constraint == UIKeyNavigation.Constraint.Vertical || this.constraint == UIKeyNavigation.Constraint.Explicit)
		{
			return null;
		}
		return this.Get(Vector3.right, true);
	}

	// Token: 0x06000597 RID: 1431 RVA: 0x00009143 File Offset: 0x00007343
	protected GameObject GetUp()
	{
		if (NGUITools.GetActive(this.onUp))
		{
			return this.onUp;
		}
		if (this.constraint == UIKeyNavigation.Constraint.Horizontal || this.constraint == UIKeyNavigation.Constraint.Explicit)
		{
			return null;
		}
		return this.Get(Vector3.up, false);
	}

	// Token: 0x06000598 RID: 1432 RVA: 0x00009179 File Offset: 0x00007379
	protected GameObject GetDown()
	{
		if (NGUITools.GetActive(this.onDown))
		{
			return this.onDown;
		}
		if (this.constraint == UIKeyNavigation.Constraint.Horizontal || this.constraint == UIKeyNavigation.Constraint.Explicit)
		{
			return null;
		}
		return this.Get(Vector3.down, false);
	}

	// Token: 0x06000599 RID: 1433 RVA: 0x00072F04 File Offset: 0x00071104
	protected GameObject Get(Vector3 myDir, bool horizontal)
	{
		Transform transform = base.transform;
		myDir = transform.TransformDirection(myDir);
		Vector3 center = UIKeyNavigation.GetCenter(base.gameObject);
		float num = float.MaxValue;
		GameObject result = null;
		for (int i = 0; i < UIKeyNavigation.list.size; i++)
		{
			UIKeyNavigation uikeyNavigation = UIKeyNavigation.list[i];
			if (!(uikeyNavigation == this))
			{
				UIButton component = uikeyNavigation.GetComponent<UIButton>();
				if (!(component != null) || component.isEnabled)
				{
					Vector3 vector = UIKeyNavigation.GetCenter(uikeyNavigation.gameObject) - center;
					if (Vector3.Dot(myDir, vector.normalized) >= 0.707f)
					{
						vector = transform.InverseTransformDirection(vector);
						if (horizontal)
						{
							vector.y *= 2f;
						}
						else
						{
							vector.x *= 2f;
						}
						float sqrMagnitude = vector.sqrMagnitude;
						if (sqrMagnitude <= num)
						{
							result = uikeyNavigation.gameObject;
							num = sqrMagnitude;
						}
					}
				}
			}
		}
		return result;
	}

	// Token: 0x0600059A RID: 1434 RVA: 0x00072FFC File Offset: 0x000711FC
	protected static Vector3 GetCenter(GameObject go)
	{
		UIWidget component = go.GetComponent<UIWidget>();
		if (component != null)
		{
			Vector3[] worldCorners = component.worldCorners;
			return (worldCorners[0] + worldCorners[2]) * 0.5f;
		}
		return go.transform.position;
	}

	// Token: 0x0600059B RID: 1435 RVA: 0x0007304C File Offset: 0x0007124C
	protected virtual void OnKey(KeyCode key)
	{
		if (!NGUITools.GetActive(this))
		{
			return;
		}
		GameObject gameObject = null;
		if (key != 9)
		{
			switch (key)
			{
			case 273:
				gameObject = this.GetUp();
				break;
			case 274:
				gameObject = this.GetDown();
				break;
			case 275:
				gameObject = this.GetRight();
				break;
			case 276:
				gameObject = this.GetLeft();
				break;
			}
		}
		else if (Input.GetKey(304) || Input.GetKey(303))
		{
			gameObject = this.GetLeft();
			if (gameObject == null)
			{
				gameObject = this.GetUp();
			}
			if (gameObject == null)
			{
				gameObject = this.GetDown();
			}
			if (gameObject == null)
			{
				gameObject = this.GetRight();
			}
		}
		else
		{
			gameObject = this.GetRight();
			if (gameObject == null)
			{
				gameObject = this.GetDown();
			}
			if (gameObject == null)
			{
				gameObject = this.GetUp();
			}
			if (gameObject == null)
			{
				gameObject = this.GetLeft();
			}
		}
		if (gameObject != null)
		{
			UICamera.selectedObject = gameObject;
		}
	}

	// Token: 0x0600059C RID: 1436 RVA: 0x000091AF File Offset: 0x000073AF
	protected virtual void OnClick()
	{
		if (NGUITools.GetActive(this) && NGUITools.GetActive(this.onClick))
		{
			UICamera.selectedObject = this.onClick;
		}
	}

	// Token: 0x040003FD RID: 1021
	public static BetterList<UIKeyNavigation> list = new BetterList<UIKeyNavigation>();

	// Token: 0x040003FE RID: 1022
	public UIKeyNavigation.Constraint constraint;

	// Token: 0x040003FF RID: 1023
	public GameObject onUp;

	// Token: 0x04000400 RID: 1024
	public GameObject onDown;

	// Token: 0x04000401 RID: 1025
	public GameObject onLeft;

	// Token: 0x04000402 RID: 1026
	public GameObject onRight;

	// Token: 0x04000403 RID: 1027
	public GameObject onClick;

	// Token: 0x04000404 RID: 1028
	public bool startsSelected;

	// Token: 0x0200008F RID: 143
	public enum Constraint
	{
		// Token: 0x04000406 RID: 1030
		None,
		// Token: 0x04000407 RID: 1031
		Vertical,
		// Token: 0x04000408 RID: 1032
		Horizontal,
		// Token: 0x04000409 RID: 1033
		Explicit
	}
}
