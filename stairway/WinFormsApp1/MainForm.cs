using stairway;

namespace WinFormsApp1
{
    public partial class MainForm : Form
    {
        /// <summary>
        /// Строитель модели
        /// </summary>
        private Builder _builder;
        /// <summary>
        /// Обработчик параметров модели
        /// </summary>
        private Parameters _parameters;

        public MainForm()
        {
            InitializeComponent();

            _builder = new Builder();
            _parameters = new Parameters();

            _parameters.ErrorMessageEvent += IsErrorAppered;
        }     

        /// <summary>
        /// Обработчик события появления ошибки
        /// </summary>
        /// <param name="sender">Источник ошибки</param>
        /// <param name="e">Сообщение ошибки</param>
        private void IsErrorAppered(object sender, ErrorArgs e)
        {

        }

        private void ParameterEntered(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Начать построение модели
        /// </summary>
        private void BuildModel()
        {

        }

    }


}
