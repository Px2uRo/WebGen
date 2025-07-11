using WebGen.Converters;
using WebGen.PlatformConverters;
using WebGen.Utils.QuickIO;

namespace WebGen
{
    internal class Program
    {
        static void Main(string[] args)
        {

            var xaml = @"
<Grid RowDefinitions=""300px, 2*"" ColumnDefinitions=""1*, 3*"">
    <Button Grid.Row=""0"" Click=""ShowMessage"" Grid.Column=""1"" Content=""Click Me"" />
    <Button Grid.Row=""0"" Click=""ShowMessage"" Grid.Column=""1"" Content=""Click Me"" />
    <TextBlock Grid.Row=""1"" Grid.Column=""0"" Text=""Hello World!"" />
</Grid>
";


            var cs = @"
class any
{
    public void ShowMessage()
    {
        MessageBox.Show(""Hello, world!"");
    }
}
";

            var converter = new AppConverter();
            var html = converter.Convert(xaml, cs);

            System.Console.WriteLine(html);
            var tar = @"I:\Xiong's\MyStudio\WorkShops\WebGen\p1\index.html";
            Console.WriteLine(tar);
            HTMLUtil.TrySave(tar, html);
        }
    }
}
