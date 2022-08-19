using System;
using UnityEngine;

// Token: 0x02000496 RID: 1174
public class ButtonsScript : MonoBehaviour
{
	// Token: 0x06002511 RID: 9489 RVA: 0x001019D8 File Offset: 0x000FFBD8
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

	// Token: 0x06002512 RID: 9490 RVA: 0x00101AE0 File Offset: 0x000FFCE0
	private string RaycastFunct(Vector3 v)
	{
		RaycastHit raycastHit;
		if (Physics.Raycast(GameObject.Find("Main Camera").GetComponent<Camera>().ScreenPointToRay(v + Vector3.forward * 10f), ref raycastHit, 500f))
		{
			return raycastHit.transform.name;
		}
		return "";
	}

	// Token: 0x06002513 RID: 9491 RVA: 0x00004095 File Offset: 0x00002295
	private void Start()
	{
	}

	// Token: 0x06002514 RID: 9492 RVA: 0x00101B38 File Offset: 0x000FFD38
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

	// Token: 0x04001DDE RID: 7646
	private string clickedOn = "";

	// Token: 0x04001DDF RID: 7647
	private Material save;

	// Token: 0x04001DE0 RID: 7648
	private Vector3 scaleSave;

	// Token: 0x04001DE1 RID: 7649
	public GameObject test;
}
