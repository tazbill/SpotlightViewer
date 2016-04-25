using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace SpotlightViewer.Model
{
    public class SpotlightImageFile
    {
        public string ImageFileName { get; set; }
        public Image FullImage { get; set; }
        public Image Thumbnail { get; set; }
    }
}
