using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CoinTracker
{
    private static int coinCount = 0;

    public static int getCoinCount()
    {
        return coinCount;
    }

    public static void setCointCount(int setCoins)
    {
        coinCount = setCoins;
    }

    public static void incrementCoinCount()
    {
        coinCount++;
    }

    public static void decrementCoinCount()
    {
        coinCount--;
    }
}