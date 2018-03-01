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
    // Struct containing a simple three-dimensional vector
    struct Vector
    {
        public int X;
        public int Y;
        public int Z;

        public Vector(int InX, int InY, int InZ)
        {
            X = InX;
            Y = InY;
            Z = InZ;
        }

        public static bool operator==(Vector Vec1, Vector Vec2)
        {
            return (Vec1.X == Vec2.X && Vec1.Y == Vec2.Y && Vec1.Z == Vec2.Z);
        }

        public static bool operator!=(Vector Vec1, Vector Vec2)
        {
            return (Vec1.X != Vec2.X || Vec1.Y != Vec2.Y || Vec1.Z != Vec2.Z);
        }
    }

    // Class containing the mahjong board
    class MahjongBoard
    {
        // === Public Functions ===
        public MahjongBoard(MainWindow WindowHandle)
        {
            this.WindowHandle = WindowHandle;

            LoadBoard();
        }

        public void LoadBoard()
        {
            string Board = Properties.Resources.TurtleBoard;
            string[] lines = Board.Split(
                new[] { Environment.NewLine },
                StringSplitOptions.None
            );

            Random TileIndexRandom = new Random();

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

                        for (int z = 0; z < ZIndex; z++)
                        {
                            int Index = TileIndexRandom.Next(0, 42);
                            CreateTile(2 +x, 1+y, z, Index);
                        }
                    }
                }
            }
        }

        public void OnTileClicked(object sender, MahjongTile ClickedTile)
        {
            SelectedTiles.Add(ClickedTile);

            if (SelectedTiles.Count == 2)
            {
                if (SelectedTiles[0] == SelectedTiles[1])
                {
                    SelectedTiles.RemoveAt(1);
                    return;
                }

                int x1 = SelectedTiles[0].TileVector.X;
                int y1 = SelectedTiles[0].TileVector.Y;
                int z1 = SelectedTiles[0].TileVector.Z;
                int x2 = SelectedTiles[1].TileVector.X;
                int y2 = SelectedTiles[1].TileVector.Y;
                int z2 = SelectedTiles[1].TileVector.Z;

                bool FirstTileAccessible = TileStore[x1 - 1, y1, z1] == null || TileStore[x1 + 1, y1, z1] == null;
                bool SecondTileAccessible = TileStore[x2 - 1, y2, z2] == null || TileStore[x2 + 1, y2, z2] == null;
                bool SameTile = SelectedTiles[0].GetTileIndex() == SelectedTiles[1].GetTileIndex();

                if (FirstTileAccessible && SecondTileAccessible && SameTile)
                {
                    Console.WriteLine("jo geil");
                }
                else
                {
                    SelectedTiles[0].IsHovered = false;
                    SelectedTiles[0].IsClicked = false;
                    SelectedTiles[0].TileCanvas.Invalidate();
                    SelectedTiles[1].IsHovered = false;
                    SelectedTiles[1].IsClicked = false;
                    SelectedTiles[1].TileCanvas.Invalidate();
                    SelectedTiles.Clear();
                    Console.WriteLine("ne");
                    return;
                }

                WindowHandle.Controls.Remove(SelectedTiles[0].TileCanvas);
                WindowHandle.Controls.Remove(SelectedTiles[1].TileCanvas);

                TileStore[x1, y1, z1] = null;
                TileStore[x2, y2, z2] = null;

                SelectedTiles.Clear();
            }
        }

        public MahjongTile CreateTile(int X, int Y, int Z, int TileIndex)
        {
            MahjongTile Tile = new MahjongTile(TileIndex);
            TileStore[X, Y, Z] = Tile;
            Tile.TileVector = new Vector(X, Y, Z);
            Tile.TileCanvas.Location = new Point(X * 43 - (Z * 5), Y * 61 - (Z * 5));
            Tile.OnTileClicked += OnTileClicked;
            WindowHandle.Controls.Add(Tile.TileCanvas);
            WindowHandle.Controls.SetChildIndex(Tile.TileCanvas, 0);

            return Tile;
        }

        // === Private Members ===
        MahjongTile[,,] TileStore = new MahjongTile[32, 32, 6];
        List<MahjongTile> SelectedTiles = new List<MahjongTile>(2);
        MainWindow WindowHandle;
    }
}
