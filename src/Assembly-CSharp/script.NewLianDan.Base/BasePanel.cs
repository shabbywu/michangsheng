namespace script.NewLianDan.Base;

public class BasePanel : UIBase
{
	public virtual void Show()
	{
		_go.SetActive(true);
	}

	public virtual void Hide()
	{
		_go.SetActive(false);
	}

	public bool IsActive()
	{
		return _go.activeSelf;
	}
}
