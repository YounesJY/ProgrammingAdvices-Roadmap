using System;
using System.Drawing;
using System.Windows.Forms;
using TicTacToeGame.Properties;

namespace TicTacToeGame
{
    public partial class FormTicTacToe : Form
    {
        private enum enPlayers : byte { player1 = 1, player2 = 2 };
        private enum enWinner : byte { inProgress = 0, player1 = 1, player2 = 2, draw = 3 };
        private enum enFlags : byte { none = 0, X = 1, O = 2 };

        private struct GameStatus
        {
            public enPlayers currentPlayer;
            public enWinner winner;
            public bool isGameOver;
        };


        PictureBox[] pictureBoxes;
        GameStatus gameStatus;
        byte playCount;


        public FormTicTacToe()
        {
            InitializeComponent();
        }
        private void formTicTacToe_Load(object sender, EventArgs e)
        {
            pictureBoxes = new PictureBox[]{
                        pictureBox1,
                        pictureBox2,
                        pictureBox3,
                        pictureBox4,
                        pictureBox5,
                        pictureBox6,
                        pictureBox7,
                        pictureBox8,
                        pictureBox9
                    };

            gameStatus = new GameStatus
            {
                currentPlayer = enPlayers.player1,
                winner = enWinner.inProgress,
                isGameOver = false
            };

            playCount = 0;


            updateTurns();
            resetPictureBoxes();
        }
        private void formTicTacToe_Paint(object sender, PaintEventArgs e)
        {
            Pen pen = new Pen(Color.White);

            pen.Width = 5;
            pen.StartCap = System.Drawing.Drawing2D.LineCap.Round;
            pen.EndCap = System.Drawing.Drawing2D.LineCap.Round;

            e.Graphics.DrawLine(pen, 525, 150, 525, 425);
            e.Graphics.DrawLine(pen, 650, 150, 650, 425);

            e.Graphics.DrawLine(pen, 400, 225, 750, 225);
            e.Graphics.DrawLine(pen, 400, 325, 750, 325);
        }


        private void resetPictureBoxes()
        {
            foreach (var pictureBox in pictureBoxes)
            {
                pictureBox.Enabled = true;
                pictureBox.Image = Resources.questionLogo;
                pictureBox.Tag = enFlags.none;
            }
        }

        private void setGameWinner()
        {
            switch (gameStatus.winner)
            {
                case enWinner.player1:
                    lblWinner.Text = "Player 1";
                    break;
                case enWinner.player2:
                    lblWinner.Text = "Player 2";
                    break;
                case enWinner.draw:
                    lblWinner.Text = "Draw";
                    break;
                default:
                    lblWinner.Text = "In Progess";
                    break;
            }
        }
        private void endGame()
        {
            gameStatus.isGameOver = true;
            lblTurn.Text = "Game Over";

            setGameWinner();
            MessageBox.Show("Game Over :)", "Just Stop Ok !", MessageBoxButtons.OK, MessageBoxIcon.Information);

            foreach (PictureBox pictureBox in pictureBoxes)
                pictureBox.Enabled = false;
        }

        private bool isRowCompleted(PictureBox[] pictures)
        {
            return (
                (enFlags)pictures[0].Tag != enFlags.none
                && pictures[0].Tag.ToString() == pictures[1].Tag.ToString()
                && pictures[0].Tag.ToString() == pictures[2].Tag.ToString()
            );
        }
        private void markTheCompletedRow(PictureBox[] pictures)
        {
            foreach (PictureBox picture in pictures)
                picture.Image = (gameStatus.winner == enWinner.player1 ? Resources.xLogoWinner : Resources.oLogoWinner);
        }

        private bool setImageLogo(PictureBox pictureBox)
        {
            if ((enFlags)(pictureBox.Tag) != enFlags.none)
            {
                MessageBox.Show("Wrong choice !", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            else
            {
                pictureBox.Image = (gameStatus.currentPlayer == enPlayers.player1) ? Resources.xLogo : Resources.oLogo;
                pictureBox.Tag = ((gameStatus.currentPlayer == enPlayers.player1) ? enFlags.X : enFlags.O);
                return true;
            }
        }
        private void checkForWinner()
        {
            /*
                //========================================================================
                //========================= [IMPORTANT NOTE] =============================
                //========================================================================
                //== -> you should learn about "Jagged Array vs MultiDimensional Array" ==
                //== -> you'll have problems in the foreach section if you misuse them  ==
                //========================================================================
            */

            PictureBox[][] pictureBoxesFlags = new PictureBox[][]
            {
              // Rows
              new PictureBox[]  { pictureBox1, pictureBox2, pictureBox3 },
              new PictureBox[]  { pictureBox4, pictureBox5, pictureBox6 },
              new PictureBox[]  { pictureBox7, pictureBox8, pictureBox9 },
              
              // Columns
              new PictureBox[]  { pictureBox1, pictureBox4, pictureBox7,},
              new PictureBox[]  { pictureBox2, pictureBox5, pictureBox8 },
              new PictureBox[]  { pictureBox3, pictureBox6, pictureBox9 },
              
              // Diagonals
              new PictureBox[]  { pictureBox1, pictureBox5, pictureBox9 },
              new PictureBox[]  { pictureBox3, pictureBox5, pictureBox7 }
            };


            foreach (PictureBox[] row in pictureBoxesFlags)
            {
                if (isRowCompleted(row))
                {
                    gameStatus.winner = (enWinner)gameStatus.currentPlayer;
                    markTheCompletedRow(row);
                    endGame();
                    break;
                }
            }

            if (playCount == 9 && gameStatus.winner == enWinner.inProgress)
            {
                gameStatus.winner = enWinner.draw;
                endGame();
            }
        }
        private void switchTurns()
        {
            gameStatus.currentPlayer = (gameStatus.currentPlayer == enPlayers.player1) ? enPlayers.player2 : enPlayers.player1;
        }
        private void updateTurns()
        {
            lblTurn.Text = (gameStatus.currentPlayer == enPlayers.player1) ? "Player 1" : "Player 2";
        }


        private void pictureBox_Click(object sender, EventArgs e)
        {
            if (setImageLogo((PictureBox)sender))
            {
                ++playCount;
                checkForWinner();
                if (!gameStatus.isGameOver)
                {
                    switchTurns();
                    updateTurns();
                }
            }
        }
        private void btnRestartGame_Click(object sender, EventArgs e)
        {
            gameStatus.currentPlayer = enPlayers.player1;
            gameStatus.winner = enWinner.inProgress;
            gameStatus.isGameOver = false;

            lblWinner.Text = "In Progress";
            playCount = 0;

            updateTurns();
            resetPictureBoxes();
        }
    }
}
