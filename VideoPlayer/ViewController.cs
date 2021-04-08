using AVFoundation;
using Foundation;
using System;
using UIKit;

namespace VideoPlayer
{
    public partial class ViewController : UIViewController
    {
        //Klasse die die Steuerung und Darstellung der Videokomponente beinhaltet
        AVPlayer avPlayer;
        //Klasse zum Rendern der Ausgabe
        AVPlayerLayer avPlayerLayer;
        //Klasse um Video und Audio timen
        AVAsset avAsset;
        //Klasse die das Wiederagebeelement verarbeitet
        AVPlayerItem avPlayerItem;



        public ViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.

            btnPlay.TouchUpInside += TouchUpInside;
            btnPause.TouchUpInside += TouchUpInside;

        }

        private void TouchUpInside(object sender, EventArgs e)
        {
            if (sender == btnPlay)
            {
                //holen wir uns das Video
                avAsset = AVAsset.FromUrl(NSUrl.FromFilename("video.mp4"));
                //Video an das Playeritem uebergeben
                avPlayerItem = new AVPlayerItem(avAsset);
                //Playeritem an den Player uebergeben
                avPlayer = new AVPlayer(avPlayerItem);
                //avPlayer an playerLayer uebergeben
                avPlayerLayer = AVPlayerLayer.FromPlayer(avPlayer);
                //Flaeche bestimmen in der das Video angezeigt werden soll
                avPlayerLayer.Frame = View.Frame;
                //Layer der View hinzufuegen
                View.Layer.AddSublayer(avPlayerLayer);

                avPlayer.Play();

            }
            else if (sender == btnPause)
            {
                avPlayer.Pause();
            }
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}