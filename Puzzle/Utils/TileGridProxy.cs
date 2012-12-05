using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Media.Imaging;
using System.Collections.Generic;
using System.IO;
using Puzzle.Vo;

namespace Puzzle.Utils {
    public class TileGridProxy {
        const string EMPTY_COL = "emptyColStateKey";
        const string EMPTY_ROW = "emptyRowStateKey";
        const string HAVE_VALID_TILE_IMAGES = "haveValidTileImageStateKey";
        const string TILES_STATE = "tilesStateStateKey";
        const string TILE_KEY_FORMAT = "tile {0} {1}";
        const int SEED = 10;

        private int horzTiles = 4;
        private int vertTiles = 4;
        private WriteableBitmap tileBitmap;
        private Grid grid;
        private Image[,] tileImages;
        private int[,] tilesState;

        private int emptyRow, emptyCol;
        private bool haveValidTileImages = false;

        private int scrambleCountdown;
        private int margin;
        private Random rand = new Random();

        public TileGridProxy(Grid grid, WriteableBitmap tileBitmap, TileSizeVo tileSizeVo, int margin) {
            this.grid = grid;
            this.tileBitmap = tileBitmap;
            this.horzTiles = tileSizeVo.Horz;
            this.vertTiles = tileSizeVo.Vert;
            this.margin = margin;
            InitTileGrid();
            InitTilesState();
        }

        public Image[,] TileImages{
            get {
                return this.tileImages;
            }
        }

        public int EmptyRow {
            get {
                return emptyRow;
            }
        }

        public int EmptyCol {
            get {
                return emptyCol;
            }
        }

        public Image[,] InitTileGrid() {
            tileImages = new Image[vertTiles, horzTiles];
            for (int col = 0; col < horzTiles; col++) {
                ColumnDefinition coldef = new ColumnDefinition();
                coldef.Width = new GridLength(1, GridUnitType.Star);
                grid.ColumnDefinitions.Add(coldef);
            }

            for (int row = 0; row < vertTiles; row++) {
                RowDefinition rowdef = new RowDefinition();
                rowdef.Height = new GridLength(1, GridUnitType.Star);
                grid.RowDefinitions.Add(rowdef);
            }

            int tileSize = this.tileBitmap.PixelWidth / horzTiles;


            emptyCol = horzTiles - 1;
            emptyRow = vertTiles - 1;
            for (int row = 0; row < vertTiles; row++) {
                for (int col = 0; col < horzTiles; col++) {
                    if (row != emptyRow || col != emptyCol) {
                        WriteableBitmap tile = new WriteableBitmap(tileSize, tileSize);

                        for (int y = 0; y < tileSize; y++) {
                            for (int x = 0; x < tileSize; x++) {
                                int yBit = row * tileSize + y;
                                int xBit = col * tileSize + x;

                                tile.Pixels[y * tileSize + x] = this.tileBitmap.Pixels[yBit * this.tileBitmap.PixelWidth + xBit];
                            }
                        }
                        GenerateImageTile(tile, row, col);
                    }
                }
            }

            haveValidTileImages = true;

            return this.tileImages;
        }

        private void GenerateImageTile(BitmapSource tile, int row, int col) {
            Image img = new Image();
            img.Stretch = Stretch.None;
            img.Source = tile;
            img.Margin = new Thickness(this.margin);
            tileImages[row, col] = img;

            Grid.SetRow(img, row);
            Grid.SetColumn(img, col);
            this.grid.Children.Add(img);
        }

        private string TileKey(int row, int col) {
            return String.Format(TILE_KEY_FORMAT, row, col);
        }

        public void SrambleTiles() {
            scrambleCountdown = SEED * vertTiles * horzTiles;

            CompositionTarget.Rendering += CompositionTargetRendering;
        }

        private void CompositionTargetRendering(object sender, EventArgs e) {
            MoveTile(tileImages[emptyRow, rand.Next(horzTiles)]);
            MoveTile(tileImages[rand.Next(vertTiles), emptyCol]);

            if (--scrambleCountdown == 0) {
                CompositionTarget.Rendering -= CompositionTargetRendering;
            }
        }

