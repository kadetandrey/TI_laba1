using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laba1TI
{
    static class RailFence
    {
        public static string Encrypt(string plainText, int key)
        {
            char[,] Rail = new char[key, plainText.Length];
            bool Down = true;
            string cipher = "";
            int row = 0;
            int column = 0;

            for (int i = 0; i < plainText.Length; i++)
            {
                Rail[row, column] = plainText[i];
                column++;
                if (Down)
                    row++;
                else
                    row--;
                if ((row == 0) || (row == key - 1))
                {
                    Down = !Down;
                }
            }
            foreach (char symbol in Rail)
            {
                if (symbol != 0)
                {
                    cipher += symbol;
                }
            }
            return cipher;
        }

        public static string Decrypt(string cipherstr, int key)
        {
            int row = 0;
            int Index = 0;
            int column = 0;
            bool Down = true;
            char[,] Rail = new char[key, cipherstr.Length];
            for (int i = 0; i < cipherstr.Length; i++)
            {
                Rail[row, column] = 'p';
                column++;
                if (Down)
                    row++;
                else
                    row--;
                if ((row == 0) || (row == key - 1))
                {
                    Down = !Down;
                }
            }


            for (int i = 0; i < key; i++)
            {
                for (int j = 0; j < cipherstr.Length; j++)
                {
                    if (Rail[i, j] == 'p')
                    {
                        Rail[i, j] = cipherstr[Index];
                        Index++;
                    }
                }
            }

            row = 0;
            column = 0;
            Down = true;
            string decipher = "";
            for (int i = 0; i < cipherstr.Length; i++)
            {
                decipher += Rail[row, column];
                column++;
                if (Down)
                    row++;
                else
                    row--;
                if ((row == 0) || (row == key - 1))
                {
                    Down = !Down;
                }
            }

            return decipher;


        }
    }

    static class Cesar
    {
        private static string rus = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя";
        private static string rusUp = "АБВГДЕЙЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ";
        public static string Encrypt(string plainText, int key)
        {
            string rez = "";
            int pos;
            for (int i = 0; i < plainText.Length; i++)
            {
                if (rus.IndexOf(plainText[i]) == -1)
                {
                    if (rusUp.IndexOf(plainText[i]) == -1)
                    {
                        rez += " ";
                    }
                    else
                    {
                        pos = rusUp.IndexOf(plainText[i]);
                        if (pos + key > 33)
                        {
                            pos = pos + key - 33;
                        }
                        else
                        {
                            pos += key;
                        }
                        rez += rusUp[pos];
                    }
                }
                else
                {
                    pos = rus.IndexOf(plainText[i]);
                    if (pos + key > 33)
                    {
                        pos = pos + key - 33;
                    }
                    else
                    {
                        pos += key;
                    }
                    rez += rus[pos];
                }
            }
            return rez;
        }

        public static string Decrypt(string plainText, int key)
        {
            string rez = "";
            int pos;
            for (int i = 0; i < plainText.Length; i++)
            {
                if (rus.IndexOf(plainText[i]) == -1)
                {
                    if (rusUp.IndexOf(plainText[i]) == -1)
                    {
                        rez += " ";
                    }
                    else
                    {
                        pos = rusUp.IndexOf(plainText[i]);
                        if (pos - key < 0)
                        {
                            pos = pos - key + 33;
                        }
                        else
                        {
                            pos -= key;
                        }
                        rez += rusUp[pos];
                    }
                }
                else
                {
                    pos = rus.IndexOf(plainText[i]);
                    if (pos - key  < 0)
                    {
                        pos = pos - key + 33;
                    }
                    else
                    {
                        pos -= key;
                    }
                    rez += rus[pos];
                }
            }
            return rez;
        }
    }

    static class Column
    {
        public static string Encrypt(string plainText, string key)
        {

            int columnCount = key.Length;
            int rowsCount = (int)Math.Ceiling((double)plainText.Length / columnCount);
            char[,] matrix = new char[rowsCount, columnCount];
            int Index = 0;
            int[,] Order = new int[2, columnCount];
            for (int i = 0; i < columnCount; i++)
            {
                Order[0, i] = (int)key[Index];
                Order[1, i] = Index;
                if (Index != key.Length)
                    Index++;
            }
            Index = 0;
            for (int i = 0; i < rowsCount; i++)
            {
                for (int j = 0; j < columnCount; j++)
                {
                    if (Index < plainText.Length)
                    {
                        matrix[i, j] = plainText[Index];
                        Index++;
                    }
                    else
                        break;
                }
            }

            int temp = 0;
            int IndexTemp;
            for (var i = 1; i < columnCount; i++)
            {
                for (var j = 0; j < columnCount - i; j++)
                {
                    if (Order[0, j] > Order[0, j + 1])
                    {
                        temp = Order[0, j + 1];
                        IndexTemp = Order[1, j + 1];
                        Order[0, j + 1] = Order[0, j];
                        Order[1, j + 1] = Order[1, j];
                        Order[0, j] = temp;
                        Order[1, j] = IndexTemp;
                    }
                }
            }

            int column;
            string cipherText = "";
            for (int i = 0; i < columnCount; i++)
            {
                column = Order[1, i];
                for (int j = 0; j < rowsCount; j++)
                    cipherText += matrix[j, column];
            }

            int index = cipherText.Length;
            for (int i = 0; i < index; i++)
            {
                if (cipherText[i] == '\0')
                {
                    cipherText = cipherText.Remove(i, 1);
                    index--;
                }

            }

            return cipherText;
        }

        public static string Decrypt(string cipherText, string key)
        {
            int columnCount = key.Length;
            int rowsCount = (int)Math.Ceiling((double)cipherText.Length / columnCount);
            char[,] matrix = new char[rowsCount, columnCount];

            int Index = 0;
            int[,] Order = new int[2, columnCount];
            for (int i = 0; i < columnCount; i++)
            {
                Order[0, i] = (int)key[Index];
                Order[1, i] = Index;
                if (Index != key.Length)
                    Index++;
            }

            Index = 0;
            for (int i = 0; i < rowsCount; i++)
            {
                for (int j = 0; j < columnCount; j++)
                {
                    if (Index < cipherText.Length)
                    {
                        matrix[i, j] = cipherText[Index];
                        Index++;
                    }
                    else
                        break;
                }
            }

            int temp;
            int IndexTemp;
            for (var i = 1; i < columnCount; i++)
            {
                for (var j = 0; j < columnCount - i; j++)
                {
                    if (Order[0, j] > Order[0, j + 1])
                    {
                        temp = Order[0, j + 1];
                        IndexTemp = Order[1, j + 1];
                        Order[0, j + 1] = Order[0, j];
                        Order[1, j + 1] = Order[1, j];
                        Order[0, j] = temp;
                        Order[1, j] = IndexTemp;
                    }
                }
            }

            int column;
            Index = 0;

            for (int i = 0; i < columnCount; i++)
            {
                column = Order[1, i];
                for (int j = 0; j < rowsCount; j++)
                    if (Index != cipherText.Length)
                    {
                        if (matrix[j, column] != '\0')
                        {
                            matrix[j, column] = cipherText[Index++];
                        }
                    }
            }

            string plainText = "";
            for (int i = 0; i < rowsCount; i++)
                for (int j = 0; j < columnCount; j++)
                    if (matrix[i, j] != '\0')
                        plainText += matrix[i, j];

            return plainText;
        }
    }

    static class Turning
    {
        public static string Encrypt(string plaintext, string key)
        {
            string ciphertext = "";
            Int32 index = 0;
            bool[,] punchingMask;
            Int32 sizeMatr = 5;
            punchingMask = new bool[sizeMatr, sizeMatr];
            char[,] cipherMatrix = new char[sizeMatr, sizeMatr];
            bool saveCenterCell;

            for (Int32 i = 0; i < sizeMatr; ++i)
                for (Int32 j = 0; j < sizeMatr; ++j)
                    if (key[index++] == '0')
                        punchingMask[i, j] = false;
                    else
                        punchingMask[i, j] = true;

            saveCenterCell = punchingMask[sizeMatr / 2, sizeMatr / 2];
            index = 0;

            while (plaintext.Length % (sizeMatr * sizeMatr) != 0)
                plaintext += ' ';

            while (index < plaintext.Length - 1)
            {
                for (Int32 i = 0; i < sizeMatr; ++i)
                {
                    for (Int32 j = 0; j < sizeMatr; ++j)
                    {
                        if (punchingMask[i, j])
                            cipherMatrix[i, j] = plaintext[index++];
                    }
                }

                if (punchingMask[sizeMatr / 2, sizeMatr / 2])
                    punchingMask[sizeMatr / 2, sizeMatr / 2] = false;

                for (Int32 i = 0; i < sizeMatr; i++)
                {
                    for (Int32 j = 0; j < sizeMatr; j++)
                    {
                        if (punchingMask[i, j])
                            cipherMatrix[j, sizeMatr - 1 - i] = plaintext[index++];
                    }
                }

                for (Int32 i = sizeMatr - 1; i >= 0; i--)
                {
                    for (Int32 j = sizeMatr - 1; j >= 0; j--)
                    {
                        if (punchingMask[sizeMatr - 1 - i, sizeMatr - 1 - j])
                            cipherMatrix[i, j] = plaintext[index++];
                    }
                }

                for (Int32 i = 0; i < sizeMatr - 1; i++)
                {
                    for (Int32 j = sizeMatr - 1; j >= 0; j--)
                    {
                        if (punchingMask[i, sizeMatr - 1 - j])
                            cipherMatrix[j, i] = plaintext[index++];
                    }
                }

                for (int i = 0; i < sizeMatr; ++i)
                {
                    for (int j = 0; j < sizeMatr; ++j)
                    {
                        ciphertext += cipherMatrix[i, j];
                    }
                }

                punchingMask[sizeMatr / 2, sizeMatr / 2] = saveCenterCell;
            }

            return ciphertext;
        }
        public static string Decrypt(string ciphertext, string key)
        {
            string plaintext = "";
            Int32 index = 0;
            bool[,] punchingMask;
            Int32 sizeMatr = 5;
            punchingMask = new bool[sizeMatr, sizeMatr];
            char[,] plainMatrix = new char[sizeMatr, sizeMatr];
            bool saveCenterCell;

            for (Int32 i = 0; i < sizeMatr; ++i)
                for (Int32 j = 0; j < sizeMatr; ++j)
                    if (key[index++] == '0')
                        punchingMask[i, j] = false;
                    else
                        punchingMask[i, j] = true;

            saveCenterCell = punchingMask[sizeMatr / 2, sizeMatr / 2];
            index = 0;

            while (ciphertext.Length % (sizeMatr * sizeMatr) != 0)
                ciphertext += ' ';

            for (Int32 i = 0; i < sizeMatr; i++)
            {
                for (Int32 j = 0; j < sizeMatr; j++)
                {
                    plainMatrix[i, j] = ciphertext[index++];
                }
            }

            for (Int32 i = 0; i < sizeMatr; ++i)
            {
                for (Int32 j = 0; j < sizeMatr; ++j)
                {
                    if (punchingMask[i, j])
                        plaintext += plainMatrix[i, j];
                }
            }

            if (punchingMask[sizeMatr / 2, sizeMatr / 2])
                punchingMask[sizeMatr / 2, sizeMatr / 2] = false;

            for (Int32 i = 0; i < sizeMatr; i++)
            {
                for (Int32 j = 0; j < sizeMatr; j++)
                {
                    if (punchingMask[i, j])
                        plaintext += plainMatrix[j, sizeMatr - 1 - i];
                }
            }

            for (Int32 i = sizeMatr - 1; i >= 0; i--)
            {
                for (Int32 j = sizeMatr - 1; j >= 0; j--)
                {
                    if (punchingMask[sizeMatr - 1 - i, sizeMatr - 1 - j])
                        plaintext += plainMatrix[i, j];
                }
            }

            for (Int32 i = 0; i < sizeMatr - 1; i++)
            {
                for (Int32 j = sizeMatr - 1; j >= 0; j--)
                {
                    if (punchingMask[i, sizeMatr - 1 - j])
                        plaintext += plainMatrix[j, i];
                }
            }
            punchingMask[sizeMatr / 2, sizeMatr / 2] = saveCenterCell;

            return plaintext;
        }
    }
}
