using NAudio.Wave;
using Newtonsoft.Json;

namespace Presentia {

    namespace Type {

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

    public static class Convert {

        public static void              PTIE_To_PTIE_File       ( Type.PTIE         The_PTIE            , string The_PTIE_File_Path ) {

            string Json = JsonConvert.SerializeObject(PTIE_To_PTIE_File(The_PTIE));
            File.WriteAllText( The_PTIE_File_Path, Json );
        }
        public static Type.PTIE_File    PTIE_To_PTIE_File       ( Type.PTIE         The_PTIE                                        ) {

            Type.PTIE_File The_PTIE_File = new Type.PTIE_File();

            The_PTIE_File.Imports = The_PTIE.Imports;
            The_PTIE_File.Sound_Import = (int)The_PTIE.Sound_Import;

            for( int i = 0 ; i < The_PTIE.Images.Count ; i++ )
                The_PTIE_File.Images.Add( new Type.PTIE_File.Image( The_PTIE.Images[i].Import, The_PTIE.Images[i].Time ) );

            return The_PTIE_File;
        }
        public static void              PTIE_To_PTIA_File       ( Type.PTIE         The_PTIE            , string The_PTIA_File_Path ) {

            Type.PTIA_File The_PTIA_File = new Type.PTIA_File();

            for( int i = 0 ; i < The_PTIE.Images.Count ; i++ )
                The_PTIA_File.Add_Image( The_PTIE.Images[i].Drawing, The_PTIE.Images[i].Time );

            The_PTIA_File.Set_Sound( File.ReadAllBytes( The_PTIE.Imports[(int)The_PTIE.Sound_Import] ) );

            object obj = new { Sound = The_PTIA_File.sound, Images = The_PTIA_File.images_as_byte, Times = The_PTIA_File.times };

            string json = JsonConvert.SerializeObject(obj);

            File.WriteAllText( The_PTIA_File_Path, json );
        }

        public static Type.PTIE         PTIE_File_To_PTIE       ( string            The_PTIE_File_Path                              ) {

            string Json = File.ReadAllText(The_PTIE_File_Path);

            if( Json == "" )
                return new Type.PTIE();

            Type.PTIE_File The_PTIE_File = JsonConvert.DeserializeObject<Type.PTIE_File>(Json);

            return PTIE_File_To_PTIE( The_PTIE_File );
        }
        public static Type.PTIE         PTIE_File_To_PTIE       ( Type.PTIE_File    The_PTIE_File                                   ) {

            Type.PTIE The_PTIE = new Type.PTIE();

            The_PTIE.Imports = The_PTIE_File.Imports;
            The_PTIE.Sound_Import = The_PTIE_File.Sound_Import;

            The_PTIE.Imports = The_PTIE.Imports.Distinct().ToList();

            for( int i = 0 ; i < The_PTIE.Imports.Count ; i++ ) {

                while( i < The_PTIE.Imports.Count && !File.Exists( The_PTIE.Imports[i] ) ) {

                    The_PTIE.Imports.RemoveAt( i );
                }
            }

            for( int i = 0 ; i < The_PTIE_File.Images.Count ; i++ )
                The_PTIE.Images.Add( new Type.PTIE.Image( Image.FromFile( The_PTIE_File.Imports[The_PTIE_File.Images[i].Import] ), The_PTIE_File.Images[i].Import, The_PTIE_File.Images[i].Time ) );

            The_PTIE.Sound_Reader = new Mp3FileReader( The_PTIE.Imports[(int)The_PTIE.Sound_Import] );
            The_PTIE.Sound_Output = new WaveOutEvent();

            return The_PTIE;
        }
        public static void              PTIE_File_To_PTIE_File  ( string            The_PTIE_File_Path  , string The_PTIA_File_Path ) {
            PTIE_To_PTIA_File( PTIE_File_To_PTIE( The_PTIE_File_Path ), The_PTIA_File_Path );
        }
        public static void              PTIE_File_To_PTIE_File  ( Type.PTIE_File    The_PTIE_File       , string The_PTIA_File_Path ) {
            PTIE_To_PTIA_File( PTIE_File_To_PTIE( The_PTIE_File ), The_PTIA_File_Path );
        }
    }
}