using System;
using System.Collections;
using KBEngine;
using UnityEngine;

// Token: 0x020004CF RID: 1231
public class IslandHolderManager : MonoBehaviour
{
	// Token: 0x060027BA RID: 10170 RVA: 0x00128ECE File Offset: 0x001270CE
	private void Awake()
	{
		this.Kamera = GameObject.Find("Main Camera");
	}

	// Token: 0x060027BB RID: 10171 RVA: 0x00004095 File Offset: 0x00002295
	private void Start()
	{
	}

	// Token: 0x060027BC RID: 10172 RVA: 0x00128EE0 File Offset: 0x001270E0
	public void creatAvatar(int avaterID, int roleType, int HP_Max, Vector3 position, Vector3 direction)
	{
		KBEngineApp.app.Client_onCreatedProxies((ulong)((long)avaterID), avaterID, "Avatar");
		Avatar avatar = (Avatar)KBEngineApp.app.entities[avaterID];
		avatar.roleTypeCell = (uint)roleType;
		avatar.position = position;
		avatar.direction = direction;
		avatar.HP_Max = HP_Max;
		avatar.HP = HP_Max;
	}

	// Token: 0x060027BD RID: 10173 RVA: 0x00128F38 File Offset: 0x00127138
	private void Update()
	{
		if (Input.GetKeyUp(27))
		{
			Application.LoadLevel("All Maps");
		}
		Input.GetKeyUp(9);
		this.endX = Input.mousePosition.x;
		this.endY = Input.mousePosition.y;
		if (Input.GetMouseButtonDown(0))
		{
			this.startX = Input.mousePosition.x;
			this.startY = Input.mousePosition.y;
			string a = this.RaycastFunction(Input.mousePosition);
			for (int i = 0; i < 100; i++)
			{
				if (a == string.Concat(i))
				{
					this.KliknutoNa = i;
					break;
				}
			}
			if (this.RaycastFunction(Input.mousePosition) == "HouseShop")
			{
				this.KliknutoNa = 21;
			}
			else if (this.RaycastFunction(Input.mousePosition) == "HolderShipFreeCoins")
			{
				this.KliknutoNa = 22;
			}
			else if (this.RaycastFunction(Input.mousePosition) == "ButtonBack")
			{
				this.KliknutoNa = 23;
			}
		}
		if (Input.GetMouseButtonUp(0))
		{
			this.endX = Input.mousePosition.x;
			this.endY = Input.mousePosition.y;
			this.forceX = -(this.endX - this.startX) / (90f * base.GetComponent<Camera>().aspect) * 5f;
			this.forceY = -(this.endY - this.startY) / (90f * base.GetComponent<Camera>().aspect);
			string a2 = this.RaycastFunction(Input.mousePosition);
			int j = 0;
			while (j < 100)
			{
				if (a2 == string.Concat(j))
				{
					if (this.KliknutoNa != this.Klik[j])
					{
						this.PomeriKameru();
						break;
					}
					Debug.Log("Level " + j);
					if (!UINPCJiaoHu.Inst.NowIsJiaoHu)
					{
						this.RaycastObj(Input.mousePosition).collider.GetComponent<MapComponent>().EventRandom();
						break;
					}
					break;
				}
				else
				{
					j++;
				}
			}
			if (this.RaycastFunction(Input.mousePosition) == "HouseShop")
			{
				if (this.KliknutoNa == this.Klik[21])
				{
					Debug.Log("HouseShop");
					return;
				}
				this.PomeriKameru();
				return;
			}
			else if (this.RaycastFunction(Input.mousePosition) == "HolderShipFreeCoins")
			{
				if (this.KliknutoNa == this.Klik[22])
				{
					Debug.Log("HolderShipFreeCoins");
					return;
				}
				this.PomeriKameru();
				return;
			}
			else if (this.RaycastFunction(Input.mousePosition) == "ButtonBack")
			{
				if (this.KliknutoNa == this.Klik[23])
				{
					Debug.Log("ButtonBack");
					Debug.Log("Objasni mi ovo");
					base.StartCoroutine(this.UcitajOstrvo("All Maps"));
					return;
				}
				this.PomeriKameru();
			}
		}
	}

