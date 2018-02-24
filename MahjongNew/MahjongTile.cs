using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MahjongNew
{
    class MahjongTile
    {
        // === Public Functions ===
        public MahjongTile(int InTileIndex)
        {
            // If the tile index is bigger than the max amount of tiles, clamp to it
            TileIndex = (InTileIndex > 42) ? 42 : InTileIndex;

            Rectangle SourceRegion = new Rectangle(TileIndex * TileSize.X, 0, TileSize.X, TileSize.Y);
            Bitmap TilesAtlas = new Bitmap(Properties.Resources.MahjongTileset);
            Bitmap TempTileTexture = TilesAtlas.Clone(SourceRegion, TilesAtlas.PixelFormat);
            TempTileTexture.RotateFlip(RotateFlipType.Rotate180FlipY);

            TileTexture = TempTileTexture;
            Console.WriteLine("Generated texture for tile " + TileIndex.ToString());

            TileCanvas = new PictureBox();
            TileCanvas.Name = "MahjongTile_" + InTileIndex;
            TileCanvas.Size = new Size(TileSize.X, TileSize.Y);
            TileCanvas.BackColor = Color.Transparent;
            TileCanvas.BackgroundImage = TileTexture;
            TileCanvas.Paint += TileCanvas_Paint;
            TileCanvas.MouseEnter += TileCanvas_MouseEnter;
            TileCanvas.MouseLeave += TileCanvas_MouseLeave;
            TileCanvas.MouseClick += TileCanvas_MouseClick;
        }

        private void TileCanvas_MouseClick(object sender, MouseEventArgs e)
        {
            IsClicked = true;
            OnTileClicked.Invoke(this, this);
        }

        private void TileCanvas_MouseLeave(object sender, EventArgs e)
        {
            IsHovered = false;
            TileCanvas.Invalidate();
        }

        private void TileCanvas_MouseEnter(object sender, EventArgs e)
        {
            IsHovered = true;
            TileCanvas.Invalidate();
        }

        private void TileCanvas_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawString(GetTileIndex().ToString(), new Font("Arial", 13), new SolidBrush(Color.Black), 0,0 );

            if (IsHovered && !IsClicked)
                e.Graphics.DrawRectangle(new Pen(Color.Black, 5), 0, 0, TileSize.X, TileSize.Y);
            else if (IsClicked)
                e.Graphics.DrawRectangle(new Pen(Color.Red, 5), 0, 0, TileSize.X, TileSize.Y);
        }

        public Image GetTileTexture()
        {
            return TileTexture;
        }

        public int GetTileIndex()
        {
            return TileIndex;
        }

        // === Private Members ===
        private int TileIndex = 0;
        private Point TileSize = new Point(48, 66);
        private Image TileTexture;
        public PictureBox TileCanvas;
        public bool IsHovered;
        public bool IsClicked;
        public EventHandler<MahjongTile> OnTileClicked;
    }
}
