using System;
using System.Collections.Generic;
using JSONClass;
using KBEngine;
using Spine;
using Spine.Unity;
using UnityEngine;
using UnityEngine.UI;
using YSGame;

public class PlayerSetRandomFace : MonoBehaviour
{
	[SerializeField]
	private SpineAtlasAsset[] faces;

	public Material sourceMaterial;

	public Sprite sprite;

	public bool SpriteUseOffset;

	private List<SkeletonDataAsset> skeletonDataAsset;

	public List<Vector3> setPosition = new List<Vector3>();

	public List<Vector3> setScale = new List<Vector3>();

	public List<YSImageRandomInfo> ImageList = new List<YSImageRandomInfo>();

	public GameObject BaseImage;

	public GameObject BaseSpine;

	private string[] buweiName = new string[16]
	{
		"Shawl_hair", "back_gown", "eyes", "gown", "hair", "l_arm", "l_big_arm", "lower_body", "mouth", "nose",
		"r_arm", "r_big_arm", "shoes", "upper_body", "a_hair", "b_hair"
	};

	private string[] randomColor = new string[2] { "Shawl_hair", "hair" };

	private List<string> SimQuanZhong = new List<string> { "upper_body", "lower_body", "r_arm", "l_arm", "shoes" };

	public bool canInit = true;

	public bool isFight;

	public bool isTalk;

	public int faceID;

	public bool isDoFu;

	public List<SkeletonDataAsset> SkeletonDataAsset
	{
		get
		{
			if (skeletonDataAsset == null)
			{
				skeletonDataAsset = new List<SkeletonDataAsset>();
				skeletonDataAsset.Add(ResManager.inst.LoadABSkeletonDataAsset(1, "new_sanxiu_2_SkeletonData"));
				skeletonDataAsset.Add(ResManager.inst.LoadABSkeletonDataAsset(2, "womensanxiu_0_SkeletonData"));
			}
			return skeletonDataAsset;
		}
	}

	private void Awake()
	{
	}

	private void OnEnable()
	{
		setFace();
	}

