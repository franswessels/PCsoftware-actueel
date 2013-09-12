
namespace BulkLoop
{
    public partial class BM
    {
        enum comcod
        {
            TEST        =0x11,
            ATTENTIE    =0xA0,
            PARAMS      =0xB0,
            IMAGER      =0xC0
        }

        enum eject
        {
            NOORD   =0,
            OOST    =1,
            ZUID    =2,
            WEST    =3
        }

     enum videoformat
        {
        VIDEOGROOT =576,  // rijlengte videobeeld nieuwe imager
        VIDEOKLEIN =384,  //  idem kolomlengte
        NZSIZE     =384,  // hoogte afbeelding komplete kaart
        OWSIZE     =256,  //  idem breedte
        ICB        =128,  // breedte kaarticon
        ICH        =128  // hoogte kaarticon
        }
    }
}