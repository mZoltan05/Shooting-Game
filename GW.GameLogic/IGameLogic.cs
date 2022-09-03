// <copyright file="IGameLogic.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace GW.GameLogic
{
    using GW.GameModel;
    using GW.GameRepository;

    /// <summary>
    /// Game logic interface.
    /// </summary>
    public interface IGameLogic
    {
        /// <summary>
        /// Initiates one tick.
        /// </summary>
        void OneTick();

        /// <summary>
        /// Gets game model.
        /// </summary>
        /// <returns>Game model instance.</returns>
        IGameModel GetModel();

        /// <summary>
        /// Gets storage repository.
        /// </summary>
        /// <returns>Storage repository instance.</returns>
        IStorageRepository GetStorageRepository();

        /// <summary>
        /// Move player.
        /// </summary>
        /// <param name="d">Moving direction.</param>
        void MovePlayer(Direction d);

        /// <summary>
        /// Shoot player cannon.
        /// </summary>
        void ShootCannon();

        /// <summary>
        /// Shoot player raygun.
        /// </summary>
        void ShootRaygun();

        /// <summary>
        /// Upgrade player shield.
        /// </summary>
        void UpgradeShield();

        /// <summary>
        /// Repair player shield.
        /// </summary>
        void RepairShield();

        /// <summary>
        /// Upgrade player cannon.
        /// </summary>
        void UpgradeCannon();
    }
}
