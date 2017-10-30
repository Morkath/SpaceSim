using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SpaceSim
{
	public partial class Comms : Window
	{
		private static string commResponseBack = "/data\\ui\\comms\\commResponse.png";
		private static string commStatic = "/data\\ui\\comms\\commStatic.png";
		private static string commActive = "/data\\ui\\comms\\commActive.png";
		private static string commResponseButton = "/data\\ui\\comms\\commResponseButton.png";

		public Comms()
		{
			try
			{
				InitializeComponent();
			}
			catch (Exception ex)
			{
				//TRAP TODO Resize this, so its most of the screen.  Make it the only window the user can interact with at the time.
			}
			this.Loaded += new RoutedEventHandler(Window_Loaded);
		}
		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			try
			{
				commsInitilizations();

				var desktopWorkingArea = System.Windows.SystemParameters.WorkArea;
				//this.Left = MainWindow.battleStatusLocation.X + MainWindow.thisBattleStatus.Width;
				//this.Top = MainWindow.battleCrewLocation.Y + MainWindow.thisBattleCrew.Height;
				this.Activate();
				this.Topmost = true;
				this.Topmost = false;
				this.Focus();
			}
			catch (Exception ex)
			{

			}
		}
		private void commsInitilizations()
		{
			Utility.setImage(commsImage, commResponseBack);
			Utility.setImage(commsImageBack, commActive);

			Utility.setImage(commsMessage, commResponseBack);
			Utility.setImage(commsMessageBack, commStatic);

			Utility.setImage(commResponse0Image, commResponseButton);
			Utility.setImage(commResponse1Image, commResponseButton);
			Utility.setImage(commResponse2Image, commResponseButton);

			setResponseMessage(commResponse0TB, "<Threaten> I'm going to enjoy spacing you into the sun for what you've done!");
			setResponseMessage(commResponse1TB, "<Mock> How old are your weapons?  Pre-Argarian war?");
			setResponseMessage(commResponse2TB, "<Capitulate> We'll drop cargo!  Just let us go!");
		}

		private void setResponseMessage(TextBlock tempTextBlock, string message)
		{
			tempTextBlock.Text = message;
		}
	}
}
