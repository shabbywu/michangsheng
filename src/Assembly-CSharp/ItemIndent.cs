using UnityEngine;

public class ItemIndent : MonoBehaviour
{
	private VerticalScroll parentScript;

	private Transform myTransform;

	private float centerOfScreen;

	private float startPosX;

	private bool regulate;

	private void Start()
	{
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		parentScript = ((Component)((Component)this).transform.parent.parent).GetComponent<VerticalScroll>();
		myTransform = ((Component)this).transform;
		centerOfScreen = Camera.main.ViewportToWorldPoint(Vector3.one / 2f).y;
		startPosX = myTransform.position.x;
		Debug.Log((object)("CenterOfScreen:" + centerOfScreen));
	}

	private void Update()
	{
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		if (parentScript.canScroll)
		{
			if (myTransform.position.y >= centerOfScreen - 2.5f && myTransform.position.y <= centerOfScreen + 2.5f)
			{
				myTransform.position = new Vector3(Mathf.Clamp(Mathf.MoveTowards(myTransform.position.x, startPosX + 0.5f - myTransform.position.y, 1f), startPosX, startPosX + 0.5f), myTransform.position.y, myTransform.position.z);
			}
			else
			{
				myTransform.position = new Vector3(Mathf.Clamp(Mathf.MoveTowards(myTransform.position.x, startPosX, 1f), startPosX, startPosX + 0.5f), myTransform.position.y, myTransform.position.z);
			}
		}
	}
}
