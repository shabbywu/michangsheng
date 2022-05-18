using System;
using System.Collections.Generic;
using UnityEngine;

namespace KBEngine
{
	// Token: 0x02000EAD RID: 3757
	public abstract class BuildBase : Entity
	{
		// Token: 0x06005AA0 RID: 23200 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void onBuildIdChanged(int oldValue)
		{
		}

		// Token: 0x06005AA1 RID: 23201 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void onDialogIDChanged(uint oldValue)
		{
		}

		// Token: 0x06005AA2 RID: 23202 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void onEntityNOChanged(uint oldValue)
		{
		}

		// Token: 0x06005AA3 RID: 23203 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void onModelIDChanged(uint oldValue)
		{
		}

		// Token: 0x06005AA4 RID: 23204 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void onModelScaleChanged(byte oldValue)
		{
		}

		// Token: 0x06005AA5 RID: 23205 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void onNameChanged(string oldValue)
		{
		}

		// Token: 0x06005AA6 RID: 23206 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void onUidChanged(uint oldValue)
		{
		}

		// Token: 0x06005AA7 RID: 23207 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void onUtypeChanged(uint oldValue)
		{
		}

		// Token: 0x06005AA8 RID: 23208 RVA: 0x0003FF1C File Offset: 0x0003E11C
		public BuildBase()
		{
		}

		// Token: 0x06005AA9 RID: 23209 RVA: 0x000042DD File Offset: 0x000024DD
		public override void onComponentsEnterworld()
		{
		}

		// Token: 0x06005AAA RID: 23210 RVA: 0x000042DD File Offset: 0x000024DD
		public override void onComponentsLeaveworld()
		{
		}

		// Token: 0x06005AAB RID: 23211 RVA: 0x0003FF37 File Offset: 0x0003E137
		public override void onGetBase()
		{
			this.baseEntityCall = new EntityBaseEntityCall_BuildBase(this.id, this.className);
		}

		// Token: 0x06005AAC RID: 23212 RVA: 0x0003FF50 File Offset: 0x0003E150
		public override void onGetCell()
		{
			this.cellEntityCall = new EntityCellEntityCall_BuildBase(this.id, this.className);
		}

		// Token: 0x06005AAD RID: 23213 RVA: 0x0003FF69 File Offset: 0x0003E169
		public override void onLoseCell()
		{
			this.cellEntityCall = null;
		}

		// Token: 0x06005AAE RID: 23214 RVA: 0x0003FF72 File Offset: 0x0003E172
		public override EntityCall getBaseEntityCall()
		{
			return this.baseEntityCall;
		}

		// Token: 0x06005AAF RID: 23215 RVA: 0x0003FF7A File Offset: 0x0003E17A
		public override EntityCall getCellEntityCall()
		{
			return this.cellEntityCall;
		}

		// Token: 0x06005AB0 RID: 23216 RVA: 0x000042DD File Offset: 0x000024DD
		public override void attachComponents()
		{
		}

		// Token: 0x06005AB1 RID: 23217 RVA: 0x000042DD File Offset: 0x000024DD
		public override void detachComponents()
		{
		}

		// Token: 0x06005AB2 RID: 23218 RVA: 0x0024F4DC File Offset: 0x0024D6DC
		public override void onRemoteMethodCall(MemoryStream stream)
		{
			ScriptModule scriptModule = EntityDef.moduledefs["Build"];
			ushort num;
			if (scriptModule.usePropertyDescrAlias)
			{
				num = (ushort)stream.readUint8();
			}
			else
			{
				num = stream.readUint16();
			}
			ushort key;
			if (scriptModule.useMethodDescrAlias)
			{
				key = (ushort)stream.readUint8();
			}
			else
			{
				key = stream.readUint16();
			}
			if (num == 0)
			{
				Method method = scriptModule.idmethods[key];
				ushort methodUtype = method.methodUtype;
				return;
			}
			ushort properUtype = scriptModule.idpropertys[num].properUtype;
		}

