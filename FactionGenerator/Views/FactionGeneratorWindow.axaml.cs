using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;


namespace FactionGenerator.Views;

public partial class FactionGeneratorWindow : Window
{
    private FactionBuilder factionBuilder = new FactionBuilder();
    private Random rnd = new();
    public FactionGeneratorWindow()
    {
        InitializeComponent();
    }
            private void GenerateFactionClick(object sender, RoutedEventArgs e)
        {
            try
            {


                string[] inputParts = new string[]
                {
                    #pragma warning disable
                    ScaleInput.Text,
                    FundsInput.Text,
                    MagicInput.Text,
                    MilitaryInput.Text,
                    ReputationInput.Text,
                    IntensityInput.Text
                    #pragma warning enable
                };

                factionBuilder.GenerateFaction(inputParts);
                FactionDetails.Text = factionBuilder.GetFactionDetails();
            }
            catch (Exception ex)
            {
                FactionDetails.Text = "Error: " + ex.Message;
            }
        }
        private void RandomScale(object sender, RoutedEventArgs e)
        {
            ScaleInput.Text = rnd.Next(0,101).ToString();
        }
        private void RandomFunds(object sender, RoutedEventArgs e)
        {
           FundsInput.Text = rnd.Next(0,101).ToString();
        }
        private void RandomMagic(object sender, RoutedEventArgs e)
        {
             MagicInput.Text = rnd.Next(0,101).ToString();
        }
        private void RandomMilitary(object sender, RoutedEventArgs e)
        {
             MilitaryInput.Text = rnd.Next(0,101).ToString();
        }
        private void RandomReputation(object sender, RoutedEventArgs e)
        {
             ReputationInput.Text = rnd.Next(0,101).ToString();
        }
        private void RandomIntensity(object sender, RoutedEventArgs e)
        {
             IntensityInput.Text = rnd.Next(0,6).ToString();
        }
        private void GenerateAllValues(object sender, RoutedEventArgs e)
        {
            ScaleInput.Text = rnd.Next(0,101).ToString();
            FundsInput.Text = rnd.Next(0,101).ToString();
            MagicInput.Text = rnd.Next(0,101).ToString();
            MilitaryInput.Text = rnd.Next(0,101).ToString();
            ReputationInput.Text = rnd.Next(0,101).ToString();
            IntensityInput.Text = rnd.Next(0,6).ToString();
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
        private void StartMenuClick(object sender, RoutedEventArgs e)
        {
            var mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close(); // Close the menu window
        }
        
}