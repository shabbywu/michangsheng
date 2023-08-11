using UnityEngine;

public class VerticalScroll : MonoBehaviour
{
	public Transform upLimit;

	public Transform downLimit;

	private Transform items;

	private float upLimitY;

	private float downLimitY;

	private bool pomeraj;

	private float startY;

	private float endY;

	public bool canScroll = true;

	private string clickedItem;

	private string releasedItem;

	private float offsetY;

	private bool bounce;

	private bool moved;

	private bool released;

	private void Start()
	{
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		items = ((Component)this).transform.Find("Items");
		upLimitY = upLimit.position.y;
		downLimitY = downLimit.position.y;
	}

	private void Update()
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0189: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_025b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0210: Unknown result type (might be due to invalid IL or missing references)
		//IL_0220: Unknown result type (might be due to invalid IL or missing references)
		//IL_0240: Unknown result type (might be due to invalid IL or missing references)
		//IL_024a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0273: Unknown result type (might be due to invalid IL or missing references)
		if (!canScroll)
		{
			return;
		}
		if (Input.GetMouseButtonDown(0))
		{
			clickedItem = RaycastFunction(Input.mousePosition);
			startY = (endY = Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
			Debug.Log((object)("start y: " + startY));
		}
		else if (Input.GetMouseButton(0))
		{
			moved = true;
			endY = Camera.main.ScreenToWorldPoint(Input.mousePosition).y;
			offsetY = endY - startY;
			items.position = new Vector3(items.position.x, Mathf.MoveTowards(items.position.y, items.position.y + offsetY, 0.5f), items.position.z);
			startY = endY;
		}
		else if (Input.GetMouseButtonUp(0) && moved)
		{
			moved = false;
			released = true;
		}
		if (released)
		{
			items.Translate(0f, offsetY, 0f);
			offsetY *= 0.92f;
		}
		if (released && startY == endY)
		{
			if (items.position.y < downLimitY)
			{
				items.position = new Vector3(items.position.x, Mathf.MoveTowards(items.position.y, downLimitY, 1f), items.position.z);
			}
			else if (items.position.y > upLimitY)
			{
				items.position = new Vector3(items.position.x, Mathf.MoveTowards(items.position.y, upLimitY, 1f), items.position.z);
			}
			else if (items.position.y == upLimitY || items.position.y == downLimitY)
			{
				released = false;
			}
		}
	}

	private string RaycastFunction(Vector3 obj)
	{
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		RaycastHit val = default(RaycastHit);
		if (Physics.Raycast(Camera.main.ScreenPointToRay(obj), ref val))
		{
			return ((Object)((RaycastHit)(ref val)).collider).name;
		}
		return string.Empty;
	}
}
