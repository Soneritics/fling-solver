using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Fling_Solver
{
    public partial class FlingSolver : Form
    {
        public FlingSolver()
        {
            InitializeComponent();
        }

        private bool GameEnded = false;
        private int A = 0, B = 1, C = 2, D = 3, E = 4, F = 5, G = 6, H = 7;
        private int board_width = 7, board_height = 8;

        private void writeBoardToConsole(bool[][] board)
        { 
            Console.WriteLine("  A B C D E F G");

            for (int y = 0; y < board_height; y++)
            {
                Console.Write(y.ToString() + " ");

                for (int x = 0; x < board_width; x++)
                    Console.Write((board[x][y] ? "x" : " ") + " ");

                Console.Write("\n");
            }                
        }

        private bool isGameEnded(bool[][] board)
        {
            int found = 0;
            for (var i = 0; i < board_width; i++)
                for (var j = 0; j < board_height; j++ )
                    if ( board[i][j] )
                        found++;

            if ( found == 1 )
                GameEnded = true;

            return GameEnded;
        }

        private bool canMove(bool[][] board, int x, int y, int direction)
        {
            switch (direction)
            {
                case 0: // Up
                    // Top line
                    if ( y <= 1 )
                        return false;

                    if (board[x][y - 1])
                        return false;

                    for (int _y = y - 2; _y >= 0; _y-- )
                        if ( board[x][_y] )
                            return true;

                    return false;
                break;

                case 1: // Right
                    // Two most right lines
                    if (x >= board_width - 2)
                        return false;

                    if (board[x + 1][y])
                        return false;

                    for (int _x = x + 2; _x < board_width; _x++)
                        if ( board[_x][y] )
                            return true;

                    return false;
                break;

                case 2: // Down
                    // Two most bottom lines
                    if (y >= board_height - 2)
                        return false;

                    if (board[x][y + 1])
                        return false;

                    for (int _y = y + 2; _y < board_height; _y++ )
                        if ( board[x][_y] )
                            return true;

                    return false;
                break;

                case 3: // Left
                    // Two most left lines
                    if ( x <= 1 )
                        return false;

                    if (board[x - 1][y])
                        return false;

                    for (int _x = x - 2; _x >= 0; _x-- )
                        if ( board[_x][y] )
                            return true;

                    return false;
                break;
            }

            return false;
        }

        private String getDirection(int direction)
        {
            String result = "No Direction";

            switch (direction)
            {
                case 0: result = "up"; break;
                case 1: result = "right"; break;
                case 2: result = "down"; break;
                case 3: result = "left"; break;
            }

            return result;
        }

        private String giveLetter(int x)
        {
            switch ( x )
            {
                case 0: return "A"; break;
                case 1: return "B"; break;
                case 2: return "C"; break;
                case 3: return "D"; break;
                case 4: return "E"; break;
                case 5: return "F"; break;
                case 6: return "G"; break;
            }

            return "X";
        }

        private bool[][] generateBoard(bool[][] board, int x, int y, int direction)
        {
            // First, copy the board
            bool[][] result = new bool[board_height][];

            for (int i = 0; i < board_width; i++)
            {
                result[i] = new bool[board_height];
                for (int j = 0; j < board_height; j++ )
                    result[i][j] = board[i][j];
            }

            // Move the fling to the chosen direction
            switch ( direction )
            {
                case 0: // Up
                    // Moves from old position
                    result[x][y] = false;

                    // To new position
                    while (y >= 0)
                    {
                        if (result[x][y])
                        {
                            // New position for
                            result[x][y + 1] = true;

                            // Quit the loop
                            break;
                        }
                        y--;
                    }

                    // Move the fling at current x,y
                    if (y > 0)
                        while (y > 0)
                        {
                            result[x][y] = false;

                            if (result[x][y - 1])
                                result[x][y] = true;

                            y--;
                        }

                    if (y == 0)
                        result[x][y] = false;
                break;

                case 1: // Right
                    // Moves from old position
                    result[x][y] = false;

                    // To new position
                    while (x < board_width)
                    {
                        if (result[x][y])
                        {
                            // New position for
                            result[x - 1][y] = true;

                            // Quit the loop
                            break;
                        }
                        x++;
                    }

                    // Move the fling at current x,y
                    if (x < board_width - 1)
                        while (x < board_width - 1)
                        {
                            result[x][y] = false;

                            if (result[x + 1][y])
                                result[x][y] = true;

                            x++;
                        }

                    if (x == board_width - 1)
                        result[x][y] = false;
                break;

                case 2: // Down
                    // Moves from old position
                    result[x][y] = false;

                    // To new position
                    while (y < board_height)
                    {
                        if (result[x][y])
                        {
                            // New position for
                            result[x][y - 1] = true;

                            // Quit the loop
                            break;
                        }
                        y++;
                    }

                    // Move the fling at current x,y
                    if (y < board_height - 1)
                        while (y < board_height - 1)
                        {
                            result[x][y] = false;

                            if (result[x][y + 1])
                                result[x][y] = true;

                            y++;
                        }

                    if (y == board_height - 1)
                        result[x][y] = false;
                break;

                case 3: // Left
                    // Moves from old position
                    result[x][y] = false;

                    // To new position
                    while (x >= 0)
                    {
                        if (result[x][y])
                        {
                            // New position for
                            result[x + 1][y] = true;

                            // Quit the loop
                            break;
                        }
                        x--;
                    }

                    // Move the fling at current x,y
                    if (x > 0)
                        while (x > 0)
                        {
                            result[x][y] = false;

                            if (result[x - 1][y])
                                result[x][y] = true;

                            x--;
                        }

                    if (x == 0)
                        result[x][y] = false;
                break;
            }

            // Return the new board
            return result;
        }

        private String solve(bool[][] board, String walkthrough)
        {
            // Result
            String _walkthrough = walkthrough;

            // Loop the board while not Game Ended
            int y = 0;
            while (y < board_height && !GameEnded)
            {
                for ( int x = 0; x < board_width; x++ )
                    if (board[x][y])
                        for ( int direction = 0; direction < 4; direction++ )
                            if (canMove(board, x, y, direction))
                            {
                                bool[][] newboard = generateBoard(board, x, y, direction);
                                _walkthrough = walkthrough + "\nMove " + giveLetter(x) + (y + 1).ToString() + " " + getDirection(direction) + solve(newboard, walkthrough);

                                if (isGameEnded(newboard))
                                    return _walkthrough;
                            }

                y++;
            }

            return _walkthrough;
        }

        // Solve button onclick event, starts the solving
        private void solveButton_Click(object sender, EventArgs e)
        {
            // Init
            GameEnded = false;

            // Load the board as on the screen
            bool[][] board = new bool[board_width][];
            for (int i = 0; i < board_width; i++)
                board[i] = new bool[board_height];

            board[A][0] = A1.Text == "x";
            board[A][1] = A2.Text == "x";
            board[A][2] = A3.Text == "x";
            board[A][3] = A4.Text == "x";
            board[A][4] = A5.Text == "x";
            board[A][5] = A6.Text == "x";
            board[A][6] = A7.Text == "x";
            board[A][7] = A8.Text == "x";

            board[B][0] = B1.Text == "x";
            board[B][1] = B2.Text == "x";
            board[B][2] = B3.Text == "x";
            board[B][3] = B4.Text == "x";
            board[B][4] = B5.Text == "x";
            board[B][5] = B6.Text == "x";
            board[B][6] = B7.Text == "x";
            board[B][7] = B8.Text == "x";

            board[C][0] = C1.Text == "x";
            board[C][1] = C2.Text == "x";
            board[C][2] = C3.Text == "x";
            board[C][3] = C4.Text == "x";
            board[C][4] = C5.Text == "x";
            board[C][5] = C6.Text == "x";
            board[C][6] = C7.Text == "x";
            board[C][7] = C8.Text == "x";

            board[D][0] = D1.Text == "x";
            board[D][1] = D2.Text == "x";
            board[D][2] = D3.Text == "x";
            board[D][3] = D4.Text == "x";
            board[D][4] = D5.Text == "x";
            board[D][5] = D6.Text == "x";
            board[D][6] = D7.Text == "x";
            board[D][7] = D8.Text == "x";

            board[E][0] = E1.Text == "x";
            board[E][1] = E2.Text == "x";
            board[E][2] = E3.Text == "x";
            board[E][3] = E4.Text == "x";
            board[E][4] = E5.Text == "x";
            board[E][5] = E6.Text == "x";
            board[E][6] = E7.Text == "x";
            board[E][7] = E8.Text == "x";

            board[F][0] = F1.Text == "x";
            board[F][1] = F2.Text == "x";
            board[F][2] = F3.Text == "x";
            board[F][3] = F4.Text == "x";
            board[F][4] = F5.Text == "x";
            board[F][5] = F6.Text == "x";
            board[F][6] = F7.Text == "x";
            board[F][7] = F8.Text == "x";

            board[G][0] = G1.Text == "x";
            board[G][1] = G2.Text == "x";
            board[G][2] = G3.Text == "x";
            board[G][3] = G4.Text == "x";
            board[G][4] = G5.Text == "x";
            board[G][5] = G6.Text == "x";
            board[G][6] = G7.Text == "x";
            board[G][7] = G8.Text == "x";

            // Debugging
            writeBoardToConsole(board);

            // Do the magic
            String walkthrough = solve(board, "");

            // GUI
            if ( !GameEnded )
                MessageBox.Show("This game could not be solved!");
            else
                MessageBox.Show("The game has been solved, try the following:\n\n" + walkthrough);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            A1.Text = "";
            A2.Text = "";
            A3.Text = "";
            A4.Text = "";
            A5.Text = "";
            A6.Text = "";
            A7.Text = "";
            A8.Text = "";

            B1.Text = "";
            B2.Text = "";
            B3.Text = "";
            B4.Text = "";
            B5.Text = "";
            B6.Text = "";
            B7.Text = "";
            B8.Text = "";

            C1.Text = "";
            C2.Text = "";
            C3.Text = "";
            C4.Text = "";
            C5.Text = "";
            C6.Text = "";
            C7.Text = "";
            C8.Text = "";

            D1.Text = "";
            D2.Text = "";
            D3.Text = "";
            D4.Text = "";
            D5.Text = "";
            D6.Text = "";
            D7.Text = "";
            D8.Text = "";

            E1.Text = "";
            E2.Text = "";
            E3.Text = "";
            E4.Text = "";
            E5.Text = "";
            E6.Text = "";
            E7.Text = "";
            E8.Text = "";

            F1.Text = "";
            F2.Text = "";
            F3.Text = "";
            F4.Text = "";
            F5.Text = "";
            F6.Text = "";
            F7.Text = "";
            F8.Text = "";

            G1.Text = "";
            G2.Text = "";
            G3.Text = "";
            G4.Text = "";
            G5.Text = "";
            G6.Text = "";
            G7.Text = "";
            G8.Text = "";
        }

        private void A8_Click(object sender, EventArgs e)
        {
            (sender as TextBox).Text = (sender as TextBox).Text == "" ? "x" : "";
        }
    }
}
