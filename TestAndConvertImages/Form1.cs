
using System.Windows.Forms;


namespace TestAndConvertImages
{


    public partial class Form1 : Form
    {

        private static System.Collections.Generic.IDictionary<string, string> m_mappings;


        public Form1()
        {
            InitializeComponent();
            m_mappings = MappingsDictionary.Init();
        }


        public static string GetMimeType(System.Drawing.Image i)
        {
            var imgguid = i.RawFormat.Guid;
            foreach (System.Drawing.Imaging.ImageCodecInfo codec in System.Drawing.Imaging.ImageCodecInfo.GetImageDecoders())
            {
                if (codec.FormatID == imgguid)
                    return codec.MimeType;
            }
            return "image/unknown";
        }


        // https://stackoverflow.com/questions/6495952/how-can-i-save-a-gif-with-a-transparent-background
        // https://stackoverflow.com/questions/32824773/saving-transparent-png-as-transparent-gif
        private static System.Drawing.Bitmap LoadGif(string path)
        {
            System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(path);
            // bool found = false;
            foreach (System.Drawing.Imaging.PropertyItem item in bmp.PropertyItems)
            {
                if (item.Id == 20740)
                {
                    int paletteIndex = item.Value[0];
                    System.Drawing.Color backGround = bmp.Palette.Entries[paletteIndex];
                    bmp.MakeTransparent(backGround);
                    // found = true;
                    break;
                }
            }

            // Property missing, punt at the color of the lower-left pixel
            //if (!found) bmp.MakeTransparent();
            return bmp;
        }


        private void ConvertWrongFiles()
        {
            string path = @"D:\username\Documents\Visual Studio 2013\TFS\COR-Basic\COR-Basic\Basicv3\Basic\images";
            path = @"D:\username\Documents\Visual Studio 2013\TFS\COR-Basic\COR-Basic\Basic\Basic\images";
            path = @"D:\username\Documents\Visual Studio 2013\TFS\SwissRE\SwissRe_V3\SwissRe\images";
            path = @"D:\username\Documents\Visual Studio 2013\TFS\COR-FM-Suite\COR_FM-Suite\Portal\images\icon";

            string[] filez = System.IO.Directory.GetFiles(path, "*.*", System.IO.SearchOption.TopDirectoryOnly);

            foreach (string fileName in filez)
            {
                string ext = System.IO.Path.GetExtension(fileName);

                using (System.Drawing.Image pic = System.Drawing.Image.FromFile(fileName))
                {
                    string expectedMimeType = m_mappings[ext];
                    string actualMimeType = GetMimeType(pic);


                    if (!System.StringComparer.OrdinalIgnoreCase.Equals(expectedMimeType, actualMimeType))
                    { 
                        System.Console.WriteLine("Error in file \"{0}\":", fileName);
                        System.Console.WriteLine("   - Expected: \"{0}\":", expectedMimeType);
                        System.Console.WriteLine("   - Actual: \"{0}\":", actualMimeType);


                           //using(var img = new Bitmap(pic)) // pic.Width, pic.Height))
                           //{
                           //    //for(int x = 0; x < pic.Width;++x)
                           //    //{
                           //    // for(int y=0; y < pic.Height;++y)
                           //    // {
                           //    //     System.Drawing.Color col = img.GetPixel(x, y);
                           //    //     int foo = (int)col.A;
                           //    //     if(foo != 0)
                           //    //     System.Console.WriteLine(foo);

                           //    // }
                           //    //}

                               



                           //    System.Drawing.Color transparent = System.Drawing.Color.FromArgb(0, 0, 0, 0);


                           //    //using (Graphics gfx = Graphics.FromImage(img))
                           //    //{
                           //    //    gfx.Clear(transparent);
                           //    //    gfx.DrawImage(pic, 0, 0);
                           //    //}



                           //    img.MakeTransparent(transparent);
                           //    string newfName = System.IO.Path.GetFileName(fileName);
                           //    img.Save(newfName, System.Drawing.Imaging.ImageFormat.Gif);
                           //}
                            
                           string newfName = System.IO.Path.GetFileName(fileName);
                           pic.Save(newfName, System.Drawing.Imaging.ImageFormat.Gif);
                    } // End if (!System.StringComparer.OrdinalIgnoreCase.Equals(expectedMimeType, actualMimeType)) 

                } // End Using pic 

            } // Next fileName 

        } // End Sub ConvertWrongFiles 


        private void button1_Click(object sender, System.EventArgs e)
        {
            ConvertWrongFiles();
        } // End Sub button1_Click 


    } // End Class Form1 : Form


} // End Namespace TestAndConvertImages
