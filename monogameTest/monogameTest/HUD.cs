using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace monogameTest
{
	public class HUD
	{
		public int playerScore, screenWidth, screenHeight;
		public SpriteFont scoreFont;
		public Vector2 scorePos;

		public HUD ()
		{
			playerScore = 0;
			screenHeight = 640;
			screenWidth = 480;
			scoreFont = null;
			scorePos = new Vector2 (screenWidth / 2, 10);
		}

		public void LoadContent(ContentManager Content)
		{
			scoreFont = Content.Load<SpriteFont> ("Decker");
		}

		public void Update(GameTime gameTime)
		{
			
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.DrawString (scoreFont, "Score : " + playerScore, scorePos, Color.White);
		}
	}
}

