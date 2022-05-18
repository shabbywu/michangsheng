using System;
using System.Collections.Generic;
using Spine;
using Spine.Unity;
using UnityEngine;
using UnityEngine.UI;
using YSGame;

// Token: 0x02000642 RID: 1602
public class PlayerSetRandomFace : MonoBehaviour
{
	// Token: 0x060027BC RID: 10172 RVA: 0x000042DD File Offset: 0x000024DD
	private void Awake()
	{
	}

	// Token: 0x060027BD RID: 10173 RVA: 0x0001F641 File Offset: 0x0001D841
	private void OnEnable()
	{
		this.setFace();
	}

	// Token: 0x060027BE RID: 10174 RVA: 0x00135878 File Offset: 0x00133A78
	public void setFace()
	{
		if (this.isFight)
		{
			return;
		}
		if (this.canInit)
		{
			this.randomAvatar(1);
			return;
		}
		JSONObject jsonObject = YSSaveGame.GetJsonObject("AvatarRandomJsonData" + Tools.instance.getSaveID(this.faceID, 0), null);
		if (jsonObject != null && jsonObject.Count > 0)
		{
			try
			{
				this.setFaceByJson(jsonObject["1"], 0);
			}
			catch (Exception)
			{
				int num;
				if (jsonObject["1"]["Sex"].I == 1 || this.isDoFu)
				{
					num = 10001;
				}
				else
				{
					num = 618;
				}
				for (int i = 0; i < SetAvatarFaceRandomInfo.inst.StaticRandomInfo.Count; i++)
				{
					if (SetAvatarFaceRandomInfo.inst.StaticRandomInfo[i].AvatarScope == num)
					{
						for (int j = 0; j < SetAvatarFaceRandomInfo.inst.StaticRandomInfo[i].faceinfoList.Count; j++)
						{
							jsonObject["1"].SetField(SetAvatarFaceRandomInfo.inst.StaticRandomInfo[i].faceinfoList[j].SkinTypeName.ToString(), SetAvatarFaceRandomInfo.inst.StaticRandomInfo[i].faceinfoList[j].SkinTypeScope);
						}
						this.setFaceByJson(jsonObject["1"], 1);
						YSSaveGame.save("AvatarRandomJsonData" + Tools.instance.getSaveID(this.faceID, 0), jsonObject, Paths.GetSavePath());
						for (int k = 0; k < 6; k++)
						{
							if (YSSaveGame.HasFile(Paths.GetSavePath(), "AvatarRandomJsonData" + Tools.instance.getSaveID(this.faceID, k)))
							{
								YSSaveGame.save("AvatarRandomJsonData" + Tools.instance.getSaveID(this.faceID, k), jsonObject, Paths.GetSavePath());
							}
						}
						break;
					}
				}
			}
		}
	}

	// Token: 0x060027BF RID: 10175 RVA: 0x00135A94 File Offset: 0x00133C94
	public void SetDoFaFace()
	{
		int num = 10001;
		jsonData.instance.AvatarRandomJsonData = new JSONObject(JSONObject.Type.OBJECT);
		jsonData.instance.refreshMonstar(1);
		jsonData.instance.AvatarRandomJsonData["1"].SetField("Name", "吾");
		JSONObject avatarRandomJsonData = jsonData.instance.AvatarRandomJsonData;
		for (int i = 0; i < SetAvatarFaceRandomInfo.inst.StaticRandomInfo.Count; i++)
		{
			if (SetAvatarFaceRandomInfo.inst.StaticRandomInfo[i].AvatarScope == num)
			{
				for (int j = 0; j < SetAvatarFaceRandomInfo.inst.StaticRandomInfo[i].faceinfoList.Count; j++)
				{
					avatarRandomJsonData["1"].SetField(SetAvatarFaceRandomInfo.inst.StaticRandomInfo[i].faceinfoList[j].SkinTypeName.ToString(), SetAvatarFaceRandomInfo.inst.StaticRandomInfo[i].faceinfoList[j].SkinTypeScope);
				}
				this.setFaceByJson(avatarRandomJsonData["1"], 1);
			}
		}
	}

	// Token: 0x060027C0 RID: 10176 RVA: 0x00135BC0 File Offset: 0x00133DC0
	public void SetSelectFace(int id, int index)
	{
		JSONObject jsonObject = YSSaveGame.GetJsonObject("AvatarRandomJsonData" + Tools.instance.getSaveID(id, index), null);
		if (jsonObject != null && jsonObject.Count > 0)
		{
			this.setFaceByJson(jsonObject["1"], 0);
			return;
		}
		throw new Exception("读取脸部数据失败");
	}

