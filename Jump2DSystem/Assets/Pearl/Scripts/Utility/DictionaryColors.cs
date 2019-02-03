using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pearl
{
    public static class DictionaryColors
    {
        private static readonly Dictionary<ColorEnum, int> dictionaryColors;

        static DictionaryColors()
        {
            dictionaryColors = new Dictionary<ColorEnum, int>();
            dictionaryColors.Add(ColorEnum.Black, 0x0);
            dictionaryColors.Add(ColorEnum.Blue, 0x0000FF);
            dictionaryColors.Add(ColorEnum.Green, 0x00FF00);
            dictionaryColors.Add(ColorEnum.Red, 0xFF0000);
            dictionaryColors.Add(ColorEnum.White, 0xFFFFFF);
        }
    }
}
