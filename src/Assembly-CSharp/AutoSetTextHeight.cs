using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class AutoSetTextHeight : MonoBehaviour
{
	[Header("宽度")]
	public float Width;

	[Header("额外高度")]
	public float ExHeight;

	private RectTransform self;

	private Text myText;

	private void Start()
	{
		self = ((Component)this).GetComponent<RectTransform>();
		myText = ((Component)this).GetComponent<Text>();
	}

	private void Update()
	{
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		self.sizeDelta = new Vector2(Width, myText.preferredHeight + ExHeight);
	}
}
