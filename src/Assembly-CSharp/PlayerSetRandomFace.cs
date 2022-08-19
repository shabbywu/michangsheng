using System;
using System.Collections.Generic;
using JSONClass;
using KBEngine;
using Spine;
using Spine.Unity;
using UnityEngine;
using UnityEngine.UI;
using YSGame;

// Token: 0x02000481 RID: 1153
public class PlayerSetRandomFace : MonoBehaviour
{
	// Token: 0x1700028E RID: 654
	// (get) Token: 0x060023F8 RID: 9208 RVA: 0x000F5A48 File Offset: 0x000F3C48
	public List<SkeletonDataAsset> SkeletonDataAsset
	{
		get
		{
			if (this.skeletonDataAsset == null)
			{
				this.skeletonDataAsset = new List<SkeletonDataAsset>();
				this.skeletonDataAsset.Add(ResManager.inst.LoadABSkeletonDataAsset(1, "new_sanxiu_2_SkeletonData"));
				this.skeletonDataAsset.Add(ResManager.inst.LoadABSkeletonDataAsset(2, "womensanxiu_0_SkeletonData"));
			}
			return this.skeletonDataAsset;
		}
	}

	// Token: 0x060023F9 RID: 9209 RVA: 0x00004095 File Offset: 0x00002295
	private void Awake()
	{
	}

	// Token: 0x060023FA RID: 9210 RVA: 0x000F5AA4 File Offset: 0x000F3CA4
	private void OnEnable()
	{
		this.setFace();
	}