	// Token: 0x060027BE RID: 10174 RVA: 0x0012920C File Offset: 0x0012740C
	private void PomeriKameru()
	{
		this.TrenutniX = this.Kamera.transform.position.x;
		this.TrenutniY = this.Kamera.transform.position.y;
		if (this.TrenutniX <= -11.87f && this.forceX < 0f)
		{
			Debug.Log("Leva granica");
			return;
		}
		if (this.TrenutniX >= 11f && this.forceX > 0f)
		{
			Debug.Log("Desna  granica");
			return;
		}
		if (this.TrenutniX + this.forceX >= -11.87f && this.TrenutniX + this.forceX <= 11f)
		{
			if (this.TrenutniX + this.forceX >= -11.87f)
			{
				Debug.Log("Desno");
				this.Kamera.transform.Translate(this.forceX, this.forceY, 0f);
				return;
			}
			if (this.TrenutniX + this.forceX <= 11f)
			{
				Debug.Log("Levo");
				this.Kamera.transform.Translate(this.forceX, this.forceY, 0f);
				return;
			}
			Debug.Log("Novo");
			return;
		}
		else
		{
			if (this.TrenutniX + this.forceX < -11.87f)
			{
				this.Kamera.GetComponent<Transform>().position = new Vector3(-11.87f, this.Kamera.transform.position.y, -10f);
				return;
			}
			if (this.TrenutniX + this.forceX > 11f)
			{
				this.Kamera.GetComponent<Transform>().position = new Vector3(11f, this.Kamera.transform.position.y, -10f);
			}
			return;
		}
	}

	// Token: 0x060027BF RID: 10175 RVA: 0x001293E4 File Offset: 0x001275E4
	private string RaycastFunction(Vector3 vector)
	{
		RaycastHit raycastHit;
		if (Physics.Raycast(Camera.main.ScreenPointToRay(vector), ref raycastHit))
		{
			return raycastHit.collider.name;
		}
		return "";
	}

	// Token: 0x060027C0 RID: 10176 RVA: 0x00129418 File Offset: 0x00127618
	private RaycastHit RaycastObj(Vector3 vector)
	{
		RaycastHit result;
		Physics.Raycast(Camera.main.ScreenPointToRay(vector), ref result);
		return result;
	}

	// Token: 0x060027C1 RID: 10177 RVA: 0x00129439 File Offset: 0x00127639
	private IEnumerator UcitajOstrvo(string ime)
	{
		GameObject.Find("OblaciPomeranje").GetComponent<Animation>().Play("OblaciPostavljanje");
		yield return new WaitForSeconds(0.85f);
		Application.LoadLevel(ime);
		yield break;
	}

	// Token: 0x0400228D RID: 8845
	private GameObject Kamera;

	// Token: 0x0400228E RID: 8846
	private int[] Klik = new int[]
	{
		0,
		1,
		2,
		3,
		4,
		5,
		6,
		7,
		8,
		9,
		10,
		11,
		12,
		13,
		14,
		15,
		16,
		17,
		18,
		19,
		20,
		21,
		22,
		23
	};

	// Token: 0x0400228F RID: 8847
	private int KliknutoNa;

	// Token: 0x04002290 RID: 8848
	private float TrenutniX;

	// Token: 0x04002291 RID: 8849
	private float TrenutniY;

	// Token: 0x04002292 RID: 8850
	private float forceX;

	// Token: 0x04002293 RID: 8851
	private float forceY;

	// Token: 0x04002294 RID: 8852
	private float startX;

	// Token: 0x04002295 RID: 8853
	private float endX;

	// Token: 0x04002296 RID: 8854
	private float startY;

	// Token: 0x04002297 RID: 8855
	private float endY;
}
