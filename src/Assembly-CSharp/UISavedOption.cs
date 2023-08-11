using UnityEngine;

[AddComponentMenu("NGUI/Interaction/Saved Option")]
public class UISavedOption : MonoBehaviour
{
	public string keyName;

	private UIPopupList mList;

	private UIToggle mCheck;

	private string key
	{
		get
		{
			if (!string.IsNullOrEmpty(keyName))
			{
				return keyName;
			}
			return "NGUI State: " + ((Object)this).name;
		}
	}

	private void Awake()
	{
		mList = ((Component)this).GetComponent<UIPopupList>();
		mCheck = ((Component)this).GetComponent<UIToggle>();
	}

	private void OnEnable()
	{
		if ((Object)(object)mList != (Object)null)
		{
			EventDelegate.Add(mList.onChange, SaveSelection);
		}
		if ((Object)(object)mCheck != (Object)null)
		{
			EventDelegate.Add(mCheck.onChange, SaveState);
		}
		if ((Object)(object)mList != (Object)null)
		{
			string @string = PlayerPrefs.GetString(key);
			if (!string.IsNullOrEmpty(@string))
			{
				mList.value = @string;
			}
			return;
		}
		if ((Object)(object)mCheck != (Object)null)
		{
			mCheck.value = PlayerPrefs.GetInt(key, 1) != 0;
			return;
		}
		string string2 = PlayerPrefs.GetString(key);
		UIToggle[] componentsInChildren = ((Component)this).GetComponentsInChildren<UIToggle>(true);
		int i = 0;
		for (int num = componentsInChildren.Length; i < num; i++)
		{
			UIToggle obj = componentsInChildren[i];
			obj.value = ((Object)obj).name == string2;
		}
	}

	private void OnDisable()
	{
		if ((Object)(object)mCheck != (Object)null)
		{
			EventDelegate.Remove(mCheck.onChange, SaveState);
		}
		if ((Object)(object)mList != (Object)null)
		{
			EventDelegate.Remove(mList.onChange, SaveSelection);
		}
		if (!((Object)(object)mCheck == (Object)null) || !((Object)(object)mList == (Object)null))
		{
			return;
		}
		UIToggle[] componentsInChildren = ((Component)this).GetComponentsInChildren<UIToggle>(true);
		int i = 0;
		for (int num = componentsInChildren.Length; i < num; i++)
		{
			UIToggle uIToggle = componentsInChildren[i];
			if (uIToggle.value)
			{
				PlayerPrefs.SetString(key, ((Object)uIToggle).name);
				break;
			}
		}
	}

	public void SaveSelection()
	{
		PlayerPrefs.SetString(key, UIPopupList.current.value);
	}

	public void SaveState()
	{
		PlayerPrefs.SetInt(key, UIToggle.current.value ? 1 : 0);
	}
}
