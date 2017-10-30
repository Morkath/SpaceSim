using System;
using System.Collections.Generic;
using System.Windows.Documents;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace SpaceSim
{
	class Utility
	{
		private static Random rnd = new Random();

		// Image Stuff
		public static void setImage(Image targetImage, string imagePath)
		{
			if (imagePath != null)
			{
				BitmapImage bi = new BitmapImage();
				bi.BeginInit();
				bi.UriSource = new Uri(imagePath, UriKind.Relative);
				bi.EndInit();
				targetImage.Source = bi;
			}
		}
		//private void OnPropertyChanged<T>([CallerMemberName]string caller = null) 
		//{
		//	 // make sure only to call this if the value actually changes
		//	 var handler = PropertyChanged;
		//	 if (handler != null) { handler(this, new PropertyChangedEventArgs(caller));}

		//	 //EqualityComparer<T>.Default.Equals;
		//}

		// Image Stuff
		public static void setImage(Image targetImage, Image sourceImage)
		{
			targetImage = sourceImage;
		}
		public static void mirrorImage(Image targetImage, bool flipHorizontal = true)
		{
			targetImage.RenderTransformOrigin = new Point(0.5, 0.5);
			ScaleTransform flipTrans = new ScaleTransform();

			if (flipHorizontal)
			{
				flipTrans.ScaleX = -1; // Horizontal
			}
			else
			{
				flipTrans.ScaleY = -1; // Vertical
			}

			targetImage.RenderTransform = flipTrans;
		}
		public static void resetImage(Image targetImage)
		{
			targetImage.RenderTransformOrigin = new Point(0.5, 0.5);
			ScaleTransform flipTrans = new ScaleTransform();

			flipTrans.ScaleX = 1; // Horizontal
			flipTrans.ScaleY = 1; // Vertical

			targetImage.RenderTransform = flipTrans;
		}

		// Progress Bar Stuff
		public static void setProgressBarInitial(ProgressBar targetProgressBar, int value)
		{
			setProgressBarMax(targetProgressBar, value);
			setProgressBarValue(targetProgressBar, value);
		}
		public static void setProgressBarInitial(ProgressBar targetProgressBar, int maxValue, int currentValue)
		{
			setProgressBarMax(targetProgressBar, maxValue);
			setProgressBarValue(targetProgressBar, currentValue);
		}
		public static void setProgressBarValue(ProgressBar targetProgressBar, int currentValue)
		{
			targetProgressBar.Value = currentValue;
		}
		public static void setProgressBarMax(ProgressBar targetProgressBar, int currentValue)
		{
			targetProgressBar.Maximum = currentValue;
		}
		public static void toggleProgressBarVisibility(ProgressBar targetProgressBar, bool toggleOff = false)
		{
			if (toggleOff == false)
			{
				if (targetProgressBar.Visibility == Visibility.Hidden)
				{
					targetProgressBar.Visibility = Visibility.Visible;
				}

				else if (targetProgressBar.Visibility == Visibility.Visible)
				{
					targetProgressBar.Visibility = Visibility.Hidden;
				}
			}

			else
			{
				targetProgressBar.Visibility = Visibility.Hidden;
			}
		}

		// Misc
		public static void setMenuVisiblity(Menu tempMenu, bool toggleOff = false)
		{
			if (toggleOff == false)
			{
				if (tempMenu.Visibility == Visibility.Hidden)
				{
					tempMenu.Visibility = Visibility.Visible;
				}

				else if (tempMenu.Visibility == Visibility.Visible)
				{
					tempMenu.Visibility = Visibility.Hidden;
				}
			}

			else
			{
				tempMenu.Visibility = Visibility.Hidden;
			}
		}
		public static int GetRandom()
		{
			return rnd.Next();
		}
		public static int GetRandom(int max)
		{
			return rnd.Next(max);
		}
		public static int GetRandom(int min, int max)
		{
			return rnd.Next(min, max);
		}

		// Log Stuff
		public static void WriteToLog(string logLine)
		{
			var paragraph = MainWindow.thisBattleArmory.infoLogFD.Blocks.LastBlock as Paragraph;
			if (paragraph != null)
			{
				paragraph.Inlines.Add(new Run(logLine));
				paragraph.Inlines.Add(new Run(System.Environment.NewLine));
			}
			//MainWindow.thisBattleArmory.infoLogFD.Blocks.Add(paragraph);
		}
		public static void AddPageToLog()
		{
			var table1 = new Table();

			Section section = new Section();
			section.BreakPageBefore = true;
			section.Blocks.Add(table1);
			MainWindow.thisBattleArmory.infoLogFD.Blocks.Add(section);

			AddBlockToLog();
		}
		public static void AddBlockToLog()
		{
			var paragraph = new Paragraph();
			MainWindow.thisBattleArmory.infoLogFD.Blocks.Add(paragraph);
		}
		public static void AddLineToLog()
		{
			MainWindow.thisBattleArmory.infoLogFD.Blocks.Add(new BlockUIContainer(new Separator()));
		}
		public static void AddNewLineToLog()
		{
			var paragraph = MainWindow.thisBattleArmory.infoLogFD.Blocks.LastBlock as Paragraph;
			if (paragraph != null)
			{
				paragraph.Inlines.Add(new Run(System.Environment.NewLine));
			}
		}
	}
}
