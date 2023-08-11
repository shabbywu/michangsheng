using UnityEngine;

namespace Tab;

public abstract class ISysPanelBase : UIBase
{
	protected ISysPanelBase()
	{
		if ((Object)(object)SingletonMono<TabUIMag>.Instance != (Object)null)
		{
			SingletonMono<TabUIMag>.Instance.SystemPanel.PanelList.Add(this);
		}
	}

	public abstract void Show();

	public virtual void Hide()
	{
		_go.SetActive(false);
	}
}
