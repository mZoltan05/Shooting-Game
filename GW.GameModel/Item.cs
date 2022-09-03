// <copyright file="Item.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace GW.GameModel
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;

    /// <summary>
    /// Types of Items.
    /// </summary>
    public enum ItemType
    {
        /// <summary>
        /// Crystal enum.
        /// </summary>
        Crystal,

        /// <summary>
        /// Part enum.
        /// </summary>
        Part,

        /// <summary>
        /// Asteroid enum.
        /// </summary>
        Asteroid,

        /// <summary>
        /// Boss Bullet enum.
        /// </summary>
        BossBullet,

        /// <summary>
        /// Enemy Bullet enum.
        /// </summary>
        EnemyBullet,

        /// <summary>
        /// Player Bullet enum.
        /// </summary>
        PlayerBullet,

        /// <summary>
        /// Player Ray enum.
        /// </summary>
        PlayerRay,
    }

    /// <summary>
    /// Dumb items.
    /// </summary>
    public class Item : GameItem
    {
        private ItemType itemType;

        /// <summary>
        /// Initializes a new instance of the <see cref="Item"/> class.
        /// </summary>
        /// <param name="itemType">Type of Item.</param>
        /// <param name="cx">X Position of Item Centre.</param>
        /// <param name="cy">Y Position of Item Centre.</param>
        public Item(ItemType itemType, double cx, double cy)
        {
            this.itemType = itemType;
            this.CX = cx;
            this.CY = cy;

            BitmapImage bmp = new BitmapImage();
            string skin = string.Empty;

            switch (this.itemType)
            {
                case ItemType.Crystal:
                    skin = @"..\..\..\..\Images\Items\Moving_Crystal.png"; this.Area = new Rect(cx, cy, 60, 60); this.DY = 6;
                    break;
                case ItemType.Part:
                    skin = @"..\..\..\..\Images\Items\Moving_Part.png"; this.Area = new Rect(cx, cy, 70, 70); this.DY = 6;
                    break;
                case ItemType.Asteroid:
                    skin = @"..\..\..\..\Images\Items\Asteroid.png"; this.Area = new Rect(cx, cy, 80, 100); this.DY = 4;
                    break;
                case ItemType.BossBullet:
                    skin = @"..\..\..\..\Images\Items\Boss_Bullet.png"; this.Area = new Rect(cx, cy, 15, 25); this.DY = 12;
                    break;
                case ItemType.EnemyBullet:
                    skin = @"..\..\..\..\Images\Items\Enemy_Bullet.png"; this.Area = new Rect(cx, cy, 10, 20); this.DY = 7;
                    break;
                case ItemType.PlayerBullet:
                    skin = @"..\..\..\..\Images\Items\Player_Bullet_1.png"; this.Area = new Rect(cx, cy, 10, 20); this.DY = -7;
                    break;
                case ItemType.PlayerRay:
                    skin = @"..\..\..\..\Images\Items\Player_Ray.png"; this.Area = new Rect(cx, cy, 25, 25); this.DY = -9;
                    break;
             }

            bmp.BeginInit();
            bmp.UriSource = new Uri(skin, UriKind.Relative);
            bmp.EndInit();
            this.Img = new ImageBrush(bmp);
        }

        /// <summary>
        /// Gets type of item.
        /// </summary>
        public ItemType ItemType
        {
            get { return this.itemType; }
        }
    }
}