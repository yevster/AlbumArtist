using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TagLib;

namespace AlbumArtist
{
    public partial class frmAlbumArtist : Form
    {
        public frmAlbumArtist()
        {
            InitializeComponent();
        }

        private void frmAlbumArtist_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (files.Length == 0) return;
            
            DialogResult dialogResult = dlgImageSelect.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                String selectedImagePath = dlgImageSelect.FileName;
                var pictures = new Picture[] { new Picture(selectedImagePath) };
                foreach (string file in files)
                {
                    var tagFile = File.Create(file);
                    tagFile.Tag.Pictures = pictures;
                    tagFile.Save();
                }                
            }
        }

        private void frmAlbumArtist_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy;
        }

        private void frmAlbumArtist_Load(object sender, EventArgs e)
        {
            dlgImageSelect.Filter = "Image files |*.PNG; *.GIF;*.JPG; *.JPEG; *.JFIF";
        }
    }
}
