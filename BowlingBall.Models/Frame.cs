using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BowlingBall.Models
{
    public class Frame
    {
        public readonly bool isLastFrame;
        private readonly int maxPins = 10;
        public int PinsLeft { get; set; }
        public bool ExtraRollAllowed { get; set; }
        public List<int> RollResults { get; } = new List<int>();
        public bool IsFrameCompleted => !isLastFrame && PinsLeft == 0 ||
                                        !isLastFrame && RollResults.Count == 2 ||
                                        RollResults.Count == 3;

        public Frame(bool isLastFrame = false)
        {
            this.isLastFrame = isLastFrame;
            PinsLeft = maxPins;
        }
    }
}
