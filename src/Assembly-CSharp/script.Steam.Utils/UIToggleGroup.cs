using System.Collections.Generic;

namespace script.Steam.Utils;

public class UIToggleGroup
{
	private List<UIToggleA> toggles = new List<UIToggleA>();

	public void AddToggle(UIToggleA toggle)
	{
		toggles.Add(toggle);
	}

	public void RemoveToggle(UIToggleA toggle)
	{
		toggles.Remove(toggle);
	}

	public void Select(UIToggleA toggle)
	{
		foreach (UIToggleA toggle2 in toggles)
		{
			if (toggle2 != toggle)
			{
				toggle2.CanCelSelect();
			}
		}
		toggle.Select();
	}

	public void SelectDefault()
	{
		Select(toggles[0]);
	}

	public void SetAllCanClick(bool flag)
	{
		foreach (UIToggleA toggle in toggles)
		{
			toggle.SetCanClick(flag);
		}
	}
}
