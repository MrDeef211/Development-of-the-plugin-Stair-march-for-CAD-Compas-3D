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
            _parameters.UpdateParametersEvent += ParameterChanged;
        }     

        /// <summary>
        /// Обработчик события появления ошибки
        /// </summary>
        /// <param name="sender">Источник ошибки</param>
        /// <param name="e">Сообщение ошибки</param>
        private void IsErrorAppered(object sender, ErrorArgs e)
        {

        }

        /// <summary>
        /// Обработчик события изменения параметров вне класса
        /// </summary>
        /// <param name="sender">Место изменения</param>
        /// <param name="e">Список изменённых параметров</param>
        private void ParameterChanged(object sender, List<ParametersTypes> e)
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
