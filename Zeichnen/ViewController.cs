using CoreGraphics;
using Foundation;
using System;
using UIKit;

namespace Zeichnen
{
    public partial class ViewController : UIViewController
    {
        public ViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.

            //Definieren einer UIView
            UIView contentView = new UIView()
            {
                //Definieren der Hintergrundfarbe (hier Grau)
                BackgroundColor = UIColor.FromRGB(123, 123, 123)
            };
            //zuweisen zur View auf dem Story
            View = contentView;

            //definieren wir einen Bereich der Views
            CGRect rechteck = UIScreen.MainScreen.Bounds;
            rechteck.Y += 50;
            rechteck.Height -= 100; //rechteck.Height = rechteck.Height - 100;

            //Vertikale StackView
            UIStackView verticalStackView = new UIStackView(rechteck)
            {
                Axis = UILayoutConstraintAxis.Vertical
            };

            UIStackView horizontalStackView = new UIStackView(rechteck)
            {
                Axis = UILayoutConstraintAxis.Horizontal,
                Alignment = UIStackViewAlignment.Center,
                Distribution = UIStackViewDistribution.EqualSpacing
            };

            CanvasView canvasView = new CanvasView();

            contentView.Add(verticalStackView);
            verticalStackView.AddArrangedSubview(horizontalStackView);
            verticalStackView.AddArrangedSubview(canvasView);

            //PickerDataModel fuer die Farbe
            PickerDataModel<UIColor> colorModel = new PickerDataModel<UIColor>
            {
                Items =
                {
                    new NamedValue<UIColor>("Rot", UIColor.Red),
                    new NamedValue<UIColor>("Gruen", UIColor.Green),
                    new NamedValue<UIColor>("Lila", UIColor.Purple),
                    new NamedValue<UIColor>("Blau", UIColor.Blue)
                }
            };

            //PickerDataModel fuer die Lineinstaerke
            PickerDataModel<float> thicknessModel = new PickerDataModel<float>
            {
                Items =
                {
                    new NamedValue<float>("Duenn", 2),
                    new NamedValue<float>("Medium", 10),
                    new NamedValue<float>("Normal", 20),
                    new NamedValue<float>("Dick", 40)
                }
            };

            //PivkerView zur Anzeige des PickerDataModels
            UIPickerView colorPicker = new UIPickerView { Model = colorModel };
            UIPickerView thicknessPicker = new UIPickerView { Model = thicknessModel };

            //Platzhalter um die Elemente in der horizontalen StackView einzuruecken
            UILabel placeholder1 = new UILabel(new CGRect(0, 0, 10, 10));
            UILabel placeholder2 = new UILabel(new CGRect(0, 0, 10, 10));

            //Instanzieren einer Toolbar
            UIToolbar toolbar = new UIToolbar(new CGRect(0, 0, UIScreen.MainScreen.Bounds.Width, 45))
            {
                BarStyle = UIBarStyle.BlackTranslucent,
                Translucent = true
            };

            //Textfelder instanzieren die per Klick den Picker in der Toolbar oeffnen
            UITextField txtColor = new UITextField
            {
                Text = colorModel.Items[0].Name,
                InputView = colorPicker,
                InputAccessoryView = toolbar
            };

            UITextField txtThickness = new UITextField
            {
                Text = thicknessModel.Items[0].Name,
                InputView = thicknessPicker,
                InputAccessoryView = toolbar
            };

            UIButton btnClear = new UIButton(UIButtonType.RoundedRect);
            btnClear.SetTitle("Clear", UIControlState.Normal);
            btnClear.SetTitleColor(UIColor.Black, UIControlState.Normal);
            btnClear.TouchUpInside += (sender, args) =>
            {
                canvasView.clearView();
            };

            horizontalStackView.AddArrangedSubview(placeholder1);
            horizontalStackView.AddArrangedSubview(txtColor);
            horizontalStackView.AddArrangedSubview(txtThickness);
            horizontalStackView.AddArrangedSubview(btnClear);
            horizontalStackView.AddArrangedSubview(placeholder2);

            thicknessModel.valueChanged += (sender, args) =>
            {
                txtThickness.Text = thicknessModel.SelectedItem.Name;
                canvasView.Linienstaerke = thicknessModel.SelectedItem.Value;
                txtThickness.ResignFirstResponder();
            };

            colorModel.valueChanged += (sender, args) =>
            {
                txtColor.Text = colorModel.SelectedItem.Name;
                canvasView.Linienfarbe = colorModel.SelectedItem.Value.CGColor;
            };

            //UIBarButtonSystemItem.FlexibleSpace nutzt komplette freie Flache
            //nachfolgende Button werden so weit wie moeglich nach rechts geschoben
            UIBarButtonItem barBtnSpacer = new UIBarButtonItem(UIBarButtonSystemItem.FlexibleSpace);
            UIBarButtonItem barBtnDone = new UIBarButtonItem(UIBarButtonSystemItem.Done, (sender, args) =>
            {
                txtColor.ResignFirstResponder();
                txtThickness.ResignFirstResponder();
            });

            toolbar.SetItems(new[] { barBtnSpacer, barBtnDone }, true);


        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}