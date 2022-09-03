// <copyright file="Repository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace GW.GameRepository
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml.Linq;
    using GW.GameModel;

    /// <summary>
    /// Implement repository issues.
    /// </summary>
    public class Repository : IStorageRepository
    {
        /// <inheritdoc/>
        public void Delete(string id)
        {
            if (File.Exists("saves.xml"))
            {
                XDocument saves = XDocument.Load("saves.xml");
                var x = saves.Descendants("save").FirstOrDefault(i => i.Element("id").Value.Equals(id, StringComparison.Ordinal));
                if (x != null)
                {
                    x.Remove();
                    saves.Save("saves.xml");
                }
            }
        }

        /// <inheritdoc/>
        public Dictionary<string, string> GetAllSaves()
        {
            Dictionary<string, string> nameScorePairs = new Dictionary<string, string>();
            if (File.Exists("saves.xml"))
            {
                XDocument saves = XDocument.Load("saves.xml");
                var q = from node in saves.Descendants("save")
                        select node;
                foreach (XElement item in q)
                {
                    if (Convert.ToInt32(item.Element("level").Value, CultureInfo.CurrentCulture) <= 5)
                    {
                        nameScorePairs.Add(item.Element("id").Value, item.Element("level").Value + "\t" + item.Element("score").Value);
                    }
                }

                return nameScorePairs;
            }

            nameScorePairs.Add(" ", " ");
            return nameScorePairs;
        }

        /// <inheritdoc/>
        public Dictionary<string, int> GetAllScores()
        {
            Dictionary<string, int> scoresToReturn = new Dictionary<string, int>();
            if (File.Exists("scores.xml"))
            {
                XDocument scores = XDocument.Load("scores.xml");
                var q = from node in scores.Descendants("topscore")
                        select node;
                if (!q.Any())
                {
                    scoresToReturn.Add("Még senkinek sem sikerült megmenteni a Galaxist! :(", 0);
                    return scoresToReturn;
                }

                foreach (XElement item in q)
                {
                    scoresToReturn.Add(item.Element("id").Value, Convert.ToInt32(item.Element("score").Value, CultureInfo.CurrentCulture));
                }

                return scoresToReturn;
            }
            else
            {
                scoresToReturn.Add(" ", 0);
                return scoresToReturn;
            }
        }

        /// <inheritdoc/>
        public IGameModel Load(string id)
        {
            XDocument saves = XDocument.Load("saves.xml");
            Model modelToReturn;
            var q = from node in saves.Descendants("save")
                    select node;
            foreach (XElement item in q)
            {
                if (item.Element("id").Value == id)
                {
                    Player p = new Player(600, 500, 10, 10, 1, 1);
                    p.PlayerCannon = new Cannon(Convert.ToInt32(item.Element("cannonLevel").Value, CultureInfo.CurrentCulture));
                    p.PlayerShield = new Shield(Convert.ToInt32(item.Element("shieldLevel").Value, CultureInfo.CurrentCulture));
                    modelToReturn = new Model(Convert.ToInt32(item.Element("level").Value, CultureInfo.CurrentCulture), Convert.ToDouble(item.Element("score").Value, CultureInfo.CurrentCulture), p, 700, 1100, id);
                    return modelToReturn;
                }
            }

            return null;
        }

        /// <inheritdoc/>
        public void Save(IGameModel model)
        {
            if (model != null)
            {
                if (File.Exists("saves.xml"))
                {
                    this.Delete(model.GetID());
                }

                XElement modelToSave = new XElement(
                        "save",
                        new XElement("id", model.GetID()),
                        new XElement("score", model.GetScore()),
                        new XElement("level", model.Level),
                        new XElement("shieldLevel", model.Player.PlayerShield.Level),
                        new XElement("cannonLevel", model.Player.PlayerCannon.Level));
                XDocument outDoc;
                if (File.Exists("saves.xml"))
                {
                    outDoc = XDocument.Load("saves.xml");
                }
                else
                {
                    outDoc = new XDocument(new XElement(
                        "saves"));
                }

                outDoc.Element("saves").Add(modelToSave);
                outDoc.Save("saves.xml");
            }
        }

        /// <inheritdoc/>
        public void SaveScore(string id, double score)
        {
            CultureInfo provider = new CultureInfo("hu-HU");
            XDocument scores;
            if (File.Exists("scores.xml"))
            {
                scores = XDocument.Load("scores.xml");
            }
            else
            {
                scores = new XDocument(new XElement(
                    "scores"));
            }

            XElement scoreToSave = new XElement("topscore", new XElement("id", id), new XElement("score", Convert.ToString(score, provider)));
            scores.Element("scores").Add(scoreToSave);
            scores.Save("scores.xml");
        }
    }
}
