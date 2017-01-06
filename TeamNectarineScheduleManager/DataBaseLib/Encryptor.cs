using System;
using System.Linq;
using System.Text;

namespace TeamNectarineScheduleManager.DataBaseLibrary
{
    public static class Encryptor
    {
        private static Random rnd = new Random();
        // 1. collapse string to chars
        // 2. encode each char and build a new string from the encoded chars
        // 3. offset position
        // 4. offset char num
        // 5. Reverse(if needed) 
        // 6. add decryption chars at the end.

        #region
        private static byte[][] charLengthMode = new byte[][]{
            new byte[]{9, 6, 8, 3, 4, 2, 1, 5, 6, 4, 5, 8, 8, 3, 1, 7, 4, 6, 4, 1},
            new byte[]{9, 6, 8, 2, 3, 1, 8, 6, 1, 9, 7, 3, 9, 9, 3, 2, 3, 6, 4, 6},
            new byte[]{9, 5, 9, 3, 5, 4, 1, 5, 4, 6, 2, 4, 7, 6, 9, 3, 8, 6, 1, 3},
            new byte[]{3, 5, 4, 7, 7, 2, 9, 9, 9, 9, 5, 7, 3, 7, 2, 4, 6, 5, 7, 8},
            new byte[]{1, 4, 3, 4, 8, 4, 5, 3, 9, 9, 4, 6, 6, 1, 3, 5, 2, 2, 6, 3},
            new byte[]{4, 2, 5, 4, 2, 4, 2, 9, 5, 5, 7, 3, 9, 2, 3, 9, 2, 1, 9, 4},
            new byte[]{6, 6, 9, 1, 6, 9, 2, 2, 5, 7, 3, 4, 8, 4, 4, 8, 9, 8, 3, 4},
            new byte[]{3, 5, 8, 4, 3, 1, 7, 4, 7, 4, 3, 2, 3, 3, 6, 8, 3, 5, 8, 3},
            new byte[]{2, 9, 5, 7, 3, 3, 4, 5, 7, 5, 9, 3, 7, 5, 1, 2, 8, 5, 8, 5},
            new byte[]{2, 7, 5, 2, 4, 2, 2, 6, 7, 6, 6, 5, 1, 9, 3, 4, 1, 1, 2, 9},
            new byte[]{6, 2, 7, 3, 4, 1, 4, 8, 2, 5, 1, 8, 7, 3, 1, 5, 4, 4, 4, 2},
            new byte[]{2, 8, 7, 1, 9, 5, 2, 7, 3, 7, 1, 7, 3, 8, 9, 1, 1, 3, 7, 3},
            new byte[]{7, 3, 5, 3, 6, 8, 8, 7, 9, 7, 9, 6, 7, 1, 8, 8, 5, 6, 4, 8},
            new byte[]{8, 6, 6, 4, 4, 4, 5, 8, 4, 1, 5, 2, 6, 1, 4, 8, 4, 5, 5, 2},
            new byte[]{7, 5, 8, 5, 8, 5, 9, 3, 1, 2, 6, 4, 9, 9, 5, 2, 7, 1, 8, 3},
            new byte[]{3, 9, 4, 9, 3, 7, 1, 6, 7, 9, 4, 9, 1, 2, 1, 4, 8, 1, 5, 9},
            new byte[]{4, 2, 8, 2, 5, 1, 3, 7, 6, 2, 1, 3, 7, 3, 8, 8, 1, 8, 2, 2},
            new byte[]{4, 4, 6, 1, 7, 6, 4, 7, 8, 1, 3, 7, 6, 8, 9, 2, 2, 2, 5, 3},
            new byte[]{4, 4, 1, 5, 9, 2, 3, 8, 4, 3, 3, 9, 9, 3, 5, 9, 6, 7, 5, 8},
            new byte[]{6, 6, 8, 5, 9, 4, 6, 9, 3, 7, 6, 9, 4, 9, 8, 1, 4, 2, 1, 5},
            new byte[]{4, 5, 3, 6, 8, 3, 8, 3, 3, 9, 6, 4, 7, 6, 6, 8, 5, 5, 5, 6},
            new byte[]{5, 4, 5, 7, 7, 3, 5, 9, 5, 5, 6, 4, 6, 9, 4, 2, 3, 3, 6, 6},
            new byte[]{6, 4, 6, 4, 2, 2, 3, 8, 3, 9, 5, 8, 6, 5, 9, 1, 9, 6, 8, 5},
            new byte[]{7, 8, 7, 8, 4, 2, 7, 7, 2, 5, 5, 3, 8, 9, 9, 2, 7, 2, 6, 5},
            new byte[]{1, 7, 7, 5, 9, 7, 8, 1, 9, 4, 2, 7, 4, 2, 8, 8, 6, 8, 1, 9},
            new byte[]{5, 5, 6, 8, 5, 3, 1, 7, 5, 8, 3, 8, 1, 4, 4, 1, 1, 2, 8, 6},
            new byte[]{3, 6, 7, 7, 3, 8, 8, 7, 1, 2, 6, 9, 2, 6, 1, 6, 1, 8, 1, 6},
            new byte[]{7, 2, 5, 2, 1, 4, 7, 9, 7, 8, 7, 4, 9, 4, 7, 8, 7, 8, 6, 7},
            new byte[]{4, 3, 6, 4, 2, 4, 6, 4, 4, 8, 9, 3, 2, 2, 8, 2, 1, 6, 7, 8},
            new byte[]{2, 9, 1, 3, 5, 2, 9, 7, 9, 3, 8, 2, 3, 4, 3, 5, 5, 2, 1, 7},
            new byte[]{7, 3, 5, 8, 8, 4, 3, 7, 6, 5, 7, 2, 2, 7, 5, 5, 3, 4, 8, 1},
            new byte[]{4, 9, 5, 2, 9, 7, 9, 8, 9, 4, 7, 9, 5, 1, 1, 8, 4, 2, 4, 1},
            new byte[]{9, 3, 2, 4, 5, 7, 1, 6, 8, 8, 2, 1, 4, 1, 3, 8, 7, 3, 9, 1},
            new byte[]{4, 5, 8, 3, 8, 7, 6, 2, 6, 7, 1, 2, 9, 7, 1, 2, 7, 2, 6, 3},
            new byte[]{7, 4, 9, 2, 2, 6, 5, 5, 7, 3, 2, 1, 2, 4, 7, 5, 5, 8, 5, 7},
            new byte[]{1, 9, 9, 2, 7, 1, 1, 7, 9, 1, 1, 6, 7, 7, 4, 9, 5, 7, 1, 3},
            new byte[]{3, 1, 4, 4, 5, 9, 9, 6, 1, 5, 7, 9, 7, 8, 2, 8, 5, 8, 4, 2},
            new byte[]{6, 8, 6, 7, 2, 5, 4, 5, 1, 7, 1, 8, 7, 3, 5, 1, 5, 6, 6, 7},
            new byte[]{1, 4, 7, 4, 4, 2, 8, 8, 6, 5, 5, 7, 3, 9, 7, 5, 5, 7, 3, 9},
            new byte[]{6, 4, 6, 3, 5, 9, 2, 5, 4, 1, 3, 8, 2, 6, 1, 2, 1, 2, 3, 9},
            new byte[]{7, 6, 9, 5, 2, 3, 5, 8, 5, 1, 6, 8, 4, 6, 2, 7, 8, 4, 8, 5},
            new byte[]{1, 4, 4, 5, 3, 6, 5, 7, 9, 5, 4, 3, 4, 3, 3, 1, 5, 1, 1, 3},
            new byte[]{4, 3, 9, 4, 5, 4, 5, 6, 4, 4, 3, 7, 3, 7, 9, 3, 2, 5, 3, 6},
            new byte[]{8, 8, 8, 1, 6, 3, 4, 1, 3, 2, 2, 7, 3, 6, 1, 7, 5, 1, 9, 8},
            new byte[]{2, 9, 3, 6, 3, 7, 9, 6, 4, 9, 8, 1, 8, 5, 4, 6, 5, 9, 7, 3},
            new byte[]{9, 4, 3, 3, 2, 6, 4, 6, 7, 2, 8, 1, 5, 4, 5, 9, 5, 3, 7, 9},
            new byte[]{3, 8, 7, 3, 3, 2, 8, 8, 3, 5, 3, 5, 3, 7, 9, 7, 6, 9, 5, 6},
            new byte[]{3, 3, 9, 2, 1, 7, 5, 5, 1, 5, 1, 2, 2, 8, 9, 2, 7, 9, 9, 1},
            new byte[]{7, 6, 4, 2, 9, 4, 8, 8, 5, 7, 3, 5, 8, 2, 7, 9, 8, 5, 3, 6},
            new byte[]{6, 3, 7, 7, 4, 1, 4, 5, 7, 7, 1, 7, 1, 4, 8, 7, 9, 8, 3, 7},
            new byte[]{9, 6, 1, 4, 3, 5, 4, 1, 2, 9, 4, 2, 7, 4, 4, 4, 8, 7, 7, 9},
            new byte[]{9, 3, 1, 7, 7, 6, 5, 4, 3, 9, 1, 1, 3, 5, 6, 1, 3, 4, 5, 9},
            new byte[]{6, 4, 4, 7, 4, 9, 9, 6, 1, 4, 8, 2, 1, 3, 7, 6, 6, 3, 6, 7},
            new byte[]{1, 1, 7, 2, 8, 3, 8, 3, 8, 4, 2, 5, 3, 8, 3, 6, 9, 3, 3, 7},
            new byte[]{6, 8, 3, 8, 2, 9, 9, 9, 7, 9, 5, 9, 7, 7, 8, 6, 6, 3, 8, 1},
            new byte[]{2, 7, 8, 7, 3, 4, 9, 3, 1, 2, 7, 3, 1, 9, 1, 2, 8, 9, 9, 9},
            new byte[]{4, 8, 5, 8, 9, 2, 7, 5, 9, 7, 9, 6, 4, 3, 6, 9, 1, 5, 5, 8},
            new byte[]{9, 6, 9, 5, 2, 2, 9, 8, 7, 7, 8, 7, 3, 9, 7, 3, 2, 1, 8, 1},
            new byte[]{6, 4, 3, 4, 2, 2, 8, 7, 3, 2, 2, 3, 4, 2, 1, 7, 8, 9, 6, 7},
            new byte[]{3, 9, 8, 3, 9, 3, 3, 5, 6, 3, 7, 9, 6, 5, 2, 6, 3, 6, 4, 1},
            new byte[]{4, 2, 5, 5, 5, 5, 4, 2, 9, 9, 5, 2, 4, 2, 8, 7, 6, 1, 5, 9},
            new byte[]{8, 3, 2, 1, 4, 2, 6, 8, 9, 5, 4, 3, 6, 6, 3, 1, 4, 3, 7, 4},
            new byte[]{8, 1, 6, 6, 8, 6, 6, 5, 6, 5, 9, 3, 2, 5, 2, 1, 9, 5, 1, 4},
            new byte[]{9, 5, 3, 5, 5, 2, 5, 8, 8, 7, 3, 3, 3, 1, 1, 7, 6, 6, 2, 8},
            new byte[]{5, 2, 3, 5, 5, 2, 2, 2, 4, 2, 1, 9, 3, 4, 5, 4, 8, 2, 2, 6},
            new byte[]{1, 8, 6, 8, 2, 7, 9, 1, 4, 5, 4, 4, 3, 3, 8, 7, 3, 8, 9, 3},
            new byte[]{1, 4, 4, 6, 7, 4, 1, 9, 6, 6, 9, 5, 4, 6, 8, 7, 6, 4, 4, 8},
            new byte[]{3, 1, 7, 9, 8, 6, 6, 1, 6, 7, 2, 1, 2, 8, 5, 6, 4, 9, 2, 7},
            new byte[]{6, 1, 5, 8, 5, 9, 9, 6, 4, 1, 2, 9, 5, 2, 1, 1, 7, 5, 8, 1},
            new byte[]{7, 1, 3, 8, 5, 9, 3, 6, 1, 2, 3, 6, 2, 6, 2, 2, 6, 9, 5, 8},
            new byte[]{7, 9, 6, 5, 6, 2, 5, 1, 7, 5, 9, 9, 1, 2, 9, 6, 7, 6, 3, 5},
            new byte[]{7, 4, 5, 3, 8, 5, 7, 8, 9, 9, 5, 8, 9, 1, 4, 8, 4, 8, 3, 3},
            new byte[]{4, 2, 9, 6, 2, 3, 5, 1, 6, 6, 5, 9, 6, 9, 1, 4, 4, 4, 7, 1},
            new byte[]{5, 7, 2, 8, 9, 2, 9, 1, 7, 2, 2, 8, 3, 9, 2, 5, 4, 9, 1, 5},
            new byte[]{1, 7, 2, 4, 4, 5, 2, 1, 9, 1, 4, 1, 1, 3, 8, 7, 1, 2, 6, 1},
            new byte[]{3, 5, 3, 5, 4, 6, 7, 7, 7, 6, 9, 5, 3, 2, 1, 7, 5, 7, 4, 5},
            new byte[]{1, 9, 1, 7, 8, 1, 8, 1, 1, 4, 5, 3, 3, 9, 3, 5, 4, 7, 9, 7},
            new byte[]{8, 4, 4, 4, 1, 8, 7, 8, 3, 1, 3, 3, 7, 2, 1, 4, 4, 6, 7, 3},
            new byte[]{8, 6, 6, 4, 2, 7, 3, 3, 9, 5, 4, 8, 7, 7, 7, 6, 5, 7, 6, 9},
            new byte[]{3, 9, 8, 1, 2, 2, 5, 1, 9, 8, 2, 8, 9, 7, 8, 2, 8, 1, 3, 4},
            new byte[]{6, 5, 8, 7, 7, 4, 6, 9, 4, 5, 8, 7, 9, 8, 9, 9, 7, 6, 2, 9},
            new byte[]{2, 2, 4, 2, 4, 5, 8, 1, 8, 5, 7, 5, 5, 3, 4, 8, 1, 2, 3, 7},
            new byte[]{6, 5, 7, 5, 9, 4, 8, 2, 7, 9, 5, 3, 8, 3, 2, 4, 1, 7, 4, 2},
            new byte[]{6, 5, 2, 9, 7, 4, 3, 7, 4, 1, 8, 6, 3, 4, 4, 7, 5, 9, 2, 9},
            new byte[]{7, 7, 4, 9, 1, 5, 6, 2, 1, 9, 8, 3, 7, 2, 2, 1, 4, 7, 7, 6},
            new byte[]{2, 2, 7, 6, 1, 8, 1, 4, 8, 5, 8, 2, 6, 4, 4, 9, 1, 2, 9, 3},
            new byte[]{7, 1, 7, 4, 3, 6, 8, 1, 3, 3, 3, 7, 2, 5, 8, 6, 9, 7, 9, 2},
            new byte[]{4, 3, 4, 5, 2, 7, 7, 4, 7, 3, 1, 2, 8, 5, 9, 2, 4, 4, 4, 5},
            new byte[]{1, 9, 2, 5, 3, 1, 9, 1, 6, 8, 1, 3, 3, 8, 8, 4, 6, 2, 3, 5},
            new byte[]{8, 5, 3, 1, 3, 1, 9, 7, 5, 6, 2, 5, 4, 5, 2, 5, 1, 8, 3, 1},
            new byte[]{8, 6, 7, 9, 3, 7, 9, 1, 6, 5, 5, 3, 1, 8, 6, 5, 6, 2, 3, 2},
            new byte[]{2, 2, 5, 9, 7, 5, 9, 8, 7, 4, 4, 7, 8, 3, 7, 3, 8, 4, 7, 6},
            new byte[]{2, 9, 2, 1, 2, 8, 5, 6, 8, 6, 8, 9, 6, 2, 8, 7, 1, 7, 2, 8},
            new byte[]{7, 1, 4, 3, 2, 7, 5, 8, 1, 4, 7, 3, 4, 6, 1, 3, 9, 5, 8, 8},
            new byte[]{1, 4, 9, 1, 6, 4, 2, 7, 4, 1, 2, 1, 3, 9, 8, 1, 5, 8, 8, 4},
            new byte[]{4, 6, 5, 5, 8, 5, 6, 1, 1, 6, 7, 6, 8, 3, 8, 4, 2, 3, 7, 8},
            new byte[]{6, 7, 4, 1, 2, 6, 1, 4, 8, 1, 7, 1, 7, 5, 5, 4, 8, 4, 2, 1},
            new byte[]{8, 1, 6, 8, 4, 6, 3, 9, 2, 4, 8, 3, 2, 1, 7, 6, 3, 5, 4, 3},
            new byte[]{5, 2, 2, 1, 8, 6, 8, 3, 8, 4, 6, 7, 1, 7, 9, 5, 2, 6, 6, 6},
            new byte[]{4, 2, 2, 4, 2, 5, 9, 7, 8, 8, 2, 4, 1, 9, 5, 6, 9, 6, 9, 7},
            new byte[]{7, 9, 1, 7, 8, 2, 2, 5, 1, 2, 5, 6, 8, 7, 8, 4, 9, 7, 8, 4},
            new byte[]{7, 2, 5, 1, 3, 3, 6, 2, 2, 4, 6, 5, 6, 8, 9, 5, 1, 5, 6, 1},
            new byte[]{3, 4, 6, 6, 4, 5, 5, 4, 2, 1, 7, 2, 7, 5, 1, 6, 4, 1, 5, 5},
            new byte[]{5, 9, 3, 7, 5, 9, 9, 9, 8, 3, 4, 5, 2, 8, 1, 3, 5, 9, 6, 8},
            new byte[]{7, 3, 1, 2, 6, 5, 7, 2, 8, 4, 2, 1, 1, 1, 5, 4, 2, 9, 1, 4},
            new byte[]{5, 5, 6, 8, 7, 2, 9, 9, 4, 8, 1, 4, 4, 6, 7, 3, 9, 2, 1, 1},
            new byte[]{2, 8, 2, 4, 1, 4, 5, 1, 4, 7, 1, 5, 9, 3, 3, 3, 6, 2, 4, 5},
            new byte[]{5, 8, 7, 9, 3, 8, 6, 4, 4, 2, 4, 9, 9, 1, 2, 6, 4, 2, 8, 4},
            new byte[]{7, 9, 5, 7, 7, 5, 5, 1, 3, 5, 7, 8, 7, 6, 5, 4, 2, 3, 2, 3},
            new byte[]{7, 2, 7, 2, 4, 1, 6, 8, 2, 6, 8, 4, 4, 1, 7, 6, 4, 9, 2, 7},
            new byte[]{3, 5, 1, 8, 5, 5, 3, 4, 6, 8, 5, 1, 1, 5, 6, 6, 3, 3, 3, 6},
            new byte[]{9, 6, 9, 6, 6, 3, 5, 6, 8, 8, 9, 2, 1, 4, 9, 2, 1, 8, 7, 9},
            new byte[]{8, 2, 3, 4, 2, 7, 4, 8, 4, 2, 3, 1, 7, 5, 7, 7, 6, 4, 2, 2},
            new byte[]{9, 7, 6, 7, 8, 4, 1, 6, 5, 5, 5, 4, 5, 4, 8, 8, 4, 6, 5, 9},
            new byte[]{8, 6, 2, 6, 5, 9, 4, 6, 6, 3, 5, 7, 7, 5, 9, 2, 6, 5, 5, 7},
            new byte[]{7, 7, 3, 9, 6, 9, 5, 4, 7, 9, 4, 1, 8, 3, 3, 3, 5, 9, 2, 9},
            new byte[]{9, 4, 1, 3, 9, 8, 7, 1, 4, 7, 2, 8, 1, 7, 2, 2, 3, 3, 9, 6},
            new byte[]{4, 1, 9, 6, 9, 5, 8, 5, 2, 6, 7, 6, 4, 6, 8, 4, 7, 1, 9, 7},
            new byte[]{3, 5, 9, 9, 8, 5, 9, 1, 2, 2, 1, 2, 9, 6, 5, 8, 5, 4, 4, 5},
            new byte[]{1, 3, 8, 8, 1, 5, 5, 5, 9, 8, 5, 3, 3, 3, 4, 9, 2, 9, 8, 4},
            new byte[]{5, 7, 3, 1, 6, 5, 7, 7, 1, 1, 8, 3, 7, 5, 7, 9, 7, 2, 9, 3},
            new byte[]{1, 5, 2, 3, 6, 7, 8, 7, 2, 6, 6, 6, 6, 9, 9, 4, 9, 7, 2, 5},
            new byte[]{8, 7, 4, 9, 9, 3, 5, 8, 5, 4, 2, 1, 9, 6, 4, 1, 5, 1, 4, 8},
            new byte[]{6, 9, 6, 4, 4, 2, 7, 7, 6, 4, 1, 9, 9, 5, 4, 3, 1, 3, 4, 4},
            new byte[]{2, 1, 2, 7, 5, 6, 6, 9, 6, 9, 8, 9, 4, 3, 8, 8, 2, 1, 5, 7},
            new byte[]{5, 8, 2, 2, 4, 2, 2, 5, 2, 1, 9, 3, 6, 9, 6, 6, 7, 8, 9, 8},
            new byte[]{6, 7, 2, 7, 5, 2, 6, 6, 6, 6, 2, 1, 7, 6, 9, 4, 9, 1, 4, 3},
            new byte[]{5, 2, 8, 5, 9, 6, 3, 4, 6, 3, 1, 3, 1, 7, 2, 8, 6, 5, 6, 3},
            new byte[]{6, 5, 9, 6, 8, 9, 6, 1, 1, 3, 6, 7, 4, 7, 3, 4, 8, 6, 8, 9},
            new byte[]{8, 3, 9, 3, 5, 8, 9, 9, 4, 1, 8, 5, 4, 2, 7, 9, 3, 3, 7, 9},
            new byte[]{9, 2, 3, 4, 6, 4, 9, 4, 4, 8, 9, 9, 7, 7, 4, 3, 6, 6, 1, 9},
            new byte[]{1, 6, 7, 1, 5, 2, 4, 3, 9, 4, 2, 6, 9, 8, 9, 6, 9, 6, 6, 2},
            new byte[]{9, 5, 5, 6, 1, 8, 7, 6, 8, 4, 4, 6, 2, 7, 9, 4, 5, 3, 9, 5},
            new byte[]{9, 7, 4, 1, 6, 4, 1, 9, 7, 7, 4, 9, 1, 9, 9, 5, 1, 1, 9, 7},
            new byte[]{4, 7, 5, 6, 9, 6, 4, 2, 8, 6, 2, 6, 6, 2, 2, 3, 1, 5, 9, 7},
            new byte[]{7, 8, 9, 1, 9, 4, 6, 3, 8, 5, 4, 5, 4, 3, 1, 3, 3, 8, 1, 1},
            new byte[]{1, 9, 1, 2, 6, 8, 4, 8, 8, 7, 4, 6, 1, 3, 9, 6, 8, 4, 6, 6},
            new byte[]{9, 2, 4, 3, 3, 6, 2, 4, 2, 9, 1, 3, 5, 9, 6, 8, 8, 7, 5, 2},
            new byte[]{1, 7, 4, 1, 2, 7, 4, 2, 3, 5, 5, 8, 3, 5, 1, 8, 6, 8, 9, 9},
            new byte[]{6, 9, 7, 8, 7, 2, 1, 2, 6, 2, 8, 1, 9, 4, 6, 3, 7, 3, 2, 5},
            new byte[]{3, 1, 1, 6, 5, 2, 9, 1, 8, 8, 2, 9, 8, 5, 6, 1, 9, 5, 4, 9},
            new byte[]{6, 2, 6, 2, 7, 5, 9, 8, 9, 7, 2, 3, 6, 5, 7, 5, 8, 1, 4, 5},
            new byte[]{2, 1, 4, 8, 9, 8, 2, 5, 7, 5, 4, 2, 9, 2, 8, 1, 4, 9, 9, 1},
            new byte[]{3, 2, 1, 1, 6, 9, 3, 4, 8, 1, 2, 8, 2, 8, 8, 6, 8, 8, 2, 8},
            new byte[]{3, 5, 1, 4, 4, 9, 4, 5, 7, 6, 2, 9, 2, 4, 3, 1, 9, 3, 2, 1},
            new byte[]{5, 8, 6, 7, 3, 6, 1, 1, 3, 3, 2, 6, 7, 2, 7, 7, 1, 7, 7, 6},
            new byte[]{5, 8, 7, 2, 9, 3, 7, 7, 5, 8, 6, 6, 7, 2, 4, 5, 8, 4, 2, 7},
            new byte[]{9, 3, 2, 5, 5, 5, 2, 9, 7, 5, 9, 3, 5, 5, 6, 6, 2, 5, 7, 9},
            new byte[]{9, 4, 5, 5, 7, 5, 9, 6, 7, 3, 9, 6, 5, 1, 7, 2, 6, 2, 7, 5},
            new byte[]{2, 7, 1, 1, 8, 3, 2, 2, 6, 7, 3, 3, 7, 2, 8, 5, 7, 7, 8, 9},
            new byte[]{6, 5, 3, 4, 5, 4, 7, 5, 7, 4, 1, 9, 8, 3, 8, 3, 4, 5, 7, 4},
            new byte[]{4, 7, 1, 1, 9, 7, 9, 3, 2, 3, 5, 9, 4, 7, 9, 6, 4, 6, 5, 5},
            new byte[]{5, 7, 5, 2, 7, 4, 4, 5, 4, 6, 2, 3, 1, 4, 5, 8, 4, 2, 5, 8},
            new byte[]{4, 5, 3, 5, 9, 1, 5, 1, 3, 2, 8, 7, 1, 2, 9, 7, 3, 8, 4, 6},
            new byte[]{3, 4, 9, 6, 1, 2, 9, 1, 3, 1, 3, 3, 2, 6, 1, 1, 5, 1, 3, 6},
            new byte[]{8, 4, 2, 3, 8, 4, 3, 8, 4, 8, 5, 5, 8, 8, 6, 9, 6, 7, 4, 7},
            new byte[]{2, 4, 8, 3, 9, 5, 6, 4, 2, 9, 3, 3, 6, 7, 7, 9, 3, 6, 7, 7},
            new byte[]{9, 2, 6, 4, 5, 7, 4, 3, 9, 1, 5, 7, 6, 6, 5, 4, 5, 9, 4, 3},
            new byte[]{8, 8, 6, 5, 6, 3, 9, 2, 2, 7, 7, 9, 9, 7, 7, 9, 8, 4, 7, 3},
            new byte[]{2, 3, 4, 2, 5, 5, 6, 9, 3, 3, 4, 4, 3, 5, 8, 2, 5, 2, 8, 3},
            new byte[]{7, 3, 2, 3, 3, 5, 8, 9, 8, 5, 6, 2, 3, 2, 1, 5, 4, 5, 1, 2},
            new byte[]{6, 9, 8, 8, 4, 3, 7, 4, 8, 6, 5, 7, 5, 4, 5, 9, 1, 2, 8, 9},
            new byte[]{6, 7, 2, 7, 7, 2, 2, 2, 4, 4, 7, 1, 1, 7, 7, 7, 4, 4, 9, 9},
            new byte[]{8, 5, 1, 3, 1, 9, 6, 8, 6, 2, 4, 3, 6, 1, 5, 8, 8, 2, 2, 2},
            new byte[]{2, 6, 9, 4, 9, 4, 6, 8, 7, 8, 5, 7, 3, 8, 2, 2, 6, 8, 7, 7},
            new byte[]{6, 4, 5, 3, 9, 5, 4, 1, 2, 8, 8, 2, 3, 7, 8, 2, 5, 7, 4, 6},
            new byte[]{4, 8, 2, 8, 6, 6, 1, 8, 9, 4, 3, 1, 5, 4, 6, 8, 1, 7, 4, 5},
            new byte[]{1, 6, 6, 9, 9, 3, 5, 5, 1, 9, 6, 9, 5, 1, 4, 7, 2, 6, 6, 4},
            new byte[]{4, 6, 1, 8, 6, 8, 7, 8, 2, 1, 1, 7, 3, 9, 6, 8, 3, 2, 8, 4},
            new byte[]{2, 5, 8, 9, 7, 3, 5, 4, 1, 9, 6, 5, 2, 1, 1, 4, 5, 7, 4, 2},
            new byte[]{5, 6, 4, 9, 8, 9, 7, 6, 5, 5, 4, 1, 7, 3, 2, 9, 8, 2, 1, 3},
            new byte[]{3, 4, 8, 2, 4, 6, 5, 2, 8, 2, 4, 6, 8, 7, 8, 5, 2, 8, 2, 8},
            new byte[]{6, 8, 8, 5, 5, 6, 7, 9, 9, 1, 5, 3, 6, 1, 8, 2, 8, 2, 6, 2},
            new byte[]{4, 5, 7, 3, 2, 8, 2, 1, 4, 6, 8, 1, 1, 3, 6, 5, 6, 3, 7, 8},
            new byte[]{9, 5, 3, 8, 6, 1, 9, 8, 9, 6, 6, 9, 2, 9, 4, 1, 2, 5, 4, 7},
            new byte[]{5, 6, 5, 3, 3, 5, 3, 4, 5, 4, 6, 6, 5, 8, 2, 8, 3, 9, 7, 2},
            new byte[]{9, 3, 1, 7, 1, 8, 8, 1, 8, 6, 3, 1, 9, 1, 3, 4, 1, 1, 5, 2},
            new byte[]{7, 5, 4, 6, 1, 1, 5, 4, 7, 5, 8, 2, 8, 2, 8, 2, 6, 7, 3, 5},
            new byte[]{7, 3, 5, 8, 1, 5, 7, 5, 5, 8, 8, 3, 5, 6, 4, 4, 9, 4, 7, 9},
            new byte[]{4, 4, 3, 3, 8, 2, 3, 7, 2, 2, 7, 3, 7, 1, 4, 5, 9, 5, 6, 6},
            new byte[]{4, 1, 9, 9, 7, 5, 7, 3, 8, 4, 2, 2, 1, 3, 2, 3, 9, 3, 1, 9},
            new byte[]{3, 9, 3, 4, 1, 6, 5, 4, 2, 9, 4, 5, 8, 1, 6, 3, 5, 4, 6, 3},
            new byte[]{4, 1, 9, 8, 9, 6, 2, 5, 9, 2, 5, 7, 4, 7, 4, 4, 8, 6, 8, 1},
            new byte[]{9, 3, 1, 8, 1, 6, 3, 1, 6, 8, 7, 6, 6, 8, 5, 2, 1, 4, 4, 5},
            new byte[]{5, 1, 5, 9, 4, 6, 2, 2, 7, 2, 5, 9, 5, 5, 3, 2, 9, 1, 8, 3},
            new byte[]{3, 1, 8, 1, 4, 9, 2, 6, 3, 2, 4, 4, 2, 4, 6, 9, 7, 1, 4, 4},
            new byte[]{5, 2, 4, 5, 4, 8, 1, 2, 5, 4, 3, 9, 6, 1, 2, 4, 2, 4, 6, 5},
            new byte[]{5, 6, 9, 5, 8, 1, 7, 2, 9, 4, 4, 8, 7, 4, 8, 1, 7, 4, 7, 6},
            new byte[]{8, 1, 1, 8, 9, 5, 7, 2, 2, 2, 8, 4, 5, 9, 9, 9, 3, 8, 6, 3},
            new byte[]{1, 9, 3, 2, 9, 4, 3, 3, 2, 1, 7, 7, 7, 1, 7, 5, 5, 1, 6, 4},
            new byte[]{4, 6, 9, 7, 5, 1, 3, 7, 4, 9, 8, 8, 7, 5, 5, 8, 8, 9, 8, 5},
            new byte[]{2, 5, 9, 1, 5, 1, 6, 5, 3, 5, 4, 3, 3, 3, 8, 5, 9, 5, 6, 9},
            new byte[]{2, 5, 8, 3, 3, 8, 9, 2, 5, 8, 6, 6, 2, 1, 2, 9, 6, 8, 2, 5},
            new byte[]{4, 8, 5, 9, 5, 5, 6, 5, 6, 9, 8, 3, 3, 9, 4, 7, 6, 6, 7, 7},
            new byte[]{1, 5, 1, 4, 8, 8, 2, 2, 1, 9, 9, 1, 4, 2, 6, 4, 9, 9, 7, 8},
            new byte[]{3, 5, 5, 6, 9, 4, 3, 3, 7, 5, 3, 9, 1, 4, 4, 8, 7, 1, 1, 7},
            new byte[]{4, 4, 4, 6, 1, 7, 8, 2, 3, 1, 7, 6, 9, 9, 5, 5, 9, 5, 5, 4},
            new byte[]{5, 9, 5, 6, 7, 9, 1, 9, 7, 8, 6, 8, 9, 9, 5, 8, 2, 8, 5, 2},
            new byte[]{6, 1, 9, 6, 7, 7, 1, 2, 9, 8, 8, 3, 6, 6, 3, 4, 8, 8, 9, 1},
            new byte[]{7, 8, 4, 5, 5, 8, 7, 9, 5, 7, 4, 2, 9, 3, 8, 4, 9, 8, 8, 9},
            new byte[]{9, 8, 1, 8, 9, 3, 3, 3, 7, 1, 9, 3, 9, 4, 4, 7, 6, 4, 2, 9},
            new byte[]{9, 2, 6, 3, 4, 7, 4, 3, 8, 8, 4, 9, 9, 1, 2, 9, 8, 6, 6, 3},
            new byte[]{6, 5, 2, 5, 7, 2, 3, 5, 9, 2, 7, 5, 7, 9, 9, 8, 3, 8, 5, 2},
            new byte[]{8, 9, 4, 9, 6, 9, 4, 8, 3, 5, 8, 1, 8, 8, 4, 5, 9, 1, 5, 3},
            new byte[]{5, 8, 4, 6, 2, 8, 3, 1, 1, 4, 6, 1, 1, 4, 4, 6, 1, 3, 8, 7},
            new byte[]{7, 4, 3, 4, 3, 6, 7, 8, 5, 5, 8, 2, 3, 2, 7, 9, 6, 8, 7, 7},
            new byte[]{7, 4, 8, 8, 8, 8, 1, 5, 5, 8, 5, 6, 2, 8, 6, 2, 6, 9, 3, 5},
            new byte[]{2, 4, 5, 6, 6, 5, 7, 9, 7, 4, 9, 3, 5, 2, 1, 4, 5, 5, 5, 5},
            new byte[]{6, 2, 4, 7, 9, 2, 9, 5, 6, 6, 4, 6, 5, 3, 5, 4, 9, 3, 3, 4},
            new byte[]{1, 1, 8, 1, 8, 3, 2, 1, 7, 3, 6, 1, 5, 8, 8, 9, 9, 7, 6, 4},
            new byte[]{5, 3, 3, 2, 1, 4, 8, 2, 6, 7, 4, 2, 3, 1, 5, 6, 4, 3, 9, 7},
            new byte[]{1, 9, 7, 4, 9, 3, 2, 6, 5, 6, 4, 2, 6, 8, 9, 7, 7, 7, 1, 5},
            new byte[]{4, 9, 5, 1, 8, 5, 8, 4, 3, 3, 6, 7, 8, 8, 4, 6, 6, 9, 2, 8},
            new byte[]{3, 2, 7, 1, 3, 3, 5, 4, 2, 7, 8, 7, 3, 5, 3, 1, 5, 6, 6, 5},
            new byte[]{4, 7, 4, 2, 9, 7, 4, 7, 4, 3, 2, 8, 5, 2, 3, 3, 2, 4, 8, 5},
            new byte[]{9, 4, 3, 5, 5, 9, 4, 3, 6, 3, 2, 1, 3, 7, 4, 5, 8, 6, 1, 6},
            new byte[]{8, 7, 9, 8, 6, 9, 2, 1, 3, 9, 9, 1, 8, 1, 5, 9, 5, 8, 7, 1},
            new byte[]{3, 1, 2, 2, 2, 1, 5, 7, 8, 7, 5, 6, 3, 2, 7, 2, 4, 4, 8, 5},
            new byte[]{7, 3, 1, 6, 4, 1, 2, 3, 3, 4, 1, 9, 9, 1, 3, 7, 6, 8, 6, 5},
            new byte[]{4, 5, 3, 5, 5, 4, 7, 7, 3, 4, 5, 1, 9, 2, 4, 1, 1, 6, 8, 1},
            new byte[]{9, 3, 5, 4, 7, 5, 6, 3, 8, 1, 5, 6, 8, 4, 2, 2, 9, 6, 2, 1},
            new byte[]{4, 5, 9, 8, 8, 9, 1, 8, 6, 3, 7, 9, 1, 6, 6, 2, 1, 8, 8, 9},
            new byte[]{7, 5, 8, 3, 8, 9, 3, 6, 9, 2, 1, 5, 5, 7, 7, 4, 3, 7, 6, 7},
            new byte[]{5, 8, 7, 5, 4, 5, 6, 3, 7, 9, 2, 2, 7, 2, 4, 9, 6, 6, 9, 9},
            new byte[]{7, 5, 8, 4, 2, 2, 5, 3, 8, 2, 8, 4, 3, 1, 5, 8, 8, 3, 9, 3},
            new byte[]{4, 1, 8, 1, 1, 9, 7, 3, 4, 8, 8, 7, 7, 6, 1, 9, 1, 3, 4, 2},
            new byte[]{1, 3, 4, 5, 1, 8, 8, 7, 7, 5, 2, 3, 6, 6, 5, 7, 6, 8, 4, 2},
            new byte[]{5, 2, 8, 1, 4, 2, 6, 6, 1, 5, 7, 5, 9, 8, 3, 1, 6, 4, 9, 2},
            new byte[]{2, 9, 6, 8, 7, 5, 1, 1, 1, 3, 4, 3, 9, 3, 7, 8, 4, 4, 1, 9},
            new byte[]{2, 2, 7, 2, 8, 6, 4, 8, 4, 5, 2, 3, 8, 2, 9, 5, 6, 1, 3, 9},
            new byte[]{1, 6, 5, 7, 1, 5, 6, 7, 2, 4, 7, 3, 3, 1, 1, 9, 3, 4, 1, 1},
            new byte[]{1, 3, 6, 6, 2, 2, 4, 7, 3, 3, 3, 4, 1, 2, 8, 6, 7, 9, 1, 3},
            new byte[]{9, 7, 6, 9, 7, 8, 9, 8, 2, 1, 2, 2, 9, 3, 8, 8, 9, 8, 6, 9},
            new byte[]{1, 8, 5, 9, 2, 3, 2, 2, 2, 4, 3, 3, 3, 7, 2, 4, 6, 7, 8, 3},
            new byte[]{2, 6, 6, 2, 1, 1, 5, 8, 3, 1, 7, 8, 4, 2, 3, 1, 3, 7, 8, 5},
            new byte[]{7, 8, 7, 7, 7, 8, 8, 2, 2, 4, 2, 3, 9, 3, 8, 5, 3, 3, 8, 2},
            new byte[]{8, 4, 8, 1, 7, 5, 9, 2, 5, 1, 2, 9, 9, 4, 6, 4, 9, 8, 5, 2},
            new byte[]{2, 3, 7, 8, 9, 4, 8, 1, 3, 7, 8, 9, 8, 7, 6, 3, 9, 6, 1, 3},
            new byte[]{9, 7, 7, 1, 3, 3, 4, 6, 6, 4, 6, 4, 8, 7, 9, 5, 6, 1, 2, 3},
            new byte[]{7, 8, 8, 7, 2, 2, 1, 3, 7, 3, 5, 3, 8, 2, 3, 5, 6, 6, 6, 6},
            new byte[]{1, 2, 5, 9, 2, 4, 2, 2, 7, 2, 6, 8, 4, 7, 5, 1, 8, 9, 9, 1},
            new byte[]{2, 2, 8, 3, 9, 3, 6, 6, 5, 4, 9, 1, 4, 5, 3, 5, 4, 9, 5, 7},
            new byte[]{1, 8, 5, 6, 6, 6, 9, 1, 7, 2, 7, 7, 7, 6, 4, 9, 4, 2, 9, 9},
            new byte[]{8, 6, 6, 4, 2, 6, 5, 8, 5, 1, 7, 9, 4, 2, 1, 3, 7, 2, 7, 3},
            new byte[]{4, 6, 9, 3, 6, 3, 4, 8, 1, 4, 1, 3, 9, 6, 9, 4, 3, 1, 4, 1},
            new byte[]{2, 1, 6, 6, 5, 8, 3, 5, 6, 2, 2, 2, 4, 7, 6, 2, 7, 4, 7, 9},
            new byte[]{3, 6, 7, 2, 9, 4, 9, 7, 2, 6, 1, 6, 8, 6, 2, 3, 1, 3, 8, 8},
            new byte[]{9, 8, 3, 9, 2, 9, 8, 3, 1, 7, 7, 9, 3, 4, 8, 5, 4, 5, 6, 2},
            new byte[]{8, 5, 3, 9, 1, 6, 4, 8, 5, 3, 2, 4, 5, 9, 1, 4, 4, 7, 9, 1},
            new byte[]{5, 7, 6, 4, 8, 2, 4, 4, 8, 6, 9, 5, 6, 7, 1, 4, 5, 3, 9, 6},
            new byte[]{5, 1, 7, 8, 6, 4, 4, 4, 4, 4, 2, 6, 5, 6, 6, 2, 9, 8, 2, 8},
            new byte[]{2, 1, 1, 8, 4, 6, 7, 8, 9, 6, 8, 2, 8, 4, 6, 5, 9, 8, 8, 5},
            new byte[]{8, 9, 9, 4, 5, 4, 1, 1, 1, 8, 5, 6, 2, 2, 4, 4, 1, 9, 4, 9},
            new byte[]{6, 6, 5, 8, 6, 9, 7, 2, 1, 1, 2, 1, 1, 6, 9, 2, 2, 6, 3, 1},
            new byte[]{6, 7, 7, 1, 1, 2, 1, 6, 9, 2, 5, 1, 4, 9, 9, 3, 5, 1, 8, 6},
            new byte[]{6, 8, 1, 1, 7, 5, 6, 5, 2, 2, 3, 4, 6, 4, 3, 8, 8, 5, 8, 4},
        };
        #endregion

