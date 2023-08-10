using UnityEngine;

public class changAnButton : MonoBehaviour
{
	private bool flagSwitch;

	private float lastTime;

	private float AllTime;

	private void Start()
	{
	}

	private void Update()
	{
		if (flagSwitch)
		{
			lastTime += Time.deltaTime;
			AllTime += Time.deltaTime;
			if (AllTime > 0.5f && lastTime > 0.1f)
			{
				EventDelegate.Execute(((Component)this).GetComponent<UIButton>().onClick);
				lastTime = 0f;
			}
		}
		if ((Object)(object)UICamera.GetMouse(0).current != (Object)(object)((Component)this).gameObject)
		{
			flagSwitch = false;
		}
	}

	protected void OnPress()
	{
		if (Input.GetMouseButtonDown(0))
		{
			flagSwitch = true;
			lastTime = 0f;
			AllTime = 0f;
		}
		else if (Input.GetMouseButtonUp(0))
		{
			flagSwitch = false;
		}
	}
}
