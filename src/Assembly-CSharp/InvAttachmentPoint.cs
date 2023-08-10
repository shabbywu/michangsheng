using UnityEngine;

[AddComponentMenu("NGUI/Examples/Item Attachment Point")]
public class InvAttachmentPoint : MonoBehaviour
{
	public InvBaseItem.Slot slot;

	private GameObject mPrefab;

	private GameObject mChild;

	public GameObject Attach(GameObject prefab)
	{
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		if ((Object)(object)mPrefab != (Object)(object)prefab)
		{
			mPrefab = prefab;
			if ((Object)(object)mChild != (Object)null)
			{
				Object.Destroy((Object)(object)mChild);
			}
			if ((Object)(object)mPrefab != (Object)null)
			{
				Transform transform = ((Component)this).transform;
				mChild = Object.Instantiate<GameObject>(mPrefab, transform.position, transform.rotation);
				Transform transform2 = mChild.transform;
				transform2.parent = transform;
				transform2.localPosition = Vector3.zero;
				transform2.localRotation = Quaternion.identity;
				transform2.localScale = Vector3.one;
			}
		}
		return mChild;
	}
}
