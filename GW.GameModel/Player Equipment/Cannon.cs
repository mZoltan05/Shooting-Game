// <copyright file="Cannon.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace GW.GameModel
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Basic weapon of player.
    /// </summary>
    public class Cannon
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Cannon"/> class.
        /// </summary>
        public Cannon()
        {
            this.Level = 1;
            this.Damage = 3;
            this.UpgradeCost = 6;
            this.Delay = 1;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Cannon"/> class.
        /// </summary>
        /// <param name="lvl">Level of cannon.</param>
        public Cannon(int lvl)
        {
            this.Level = lvl;
            switch (lvl)
            {
                case 1:
                    this.Damage = 3;
                    this.UpgradeCost = 6;
                    this.Delay = 1;
                    break;
                case 2:
                    this.Damage = 7;
                    this.UpgradeCost = 12;
                    this.Delay = 0.5;
                    break;
                case 3:
                    this.Damage = 12;
                    this.UpgradeCost = 100;
                    this.Delay = 0.3;
                    break;
            }
        }

        /// <summary>
        /// Gets or sets level of cannon.
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// Gets or sets damage of cannon.
        /// </summary>
        public double Damage { get; set; }

        /// <summary>
        /// Gets or sets Upgrade Cost of cannon.
        /// </summary>
        public int UpgradeCost { get; set; }

        /// <summary>
        /// Gets or sets Delay between shots.
        /// </summary>
        public double Delay { get; set; }
    }
}
