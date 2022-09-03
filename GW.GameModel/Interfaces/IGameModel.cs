// <copyright file="IGameModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace GW.GameModel
{
    using System.Collections.Generic;
    using System.Windows.Media;

    /// <summary>
    /// Base game model interface.
    /// </summary>
    public interface IGameModel
    {
        /// <summary>
        /// Gets or sets current level.
        /// </summary>
        int Level { get; set; }

        /// <summary>
        /// Gets or sets the player.
        /// </summary>
        Player Player { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is saved.
        /// </summary>
        bool IsSaved { get; set; }

        /// <summary>
        /// Gets or sets Game Status.
        /// </summary>
        short GameStatus { get; set; }

        /// <summary>
        /// Gets enemies collection.
        /// </summary>
        ICollection<Enemy> Enemies { get; }

        /// <summary>
        /// Gets or sets the boss.
        /// </summary>
        Boss Boss { get; set; }

        /// <summary>
        /// Gets displayed items.
        /// </summary>
        ICollection<Item> Items { get; }

        /// <summary>
        /// Gets model width.
        /// </summary>
        /// <returns>Model width (px).</returns>
        double GetWidth();

        /// <summary>
        /// Gets model height.
        /// </summary>
        /// <returns>Model height (px).</returns>
        double GetHeight();

        /// <summary>
        /// Gets score value.
        /// </summary>
        /// <returns>Current score.</returns>
        double GetScore();

        /// <summary>
        /// Increase the current score.
        /// </summary>
        /// <param name="val">Value of increase.</param>
        void IncreaseScore(double val);

        /// <summary>
        /// Gets player ID.
        /// </summary>
        /// <returns>ID of player.</returns>
        string GetID();

        /// <summary>
        /// Set the model's id.
        /// </summary>
        /// <param name="id">Id of model.</param>
        void SetID(string id);

        /// <summary>
        /// Get Image from model.
        /// </summary>
        /// <param name="image">Image id.</param>
        /// <returns>ImageBrush.</returns>
        ImageBrush GetImage(int image);

        /// <summary>
        /// Add new item to collection.
        /// </summary>
        /// <param name="newItem">New item.</param>
        void AddItem(Item newItem);
    }
}
