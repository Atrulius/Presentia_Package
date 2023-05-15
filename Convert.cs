using Newtonsoft.Json;
using NAudio.Wave;

namespace Presentia.Convert {

    namespace PTIA {

        public static class To {

        }

        public static class From {

        }
    }

    namespace PTIA_File {

        public static class To {

            public static void              PTIE_File   ( string            The_PTIE_File_Path  , string The_PTIA_File_Path ) {

            }
            public static void              PTIE_File   ( Type.PTIE_File    The_PTIE_File       , string The_PTIA_File_Path ) {
                
            }

            public static void              PTIE        ( Type.PTIE         The_PTIE            , string The_PTIA_File_Path ) {

            }
        }

        public static class From {

            public static void              PTIE_File   ( string            The_PTIE_File_Path  , string The_PTIA_File_Path ) {
                Convert.PTIA_File.From.PTIE( Convert.PTIE.From.PTIE_File( The_PTIE_File_Path )  , The_PTIA_File_Path );
            }
            public static void              PTIE_File   ( Type.PTIE_File    The_PTIE_File       , string The_PTIA_File_Path ) {
                Convert.PTIA_File.From.PTIE( Convert.PTIE.From.PTIE_File( The_PTIE_File )       , The_PTIA_File_Path );
            }
            public static void              PTIE        ( Type.PTIE         The_PTIE            , string The_PTIA_File_Path ) {

                Type.PTIA_File The_PTIA_File = new Type.PTIA_File();

                for( int i = 0 ; i < The_PTIE.Images.Count ; i++ )
                    The_PTIA_File.Add_Image( The_PTIE.Images[i].Drawing, The_PTIE.Images[i].Time );

                The_PTIA_File.Set_Sound( File.ReadAllBytes( The_PTIE.Imports[(int)The_PTIE.Sound_Import] ) );

                object obj = new { Sound = The_PTIA_File.sound, Images = The_PTIA_File.images_as_byte, Times = The_PTIA_File.times };

                string json = JsonConvert.SerializeObject(obj);

                File.WriteAllText( The_PTIA_File_Path, json );
            }
        }
    }

    namespace PTIE {

        public static class To {
            
            public static void              PTIE_File   ( Type.PTIE         The_PTIE            , string The_PTIE_File_Path ) {

                string Json = JsonConvert.SerializeObject(Convert.PTIE_File.From.PTIE(The_PTIE));
                File.WriteAllText(The_PTIE_File_Path, Json);
            }
            public static Type.PTIE_File    PTIE_File   ( Type.PTIE         The_PTIE                                        ) {

                Type.PTIE_File The_PTIE_File = new Type.PTIE_File();

                The_PTIE_File.Imports = The_PTIE.Imports;
                The_PTIE_File.Sound_Import = (int)The_PTIE.Sound_Import;

                for (int i = 0; i < The_PTIE.Images.Count; i++)
                    The_PTIE_File.Images.Add(new Type.PTIE_File.Image(The_PTIE.Images[i].Import, The_PTIE.Images[i].Time));

                return The_PTIE_File;
            }
            
            public static void              PTIA_File   ( Type.PTIE         The_PTIE            , string The_PTIA_File_Path ) {

                Type.PTIA_File The_PTIA_File = new Type.PTIA_File();

                for( int i = 0 ; i < The_PTIE.Images.Count ; i++ )
                    The_PTIA_File.Add_Image( The_PTIE.Images[i].Drawing, The_PTIE.Images[i].Time );

                The_PTIA_File.Set_Sound( File.ReadAllBytes( The_PTIE.Imports[(int)The_PTIE.Sound_Import] ) );

                object obj = new { Sound = The_PTIA_File.sound, Images = The_PTIA_File.images_as_byte, Times = The_PTIA_File.times };

                string json = JsonConvert.SerializeObject(obj);

                File.WriteAllText( The_PTIA_File_Path, json );
            }
        }

        public static class From {

            public static Type.PTIE         PTIE_File   ( string            The_PTIE_File_Path                              ) {

                string Json = File.ReadAllText(The_PTIE_File_Path);

                if( Json == "" )
                    return new Type.PTIE();

                Type.PTIE_File The_PTIE_File = JsonConvert.DeserializeObject<Type.PTIE_File>(Json);

                return Convert.PTIE.From.PTIE_File( The_PTIE_File );
            }
            public static Type.PTIE         PTIE_File   ( Type.PTIE_File    The_PTIE_File                                   ) {

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
            public static Type.PTIE         Nothing     (                                                                   ) {

                Type.PTIE The_PTIE = new Type.PTIE();

                The_PTIE.Imports = new List<string>();
                The_PTIE.Images = new List<Type.PTIE.Image>();
                The_PTIE.Sound_Import = null;

                return The_PTIE;
            }
        }
    }

    namespace PTIE_File {

        public static class To {

            public static Type.PTIE         PTIE        ( string            The_PTIE_File_Path                              ) {

                string Json = File.ReadAllText(The_PTIE_File_Path);

                if( Json == "" )
                    return new Type.PTIE();

                Type.PTIE_File The_PTIE_File = JsonConvert.DeserializeObject<Type.PTIE_File>(Json);

                return Convert.PTIE.From.PTIE_File( The_PTIE_File );
            }
            public static Type.PTIE         PTIE        ( Type.PTIE_File    The_PTIE_File                                   ) {

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

            public static void              PTIE_File   ( string            The_PTIE_File_Path  , string The_PTIA_File_Path ) {
                Convert.PTIA_File.From.PTIE( Convert.PTIE.From.PTIE_File( The_PTIE_File_Path )  , The_PTIA_File_Path );
            }
            public static void              PTIE_File   ( Type.PTIE_File    The_PTIE_File       , string The_PTIA_File_Path ) {
                Convert.PTIA_File.From.PTIE( Convert.PTIE.From.PTIE_File( The_PTIE_File )       , The_PTIA_File_Path );
            }
        }

        public static class From {
            
            public static void              PTIE        ( Type.PTIE         The_PTIE            , string The_PTIE_File_Path ) {

                string Json = JsonConvert.SerializeObject(Convert.PTIE_File.From.PTIE(The_PTIE));
                File.WriteAllText(The_PTIE_File_Path, Json);
            }
            public static Type.PTIE_File    PTIE        ( Type.PTIE         The_PTIE                                        ) {

                Type.PTIE_File The_PTIE_File = new Type.PTIE_File();

                The_PTIE_File.Imports = The_PTIE.Imports;
                The_PTIE_File.Sound_Import = (int)The_PTIE.Sound_Import;

                for (int i = 0; i < The_PTIE.Images.Count; i++)
                    The_PTIE_File.Images.Add(new Type.PTIE_File.Image(The_PTIE.Images[i].Import, The_PTIE.Images[i].Time));

                return The_PTIE_File;
            }
        }
    }
}