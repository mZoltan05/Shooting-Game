// <copyright file="Shield.cs" company="PlaceholderCompany">
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
    /// Shield of the Player.
    /// </summary>
    public class Shield
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Shield"/> class.
        /// </summary>
        public Shield()
        {
            this.Level = 1;
            this.DamageReduction = 0;
            this.UpgradeCost = 4;
            this.RepairCost = 0;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Shield"/> class.
        /// </summary>
        /// <param name="lvl">Level of shield.</param>
        public Shield(int lvl)
        {
            this.Level = lvl;
            switch (lvl)
            {
                case 1:
                    this.DamageReduction = 0;
                    this.UpgradeCost = 4;
                    this.RepairCost = 0;
                    break;
                case 2:
                    this.DamageReduction = 0.2;
                    this.UpgradeCost = 6;
                    this.RepairCost = 2;
                    break;
                case 3:
                    this.DamageReduction = 0.3;
                    this.UpgradeCost = 9;
                    this.RepairCost = 3;
                    break;
                case 4:
                    this.DamageReduction = 0.4;
                    this.UpgradeCost = 12;
                    this.RepairCost = 4;
                    break;
                case 5:
                    this.DamageReduction = 0.5;
                    this.UpgradeCost = 100;
                    this.RepairCost = 5;
                    break;
            }
        }

        /// <summary>
        /// Gets or sets level of the shield.
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// Gets or sets Damage Reduction.
        /// </summary>
        public double DamageReduction { get; set; }

        /// <summary>
        /// Gets or sets level of the shield.
        /// </summary>
        public int UpgradeCost { get; set; }

        /// <summary>
        /// Gets or sets level of the shield.
        /// </summary>
        public int RepairCost { get; set; }
    }
}
