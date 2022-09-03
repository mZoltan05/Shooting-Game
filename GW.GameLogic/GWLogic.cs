// <copyright file="GWLogic.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace GW.GameLogic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using GW.GameModel;
    using GW.GameRepository;

    /// <summary>
    /// GW game logic class.
    /// </summary>
    public class GWLogic : IGameLogic
    {
        private IGameModel model;
        private IStorageRepository storageRepository;
        private Random random;
        private int tick;

        /// <summary>
        /// Initializes a new instance of the <see cref="GWLogic"/> class.
        /// </summary>
        /// <param name="model">Game model.</param>
        /// <param name="storageRepository">Storage repository.</param>
        public GWLogic(IGameModel model, IStorageRepository storageRepository)
        {
            this.model = model;
            this.storageRepository = storageRepository;
            this.random = new Random();
            this.tick = 0;
        }

        /// <inheritdoc/>
        public IGameModel GetModel()
        {
            return this.model;
        }

        /// <inheritdoc/>
        public IStorageRepository GetStorageRepository()
        {
            return this.storageRepository;
        }

        /// <inheritdoc/>
        public void OneTick()
        {
            if (this.model.Level < 6)
            {
                if (this.model.GameStatus == 1)
                {
                    this.tick = 0;
                    this.model.GameStatus = 0;
                    this.model.Player.HealthPoint = 1000;
                }

                if (this.model.GameStatus == 0)
                {
                    this.Level();
                    this.BossTick();
                    this.EnemyTick();
                    this.ItemsTick();
                    this.tick++;
                }
            }
            else
            {
                this.model.GameStatus = 2;
            }
        }

        /// <inheritdoc/>
        public void MovePlayer(Direction d)
        {
            double cx = this.model.Player.CX;
            double cy = this.model.Player.CY;

            switch (d)
            {
                case Direction.Left:
                    cx = (cx - this.model.Player.DX < 0) ? 0 : (cx - this.model.Player.DX);
                    break;

                case Direction.Right:
                    cx = (cx + this.model.Player.DX + this.model.Player.Area.Width > this.model.GetWidth())
                        ? (this.model.GetWidth() - this.model.Player.Area.Width)
                        : (cx + this.model.Player.DX);
                    break;

                case Direction.Up:
                    cy = (cy - this.model.Player.DY < 0) ? 0 : (cy - this.model.Player.DY);
                    break;

                case Direction.Down:
                    cy = (cy + this.model.Player.DY + this.model.Player.Area.Height > this.model.GetHeight())
                        ? (this.model.GetHeight() - this.model.Player.Area.Height)
                        : (cy + this.model.Player.DY);
                    break;
            }

            if (this.model.Player.CX != cx)
            {
                this.model.Player.CX = cx;
            }

            if (this.model.Player.CY != cy)
            {
                this.model.Player.CY = cy;
            }
        }

        /// <inheritdoc/>
        public void ShootCannon()
        {
            Item bullet = new (ItemType.PlayerBullet, 0, 0)
            {
                CX = this.model.Player.CX + (this.model.Player.Area.Width / 2),
            };
            bullet.CY = this.model.Player.CY - bullet.Area.Height;
            this.model.Items.Add(bullet);
        }

        /// <inheritdoc/>
        public void ShootRaygun()
        {
            if (this.model.Player.PlayerRayGun.Available)
            {
                Item ray = new (ItemType.PlayerRay, 0, 0)
                {
                    CX = this.model.Player.CX + (this.model.Player.Area.Width / 2),
                };
                ray.CY = this.model.Player.CY - ray.Area.Height;
                this.model.Items.Add(ray);
                this.model.Player.CollectedCrystalsCount -= 3;
                if (this.model.Player.CollectedCrystalsCount < this.model.Player.PlayerRayGun.NeededCrystalsToShot)
                {
                    this.model.Player.PlayerRayGun.Available = false;
                }
            }
        }

        /// <inheritdoc/>
        public void UpgradeShield()
        {
            if (this.model.Player.PlayerShield != null)
            {
                if (this.model.Player.PlayerShield.Level < 5)
                {
                    if (this.model.Player.CollectedPartsCount >= this.model.Player.PlayerShield.UpgradeCost)
                    {
                        this.model.Player.CollectedPartsCount -= this.model.Player.PlayerShield.UpgradeCost;
                        int level = this.model.Player.PlayerShield.Level + 1;
                        this.model.Player.PlayerShield = null;
                        this.model.Player.PlayerShield = new Shield(level);
                    }
                }
            }
        }

        /// <inheritdoc/>
        public void RepairShield()
        {
            if (this.model.Player.PlayerShield != null)
            {
                if (this.model.Player.CollectedPartsCount >= this.model.Player.PlayerShield.RepairCost)
                {
                    this.model.Player.CollectedPartsCount -= this.model.Player.PlayerShield.RepairCost;
                    int level = this.model.Player.PlayerShield.Level;
                    this.model.Player.PlayerShield = null;
                    this.model.Player.PlayerShield = new Shield(level);
                }
            }
        }

        /// <inheritdoc/>
        public void UpgradeCannon()
        {
            if (this.model.Player.PlayerCannon != null)
            {
                if (this.model.Player.PlayerCannon.Level < 3)
                {
                    if (this.model.Player.CollectedPartsCount >= this.model.Player.PlayerCannon.UpgradeCost)
                    {
                        this.model.Player.CollectedPartsCount -= this.model.Player.PlayerCannon.UpgradeCost;
                        int level = this.model.Player.PlayerCannon.Level + 1;
                        this.model.Player.PlayerCannon = null;
                        this.model.Player.PlayerCannon = new Cannon(level);
                    }
                }
            }
        }

        private void Level()
        {
            // 1 sec ~= 25 tick
            if (this.model.GameStatus == 0)
            {
                if (this.tick == 0)
                {
                    this.AddBoss();
                    this.model.Boss.IsVisible = false;
                    this.AddCrystal();
                    this.AddEnemy();
                    this.AddPart();
                }
                else if (this.tick == 90)
                {
                    this.AddAsteroid();
                    this.AddPart();
                }
                else if (this.tick == 180)
                {
                    this.AddEnemy();
                    this.AddPart();
                }
                else if (this.tick == 270)
                {
                    this.AddAsteroid();
                    this.AddPart();
                }
                else if (this.tick == 300)
                {
                    this.AddCrystal();
                }
                else if (this.tick == 360)
                {
                    this.AddEnemy();
                    this.AddPart();
                }
                else if (this.tick == 450)
                {
                    this.AddAsteroid();
                }
                else if (this.tick == 540)
                {
                    this.AddEnemy();
                    this.AddPart();
                }
                else if (this.tick == 600)
                {
                    this.AddCrystal();
                }
                else if (this.tick == 630)
                {
                    this.AddAsteroid();
                    this.AddPart();
                }
                else if (this.tick == 720)
                {
                    this.AddEnemy();
                    this.AddPart();
                }
                else if (this.tick == 810)
                {
                    this.AddAsteroid();
                    this.AddPart();
                }
                else if (this.tick == 900)
                {
                    this.AddPart();
                    this.model.Boss.IsVisible = true;
                }
            }
        }

        private void BossTick()
        {
            Boss boss = this.model.Boss;
            if (boss != null && boss.IsVisible)
            {
                bool delete = false;

                double cx = boss.CX + boss.DX;
                double right = cx + boss.Area.Width;
                if (cx < 0 || right >= this.model.GetWidth())
                {
                    boss.DX = -boss.DX;
                    cx = (cx < 0) ? 0 : cx;
                    cx = (right >= this.model.GetWidth()) ? this.model.GetWidth() - boss.Area.Width : cx;
                }

                if (boss.CX != cx)
                {
                    boss.CX = cx;
                }

                if (Math.Abs((this.model.Boss.CX + (this.model.Boss.Area.Width / 2)) - (this.model.Player.CX + (this.model.Player.Area.Width / 2))) <= 10)
                {
                    Item bullet = new (ItemType.BossBullet, this.model.Player.CX + (this.model.Player.Area.Width / 2), this.model.Boss.CY + this.model.Boss.Area.Height);
                    this.model.Items.Add(bullet);
                }

                if (this.model.Boss.HealthPoint <= 0)
                {
                    delete = true;
                }

                if (delete)
                {
                    this.model.Boss.IsVisible = false;
                    this.model.GameStatus = 1;
                    if (this.model.Level < 6)
                    {
                        this.model.Level++;
                    }

                    if (this.model.Level == 6)
                    {
                        this.model.GameStatus = 2;
                    }
                }
            }
        }

        private void EnemyTick()
        {
            List<Enemy> delEnemies = new ();
            foreach (Enemy enemy in this.model.Enemies)
            {
                if (enemy != null && enemy.IsVisible)
                {
                    bool delete = false;

                    double cx = enemy.CX + enemy.DX;
                    double right = cx + enemy.Area.Width;
                    if (cx < 0 || right >= this.model.GetWidth())
                    {
                        enemy.DX = -enemy.DX;
                        cx = (cx < 0) ? 0 : cx;
                        cx = (right >= this.model.GetWidth()) ? this.model.GetWidth() - enemy.Area.Width : cx;
                    }

                    if (enemy.CX != cx)
                    {
                        enemy.CX = cx;
                    }

                    double cy = enemy.CY + enemy.DY;
                    if (cy < 0 || cy > this.model.GetHeight())
                    {
                        delete = true;
                    }

                    if (enemy.CY != cy)
                    {
                        enemy.CY = cy;
                    }

                    if (Math.Abs((enemy.CX + (enemy.Area.Width / 2)) - (this.model.Player.CX + (this.model.Player.Area.Width / 2))) <= 10)
                    {
                        Item bullet = new (ItemType.EnemyBullet, this.model.Player.CX + (this.model.Player.Area.Width / 2), enemy.CY + enemy.Area.Height);
                        this.model.Items.Add(bullet);
                    }

                    if (enemy.HealthPoint <= 0)
                    {
                        delete = true;
                    }

                    if (delete)
                    {
                        delEnemies.Add(enemy);
                    }
                }
            }

            foreach (Enemy enemy in delEnemies)
            {
                this.model.Enemies.Remove(enemy);
            }
        }

        private void ItemsTick()
        {
            Player player = this.model.Player;
            Boss boss = this.model.Boss;

            List<Item> delItems = new ();

            foreach (Item item in this.model.Items)
            {
                bool delete = false;
                if (player.IsCollision(item))
                {
                    double playerDamage = 0.0;
                    switch (item.ItemType)
                    {
                        case ItemType.EnemyBullet:
                            playerDamage = (this.model.Enemies.Count > 0) ? this.model.Enemies.FirstOrDefault().Damage : 0;
                            break;
                        case ItemType.BossBullet:
                            playerDamage = boss == null ? 0 : boss.Damage;
                            break;
                        case ItemType.Asteroid:
                            playerDamage = this.random.NextDouble() * 300;
                            break;
                        case ItemType.Crystal:
                            this.model.IncreaseScore(10);
                            player.CollectedCrystalsCount++;
                            if (player.CollectedCrystalsCount >= 3)
                            {
                                player.PlayerRayGun.Available = true;
                            }

                            break;
                        case ItemType.Part:
                            this.model.IncreaseScore(10);
                            player.CollectedPartsCount++;
                            break;
                        default:
                            break;
                    }

                    delete = true;
                    if (playerDamage > 0.0)
                    {
                        player.HealthPoint -= (1 - player.PlayerShield.DamageReduction) * playerDamage;
                        if (player.PlayerShield.DamageReduction > 0)
                        {
                            player.PlayerShield.DamageReduction -= 0.1 * (playerDamage / 300) * player.PlayerShield.Level;
                            if (player.PlayerShield.DamageReduction < 0)
                            {
                                player.PlayerShield.DamageReduction = 0;
                            }
                        }

                        if (player.HealthPoint <= 0)
                        {
                            this.model.GameStatus = -1;
                        }
                    }
                }

                if (boss != null && boss.IsVisible && boss.IsCollision(item))
                {
                    double bossDamage = 0.0;
                    switch (item.ItemType)
                    {
                        case ItemType.PlayerBullet:
                            bossDamage = player.PlayerCannon.Damage;
                            break;
                        case ItemType.PlayerRay:
                            bossDamage = player.PlayerRayGun.Damage;
                            break;
                    }

                    if (bossDamage > 0.0)
                    {
                        delete = true;
                        boss.HealthPoint -= bossDamage;
                        this.model.IncreaseScore(bossDamage);
                    }
                }

                foreach (Enemy enemy in this.model.Enemies.Where(x => x.IsCollision(item)))
                {
                    double enemyDamage = 0.0;
                    switch (item.ItemType)
                    {
                        case ItemType.PlayerBullet:
                            enemyDamage = player.PlayerCannon.Damage;
                            break;
                        case ItemType.PlayerRay:
                            enemyDamage = player.PlayerRayGun.Damage;
                            break;
                    }

                    if (enemyDamage > 0.0)
                    {
                        delete = true;
                        enemy.HealthPoint -= enemyDamage;
                        this.model.IncreaseScore(enemyDamage);
                    }
                }

                double cx = item.CX + item.DX;
                double cy = item.CY + item.DY;

                delete = delete || cy < 0 || cy > this.model.GetHeight();

                if (delete)
                {
                    delItems.Add(item);
                }
                else
                {
                    item.CX = cx;
                    item.CY = cy;
                }
            }

            foreach (Item item in delItems)
            {
                this.model.Items.Remove(item);
            }
        }

        private void AddDumbItem(ItemType itemType)
        {
            this.model.Items.Add(
                new Item(
                    itemType,
                    this.random.Next(
                        (int)(this.model.GetWidth() * 0.1),
                        (int)(this.model.GetWidth() * 0.9)),
                    0));
        }

        private void AddAsteroid()
        {
            this.AddDumbItem(ItemType.Asteroid);
        }

        private void AddPart()
        {
            this.AddDumbItem(ItemType.Part);
        }

        private void AddCrystal()
        {
            this.AddDumbItem(ItemType.Crystal);
        }

        private void AddEnemy()
        {
            int x = this.random.Next(0, (int)this.model.GetWidth());
            EnemyType enemyType;
            enemyType = this.model.Level switch
            {
                1 => EnemyType.Cutter,
                2 => EnemyType.Gunboat,
                3 => EnemyType.Fury,
                4 => EnemyType.Interceptor,
                5 => EnemyType.Destroyer,
                _ => EnemyType.Cutter,
            };
            this.model.Enemies.Add(new Enemy(enemyType, x, 0));
        }

        private void AddBoss()
        {
            int x = (int)this.model.GetWidth() / 2;
            switch (this.model.Level)
            {
                case 1:
                    this.model.Boss = new Boss("Merob-zaa Keddis", x, 0);
                    break;
                case 2:
                    this.model.Boss = new Boss("Grof Kraq", x, 0);
                    break;
                case 3:
                    this.model.Boss = new Boss("Phlamvom Zekvud", x, 0);
                    break;
                case 4:
                    this.model.Boss = new Boss("Duuq Zokrorr", x, 0);
                    break;
                case 5:
                    this.model.Boss = new Boss("Darth Wralgarr", x, 0);
                    break;
                default:
                    break;
            }

            this.model.Boss.IsVisible = false;
        }
    }
}
