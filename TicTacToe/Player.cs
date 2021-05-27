using System;
using System.Collections.Generic;
using System.Text;

namespace TicTacToe
{
    public class Player
    {
        private int m_Score;
        private string m_ID;
        private int m_Move;
        private int m_BoardSize;
        private int[] m_ArrayRowCounter;
        private int[] m_ArrayColumnCounter;
        private int m_PrimaryDiagonalCounter;
        private int m_SecondaryDiagonalCounter;
       
        public Player(string i_Id, int i_BoardSize)
        {
            m_ID = i_Id;
            m_Score = 0;
            m_Move = 0;
            m_BoardSize = i_BoardSize;
            m_ArrayRowCounter = new int[m_BoardSize + 1];
            m_ArrayColumnCounter = new int[m_BoardSize + 1];
            m_PrimaryDiagonalCounter = 0;
            m_SecondaryDiagonalCounter = 0;
        }

        ////properties
        public int Score
        {
            get
            {
                return m_Score;
            }

            set
            {
                m_Score = value;
            }
        }

        public string Id
        {
            get
            {
                return m_ID;
            }

            set
            {
                m_ID = value;
            }
        }

        public int Move
        {
            get
            {
                return m_Move;
            }

            set
            {
                m_Move = value;
            }
        }

        public int BoardSize
        {
            get
            {
                return m_BoardSize;
            }

            set
            {
                m_BoardSize = value;
            }
        }

        public int[] ArrayRowCounter
        {
            get
            {
                return m_ArrayRowCounter;
            }

            set
            {
                m_ArrayRowCounter = value;
            }
        }

        public int[] ArrayColumnCounter
        {
            get
            {
                return m_ArrayColumnCounter;
            }

            set
            {
                m_ArrayColumnCounter = value;
            }
        }

        public int PrimaryDiagonalCounter
        {
            get
            {
                return m_PrimaryDiagonalCounter;
            }

            set
            {
                m_PrimaryDiagonalCounter = value;
            }
        } 

        public int SecondaryDiagonalCounter
        {
            get
            {
                return m_SecondaryDiagonalCounter;
            }

            set
            {
                m_SecondaryDiagonalCounter = value;
            }
        }

        public void InitializeCounter()
        {
            Array.Clear(m_ArrayRowCounter, 0, m_ArrayRowCounter.Length);
            Array.Clear(m_ArrayColumnCounter, 0, m_ArrayColumnCounter.Length);
            m_PrimaryDiagonalCounter = 0;
            m_SecondaryDiagonalCounter = 0;
        }
    }
}
