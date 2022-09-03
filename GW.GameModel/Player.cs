// <copyright file="Player.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace GW.GameModel
{
    using System;
    using System.Windows;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;

    /// <summary>
    /// Player base class.
    /// </summary>
    public class Player : GameItem
    {
        private Shield playerShield; // Ctor -> new shield
        private Cannon playerCannon;
        private RayGun playerRayGun;

        /// <summary>
        /// Initializes a new instance of the <see cref="Player"/> class.
        /// </summary>
        /// <param name="cx">X position of player centre.</param>
        /// <param name="cy">Y position of player centre.</param>
        /// <param name="dx">X component of speed.</param>
        /// <param name="dy">Y component of speed.</param>
        /// <param name="shieldLvl">Current level of shield.</param>
        /// <param name="cannonLvl">Current level of cannon.</param>
        public Player(double cx, double cy, double dx, double dy, int shieldLvl, int cannonLvl)
        {
            this.playerShield = new Shield(shieldLvl);
            this.playerCannon = new Cannon(cannonLvl);
            this.playerRayGun = new RayGun();
            this.CX = cx;
            this.CY = cy;
            this.DX = dx;
            this.DY = dy;
            this.HealthPoint = 1000;

            BitmapImage bmp = new BitmapImage();
            string skin = @"..\..\..\..\Images\Player.png";
            bmp.BeginInit();
            bmp.UriSource = new Uri(skin, UriKind.Relative);
            bmp.EndInit();
            this.Img = new ImageBrush(bmp);
        }

        /// <summary>
        /// Gets or sets player Shield.
        /// </summary>
        public Shield PlayerShield
        {
            get { return this.playerShield; } set { this.playerShield = value; }
        }

        /// <summary>
        /// Gets or sets Player Cannon.
        /// </summary>
        public Cannon PlayerCannon
        {
            get { return this.playerCannon; } set { this.playerCannon = value; }
        }

        /// <summary>
        /// Gets Player Raygun.
        /// </summary>
        public RayGun PlayerRayGun
        {
            get { return this.playerRayGun; }
        }

        /// <summary>
        /// Gets or sets HealthPoint of player.
        /// </summary>
        public double HealthPoint { get; set; }

        /// <summary>
        /// Gets or sets collected parts count.
        /// </summary>
        public int CollectedPartsCount { get; set; }

        /// <summary>
        /// Gets or sets collected crystals count.
        /// </summary>
        public int CollectedCrystalsCount { get; set; }
    }
}