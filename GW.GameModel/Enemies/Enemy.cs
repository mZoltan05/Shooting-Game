// <copyright file="Enemy.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace GW.GameModel
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;

    /// <summary>
    /// Types of Enemies.
    /// </summary>
    public enum EnemyType
    {
        /// <summary>
        /// Cutter enum.
        /// </summary>
        Cutter,

        /// <summary>
        /// Gunboat enum.
        /// </summary>
        Gunboat,

        /// <summary>
        /// Fury enum.
        /// </summary>
        Fury,

        /// <summary>
        /// Interceptor enum.
        /// </summary>
        Interceptor,

        /// <summary>
        /// Destroyer enum.
        /// </summary>
        Destroyer,
    }

    /// <summary>
    /// Enemy ships.
    /// </summary>
    public class Enemy : GameItem
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Enemy"/> class.
        /// </summary>
        /// <param name="typeOfEnemy">Type of Enemy.</param>
        /// <param name="cx">X position of enemys centre.</param>
        /// <param name="cy">Y position of enemys centre.</param>
        public Enemy(EnemyType typeOfEnemy, int cx, int cy)
        {
            this.TypeOfEnemy = typeOfEnemy;
            BitmapImage bmp = new BitmapImage();
            string skin = string.Empty;

            switch (typeOfEnemy)
            {
                case EnemyType.Cutter:
                    this.HealthPoint = 3; this.Damage = 1; skin = @"..\..\..\..\Images\Enemies\Cutter.png"; this.Area = new Rect(cx, cy, 80, 80); this.DX = 8; this.DY = 1;
                    break;
                case EnemyType.Gunboat:
                    this.HealthPoint = 7; this.Damage = 3; skin = @"..\..\..\..\Images\Enemies\Gunboat.png"; this.Area = new Rect(cx, cy, 80, 80); this.DX = 8; this.DY = 1;
                    break;
                case EnemyType.Fury:
                    this.HealthPoint = 12; this.Damage = 10; skin = @"..\..\..\..\Images\Enemies\Fury.png"; this.Area = new Rect(cx, cy, 80, 80); this.DX = 8; this.DY = 1;
                    break;
                case EnemyType.Interceptor:
                    this.HealthPoint = 17; this.Damage = 15; skin = @"..\..\..\..\Images\Enemies\Interceptor.png"; this.Area = new Rect(cx, cy, 100, 130); this.DX = 8; this.DY = 1;
                    break;
                case EnemyType.Destroyer:
                    this.HealthPoint = 20; this.Damage = 18; skin = @"..\..\..\..\Images\Enemies\Destroyer.png"; this.Area = new Rect(cx, cy, 80, 110); this.DX = 8; this.DY = 1;
                    break;
            }

            bmp.BeginInit();
            bmp.UriSource = new Uri(skin, UriKind.Relative);
            bmp.EndInit();
            this.Img = new ImageBrush(bmp);

            this.CX = cx;
            this.CY = cy;
            this.IsVisible = true;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the enemy is visible.
        /// </summary>
        public bool IsVisible { get; set; }

        /// <summary>
        /// Gets or sets type of enemy.
        /// </summary>
        public EnemyType TypeOfEnemy { get; set; }

        /// <summary>
        /// Gets or sets HealthPoint of enemy.
        /// </summary>
        public double HealthPoint { get; set; }

        /// <summary>
        /// Gets or sets damage of enemy.
        /// </summary>
        public double Damage { get; set; }
    }
}
