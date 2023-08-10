using System.Collections;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
	private const int CAMPAIGN_MODE = 1;

	private const int TIME_MODE = 2;

	private const int MORE_APPS = 3;

	private GameObject clickedOnObj;

	public Mesh[] arrayOfButtonMeshes;

	private bool soundOn = true;

	private bool musicOn = true;

	private void Start()
	{
	}

	private void Update()
	{
		if (!Input.GetMouseButtonDown(0))
		{
			return;
		}
		clickedOnObj = GetClickedObject();
		if (!((Object)(object)clickedOnObj != (Object)null))
		{
			return;
		}
		if (((Object)clickedOnObj).name.Equals("Main Campaign"))
		{
			clickedOnObj.GetComponent<Animation>().Play();
			((MonoBehaviour)this).StartCoroutine(WaitForAnimation(clickedOnObj.GetComponent<Animation>(), loadAnotherScene: true, 3));
		}
		else if (((Object)clickedOnObj).name.Equals("Main Endless"))
		{
			clickedOnObj.GetComponent<Animation>().Play();
			((MonoBehaviour)this).StartCoroutine(WaitForAnimation(clickedOnObj.GetComponent<Animation>(), loadAnotherScene: true, 3));
		}
		else if (((Object)clickedOnObj).name.Equals("Main More Apps"))
		{
			clickedOnObj.GetComponent<Animation>().Play();
			((MonoBehaviour)this).StartCoroutine(WaitForAnimation(clickedOnObj.GetComponent<Animation>(), loadAnotherScene: true, 3));
		}
		else if (((Object)clickedOnObj).name.Equals("ButtonSound"))
		{
			if (soundOn)
			{
				clickedOnObj.GetComponent<MeshFilter>().mesh = arrayOfButtonMeshes[1];
			}
			else
			{
				clickedOnObj.GetComponent<Animation>().Play();
				clickedOnObj.GetComponent<MeshFilter>().mesh = arrayOfButtonMeshes[0];
			}
			soundOn = !soundOn;
		}
		else if (((Object)clickedOnObj).name.Equals("ButtonMusic"))
		{
			if (musicOn)
			{
				clickedOnObj.GetComponent<MeshFilter>().mesh = arrayOfButtonMeshes[3];
			}
			else
			{
				clickedOnObj.GetComponent<Animation>().Play();
				clickedOnObj.GetComponent<MeshFilter>().mesh = arrayOfButtonMeshes[2];
			}
			musicOn = !musicOn;
		}
	}

	private IEnumerator WaitForAnimation(Animation animation, bool loadAnotherScene, int indexOfSceneToLoad)
	{
		do
		{
			yield return null;
		}
		while (animation.isPlaying);
		if (loadAnotherScene)
		{
			Application.LoadLevel(indexOfSceneToLoad);
		}
	}

	private GameObject GetClickedObject()
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		GameObject result = null;
		Ray val = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit val2 = default(RaycastHit);
		if (Physics.Raycast(((Ray)(ref val)).origin, ((Ray)(ref val)).direction * 10f, ref val2))
		{
			result = ((Component)((RaycastHit)(ref val2)).collider).gameObject;
		}
		return result;
	}
}
