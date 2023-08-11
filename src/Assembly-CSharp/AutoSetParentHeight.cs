using UnityEngine;

public class AutoSetParentHeight : MonoBehaviour
{
	[SerializeField]
	private RectTransform child;

	public float width;

	private RectTransform self;

	private void Start()
	{
		self = ((Component)this).GetComponent<RectTransform>();
	}

	private void Update()
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		self.sizeDelta = new Vector2(width, child.sizeDelta.y);
	}
}
