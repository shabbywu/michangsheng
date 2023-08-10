using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class UIPosFollow : MonoBehaviour
{
	public RectTransform Target;

	public bool FollowActive;

	private RectTransform Me;

	private GameObject Obj;

	private void Awake()
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Expected O, but got Unknown
		Me = (RectTransform)((Component)this).transform;
		Obj = ((Component)((Component)this).transform.GetChild(0)).gameObject;
	}

	private void LateUpdate()
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		if ((Object)(object)Target != (Object)null)
		{
			Me.anchoredPosition = Target.anchoredPosition;
			if (FollowActive && Obj.activeInHierarchy != ((Component)Target).gameObject.activeInHierarchy)
			{
				Obj.SetActive(((Component)Target).gameObject.activeInHierarchy);
			}
		}
	}
}
