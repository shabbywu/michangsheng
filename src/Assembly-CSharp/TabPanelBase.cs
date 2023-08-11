using UnityEngine;

public class TabPanelBase : MonoBehaviour
{
	public virtual void OnPanelShow()
	{
		((Component)this).gameObject.SetActive(true);
	}

	public virtual void OnPanelHide()
	{
		((Component)this).gameObject.SetActive(false);
	}
}