        private static string EncryptChar(char inputChar, byte lengthMode, int order)
        {
            StringBuilder outputStringBuilder = new StringBuilder();
            order = AdjustOrder(order);
            int sum = 0;
            byte targetCountOfChars = charLengthMode[lengthMode][order];

            // Make randoom fillers
            for (int i = 0; i < targetCountOfChars - 1; i++)
            {
                byte current = (byte)rnd.Next(256);
                outputStringBuilder.Append((char)current);
                sum += current;
            }
            sum = AdjustByte(sum);

            // Calculate the last char.
            if (sum > (byte)inputChar)
            {
                sum -= 256;
            }

            char lastChar = (char)((byte)inputChar - sum);
            outputStringBuilder.Append(lastChar);

            return outputStringBuilder.ToString();
        }

        private static char DecryptChar(string encryptedChar)
        {
            int sum = 0;
            for (int i = 0; i < encryptedChar.Length; i++)
            {
                sum += encryptedChar[i];
            }

            return (char)AdjustByte(sum);
        }

        private static byte AdjustOrder(int order)
        {
            while (order > 19)
            {
                order -= 20;
            }
            while (order < 0)
            {
                order += 20;
            }
            return (byte)order;
        }

        private static byte AdjustByte(int input)
        {
            while (input > 255)
            {
                input -= 256;
            }
            while (input < 0)
            {
                input += 256;
            }
            return (byte)input;
        }

