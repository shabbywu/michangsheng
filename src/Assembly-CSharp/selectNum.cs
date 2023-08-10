using UnityEngine;

public class selectNum : MonoBehaviour
{
	public UIButton cancel;

	public UIButton ok;

	public UILabel label;

	private static selectNum _instence;

	public static selectNum instence => _instence;

	private void Awake()
	{
		_instence = this;
	}

	public void setChoice(EventDelegate OK, EventDelegate Cancel, string text = "选择数量")
	{
		open();
		label.text = text;
		cancel.onClick.Clear();
		ok.onClick.Clear();
		cancel.onClick.Add(Cancel);
		ok.onClick.Add(OK);
		cancel.onClick.Add(new EventDelegate(close));
		ok.onClick.Add(new EventDelegate(close));
	}

	public void close()
	{
		((Component)this).gameObject.SetActive(false);
	}

	public void open()
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		((Component)this).gameObject.SetActive(true);
		((Component)this).transform.localPosition = Vector3.up * -2000f;
		((Component)this).transform.localScale = Vector3.one;
	}

	private void Start()
	{
	}
}
