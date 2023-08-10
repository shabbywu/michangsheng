using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MyDropdown : MonoBehaviour
{
	[SerializeField]
	private GameObject content;

	[SerializeField]
	private Button startBtn;

	[SerializeField]
	private Button bigBtn;

	private void Start()
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Expected O, but got Unknown
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Expected O, but got Unknown
		((UnityEvent)startBtn.onClick).AddListener(new UnityAction(Open));
		((UnityEvent)bigBtn.onClick).AddListener(new UnityAction(Close));
	}

	private void Open()
	{
		content.SetActive(true);
		((Component)startBtn).gameObject.SetActive(false);
		((Component)bigBtn).gameObject.SetActive(true);
	}

	public void Close()
	{
		((Component)startBtn).gameObject.SetActive(true);
		content.SetActive(false);
		((Component)bigBtn).gameObject.SetActive(false);
	}
}
