using System;
using System.Collections;
using UnityEngine;

// Token: 0x020006C4 RID: 1732
public class MainMenuManager : MonoBehaviour
{
	// Token: 0x06002B49 RID: 11081 RVA: 0x000042DD File Offset: 0x000024DD
	private void Start()
	{
	}

	// Token: 0x06002B4A RID: 11082 RVA: 0x001509EC File Offset: 0x0014EBEC
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

	// Token: 0x06002B4B RID: 11083 RVA: 0x0002156A File Offset: 0x0001F76A
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

	// Token: 0x06002B4C RID: 11084 RVA: 0x00150BD0 File Offset: 0x0014EDD0
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

	// Token: 0x04002586 RID: 9606
	private const int CAMPAIGN_MODE = 1;

	// Token: 0x04002587 RID: 9607
	private const int TIME_MODE = 2;

	// Token: 0x04002588 RID: 9608
	private const int MORE_APPS = 3;

	// Token: 0x04002589 RID: 9609
	private GameObject clickedOnObj;

	// Token: 0x0400258A RID: 9610
	public Mesh[] arrayOfButtonMeshes;

	// Token: 0x0400258B RID: 9611
	private bool soundOn = true;

	// Token: 0x0400258C RID: 9612
	private bool musicOn = true;
}
