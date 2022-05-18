using System;
using UnityEngine;

// Token: 0x02000024 RID: 36
public class LTGUI
{
	// Token: 0x06000170 RID: 368 RVA: 0x00005234 File Offset: 0x00003434
	public static void init()
	{
		if (LTGUI.levels == null)
		{
			LTGUI.levels = new LTRect[LTGUI.RECT_LEVELS * LTGUI.RECTS_PER_LEVEL];
			LTGUI.levelDepths = new int[LTGUI.RECT_LEVELS];
		}
	}

	// Token: 0x06000171 RID: 369 RVA: 0x00062098 File Offset: 0x00060298
	public static void initRectCheck()
	{
		if (LTGUI.buttons == null)
		{
			LTGUI.buttons = new Rect[LTGUI.BUTTONS_MAX];
			LTGUI.buttonLevels = new int[LTGUI.BUTTONS_MAX];
			LTGUI.buttonLastFrame = new int[LTGUI.BUTTONS_MAX];
			for (int i = 0; i < LTGUI.buttonLevels.Length; i++)
			{
				LTGUI.buttonLevels[i] = -1;
			}
		}
	}

	// Token: 0x06000172 RID: 370 RVA: 0x000620F4 File Offset: 0x000602F4
	public static void reset()
	{
		if (LTGUI.isGUIEnabled)
		{
			LTGUI.isGUIEnabled = false;
			for (int i = 0; i < LTGUI.levels.Length; i++)
			{
				LTGUI.levels[i] = null;
			}
			for (int j = 0; j < LTGUI.levelDepths.Length; j++)
			{
				LTGUI.levelDepths[j] = 0;
			}
		}
	}

	// Token: 0x06000173 RID: 371 RVA: 0x00062144 File Offset: 0x00060344
	public static void update(int updateLevel)
	{
		if (LTGUI.isGUIEnabled)
		{
			LTGUI.init();
			if (LTGUI.levelDepths[updateLevel] > 0)
			{
				LTGUI.color = GUI.color;
				int num = updateLevel * LTGUI.RECTS_PER_LEVEL;
				int num2 = num + LTGUI.levelDepths[updateLevel];
				for (int i = num; i < num2; i++)
				{
					LTGUI.r = LTGUI.levels[i];
					if (LTGUI.r != null)
					{
						if (LTGUI.r.useColor)
						{
							GUI.color = LTGUI.r.color;
						}
						if (LTGUI.r.type == LTGUI.Element_Type.Label)
						{
							if (LTGUI.r.style != null)
							{
								GUI.skin.label = LTGUI.r.style;
							}
							if (LTGUI.r.useSimpleScale)
							{
								GUI.Label(new Rect((LTGUI.r.rect.x + LTGUI.r.margin.x + LTGUI.r.relativeRect.x) * LTGUI.r.relativeRect.width, (LTGUI.r.rect.y + LTGUI.r.margin.y + LTGUI.r.relativeRect.y) * LTGUI.r.relativeRect.height, LTGUI.r.rect.width * LTGUI.r.relativeRect.width, LTGUI.r.rect.height * LTGUI.r.relativeRect.height), LTGUI.r.labelStr);
							}
							else
							{
								GUI.Label(new Rect(LTGUI.r.rect.x + LTGUI.r.margin.x, LTGUI.r.rect.y + LTGUI.r.margin.y, LTGUI.r.rect.width, LTGUI.r.rect.height), LTGUI.r.labelStr);
							}
						}
						else if (LTGUI.r.type == LTGUI.Element_Type.Texture && LTGUI.r.texture != null)
						{
							Vector2 vector = LTGUI.r.useSimpleScale ? new Vector2(0f, LTGUI.r.rect.height * LTGUI.r.relativeRect.height) : new Vector2(LTGUI.r.rect.width, LTGUI.r.rect.height);
							if (LTGUI.r.sizeByHeight)
							{
								vector.x = (float)LTGUI.r.texture.width / (float)LTGUI.r.texture.height * vector.y;
							}
							if (LTGUI.r.useSimpleScale)
							{
								GUI.DrawTexture(new Rect((LTGUI.r.rect.x + LTGUI.r.margin.x + LTGUI.r.relativeRect.x) * LTGUI.r.relativeRect.width, (LTGUI.r.rect.y + LTGUI.r.margin.y + LTGUI.r.relativeRect.y) * LTGUI.r.relativeRect.height, vector.x, vector.y), LTGUI.r.texture);
							}
							else
							{
								GUI.DrawTexture(new Rect(LTGUI.r.rect.x + LTGUI.r.margin.x, LTGUI.r.rect.y + LTGUI.r.margin.y, vector.x, vector.y), LTGUI.r.texture);
							}
						}
					}
				}
				GUI.color = LTGUI.color;
			}
		}
	}

