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
            ValidateCoinAmount(amount);
            CoinCount -= amount;
        }

        public void ValidateCoinAmount(int amount)
        {
            if (CoinCount < amount)
            {
                throw new InvalidOperationException("Need " + amount + " coins!");
            }
        }
    }
}
