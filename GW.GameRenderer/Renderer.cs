// <copyright file="Renderer.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace GW.GameRenderer
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using GW.GameModel;

    /// <summary>
    /// Game Renderer.
    /// </summary>
    public class Renderer
    {
        private IGameModel model;
        private Rect bgRect;
        private Rect partRect;
        private Rect crystalRect;
        private Rect playerRect;
        private Rect cannonRect;
        private Rect shieldRect;
        private Item part;
        private Item crystal;
        private List<Rect> items;
        private Typeface font = new Typeface("Arial");
        private Point textLocation = new Point(10, 10);
        private FormattedText text;
        private FormattedText score;
        private double oldScore = -1;
        private Pen pen;
        private Player player;
        private int level;

        /// <summary>
        /// Initializes a new instance of the <see cref="Renderer"/> class.
        /// </summary>
        /// <param name="model">Model.</param>
        public Renderer(IGameModel model)
        {
            this.model = model;
            this.bgRect = new Rect(0, 0, this.model.GetWidth(), this.model.GetHeight());
            this.pen = new Pen(Brushes.Black, 0);
            this.playerRect = new Rect(this.model.Player.CX, this.model.Player.CY, this.model.Player.Area.Width, this.model.Player.Area.Height);
            this.items = new List<Rect>();
            this.crystalRect = new Rect(950, 605, 70, 70);
            this.crystal = new Item(ItemType.Crystal, 400, 400);
            this.partRect = new Rect(1030, 605, 70, 70);
            this.part = new Item(ItemType.Part, 450, 450);
            this.shieldRect = new Rect(1010, 10, 50, 60);
            this.cannonRect = new Rect(970, 110, 120, 90);
        }

        /// <summary>
        /// Render.
        /// </summary>
        /// <param name="ctx">ctx.</param>
        /// <param name="lvl">Level.</param>
        /// <param name="arg2">Argument number 2.</param>
        /// <param name="dict">Dict string,string.</param>
        /// <param name="scores">Scores.</param>
        public void BuildDisplay(DrawingContext ctx, int lvl, int arg2, Dictionary<string, string> dict, Dictionary<string, int> scores)
        {
            this.level = lvl;
            if (ctx != null)
            {
                switch (lvl)
                {
                    case 0: this.Menu(ctx, arg2); break;
                    case 1: this.Level1(ctx); break;
                    case 1000: this.WinGame(ctx); break;
                    case 30: this.HighScore(ctx, scores); break;
                    case -1: this.EndGame(ctx); break;
                    case 100: this.GetName(ctx, dict); break;
                    case 101: this.FirstScene(ctx); break;
                    case 10:
                        if (dict != null)
                        {
                            this.LoadScreen(ctx, dict, arg2);
                        }
                        else
                        {
                            this.LoadScreen(ctx, null, arg2);
                        }

                        break;
                }
            }
        }

        private void HighScore(DrawingContext ctx, Dictionary<string, int> scores)
        {
            if (scores != null)
            {
                this.DrawBackground(ctx, 0);
                this.text = new FormattedText("Vissza - ESC", System.Globalization.CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Arial"), 30, Brushes.Red, VisualTreeHelper.GetDpi(new FrameworkElement()).PixelsPerDip);
                ctx.DrawText(this.text, new Point(870, 600));

                this.text = new FormattedText("Dicsőségcsarnok", System.Globalization.CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Arial"), 30, Brushes.Red, VisualTreeHelper.GetDpi(new FrameworkElement()).PixelsPerDip);
                ctx.DrawText(this.text, new Point(0, 10));
                Point scoreCoordinate = new Point(200, 115);
                int cnt = 0;
                if (scores.Count > 1 || scores.First().Value != 0)
                {
                    foreach (var item in scores)
                    {
                        cnt++;
                        this.text = new FormattedText(cnt.ToString(CultureInfo.CurrentCulture) + ". " + item.Key, System.Globalization.CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Arial"), 30, Brushes.LightYellow, VisualTreeHelper.GetDpi(new FrameworkElement()).PixelsPerDip);
                        ctx.DrawText(this.text, scoreCoordinate);

                        this.text = new FormattedText(item.Value.ToString(CultureInfo.CurrentCulture) + "pont", System.Globalization.CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Arial"), 30, Brushes.LightYellow, VisualTreeHelper.GetDpi(new FrameworkElement()).PixelsPerDip);
                        ctx.DrawText(this.text, new Point(scoreCoordinate.X + 300, scoreCoordinate.Y));
                        scoreCoordinate = new Point(scoreCoordinate.X, scoreCoordinate.Y + 40);
                    }
                }
                else
                {
                    this.text = new FormattedText("Még nem hódította meg senki sem a Galaxist.", System.Globalization.CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Arial"), 30, Brushes.LightYellow, VisualTreeHelper.GetDpi(new FrameworkElement()).PixelsPerDip);
                    ctx.DrawText(this.text, scoreCoordinate);
                }
            }
        }

        private void WinGame(DrawingContext ctx)
        {
            this.DrawBackground(ctx, 1);
            this.DrawPlayer(ctx);
            this.text = new FormattedText("Sikeres Küldetés!", System.Globalization.CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Arial"), 80, Brushes.Blue, VisualTreeHelper.GetDpi(new FrameworkElement()).PixelsPerDip);
            ctx.DrawText(this.text, new Point(20, 350));
            this.text = new FormattedText("Megmentetted a Galaxist!", System.Globalization.CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Arial"), 50, Brushes.LightBlue, VisualTreeHelper.GetDpi(new FrameworkElement()).PixelsPerDip);
            ctx.DrawText(this.text, new Point(20, 450));
            this.text = new FormattedText("Helyet kaptál a dicsőségcsarnokban!", System.Globalization.CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Arial"), 50, Brushes.LightBlue, VisualTreeHelper.GetDpi(new FrameworkElement()).PixelsPerDip);
            ctx.DrawText(this.text, new Point(20, 500));
        }

        private void EndGame(DrawingContext ctx)
        {
            this.DrawBackground(ctx, 1);
            this.DrawPlayer(ctx);
            this.text = new FormattedText("A küldetés elbukott!", System.Globalization.CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Arial"), 80, Brushes.DarkRed, VisualTreeHelper.GetDpi(new FrameworkElement()).PixelsPerDip);
            ctx.DrawText(this.text, new Point(20, 350));
        }

        private void FirstScene(DrawingContext ctx)
        {
            this.DrawBackground(ctx, 2);
            string t = "A Stivaa csillagrendszert veszély fenyegeti, mióta Darth Wralgarr elhatározta,";
            this.text = new FormattedText(t, System.Globalization.CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Arial"), 25, Brushes.LightBlue, VisualTreeHelper.GetDpi(new FrameworkElement()).PixelsPerDip);
            ctx.DrawText(this.text, new Point(100, 100));
            t = "hogy leigázza az összes bolygót és átveszi az uralmat a stratégiailag fontos bányákon.";
            this.text = new FormattedText(t, System.Globalization.CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Arial"), 25, Brushes.LightBlue, VisualTreeHelper.GetDpi(new FrameworkElement()).PixelsPerDip);
            ctx.DrawText(this.text, new Point(100, 130));
            t = "Ha meg tudja szerezni mind az 5 bányát, akkor elegendő B-kristályt termelhet";
            this.text = new FormattedText(t, System.Globalization.CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Arial"), 25, Brushes.LightBlue, VisualTreeHelper.GetDpi(new FrameworkElement()).PixelsPerDip);
            ctx.DrawText(this.text, new Point(100, 160));
            t = "az egész Galaxis elpusztításához.";
            this.text = new FormattedText(t, System.Globalization.CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Arial"), 25, Brushes.LightBlue, VisualTreeHelper.GetDpi(new FrameworkElement()).PixelsPerDip);
            ctx.DrawText(this.text, new Point(100, 190));
            t = "Elindult egy mentőflotta a Wassi bolygóról, de túl sok csillagugrás mire odaérnek,";
            this.text = new FormattedText(t, System.Globalization.CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Arial"), 25, Brushes.LightBlue, VisualTreeHelper.GetDpi(new FrameworkElement()).PixelsPerDip);
            ctx.DrawText(this.text, new Point(100, 250));

            t = "így az ő segítségükre már nem számíthatnak. Sok más rendszer elesett";
            this.text = new FormattedText(t, System.Globalization.CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Arial"), 25, Brushes.LightBlue, VisualTreeHelper.GetDpi(new FrameworkElement()).PixelsPerDip);
            ctx.DrawText(this.text, new Point(100, 280));
            t = "és bár a Stivaa-nak nincs hadserege, de lelkes önkéntesekből akad bőven.";
            this.text = new FormattedText(t, System.Globalization.CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Arial"), 25, Brushes.LightBlue, VisualTreeHelper.GetDpi(new FrameworkElement()).PixelsPerDip);
            ctx.DrawText(this.text, new Point(100, 310));
            t = "Meg kell akadályozni az inváziót még az űrben, mert szárazföldön";
            this.text = new FormattedText(t, System.Globalization.CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Arial"), 25, Brushes.LightBlue, VisualTreeHelper.GetDpi(new FrameworkElement()).PixelsPerDip);
            ctx.DrawText(this.text, new Point(100, 360));
            t = "Wralgarr droidjai kegyelem nélkül pusztítanak.";
            this.text = new FormattedText(t, System.Globalization.CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Arial"), 25, Brushes.LightBlue, VisualTreeHelper.GetDpi(new FrameworkElement()).PixelsPerDip);
            ctx.DrawText(this.text, new Point(100, 390));

            t = "Weck Thrys, az Elzati bányász ellopja apja hajóját, hogy megmentse otthonát";
            this.text = new FormattedText(t, System.Globalization.CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Arial"), 25, Brushes.LightBlue, VisualTreeHelper.GetDpi(new FrameworkElement()).PixelsPerDip);
            ctx.DrawText(this.text, new Point(100, 450));

            t = "és a Galaxist. Az ő bőrébe bújva élhetjük át az űrcsaták világát...";
            this.text = new FormattedText(t, System.Globalization.CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Arial"), 25, Brushes.LightBlue, VisualTreeHelper.GetDpi(new FrameworkElement()).PixelsPerDip);
            ctx.DrawText(this.text, new Point(100, 480));
        }

        private void GetName(DrawingContext ctx, Dictionary<string, string> dict)
        {
            this.DrawBackground(ctx, 0);
            this.text = new FormattedText("Vissza - ESC", System.Globalization.CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Arial"), 30, Brushes.Red, VisualTreeHelper.GetDpi(new FrameworkElement()).PixelsPerDip);
            ctx.DrawText(this.text, new Point(830, 600));
            this.text = new FormattedText("Mi a neved, kalandor?", System.Globalization.CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Arial"), 40, Brushes.Red, VisualTreeHelper.GetDpi(new FrameworkElement()).PixelsPerDip);
            ctx.DrawText(this.text, new Point(350, 30));
            ctx.DrawRoundedRectangle(new ImageBrush(), new Pen(Brushes.Red, 3), new Rect(300, 100, 550, 40), 10, 10);
            string name = dict.Last().Key != null ? dict.Last().Key : " ";
            this.text = new FormattedText(name, System.Globalization.CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Arial"), 30, Brushes.Red, VisualTreeHelper.GetDpi(new FrameworkElement()).PixelsPerDip);
            ctx.DrawText(this.text, new Point(310, 105));
        }

        private void LoadScreen(DrawingContext ctx, Dictionary<string, string> dict, int selected)
        {
            this.DrawBackground(ctx, 0);
            this.text = new FormattedText("Vissza - ESC", System.Globalization.CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Arial"), 30, Brushes.Red, VisualTreeHelper.GetDpi(new FrameworkElement()).PixelsPerDip);
            ctx.DrawText(this.text, new Point(870, 600));
            if (dict != null)
            {
                this.text = new FormattedText("Játék Betöltése", System.Globalization.CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Arial"), 30, Brushes.Red, VisualTreeHelper.GetDpi(new FrameworkElement()).PixelsPerDip);
                ctx.DrawText(this.text, new Point(0, 10));
                ctx.DrawRoundedRectangle(new ImageBrush(), new Pen(Brushes.Red, 3), new Rect(300, 100, 550, 500), 10, 10);
                Point saveCoordinate = new Point(310, 115);
                this.text = new FormattedText("Játékos\t\t  Szint\tPontszám", System.Globalization.CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Arial"), 30, Brushes.Red, VisualTreeHelper.GetDpi(new FrameworkElement()).PixelsPerDip);
                ctx.DrawText(this.text, new Point(saveCoordinate.X, saveCoordinate.Y - 50));
                this.SelectedDraw(ctx, selected);
                foreach (KeyValuePair<string, string> item in dict)
                {
                    this.text = new FormattedText(item.Key, System.Globalization.CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Arial"), 30, Brushes.Red, VisualTreeHelper.GetDpi(new FrameworkElement()).PixelsPerDip);
                    ctx.DrawText(this.text, saveCoordinate);

                    this.text = new FormattedText(item.Value, System.Globalization.CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Arial"), 30, Brushes.Red, VisualTreeHelper.GetDpi(new FrameworkElement()).PixelsPerDip);
                    ctx.DrawText(this.text, new Point(saveCoordinate.X + 300, saveCoordinate.Y));
                    saveCoordinate = new Point(saveCoordinate.X, saveCoordinate.Y + 40);
                }

                this.text = new FormattedText("[T] - Törlés", System.Globalization.CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Arial"), 30, Brushes.Red, VisualTreeHelper.GetDpi(new FrameworkElement()).PixelsPerDip);
                ctx.DrawText(this.text, new Point(870, 550));

                this.text = new FormattedText("[ENTER] - Betölt", System.Globalization.CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Arial"), 30, Brushes.Red, VisualTreeHelper.GetDpi(new FrameworkElement()).PixelsPerDip);
                ctx.DrawText(this.text, new Point(860, 500));
            }
            else
            {
                this.text = new FormattedText("Jelenleg nincs mentett játék", System.Globalization.CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Arial"), 30, Brushes.Red, VisualTreeHelper.GetDpi(new FrameworkElement()).PixelsPerDip);
                ctx.DrawText(this.text, new Point(0, 10));
            }
           }

        private void Menu(DrawingContext ctx, int selected)
        {
            this.DrawBackground(ctx, 0);
            ctx.DrawRectangle(this.model.GetImage(2), this.pen, new Rect(0, 422, 1100, 240));
            this.SelectedDraw(ctx, selected);
            this.MenuTxt(ctx);
        }

        private void SelectedDraw(DrawingContext ctx, int selected)
        {
            Rect r;
            if (this.level == 0)
            {
                switch (selected)
                {
                    case 1: r = new Rect(430, 70, 220, 80); break;
                    case 2: r = new Rect(430, 150, 220, 80); break;
                    case 3: r = new Rect(330, 230, 420, 80); break;
                    case 4: r = new Rect(445, 310, 200, 80); break;
                }
            }
            else
            {
                switch (selected)
                {
                    case 1: r = new Rect(305, 115, 540, 34); break;
                    case 2: r = new Rect(305, 155, 540, 34); break;
                    case 3: r = new Rect(305, 195, 540, 34); break;
                    case 5: r = new Rect(305, 235, 540, 34); break;
                    case 6: r = new Rect(305, 275, 540, 34); break;
                    case 7: r = new Rect(305, 315, 540, 34); break;
                    case 8: r = new Rect(305, 355, 540, 34); break;
                    case 9: r = new Rect(305, 395, 540, 34); break;
                }
            }

            ctx.DrawRoundedRectangle(Brushes.DarkViolet, this.pen, r, 10, 10);
        }

        private void MenuTxt(DrawingContext ctx)
        {
            this.text = new FormattedText("Új Játék", System.Globalization.CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Arial"), 50, Brushes.Red, VisualTreeHelper.GetDpi(new FrameworkElement()).PixelsPerDip);
            ctx.DrawText(this.text, new Point(450, 80));
            this.text = new FormattedText("Betöltés", System.Globalization.CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Arial"), 50, Brushes.Red, VisualTreeHelper.GetDpi(new FrameworkElement()).PixelsPerDip);
            ctx.DrawText(this.text, new Point(450, 160));
            this.text = new FormattedText("Dicsőségcsarnok", System.Globalization.CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Arial"), 50, Brushes.Red, VisualTreeHelper.GetDpi(new FrameworkElement()).PixelsPerDip);
            ctx.DrawText(this.text, new Point(350, 240));
            this.text = new FormattedText("Kilépés", System.Globalization.CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Arial"), 50, Brushes.Red, VisualTreeHelper.GetDpi(new FrameworkElement()).PixelsPerDip);
            ctx.DrawText(this.text, new Point(465, 320));
        }

        private void Level1(DrawingContext ctx)
        {
            this.DrawBackground(ctx, 1);

            this.DrawText(ctx);
            this.player = this.model.Player;

            this.DrawPlayer(ctx);

            if (this.model.Enemies.Count != 0)
            {
                this.DrawEnemies(ctx);
            }

            if (this.model.Boss != null && this.model.Boss.IsVisible)
            {
                this.DrawBoss(ctx);
            }

            if (this.model.Items.Count != 0)
            {
                this.DrawItems(ctx);
            }

            this.DrawUpgrades(ctx);
            this.DrawHpAndShield(ctx);
            this.DrawCrystalAndPart(ctx);
        }

        private void DrawUpgrades(DrawingContext ctx)
        {
            if (this.model.Player.CollectedPartsCount >= this.model.Player.PlayerShield.UpgradeCost)
            {
                ctx.DrawRectangle(this.model.GetImage(5), this.pen, this.shieldRect);
                this.text = new FormattedText("[U] - fejlesztés", System.Globalization.CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Arial"), 15, Brushes.LightYellow, VisualTreeHelper.GetDpi(new FrameworkElement()).PixelsPerDip);
                ctx.DrawText(this.text, new Point(900, 30));
            }

            if (this.model.Player.CollectedPartsCount >= this.model.Player.PlayerCannon.UpgradeCost)
            {
                ctx.DrawRectangle(this.model.GetImage(4), this.pen, this.cannonRect);
                this.text = new FormattedText("[W] - fejlesztés", System.Globalization.CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Arial"), 15, Brushes.LightYellow, VisualTreeHelper.GetDpi(new FrameworkElement()).PixelsPerDip);
                ctx.DrawText(this.text, new Point(900, 145));
            }

            if (this.model.Player.CollectedPartsCount >= this.model.Player.PlayerShield.RepairCost && this.model.Player.PlayerShield.Level > 1)
            {
                this.text = new FormattedText("[R] - Pajzs javítása", System.Globalization.CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Arial"), 15, Brushes.LightYellow, VisualTreeHelper.GetDpi(new FrameworkElement()).PixelsPerDip);
                ctx.DrawText(this.text, new Point(900, 240));
            }
        }

        private void DrawCrystalAndPart(DrawingContext ctx)
        {
            ctx.DrawRectangle(this.part.Img, this.pen, this.partRect);
            this.text = new FormattedText(this.model.Player.CollectedPartsCount.ToString(System.Globalization.CultureInfo.CurrentCulture), System.Globalization.CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Arial"), 25, Brushes.LightGreen, VisualTreeHelper.GetDpi(new FrameworkElement()).PixelsPerDip);
            ctx.DrawText(this.text, new Point(1020, 630));
            ctx.DrawRectangle(this.crystal.Img, this.pen, this.crystalRect);
            this.text = new FormattedText(this.model.Player.CollectedCrystalsCount.ToString(System.Globalization.CultureInfo.CurrentCulture), System.Globalization.CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Arial"), 25, Brushes.LightBlue, VisualTreeHelper.GetDpi(new FrameworkElement()).PixelsPerDip);
            ctx.DrawText(this.text, new Point(940, 630));
        }

        private void DrawHpAndShield(DrawingContext ctx)
        {
            string hp = hp = $"Életerő {Math.Round(this.model.Player.HealthPoint).ToString(CultureInfo.CurrentCulture)}/1000";

            this.text = new FormattedText(hp, System.Globalization.CultureInfo.CurrentCulture, FlowDirection.LeftToRight, this.font, 25, Brushes.Red, VisualTreeHelper.GetDpi(new FrameworkElement()).PixelsPerDip);
            ctx.DrawText(this.text, new Point(10, 600));

            if (this.model.Player.PlayerShield.DamageReduction > 0)
            {
                 hp = $"Pajzs {this.model.Player.PlayerShield.Level} szint, Aktív";
            }
            else
            {
                  hp = $"Pajzs {this.model.Player.PlayerShield.Level} szint, NEM MŰKÖDIK";
            }

            this.text = new FormattedText(hp, System.Globalization.CultureInfo.CurrentCulture, FlowDirection.LeftToRight, this.font, 25, Brushes.Red, VisualTreeHelper.GetDpi(new FrameworkElement()).PixelsPerDip);
            ctx.DrawText(this.text, new Point(10, 630));
            if (this.model.Player.PlayerRayGun.Available)
            {
                this.text = new FormattedText("Sugárvető aktív", System.Globalization.CultureInfo.CurrentCulture, FlowDirection.LeftToRight, this.font, 25, Brushes.LightBlue, VisualTreeHelper.GetDpi(new FrameworkElement()).PixelsPerDip);
                ctx.DrawText(this.text, new Point(400, 630));
            }

            if (this.model.IsSaved)
            {
                this.text = new FormattedText("Játék Mentve", System.Globalization.CultureInfo.CurrentCulture, FlowDirection.LeftToRight, this.font, 25, Brushes.Yellow, VisualTreeHelper.GetDpi(new FrameworkElement()).PixelsPerDip);
                ctx.DrawText(this.text, new Point(400, 10));
            }
        }

        private void DrawItems(DrawingContext ctx)
        {
            foreach (Item item in this.model.Items)
            {
                ctx.DrawRectangle(item.Img, this.pen, new Rect(item.CX, item.CY, item.Area.Width, item.Area.Height));
            }
        }

        private void DrawBoss(DrawingContext ctx)
        {
            ctx.DrawRectangle(this.model.Boss.Img, this.pen, new Rect(this.model.Boss.CX, this.model.Boss.CY, this.model.Boss.Area.Width, this.model.Boss.Area.Height));
        }

        private void DrawPlayer(DrawingContext ctx)
        {
            this.playerRect.X = this.player.CX;
            this.playerRect.Y = this.player.CY;
            ctx.DrawRectangle(this.player.Img, this.pen, this.playerRect);
        }

        private void DrawEnemies(DrawingContext ctx)
        {
            foreach (Enemy enemy in this.model.Enemies)
            {
                ctx.DrawRectangle(enemy.Img, this.pen, new Rect(enemy.CX, enemy.CY, enemy.Area.Width, enemy.Area.Height));
            }
        }

        private void DrawText(DrawingContext ctx)
        {
            if (this.oldScore != this.model.GetScore())
            {
                this.score = new FormattedText(this.model.GetScore() + " pont", System.Globalization.CultureInfo.CurrentCulture, FlowDirection.LeftToRight, this.font, 16, Brushes.LightYellow, VisualTreeHelper.GetDpi(new FrameworkElement()).PixelsPerDip);
                this.oldScore = this.model.GetScore();
            }

            ctx.DrawText(this.score, this.textLocation);
            this.text = new FormattedText(this.model.Level + " szint", System.Globalization.CultureInfo.CurrentCulture, FlowDirection.LeftToRight, this.font, 16, Brushes.LightYellow, VisualTreeHelper.GetDpi(new FrameworkElement()).PixelsPerDip);

            ctx.DrawText(this.text, new Point(120, 10));
        }

        private void DrawBackground(DrawingContext ctx, int level)
        {
            switch (level)
            {
                case 1:
                    ctx.DrawRectangle(this.model.GetImage(1), this.pen, this.bgRect); break;
                case 0:
                    ctx.DrawRectangle(this.model.GetImage(0), this.pen, this.bgRect); break;
                case 2:
                    ctx.DrawRectangle(this.model.GetImage(3), this.pen, this.bgRect); break;
            }
        }
    }
}
