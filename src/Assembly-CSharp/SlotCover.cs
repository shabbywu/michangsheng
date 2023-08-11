using UnityEngine;

public class SlotCover : MonoBehaviour
{
	private Inventory inv;

	private RectTransform rT;

	private void Start()
	{
		inv = ((Component)((Component)this).transform.parent.parent.parent.parent).GetComponent<Inventory>();
		rT = ((Component)this).GetComponent<RectTransform>();
	}

	private void Update()
	{
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		rT.sizeDelta = Vector2.op_Implicit(new Vector3((float)inv.slotSize, (float)inv.slotSize, 0f));
	}
}
