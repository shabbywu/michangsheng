using System;
using UnityEngine;

// Token: 0x0200006B RID: 107
[AddComponentMenu("NGUI/Interaction/Key Navigation")]
public class UIKeyNavigation : MonoBehaviour
{
	// Token: 0x0600053D RID: 1341 RVA: 0x0001CA15 File Offset: 0x0001AC15
	protected virtual void OnEnable()
	{
		UIKeyNavigation.list.Add(this);
		if (this.startsSelected && (UICamera.selectedObject == null || !NGUITools.GetActive(UICamera.selectedObject)))
		{
			UICamera.currentScheme = UICamera.ControlScheme.Controller;
			UICamera.selectedObject = base.gameObject;
		}
	}

	// Token: 0x0600053E RID: 1342 RVA: 0x0001CA54 File Offset: 0x0001AC54
	protected virtual void OnDisable()
	{
		UIKeyNavigation.list.Remove(this);
	}

	// Token: 0x0600053F RID: 1343 RVA: 0x0001CA62 File Offset: 0x0001AC62
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

	// Token: 0x06000540 RID: 1344 RVA: 0x0001CA98 File Offset: 0x0001AC98
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

	// Token: 0x06000541 RID: 1345 RVA: 0x0001CACE File Offset: 0x0001ACCE
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

	// Token: 0x06000542 RID: 1346 RVA: 0x0001CB04 File Offset: 0x0001AD04
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

	// Token: 0x06000543 RID: 1347 RVA: 0x0001CB3C File Offset: 0x0001AD3C
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

	// Token: 0x06000544 RID: 1348 RVA: 0x0001CC34 File Offset: 0x0001AE34
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

	// Token: 0x06000545 RID: 1349 RVA: 0x0001CC84 File Offset: 0x0001AE84
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

	// Token: 0x06000546 RID: 1350 RVA: 0x0001CD89 File Offset: 0x0001AF89
	protected virtual void OnClick()
	{
		if (NGUITools.GetActive(this) && NGUITools.GetActive(this.onClick))
		{
			UICamera.selectedObject = this.onClick;
		}
	}

	// Token: 0x04000364 RID: 868
	public static BetterList<UIKeyNavigation> list = new BetterList<UIKeyNavigation>();

	// Token: 0x04000365 RID: 869
	public UIKeyNavigation.Constraint constraint;

	// Token: 0x04000366 RID: 870
	public GameObject onUp;

	// Token: 0x04000367 RID: 871
	public GameObject onDown;

	// Token: 0x04000368 RID: 872
	public GameObject onLeft;

	// Token: 0x04000369 RID: 873
	public GameObject onRight;

	// Token: 0x0400036A RID: 874
	public GameObject onClick;

	// Token: 0x0400036B RID: 875
	public bool startsSelected;

	// Token: 0x020011E7 RID: 4583
	public enum Constraint
	{
		// Token: 0x040063E9 RID: 25577
		None,
		// Token: 0x040063EA RID: 25578
		Vertical,
		// Token: 0x040063EB RID: 25579
		Horizontal,
		// Token: 0x040063EC RID: 25580
		Explicit
	}
}
