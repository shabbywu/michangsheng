using UnityEngine;
using UnityEngine.UI;

public class LunDaoSayWord : MonoBehaviour
{
	[SerializeField]
	private RectTransform bg;

	private RectTransform contentRectTransform;

	[SerializeField]
	private Text content;

	public void Say(string msg)
	{
		content.text = msg;
		((MonoBehaviour)this).Invoke("Show", 0.1f);
	}

	private void Show()
	{
		((Component)this).gameObject.SetActive(true);
		((MonoBehaviour)this).Invoke("Hide", 2f);
	}

	public void Hide()
	{
		((Component)this).gameObject.SetActive(false);
	}

	private void Awake()
	{
		contentRectTransform = ((Component)content).gameObject.GetComponent<RectTransform>();
	}

	private void Update()
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		bg.sizeDelta = new Vector2(334f, contentRectTransform.sizeDelta.y + 20f);
	}
}
