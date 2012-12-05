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

namespace Puzzle.Vo {
    public class TileSizeVo {
        public TileSizeVo (int horz, int vert) {
            Horz = horz;
            Vert = vert;
        }

        public int Horz { get; set; }
        public int Vert { get; set; }
    }
}
