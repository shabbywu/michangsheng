using UnityEngine;

public class TabButton : MonoBehaviour
{
	public TabGroup Group;

	public bool IsOn;

	public bool IsFirst;

	public virtual void Awake()
	{
		Group.AddTab(this);
	}

	public virtual void OnToggle()
	{
		IsOn = true;
	}

	public virtual void OnLose()
	{
		IsOn = false;
	}

	public virtual void OnButtonClick()
	{
		Group.TryToggle(this);
	}
}
