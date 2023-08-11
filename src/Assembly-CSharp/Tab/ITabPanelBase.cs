namespace Tab;

public abstract class ITabPanelBase : UIBase
{
	public bool HasHp;

	protected ITabPanelBase()
	{
		SingletonMono<TabUIMag>.Instance.PanelList.Add(this);
	}

	public abstract void Show();

	public virtual void Hide()
	{
		_go.SetActive(false);
	}
}
