using System.Windows;
using System.Windows.Controls;

namespace Hotel.Utilities.MyCustomControls;

public class MyButton : RadioButton
{
    static MyButton()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(MyButton), new FrameworkPropertyMetadata(typeof(MyButton)));
    }
}