	public void setFace()
	{
		if (isFight)
		{
			return;
		}
		if (canInit)
		{
			randomAvatar(1);
			return;
		}
		JSONObject avatarRandomJsonData = jsonData.instance.AvatarRandomJsonData;
		if (avatarRandomJsonData == null || avatarRandomJsonData.Count <= 0)
		{
			return;
		}
		try
		{
			setFaceByJson(avatarRandomJsonData["1"]);
		}
		catch (Exception)
		{
			int num = -1;
			num = ((avatarRandomJsonData["1"]["Sex"].I != 1 && !isDoFu) ? 618 : 10001);
			for (int i = 0; i < SetAvatarFaceRandomInfo.inst.StaticRandomInfo.Count; i++)
			{
				if (SetAvatarFaceRandomInfo.inst.StaticRandomInfo[i].AvatarScope == num)
				{
					for (int j = 0; j < SetAvatarFaceRandomInfo.inst.StaticRandomInfo[i].faceinfoList.Count; j++)
					{
						avatarRandomJsonData["1"].SetField(SetAvatarFaceRandomInfo.inst.StaticRandomInfo[i].faceinfoList[j].SkinTypeName.ToString(), SetAvatarFaceRandomInfo.inst.StaticRandomInfo[i].faceinfoList[j].SkinTypeScope);
					}
					setFaceByJson(avatarRandomJsonData["1"], 1);
					break;
				}
			}
		}
	}

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
				setFaceByJson(avatarRandomJsonData["1"], 1);
			}
		}
	}

	public void SetSelectFace(int id, int index)
	{
		JSONObject jsonObject = YSSaveGame.GetJsonObject("AvatarRandomJsonData" + Tools.instance.getSaveID(id, index));
		if (jsonObject != null && jsonObject.Count > 0)
		{
			setFaceByJson(jsonObject["1"]);
			return;
		}
		throw new Exception("读取脸部数据失败");
	}

	public void NewSetSelectFace(int id, int index, JSONObject face)
	{
		if (face != null)
		{
			setFaceByJson(face);
		}
		else
		{
			SetSelectFace(id, index);
		}
	}

	public void SetNPCFace(int npcid)
	{
		faceID = npcid;
		randomAvatar(npcid);
	}

	public void randomAvatar(int monstarID)
	{
		//IL_0176: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a8: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			JSONObject randomInfo = jsonData.instance.AvatarRandomJsonData[monstarID.ToString()];
			JSONObject jSONObject = jsonData.instance.AvatarJsonData[monstarID.ToString()];
			int num = jSONObject["face"].I;
			string text = "";
			if (jSONObject.HasField("workshoplihui"))
			{
				text = jSONObject["workshoplihui"].str;
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
			if (num != 0 && (Object)(object)((Component)this).GetComponent<SkeletonGraphic>() != (Object)null)
			{
				Sprite val = (string.IsNullOrWhiteSpace(text) ? ModResources.LoadSprite($"Effect/Prefab/gameEntity/Avater/Avater{num}/{num}") : ModResources.LoadSprite($"workshop_{text}_{num}"));
				if ((Object)(object)val != (Object)null)
				{
					flag = true;
					BaseImage.SetActive(true);
					BaseSpine.SetActive(false);
					Image componentInChildren = BaseImage.GetComponentInChildren<Image>();
					componentInChildren.sprite = val;
					if (SpriteUseOffset)
					{
						string key = "P" + ((Object)val).name;
						if (TouXiangPianYi.DataDict.ContainsKey(key))
						{
							TouXiangPianYi touXiangPianYi = TouXiangPianYi.DataDict[key];
							((Graphic)componentInChildren).rectTransform.anchoredPosition = new Vector2((float)touXiangPianYi.PX / 100f, (float)touXiangPianYi.PY / 100f);
							((Transform)((Graphic)componentInChildren).rectTransform).localScale = new Vector3((float)touXiangPianYi.SX / 100f, (float)touXiangPianYi.SY / 100f, 1f);
						}
					}
				}
			}
			if (!flag)
			{
				if ((Object)(object)BaseImage != (Object)null)
				{
					BaseImage.SetActive(false);
				}
				if ((Object)(object)BaseSpine != (Object)null)
				{
					BaseSpine.SetActive(true);
				}
				setFaceByJson(randomInfo);
			}
		}
		catch (Exception ex)
		{
			Debug.LogError((object)$"设置形象时出错，ID:{monstarID}，错误:{ex.Message}\n{ex.StackTrace}");
			throw ex;
		}
	}

	public void setFaceByJson(JSONObject randomInfo, int type = 0)
	{
		//IL_01bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d8: Unknown result type (might be due to invalid IL or missing references)
		SkeletonGraphic component = ((Component)this).GetComponent<SkeletonGraphic>();
		if ((Object)(object)component == (Object)null)
		{
			((Component)this).GetComponent<SkeletonRenderer>().Initialize(true);
			{
				foreach (JSONObject item in jsonData.instance.SuiJiTouXiangGeShuJsonData.list)
				{
					List<int> suijiList = jsonData.instance.getSuijiList(item["StrID"].str, "Sex" + randomInfo["Sex"].I);
					if (suijiList.Count != 0 && randomInfo.HasField(item["StrID"].str))
					{
						int value = suijiList[randomInfo[item["StrID"].str].I % suijiList.Count];
						if (suijiList.Contains(randomInfo[item["StrID"].str].I))
						{
							value = randomInfo[item["StrID"].str].I;
						}
						ChangeSlot(item["StrID"].str, value, randomInfo);
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
				component.skeletonDataAsset = SkeletonDataAsset[index];
				component.initialSkinName = "0";
				if (isTalk)
				{
					component.startingAnimation = "talk_0";
				}
				component.Initialize(true);
				((Component)((Component)this).transform).GetComponent<RectTransform>().anchoredPosition = Vector2.op_Implicit(setPosition[index]);
				((Component)this).transform.localScale = setScale[index];
			}
		}
		catch
		{
			Debug.Log((object)"");
		}
		foreach (JSONObject item2 in jsonData.instance.SuiJiTouXiangGeShuJsonData.list)
		{
			List<int> suijiList2 = jsonData.instance.getSuijiList(item2["StrID"].str, "Sex" + randomInfo["Sex"].I);
			if (suijiList2.Count != 0 && (type != 1 || randomInfo.HasField(item2["StrID"].str)))
			{
				int value2 = suijiList2[randomInfo[item2["StrID"].str].I % suijiList2.Count];
				if (suijiList2.Contains(randomInfo[item2["StrID"].str].I))
				{
					value2 = randomInfo[item2["StrID"].str].I;
				}
				CanvasChangeSlot(item2["StrID"].str, value2, randomInfo);
			}
		}
	}

	private void CanvasChangeSlot(string unit, int value, JSONObject randomInfo)
	{
		CanvasChangeSlot(unit + "_" + value, randomInfo);
	}

	public void CanvasChangeSlot(string unit, JSONObject randomInfo)
	{
		SkeletonGraphic component = ((Component)this).GetComponent<SkeletonGraphic>();
		Skin val = component.Skeleton.Data.FindSkin(unit);
		if (val != null)
		{
			setAttach(component, val, randomInfo);
		}
	}

	public void ChangeSlot(string unit, int value, JSONObject randomInfo)
	{
		SkeletonRenderer component = ((Component)this).GetComponent<SkeletonRenderer>();
		Skin val = component.Skeleton.Data.FindSkin(unit + "_" + value);
		if (val != null)
		{
			setAttach(component, val, randomInfo);
		}
	}

	public int getHairColor(int random, string Na)
	{
		return 256 - (int)jsonData.instance.HairRandomColorJsonData[random][Na].n;
	}

	public int getRandomColor(JSONObject aa, int random, string Na)
	{
		return 256 - (int)aa[random][Na].n;
	}

	public void setAttach(SkeletonRenderer skeletonRenderer, Skin skin, JSONObject randomInfo)
	{
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Unknown result type (might be due to invalid IL or missing references)
		//IL_017b: Unknown result type (might be due to invalid IL or missing references)
		foreach (KeyValuePair<AttachmentKeyTuple, Attachment> attachment2 in skeletonRenderer.Skeleton.Skin.Attachments)
		{
			int slotIndex = attachment2.Key.slotIndex;
			Slot val = skeletonRenderer.Skeleton.Slots.Items[slotIndex];
			if (val.Attachment == attachment2.Value)
			{
				if ((Object)(object)jsonData.instance != (Object)null)
				{
					setRandomColor("hairColorR", attachment2.Key.name.Contains("hair"), randomInfo, val, jsonData.instance.HairRandomColorJsonData);
					setRandomColor("mouthColor", attachment2.Key.name.Contains("wouth"), randomInfo, val, jsonData.instance.MouthRandomColorJsonData);
					setRandomColor("blushColor", attachment2.Key.name.Contains("blush"), randomInfo, val, jsonData.instance.SaiHonRandomColorJsonData);
					setRandomColor("yanzhuColor", attachment2.Key.name.Contains("yanqiu"), randomInfo, val, jsonData.instance.YanZhuYanSeRandomColorJsonData);
					setRandomColor("tezhengColor", attachment2.Key.name.Contains("characteristic"), randomInfo, val, jsonData.instance.MianWenYanSeRandomColorJsonData);
					setRandomColor("eyebrowColor", attachment2.Key.name.Contains("eyebrow"), randomInfo, val, jsonData.instance.MeiMaoYanSeRandomColorJsonData);
					setRandomColor("tezhengColor", attachment2.Key.name.Contains("feature"), randomInfo, val, jsonData.instance.MianWenYanSeRandomColorJsonData);
				}
				Attachment attachment = skin.GetAttachment(slotIndex, attachment2.Key.name);
				if (attachment != null)
				{
					val.Attachment = attachment;
				}
			}
		}
	}

	public void setAttach(SkeletonGraphic skeletonRenderer, Skin skin, JSONObject randomInfo)
	{
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Unknown result type (might be due to invalid IL or missing references)
		//IL_017b: Unknown result type (might be due to invalid IL or missing references)
		foreach (KeyValuePair<AttachmentKeyTuple, Attachment> attachment2 in skeletonRenderer.Skeleton.Skin.Attachments)
		{
			int slotIndex = attachment2.Key.slotIndex;
			Slot val = skeletonRenderer.Skeleton.Slots.Items[slotIndex];
			if (val.Attachment == attachment2.Value)
			{
				if ((Object)(object)jsonData.instance != (Object)null)
				{
					setRandomColor("hairColorR", attachment2.Key.name.Contains("hair"), randomInfo, val, jsonData.instance.HairRandomColorJsonData);
					setRandomColor("mouthColor", attachment2.Key.name.Contains("wouth"), randomInfo, val, jsonData.instance.MouthRandomColorJsonData);
					setRandomColor("blushColor", attachment2.Key.name.Contains("blush"), randomInfo, val, jsonData.instance.SaiHonRandomColorJsonData);
					setRandomColor("yanzhuColor", attachment2.Key.name.Contains("yanqiu"), randomInfo, val, jsonData.instance.YanZhuYanSeRandomColorJsonData);
					setRandomColor("tezhengColor", attachment2.Key.name.Contains("characteristic"), randomInfo, val, jsonData.instance.MianWenYanSeRandomColorJsonData);
					setRandomColor("eyebrowColor", attachment2.Key.name.Contains("eyebrow"), randomInfo, val, jsonData.instance.MeiMaoYanSeRandomColorJsonData);
					setRandomColor("tezhengColor", attachment2.Key.name.Contains("feature"), randomInfo, val, jsonData.instance.MianWenYanSeRandomColorJsonData);
				}
				Attachment attachment = skin.GetAttachment(slotIndex, attachment2.Key.name);
				if (attachment != null)
				{
					val.Attachment = attachment;
				}
			}
		}
	}

	public void setRandomColor(string ColorName, bool hasName, JSONObject randomInfo, Slot slot, JSONObject color)
	{
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		if (hasName && randomInfo != null && (Object)(object)jsonData.instance != (Object)null)
		{
			try
			{
				int random = (int)randomInfo[ColorName].n % color.Count;
				int num = getRandomColor(color, random, "R");
				int num2 = getRandomColor(color, random, "G");
				int num3 = getRandomColor(color, random, "B");
				SkeletonExtensions.SetColor(slot, new Color((float)num, (float)num2, (float)num3));
			}
			catch (Exception)
			{
				SkeletonExtensions.SetColor(slot, new Color(1f, 1f, 1f));
			}
		}
	}
}
