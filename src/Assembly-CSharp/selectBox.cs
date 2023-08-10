using UnityEngine;

public class selectBox : MonoBehaviour, IESCClose
{
	public UIButton cancel;

	public UIButton ok;

	public UILabel label;

	public UIButton lianQiOK;

	public static selectBox _instence;

	public static selectBox instence => _instence;

	private void Awake()
	{
		_instence = this;
	}

	public void setChoice(string text, EventDelegate OK, EventDelegate Cancel)
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

	public void LianDanChoice(string text, EventDelegate OK, EventDelegate Cancel, Vector3 scale)
	{
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		open();
		label.text = text;
		cancel.onClick.Clear();
		ok.onClick.Clear();
		cancel.onClick.Add(Cancel);
		ok.onClick.Add(OK);
		cancel.onClick.Add(new EventDelegate(close));
		ok.onClick.Add(new EventDelegate(close));
		((Component)this).transform.localScale = scale;
	}

	public void LianQiChoice(string text, EventDelegate OK, EventDelegate Cancel, Vector3 scale)
	{
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		open();
		label.text = text;
		cancel.onClick.Clear();
		ok.onClick.Clear();
		cancel.onClick.Add(Cancel);
		ok.onClick.Add(new EventDelegate(close));
		ok.onClick.Add(OK);
		cancel.onClick.Add(new EventDelegate(close));
		((Component)this).transform.localScale = scale;
	}

	public void LianQiResult(string text, EventDelegate OK, EventDelegate Cancel, Vector3 scale)
	{
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		open();
		label.text = text;
		lianQiOK.onClick.Clear();
		lianQiOK.onClick.Add(OK);
		lianQiOK.onClick.Add(new EventDelegate(close));
		cancel.onClick.Clear();
		((Component)ok).gameObject.SetActive(false);
		((Component)cancel).gameObject.SetActive(false);
		((Component)lianQiOK).gameObject.SetActive(true);
		((Component)this).transform.localScale = scale;
	}

	public void setBtnBackSprite(string canceName, string OkName)
	{
		((Component)cancel).GetComponent<UISprite>().spriteName = canceName;
		((Component)ok).GetComponent<UISprite>().spriteName = OkName;
		cancel.normalSprite = canceName;
		ok.normalSprite = OkName;
	}

	public void close()
	{
		Tools.canClickFlag = true;
		((Component)this).gameObject.SetActive(false);
		ESCCloseManager.Inst.UnRegisterClose(this);
	}

	public void open()
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		Tools.canClickFlag = false;
		((Component)this).gameObject.SetActive(true);
		((Component)this).transform.localPosition = Vector3.up * -2000f;
		((Component)this).transform.localScale = Vector3.one;
		((Component)cancel).gameObject.SetActive(true);
		((Component)ok).gameObject.SetActive(true);
		if ((Object)(object)lianQiOK != (Object)null)
		{
			((Component)lianQiOK).gameObject.SetActive(false);
		}
		ESCCloseManager.Inst.RegisterClose(this);
	}

	private void Start()
	{
	}

	public bool TryEscClose()
	{
		if (((Component)cancel).gameObject.activeInHierarchy)
		{
			int count = cancel.onClick.Count;
			for (int i = 0; i < count; i++)
			{
				if (cancel.onClick[i] != null)
				{
					cancel.onClick[i].Execute();
				}
			}
		}
		return true;
	}
}
