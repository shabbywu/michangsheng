using System.Collections.Generic;
using Bag;
using KBEngine;
using Newtonsoft.Json.Linq;
using Spine.Unity;
using UnityEngine;
using UnityEngine.UI;

public class MapPlayerSeaShow : MonoBehaviour
{
	public GameObject ChuanObj;

	public SkeletonAnimation ChuanSpine;

	public SkeletonDataAsset ChuanGuGe;

	public SkeletonDataAsset YouYongGuGe;

	public Animator Anim;

	public SpriteRenderer SeaZheZhao;

	public List<Sprite> ShenShiZheZhaoList;

	public GameObject HPObj;

	public Slider HPSlider;

	public Image YouYaoYe;

	public Image QuYaoYe;

	public Text Percent;

	private MapPlayerController controller;

	private MeshRenderer meshRenderer;

	private static Vector3 fanXiang = new Vector3(-1f, 1f, 1f);

	public void Init(MapPlayerController controller)
	{
		this.controller = controller;
	}

	private void Update()
	{
		if (controller.IsOnSea && controller.ShowType == MapPlayerShowType.灵舟)
		{
			RefreshLingZhouHP();
		}
	}

	public void SetDir(SeaAvatarObjBase.Directon directon)
	{
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		Anim.SetInteger("direction", (int)directon);
		if (directon == SeaAvatarObjBase.Directon.Right)
		{
			((Component)ChuanSpine).transform.localScale = fanXiang;
		}
		else
		{
			((Component)ChuanSpine).transform.localScale = Vector3.one;
		}
	}

	public void Refresh()
	{
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		if ((Object)(object)meshRenderer == (Object)null)
		{
			meshRenderer = ((Component)ChuanSpine).GetComponent<MeshRenderer>();
		}
		if (controller.IsOnSea)
		{
			((Renderer)meshRenderer).sortingOrder = 1;
			((Component)SeaZheZhao).gameObject.SetActive(true);
			switch (controller.ShowType)
			{
			case MapPlayerShowType.灵舟:
			{
				ChuanObj.SetActive(true);
				ChuanObj.transform.localScale = new Vector3(0.4f, 0.4f, 1f);
				HPObj.SetActive(true);
				string skinName = $"boat_{controller.EquipLingZhou.quality}";
				SetSpine(ChuanGuGe, skinName);
				break;
			}
			case MapPlayerShowType.游泳:
				ChuanObj.SetActive(true);
				ChuanObj.transform.localScale = new Vector3(0.2f, 0.2f, 1f);
				HPObj.SetActive(false);
				SetSpine(YouYongGuGe, "default");
				break;
			default:
				HPObj.SetActive(false);
				ChuanObj.SetActive(false);
				break;
			}
			SetItemBuff();
			SetShiYe();
		}
		else
		{
			((Component)SeaZheZhao).gameObject.SetActive(false);
			HPObj.SetActive(false);
			ChuanObj.SetActive(false);
		}
	}

	private void RefreshLingZhouHP()
	{
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		BaseItem lingZhou = PlayerEx.Player.GetLingZhou();
		if (HPObj.activeSelf && lingZhou != null)
		{
			int num = (int)jsonData.instance.LingZhouPinJie[controller.EquipLingZhou.quality.ToString()][(object)"Naijiu"];
			Percent.text = lingZhou.Seid["NaiJiu"].I + "/" + num;
			HPSlider.value = lingZhou.Seid["NaiJiu"].n / (float)num * 100f;
			float num2 = Mathf.Clamp(Mathf.Sin(Time.time) + 0.5f, 0f, 1f);
			Color color = default(Color);
			((Color)(ref color))._002Ector(1f, 1f, 1f, num2);
			((Graphic)YouYaoYe).color = color;
			((Graphic)QuYaoYe).color = color;
		}
	}

	private void SetItemBuff()
	{
		Avatar player = PlayerEx.Player;
		if (player.ItemSeid27Days() > 0)
		{
			string text = ((string)player.ItemBuffList["27"][(object)"icon"]).ToCN();
			if (text == "诱")
			{
				((Component)YouYaoYe).gameObject.SetActive(true);
				((Component)QuYaoYe).gameObject.SetActive(false);
			}
			else if (text == "驱")
			{
				((Component)YouYaoYe).gameObject.SetActive(false);
				((Component)QuYaoYe).gameObject.SetActive(true);
			}
		}
		else
		{
			((Component)YouYaoYe).gameObject.SetActive(false);
			((Component)QuYaoYe).gameObject.SetActive(false);
		}
	}

	private void SetShiYe()
	{
		((Component)SeaZheZhao).gameObject.SetActive(true);
		Avatar avatar = Tools.instance.getPlayer();
		int num = (int)Tools.FindJTokens((JToken)(object)jsonData.instance.EndlessSeaShiYe, (JToken aa) => ((int)aa[(object)"shenshi"] >= avatar.shengShi) ? true : false)[(object)"id"];
		if (num > ShenShiZheZhaoList.Count)
		{
			num = ShenShiZheZhaoList.Count;
		}
		else if (num <= 0)
		{
			num = 1;
		}
		Sprite sprite = ShenShiZheZhaoList[num - 1];
		SeaZheZhao.sprite = sprite;
	}

	private void SetSpine(SkeletonDataAsset spine, string skinName)
	{
		if ((Object)(object)((SkeletonRenderer)ChuanSpine).skeletonDataAsset != (Object)(object)spine)
		{
			((SkeletonRenderer)ChuanSpine).skeletonDataAsset = spine;
			((SkeletonRenderer)ChuanSpine).initialSkinName = skinName;
			((SkeletonRenderer)ChuanSpine).Initialize(true);
		}
		else if (((SkeletonRenderer)ChuanSpine).initialSkinName != skinName)
		{
			((SkeletonRenderer)ChuanSpine).initialSkinName = skinName;
			((SkeletonRenderer)ChuanSpine).Initialize(true);
		}
	}
}
