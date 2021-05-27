using System;
using System.Collections.Generic;
using System.Text;

namespace TicTacToe
{
    public class BoardGame
    {
        private readonly int r_Size;
        private char[,] m_GameMatrix;
        
        public BoardGame(int i_Size)
        {
            r_Size = i_Size;
            m_GameMatrix = new char[r_Size + 1, r_Size + 1];
        }
 
        public int Size
        {
            get
            {
                return r_Size;
            }   
        }
        
        public char[,] GameMatrix
        {
            get
            {
                return m_GameMatrix;
            }

            set
            {
                m_GameMatrix = value;
            }
        }

        public void ClearGameBoard()
        {
            Array.Clear(this.m_GameMatrix, 0, m_GameMatrix.Length);
        }   
    }
}