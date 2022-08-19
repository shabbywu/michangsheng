using System;
using System.Collections.Generic;
using Bag;
using KBEngine;
using Newtonsoft.Json.Linq;
using Spine.Unity;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000196 RID: 406
public class MapPlayerSeaShow : MonoBehaviour
{
	// Token: 0x06001144 RID: 4420 RVA: 0x00067D5B File Offset: 0x00065F5B
	public void Init(MapPlayerController controller)
	{
		this.controller = controller;
	}

	// Token: 0x06001145 RID: 4421 RVA: 0x00067D64 File Offset: 0x00065F64
	private void Update()
	{
		if (this.controller.IsOnSea && this.controller.ShowType == MapPlayerShowType.灵舟)
		{
			this.RefreshLingZhouHP();
		}
	}

	// Token: 0x06001146 RID: 4422 RVA: 0x00067D88 File Offset: 0x00065F88
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

	// Token: 0x06001147 RID: 4423 RVA: 0x00067DD8 File Offset: 0x00065FD8
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

	// Token: 0x06001148 RID: 4424 RVA: 0x00067F54 File Offset: 0x00066154
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

	// Token: 0x06001149 RID: 4425 RVA: 0x00068064 File Offset: 0x00066264
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

	// Token: 0x0600114A RID: 4426 RVA: 0x0006812C File Offset: 0x0006632C
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

	// Token: 0x0600114B RID: 4427 RVA: 0x000681C8 File Offset: 0x000663C8
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

	// Token: 0x04000C6C RID: 3180
	public GameObject ChuanObj;

	// Token: 0x04000C6D RID: 3181
	public SkeletonAnimation ChuanSpine;

	// Token: 0x04000C6E RID: 3182
	public SkeletonDataAsset ChuanGuGe;

	// Token: 0x04000C6F RID: 3183
	public SkeletonDataAsset YouYongGuGe;

	// Token: 0x04000C70 RID: 3184
	public Animator Anim;

	// Token: 0x04000C71 RID: 3185
	public SpriteRenderer SeaZheZhao;

	// Token: 0x04000C72 RID: 3186
	public List<Sprite> ShenShiZheZhaoList;

	// Token: 0x04000C73 RID: 3187
	public GameObject HPObj;

	// Token: 0x04000C74 RID: 3188
	public Slider HPSlider;

	// Token: 0x04000C75 RID: 3189
	public Image YouYaoYe;

	// Token: 0x04000C76 RID: 3190
	public Image QuYaoYe;

	// Token: 0x04000C77 RID: 3191
	public Text Percent;

	// Token: 0x04000C78 RID: 3192
	private MapPlayerController controller;

	// Token: 0x04000C79 RID: 3193
	private MeshRenderer meshRenderer;

	// Token: 0x04000C7A RID: 3194
	private static Vector3 fanXiang = new Vector3(-1f, 1f, 1f);
}
