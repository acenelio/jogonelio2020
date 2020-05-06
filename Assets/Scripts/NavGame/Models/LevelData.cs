using System;

namespace NavGame.Models
{
    public class LevelData
    {

        public int CoinCount { get; private set; }

        public void AddCoins(int amount)
        {
            CoinCount += amount;
        }

        public void ConsumeCoins(int amount)
        {
            ValidadeCoinAmount(amount);
            CoinCount -= amount;
        }

        public void ValidadeCoinAmount(int amount)
        {
            if (CoinCount < amount)
            {
                throw new InvalidOperationException("Need " + amount + " coins! You have only " + CoinCount);
            }
        }
    }
}
