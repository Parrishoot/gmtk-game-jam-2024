using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class ArrayUtils
{

    // Modified from: https://www.noveltech.dev/csharp-flatten-array   
    public static T[] Flatten<T>(this T[,] input) {

        int width = input.GetLength(0);
        int height = input.GetLength(1);
        T[] flattened = new T[width * height];

        for (int j = 0; j < height; j++)
        {
            for (int i = 0; i < width; i++)
            {
                flattened[j * width + i] = input[i, j];

            }
        }

        return flattened;
    }


    public static T[,] Unflatten<T>(this T[] input, int width, int height)
    {
        T[,] unflattened = new T[width, height];

        for (int j = 0; j < height; j++)
        {
            for (int i = 0; i < width; i++)
            {
                unflattened[i, j] = input[j * width + i];

            }
        }

        return unflattened;
    }
}
