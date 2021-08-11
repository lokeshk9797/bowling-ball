using System;
using NUnit.Framework;

namespace BowlingBall.Tests
{
    [TestFixture]
    public class GameFixture
    {
        private Game _game;
        [SetUp]
        public void SetUp()
        {
            _game = new Game();
        }

        [Test]
        public void Game_WhenNoPinsKnocked_ScoreShouldBeZero()
        {
            Roll(_game, 0, 20);
            var result = _game.GetScore();
            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public void Game_WhenAllStrikes_MaximumScoreOf300()
        {
            //Running 12 times as during last frame we get two extra tries and they can be Strikes
            Roll(_game, 10, 12);
            var result = _game.GetScore();
            Assert.That(result, Is.EqualTo(300));
        }
        
        [Test]
        public void Game_WhenEveryFrameIsSpareWithBonusStrike_ScoreOf155()
        {
            //Knocking 5 pins in every turn
            Roll(_game, 5, 20);
            //Bonus Strike
            _game.Roll(10);
            var result = _game.GetScore();
            Assert.That(result, Is.EqualTo(155));
        }

        [Test]
        public void Game_WhenEveryFrameIsSpareWithBonusStrike_ScoreOf191()
        {
            //Knocking 9 then 1 and last strike
            int[] arr = new int[] { 9, 1, 9, 1, 9, 1, 9, 1, 9, 1, 9, 1, 9, 1, 9, 1, 9, 1, 9, 1, 10 };
            Roll(_game, arr);
            var result = _game.GetScore();
            Assert.That(result, Is.EqualTo(191));
        }

        [Test]
        public void Game_WhenEveryFrameIsSpareWithBonusStrike_ScoreOf()
        {
            //Knocking 1 then 9 and strike in last turn
            int[] arr = new int[] {  1, 9, 1, 9, 1, 9, 1, 9, 1, 9, 1, 9, 1, 9, 1, 9, 1, 9, 1,9, 10 };
            Roll(_game, arr);
            var result = _game.GetScore();
            Assert.That(result, Is.EqualTo(119));
        }
        [Test]
        public void Game_WhenNoSpareORStrike_ScoreOf90()
        {
            
            int[] arr = new int[] {  6,3,6,3,6,3,6,3,6,3,6,3,6,3,6,3,6,3,6,3 };
            Roll(_game, arr);
            var result = _game.GetScore();
            Assert.That(result, Is.EqualTo(90));
        }
        [Test]
        public void Game_WhenFirstSpareThenAllGutters_ScoreOf10()
        {
            
            int[] arr = new int[] { 2, 8, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            Roll(_game, arr);
            var result = _game.GetScore();
            Assert.That(result, Is.EqualTo(10));
        }
        [Test]
        public void Game_RealGameScore_ScoreOf187()
        {
            //Scores used in Example PDF
            int[] arr = new int[] { 10,9,1,5,5,7,2,10,10,10,9,0,8,2,9,1,10 };
            Roll(_game, arr);
            var result = _game.GetScore();
            Assert.That(result, Is.EqualTo(187));
        }



        private void Roll(Game game, int pins, int times)
        {
            for (int i = 0; i < times; i++)
            {
                game.Roll(pins);
            }
        }

        private void Roll(Game game, int[] pinss)
        {
            foreach (int pin in pinss)
            {
                game.Roll(pin);
            }
        }
    }
}
