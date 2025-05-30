using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BowlingGame
{
    public class BowlingGame
    {
        private int[] rolls = new int[21]; // Maximum of 21 rolls in a game
        private int currentRoll = 0;
        public void Roll(int pins)
        {
            if (pins < 0 || pins > 10)
                throw new ArgumentOutOfRangeException(nameof(pins), "Pins must be between 0 and 10.");
            if (currentRoll >= rolls.Length)
                throw new InvalidOperationException("Cannot roll more than 21 times.");
            if (rolls[18] != 10 && rolls[18] + rolls[19] != 10 && currentRoll >= rolls.Length-1)
                throw new InvalidOperationException("Cannot roll more than 20 times.");
            // Check for rolls in a frame
            if (currentRoll%2 == 1 && currentRoll < 19 && rolls[currentRoll-1] + pins > 10)
                throw new ArgumentOutOfRangeException(nameof(pins), "Pins in a frame must be between 0 and 10.");

            rolls[currentRoll++] = pins;
        }
        public int Score()
        {
            int score = 0;
            int rollIndex = 0;
            for (int frame = 0; frame < 10; frame++)
            {
                if (IsStrike(rollIndex)) // Strike
                {
                    score += 10 + (rollIndex == 18 ? StrikeBonus(rollIndex - 1) : StrikeBonus(rollIndex));
                    rollIndex += 2;
                }
                else if (IsSpare(rollIndex)) // Spare
                {
                    score += 10 + SpareBonus(rollIndex);
                    rollIndex += 2;
                }
                else // Open frame
                {
                    score += rolls[rollIndex] + rolls[rollIndex + 1];
                    rollIndex += 2;
                }
            }
            return score;
        }
        public void Reset()
        {
            rolls = new int[21]; // Reset the rolls
            currentRoll = 0; // Reset the current roll index
        }
        public void PlayRandomGame()
        {
            Reset();
            Random random = new Random();
            int pins;
            for (int frame = 0; frame < 10; frame++)
            {
                pins = random.Next(0, 11); // Randomly roll between 0 and 10 pins
                Roll(pins);
                
                pins = random.Next(0, 11 - rolls[currentRoll - 1]); // Ensure total does not exceed 10
                Roll(pins);
            }
            if (rolls[currentRoll - 1] + rolls[currentRoll - 2] == 10) // If last frame is a spare, roll 1 more time
            {
                Roll(random.Next(0, 11)); // Roll again for bonus
            }
            if (rolls[currentRoll - 1] == 0 && rolls[currentRoll - 2] == 10) // If last roll is a strike, roll 2 more time
            {
                currentRoll--;
                Roll(random.Next(0, 11));
                Roll(random.Next(0, 11)); // Roll again for bonus
            }
            PrintGame();
        }
        public void PrintGame()
        {
            for (int i = 0; i < currentRoll-1; i++)
            {
                Console.Write(rolls[i++] + " ");
                Console.Write(rolls[i] + " \n");
            }
            if(currentRoll % 2 == 1) // If the last roll is a single roll, print it separately
            {
                Console.WriteLine(rolls[currentRoll - 1]);
            }          
            Console.WriteLine($"Current game score = {Score()}");
        }
        private bool IsStrike(int rollIndex) => rolls[rollIndex] == 10;
        private bool IsSpare(int rollIndex) => rolls[rollIndex] + rolls[rollIndex + 1] == 10;
        private int StrikeBonus(int rollIndex) => rolls[rollIndex + 2] + rolls[rollIndex + 3];
        private int SpareBonus(int rollIndex) => rolls[rollIndex + 2];
    }
}
