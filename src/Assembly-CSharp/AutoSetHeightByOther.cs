using UnityEngine;

public class AutoSetHeightByOther : MonoBehaviour
{
	[Header("高度参考")]
	public RectTransform Other;

	[Header("宽度")]
	public float Width;

	[Header("额外高度")]
	public float ExHeight;

	private RectTransform self;

	private void Start()
	{
		self = ((Component)this).GetComponent<RectTransform>();
	}

	private void Update()
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		self.sizeDelta = new Vector2(Width, Other.sizeDelta.y + ExHeight);
	}
}