	// Token: 0x06000174 RID: 372 RVA: 0x00062554 File Offset: 0x00060754
	public static bool checkOnScreen(Rect rect)
	{
		bool flag = rect.x + rect.width < 0f;
		bool flag2 = rect.x > (float)Screen.width;
		bool flag3 = rect.y > (float)Screen.height;
		bool flag4 = rect.y + rect.height < 0f;
		return !flag && !flag2 && !flag3 && !flag4;
	}

	// Token: 0x06000175 RID: 373 RVA: 0x000625B8 File Offset: 0x000607B8
	public static void destroy(int id)
	{
		int num = id & 65535;
		int num2 = id >> 16;
		if (id >= 0 && LTGUI.levels[num] != null && LTGUI.levels[num].hasInitiliazed && LTGUI.levels[num].counter == num2)
		{
			LTGUI.levels[num] = null;
		}
	}

	// Token: 0x06000176 RID: 374 RVA: 0x00062604 File Offset: 0x00060804
	public static void destroyAll(int depth)
	{
		int num = depth * LTGUI.RECTS_PER_LEVEL + LTGUI.RECTS_PER_LEVEL;
		int num2 = depth * LTGUI.RECTS_PER_LEVEL;
		while (LTGUI.levels != null && num2 < num)
		{
			LTGUI.levels[num2] = null;
			num2++;
		}
	}

	// Token: 0x06000177 RID: 375 RVA: 0x00005261 File Offset: 0x00003461
	public static LTRect label(Rect rect, string label, int depth)
	{
		return LTGUI.label(new LTRect(rect), label, depth);
	}

	// Token: 0x06000178 RID: 376 RVA: 0x00005270 File Offset: 0x00003470
	public static LTRect label(LTRect rect, string label, int depth)
	{
		rect.type = LTGUI.Element_Type.Label;
		rect.labelStr = label;
		return LTGUI.element(rect, depth);
	}

	// Token: 0x06000179 RID: 377 RVA: 0x00005287 File Offset: 0x00003487
	public static LTRect texture(Rect rect, Texture texture, int depth)
	{
		return LTGUI.texture(new LTRect(rect), texture, depth);
	}

	// Token: 0x0600017A RID: 378 RVA: 0x00005296 File Offset: 0x00003496
	public static LTRect texture(LTRect rect, Texture texture, int depth)
	{
		rect.type = LTGUI.Element_Type.Texture;
		rect.texture = texture;
		return LTGUI.element(rect, depth);
	}

	// Token: 0x0600017B RID: 379 RVA: 0x00062640 File Offset: 0x00060840
	public static LTRect element(LTRect rect, int depth)
	{
		LTGUI.isGUIEnabled = true;
		LTGUI.init();
		int num = depth * LTGUI.RECTS_PER_LEVEL + LTGUI.RECTS_PER_LEVEL;
		int num2 = 0;
		if (rect != null)
		{
			LTGUI.destroy(rect.id);
		}
		if (rect.type == LTGUI.Element_Type.Label && rect.style != null && rect.style.normal.textColor.a <= 0f)
		{
			Debug.LogWarning("Your GUI normal color has an alpha of zero, and will not be rendered.");
		}
		if (rect.relativeRect.width == float.PositiveInfinity)
		{
			rect.relativeRect = new Rect(0f, 0f, (float)Screen.width, (float)Screen.height);
		}
		for (int i = depth * LTGUI.RECTS_PER_LEVEL; i < num; i++)
		{
			LTGUI.r = LTGUI.levels[i];
			if (LTGUI.r == null)
			{
				LTGUI.r = rect;
				LTGUI.r.rotateEnabled = true;
				LTGUI.r.alphaEnabled = true;
				LTGUI.r.setId(i, LTGUI.global_counter);
				LTGUI.levels[i] = LTGUI.r;
				if (num2 >= LTGUI.levelDepths[depth])
				{
					LTGUI.levelDepths[depth] = num2 + 1;
				}
				LTGUI.global_counter++;
				return LTGUI.r;
			}
			num2++;
		}
		Debug.LogError("You ran out of GUI Element spaces");
		return null;
	}

