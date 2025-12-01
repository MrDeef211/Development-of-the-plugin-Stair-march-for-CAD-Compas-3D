using System.Reflection.Metadata;
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
        /// <summary>
        /// Привязанные параметры для TextBox
        /// </summary>
        private Dictionary<ParametersTypes, TextBox> _textboxByParameter;
        /// <summary>
        /// Список активных ошибок
        /// </summary>
        private Dictionary<ParametersTypes, List<string>> _activeErrors;

        public MainForm()
        {
            InitializeComponent();
            InitializeUIBindings();

            _builder = new Builder();
            _parameters = new Parameters();
            _activeErrors = new Dictionary<ParametersTypes, List<string>>();

            _parameters.ErrorMessageEvent += IsErrorAppered;
            _parameters.UpdateParametersEvent += ParameterChanged;

            _parameters.FullUpdateParameters();
        }     

        /// <summary>
        /// Обработчик события появления ошибки
        /// </summary>
        /// <param name="sender">Источник ошибки</param>
        /// <param name="e">Сообщение ошибки со списком переменных с ошибкой</param>
        private void IsErrorAppered(object sender, ErrorArgs e)
        {
            foreach (var parameter in e.ParametersList)
            {
                if (!_activeErrors.ContainsKey(parameter))
                    _activeErrors[parameter] = new List<string>();

                if (!_activeErrors[parameter].Contains(e.Message))
                    _activeErrors[parameter].Add(e.Message);

                MarkTextboxAsError(_textboxByParameter[parameter]);
            }

            UpdateErrorBox();
        }

        /// <summary>
        /// Обработчик события обновления параметра
        /// </summary>
        /// <param name="sender">Место изменения</param>
        /// <param name="e">Список изменённых параметров</param>
        private void ParameterChanged(object sender, List<ParametersTypes> e)
        {
            foreach (var parameter in e)
            {
                var textBox = _textboxByParameter[parameter];
                textBox.Text = _parameters.GetParameter(parameter).ToString();

                if (_activeErrors.ContainsKey(parameter))
                {
                    _activeErrors.Remove(parameter);
                    ClearTextboxError(textBox);
                }
            }

            UpdateErrorBox();
        }


        private void ParameterEntered(object sender, EventArgs e)
        {
            var textBox = (TextBox)sender;

            var parameter = _textboxByParameter.First(x => x.Value == textBox).Key;

            if (!_textboxByParameter.ContainsValue(textBox))
            {
                IsErrorAppered(this, new ErrorArgs("Параметр не должен быть пустым", 
                    new List<ParametersTypes> { parameter }));
                return;
            }

            if (double.TryParse(textBox.Text, out double value))
            {
                _parameters.SetParameter(parameter, value);
            }
            else
            {
                IsErrorAppered(this, new ErrorArgs("Некорректный формат числа",
                    new List<ParametersTypes> { parameter }));
            }
        }



        /// <summary>
        /// Начать построение модели
        /// </summary>
        private void BuildModel()
        {

        }

        /// <summary>
        /// Инициализирует привязку TextBox и соответствующие им ParametersTypes
        /// </summary>
        private void InitializeUIBindings()
        {
            _textboxByParameter = new Dictionary<ParametersTypes, TextBox>
            {

            { ParametersTypes.Height,  HeightTextBox},
            { ParametersTypes.Length,  LengthTextBox},
            { ParametersTypes.PlatformLengthUp,  PlatformLengthUpTextBox},
            { ParametersTypes.PlatformLengthDown,  PlatformLengthDownTextBox},
            { ParametersTypes.PlatformHeight,  PlatformHeightTextBox},
            { ParametersTypes.StepAmount,  StepAmountTextBox},
            { ParametersTypes.StepHeight,  StepHeightTextBox},
            { ParametersTypes.StepProjectionHeight,  StepProtjectionHeightTextBox},
            { ParametersTypes.StepProjectionLength,  StepProjectionLengthTextBox},
            { ParametersTypes.Width,  WidthTextBox}

            };
        }

        /// <summary>
        /// Подсветить textBox
        /// </summary>
        /// <param name="textBox">Целевой textBox</param>
        private void MarkTextboxAsError(TextBox textBox)
        {
            textBox.BackColor = Color.LightCoral;
        }

        /// <summary>
        /// Убрать подсветку для textBox
        /// </summary>
        /// <param name="textBox">Целевой textBox</param>
        private void ClearTextboxError(TextBox textBox)
        {
            textBox.BackColor = SystemColors.Window;
        }

        /// <summary>
        /// Обновить список ошибок
        /// </summary>
        private void UpdateErrorBox()
        {
            if (_activeErrors.Count == 0)
            {
                ErrorTextBox.Text = "";
                return;
            }

            // Берём ВСЕ строки ошибок из всех параметров
            var Errors = _activeErrors
                .SelectMany(kv => kv.Value) 
                .Distinct()                 
                .ToList();

            ErrorTextBox.Text = string.Join(Environment.NewLine, Errors);
        }

    }


}
