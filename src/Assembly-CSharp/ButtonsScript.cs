using System;
using UnityEngine;

// Token: 0x02000670 RID: 1648
public class ButtonsScript : MonoBehaviour
{
	// Token: 0x0600292D RID: 10541 RVA: 0x00141608 File Offset: 0x0013F808
	private void AnimateButton(GameObject btn, int Onoff)
	{
		if (btn.name != "PlayerName")
		{
			if (Onoff == 1)
			{
				this.scaleSave = btn.transform.localScale;
				btn.transform.localScale = this.scaleSave * 0.8f;
				this.save = new Material(btn.GetComponent<Renderer>().sharedMaterial);
				Material material = new Material(btn.GetComponent<Renderer>().sharedMaterial);
				material.color = new Color(this.save.color.r - 0.2f, this.save.color.g - 0.2f, this.save.color.b - 0.2f, this.save.color.a);
				btn.GetComponent<Renderer>().sharedMaterial = material;
				return;
			}
			btn.transform.localScale = this.scaleSave;
			btn.GetComponent<Renderer>().sharedMaterial = this.save;
		}
	}

	// Token: 0x0600292E RID: 10542 RVA: 0x00141710 File Offset: 0x0013F910
	private string RaycastFunct(Vector3 v)
	{
		RaycastHit raycastHit;
		if (Physics.Raycast(GameObject.Find("Main Camera").GetComponent<Camera>().ScreenPointToRay(v + Vector3.forward * 10f), ref raycastHit, 500f))
		{
			return raycastHit.transform.name;
		}
		return "";
	}

	// Token: 0x0600292F RID: 10543 RVA: 0x000042DD File Offset: 0x000024DD
	private void Start()
	{
	}

	// Token: 0x06002930 RID: 10544 RVA: 0x00141768 File Offset: 0x0013F968
	private void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			this.clickedOn = this.RaycastFunct(Input.mousePosition);
			if (this.clickedOn != "")
			{
				this.AnimateButton(GameObject.Find(this.clickedOn), 1);
			}
		}
		if (Input.GetMouseButtonUp(0))
		{
			if (this.clickedOn != "")
			{
				this.AnimateButton(GameObject.Find(this.clickedOn), 2);
			}
			string a = this.RaycastFunct(Input.mousePosition);
			if (a == this.clickedOn && a != "")
			{
				Debug.Log("Boza je kralj!!!!");
			}
		}
	}

	// Token: 0x040022F0 RID: 8944
	private string clickedOn = "";

	// Token: 0x040022F1 RID: 8945
	private Material save;

	// Token: 0x040022F2 RID: 8946
	private Vector3 scaleSave;

	// Token: 0x040022F3 RID: 8947
	public GameObject test;
}
