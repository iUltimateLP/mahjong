using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.IO;

namespace MahjongNew
{
    class MahjongBoard
    {
        // === Public Functions ===
        public MahjongBoard(MainWindow WindowHandle, bool InTestMode)
        {
            TestMode = InTestMode;
            this.WindowHandle = WindowHandle;

            /*
            for (int x = 0; x < 7; x++)
            {
                for (int y = 0; y < 7; y++)
                {
                    MahjongTile Tile = new MahjongTile(y * 7 + 0 + x);
                    TileStore[x, y, 0] = Tile;
                    Tile.TileCanvas.Location = new Point(x * 43, y * 61);
                    Tile.OnTileClicked += OnTileClicked;
                    WindowHandle.Controls.Add(Tile.TileCanvas);
                    WindowHandle.Controls.SetChildIndex(Tile.TileCanvas, 0);
                }
            }

            for (int x = 0; x < 4; x++)
            {
                for (int y = 0; y < 4; y++)
                {
                    MahjongTile Tile = new MahjongTile(y * 7 + 0 + x);
                    TileStore[x, y, 1] = Tile;
                    Tile.TileCanvas.Location = new Point(x * 43 - (1 * 5), y * 61 - (1 * 5));
                    Tile.OnTileClicked += OnTileClicked;
                    WindowHandle.Controls.Add(Tile.TileCanvas);
                    WindowHandle.Controls.SetChildIndex(Tile.TileCanvas, 0);
                }
            }

            for (int x = 0; x < 2; x++)
            {
                for (int y = 0; y < 2; y++)
                {
                    MahjongTile Tile = new MahjongTile(y * 7 + 0 + x);
                    TileStore[x, y, 2] = Tile;
                    Tile.TileCanvas.Location = new Point(x * 43 - (2 * 5), y * 61 - (2 * 5));
                    Tile.OnTileClicked += OnTileClicked;
                    WindowHandle.Controls.Add(Tile.TileCanvas);
                    WindowHandle.Controls.SetChildIndex(Tile.TileCanvas, 0);
                }
            }*/

            LoadBoardFromFile("",false);
        }

        public void LoadBoardFromFile(string Filename, bool Center)
        {
            string Board = Properties.Resources.TurtleBoard;
            string[] lines = Board.Split(
                new[] { Environment.NewLine },
                StringSplitOptions.None
            );

            for(int y = 0; y < lines.Length; y++)
            {
                char[] CharArray = lines[y].ToCharArray();
                for(int x = 0; x < CharArray.Length; x++)
                {
                    char Char = CharArray[x];
                    if (Char != '0')
                    {
                        int ZIndex = int.Parse(Char.ToString());
                        Point Coordinate = new Point(x, y);

                        MahjongTile Tile = new MahjongTile(0);
                        TileStore[x, y, ZIndex] = Tile;
                        Tile.TileCanvas.Location = new Point(x * 43 - (ZIndex * 5), y * 61 - (ZIndex * 5));
                        Tile.OnTileClicked += OnTileClicked;
                        WindowHandle.Controls.Add(Tile.TileCanvas);
                        WindowHandle.Controls.SetChildIndex(Tile.TileCanvas, 0);
                    }
                }
            }
        }

        public void OnTileClicked(object sender, MahjongTile ClickedTile)
        {
            SelectedTiles.Add(ClickedTile);

            if (SelectedTiles.Count == 2)
            {
                WindowHandle.Controls.Remove(SelectedTiles[0].TileCanvas);
                WindowHandle.Controls.Remove(SelectedTiles[1].TileCanvas);

                SelectedTiles.Clear();
            }
        }

        // === Private Members ===
        MahjongTile[,,] TileStore = new MahjongTile[32, 32, 6];
        List<MahjongTile> SelectedTiles = new List<MahjongTile>(2);
        MainWindow WindowHandle;
        bool TestMode;
    }
}