	// Token: 0x060027C1 RID: 10177 RVA: 0x0001F649 File Offset: 0x0001D849
	public void SetNPCFace(int npcid)
	{
		this.faceID = npcid;
		this.RandomAvatar(npcid);
	}

	// Token: 0x060027C2 RID: 10178 RVA: 0x00135C14 File Offset: 0x00133E14
	public void randomAvatar(int monstarID)
	{
		try
		{
			JSONObject randomInfo = jsonData.instance.AvatarRandomJsonData[monstarID.ToString()];
			int i = jsonData.instance.AvatarJsonData[monstarID.ToString()]["face"].I;
			if (monstarID == 1 && PlayerEx.Player != null)
			{
				i = PlayerEx.Player.Face.I;
			}
			if (i != 0 && base.GetComponent<SkeletonGraphic>() != null)
			{
				this.BaseImage.SetActive(true);
				this.BaseSpine.SetActive(false);
				this.BaseImage.GetComponentInChildren<Image>().sprite = ModResources.LoadSprite(string.Format("Effect/Prefab/gameEntity/Avater/Avater{0}/{1}", i, i));
			}
			else
			{
				if (this.BaseImage != null)
				{
					this.BaseImage.SetActive(false);
				}
				if (this.BaseSpine != null)
				{
					this.BaseSpine.SetActive(true);
				}
				this.setFaceByJson(randomInfo, 0);
			}
		}
		catch (Exception ex)
		{
			Debug.LogError(string.Format("设置形象时出错，ID:{0}，错误:{1}\n{2}", monstarID, ex.Message, ex.StackTrace));
			throw ex;
		}
	}

	// Token: 0x060027C3 RID: 10179 RVA: 0x00135D44 File Offset: 0x00133F44
	public void RandomAvatar(int monstarID)
	{
		JSONObject randomInfo = jsonData.instance.AvatarRandomJsonData[monstarID.ToString()];
		int i = jsonData.instance.AvatarJsonData[monstarID.ToString()]["face"].I;
		if (monstarID == 1 && PlayerEx.Player != null)
		{
			i = PlayerEx.Player.Face.I;
		}
		if (i != 0 && base.GetComponent<SkeletonGraphic>() != null)
		{
			this.BaseImage.SetActive(true);
			this.BaseSpine.SetActive(false);
			this.BaseImage.GetComponentInChildren<Image>().sprite = ModResources.LoadSprite(string.Format("Effect/Prefab/gameEntity/Avater/Avater{0}/{1}", i, i));
			return;
		}
		if (this.BaseImage != null)
		{
			this.BaseImage.SetActive(false);
		}
		if (this.BaseSpine != null)
		{
			this.BaseSpine.SetActive(true);
		}
		this.setFaceByJson(randomInfo, 0);
	}

