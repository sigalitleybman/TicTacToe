using System;
using System.Collections.Generic;
using System.Text;

namespace TicTacToe
{
    public enum ePrintReason
    {
        Define, 
        Clear,
        BoardState,
        Winner,
        HumanX,
        HumanO,
        Computer,
        Row,
        Column,
        PlaceTaken,
        Quit,
        Tie,
        WrongInput,
        Input,
    }
}