        private static string OffsetPosition(string sourceString, int offset)
        {
            if (sourceString.Length == 0)
            {
                return "";
            }
            StringBuilder sourceSB = new StringBuilder();
            sourceSB.Append(sourceString);
            StringBuilder resultSB = new StringBuilder();
            resultSB.Append(sourceString);

            // Adjust the offset to be in the string's bounds.
            while (offset >= sourceString.Length)
            {
                offset -= sourceString.Length;
            }
            while (offset < 0)
            {
                offset += sourceSB.Length;
            }
            for (int resultIndex = offset, sourceIndex = 0; resultIndex < sourceSB.Length; resultIndex++, sourceIndex++)
            {
                resultSB[resultIndex] = sourceSB[sourceIndex];
            }
            for (int resultIndex = 0, sourceIndex = sourceSB.Length - offset;
                resultIndex < offset;
                resultIndex++, sourceIndex++)
            {
                resultSB[resultIndex] = sourceSB[sourceIndex];
            }
            return resultSB.ToString();
        }

        private static string OffsetCharNum(string sourceString, int offset)
        {
            offset = AdjustByte(offset);
            StringBuilder resultSB = new StringBuilder();
            resultSB.Append(sourceString);
            for (int i = 0; i < resultSB.Length; i++)
            {
                int currentCharNum = (byte)resultSB[i] + offset;
                currentCharNum = AdjustByte(currentCharNum);
                resultSB[i] = (char)currentCharNum;
            }
            return resultSB.ToString();
        }

