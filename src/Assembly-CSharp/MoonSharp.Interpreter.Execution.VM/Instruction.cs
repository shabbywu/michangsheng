using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MoonSharp.Interpreter.Debugging;

namespace MoonSharp.Interpreter.Execution.VM;

internal class Instruction
{
	internal OpCode OpCode;

	internal SymbolRef Symbol;

	internal SymbolRef[] SymbolList;

	internal string Name;

	internal DynValue Value;

	internal int NumVal;

	internal int NumVal2;

	internal SourceRef SourceCodeRef;

	internal Instruction(SourceRef sourceref)
	{
		SourceCodeRef = sourceref;
	}

	public override string ToString()
	{
		string text = OpCode.ToString().ToUpperInvariant();
		int fieldUsage = (int)OpCode.GetFieldUsage();
		if (fieldUsage != 0)
		{
			text += GenSpaces();
		}
		if (OpCode == OpCode.Meta || (fieldUsage & 0x8010) == 32784)
		{
			text = text + " " + NumVal.ToString("X8");
		}
		else if (((uint)fieldUsage & 0x10u) != 0)
		{
			text = text + " " + NumVal;
		}
		if (((uint)fieldUsage & 0x20u) != 0)
		{
			text = text + " " + NumVal2;
		}
		if (((uint)fieldUsage & 4u) != 0)
		{
			text = text + " " + Name;
		}
		if (((uint)fieldUsage & 8u) != 0)
		{
			text = text + " " + PurifyFromNewLines(Value);
		}
		if (((uint)fieldUsage & (true ? 1u : 0u)) != 0)
		{
			text = text + " " + Symbol;
		}
		if (((uint)fieldUsage & 2u) != 0 && SymbolList != null)
		{
			text = text + " " + string.Join(",", SymbolList.Select((SymbolRef s) => s.ToString()).ToArray());
		}
		return text;
	}

	private string PurifyFromNewLines(DynValue Value)
	{
		if (Value == null)
		{
			return "";
		}
		return Value.ToString().Replace('\n', ' ').Replace('\r', ' ');
	}

	private string GenSpaces()
	{
		return new string(' ', 10 - OpCode.ToString().Length);
	}

	internal void WriteBinary(BinaryWriter wr, int baseAddress, Dictionary<SymbolRef, int> symbolMap)
	{
		wr.Write((byte)OpCode);
		int fieldUsage = (int)OpCode.GetFieldUsage();
		if ((fieldUsage & 0x8010) == 32784)
		{
			wr.Write(NumVal - baseAddress);
		}
		else if (((uint)fieldUsage & 0x10u) != 0)
		{
			wr.Write(NumVal);
		}
		if (((uint)fieldUsage & 0x20u) != 0)
		{
			wr.Write(NumVal2);
		}
		if (((uint)fieldUsage & 4u) != 0)
		{
			wr.Write(Name ?? "");
		}
		if (((uint)fieldUsage & 8u) != 0)
		{
			DumpValue(wr, Value);
		}
		if (((uint)fieldUsage & (true ? 1u : 0u)) != 0)
		{
			WriteSymbol(wr, Symbol, symbolMap);
		}
		if (((uint)fieldUsage & 2u) != 0)
		{
			wr.Write(SymbolList.Length);
			for (int i = 0; i < SymbolList.Length; i++)
			{
				WriteSymbol(wr, SymbolList[i], symbolMap);
			}
		}
	}

	private static void WriteSymbol(BinaryWriter wr, SymbolRef symbolRef, Dictionary<SymbolRef, int> symbolMap)
	{
		int value = ((symbolRef == null) ? (-1) : symbolMap[symbolRef]);
		wr.Write(value);
	}

	private static SymbolRef ReadSymbol(BinaryReader rd, SymbolRef[] deserializedSymbols)
	{
		int num = rd.ReadInt32();
		if (num < 0)
		{
			return null;
		}
		return deserializedSymbols[num];
	}

	internal static Instruction ReadBinary(SourceRef chunkRef, BinaryReader rd, int baseAddress, Table envTable, SymbolRef[] deserializedSymbols)
	{
		Instruction instruction = new Instruction(chunkRef);
		instruction.OpCode = (OpCode)rd.ReadByte();
		int fieldUsage = (int)instruction.OpCode.GetFieldUsage();
		if ((fieldUsage & 0x8010) == 32784)
		{
			instruction.NumVal = rd.ReadInt32() + baseAddress;
		}
		else if (((uint)fieldUsage & 0x10u) != 0)
		{
			instruction.NumVal = rd.ReadInt32();
		}
		if (((uint)fieldUsage & 0x20u) != 0)
		{
			instruction.NumVal2 = rd.ReadInt32();
		}
		if (((uint)fieldUsage & 4u) != 0)
		{
			instruction.Name = rd.ReadString();
		}
		if (((uint)fieldUsage & 8u) != 0)
		{
			instruction.Value = ReadValue(rd, envTable);
		}
		if (((uint)fieldUsage & (true ? 1u : 0u)) != 0)
		{
			instruction.Symbol = ReadSymbol(rd, deserializedSymbols);
		}
		if (((uint)fieldUsage & 2u) != 0)
		{
			int num = rd.ReadInt32();
			instruction.SymbolList = new SymbolRef[num];
			for (int i = 0; i < instruction.SymbolList.Length; i++)
			{
				instruction.SymbolList[i] = ReadSymbol(rd, deserializedSymbols);
			}
		}
		return instruction;
	}

	private static DynValue ReadValue(BinaryReader rd, Table envTable)
	{
		if (!rd.ReadBoolean())
		{
			return null;
		}
		DataType dataType = (DataType)rd.ReadByte();
		return dataType switch
		{
			DataType.Nil => DynValue.NewNil(), 
			DataType.Void => DynValue.Void, 
			DataType.Boolean => DynValue.NewBoolean(rd.ReadBoolean()), 
			DataType.Number => DynValue.NewNumber(rd.ReadDouble()), 
			DataType.String => DynValue.NewString(rd.ReadString()), 
			DataType.Table => DynValue.NewTable(envTable), 
			_ => throw new NotSupportedException($"Unsupported type in chunk dump : {dataType}"), 
		};
	}

	private void DumpValue(BinaryWriter wr, DynValue value)
	{
		if (value == null)
		{
			wr.Write(value: false);
			return;
		}
		wr.Write(value: true);
		wr.Write((byte)value.Type);
		switch (value.Type)
		{
		case DataType.Boolean:
			wr.Write(value.Boolean);
			break;
		case DataType.Number:
			wr.Write(value.Number);
			break;
		case DataType.String:
			wr.Write(value.String);
			break;
		default:
			throw new NotSupportedException($"Unsupported type in chunk dump : {value.Type}");
		case DataType.Nil:
		case DataType.Void:
		case DataType.Table:
			break;
		}
	}

	internal void GetSymbolReferences(out SymbolRef[] symbolList, out SymbolRef symbol)
	{
		InstructionFieldUsage fieldUsage = OpCode.GetFieldUsage();
		symbol = null;
		symbolList = null;
		if ((fieldUsage & InstructionFieldUsage.Symbol) != 0)
		{
			symbol = Symbol;
		}
		if ((fieldUsage & InstructionFieldUsage.SymbolList) != 0)
		{
			symbolList = SymbolList;
		}
	}
}