	// Token: 0x0600017C RID: 380 RVA: 0x00062774 File Offset: 0x00060974
	public static bool hasNoOverlap(Rect rect, int depth)
	{
		LTGUI.initRectCheck();
		bool result = true;
		bool flag = false;
		for (int i = 0; i < LTGUI.buttonLevels.Length; i++)
		{
			if (LTGUI.buttonLevels[i] >= 0)
			{
				if (LTGUI.buttonLastFrame[i] + 1 < Time.frameCount)
				{
					LTGUI.buttonLevels[i] = -1;
				}
				else if (LTGUI.buttonLevels[i] > depth && LTGUI.pressedWithinRect(LTGUI.buttons[i]))
				{
					result = false;
				}
			}
			if (!flag && LTGUI.buttonLevels[i] < 0)
			{
				flag = true;
				LTGUI.buttonLevels[i] = depth;
				LTGUI.buttons[i] = rect;
				LTGUI.buttonLastFrame[i] = Time.frameCount;
			}
		}
		return result;
	}

	// Token: 0x0600017D RID: 381 RVA: 0x00062810 File Offset: 0x00060A10
	public static bool pressedWithinRect(Rect rect)
	{
		Vector2 vector = LTGUI.firstTouch();
		if (vector.x < 0f)
		{
			return false;
		}
		float num = (float)Screen.height - vector.y;
		return vector.x > rect.x && vector.x < rect.x + rect.width && num > rect.y && num < rect.y + rect.height;
	}

	// Token: 0x0600017E RID: 382 RVA: 0x00062884 File Offset: 0x00060A84
	public static bool checkWithinRect(Vector2 vec2, Rect rect)
	{
		vec2.y = (float)Screen.height - vec2.y;
		return vec2.x > rect.x && vec2.x < rect.x + rect.width && vec2.y > rect.y && vec2.y < rect.y + rect.height;
	}

	// Token: 0x0600017F RID: 383 RVA: 0x000052AD File Offset: 0x000034AD
	public static Vector2 firstTouch()
	{
		if (Input.touchCount > 0)
		{
			return Input.touches[0].position;
		}
		if (Input.GetMouseButton(0))
		{
			return Input.mousePosition;
		}
		return new Vector2(float.NegativeInfinity, float.NegativeInfinity);
	}

	// Token: 0x04000125 RID: 293
	public static int RECT_LEVELS = 5;

	// Token: 0x04000126 RID: 294
	public static int RECTS_PER_LEVEL = 10;

	// Token: 0x04000127 RID: 295
	public static int BUTTONS_MAX = 24;

	// Token: 0x04000128 RID: 296
	private static LTRect[] levels;

	// Token: 0x04000129 RID: 297
	private static int[] levelDepths;

	// Token: 0x0400012A RID: 298
	private static Rect[] buttons;

	// Token: 0x0400012B RID: 299
	private static int[] buttonLevels;

	// Token: 0x0400012C RID: 300
	private static int[] buttonLastFrame;

	// Token: 0x0400012D RID: 301
	private static LTRect r;

	// Token: 0x0400012E RID: 302
	private static Color color = Color.white;

	// Token: 0x0400012F RID: 303
	private static bool isGUIEnabled = false;

	// Token: 0x04000130 RID: 304
	private static int global_counter = 0;

	// Token: 0x02000025 RID: 37
	public enum Element_Type
	{
		// Token: 0x04000132 RID: 306
		Texture,
		// Token: 0x04000133 RID: 307
		Label
	}
}
