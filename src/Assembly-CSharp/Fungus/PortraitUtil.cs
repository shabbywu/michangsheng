using MoonSharp.Interpreter;
using UnityEngine;

namespace Fungus;

public static class PortraitUtil
{
	public static PortraitOptions ConvertTableToPortraitOptions(Table table, Stage stage)
	{
		PortraitOptions portraitOptions = new PortraitOptions();
		portraitOptions.character = table.Get("character").ToObject<Character>() ?? portraitOptions.character;
		portraitOptions.replacedCharacter = table.Get("replacedCharacter").ToObject<Character>() ?? portraitOptions.replacedCharacter;
		if (!table.Get("portrait").IsNil())
		{
			portraitOptions.portrait = portraitOptions.character.GetPortrait(table.Get("portrait").CastToString());
		}
		if (!table.Get("display").IsNil())
		{
			portraitOptions.display = table.Get("display").ToObject<DisplayType>();
		}
		if (!table.Get("offset").IsNil())
		{
			portraitOptions.offset = table.Get("offset").ToObject<PositionOffset>();
		}
		if (!table.Get("fromPosition").IsNil())
		{
			portraitOptions.fromPosition = stage.GetPosition(table.Get("fromPosition").CastToString());
		}
		if (!table.Get("toPosition").IsNil())
		{
			portraitOptions.toPosition = stage.GetPosition(table.Get("toPosition").CastToString());
		}
		if (!table.Get("facing").IsNil())
		{
			FacingDirection facing = FacingDirection.None;
			DynValue dynValue = table.Get("facing");
			if (dynValue.Type == DataType.String)
			{
				if (string.Compare(dynValue.String, "left", ignoreCase: true) == 0)
				{
					facing = FacingDirection.Left;
				}
				else if (string.Compare(dynValue.String, "right", ignoreCase: true) == 0)
				{
					facing = FacingDirection.Right;
				}
			}
			else
			{
				facing = table.Get("facing").ToObject<FacingDirection>();
			}
			portraitOptions.facing = facing;
		}
		if (!table.Get("useDefaultSettings").IsNil())
		{
			portraitOptions.useDefaultSettings = table.Get("useDefaultSettings").CastToBool();
		}
		if (!table.Get("fadeDuration").IsNil())
		{
			portraitOptions.fadeDuration = table.Get("fadeDuration").ToObject<float>();
		}
		if (!table.Get("moveDuration").IsNil())
		{
			portraitOptions.moveDuration = table.Get("moveDuration").ToObject<float>();
		}
		if (!table.Get("move").IsNil())
		{
			portraitOptions.move = table.Get("move").CastToBool();
		}
		else if ((Object)(object)portraitOptions.fromPosition != (Object)(object)portraitOptions.toPosition)
		{
			portraitOptions.move = true;
		}
		if (!table.Get("shiftIntoPlace").IsNil())
		{
			portraitOptions.shiftIntoPlace = table.Get("shiftIntoPlace").CastToBool();
		}
		if (!table.Get("waitUntilFinished").IsNil())
		{
			portraitOptions.waitUntilFinished = table.Get("waitUntilFinished").CastToBool();
		}
		return portraitOptions;
	}
}
