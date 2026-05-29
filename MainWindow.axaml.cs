using Avalonia.Controls;
using AstroHabitsDesktop.Presentation.ViewModels;

namespace AstroHabitsDesktop;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        DataContext = new MainViewModel();
    }
}