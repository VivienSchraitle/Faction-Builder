/*
using Avalonia.Controls;
using Avalonia.Interactivity;
using System;

namespace WindowManager{
    public partial class MainWindow : Window
    {
        private FactionBuilder factionBuilder = new FactionBuilder();

        public MainWindow()
        {
            InitializeComponent();
            //ScaleInput.IsEnabled = true;
            //ScaleInput.IsVisible = true;
            //ScaleInput.Text = "hello";
        }

        private void GenerateFactionClick(object sender, RoutedEventArgs e)
        {
            try
            {
                string[] inputParts = new string[]
                {
                    ScaleInput.Text,
                    FundsInput.Text,
                    MagicInput.Text,
                    MilitaryInput.Text,
                    ReputationInput.Text,
                    IntensityInput.Text
                };

                factionBuilder.GenerateFaction(inputParts);
                FactionDetails.Text = factionBuilder.GetFactionDetails();
            }
            catch (Exception ex)
            {
                FactionDetails.Text = "Error: " + ex.Message;
            }
        }

        private void SaveFactionClick(object sender, RoutedEventArgs e)
        {
            try
            {
                factionBuilder.SaveFaction();
                FactionDetails.Text += "\nFaction saved successfully.";
            }
            catch (Exception ex)
            {
                FactionDetails.Text = "Error: " + ex.Message;
            }
        }
        
    }
}
*/