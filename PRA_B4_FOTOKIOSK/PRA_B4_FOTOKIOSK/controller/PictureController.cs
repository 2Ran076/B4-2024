using PRA_B4_FOTOKIOSK.magie;
using PRA_B4_FOTOKIOSK.models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRA_B4_FOTOKIOSK.controller
{
    public class PictureController
    {
        // De window die we laten zien op het scherm
        public static Home Window { get; set; }


        // De lijst met fotos die we laten zien
        public List<KioskPhoto> PicturesToDisplay = new List<KioskPhoto>();


        // Start methode die wordt aangeroepen wanneer de foto pagina opent.
        public void Start()
        {
            var now = DateTime.Now;
            int currentDay = (int)now.DayOfWeek;

            foreach (string dir in Directory.GetDirectories(@"../../../fotos"))
            {
                if (int.TryParse(Path.GetFileNameWithoutExtension(dir.Split('_').FirstOrDefault()), out int dayNumber))
                {
                    if (dayNumber == currentDay)
                    {
                        foreach (string file in Directory.GetFiles(dir, "*.jpg"))
                        {
                            PicturesToDisplay.Add(new KioskPhoto() { Id = 8824, Source = file });
                        }
                    }
                }
            }

            foreach (var photo in PicturesToDisplay)
            {
                Console.WriteLine(photo.Source);
            }

            PictureManager.UpdatePictures(PicturesToDisplay);
        }




        // Wordt uitgevoerd wanneer er op de Refresh knop is geklikt
        public void RefreshButtonClick()
        {

        }

    }
}
