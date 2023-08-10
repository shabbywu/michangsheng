using UnityEngine;

[AddComponentMenu("NGUI/Examples/Drag and Drop Item (Example)")]
public class ExampleDragDropItem : UIDragDropItem
{
	public GameObject prefab;

	protected override void OnDragDropRelease(GameObject surface)
	{
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		if ((Object)(object)surface != (Object)null)
		{
			ExampleDragDropSurface component = surface.GetComponent<ExampleDragDropSurface>();
			if ((Object)(object)component != (Object)null)
			{
				GameObject obj = NGUITools.AddChild(((Component)component).gameObject, prefab);
				obj.transform.localScale = ((Component)component).transform.localScale;
				Transform transform = obj.transform;
				transform.position = UICamera.lastWorldPosition;
				if (component.rotatePlacedObject)
				{
					transform.rotation = Quaternion.LookRotation(((RaycastHit)(ref UICamera.lastHit)).normal) * Quaternion.Euler(90f, 0f, 0f);
				}
				NGUITools.Destroy((Object)(object)((Component)this).gameObject);
				return;
			}
		}
		base.OnDragDropRelease(surface);
	}
}
