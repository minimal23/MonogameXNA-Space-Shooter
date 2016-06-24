using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace monogameTest
{
	public class Enemy
	{
		public Texture2D texture, bulletTexture;
		public Rectangle boundingBox;
		public Vector2 position;
		public int speed, hp, bulletDelay;
		public bool isVisible;
		public List<Bullet> bulletList;

		public Enemy (Texture2D newTexture, Vector2 newPos, Texture2D newBulletTexture)
		{
			bulletList = new List<Bullet>();
			texture = newTexture;
			position = newPos;
			bulletTexture = newBulletTexture;
			hp = 100;
			bulletDelay = 0;
			speed = 5;
			isVisible = true;
		}

		public void Update(GameTime gameTime)
		{
			boundingBox = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
			position.Y += speed;
			if (position.Y >= 640)
				isVisible = false;
			EnemyShoot();
			UpdateBullet();
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			foreach (Bullet b in bulletList)
				b.Draw(spriteBatch);
			spriteBatch.Draw(texture, position, Color.White);
		}

		public void UpdateBullet()
		{
			foreach (Bullet b in bulletList)
			{
				b.boundingBox = new Rectangle((int)b.position.X, (int)b.position.Y, b.texture.Width, b.texture.Height);
				b.position.Y += 7;
				if (b.position.Y >= 640)
					b.isVisible = false;
			}

			for (int i = 0; i < bulletList.Count; i++)
			{
				if (!bulletList[i].isVisible)
				{
					bulletList.RemoveAt(i);
					i--;
				}
			}
		}

		public void EnemyShoot()
		{
			if (bulletDelay >= 0)
				bulletDelay--;
			if (bulletDelay == 0)
			{
				Bullet newBullet = new Bullet(bulletTexture);
				newBullet.position = new Vector2(position.X + texture.Width / 2 - newBullet.texture.Width / 2, position.Y + 3*texture.Height / 4);
				newBullet.isVisible = true;
				if (bulletList.Count < 20)
					bulletList.Add(newBullet);
			}
			if (bulletDelay < 0)
				bulletDelay = 70;
		}
	}
}

