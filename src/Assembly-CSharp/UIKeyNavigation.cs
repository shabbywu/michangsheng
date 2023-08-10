using UnityEngine;

[AddComponentMenu("NGUI/Interaction/Key Navigation")]
public class UIKeyNavigation : MonoBehaviour
{
	public enum Constraint
	{
		None,
		Vertical,
		Horizontal,
		Explicit
	}

	public static BetterList<UIKeyNavigation> list = new BetterList<UIKeyNavigation>();

	public Constraint constraint;

	public GameObject onUp;

	public GameObject onDown;

	public GameObject onLeft;

	public GameObject onRight;

	public GameObject onClick;

	public bool startsSelected;

	protected virtual void OnEnable()
	{
		list.Add(this);
		if (startsSelected && ((Object)(object)UICamera.selectedObject == (Object)null || !NGUITools.GetActive(UICamera.selectedObject)))
		{
			UICamera.currentScheme = UICamera.ControlScheme.Controller;
			UICamera.selectedObject = ((Component)this).gameObject;
		}
	}

	protected virtual void OnDisable()
	{
		list.Remove(this);
	}

	protected GameObject GetLeft()
	{
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		if (NGUITools.GetActive(onLeft))
		{
			return onLeft;
		}
		if (constraint == Constraint.Vertical || constraint == Constraint.Explicit)
		{
			return null;
		}
		return Get(Vector3.left, horizontal: true);
	}

	private GameObject GetRight()
	{
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		if (NGUITools.GetActive(onRight))
		{
			return onRight;
		}
		if (constraint == Constraint.Vertical || constraint == Constraint.Explicit)
		{
			return null;
		}
		return Get(Vector3.right, horizontal: true);
	}

	protected GameObject GetUp()
	{
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		if (NGUITools.GetActive(onUp))
		{
			return onUp;
		}
		if (constraint == Constraint.Horizontal || constraint == Constraint.Explicit)
		{
			return null;
		}
		return Get(Vector3.up, horizontal: false);
	}

	protected GameObject GetDown()
	{
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		if (NGUITools.GetActive(onDown))
		{
			return onDown;
		}
		if (constraint == Constraint.Horizontal || constraint == Constraint.Explicit)
		{
			return null;
		}
		return Get(Vector3.down, horizontal: false);
	}

	protected GameObject Get(Vector3 myDir, bool horizontal)
	{
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		Transform transform = ((Component)this).transform;
		myDir = transform.TransformDirection(myDir);
		Vector3 center = GetCenter(((Component)this).gameObject);
		float num = float.MaxValue;
		GameObject result = null;
		for (int i = 0; i < list.size; i++)
		{
			UIKeyNavigation uIKeyNavigation = list[i];
			if ((Object)(object)uIKeyNavigation == (Object)(object)this)
			{
				continue;
			}
			UIButton component = ((Component)uIKeyNavigation).GetComponent<UIButton>();
			if ((Object)(object)component != (Object)null && !component.isEnabled)
			{
				continue;
			}
			Vector3 val = GetCenter(((Component)uIKeyNavigation).gameObject) - center;
			if (!(Vector3.Dot(myDir, ((Vector3)(ref val)).normalized) < 0.707f))
			{
				val = transform.InverseTransformDirection(val);
				if (horizontal)
				{
					val.y *= 2f;
				}
				else
				{
					val.x *= 2f;
				}
				float sqrMagnitude = ((Vector3)(ref val)).sqrMagnitude;
				if (!(sqrMagnitude > num))
				{
					result = ((Component)uIKeyNavigation).gameObject;
					num = sqrMagnitude;
				}
			}
		}
		return result;
	}

	protected static Vector3 GetCenter(GameObject go)
	{
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		UIWidget component = go.GetComponent<UIWidget>();
		if ((Object)(object)component != (Object)null)
		{
			Vector3[] worldCorners = component.worldCorners;
			return (worldCorners[0] + worldCorners[2]) * 0.5f;
		}
		return go.transform.position;
	}

	protected virtual void OnKey(KeyCode key)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Invalid comparison between Unknown and I4
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Expected I4, but got Unknown
		if (!NGUITools.GetActive((Behaviour)(object)this))
		{
			return;
		}
		GameObject val = null;
		if ((int)key != 9)
		{
			switch (key - 273)
			{
			case 3:
				val = GetLeft();
				break;
			case 2:
				val = GetRight();
				break;
			case 0:
				val = GetUp();
				break;
			case 1:
				val = GetDown();
				break;
			}
		}
		else if (Input.GetKey((KeyCode)304) || Input.GetKey((KeyCode)303))
		{
			val = GetLeft();
			if ((Object)(object)val == (Object)null)
			{
				val = GetUp();
			}
			if ((Object)(object)val == (Object)null)
			{
				val = GetDown();
			}
			if ((Object)(object)val == (Object)null)
			{
				val = GetRight();
			}
		}
		else
		{
			val = GetRight();
			if ((Object)(object)val == (Object)null)
			{
				val = GetDown();
			}
			if ((Object)(object)val == (Object)null)
			{
				val = GetUp();
			}
			if ((Object)(object)val == (Object)null)
			{
				val = GetLeft();
			}
		}
		if ((Object)(object)val != (Object)null)
		{
			UICamera.selectedObject = val;
		}
	}

	protected virtual void OnClick()
	{
		if (NGUITools.GetActive((Behaviour)(object)this) && NGUITools.GetActive(onClick))
		{
			UICamera.selectedObject = onClick;
		}
	}
}
