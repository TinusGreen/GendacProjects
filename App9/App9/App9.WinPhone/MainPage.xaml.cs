using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.IO;
using System.IO.IsolatedStorage;
using Microsoft.Xna.Framework.Media;


namespace App9.WinPhone
{
    public partial class MainPage : global::Xamarin.Forms.Platform.WinPhone.FormsApplicationPage
    {
        bool eraseflag = true;
        private Stack<Stroke> _removedStrokes = new Stack<Stroke>();
        BitmapImage biImage = new BitmapImage();
        Stroke NewStroke;

        public MainPage()
        {
            InitializeComponent();
            SetBoundary();
            ucImg.clsbtn.Click += clsbtn_Click;
        }

        private void clsbtn_Click(object sender, RoutedEventArgs e)
        {
            ucImg.Visibility = Visibility.Collapsed;
            TitlePanel.IsHitTestVisible = true;
            ContentPanel.IsHitTestVisible = true;
        }


        //A new stroke object named MyStroke is created. MyStroke is added to the StrokeCollection of the InkPresenter named MyIP
        private void MyIP_MouseLeftButtonDown(object sender, MouseEventArgs e)
        {
            MyIP.CaptureMouse();
            if (eraseflag == true)
            {

                StylusPointCollection MyStylusPointCollection = new StylusPointCollection();
                MyStylusPointCollection.Add(e.StylusDevice.GetStylusPoints(MyIP));
                NewStroke = new Stroke(MyStylusPointCollection);
                NewStroke.DrawingAttributes.Color = Colors.Red;
                MyIP.Strokes.Add(NewStroke); StylusPointCollection ErasePointCollection = new StylusPointCollection();


            }
            else
            {
                StylusPointCollection pointErasePoints = e.StylusDevice.GetStylusPoints(MyIP);
                StrokeCollection hitStrokes = MyIP.Strokes.HitTest(pointErasePoints);
                if (hitStrokes.Count > 0)
                {
                    foreach (Stroke hitStroke in hitStrokes)
                    {
                        MyIP.Strokes.Remove(hitStroke);
                        //undoStack.Push(hitStroke);
                        //undoStateBufferStack.Push(true);
                    }
                }
            }
        }

        //StylusPoint objects are collected from the MouseEventArgs and added to MyStroke. 
        private void MyIP_MouseMove(object sender, MouseEventArgs e)
        {
            if (NewStroke != null)
                NewStroke.StylusPoints.Add(e.StylusDevice.GetStylusPoints(MyIP));
        }

        //MyStroke is completed
        private void MyIP_LostMouseCapture(object sender, MouseEventArgs e)
        {
            NewStroke = null;

        }

        //Set the Clip property of the inkpresenter so that the strokes
        //are contained within the boundary of the inkpresenter
        private void SetBoundary()
        {
            RectangleGeometry MyRectangleGeometry = new RectangleGeometry();
            MyRectangleGeometry.Rect = new Rect(0, 0, MyIP.ActualWidth, MyIP.ActualHeight);
            MyIP.Clip = MyRectangleGeometry;
        }

        private void Clrbtn_Click(object sender, RoutedEventArgs e)
        {
            MyIP.Strokes.Clear();
            savebtn.IsEnabled = false;
        }

        private void Erasebtn_Click(object sender, RoutedEventArgs e)
        {
            if (MyIP.Strokes != null && MyIP.Strokes.Count > 0)
            {
                if (eraseflag == true)
                {
                    eraseflag = false;
                    MessageBox.Show("Note:You are in erase mode,so please again tap on erase btn to draw your signature!");
                }
                else
                {
                    eraseflag = true;
                }
            }
            else
            {
                MessageBox.Show("Note:There is no signature line to errase!");
                eraseflag = true;
            }
        }

        private void btnUndo_Click(object sender, RoutedEventArgs e)
        {
            if (MyIP.Strokes != null && MyIP.Strokes.Count > 0)
            {
                _removedStrokes.Push(MyIP.Strokes.Last());
                MyIP.Strokes.RemoveAt(MyIP.Strokes.Count - 1);
            }
            else
            {
                MessageBox.Show("Note:There is no signature line to undo!");
            }
        }

        private void btnRedo_Click(object sender, RoutedEventArgs e)
        {
            if (_removedStrokes != null && _removedStrokes.Count > 0)
            {
                MyIP.Strokes.Add(_removedStrokes.Pop());
            }
            else
            {
                MessageBox.Show("Note:There is no signature line to redo!");
            }
        }



        private void btncaprture_Click(object sender, RoutedEventArgs e)
        {
            if (MyIP.Strokes != null && MyIP.Strokes.Count > 0)
            {
                WriteableBitmap wbBitmap = new WriteableBitmap(MyIP, new TranslateTransform());
                EditableImage eiImage = new EditableImage(wbBitmap.PixelWidth, wbBitmap.PixelHeight);

                try
                {
                    for (int y = 0; y < wbBitmap.PixelHeight; ++y)
                    {
                        for (int x = 0; x < wbBitmap.PixelWidth; ++x)
                        {
                            int pixel = wbBitmap.Pixels[wbBitmap.PixelWidth * y + x];
                            eiImage.SetPixel(x, y,
                            (byte)((pixel >> 16) & 0xFF),
                            (byte)((pixel >> 8) & 0xFF),
                            (byte)(pixel & 0xFF), (byte)((pixel >> 24) & 0xFF)
                            );
                        }
                    }
                }
                catch (System.Security.SecurityException)
                {
                    throw new Exception("Cannot print images from other domains");
                }

                // Save it to disk
                Stream streamPNG = eiImage.GetStream();
                StreamReader srPNG = new StreamReader(streamPNG);
                byte[] baBinaryData = new Byte[streamPNG.Length];
                long bytesRead = streamPNG.Read(baBinaryData, 0, (int)streamPNG.Length);

                IsolatedStorageFileStream isfStream = new IsolatedStorageFileStream("tempsignature.png", FileMode.Create, IsolatedStorageFile.GetUserStoreForApplication());
                isfStream.Write(baBinaryData, 0, baBinaryData.Length);
                isfStream.Close();

                //Show to image
                isfStream = new IsolatedStorageFileStream("tempsignature.png", FileMode.Open, IsolatedStorageFile.GetUserStoreForApplication());

                biImage.SetSource(isfStream);
                isfStream.Close();
                ucImg.Visibility = Visibility.Visible;
                ucImg.SignCapture.Source = biImage;
                savebtn.IsEnabled = true;
                TitlePanel.IsHitTestVisible = false;
                ContentPanel.IsHitTestVisible = false;
            }
            else
            {
                MessageBox.Show("Note:There is no signature line to capture!");
            }
        }
        
        private void savebtncaprture_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Are you sure save signature to media library?", "", MessageBoxButton.OKCancel);
            if (result == MessageBoxResult.OK)
            {
                WriteableBitmap wb = new WriteableBitmap(biImage);
                using (var stream = new MemoryStream())
                {
                    // Save the picture to the Windows Phone media library.
                    wb.SaveJpeg(stream, wb.PixelWidth, wb.PixelHeight, 0, 100);
                    stream.Seek(0, SeekOrigin.Begin);
                    new MediaLibrary().SavePicture("tempsignature.png", stream);
                }
            }
        }

    }
}