	// Token: 0x060027C4 RID: 10180 RVA: 0x00135E3C File Offset: 0x0013403C
	public void setFaceByJson(JSONObject randomInfo, int type = 0)
	{
		SkeletonGraphic component = base.GetComponent<SkeletonGraphic>();
		if (component == null)
		{
			base.GetComponent<SkeletonRenderer>().Initialize(true);
			using (List<JSONObject>.Enumerator enumerator = jsonData.instance.SuiJiTouXiangGeShuJsonData.list.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					JSONObject jsonobject = enumerator.Current;
					List<int> suijiList = jsonData.instance.getSuijiList(jsonobject["StrID"].str, "Sex" + randomInfo["Sex"].I);
					if (suijiList.Count != 0 && randomInfo.HasField(jsonobject["StrID"].str))
					{
						int value = suijiList[randomInfo[jsonobject["StrID"].str].I % suijiList.Count];
						if (suijiList.Contains(randomInfo[jsonobject["StrID"].str].I))
						{
							value = randomInfo[jsonobject["StrID"].str].I;
						}
						this.ChangeSlot(jsonobject["StrID"].str, value, randomInfo);
					}
				}
				return;
			}
		}
		if ((int)randomInfo["Sex"].n <= 2)
		{
			int index = (int)randomInfo["Sex"].n - 1;
			component.skeletonDataAsset = this.otherSkeletonDataAsset[index];
			component.initialSkinName = "0";
			if (this.isTalk)
			{
				component.startingAnimation = "talk_0";
			}
			component.Initialize(true);
			base.transform.GetComponent<RectTransform>().anchoredPosition = this.setPosition[index];
			base.transform.localScale = this.setScale[index];
		}
		foreach (JSONObject jsonobject2 in jsonData.instance.SuiJiTouXiangGeShuJsonData.list)
		{
			List<int> suijiList2 = jsonData.instance.getSuijiList(jsonobject2["StrID"].str, "Sex" + randomInfo["Sex"].I);
			if (suijiList2.Count != 0 && (type != 1 || randomInfo.HasField(jsonobject2["StrID"].str)))
			{
				int value2 = suijiList2[randomInfo[jsonobject2["StrID"].str].I % suijiList2.Count];
				if (suijiList2.Contains(randomInfo[jsonobject2["StrID"].str].I))
				{
					value2 = randomInfo[jsonobject2["StrID"].str].I;
				}
				this.CanvasChangeSlot(jsonobject2["StrID"].str, value2, randomInfo);
			}
		}
	}

	// Token: 0x060027C5 RID: 10181 RVA: 0x0001F659 File Offset: 0x0001D859
	private void CanvasChangeSlot(string unit, int value, JSONObject randomInfo)
	{
		this.CanvasChangeSlot(unit + "_" + value, randomInfo);
	}

	// Token: 0x060027C6 RID: 10182 RVA: 0x00136190 File Offset: 0x00134390
	public void CanvasChangeSlot(string unit, JSONObject randomInfo)
	{
		SkeletonGraphic component = base.GetComponent<SkeletonGraphic>();
		Skin skin = component.Skeleton.Data.FindSkin(unit);
		if (skin == null)
		{
			return;
		}
		this.setAttach(component, skin, randomInfo);
	}

	// Token: 0x060027C7 RID: 10183 RVA: 0x001361C4 File Offset: 0x001343C4
	public void ChangeSlot(string unit, int value, JSONObject randomInfo)
	{
		SkeletonRenderer component = base.GetComponent<SkeletonRenderer>();
		Skin skin = component.Skeleton.Data.FindSkin(unit + "_" + value);
		if (skin == null)
		{
			return;
		}
		this.setAttach(component, skin, randomInfo);
	}

	// Token: 0x060027C8 RID: 10184 RVA: 0x0001F673 File Offset: 0x0001D873
	public int getHairColor(int random, string Na)
	{
		return 256 - (int)jsonData.instance.HairRandomColorJsonData[random][Na].n;
	}

	// Token: 0x060027C9 RID: 10185 RVA: 0x0001F697 File Offset: 0x0001D897
	public int getRandomColor(JSONObject aa, int random, string Na)
	{
		return 256 - (int)aa[random][Na].n;
	}

	// Token: 0x060027CA RID: 10186 RVA: 0x00136208 File Offset: 0x00134408
	public void setAttach(SkeletonRenderer skeletonRenderer, Skin skin, JSONObject randomInfo)
	{
		foreach (KeyValuePair<Skin.AttachmentKeyTuple, Attachment> keyValuePair in skeletonRenderer.Skeleton.Skin.Attachments)
		{
			int slotIndex = keyValuePair.Key.slotIndex;
			Slot slot = skeletonRenderer.Skeleton.Slots.Items[slotIndex];
			if (slot.Attachment == keyValuePair.Value)
			{
				if (jsonData.instance != null)
				{
					this.setRandomColor("hairColorR", keyValuePair.Key.name.Contains("hair"), randomInfo, slot, jsonData.instance.HairRandomColorJsonData);
					this.setRandomColor("mouthColor", keyValuePair.Key.name.Contains("wouth"), randomInfo, slot, jsonData.instance.MouthRandomColorJsonData);
					this.setRandomColor("blushColor", keyValuePair.Key.name.Contains("blush"), randomInfo, slot, jsonData.instance.SaiHonRandomColorJsonData);
					this.setRandomColor("yanzhuColor", keyValuePair.Key.name.Contains("yanqiu"), randomInfo, slot, jsonData.instance.YanZhuYanSeRandomColorJsonData);
					this.setRandomColor("tezhengColor", keyValuePair.Key.name.Contains("characteristic"), randomInfo, slot, jsonData.instance.MianWenYanSeRandomColorJsonData);
					this.setRandomColor("eyebrowColor", keyValuePair.Key.name.Contains("eyebrow"), randomInfo, slot, jsonData.instance.MeiMaoYanSeRandomColorJsonData);
					this.setRandomColor("tezhengColor", keyValuePair.Key.name.Contains("feature"), randomInfo, slot, jsonData.instance.MianWenYanSeRandomColorJsonData);
				}
				Attachment attachment = skin.GetAttachment(slotIndex, keyValuePair.Key.name);
				if (attachment != null)
				{
					slot.Attachment = attachment;
				}
			}
		}
	}

	// Token: 0x060027CB RID: 10187 RVA: 0x00136410 File Offset: 0x00134610
	public void setAttach(SkeletonGraphic skeletonRenderer, Skin skin, JSONObject randomInfo)
	{
		foreach (KeyValuePair<Skin.AttachmentKeyTuple, Attachment> keyValuePair in skeletonRenderer.Skeleton.Skin.Attachments)
		{
			int slotIndex = keyValuePair.Key.slotIndex;
			Slot slot = skeletonRenderer.Skeleton.Slots.Items[slotIndex];
			if (slot.Attachment == keyValuePair.Value)
			{
				if (jsonData.instance != null)
				{
					this.setRandomColor("hairColorR", keyValuePair.Key.name.Contains("hair"), randomInfo, slot, jsonData.instance.HairRandomColorJsonData);
					this.setRandomColor("mouthColor", keyValuePair.Key.name.Contains("wouth"), randomInfo, slot, jsonData.instance.MouthRandomColorJsonData);
					this.setRandomColor("blushColor", keyValuePair.Key.name.Contains("blush"), randomInfo, slot, jsonData.instance.SaiHonRandomColorJsonData);
					this.setRandomColor("yanzhuColor", keyValuePair.Key.name.Contains("yanqiu"), randomInfo, slot, jsonData.instance.YanZhuYanSeRandomColorJsonData);
					this.setRandomColor("tezhengColor", keyValuePair.Key.name.Contains("characteristic"), randomInfo, slot, jsonData.instance.MianWenYanSeRandomColorJsonData);
					this.setRandomColor("eyebrowColor", keyValuePair.Key.name.Contains("eyebrow"), randomInfo, slot, jsonData.instance.MeiMaoYanSeRandomColorJsonData);
					this.setRandomColor("tezhengColor", keyValuePair.Key.name.Contains("feature"), randomInfo, slot, jsonData.instance.MianWenYanSeRandomColorJsonData);
				}
				Attachment attachment = skin.GetAttachment(slotIndex, keyValuePair.Key.name);
				if (attachment != null)
				{
					slot.Attachment = attachment;
				}
			}
		}
	}

	// Token: 0x060027CC RID: 10188 RVA: 0x00136618 File Offset: 0x00134818
	public void setRandomColor(string ColorName, bool hasName, JSONObject randomInfo, Slot slot, JSONObject color)
	{
		if (hasName && randomInfo != null && jsonData.instance != null)
		{
			try
			{
				int random = (int)randomInfo[ColorName].n % color.Count;
				int num = this.getRandomColor(color, random, "R");
				int num2 = this.getRandomColor(color, random, "G");
				int num3 = this.getRandomColor(color, random, "B");
				SkeletonExtensions.SetColor(slot, new Color((float)num, (float)num2, (float)num3));
			}
			catch (Exception)
			{
				SkeletonExtensions.SetColor(slot, new Color(1f, 1f, 1f));
			}
		}
	}

	// Token: 0x040021A8 RID: 8616
	[SerializeField]
	private SpineAtlasAsset[] faces;

	// Token: 0x040021A9 RID: 8617
	public Material sourceMaterial;

	// Token: 0x040021AA RID: 8618
	public Sprite sprite;

	// Token: 0x040021AB RID: 8619
	public List<SkeletonDataAsset> otherSkeletonDataAsset = new List<SkeletonDataAsset>();

	// Token: 0x040021AC RID: 8620
	public List<Vector3> setPosition = new List<Vector3>();

	// Token: 0x040021AD RID: 8621
	public List<Vector3> setScale = new List<Vector3>();

	// Token: 0x040021AE RID: 8622
	public List<YSImageRandomInfo> ImageList = new List<YSImageRandomInfo>();

	// Token: 0x040021AF RID: 8623
	public GameObject BaseImage;

	// Token: 0x040021B0 RID: 8624
	public GameObject BaseSpine;

	// Token: 0x040021B1 RID: 8625
	private string[] buweiName = new string[]
	{
		"Shawl_hair",
		"back_gown",
		"eyes",
		"gown",
		"hair",
		"l_arm",
		"l_big_arm",
		"lower_body",
		"mouth",
		"nose",
		"r_arm",
		"r_big_arm",
		"shoes",
		"upper_body",
		"a_hair",
		"b_hair"
	};

	// Token: 0x040021B2 RID: 8626
	private string[] randomColor = new string[]
	{
		"Shawl_hair",
		"hair"
	};

	// Token: 0x040021B3 RID: 8627
	private List<string> SimQuanZhong = new List<string>
	{
		"upper_body",
		"lower_body",
		"r_arm",
		"l_arm",
		"shoes"
	};

	// Token: 0x040021B4 RID: 8628
	public bool canInit = true;

	// Token: 0x040021B5 RID: 8629
	public bool isFight;

	// Token: 0x040021B6 RID: 8630
	public bool isTalk;

	// Token: 0x040021B7 RID: 8631
	public int faceID;

	// Token: 0x040021B8 RID: 8632
	public bool isDoFu;
}
