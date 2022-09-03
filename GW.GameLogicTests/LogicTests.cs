// <copyright file="LogicTests.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace GW.GameLogicTests
{
    using System.Collections.Generic;
    using GW.GameLogic;
    using GW.GameModel;
    using GW.GameRepository;
    using Moq;
    using NUnit.Framework;

    /// <summary>
    /// Game Logic tests.
    /// </summary>
    [TestFixture]
    public class LogicTests
    {
        private Mock<IGameModel> mockedGameModel;
        private Mock<IStorageRepository> mockedRepository;
        private GWLogic testGameLogic;

        /// <summary>
        /// This method initializes mocked dependencies and a new GWLogic.
        /// </summary>
        [SetUp]
        public void Init()
        {
            this.mockedGameModel = new Mock<IGameModel>();
            this.mockedRepository = new Mock<IStorageRepository>();
            this.testGameLogic = new GWLogic(this.mockedGameModel.Object, this.mockedRepository.Object);
        }

        /// <summary>
        /// MovePlayer method boundary check test.
        /// </summary>
        [Test]
        public void MovePlayerUpOutTest()
        {
            Player testPlayer = new (1, 1, 10, 10, 0, 0);
            testPlayer.Area = new System.Windows.Rect(testPlayer.CX, testPlayer.CY, 80, 110);
            this.mockedGameModel.Setup(x => x.Player).Returns(testPlayer);
            this.mockedGameModel.Setup(x => x.GetHeight()).Returns(700);
            this.mockedGameModel.Setup(x => x.GetWidth()).Returns(1100);

            this.testGameLogic.MovePlayer(Direction.Up);

            Assert.That(testPlayer.CX, Is.EqualTo(1));
            Assert.That(testPlayer.CY, Is.EqualTo(0));
        }

        /// <summary>
        /// MovePlayer method boundary check test.
        /// </summary>
        [Test]
        public void MovePlayerUpNormalTest()
        {
            Player testPlayer = new (500, 500, 10, 10, 0, 0);
            testPlayer.Area = new System.Windows.Rect(testPlayer.CX, testPlayer.CY, 80, 110);
            this.mockedGameModel.Setup(x => x.Player).Returns(testPlayer);
            this.mockedGameModel.Setup(x => x.GetHeight()).Returns(700);
            this.mockedGameModel.Setup(x => x.GetWidth()).Returns(1100);

            this.testGameLogic.MovePlayer(Direction.Up);

            Assert.That(testPlayer.CX, Is.EqualTo(500));
            Assert.That(testPlayer.CY, Is.EqualTo(490));
        }

        /// <summary>
        /// MovePlayer method boundary check test.
        /// </summary>
        [Test]
        public void MovePlayerLeftOutTest()
        {
            Player testPlayer = new (1, 1, 10, 10, 0, 0);
            testPlayer.Area = new System.Windows.Rect(testPlayer.CX, testPlayer.CY, 80, 110);
            this.mockedGameModel.Setup(x => x.Player).Returns(testPlayer);
            this.mockedGameModel.Setup(x => x.GetHeight()).Returns(700);
            this.mockedGameModel.Setup(x => x.GetWidth()).Returns(1100);

            this.testGameLogic.MovePlayer(Direction.Left);

            Assert.That(testPlayer.CX, Is.EqualTo(0));
            Assert.That(testPlayer.CY, Is.EqualTo(1));
        }

        /// <summary>
        /// MovePlayer method boundary check test.
        /// </summary>
        [Test]
        public void MovePlayerLeftNormalTest()
        {
            Player testPlayer = new (500, 500, 10, 10, 0, 0);
            testPlayer.Area = new System.Windows.Rect(testPlayer.CX, testPlayer.CY, 80, 110);
            this.mockedGameModel.Setup(x => x.Player).Returns(testPlayer);
            this.mockedGameModel.Setup(x => x.GetHeight()).Returns(700);
            this.mockedGameModel.Setup(x => x.GetWidth()).Returns(1100);

            this.testGameLogic.MovePlayer(Direction.Left);

            Assert.That(testPlayer.CX, Is.EqualTo(490));
            Assert.That(testPlayer.CY, Is.EqualTo(500));
        }

        /// <summary>
        /// MovePlayer method boundary check test.
        /// </summary>
        [Test]
        public void MovePlayerDownOutTest()
        {
            Player testPlayer = new (500, 590, 10, 10, 0, 0);
            testPlayer.Area = new System.Windows.Rect(testPlayer.CX, testPlayer.CY, 80, 110);
            this.mockedGameModel.Setup(x => x.Player).Returns(testPlayer);
            this.mockedGameModel.Setup(x => x.GetHeight()).Returns(700);
            this.mockedGameModel.Setup(x => x.GetWidth()).Returns(1100);

            this.testGameLogic.MovePlayer(Direction.Down);

            Assert.That(testPlayer.CX, Is.EqualTo(500));
            Assert.That(testPlayer.CY, Is.EqualTo(590));
        }

        /// <summary>
        /// MovePlayer method boundary check test.
        /// </summary>
        [Test]
        public void MovePlayerDownNormalTest()
        {
            Player testPlayer = new (500, 500, 10, 10, 0, 0);
            testPlayer.Area = new System.Windows.Rect(testPlayer.CX, testPlayer.CY, 80, 110);
            this.mockedGameModel.Setup(x => x.Player).Returns(testPlayer);
            this.mockedGameModel.Setup(x => x.GetHeight()).Returns(700);
            this.mockedGameModel.Setup(x => x.GetWidth()).Returns(1100);

            this.testGameLogic.MovePlayer(Direction.Down);

            Assert.That(testPlayer.CX, Is.EqualTo(500));
            Assert.That(testPlayer.CY, Is.EqualTo(510));
        }

        /// <summary>
        /// MovePlayer method boundary check test.
        /// </summary>
        [Test]
        public void MovePlayerRightOutTest()
        {
            Player testPlayer = new (1020, 500, 10, 10, 0, 0);
            testPlayer.Area = new System.Windows.Rect(testPlayer.CX, testPlayer.CY, 80, 110);
            this.mockedGameModel.Setup(x => x.Player).Returns(testPlayer);
            this.mockedGameModel.Setup(x => x.GetHeight()).Returns(700);
            this.mockedGameModel.Setup(x => x.GetWidth()).Returns(1100);

            this.testGameLogic.MovePlayer(Direction.Right);

            Assert.That(testPlayer.CX, Is.EqualTo(1020));
            Assert.That(testPlayer.CY, Is.EqualTo(500));
        }

        /// <summary>
        /// MovePlayer method boundary check test.
        /// </summary>
        [Test]
        public void MovePlayerRightNormalTest()
        {
            Player testPlayer = new (500, 500, 10, 10, 0, 0);
            testPlayer.Area = new System.Windows.Rect(testPlayer.CX, testPlayer.CY, 80, 110);
            this.mockedGameModel.Setup(x => x.Player).Returns(testPlayer);
            this.mockedGameModel.Setup(x => x.GetHeight()).Returns(700);
            this.mockedGameModel.Setup(x => x.GetWidth()).Returns(1100);

            this.testGameLogic.MovePlayer(Direction.Right);

            Assert.That(testPlayer.CX, Is.EqualTo(510));
            Assert.That(testPlayer.CY, Is.EqualTo(500));
        }

        /// <summary>
        /// RepairShield check test.
        /// </summary>
        [Test]
        public void RepairShieldLevel2NormalTest()
        {
            Player testPlayer = new (500, 500, 10, 10, 0, 0);
            testPlayer.CollectedPartsCount = 2;
            testPlayer.PlayerShield = new Shield(2)
            {
                DamageReduction = 0,
            };
            this.mockedGameModel.Setup(x => x.Player).Returns(testPlayer);

            this.testGameLogic.RepairShield();

            Assert.That(testPlayer.CollectedPartsCount, Is.Zero);
            Assert.That(testPlayer.PlayerShield.DamageReduction, Is.EqualTo(0.2));
        }

        /// <summary>
        /// RepairShield check test.
        /// </summary>
        [Test]
        public void RepairShieldLevel2NotEnoughPartsTest()
        {
            Player testPlayer = new (500, 500, 10, 10, 0, 0);
            testPlayer.CollectedPartsCount = 0;
            testPlayer.PlayerShield = new Shield(2)
            {
                DamageReduction = 0,
            };
            this.mockedGameModel.Setup(x => x.Player).Returns(testPlayer);

            this.testGameLogic.RepairShield();

            Assert.That(testPlayer.CollectedPartsCount, Is.Zero);
            Assert.That(testPlayer.PlayerShield.DamageReduction, Is.EqualTo(0));
        }

        /// <summary>
        /// RepairShield check test.
        /// </summary>
        [Test]
        public void RepairShieldLevel3NormalTest()
        {
            Player testPlayer = new (500, 500, 10, 10, 0, 0);
            testPlayer.CollectedPartsCount = 3;
            testPlayer.PlayerShield = new Shield(3)
            {
                DamageReduction = 0,
            };
            this.mockedGameModel.Setup(x => x.Player).Returns(testPlayer);

            this.testGameLogic.RepairShield();

            Assert.That(testPlayer.CollectedPartsCount, Is.Zero);
            Assert.That(testPlayer.PlayerShield.DamageReduction, Is.EqualTo(0.3));
        }

        /// <summary>
        /// RepairShield check test.
        /// </summary>
        [Test]
        public void RepairShieldLevel3NotEnoughPartsTest()
        {
            Player testPlayer = new (500, 500, 10, 10, 0, 0);
            testPlayer.CollectedPartsCount = 0;
            testPlayer.PlayerShield = new Shield(3)
            {
                DamageReduction = 0,
            };
            this.mockedGameModel.Setup(x => x.Player).Returns(testPlayer);

            this.testGameLogic.RepairShield();

            Assert.That(testPlayer.CollectedPartsCount, Is.Zero);
            Assert.That(testPlayer.PlayerShield.DamageReduction, Is.EqualTo(0));
        }

        /// <summary>
        /// RepairShield check test.
        /// </summary>
        [Test]
        public void RepairShieldLevel4NormalTest()
        {
            Player testPlayer = new (500, 500, 10, 10, 0, 0);
            testPlayer.CollectedPartsCount = 4;
            testPlayer.PlayerShield = new Shield(4)
            {
                DamageReduction = 0,
            };
            this.mockedGameModel.Setup(x => x.Player).Returns(testPlayer);

            this.testGameLogic.RepairShield();

            Assert.That(testPlayer.CollectedPartsCount, Is.Zero);
            Assert.That(testPlayer.PlayerShield.DamageReduction, Is.EqualTo(0.4));
        }

        /// <summary>
        /// RepairShield check test.
        /// </summary>
        [Test]
        public void RepairShieldLevel4NotEnoughPartsTest()
        {
            Player testPlayer = new (500, 500, 10, 10, 0, 0);
            testPlayer.CollectedPartsCount = 0;
            testPlayer.PlayerShield = new Shield(4)
            {
                DamageReduction = 0,
            };
            this.mockedGameModel.Setup(x => x.Player).Returns(testPlayer);

            this.testGameLogic.RepairShield();

            Assert.That(testPlayer.CollectedPartsCount, Is.Zero);
            Assert.That(testPlayer.PlayerShield.DamageReduction, Is.EqualTo(0));
        }

        /// <summary>
        /// RepairShield check test.
        /// </summary>
        [Test]
        public void RepairShieldLevel5NormalTest()
        {
            Player testPlayer = new (500, 500, 10, 10, 0, 0);
            testPlayer.CollectedPartsCount = 5;
            testPlayer.PlayerShield = new Shield(5)
            {
                DamageReduction = 0,
            };
            this.mockedGameModel.Setup(x => x.Player).Returns(testPlayer);

            this.testGameLogic.RepairShield();

            Assert.That(testPlayer.CollectedPartsCount, Is.Zero);
            Assert.That(testPlayer.PlayerShield.DamageReduction, Is.EqualTo(0.5));
        }

        /// <summary>
        /// RepairShield check test.
        /// </summary>
        [Test]
        public void RepairShieldLevel5NotEnoughPartsTest()
        {
            Player testPlayer = new (500, 500, 10, 10, 0, 0);
            testPlayer.CollectedPartsCount = 0;
            testPlayer.PlayerShield = new Shield(5)
            {
                DamageReduction = 0,
            };
            this.mockedGameModel.Setup(x => x.Player).Returns(testPlayer);

            this.testGameLogic.RepairShield();

            Assert.That(testPlayer.CollectedPartsCount, Is.Zero);
            Assert.That(testPlayer.PlayerShield.DamageReduction, Is.EqualTo(0));
        }

        /// <summary>
        /// UpgradeShield check test.
        /// </summary>
        [Test]
        public void UpgradeShieldLevel1NormalTest()
        {
            Player testPlayer = new (500, 500, 10, 10, 0, 0)
            {
                CollectedPartsCount = 4,
                PlayerShield = new Shield(1),
            };
            this.mockedGameModel.Setup(x => x.Player).Returns(testPlayer);

            this.testGameLogic.UpgradeShield();

            Assert.That(testPlayer.CollectedPartsCount, Is.Zero);
            Assert.That(testPlayer.PlayerShield.Level, Is.EqualTo(2));
        }

        /// <summary>
        /// UpgradeShield check test.
        /// </summary>
        [Test]
        public void UpgradeShieldLevel1NotEnoughPartsTest()
        {
            Player testPlayer = new (500, 500, 10, 10, 0, 0);
            testPlayer.CollectedPartsCount = 0;
            testPlayer.PlayerShield = new Shield(1);
            this.mockedGameModel.Setup(x => x.Player).Returns(testPlayer);

            this.testGameLogic.UpgradeShield();

            Assert.That(testPlayer.CollectedPartsCount, Is.Zero);
            Assert.That(testPlayer.PlayerShield.Level, Is.EqualTo(1));
        }

         /// <summary>
        /// UpgradeShield check test.
        /// </summary>
        [Test]
        public void UpgradeShieldLevel2NormalTest()
        {
            Player testPlayer = new (500, 500, 10, 10, 0, 0)
            {
                CollectedPartsCount = 6,
                PlayerShield = new Shield(2),
            };
            this.mockedGameModel.Setup(x => x.Player).Returns(testPlayer);

            this.testGameLogic.UpgradeShield();

            Assert.That(testPlayer.CollectedPartsCount, Is.Zero);
            Assert.That(testPlayer.PlayerShield.Level, Is.EqualTo(3));
        }

        /// <summary>
        /// UpgradeShield check test.
        /// </summary>
        [Test]
        public void UpgradeShieldLevel2NotEnoughPartsTest()
        {
            Player testPlayer = new (500, 500, 10, 10, 0, 0)
            {
                CollectedPartsCount = 0,
                PlayerShield = new Shield(2),
            };
            this.mockedGameModel.Setup(x => x.Player).Returns(testPlayer);

            this.testGameLogic.UpgradeShield();

            Assert.That(testPlayer.CollectedPartsCount, Is.Zero);
            Assert.That(testPlayer.PlayerShield.Level, Is.EqualTo(2));
        }

        /// <summary>
        /// UpgradeShield check test.
        /// </summary>
        [Test]
        public void UpgradeShieldLevel3NormalTest()
        {
            Player testPlayer = new (500, 500, 10, 10, 0, 0)
            {
                CollectedPartsCount = 9,
                PlayerShield = new Shield(3),
            };
            this.mockedGameModel.Setup(x => x.Player).Returns(testPlayer);

            this.testGameLogic.UpgradeShield();

            Assert.That(testPlayer.CollectedPartsCount, Is.Zero);
            Assert.That(testPlayer.PlayerShield.Level, Is.EqualTo(4));
        }

        /// <summary>
        /// UpgradeShield check test.
        /// </summary>
        [Test]
        public void UpgradeShieldLevel3NotEnoughPartsTest()
        {
            Player testPlayer = new (500, 500, 10, 10, 0, 0)
            {
                CollectedPartsCount = 0,
                PlayerShield = new Shield(3),
            };
            this.mockedGameModel.Setup(x => x.Player).Returns(testPlayer);

            this.testGameLogic.UpgradeShield();

            Assert.That(testPlayer.CollectedPartsCount, Is.Zero);
            Assert.That(testPlayer.PlayerShield.Level, Is.EqualTo(3));
        }

        /// <summary>
        /// UpgradeShield check test.
        /// </summary>
        [Test]
        public void UpgradeShieldLevel4NormalTest()
        {
            Player testPlayer = new (500, 500, 10, 10, 0, 0)
            {
                CollectedPartsCount = 12,
                PlayerShield = new Shield(4),
            };
            this.mockedGameModel.Setup(x => x.Player).Returns(testPlayer);

            this.testGameLogic.UpgradeShield();

            Assert.That(testPlayer.CollectedPartsCount, Is.Zero);
            Assert.That(testPlayer.PlayerShield.Level, Is.EqualTo(5));
        }

        /// <summary>
        /// UpgradeShield check test.
        /// </summary>
        [Test]
        public void UpgradeShieldLevel4NotEnoughPartsTest()
        {
            Player testPlayer = new (500, 500, 10, 10, 0, 0)
            {
                CollectedPartsCount = 0,
                PlayerShield = new Shield(4),
            };
            this.mockedGameModel.Setup(x => x.Player).Returns(testPlayer);

            this.testGameLogic.UpgradeShield();

            Assert.That(testPlayer.CollectedPartsCount, Is.Zero);
            Assert.That(testPlayer.PlayerShield.Level, Is.EqualTo(4));
        }

        /// <summary>
        /// UpgradeShield check test.
        /// </summary>
        [Test]
        public void UpgradeShieldLevel5NormalTest()
        {
            Player testPlayer = new (500, 500, 10, 10, 0, 0)
            {
                CollectedPartsCount = int.MaxValue,
                PlayerShield = new Shield(5),
            };
            this.mockedGameModel.Setup(x => x.Player).Returns(testPlayer);

            this.testGameLogic.UpgradeShield();

            Assert.That(testPlayer.CollectedPartsCount, Is.EqualTo(int.MaxValue));
            Assert.That(testPlayer.PlayerShield.Level, Is.EqualTo(5));
        }

        /// <summary>
        /// UpgradeCannon test.
        /// </summary>
        [Test]
        public void UpgradeCannonLevel1NormalTest()
        {
            Player testPlayer = new (500, 500, 10, 10, 0, 0)
            {
                CollectedPartsCount = 6,
                PlayerCannon = new Cannon(1),
            };
            this.mockedGameModel.Setup(x => x.Player).Returns(testPlayer);

            this.testGameLogic.UpgradeCannon();

            Assert.That(testPlayer.CollectedPartsCount, Is.EqualTo(0));
            Assert.That(testPlayer.PlayerCannon.Level, Is.EqualTo(2));
        }

        /// <summary>
        /// UpgradeCannon test.
        /// </summary>
        [Test]
        public void UpgradeCannonLevel1NotEnoughPartsTest()
        {
            Player testPlayer = new (500, 500, 10, 10, 0, 0)
            {
                CollectedPartsCount = 0,
                PlayerCannon = new Cannon(1),
            };
            this.mockedGameModel.Setup(x => x.Player).Returns(testPlayer);

            this.testGameLogic.UpgradeCannon();

            Assert.That(testPlayer.CollectedPartsCount, Is.Zero);
            Assert.That(testPlayer.PlayerCannon.Level, Is.EqualTo(1));
        }

        /// <summary>
        /// UpgradeCannon test.
        /// </summary>
        [Test]
        public void UpgradeCannonLevel2NormalTest()
        {
            Player testPlayer = new (500, 500, 10, 10, 0, 0)
            {
                CollectedPartsCount = 12,
                PlayerCannon = new Cannon(2),
            };
            this.mockedGameModel.Setup(x => x.Player).Returns(testPlayer);

            this.testGameLogic.UpgradeCannon();

            Assert.That(testPlayer.CollectedPartsCount, Is.Zero);
            Assert.That(testPlayer.PlayerCannon.Level, Is.EqualTo(3));
        }

        /// <summary>
        /// UpgradeCannon test.
        /// </summary>
        [Test]
        public void UpgradeCannonLevel2NotEnoughPartsTest()
        {
            Player testPlayer = new (500, 500, 10, 10, 0, 0)
            {
                CollectedPartsCount = 0,
                PlayerCannon = new Cannon(2),
            };
            this.mockedGameModel.Setup(x => x.Player).Returns(testPlayer);

            this.testGameLogic.UpgradeCannon();

            Assert.That(testPlayer.CollectedPartsCount, Is.Zero);
            Assert.That(testPlayer.PlayerCannon.Level, Is.EqualTo(2));
        }

        /// <summary>
        /// UpgradeCannon test.
        /// </summary>
        [Test]
        public void UpgradeCannonLevel3NormalTest()
        {
            Player testPlayer = new (500, 500, 10, 10, 0, 0)
            {
                CollectedPartsCount = int.MaxValue,
                PlayerCannon = new Cannon(3),
            };
            this.mockedGameModel.Setup(x => x.Player).Returns(testPlayer);

            this.testGameLogic.UpgradeCannon();

            Assert.That(testPlayer.CollectedPartsCount, Is.EqualTo(int.MaxValue));
            Assert.That(testPlayer.PlayerCannon.Level, Is.EqualTo(3));
        }

        /// <summary>
        /// ShootCannon test.
        /// </summary>
        [Test]
        public void ShootCannonTest()
        {
            Player testPlayer = new (500, 500, 10, 10, 0, 0)
            {
                CollectedPartsCount = int.MaxValue,
                PlayerCannon = new Cannon(1),
            };
            this.mockedGameModel.Setup(x => x.Player).Returns(testPlayer);
            List<Item> testItems = new ();
            this.mockedGameModel.Setup(x => x.Items).Returns(testItems);

            this.testGameLogic.ShootCannon();

            Assert.That(testItems.Count, Is.EqualTo(1));
            Assert.That(testItems[0].ItemType, Is.EqualTo(ItemType.PlayerBullet));
        }

        /// <summary>
        /// ShootRaygun test.
        /// </summary>
        [Test]
        public void ShootRaygunTestNormal()
        {
            Player testPlayer = new (500, 500, 10, 10, 0, 0)
            {
                CollectedCrystalsCount = 3,
            };
            testPlayer.PlayerRayGun.Available = true;
            this.mockedGameModel.Setup(x => x.Player).Returns(testPlayer);
            List<Item> testItems = new ();
            this.mockedGameModel.Setup(x => x.Items).Returns(testItems);

            this.testGameLogic.ShootRaygun();

            Assert.That(testPlayer.CollectedCrystalsCount, Is.Zero);
            Assert.That(testItems.Count, Is.EqualTo(1));
            Assert.That(testItems[0].ItemType, Is.EqualTo(ItemType.PlayerRay));
        }

        /// <summary>
        /// ShootRaygun test.
        /// </summary>
        [Test]
        public void ShootRaygunTestNotEnoughCrystals()
        {
            Player testPlayer = new (500, 500, 10, 10, 0, 0)
            {
                CollectedPartsCount = 0,
            };
            testPlayer.PlayerRayGun.Available = false;
            this.mockedGameModel.Setup(x => x.Player).Returns(testPlayer);
            List<Item> testItems = new ();
            this.mockedGameModel.Setup(x => x.Items).Returns(testItems);

            this.testGameLogic.ShootRaygun();

            Assert.That(testPlayer.CollectedCrystalsCount, Is.Zero);
            Assert.That(testItems.Count, Is.Zero);
        }
    }
}
