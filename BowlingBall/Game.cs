using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BowlingBall.Models;

namespace BowlingBall
{
    public class Game
    {
        private readonly int totalNoOfFrames = 10;
        private readonly int maxNoOFPins = 10;
        private readonly List<Frame> frames = new List<Frame>();
        private int score;

        public void Roll(int pins)
        {
            //Creating a new frame for first time or if last frame is Completed
            if (!frames.Any() || frames.Last().IsFrameCompleted)
            {
                var isLastFrame = frames.Count == totalNoOfFrames - 1;

                frames.Add(new Frame(isLastFrame));
            }
            frames.Last().RollResults.Add(pins);
            frames.Last().PinsLeft -= pins;

            //Bonus throw for last round if strike or spare
            if (frames.Last().isLastFrame && frames.Last().PinsLeft == 0)
            {
                PlayLastFrameBonus(frames.Last());
            }
        }

        private void PlayLastFrameBonus(Frame frame)
        {
            frame.PinsLeft = maxNoOFPins;
            frame.ExtraRollAllowed = true;

        }


        public int GetScore()
        {
            for (int frameIndex = 0; frameIndex < frames.Count; frameIndex++)
            {
                var frame = frames[frameIndex];
                var frameScore = 0;
                var bonusScore = 0;
                var isStrike = false;
                var maxRollInFrame = frame.RollResults.Count < 2 ? frame.RollResults.Count : 2;

                for (int rollIndex = 0; rollIndex < maxRollInFrame; rollIndex++)
                {
                    var result = frame.RollResults[rollIndex];
                    frameScore = frameScore + result;

                    //Checking for Strike
                    if (result == maxNoOFPins)
                    {
                        isStrike = true;
                        bonusScore += CalculateBonus(frameIndex, rollIndex, 2);
                        break;
                    }

                }

                //Checking for Spare
                if (!isStrike && frameScore == maxNoOFPins)
                {
                    bonusScore += CalculateBonus(frameIndex, maxRollInFrame - 1, 1);
                }

                score += frameScore + bonusScore;

            }
            return score;
        }

        private int CalculateBonus(int frameIndex, int rollIndex, int rollCount)
        {
            int bonusPoints = 0;

            if (rollCount == 0)
            {
                return 0;
            }

            if (frames[frameIndex].RollResults.Count > rollIndex + 1)
            {
                bonusPoints += frames[frameIndex].RollResults[rollIndex + 1];
                bonusPoints += CalculateBonus(frameIndex, rollIndex + 1, rollCount - 1);
            }
            else
            {
                if (frames.Count > frameIndex + 1)
                {
                    bonusPoints += frames[frameIndex + 1].RollResults[0];
                    bonusPoints += CalculateBonus(frameIndex + 1, 0, rollCount - 1);
                }
            }
            return bonusPoints;
        }
    }
}
