using Contacts;
using ContactsUI;
using Foundation;
using MessageUI;
using System;
using UIKit;

namespace Kommunikation
{
    public partial class ViewController : UIViewController, ICNContactViewControllerDelegate
    {

        private MFMessageComposeViewController nachrichtenController;
        private MFMailComposeViewController mailController;
        private readonly CNContactStore store = new CNContactStore();

        public ViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            checkContactAccess();

            btnNewContact.TouchUpInside += BtnNewContact_TouchUpInside;
            btnNewContactWithData.TouchUpInside += BtnNewContactWithData_TouchUpInside;
            btnGivenContact.TouchUpInside += BtnGivenContact_TouchUpInside;

            //Senden einer Email -> Asynchron, da wir nicht wissen wie lange es dauert
            //die Email zu versenden
            //asynchron ist notwendig, damit unsere App die ganze Zeit bedienbar bleibt
            btnSendEmail.TouchUpInside += async (object sender, EventArgs args) =>
            {
                if (MFMailComposeViewController.CanSendMail)
                {
                    mailController = new MFMailComposeViewController();
                    //Empfaenger festlegen
                    mailController.SetToRecipients(new string[] { "info@example.com" });
                    //Titel festlegen
                    mailController.SetSubject("Email aus dem Alfatraining-Kurs");
                    //Nachrichtentext und Format (TXT/HTML) festlegen
                    mailController.SetMessageBody("Hier steht der Nachrichtentext", false);
                    //Verarbeitung wenn Emailversand "abgeschlossen" (egal ob erfolgreich oder nicht)
                    mailController.Finished += mailController_Finished;
                    await PresentViewControllerAsync(mailController, true);
                }
            };

            btnSendSMS.TouchUpInside += async (sender, args) =>
            {
                if (MFMessageComposeViewController.CanSendText)
                {
                    nachrichtenController = new MFMessageComposeViewController();
                    nachrichtenController.Recipients = new string[] { "1234567890" };
                    nachrichtenController.Body = "Text aus dem Alfatraining-Kurs";
                    nachrichtenController.Finished += nachrichtenController_Finished;
                    await PresentViewControllerAsync(nachrichtenController, true);
                }
            };
        }

        private void BtnGivenContact_TouchUpInside(object sender, EventArgs e)
        {
            //Filterwort/Suchbegriff festlegen
            var predicate = CNContact.GetPredicateForContacts("Appleseed");
            //nur bestimmte Felder zurueck
            var fetchKeys = new NSString[] { CNContactKey.GivenName, CNContactKey.FamilyName };
            //Instanzieren ein Fehlerobjekt
            NSError error;
            //Hole Kontakt(e) aus Adressbuch
            var contacts = store.GetUnifiedContacts(predicate, fetchKeys, out error);
            //Ausgabe des ersten gefundenen Kontakts (Vor und Nacname) im Label Output
            lblOutput.Text = contacts[0].GivenName + " " + contacts[0].FamilyName;
        }

        private void BtnNewContactWithData_TouchUpInside(object sender, EventArgs e)
        {
            var contact = new CNMutableContact
            {
                FamilyName = Name.familienname,
                GivenName = Name.vorname

            };

            contact.PhoneNumbers = new CNLabeledValue<CNPhoneNumber>[]
            {
                new CNLabeledValue<CNPhoneNumber>("iPhone", new CNPhoneNumber(PhoneNumber.iPhone)),
                new CNLabeledValue<CNPhoneNumber>("mobil", new CNPhoneNumber(PhoneNumber.mobile))
            };

            var adresse = new CNMutablePostalAddress
            {
                Street = Adresse.strasse,
                City = Adresse.stadt,
                PostalCode = Adresse.plz
            };

            contact.PostalAddresses = new CNLabeledValue<CNPostalAddress>[]
            {
                new CNLabeledValue<CNPostalAddress>(CNLabelKey.Home, adresse)
            };

            var contactViewController = CNContactViewController.FromNewContact(contact);
            contactViewController.Delegate = this;
            NavigationController.PushViewController(contactViewController, true);
        }

        private void BtnNewContact_TouchUpInside(object sender, EventArgs e)
        {
            //Erstellen einen neuen ContactViewController ohne Übergabe eines Contacts
            var contactViewController = CNContactViewController.FromNewContact(null);
            contactViewController.Delegate = this;
            
            NavigationController.PushViewController(contactViewController, true);

        }

        private async void nachrichtenController_Finished(object sender, MFMessageComposeResultEventArgs e)
        {
            switch (e.Result)
            {
                case MessageComposeResult.Sent:
                    Console.WriteLine("Nachricht wurde gesendet");
                    break;
                case MessageComposeResult.Failed:
                    Console.WriteLine("Nachricht versenden fehlgeschlagen");
                    break;
                case MessageComposeResult.Cancelled:
                    Console.WriteLine("Nachrichtversand abgebrochen");
                    break;
            }
            e.Controller.Finished -= nachrichtenController_Finished;
            await e.Controller.DismissViewControllerAsync(true);
        }

        private async void mailController_Finished(object sender, MFComposeResultEventArgs e)
        {
            switch (e.Result)
            {
                case MFMailComposeResult.Sent:
                    Console.WriteLine("Nachricht wurde gesendet");
                    break;
                case MFMailComposeResult.Failed:
                    Console.WriteLine("Nachricht senden fehlgeschlagen");
                    break;
                case MFMailComposeResult.Cancelled:
                    Console.WriteLine("Nachrichtversand wurde abgebrochen");
                    break;
            }
            e.Controller.Finished -= mailController_Finished;
            await e.Controller.DismissViewControllerAsync(true);
        }

        private void checkContactAccess()
        {
            var status = CNContactStore.GetAuthorizationStatus(CNEntityType.Contacts);
            switch (status)
            {
                case CNAuthorizationStatus.Authorized:
                    break;
                case CNAuthorizationStatus.NotDetermined:
                    store.RequestAccess(CNEntityType.Contacts, (bool granted, NSError error) =>
                    {
                        if (granted)
                            Console.WriteLine("Zugriff erlaubt");
                    });
                    break;
                case CNAuthorizationStatus.Denied:
                case CNAuthorizationStatus.Restricted:
                    Console.WriteLine("Zugriff verweigert");
                    break;
            }
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}