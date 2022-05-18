using System;
using System.Collections.Generic;
using Bag;
using KBEngine;
using Newtonsoft.Json.Linq;
using Spine.Unity;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000287 RID: 647
public class MapPlayerSeaShow : MonoBehaviour
{
	// Token: 0x060013CD RID: 5069 RVA: 0x00012806 File Offset: 0x00010A06
	public void Init(MapPlayerController controller)
	{
		this.controller = controller;
	}

	// Token: 0x060013CE RID: 5070 RVA: 0x0001280F File Offset: 0x00010A0F
	private void Update()
	{
		if (this.controller.IsOnSea && this.controller.ShowType == MapPlayerShowType.灵舟)
		{
			this.RefreshLingZhouHP();
		}
	}

	// Token: 0x060013CF RID: 5071 RVA: 0x000B62F8 File Offset: 0x000B44F8
	public void SetDir(SeaAvatarObjBase.Directon directon)
	{
		this.Anim.SetInteger("direction", (int)directon);
		if (directon == SeaAvatarObjBase.Directon.Right)
		{
			this.ChuanSpine.transform.localScale = MapPlayerSeaShow.fanXiang;
			return;
		}
		this.ChuanSpine.transform.localScale = Vector3.one;
	}

	// Token: 0x060013D0 RID: 5072 RVA: 0x000B6348 File Offset: 0x000B4548
	public void Refresh()
	{
		if (this.meshRenderer == null)
		{
			this.meshRenderer = this.ChuanSpine.GetComponent<MeshRenderer>();
		}
		if (this.controller.IsOnSea)
		{
			this.meshRenderer.sortingOrder = 1;
			this.SeaZheZhao.gameObject.SetActive(true);
			MapPlayerShowType showType = this.controller.ShowType;
			if (showType != MapPlayerShowType.灵舟)
			{
				if (showType != MapPlayerShowType.游泳)
				{
					this.HPObj.SetActive(false);
					this.ChuanObj.SetActive(false);
				}
				else
				{
					this.ChuanObj.SetActive(true);
					this.ChuanObj.transform.localScale = new Vector3(0.2f, 0.2f, 1f);
					this.HPObj.SetActive(false);
					this.SetSpine(this.YouYongGuGe, "default");
				}
			}
			else
			{
				this.ChuanObj.SetActive(true);
				this.ChuanObj.transform.localScale = new Vector3(0.4f, 0.4f, 1f);
				this.HPObj.SetActive(true);
				string skinName = string.Format("boat_{0}", this.controller.EquipLingZhou.quality);
				this.SetSpine(this.ChuanGuGe, skinName);
			}
			this.SetItemBuff();
			this.SetShiYe();
			return;
		}
		this.SeaZheZhao.gameObject.SetActive(false);
		this.HPObj.SetActive(false);
		this.ChuanObj.SetActive(false);
	}

	// Token: 0x060013D1 RID: 5073 RVA: 0x000B64C4 File Offset: 0x000B46C4
	private void RefreshLingZhouHP()
	{
		BaseItem lingZhou = PlayerEx.Player.GetLingZhou();
		if (this.HPObj.activeSelf && lingZhou != null)
		{
			int num = (int)jsonData.instance.LingZhouPinJie[this.controller.EquipLingZhou.quality.ToString()]["Naijiu"];
			this.Percent.text = lingZhou.Seid["NaiJiu"].I + "/" + num;
			this.HPSlider.value = lingZhou.Seid["NaiJiu"].n / (float)num * 100f;
			float num2 = Mathf.Clamp(Mathf.Sin(Time.time) + 0.5f, 0f, 1f);
			Color color;
			color..ctor(1f, 1f, 1f, num2);
			this.YouYaoYe.color = color;
			this.QuYaoYe.color = color;
		}
	}

	// Token: 0x060013D2 RID: 5074 RVA: 0x000B65D4 File Offset: 0x000B47D4
	private void SetItemBuff()
	{
		Avatar player = PlayerEx.Player;
		if (player.ItemSeid27Days() > 0)
		{
			string a = ((string)player.ItemBuffList["27"]["icon"]).ToCN();
			if (a == "诱")
			{
				this.YouYaoYe.gameObject.SetActive(true);
				this.QuYaoYe.gameObject.SetActive(false);
				return;
			}
			if (a == "驱")
			{
				this.YouYaoYe.gameObject.SetActive(false);
				this.QuYaoYe.gameObject.SetActive(true);
				return;
			}
		}
		else
		{
			this.YouYaoYe.gameObject.SetActive(false);
			this.QuYaoYe.gameObject.SetActive(false);
		}
	}

	// Token: 0x060013D3 RID: 5075 RVA: 0x000B669C File Offset: 0x000B489C
	private void SetShiYe()
	{
		this.SeaZheZhao.gameObject.SetActive(true);
		Avatar avatar = Tools.instance.getPlayer();
		int num = (int)Tools.FindJTokens(jsonData.instance.EndlessSeaShiYe, (JToken aa) => (int)aa["shenshi"] >= avatar.shengShi)["id"];
		if (num > this.ShenShiZheZhaoList.Count)
		{
			num = this.ShenShiZheZhaoList.Count;
		}
		else if (num <= 0)
		{
			num = 1;
		}
		Sprite sprite = this.ShenShiZheZhaoList[num - 1];
		this.SeaZheZhao.sprite = sprite;
	}

	// Token: 0x060013D4 RID: 5076 RVA: 0x000B6738 File Offset: 0x000B4938
	private void SetSpine(SkeletonDataAsset spine, string skinName)
	{
		if (this.ChuanSpine.skeletonDataAsset != spine)
		{
			this.ChuanSpine.skeletonDataAsset = spine;
			this.ChuanSpine.initialSkinName = skinName;
			this.ChuanSpine.Initialize(true);
			return;
		}
		if (this.ChuanSpine.initialSkinName != skinName)
		{
			this.ChuanSpine.initialSkinName = skinName;
			this.ChuanSpine.Initialize(true);
		}
	}

	// Token: 0x04000F70 RID: 3952
	public GameObject ChuanObj;

	// Token: 0x04000F71 RID: 3953
	public SkeletonAnimation ChuanSpine;

	// Token: 0x04000F72 RID: 3954
	public SkeletonDataAsset ChuanGuGe;

	// Token: 0x04000F73 RID: 3955
	public SkeletonDataAsset YouYongGuGe;

	// Token: 0x04000F74 RID: 3956
	public Animator Anim;

	// Token: 0x04000F75 RID: 3957
	public SpriteRenderer SeaZheZhao;

	// Token: 0x04000F76 RID: 3958
	public List<Sprite> ShenShiZheZhaoList;

	// Token: 0x04000F77 RID: 3959
	public GameObject HPObj;

	// Token: 0x04000F78 RID: 3960
	public Slider HPSlider;

	// Token: 0x04000F79 RID: 3961
	public Image YouYaoYe;

	// Token: 0x04000F7A RID: 3962
	public Image QuYaoYe;

	// Token: 0x04000F7B RID: 3963
	public Text Percent;

	// Token: 0x04000F7C RID: 3964
	private MapPlayerController controller;

	// Token: 0x04000F7D RID: 3965
	private MeshRenderer meshRenderer;

	// Token: 0x04000F7E RID: 3966
	private static Vector3 fanXiang = new Vector3(-1f, 1f, 1f);
}
