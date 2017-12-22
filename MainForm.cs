using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Linq;
using WindowsFormsApplication341.Model;

namespace WindowsFormsApplication341
{
    public partial class MainForm : Form
    {
        private Game game;

        public MainForm()
        {
            InitializeComponent();

            game = new Game();
            game.Bounds = pnGame.ClientRectangle;
            game.InitLevel();
            pnGame.Game = game;

            var tm = new Timer{Enabled = true, Interval = 20};
            tm.Tick += delegate
            {
                var dt = 0.1f;
                game.Update(dt);
                pnGame.Invalidate();

                if (game.IsGameOver())//мяч потерян
                {
                    tm.Stop();
                    MessageBox.Show("Game over!");
                    game.InitLevel();//начало новой игры
                    tm.Start();
                }

                if (game.IsLevelCompleted())//уровень пройден?
                {
                    tm.Stop();
                    game.NextLevelNumber++;//след уровень
                    game.InitLevel();
                    tm.Start();
                }
            };
        }

        private void MainForm_Load(object sender, System.EventArgs e)
        {

        }
    }
}
