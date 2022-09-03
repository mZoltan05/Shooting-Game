// <copyright file="RayGun.cs" company="PlaceholderCompany">
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
    /// Special player weapon.
    /// </summary>
    public class RayGun
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RayGun"/> class.
        /// </summary>
        public RayGun()
        {
            this.NeededCrystalsToShot = 3;
            this.Damage = 17;
            this.Available = false;
        }

        /// <summary>
        /// Gets Needed Crystals count to shot.
        /// </summary>
        public int NeededCrystalsToShot { get; }

        /// <summary>
        /// Gets Damage of the shot.
        /// </summary>
        public double Damage { get; }

        /// <summary>
        /// Gets or sets a value indicating whether raygun available property.
        /// </summary>
        public bool Available { get; set; }
    }
}