	// Token: 0x060023FB RID: 9211 RVA: 0x000F5AAC File Offset: 0x000F3CAC
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
		JSONObject avatarRandomJsonData = jsonData.instance.AvatarRandomJsonData;
		if (avatarRandomJsonData != null && avatarRandomJsonData.Count > 0)
		{
			try
			{
				this.setFaceByJson(avatarRandomJsonData["1"], 0);
			}
			catch (Exception)
			{
				int num;
				if (avatarRandomJsonData["1"]["Sex"].I == 1 || this.isDoFu)
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
							avatarRandomJsonData["1"].SetField(SetAvatarFaceRandomInfo.inst.StaticRandomInfo[i].faceinfoList[j].SkinTypeName.ToString(), SetAvatarFaceRandomInfo.inst.StaticRandomInfo[i].faceinfoList[j].SkinTypeScope);
						}
						this.setFaceByJson(avatarRandomJsonData["1"], 1);
						break;
					}
				}
			}
		}
	}

	// Token: 0x060023FC RID: 9212 RVA: 0x000F5C2C File Offset: 0x000F3E2C
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

	// Token: 0x060023FD RID: 9213 RVA: 0x000F5D58 File Offset: 0x000F3F58
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

	// Token: 0x060023FE RID: 9214 RVA: 0x000F5DAB File Offset: 0x000F3FAB
	public void NewSetSelectFace(int id, int index, JSONObject face)
	{
		if (face != null)
		{
			this.setFaceByJson(face, 0);
			return;
		}
		this.SetSelectFace(id, index);
	}

	// Token: 0x060023FF RID: 9215 RVA: 0x000F5DC1 File Offset: 0x000F3FC1
	public void SetNPCFace(int npcid)
	{
		this.faceID = npcid;
		this.randomAvatar(npcid);
	}

	// Token: 0x06002400 RID: 9216 RVA: 0x000F5DD4 File Offset: 0x000F3FD4
	public void randomAvatar(int monstarID)
	{
		try
		{
			JSONObject randomInfo = jsonData.instance.AvatarRandomJsonData[monstarID.ToString()];
			JSONObject jsonobject = jsonData.instance.AvatarJsonData[monstarID.ToString()];
			int num = jsonobject["face"].I;
			string text = "";
			if (jsonobject.HasField("workshoplihui"))
			{
				text = jsonobject["workshoplihui"].str;
			}
			if (monstarID == 1)
			{
				Avatar player = PlayerEx.Player;
				if (player != null)
				{
					num = player.Face;
					text = player.FaceWorkshop;
				}
			}
			bool flag = false;
			if (num != 0 && base.GetComponent<SkeletonGraphic>() != null)
			{
				Sprite sprite;
				if (!string.IsNullOrWhiteSpace(text))
				{
					sprite = ModResources.LoadSprite(string.Format("workshop_{0}_{1}", text, num));
				}
				else
				{
					sprite = ModResources.LoadSprite(string.Format("Effect/Prefab/gameEntity/Avater/Avater{0}/{1}", num, num));
				}
				if (sprite != null)
				{
					flag = true;
					this.BaseImage.SetActive(true);
					this.BaseSpine.SetActive(false);
					Image componentInChildren = this.BaseImage.GetComponentInChildren<Image>();
					componentInChildren.sprite = sprite;
					if (this.SpriteUseOffset)
					{
						string key = "P" + sprite.name;
						if (TouXiangPianYi.DataDict.ContainsKey(key))
						{
							TouXiangPianYi touXiangPianYi = TouXiangPianYi.DataDict[key];
							componentInChildren.rectTransform.anchoredPosition = new Vector2((float)touXiangPianYi.PX / 100f, (float)touXiangPianYi.PY / 100f);
							componentInChildren.rectTransform.localScale = new Vector3((float)touXiangPianYi.SX / 100f, (float)touXiangPianYi.SY / 100f, 1f);
						}
					}
				}
			}
			if (!flag)
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

	// Token: 0x06002401 RID: 9217 RVA: 0x000F601C File Offset: 0x000F421C
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
		try
		{
			if ((int)randomInfo["Sex"].n <= 2)
			{
				int index = (int)randomInfo["Sex"].n - 1;
				component.skeletonDataAsset = this.SkeletonDataAsset[index];
				component.initialSkinName = "0";
				if (this.isTalk)
				{
					component.startingAnimation = "talk_0";
				}
				component.Initialize(true);
				base.transform.GetComponent<RectTransform>().anchoredPosition = this.setPosition[index];
				base.transform.localScale = this.setScale[index];
			}
		}
		catch
		{
			Debug.Log("");
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

	// Token: 0x06002402 RID: 9218 RVA: 0x000F6398 File Offset: 0x000F4598
	private void CanvasChangeSlot(string unit, int value, JSONObject randomInfo)
	{
		this.CanvasChangeSlot(unit + "_" + value, randomInfo);
	}

	// Token: 0x06002403 RID: 9219 RVA: 0x000F63B4 File Offset: 0x000F45B4
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

	// Token: 0x06002404 RID: 9220 RVA: 0x000F63E8 File Offset: 0x000F45E8
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

	// Token: 0x06002405 RID: 9221 RVA: 0x000F642B File Offset: 0x000F462B
	public int getHairColor(int random, string Na)
	{
		return 256 - (int)jsonData.instance.HairRandomColorJsonData[random][Na].n;
	}

	// Token: 0x06002406 RID: 9222 RVA: 0x000F644F File Offset: 0x000F464F
	public int getRandomColor(JSONObject aa, int random, string Na)
	{
		return 256 - (int)aa[random][Na].n;
	}

	// Token: 0x06002407 RID: 9223 RVA: 0x000F646C File Offset: 0x000F466C
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

	// Token: 0x06002408 RID: 9224 RVA: 0x000F6674 File Offset: 0x000F4874
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

	// Token: 0x06002409 RID: 9225 RVA: 0x000F687C File Offset: 0x000F4A7C
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

	// Token: 0x04001CC5 RID: 7365
	[SerializeField]
	private SpineAtlasAsset[] faces;

	// Token: 0x04001CC6 RID: 7366
	public Material sourceMaterial;

	// Token: 0x04001CC7 RID: 7367
	public Sprite sprite;

	// Token: 0x04001CC8 RID: 7368
	public bool SpriteUseOffset;

	// Token: 0x04001CC9 RID: 7369
	private List<SkeletonDataAsset> skeletonDataAsset;

	// Token: 0x04001CCA RID: 7370
	public List<Vector3> setPosition = new List<Vector3>();

	// Token: 0x04001CCB RID: 7371
	public List<Vector3> setScale = new List<Vector3>();

	// Token: 0x04001CCC RID: 7372
	public List<YSImageRandomInfo> ImageList = new List<YSImageRandomInfo>();

	// Token: 0x04001CCD RID: 7373
	public GameObject BaseImage;

	// Token: 0x04001CCE RID: 7374
	public GameObject BaseSpine;

	// Token: 0x04001CCF RID: 7375
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

	// Token: 0x04001CD0 RID: 7376
	private string[] randomColor = new string[]
	{
		"Shawl_hair",
		"hair"
	};

	// Token: 0x04001CD1 RID: 7377
	private List<string> SimQuanZhong = new List<string>
	{
		"upper_body",
		"lower_body",
		"r_arm",
		"l_arm",
		"shoes"
	};

	// Token: 0x04001CD2 RID: 7378
	public bool canInit = true;

	// Token: 0x04001CD3 RID: 7379
	public bool isFight;

	// Token: 0x04001CD4 RID: 7380
	public bool isTalk;

	// Token: 0x04001CD5 RID: 7381
	public int faceID;

	// Token: 0x04001CD6 RID: 7382
	public bool isDoFu;
}
