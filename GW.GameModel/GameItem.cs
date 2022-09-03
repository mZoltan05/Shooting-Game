// <copyright file="GameItem.cs" company="PlaceholderCompany">
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

    /// <summary>
    /// Parent class of all item.
    /// </summary>
    public abstract class GameItem
    {
        private Rect area;

        /// <summary>
        /// Gets or sets X component of speed.
        /// </summary>
        public double DX { get; set; }

        /// <summary>
        /// Gets or sets Y component of speed.
        /// </summary>
        public double DY { get; set; }

        /// <summary>
        /// Gets or sets image to fill items.
        /// </summary>
        public ImageBrush Img { get; set; }

        /// <summary>
        /// Gets or sets centre X position.
        /// </summary>
        public double CX { get; set; }

        /// <summary>
        /// Gets or sets centre Y position.
        /// </summary>
        public double CY { get; set; }

        /// <summary>
        /// Gets or sets the area of the item.
        /// </summary>
        public Rect Area
        {
            get { return this.area; }
            set { this.area = value; }
        }

        /// <summary>
        /// Detect Collision.
        /// </summary>
        /// <param name="other">The other item to detect.</param>
        /// <returns>Is there a collision.</returns>
        public bool IsCollision(GameItem other)
        {
            if ((other.CX >= this.CX && other.CX < this.CX + this.Area.Width && other.CY >= this.CY && other.CY < this.CY + this.Area.Height)
                || (this.CX >= other.CX && this.CX < other.CX + other.Area.Width && this.CY >= other.CY && this.CY < other.CY + other.Area.Height))
            {
                return true;
            }
            else
            {
                return false;
            }

            // return this.area.IntersectsWith(other.Area);
        }
    }
}
