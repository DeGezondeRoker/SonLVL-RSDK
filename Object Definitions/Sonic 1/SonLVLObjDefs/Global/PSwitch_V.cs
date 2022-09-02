using SonicRetro.SonLVL.API;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S1ObjectDefinitions.Global
{
	class PSwitch_V : ObjectDefinition
	{
		private PropertySpec[] properties;
		private readonly Sprite[] sprites = new Sprite[17];
		
		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new List<byte>()); }
		}

		public override void Init(ObjectData data)
		{
			BitmapBits sheet = LevelData.GetSpriteSheet("Global/Display.gif");
			
			int index = 0;
			for (int i = 0; i < 8; i++)
			{
				sprites[index] = new Sprite(sheet.GetSection(46 + (i * 17), 158, 16, 16), -8, -8);
				index++;
			}
			
			for (int i = 0; i < 8; i++)
			{
				sprites[index] = new Sprite(sheet.GetSection(46 + (i * 17), 175, 16, 16), -8, -8);
				index++;
			}
			
			sprites[16] = new Sprite(sheet.GetSection(182, 141, 16, 16), -8, -8);
			
			properties = new PropertySpec[6];
			properties[0] = new PropertySpec("Size", typeof(int), "Extended",
				"How tall the Plane Switch will be.", null, new Dictionary<string, int>
				{
					{ "4 Nodes", 0 },
					{ "8 Nodes", 1 },
					{ "16 Nodes", 2 },
					{ "32 Nodes", 3 }
				},
				(obj) => obj.PropertyValue & 3,
				(obj, value) => obj.PropertyValue = (byte)((obj.PropertyValue & 252) | (byte)((int)value)));
			
			properties[1] = new PropertySpec("Left Collision Plane", typeof(int), "Extended",
				"Which plane is to the left.", null, new Dictionary<string, int>
				{
					{ "Plane A", 0 },
					{ "Plane B", 4 }
				},
				(obj) => obj.PropertyValue & 4,
				(obj, value) => obj.PropertyValue = (byte)((obj.PropertyValue & 251) | (byte)((int)value)));
			
			properties[2] = new PropertySpec("Right Collision Plane", typeof(int), "Extended",
				"Which plane is to the right.", null, new Dictionary<string, int>
				{
					{ "Plane A", 0 },
					{ "Plane B", 8 }
				},
				(obj) => obj.PropertyValue & 8,
				(obj, value) => obj.PropertyValue = (byte)((obj.PropertyValue & 247) | (byte)((int)value)));
			
			properties[3] = new PropertySpec("Left Draw Order", typeof(int), "Extended",
				"Which draw layer is to the left.", null, new Dictionary<string, int>
				{
					{ "Low Layer", 0 },
					{ "High Layer", 16 }
				},
				(obj) => obj.PropertyValue & 16,
				(obj, value) => obj.PropertyValue = (byte)((obj.PropertyValue & 239) | (byte)((int)value)));
			
			properties[4] = new PropertySpec("Right Draw Order", typeof(int), "Extended",
				"Which draw layer is to the right.", null, new Dictionary<string, int>
				{
					{ "Low Layer", 0 },
					{ "High Layer", 32 }
				},
				(obj) => obj.PropertyValue & 32,
				(obj, value) => obj.PropertyValue = (byte)((obj.PropertyValue & 223) | (byte)((int)value)));
			
			properties[5] = new PropertySpec("Grounded", typeof(int), "Extended",
				"If only grounded players should be affected.", null, new Dictionary<string, int>
				{
					{ "False", 0 },
					{ "True", 128 }
				},
				(obj) => obj.PropertyValue & 128,
				(obj, value) => obj.PropertyValue = (byte)((obj.PropertyValue & 127) | (byte)((int)value)));
		}
		
		public override byte DefaultSubtype
		{
			get { return 0; }
		}
		
		public override PropertySpec[] CustomProperties
		{
			get { return properties; }
		}

		public override string SubtypeName(byte subtype)
		{
			return null;
		}

		public override Sprite Image
		{
			get { return sprites[16]; }
		}

		public override Sprite SubtypeImage(byte subtype)
		{
			return sprites[16];
		}

		public override Sprite GetSprite(ObjectEntry obj)
		{
			int count = Math.Max((1 << ((obj.PropertyValue & 3) + 2)), 1);
			int sy = -(((count) * 16) / 2) + 8;
			List<Sprite> sprs = new List<Sprite>();
			for (int i = 0; i < count; i++)
			{
				if (obj.PropertyValue > 127)
				{
					Sprite back = new Sprite(sprites[16]);
					back.Offset(0, sy + (i * 16));
					sprs.Add(back);
				}
				
				Sprite tmp = new Sprite(sprites[(obj.PropertyValue >> 2) & 15]);
				tmp.Offset(0, sy + (i * 16));
				sprs.Add(tmp);
			}
			return new Sprite(sprs.ToArray());
		}
	}
}