        public void MoveTile(Image img) {
            int touchedRow = -1, touchedCol = -1;

            for (int y = 0; y < vertTiles; y++) {
                for (int x = 0; x < horzTiles; x++) {
                    if (tileImages[y, x] == img) {
                        touchedCol = x;
                        touchedRow = y;
                    }
                }
            }

            if (touchedRow == emptyRow) {
                int sign = Math.Sign(touchedCol - emptyCol);

                for (int x = emptyCol; x != touchedCol; x += sign) {
                    tileImages[touchedRow, x] = tileImages[touchedRow, x + sign];

                    SwapTileStateValue(touchedRow, x, touchedRow, x + sign);
                    Grid.SetColumn(tileImages[touchedRow, x], x);
                }

                tileImages[touchedRow, touchedCol] = null;
                emptyCol = touchedCol;


            } else if (touchedCol == emptyCol) {
                int sign = Math.Sign(touchedRow - emptyRow);

                for (int y = emptyRow; y != touchedRow; y += sign) {
                    tileImages[y, touchedCol] = tileImages[y + sign, touchedCol];

                    SwapTileStateValue(y, touchedCol, y + sign, touchedCol);
                    Grid.SetRow(tileImages[y, touchedCol], y);
                }

                tileImages[touchedRow, touchedCol] = null;
                emptyRow = touchedRow;
            }
        }

        private void SwapTileStateValue(int row1, int col1, int row2, int col2) {
            int temp = tilesState[row1, col1];
            tilesState[row1, col1] = tilesState[row2, col2];
            tilesState[row2, col2] = temp;
        }

        public void InitTilesState() {
            tilesState = new int[vertTiles, horzTiles];

            int i = 0;
            for (int row = 0; row < vertTiles; row++) {
                for (int col = 0; col < horzTiles; col++) {
                    tilesState[row, col] = i++;
                }
            }
        }

        public bool MoveTileAndCheckGameOver(Image img) {
            this.MoveTile(img);

            return IsGameOver();
        }

        private bool IsGameOver() {
            for (int row = 0; row < vertTiles; row++) {
                for (int col = 0; col < horzTiles; col++) {
                    if (tilesState[row, col] != (row * horzTiles + col)) {
                        return false;
                    }
                }
            }

            return true;
        }

        public void SaveState(IDictionary<string, object> state) {
            state[HAVE_VALID_TILE_IMAGES] = haveValidTileImages;

            if (haveValidTileImages) {
                state[EMPTY_ROW] = emptyRow;
                state[EMPTY_COL] = emptyCol;

                for (int row = 0; row < vertTiles; row++) {
                    for (int col = 0; col < horzTiles; col++) {
                        if (col != emptyCol || row != emptyRow) {
                            WriteableBitmap tile = tileImages[row, col].Source as WriteableBitmap;
                            MemoryStream stream = new MemoryStream();
                            tile.SaveJpeg(stream, tile.PixelWidth, tile.PixelHeight, 0, 75);
                            state[TileKey(row, col)] = stream.GetBuffer();
                        }
                    }
                }
            }
        }

        public void LoadState(IDictionary<string, object> state) {
            object objHaveValidatTileImages;

            if (state.TryGetValue(HAVE_VALID_TILE_IMAGES, out objHaveValidatTileImages)
                    && (bool)objHaveValidatTileImages) {
                emptyRow = (int)state[EMPTY_ROW];
                emptyCol = (int)state[EMPTY_COL];

                for (int row = 0; row < vertTiles; row++) {
                    for (int col = 0; col < horzTiles; col++) {
                        if (col != emptyCol || row != emptyRow) {
                            byte[] buffer = (byte[])state[TileKey(row, col)];
                            MemoryStream stream = new MemoryStream(buffer);
                            BitmapImage bitmapImage = new BitmapImage();
                            bitmapImage.SetSource(stream);
                            WriteableBitmap tile = new WriteableBitmap(bitmapImage);
                            GenerateImageTile(tile, row, col);
                        }
                    }
                }

                haveValidTileImages = true;
            }
        }
    }
}