		// Token: 0x06005AB3 RID: 23219 RVA: 0x0024F55C File Offset: 0x0024D75C
		public override void onUpdatePropertys(MemoryStream stream)
		{
			ScriptModule scriptModule = EntityDef.moduledefs["Build"];
			Dictionary<ushort, Property> idpropertys = scriptModule.idpropertys;
			while (stream.length() > 0U)
			{
				ushort num;
				ushort key;
				if (scriptModule.usePropertyDescrAlias)
				{
					num = (ushort)stream.readUint8();
					key = (ushort)stream.readUint8();
				}
				else
				{
					num = stream.readUint16();
					key = stream.readUint16();
				}
				if (num != 0)
				{
					ushort properUtype = idpropertys[num].properUtype;
					return;
				}
				Property property = idpropertys[key];
				ushort properUtype2 = property.properUtype;
				if (properUtype2 <= 102)
				{
					if (properUtype2 != 97)
					{
						if (properUtype2 == 102)
						{
							uint oldValue = this.dialogID;
							this.dialogID = stream.readUint32();
							if (property.isBase())
							{
								if (this.inited)
								{
									this.onDialogIDChanged(oldValue);
								}
							}
							else if (this.inWorld)
							{
								this.onDialogIDChanged(oldValue);
							}
						}
					}
					else
					{
						int buildId = this.BuildId;
						this.BuildId = stream.readInt32();
						if (property.isBase())
						{
							if (this.inited)
							{
								this.onBuildIdChanged(buildId);
							}
						}
						else if (this.inWorld)
						{
							this.onBuildIdChanged(buildId);
						}
					}
				}
				else
				{
					switch (properUtype2)
					{
					case 40000:
					{
						Vector3 position = this.position;
						this.position = stream.readVector3();
						if (property.isBase())
						{
							if (this.inited)
							{
								this.onPositionChanged(position);
							}
						}
						else if (this.inWorld)
						{
							this.onPositionChanged(position);
						}
						break;
					}
					case 40001:
					{
						Vector3 direction = this.direction;
						this.direction = stream.readVector3();
						if (property.isBase())
						{
							if (this.inited)
							{
								this.onDirectionChanged(direction);
							}
						}
						else if (this.inWorld)
						{
							this.onDirectionChanged(direction);
						}
						break;
					}
					case 40002:
						stream.readUint32();
						break;
					default:
						switch (properUtype2)
						{
						case 41003:
						{
							string oldValue2 = this.name;
							this.name = stream.readUnicode();
							if (property.isBase())
							{
								if (this.inited)
								{
									this.onNameChanged(oldValue2);
								}
							}
							else if (this.inWorld)
							{
								this.onNameChanged(oldValue2);
							}
							break;
						}
						case 41004:
						{
							uint oldValue3 = this.uid;
							this.uid = stream.readUint32();
							if (property.isBase())
							{
								if (this.inited)
								{
									this.onUidChanged(oldValue3);
								}
							}
							else if (this.inWorld)
							{
								this.onUidChanged(oldValue3);
							}
							break;
						}
						case 41005:
						{
							uint oldValue4 = this.utype;
							this.utype = stream.readUint32();
							if (property.isBase())
							{
								if (this.inited)
								{
									this.onUtypeChanged(oldValue4);
								}
							}
							else if (this.inWorld)
							{
								this.onUtypeChanged(oldValue4);
							}
							break;
						}
						case 41006:
						{
							uint oldValue5 = this.modelID;
							this.modelID = stream.readUint32();
							if (property.isBase())
							{
								if (this.inited)
								{
									this.onModelIDChanged(oldValue5);
								}
							}
							else if (this.inWorld)
							{
								this.onModelIDChanged(oldValue5);
							}
							break;
						}
						case 41007:
						{
							byte oldValue6 = this.modelScale;
							this.modelScale = stream.readUint8();
							if (property.isBase())
							{
								if (this.inited)
								{
									this.onModelScaleChanged(oldValue6);
								}
							}
							else if (this.inWorld)
							{
								this.onModelScaleChanged(oldValue6);
							}
							break;
						}
						default:
							if (properUtype2 == 51007)
							{
								uint oldValue7 = this.entityNO;
								this.entityNO = stream.readUint32();
								if (property.isBase())
								{
									if (this.inited)
									{
										this.onEntityNOChanged(oldValue7);
									}
								}
								else if (this.inWorld)
								{
									this.onEntityNOChanged(oldValue7);
								}
							}
							break;
						}
						break;
					}
				}
			}
		}

