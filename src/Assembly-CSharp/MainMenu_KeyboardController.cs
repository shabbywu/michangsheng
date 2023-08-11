using UnityEngine;
using UnityEngine.EventSystems;

public class MainMenu_KeyboardController : MonoBehaviour
{
	public EventSystem eventSystem;

	public GameObject currentSelectedGameobject;

	private void Start()
	{
		currentSelectedGameobject = eventSystem.currentSelectedGameObject;
	}

	private void Update()
	{
		if ((Object)(object)eventSystem.currentSelectedGameObject != (Object)(object)currentSelectedGameobject)
		{
			if ((Object)(object)eventSystem.currentSelectedGameObject == (Object)null)
			{
				eventSystem.SetSelectedGameObject(currentSelectedGameobject);
			}
			else
			{
				currentSelectedGameobject = eventSystem.currentSelectedGameObject;
			}
		}
	}

	public void SetNextSelectedGameobject(GameObject NextGameObject)
	{
		if ((Object)(object)NextGameObject != (Object)null)
		{
			currentSelectedGameobject = NextGameObject;
			eventSystem.SetSelectedGameObject(currentSelectedGameobject);
		}
	}
}
