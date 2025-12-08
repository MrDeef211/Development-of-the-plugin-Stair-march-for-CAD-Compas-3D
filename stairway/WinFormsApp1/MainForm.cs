using System.Collections.Generic;
using System.Linq;
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
        private Dictionary<string, List<ParametersTypes>> _activeErrors;
        /// <summary>
        /// Текстовый эквивалент ParametersTypes на русском
        /// </summary>
        private Dictionary<ParametersTypes, string> _localization;

        public MainForm()
        {
            InitializeComponent();
            InitializeUIBindings();
            InitializeLocalization();

            _builder = new Builder();
            _parameters = new Parameters();
            _activeErrors = new Dictionary<string, List<ParametersTypes>>();

            _parameters.ErrorMessageEvent += IsErrorAppered;
            _parameters.UpdateParametersEvent += ParameterUpdated;

            _parameters.FullUpdateParameters();
        }

        /// <summary>
        /// Обработчик события появления ошибки
        /// </summary>
        /// <param name="sender">Источник ошибки</param>
        /// <param name="e">Сообщение ошибки со списком переменных с ошибкой</param>
        private void IsErrorAppered(object sender, ErrorArgs e)
        {
            string newError;
            if (e.ParametersList.Count == 1)
                newError = _localization[e.ParametersList[0]] + e.Message;
            else
                newError = e.Message;
            if (!_activeErrors.ContainsKey(newError))
                _activeErrors[newError] = new List<ParametersTypes>(e.ParametersList);

            //if (!_activeErrors[e.Message].Contains(e.ParametersList))
            //    _activeErrors[parameter].Add(e.Message);

            foreach (var parameter in e.ParametersList)
            {
                MarkTextboxAsError(_textboxByParameter[parameter]);
            }



            UpdateErrorBox();
        }

        /// <summary>
        /// Обработчик события обновления параметра
        /// </summary>
        /// <param name="sender">Место изменения</param>
        /// <param name="e">Список изменённых параметров</param>
        private void ParameterUpdated(object sender, List<ParametersTypes> e)
        {
            foreach (var parameter in e)
            {
                var textBox = _textboxByParameter[parameter];
                textBox.Text = _parameters.GetParameter(parameter).ToString();

                foreach (var messege in _activeErrors.Keys)
                {
                    foreach (var thisParameter in _activeErrors[messege])
                    {

                        if (thisParameter == parameter)
                        {
                            foreach (var deletingParameter in _activeErrors[messege])
                            {
                                ClearTextboxError(_textboxByParameter[deletingParameter]);
                            }

                            _activeErrors.Remove(messege);


                        }

                    }

                }


            }

            UpdateErrorBox();
        }


        private void ParameterEntered(object sender, KeyEventArgs e)
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
            if (_activeErrors.Count != 0)
            {
                MessageBox.Show("Сначала исправьте все ошибки");
                return;
            }
            var inputParameters = _parameters.GetParameters();
            var outputParameters = new Dictionary<ParametersTypes, double>();

            foreach (var parameter in inputParameters.Keys)
                outputParameters.Add(parameter, inputParameters[parameter].Value);

            _builder.Build(outputParameters);
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
        /// Инициализирует заполнение _localization
        /// </summary>
        private void InitializeLocalization()
        {
            _localization = new Dictionary<ParametersTypes, string>
            {
                { ParametersTypes.Height, "Высота марша" },
                { ParametersTypes.Length, "Длина пролёта" },
                { ParametersTypes.PlatformLengthUp, "Число ступеней" },
                { ParametersTypes.PlatformLengthDown, "Высота ступени" },
                { ParametersTypes.PlatformHeight, "Ширина марша" },
                { ParametersTypes.StepAmount, "Глубина выступа" },
                { ParametersTypes.StepHeight, "Толщина выступа" },
                { ParametersTypes.StepProjectionHeight, "Длина нижней платформы" },
                { ParametersTypes.StepProjectionLength, "Длина верхней платформы" },
                { ParametersTypes.Width, "Толщина марша" }
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
            var Errors = _activeErrors.Keys;

            ErrorTextBox.Text = string.Join(Environment.NewLine, Errors);
        }

        private void BuildButton_Click(object sender, EventArgs e)
        {
            BuildModel();
        }
    }


}