		// Token: 0x06005AB4 RID: 23220 RVA: 0x0024F93C File Offset: 0x0024DB3C
		public override void callPropertysSetMethods()
		{
			Dictionary<ushort, Property> idpropertys = EntityDef.moduledefs["Build"].idpropertys;
			int buildId = this.BuildId;
			Property property = idpropertys[4];
			if (property.isBase())
			{
				if (this.inited && !this.inWorld)
				{
					this.onBuildIdChanged(buildId);
				}
			}
			else if (this.inWorld && (!property.isOwnerOnly() || base.isPlayer()))
			{
				this.onBuildIdChanged(buildId);
			}
			uint oldValue = this.dialogID;
			Property property2 = idpropertys[5];
			if (property2.isBase())
			{
				if (this.inited && !this.inWorld)
				{
					this.onDialogIDChanged(oldValue);
				}
			}
			else if (this.inWorld && (!property2.isOwnerOnly() || base.isPlayer()))
			{
				this.onDialogIDChanged(oldValue);
			}
			Vector3 direction = this.direction;
			Property property3 = idpropertys[2];
			if (property3.isBase())
			{
				if (this.inited && !this.inWorld)
				{
					this.onDirectionChanged(direction);
				}
			}
			else if (this.inWorld && (!property3.isOwnerOnly() || base.isPlayer()))
			{
				this.onDirectionChanged(direction);
			}
			uint oldValue2 = this.entityNO;
			Property property4 = idpropertys[6];
			if (property4.isBase())
			{
				if (this.inited && !this.inWorld)
				{
					this.onEntityNOChanged(oldValue2);
				}
			}
			else if (this.inWorld && (!property4.isOwnerOnly() || base.isPlayer()))
			{
				this.onEntityNOChanged(oldValue2);
			}
			uint oldValue3 = this.modelID;
			Property property5 = idpropertys[7];
			if (property5.isBase())
			{
				if (this.inited && !this.inWorld)
				{
					this.onModelIDChanged(oldValue3);
				}
			}
			else if (this.inWorld && (!property5.isOwnerOnly() || base.isPlayer()))
			{
				this.onModelIDChanged(oldValue3);
			}
			byte oldValue4 = this.modelScale;
			Property property6 = idpropertys[8];
			if (property6.isBase())
			{
				if (this.inited && !this.inWorld)
				{
					this.onModelScaleChanged(oldValue4);
				}
			}
			else if (this.inWorld && (!property6.isOwnerOnly() || base.isPlayer()))
			{
				this.onModelScaleChanged(oldValue4);
			}
			string oldValue5 = this.name;
			Property property7 = idpropertys[9];
			if (property7.isBase())
			{
				if (this.inited && !this.inWorld)
				{
					this.onNameChanged(oldValue5);
				}
			}
			else if (this.inWorld && (!property7.isOwnerOnly() || base.isPlayer()))
			{
				this.onNameChanged(oldValue5);
			}
			Vector3 position = this.position;
			Property property8 = idpropertys[1];
			if (property8.isBase())
			{
				if (this.inited && !this.inWorld)
				{
					this.onPositionChanged(position);
				}
			}
			else if (this.inWorld && (!property8.isOwnerOnly() || base.isPlayer()))
			{
				this.onPositionChanged(position);
			}
			uint oldValue6 = this.uid;
			Property property9 = idpropertys[10];
			if (property9.isBase())
			{
				if (this.inited && !this.inWorld)
				{
					this.onUidChanged(oldValue6);
				}
			}
			else if (this.inWorld && (!property9.isOwnerOnly() || base.isPlayer()))
			{
				this.onUidChanged(oldValue6);
			}
			uint oldValue7 = this.utype;
			Property property10 = idpropertys[11];
			if (property10.isBase())
			{
				if (this.inited && !this.inWorld)
				{
					this.onUtypeChanged(oldValue7);
					return;
				}
			}
			else if (this.inWorld && (!property10.isOwnerOnly() || base.isPlayer()))
			{
				this.onUtypeChanged(oldValue7);
			}
		}

		// Token: 0x040059EC RID: 23020
		public EntityBaseEntityCall_BuildBase baseEntityCall;

		// Token: 0x040059ED RID: 23021
		public EntityCellEntityCall_BuildBase cellEntityCall;

		// Token: 0x040059EE RID: 23022
		public int BuildId;

		// Token: 0x040059EF RID: 23023
		public uint dialogID;

		// Token: 0x040059F0 RID: 23024
		public uint entityNO;

		// Token: 0x040059F1 RID: 23025
		public uint modelID;

		// Token: 0x040059F2 RID: 23026
		public byte modelScale = 30;

		// Token: 0x040059F3 RID: 23027
		public string name = "";

		// Token: 0x040059F4 RID: 23028
		public uint uid;

		// Token: 0x040059F5 RID: 23029
		public uint utype;
	}
}
