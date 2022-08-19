using System;
using System.Collections;
using UnityEngine;

// Token: 0x020004BF RID: 1215
public class MainMenuManager : MonoBehaviour
{
	// Token: 0x06002665 RID: 9829 RVA: 0x00004095 File Offset: 0x00002295
	private void Start()
	{
	}

	// Token: 0x06002666 RID: 9830 RVA: 0x0010C5E8 File Offset: 0x0010A7E8
	private void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			this.clickedOnObj = this.GetClickedObject();
			if (this.clickedOnObj != null)
			{
				if (this.clickedOnObj.name.Equals("Main Campaign"))
				{
					this.clickedOnObj.GetComponent<Animation>().Play();
					base.StartCoroutine(this.WaitForAnimation(this.clickedOnObj.GetComponent<Animation>(), true, 3));
					return;
				}
				if (this.clickedOnObj.name.Equals("Main Endless"))
				{
					this.clickedOnObj.GetComponent<Animation>().Play();
					base.StartCoroutine(this.WaitForAnimation(this.clickedOnObj.GetComponent<Animation>(), true, 3));
					return;
				}
				if (this.clickedOnObj.name.Equals("Main More Apps"))
				{
					this.clickedOnObj.GetComponent<Animation>().Play();
					base.StartCoroutine(this.WaitForAnimation(this.clickedOnObj.GetComponent<Animation>(), true, 3));
					return;
				}
				if (this.clickedOnObj.name.Equals("ButtonSound"))
				{
					if (this.soundOn)
					{
						this.clickedOnObj.GetComponent<MeshFilter>().mesh = this.arrayOfButtonMeshes[1];
					}
					else
					{
						this.clickedOnObj.GetComponent<Animation>().Play();
						this.clickedOnObj.GetComponent<MeshFilter>().mesh = this.arrayOfButtonMeshes[0];
					}
					this.soundOn = !this.soundOn;
					return;
				}
				if (this.clickedOnObj.name.Equals("ButtonMusic"))
				{
					if (this.musicOn)
					{
						this.clickedOnObj.GetComponent<MeshFilter>().mesh = this.arrayOfButtonMeshes[3];
					}
					else
					{
						this.clickedOnObj.GetComponent<Animation>().Play();
						this.clickedOnObj.GetComponent<MeshFilter>().mesh = this.arrayOfButtonMeshes[2];
					}
					this.musicOn = !this.musicOn;
				}
			}
		}
	}

	// Token: 0x06002667 RID: 9831 RVA: 0x0010C7C9 File Offset: 0x0010A9C9
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
		yield break;
	}

	// Token: 0x06002668 RID: 9832 RVA: 0x0010C7E8 File Offset: 0x0010A9E8
	private GameObject GetClickedObject()
	{
		GameObject result = null;
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit raycastHit;
		if (Physics.Raycast(ray.origin, ray.direction * 10f, ref raycastHit))
		{
			result = raycastHit.collider.gameObject;
		}
		return result;
	}

	// Token: 0x04001FC8 RID: 8136
	private const int CAMPAIGN_MODE = 1;

	// Token: 0x04001FC9 RID: 8137
	private const int TIME_MODE = 2;

	// Token: 0x04001FCA RID: 8138
	private const int MORE_APPS = 3;

	// Token: 0x04001FCB RID: 8139
	private GameObject clickedOnObj;

	// Token: 0x04001FCC RID: 8140
	public Mesh[] arrayOfButtonMeshes;

	// Token: 0x04001FCD RID: 8141
	private bool soundOn = true;

	// Token: 0x04001FCE RID: 8142
	private bool musicOn = true;
}