        private static string ReverseString(string sourceString)
        {
            StringBuilder resultSB = new StringBuilder();
            resultSB.Append(sourceString);
            for (int resultIndex = 0, sourceIndex = sourceString.Length - 1;
                (resultIndex < sourceString.Length) && (sourceIndex >= 0);
                resultIndex++, sourceIndex--)
            {
                resultSB[sourceIndex] = sourceString[resultIndex];
            }
            return resultSB.ToString();
        }

        public static string EncryptString(string sourceString)
        {
            char[] inputCharArray = sourceString.ToArray();
            string encryptionStr = "";
            StringBuilder encryptionSB = new StringBuilder();

            // Handle Length mode and char encryption.
            char lengthModeKey = (char)rnd.Next(256);
            for (int i = 0; i < inputCharArray.Length; i++)
            {
                encryptionSB.Append(EncryptChar(inputCharArray[i], (byte)lengthModeKey, i));
            }
            encryptionStr = encryptionSB.ToString();

            // Handle Position offset.
            char offsetPositionKey = (char)rnd.Next(256);
            encryptionStr = OffsetPosition(encryptionSB.ToString(), (byte)offsetPositionKey);


            // Handle Char number offset.
            char offsetCharNumKey = (char)rnd.Next(256);
            encryptionStr = OffsetCharNum(encryptionStr, (byte)offsetCharNumKey);


            // Handle reverse.
            char reverseKey = (char)rnd.Next(256);
            if ((byte)reverseKey % 2 == 0)
            {
                encryptionStr = ReverseString(encryptionStr);
            }

            // Build final string.
            encryptionSB.Clear();
            encryptionSB.Append(encryptionStr);
            encryptionSB.Append(lengthModeKey);
            encryptionSB.Append(offsetPositionKey);
            encryptionSB.Append(offsetCharNumKey);
            encryptionSB.Append(reverseKey);

            return encryptionSB.ToString();
        }

