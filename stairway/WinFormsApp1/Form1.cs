using stairway;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {

        private Parameters parameters = new Parameters();

        public Form1()
        {
            InitializeComponent();
            parameters.ErrorMessageEvent += IsErrorAppered;
        }     

    private void IsErrorAppered(object sender, ErrorArgs e)
        {

        }

    }


}
