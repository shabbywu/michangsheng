using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class moveCamer : Selectable, IBeginDragHandler, IEventSystemHandler, IDragHandler, IEndDragHandler
{
	private SmoothFollow sf;

	private Image itemimage;

	private int touchfingerid = -1;

	private Rect imageRect;

	private void Start()
	{
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		sf = ((Component)Camera.main).GetComponent<SmoothFollow>();
		itemimage = ((Component)this).gameObject.GetComponent<Image>();
		Vector3 position = ((Transform)((Graphic)itemimage).rectTransform).position;
		Rect rect = ((Graphic)itemimage).rectTransform.rect;
		int num = Screen.width / 711;
		int num2 = Screen.height / 400;
		Vector2 size = ((Rect)(ref rect)).size;
		float num3 = size.x * (float)num;
		float num4 = size.y * (float)num2;
		imageRect = new Rect(new Vector2(position.x - num3 / 2f, position.y - num4 / 2f), new Vector2(num3, num4));
	}

	private void Update()
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Invalid comparison between Unknown and I4
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Invalid comparison between Unknown and I4
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		if (Input.touchCount != 1)
		{
			return;
		}
		Touch[] touches = Input.touches;
		for (int i = 0; i < touches.Length; i++)
		{
			Touch val = touches[i];
			if ((int)((Touch)(ref val)).phase == 0)
			{
				Vector2 position = ((Touch)(ref val)).position;
				if (((Rect)(ref imageRect)).Contains(position) && touchfingerid == -1)
				{
					touchfingerid = ((Touch)(ref val)).fingerId;
				}
			}
			if ((int)((Touch)(ref val)).phase == 1 && touchfingerid != -1 && ((Touch)(ref val)).fingerId == touchfingerid)
			{
				float x = ((Touch)(ref val)).deltaPosition.x;
				sf.rotateCamerX(x);
			}
			if ((int)((Touch)(ref val)).phase == 3 && ((Touch)(ref val)).fingerId == touchfingerid)
			{
				touchfingerid = -1;
			}
		}
	}

	public void OnBeginDrag(PointerEventData data)
	{
	}

	public void OnDrag(PointerEventData data)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		if (data.pressPosition.x > 0f)
		{
			((Component)sf).transform.position = new Vector3(((Component)sf).transform.position.x + data.pressPosition.x, ((Component)sf).transform.position.y, ((Component)sf).transform.position.x);
		}
		if (data.pressPosition.y > 0f)
		{
			((Component)sf).transform.position = new Vector3(((Component)sf).transform.position.x, ((Component)sf).transform.position.y + data.pressPosition.y, ((Component)sf).transform.position.x);
		}
		sf.FollowUpdate();
	}

	public void OnEndDrag(PointerEventData data)
	{
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		((Component)this).transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		((Component)this).transform.localScale = new Vector3(1f, 1f, 1f);
	}
}
