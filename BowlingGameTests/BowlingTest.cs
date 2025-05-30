using BowlingGame;

namespace BowlingGameTests
{
    [TestClass]
    public sealed class BowlingTest
    {
        private BowlingGame.BowlingGame _game { get; set; }

        [TestInitialize]
        public void Setup()
        {
            _game = new BowlingGame.BowlingGame();
        }

        [TestMethod]
        public void CanThrowOutOfRangeException()
        {
            ArgumentOutOfRangeException ex = Assert.ThrowsException<ArgumentOutOfRangeException>(() => _game.Roll(-1));
            Assert.IsNotNull(ex);
        }
        [TestMethod]
        public void CanThrowInvalidOperationException()
        {
            _game.PlayRandomGame();
            InvalidOperationException ex = Assert.ThrowsException<InvalidOperationException>(() => _game.Roll(1));
            Assert.IsNotNull(ex);
        }
        [TestMethod]
        public void CanPlayRandomGame()
        {
            _game.PlayRandomGame();
            Assert.IsTrue(_game.Score() >= 0 && _game.Score() <= 300, "Score should be between 0 and 300 for a valid game.");
        }
        [TestMethod]
        public void CanPlayGame1()
        {
            int[] game = [
                1, 4, // Frame 1
                4, 5, // Frame 2
                6, 3, // Frame 3
                5, 4, // Frame 4
                10,0, // Frame 5 (strike) 43
                0, 1, // Frame 6
                7, 3, // Frame 7 (spare)  60
                6, 4, // Frame 8 (spare)  80
                10, 0,// Frame 9 (strike) 100
                2, 8, // Final frame (spare) 116
                6     // Bonus roll after spare
            ];
            _game.Reset();
            for (int i = 0;  i < game.Length; i++)
            {
                _game.Roll(game[i]);
            };
            _game.PrintGame();
            Assert.AreEqual(116, _game.Score(), "Score should be 116 for the given game rolls.");
        }
        [TestMethod]
        public void CanPlayGame2()
        {
            int[] game = [
                4, 3, // Frame 1
                7, 3, // Frame 2 (spare) 22
                5, 2, // Frame 3
                8, 1, // Frame 4
                4, 6, // Frame 5 (spare) 50
                2, 4, // Frame 6
                8, 0, // Frame 7 
                8, 0, // Frame 8 
                8, 2, // Frame 9 (spare) 92
                10, 1, // Final frame (strike) 110
                7     // Bonus roll after strike
                ];
            _game.Reset();
            for (int i = 0; i < game.Length; i++)
            {
                _game.Roll(game[i]);
            }
            ;
            _game.PrintGame();
            Assert.AreEqual(110, _game.Score(), "Score should be 110 for the given game rolls.");
        }
        [TestMethod]
        public void CanPlayGame3()
        {
            int[] game = [
                4, 3, // Frame 1
                7, 3, // Frame 2 (spare) 22
                5, 2, // Frame 3
                8, 1, // Frame 4
                4, 6, // Frame 5 (spare) 50
                2, 4, // Frame 6
                8, 0, // Frame 7 
                8, 0, // Frame 8 
                8, 2, // Frame 9 (spare) 90
                8, 1, // Final frame (strike) 99
                ];
            _game.Reset();
            for (int i = 0; i < game.Length; i++)
            {
                _game.Roll(game[i]);
            }
           ;
            _game.PrintGame();
            Assert.AreEqual(99, _game.Score(), "Score should be 99 for the given game rolls.");
        }
        [TestMethod]
        public void CanPlayGame4()
        {
            int[] game = [
                4, 3, // Frame 1
                7, 3, // Frame 2 (spare) 22
                5, 2, // Frame 3
                8, 1, // Frame 4
                4, 6, // Frame 5 (spare) 50
                2, 4, // Frame 6
                8, 0, // Frame 7 
                8, 0, // Frame 8 
                8, 2, // Frame 9 (spare) 92
                10, 10, // Final frame (strike) 122
                10
                ];
            _game.Reset();
            for (int i = 0; i < game.Length; i++)
            {
                _game.Roll(game[i]);
            }
          ;
            _game.PrintGame();
            Assert.AreEqual(122, _game.Score(), "Score should be 122 for the given game rolls.");
        }
    }
}
