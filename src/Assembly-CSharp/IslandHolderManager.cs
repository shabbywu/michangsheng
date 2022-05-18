using System;
using System.Collections;
using KBEngine;
using UnityEngine;

// Token: 0x02000735 RID: 1845
public class IslandHolderManager : MonoBehaviour
{
	// Token: 0x06002EC8 RID: 11976 RVA: 0x00022B6E File Offset: 0x00020D6E
	private void Awake()
	{
		this.Kamera = GameObject.Find("Main Camera");
	}

	// Token: 0x06002EC9 RID: 11977 RVA: 0x000042DD File Offset: 0x000024DD
	private void Start()
	{
	}

	// Token: 0x06002ECA RID: 11978 RVA: 0x00174840 File Offset: 0x00172A40
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

	// Token: 0x06002ECB RID: 11979 RVA: 0x00174898 File Offset: 0x00172A98
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

	// Token: 0x06002ECC RID: 11980 RVA: 0x00174B6C File Offset: 0x00172D6C
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

	// Token: 0x06002ECD RID: 11981 RVA: 0x00149A14 File Offset: 0x00147C14
	private string RaycastFunction(Vector3 vector)
	{
		RaycastHit raycastHit;
		if (Physics.Raycast(Camera.main.ScreenPointToRay(vector), ref raycastHit))
		{
			return raycastHit.collider.name;
		}
		return "";
	}

	// Token: 0x06002ECE RID: 11982 RVA: 0x00174D44 File Offset: 0x00172F44
	private RaycastHit RaycastObj(Vector3 vector)
	{
		RaycastHit result;
		Physics.Raycast(Camera.main.ScreenPointToRay(vector), ref result);
		return result;
	}

	// Token: 0x06002ECF RID: 11983 RVA: 0x00022B80 File Offset: 0x00020D80
	private IEnumerator UcitajOstrvo(string ime)
	{
		GameObject.Find("OblaciPomeranje").GetComponent<Animation>().Play("OblaciPostavljanje");
		yield return new WaitForSeconds(0.85f);
		Application.LoadLevel(ime);
		yield break;
	}

	// Token: 0x040029DB RID: 10715
	private GameObject Kamera;

	// Token: 0x040029DC RID: 10716
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

	// Token: 0x040029DD RID: 10717
	private int KliknutoNa;

	// Token: 0x040029DE RID: 10718
	private float TrenutniX;

	// Token: 0x040029DF RID: 10719
	private float TrenutniY;

	// Token: 0x040029E0 RID: 10720
	private float forceX;

	// Token: 0x040029E1 RID: 10721
	private float forceY;

	// Token: 0x040029E2 RID: 10722
	private float startX;

	// Token: 0x040029E3 RID: 10723
	private float endX;

	// Token: 0x040029E4 RID: 10724
	private float startY;

	// Token: 0x040029E5 RID: 10725
	private float endY;
}
