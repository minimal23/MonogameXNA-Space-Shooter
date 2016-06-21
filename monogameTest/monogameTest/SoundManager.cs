using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace monogameTest
{
	public class SoundManager
	{
		public SoundEffect pShootSound, explosionSound;
		public Song bgM;

		public SoundManager ()
		{
			pShootSound = null;
			explosionSound = null;
			bgM = null;
		}

		public void LoadContent (ContentManager Content)
		{
			pShootSound = Content.Load<SoundEffect> ("playershoot");
			explosionSound = Content.Load<SoundEffect> ("explode");
			bgM = Content.Load<Song> ("theme");
		}
	}
}

