// <copyright file="Boss.cs" company="PlaceholderCompany">
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
    /// Enemy Bosses.
    /// </summary>
    public class Boss : GameItem
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Boss"/> class.
        /// </summary>
        /// <param name="name">Name of Boss.</param>
        /// <param name="cx">X position of Boss.</param>
        /// <param name="cy">Y position of Boss.</param>
        public Boss(string name, int cx, int cy)
        {
            this.Name = name;
            BitmapImage bmp = new BitmapImage();
            string skin = string.Empty;
            switch (name)
            {
                case "Darth Wralgarr":
                    this.Damage = 300; this.HealthPoint = 130; skin = @"..\..\..\..\Images\Enemies\Boss5.png"; this.Area = new Rect(cx, cy, 110, 140); this.DX = 8; this.DY = 0;
                    break;
                case "Duuq Zokrorr":
                    this.Damage = 200; this.HealthPoint = 100; skin = @"..\..\..\..\Images\Enemies\Boss4.png"; this.Area = new Rect(cx, cy, 110, 140); this.DX = 8; this.DY = 0;
                    break;
                case "Phlamvom Zekvud":
                    this.Damage = 150; this.HealthPoint = 80; skin = @"..\..\..\..\Images\Enemies\Boss3.png"; this.Area = new Rect(cx, cy, 110, 140); this.DX = 8; this.DY = 0;
                    break;
                case "Grof Kraq":
                    this.Damage = 100; this.HealthPoint = 60; skin = @"..\..\..\..\Images\Enemies\Boss2.png"; this.Area = new Rect(cx, cy, 130, 150); this.DX = 8; this.DY = 0;
                    break;
                case "Merob-zaa Keddis":
                    this.Damage = 50; this.HealthPoint = 40; skin = @"..\..\..\..\Images\Enemies\Boss1.png"; this.Area = new Rect(cx, cy, 130, 150); this.DX = 8; this.DY = 0;
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
        /// Gets or sets a value indicating whether the boss is visible.
        /// </summary>
        public bool IsVisible { get; set; }

        /// <summary>
        /// Gets or sets the damage of boss.
        /// </summary>
        public double Damage { get; set; }

        /// <summary>
        /// Gets or sets the healthpoint of boss.
        /// </summary>
        public double HealthPoint { get; set; }

        /// <summary>
        /// Gets or sets the name of the boss.
        /// </summary>
        public string Name { get; set; }
    }
}
