// <copyright file="GameControl.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace GW.UI
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using System.Windows.Threading;
    using GW.GameLogic;
    using GW.GameModel;
    using GW.GameRenderer;
    using GW.GameRepository;

    /// <summary>
    /// GameControl.
    /// </summary>
    public class GameControl : FrameworkElement
    {
        private IGameModel model;
        private IStorageRepository repository;
        private Renderer renderer;
        private IGameLogic logic;
        private bool isPaused;
        private int lvl;
        private int selected;
        private DispatcherTimer mainTimer;
        private Dictionary<string, string> dict; // Argument of BuildDisplay. Can be used for Scores or Saves
        private string name;
        private int tickGoal;
        private Dictionary<string, int> scores;

        /// <summary>
        /// Initializes a new instance of the <see cref="GameControl"/> class.
        /// </summary>
        public GameControl()
        {
            this.Loaded += this.GameControl_Loaded;
        }

        /// <inheritdoc/>
        protected override void OnRender(DrawingContext drawingContext)
        {
            if (this.renderer != null)
            {
                this.renderer.BuildDisplay(drawingContext, this.lvl, this.selected, this.dict, this.scores);
            }
        }

        private void GameControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.isPaused = false;
            this.repository = new Repository();
            this.scores = this.repository.GetAllScores();
            this.lvl = 0; // 0 -> Menu
            this.selected = 1;
            Player p = new Player(600, 500, 10, 10, 1, 1);
            this.name = string.Empty;
            this.dict = new Dictionary<string, string>();
            this.model = new Model(1, 0, p, this.ActualHeight, this.ActualWidth, "id");
            this.model.Player = p;
            this.model.GameStatus = 10;
            this.model.Level = 1;
            this.logic = new GWLogic(this.model, this.repository);
            this.renderer = new Renderer(this.model);

            Window win = Window.GetWindow(this);
            if (win != null)
            {
                this.mainTimer = new DispatcherTimer();
                this.mainTimer.Interval = TimeSpan.FromMilliseconds(40);
                this.mainTimer.Tick += this.MainTimer_Tick;
                this.mainTimer.Start();

                win.KeyDown += this.Win_KeyDown;
            }

            this.InvalidateVisual();
        }

        private void Win_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (this.lvl == 1 || this.lvl == 2 || this.lvl == 3 || this.lvl == 4 || this.lvl == 5)
            {
                switch (e.Key)
                {
                    case Key.Left: this.logic.MovePlayer(Direction.Left); break;
                    case Key.Right: this.logic.MovePlayer(Direction.Right); break;
                    case Key.Up: this.logic.MovePlayer(Direction.Up); break;
                    case Key.Down: this.logic.MovePlayer(Direction.Down); break;

                    case Key.P:
                        if (!this.isPaused)
                        {
                            this.mainTimer.Stop();
                            this.isPaused = true;
                        }
                        else
                        {
                            this.mainTimer.Start();
                            this.isPaused = false;
                        }

                        break;
                    case Key.Space: this.logic.ShootCannon(); break;
                    case Key.LeftCtrl: this.logic.ShootRaygun(); break;
                    case Key.U: this.logic.UpgradeShield(); break;
                    case Key.W: this.logic.UpgradeCannon(); break;
                    case Key.R: this.logic.RepairShield(); break;
                }

                // this.InvalidateVisual();
            }
            else
            {
                switch (this.lvl)
                {
                    case 0: this.Menu(e); break;
                    case -1:
                        if (e.Key == Key.Enter)
                        {
                            this.ResetGame();
                        }

                        break;
                    case 30:
                        if (e.Key == Key.Escape)
                        {
                            this.lvl = 0;
                        }

                        this.InvalidateVisual();
                        break;
                    case 1000:
                        if (e.Key == Key.Enter)
                        {
                            this.repository.SaveScore(this.model.GetID(), this.model.GetScore());
                            this.repository.Delete(this.model.GetID());
                            this.ResetGame();
                        }

                        break;
                    case 100: this.NewGame(e); break;
                    case 101:
                        if (e.Key == Key.Enter)
                        {
                            this.lvl = 1;
                            this.model.GameStatus = 0;

                            // DELME
                            this.model.Player.CollectedCrystalsCount = 0;
                            this.model.Player.PlayerRayGun.Available = false;

                            this.model.Player.PlayerShield = new Shield();
                            this.model.Player.PlayerCannon = new Cannon();
                            this.model.Player.CollectedPartsCount = 0;
                            this.InvalidateVisual();
                        }

                        break;

                    case 10: // Load menu
                        switch (e.Key)
                        {
                            case Key.Escape: this.lvl = 0; this.InvalidateVisual(); break;
                            case Key.Up:
                                if (this.selected > 1)
                                {
                                    this.selected--;
                                }

                                this.InvalidateVisual(); break;
                            case Key.Down:
                                if (this.selected < this.dict.Count)
                                {
                                    this.selected++;
                                }

                                this.InvalidateVisual(); break;

                            case Key.Enter:
                                if (this.dict.First().Key != " " || this.dict.First().Value != " ")
                                {
                                    int loadcnt = 0;
                                    foreach (KeyValuePair<string, string> item in this.dict)
                                    {
                                        loadcnt++;
                                        if (loadcnt == this.selected)
                                        {
                                            this.model = this.repository.Load(item.Key);
                                        }
                                    }

                                    this.lvl = 1;
                                    this.model.GameStatus = 0;
                                    this.logic = new GWLogic(this.model, this.repository);
                                    this.renderer = new Renderer(this.model);
                                    this.InvalidateVisual();
                                }

                                break;

                            case Key.T:

                                int deletecnt = 0;
                                foreach (KeyValuePair<string, string> item in this.dict)
                                {
                                    deletecnt++;
                                    if (deletecnt == this.selected)
                                    {
                                        this.repository.Delete(item.Key);
                                    }
                                }

                                this.LoadMenu();
                                this.InvalidateVisual();

                                break;
                        }

                        break;
                }
            }
        }

        private void Menu(KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Enter:
                    switch (this.selected)
                    {
                        case 4:
                            Window.GetWindow(this).Close();
                            break;
                        case 3: this.lvl = 30; this.HighScore(); this.InvalidateVisual(); break;
                        case 2: this.lvl = 10; this.LoadMenu(); break;
                        case 1: this.lvl = 100; this.dict = new Dictionary<string, string>(); this.dict.Add(" ", " "); this.InvalidateVisual(); break;
                    }

                    break;
                case Key.Escape:
                    Window.GetWindow(this).Close();
                    break;
                case Key.Up:
                    if (this.selected != 1)
                    {
                        this.selected--;
                        this.InvalidateVisual();
                    }

                    break;
                case Key.Down:
                    if (this.selected != 4)
                    {
                        this.selected++;
                        this.InvalidateVisual();
                    }

                    break;
            }
        }

        private void HighScore()
        {
            Dictionary<string, int> tempList = this.repository.GetAllScores();
            this.scores = new Dictionary<string, int>();
            KeyValuePair<string, int> maxitem;
            while (tempList.Count > 0)
            {
                maxitem = new KeyValuePair<string, int>(" ", 0);
                foreach (KeyValuePair<string, int> item in tempList)
                {
                    if (Convert.ToInt32(item.Value) > Convert.ToInt32(maxitem.Value))
                    {
                        maxitem = item;
                    }
                }

                this.scores.Add(maxitem.Key, maxitem.Value);
                tempList.Remove(maxitem.Key);
            }
        }

        private void NewGame(KeyEventArgs e)
        {
            this.dict.Remove(this.name);
            switch (e.Key)
            {
                case Key.Escape: this.lvl = 0; this.name = string.Empty; this.dict = new Dictionary<string, string>(); break;
                case Key.Enter: this.model.SetID(this.name); this.lvl = 101; break;
                case Key.Space: this.name += " "; break;
                case Key.Back:
                    if (this.name.Length > 0)
                    {
                        this.name = this.name.Remove(this.name.Length - 1);
                    }

                    break;
                default:
                    if (this.name.Length < 20)
                    {
                        this.name += e.Key.ToString();
                    }

                    break;
            }

            this.dict.Add(this.name, ".");
            this.InvalidateVisual();
        }

        private void LoadMenu()
        {
            this.dict = this.repository.GetAllSaves();
            this.selected = 1;
            this.InvalidateVisual();
        }

        private void ResetGame()
        {
            this.model.GameStatus = 10;
            this.lvl = 0;
            this.model.Level = 1;
            this.model.Player = new Player(600, 500, 10, 10, 1, 1);
            this.name = string.Empty;
            this.model.SetID("NEWGAME");
        }

        private void MainTimer_Tick(object sender, EventArgs e)
        {
            this.logic.OneTick();
            if (this.tickGoal > 0)
            {
                this.tickGoal--;
                if (!this.model.IsSaved)
                {
                    this.model.IsSaved = true;
                }
                else
                {
                    if (this.model.IsSaved)
                    {
                        this.model.IsSaved = false;
                    }
                }
            }

            if (this.model.GameStatus == -1)
                {
                    this.lvl = -1;
                }

            if (this.model.GameStatus == 1)
                {
                    this.tickGoal = 50;
                    this.repository.Save(this.model);
                }

            if (this.model.GameStatus == 2)
                {
                    this.lvl = 1000;
                    this.model.GameStatus = 0;
                }

            this.InvalidateVisual();
        }
    }
}
