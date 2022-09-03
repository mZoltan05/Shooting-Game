// <copyright file="Model.cs" company="PlaceholderCompany">
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
    /// GameModel.
    /// </summary>
    public class Model : IGameModel
    {
        private Boss actualBoss;
        private ICollection<Enemy> enemies;
        private ICollection<Item> items;
        private int level;
        private double score;
        private Player player;
        private double height;
        private double width;
        private string id;
        private ImageBrush[] images;
        private short gameStatus;

        /// <summary>
        /// Initializes a new instance of the <see cref="Model"/> class.
        /// </summary>
        /// <param name="level">Current level.</param>
        /// <param name="score">Current score.</param>
        /// <param name="player">Player with actual parameters.</param>
        /// <param name="height">Height of Window.</param>
        /// <param name="width">Width of Window.</param>
        /// <param name="id">Id of the game.</param>
        public Model(int level, double score, Player player, double height, double width, string id)
        {
            this.actualBoss = null;
            this.enemies = new List<Enemy>();
            this.items = new List<Item>();
            this.level = level;
            this.score = score;
            this.player = player;
            this.height = height;
            this.width = width;
            this.id = id;
            this.player.Area = new Rect(this.player.CX, this.player.CY, 80, 110);
            this.images = new ImageBrush[10];
            this.gameStatus = 0;
            this.IsSaved = false;
            this.InitImages();
         }

        /// <summary>
        /// Gets or sets Player instance.
        /// </summary>
        public Player Player
        {
            get { return this.player; } set { this.player = value; }
        }

        /// <inheritdoc/>
        public short GameStatus
        {
            get { return this.gameStatus; } set { this.gameStatus = value; }
        }

        /// <inheritdoc/>
        public Boss Boss
        {
            get { return this.actualBoss; } set { this.actualBoss = value; }
        }

        /// <inheritdoc/>
        public ICollection<Item> Items
        {
            get { return this.items; }
        }

        /// <inheritdoc/>
        public int Level
        {
            get { return this.level; } set { this.level = value; }
        }

        /// <inheritdoc/>
        public ICollection<Enemy> Enemies
        {
            get { return this.enemies; }
        }

        /// <inheritdoc/>
        public bool IsSaved { get; set; }

        /// <summary>
        /// Get image.
        /// </summary>
        /// <param name="image">id of image.</param>
        /// <returns>Selected image.</returns>
        public ImageBrush GetImage(int image)
        {
            return this.images[image];
        }

        /// <summary>
        /// Appears a new Enemy.
        /// </summary>
        /// <param name="newEnemy">New Enemy.</param>
        public void AddEnemy(Enemy newEnemy)
        {
            this.enemies.Add(newEnemy);
        }

        /// <summary>
        /// Get the Height of the Game.
        /// </summary>
        /// <returns>Height.</returns>
        public double GetHeight()
        {
            return this.height;
        }

        /// <summary>
        /// Appears a new item.
        /// </summary>
        /// <param name="newItem">New Item.</param>
        public void AddItem(Item newItem)
        {
            this.items.Add(newItem);
        }

        /// <summary>
        /// Increase the current score.
        /// </summary>
        /// <param name="val">Value of increase.</param>
        public void IncreaseScore(double val)
        {
            this.score += val;
        }

        /// <summary>
        /// Get the current score.
        /// </summary>
        /// <returns>Score.</returns>
        public double GetScore()
        {
            return this.score;
        }

        /// <summary>
        /// Get the Width of the Game.
        /// </summary>
        /// <returns>Width.</returns>
        public double GetWidth()
        {
            return this.width;
        }

        /// <inheritdoc/>
        public string GetID()
        {
            return this.id;
        }

        /// <summary>
        /// Set the id of the game.
        /// </summary>
        /// <param name="id">ID.</param>
        public void SetID(string id)
        {
            this.id = id;
        }

        /// <summary>
        /// Initialization images.
        /// </summary>
        private void InitImages()
        {
            BitmapImage bmp = new BitmapImage();
            bmp.BeginInit();
            bmp.UriSource = new Uri(@"..\..\..\..\Images\BG_Battle.jpg", UriKind.Relative);
            bmp.EndInit();
            this.images[1] = new ImageBrush(bmp);

            bmp = new BitmapImage();
            bmp.BeginInit();
            bmp.UriSource = new Uri(@"..\..\..\..\Images\BG_Menu.png", UriKind.Relative);
            bmp.EndInit();
            this.images[0] = new ImageBrush(bmp);

            bmp = new BitmapImage();
            bmp.BeginInit();
            bmp.UriSource = new Uri(@"..\..\..\..\Images\MenuBoard.png", UriKind.Relative);
            bmp.EndInit();
            this.images[2] = new ImageBrush(bmp);

            bmp = new BitmapImage();
            bmp.BeginInit();
            bmp.UriSource = new Uri(@"..\..\..\..\Images\First_Scene.gif", UriKind.Relative);
            bmp.EndInit();
            this.images[3] = new ImageBrush(bmp);

            bmp = new BitmapImage();
            bmp.BeginInit();
            bmp.UriSource = new Uri(@"..\..\..\..\Images\Items\Cannon.png", UriKind.Relative);
            bmp.EndInit();
            this.images[4] = new ImageBrush(bmp);

            bmp = new BitmapImage();
            bmp.BeginInit();
            bmp.UriSource = new Uri(@"..\..\..\..\Images\Items\Shield.png", UriKind.Relative);
            bmp.EndInit();
            this.images[5] = new ImageBrush(bmp);
        }
    }
}
