using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using MoonSharp.Interpreter;
using MoonSharp.Interpreter.Serialization.Json;

namespace MoonSharp.VsCodeDebugger.SDK
{
	// Token: 0x02000DAF RID: 3503
	public abstract class ProtocolServer
	{
		// Token: 0x0600638B RID: 25483 RVA: 0x0027B0AF File Offset: 0x002792AF
		public ProtocolServer()
		{
			this._sequenceNumber = 1;
			this._bodyLength = -1;
			this._rawData = new ByteBuffer();
		}

		// Token: 0x0600638C RID: 25484 RVA: 0x0027B0D0 File Offset: 0x002792D0
		public void ProcessLoop(Stream inputStream, Stream outputStream)
		{
			this._outputStream = outputStream;
			byte[] array = new byte[4096];
			this._stopRequested = false;
			while (!this._stopRequested)
			{
				int num = inputStream.Read(array, 0, array.Length);
				if (num == 0)
				{
					break;
				}
				if (num > 0)
				{
					this._rawData.Append(array, num);
					this.ProcessData();
				}
			}
		}

		// Token: 0x0600638D RID: 25485 RVA: 0x0027B126 File Offset: 0x00279326
		public void Stop()
		{
			this._stopRequested = true;
		}

		// Token: 0x0600638E RID: 25486 RVA: 0x0027B12F File Offset: 0x0027932F
		public void SendEvent(Event e)
		{
			this.SendMessage(e);
		}

		// Token: 0x0600638F RID: 25487
		protected abstract void DispatchRequest(string command, Table args, Response response);

		// Token: 0x06006390 RID: 25488 RVA: 0x0027B138 File Offset: 0x00279338
		private void ProcessData()
		{
			for (;;)
			{
				if (this._bodyLength >= 0)
				{
					if (this._rawData.Length < this._bodyLength)
					{
						break;
					}
					byte[] bytes = this._rawData.RemoveFirst(this._bodyLength);
					this._bodyLength = -1;
					this.Dispatch(ProtocolServer.Encoding.GetString(bytes));
				}
				else
				{
					string @string = this._rawData.GetString(ProtocolServer.Encoding);
					int num = @string.IndexOf("\r\n\r\n");
					if (num == -1)
					{
						break;
					}
					Match match = ProtocolServer.CONTENT_LENGTH_MATCHER.Match(@string);
					if (!match.Success || match.Groups.Count != 2)
					{
						break;
					}
					this._bodyLength = Convert.ToInt32(match.Groups[1].ToString());
					this._rawData.RemoveFirst(num + "\r\n\r\n".Length);
				}
			}
		}

		// Token: 0x06006391 RID: 25489 RVA: 0x0027B20C File Offset: 0x0027940C
		private void Dispatch(string req)
		{
			try
			{
				Table table = JsonTableConverter.JsonToTable(req, null);
				if (table != null && table["type"].ToString() == "request")
				{
					if (this.TRACE)
					{
						Console.Error.WriteLine(string.Format("C {0}: {1}", table["command"], req));
					}
					Response response = new Response(table);
					this.DispatchRequest(table.Get("command").String, table.Get("arguments").Table, response);
					this.SendMessage(response);
				}
			}
			catch
			{
			}
		}

		// Token: 0x06006392 RID: 25490 RVA: 0x0027B2B4 File Offset: 0x002794B4
		protected void SendMessage(ProtocolMessage message)
		{
			int sequenceNumber = this._sequenceNumber;
			this._sequenceNumber = sequenceNumber + 1;
			message.seq = sequenceNumber;
			if (this.TRACE_RESPONSE && message.type == "response")
			{
				Console.Error.WriteLine(string.Format(" R: {0}", JsonTableConverter.ObjectToJson(message)));
			}
			if (this.TRACE && message.type == "event")
			{
				Event @event = (Event)message;
				Console.Error.WriteLine(string.Format("E {0}: {1}", @event.@event, JsonTableConverter.ObjectToJson(@event.body)));
			}
			byte[] array = ProtocolServer.ConvertToBytes(message);
			try
			{
				this._outputStream.Write(array, 0, array.Length);
				this._outputStream.Flush();
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x06006393 RID: 25491 RVA: 0x0027B38C File Offset: 0x0027958C
		private static byte[] ConvertToBytes(ProtocolMessage request)
		{
			string s = JsonTableConverter.ObjectToJson(request);
			byte[] bytes = ProtocolServer.Encoding.GetBytes(s);
			string s2 = string.Format("Content-Length: {0}{1}", bytes.Length, "\r\n\r\n");
			byte[] bytes2 = ProtocolServer.Encoding.GetBytes(s2);
			byte[] array = new byte[bytes2.Length + bytes.Length];
			Buffer.BlockCopy(bytes2, 0, array, 0, bytes2.Length);
			Buffer.BlockCopy(bytes, 0, array, bytes2.Length, bytes.Length);
			return array;
		}

		// Token: 0x040055E3 RID: 21987
		public bool TRACE;

		// Token: 0x040055E4 RID: 21988
		public bool TRACE_RESPONSE;

		// Token: 0x040055E5 RID: 21989
		protected const int BUFFER_SIZE = 4096;

		// Token: 0x040055E6 RID: 21990
		protected const string TWO_CRLF = "\r\n\r\n";

		// Token: 0x040055E7 RID: 21991
		protected static readonly Regex CONTENT_LENGTH_MATCHER = new Regex("Content-Length: (\\d+)");

		// Token: 0x040055E8 RID: 21992
		protected static readonly Encoding Encoding = Encoding.UTF8;

		// Token: 0x040055E9 RID: 21993
		private int _sequenceNumber;

		// Token: 0x040055EA RID: 21994
		private Stream _outputStream;

		// Token: 0x040055EB RID: 21995
		private ByteBuffer _rawData;

		// Token: 0x040055EC RID: 21996
		private int _bodyLength;

		// Token: 0x040055ED RID: 21997
		private bool _stopRequested;
	}
}
