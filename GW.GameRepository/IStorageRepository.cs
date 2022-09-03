// <copyright file="IStorageRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace GW.GameRepository
{
    using System.Collections.Generic;
    using GW.GameModel;

    /// <summary>
    /// Storage repository interface.
    /// </summary>
    public interface IStorageRepository
    {
        /// <summary>
        /// Loads game model from savefile.
        /// </summary>
        /// <param name="id">Player id.</param>
        /// <returns>Loaded game model instance.</returns>
        IGameModel Load(string id);

        /// <summary>
        /// Saves game model to savefile.
        /// </summary>
        /// <param name="model">Game model instance to save.</param>
        void Save(IGameModel model);

        /// <summary>
        /// Save score.
        /// </summary>
        /// <param name="id">Player id.</param>
        /// <param name="score">Current score.</param>
        void SaveScore(string id, double score);

        /// <summary>
        /// Deletes game in gamefile.
        /// </summary>
        /// <param name="id">Player id.</param>
        void Delete(string id);

        /// <summary>
        /// Gets dictionary of playerid -> gamedetail.
        /// </summary>
        /// <returns>Dictionary of game details, identified by playerids.</returns>
        Dictionary<string, string> GetAllSaves();

        /// <summary>
        /// Gets collection of scores in savefile.
        /// </summary>
        /// <returns>Collection of all scores.</returns>
        Dictionary<string, int> GetAllScores();
    }
}
