using NAudio.Wave;

namespace Presentia.Type {

    public class PTIA {

    }

    public class PTIA_File {

        public List<byte[]> images_as_byte = new List<byte[]>();

        public List<int> times = new List<int>();

        public byte[] sound;

        public void Add_Image( Image Image, int Time ) {

            ImageConverter converter = new ImageConverter();
            byte[] arr = (byte[])converter.ConvertTo(Image, typeof(byte[]));

            images_as_byte.Add( arr );

            times.Add( Time );
        }

        public void Set_Sound( byte[] Sound ) {

            sound = Sound;
        }
    }

    public class PTIE {

        public List<string> Imports = new List<string>();

        public List<Image> Images = new List<Image>();

        public class Image {

            public Image( System.Drawing.Image The_Image, int The_Import_Index, int The_Time ) {

                Drawing = The_Image;

                Import = The_Import_Index;

                Time = The_Time;
            }

            public System.Drawing.Image Drawing;

            public int Import;

            public int Time;
        }

        public Mp3FileReader Sound_Reader;
        public WaveOutEvent Sound_Output = new WaveOutEvent();

        public int? Sound_Import;
    }

    public class PTIE_File {

        public List<string> Imports = new List<string>();

        public int Sound_Import;

        public List<Image> Images = new List<Image>();

        public class Image {

            public Image( int The_Import_Index, int The_Time ) {

                Import = The_Import_Index;

                Time = The_Time;
            }

            public int Import;

            public int Time;
        }
    }
}