        public static string DecryptString(string sourceString)
        {
            if (sourceString.Length < 5)
            {
                return "";
            }

            // Extract keys.
            char reverseKey = sourceString[sourceString.Length - 1];
            char offsetCharNumKey = sourceString[sourceString.Length - 2];
            char offsetPositionKey = sourceString[sourceString.Length - 3];
            char lengthModeKey = sourceString[sourceString.Length - 4];

            StringBuilder sourceSB = new StringBuilder();
            sourceSB.Append(sourceString);
            string decryptionStr = sourceSB.ToString();
            decryptionStr = decryptionStr.Remove(decryptionStr.Length - 4);
            sourceSB.Clear();
            StringBuilder resultSB = new StringBuilder();


            // Handle reverse.
            if (reverseKey % 2 == 0)
            {
                decryptionStr = ReverseString(decryptionStr);
            }

            // Handle char number offset.
            decryptionStr = OffsetCharNum(decryptionStr, (-1) * (byte)offsetCharNumKey);


            //// Handle position offset.
            decryptionStr = OffsetPosition(decryptionStr, (-1) * offsetPositionKey);

            // Handle char decryption.
            sourceSB.Append(decryptionStr);
            int order = 0;
            StringBuilder bufferSB = new StringBuilder();
            while (sourceSB.Length > 0)
            {
                for (int i = 0; i < charLengthMode[(byte)lengthModeKey][AdjustOrder(order)]; i++)
                {
                    bufferSB.Append(sourceSB[i]);
                }
                resultSB.Append(DecryptChar(bufferSB.ToString()));
                sourceSB.Remove(0, charLengthMode[(byte)lengthModeKey][AdjustOrder(order)]);

                bufferSB.Clear();
                order++;
            }

            return resultSB.ToString(); ;
        }
    }
}