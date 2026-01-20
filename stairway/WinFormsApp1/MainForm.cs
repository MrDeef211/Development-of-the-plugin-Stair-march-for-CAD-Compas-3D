using Builders;
using Model;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Reflection.Metadata;
using System.Security.Cryptography;
using System.Text.Json;


namespace UI
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
        /// <summary>
        /// Путь для сохранения json с параметрами
        /// </summary>
        private readonly string _path =
            Path.Combine(AppContext.BaseDirectory, "parameters.json");

        public MainForm()
        {
            InitializeComponent();
            InitializeUIBindings();
            InitializeLocalization();

            _builder = new Builder();
            _parameters = new Parameters();
            _activeErrors = new Dictionary<string, List<ParametersTypes>>();

            _parameters.ErrorMessageEvent += IsErrorAppered;
            _parameters.UpdateParameterErrorsEvent += ParameterUpdateErrors;
            _parameters.UpdateParameterValueEvent += ParameterUpdateValue;

            _parameters.FullUpdateParameters();
        }

        /// <summary>
        /// Обработчик события появления ошибки
        /// </summary>
        /// <param name="sender">Источник ошибки</param>
        /// <param name="e">
        /// Сообщение ошибки со списком переменных с ошибкой
        /// </param>
        private void IsErrorAppered(object sender, ErrorArgs e)
        {
            string newError;
            if (e.ParametersList.Count == 1)
                newError = _localization[e.ParametersList[0]] + e.Message;
            else
                newError = e.Message;
            if (!_activeErrors.ContainsKey(newError))
                _activeErrors[newError] =
                    new List<ParametersTypes>(e.ParametersList);

            foreach (var parameter in e.ParametersList)
            {
                MarkTextboxAsError(_textboxByParameter[parameter]);
            }



            UpdateErrorBox();
        }

        /// <summary>
        /// Обработчик события обновления ошибок параметра 
        /// для очистки связанных ошибок
        /// </summary>
        /// <param name="e">Обновлённый параметр</param>
        private void ParameterUpdateErrors(object sender, ParametersTypes e)
        {
            // Находим сообщения, связанные с этим параметром
            var messagesToRemove = _activeErrors
                .Where(kvp => kvp.Value.Contains(e))
                .Select(kvp => kvp.Key)
                .ToList();

            foreach (var message in messagesToRemove)
            {
                foreach (var p in _activeErrors[message])
                {
                    ClearTextboxError(_textboxByParameter[p]);
                }

                _activeErrors.Remove(message);
            }

            UpdateErrorBox();
        }

        /// <summary>
        /// Обработчик события обновления значения параметра
        /// </summary>
        /// <param name="e">Обновлённый параметр</param>
        private void ParameterUpdateValue(object sender, ParametersTypes e)
        {
            _textboxByParameter[e].Text =
                _parameters.GetParameter(e).ToString();
        }

        /// <summary>
        /// Событие введения параметра в поле
        /// </summary>
        private void ParameterEntered(object sender, KeyEventArgs e)
        {
            var textBox = (TextBox)sender;

            var parameter =
                _textboxByParameter.First(x => x.Value == textBox).Key;

            if (!_textboxByParameter.ContainsValue(textBox))
            {
                IsErrorAppered(this, new ErrorArgs(
                    "Параметр не должен быть пустым",
                    new List<ParametersTypes> { parameter }));
                return;
            }

            if (double.TryParse(textBox.Text, out double value))
            {
                _parameters.SetParameter(parameter, value);
            }
            else
            {
                IsErrorAppered(this, new ErrorArgs(
                    "Некорректный формат числа",
                    new List<ParametersTypes> { parameter }));
            }
        }



        /// <summary>
        /// Начать построение модели
        /// </summary>
        private void BuildModel(object sender, EventArgs e)
        {
            if (_activeErrors.Count != 0)
            {
                MessageBox.Show("Сначала исправьте все ошибки");
                return;
            }
            var inputParameters = _parameters.GetParameters();

            var outputParameters =
                new Dictionary<ParametersTypes, double>();

            foreach (var parameter in inputParameters.Keys)
                outputParameters.Add(
                    parameter,
                    inputParameters[parameter].Value);
            try
            {
                _builder.Build(outputParameters);
            }
            catch (BuildException ex)
            {
                MessageBox.Show($"Ошибка при построении модели: {ex.Message}");
            }
        }

        /// <summary>
        /// Закрытие формы
        /// </summary>
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_activeErrors.Count != 0)
            {
                DialogResult result = MessageBox.Show(
                    "Вы уверены, что хотите выйти? \n" +
                    "У вас есть ошибки, поэтому данные не сохранятся.",    
                    "Предупреждение",                         
                    MessageBoxButtons.OKCancel,               
                    MessageBoxIcon.Warning                    
                );

                if (result == DialogResult.Cancel)
                {
                    e.Cancel = true;
                }
            }    
            else
            {
                SaveParameters(_parameters.GetParameters());
            }
        }

        /// <summary>
        /// Инициализирует привязку TextBox и 
        /// соответствующие им ParametersTypes
        /// </summary>
        private void InitializeUIBindings()
        {
            _textboxByParameter = new Dictionary<ParametersTypes, TextBox>
            {

            { ParametersTypes.Height,
                    HeightTextBox},
            { ParametersTypes.Length,
                    LengthTextBox},
            { ParametersTypes.PlatformLengthUp,
                    PlatformLengthUpTextBox},
            { ParametersTypes.PlatformLengthDown,
                    PlatformLengthDownTextBox},
            { ParametersTypes.PlatformHeight,
                    PlatformHeightTextBox},
            { ParametersTypes.StepAmount,
                    StepAmountTextBox},
            { ParametersTypes.StepHeight,
                    StepHeightTextBox},
            { ParametersTypes.StepProjectionHeight,
                    StepProtjectionHeightTextBox},
            { ParametersTypes.StepProjectionLength,
                    StepProjectionLengthTextBox},
            { ParametersTypes.Width,
                    WidthTextBox}

            };
        }

        /// <summary>
        /// Инициализирует заполнение _localization
        /// </summary>
        private void InitializeLocalization()
        {
            _localization = new Dictionary<ParametersTypes, string>
            {
                { ParametersTypes.Height,
                    "Высота марша" },
                { ParametersTypes.Length,
                    "Длина пролёта" },
                { ParametersTypes.PlatformLengthUp,
                    "Длина верхней платформы" },
                { ParametersTypes.PlatformLengthDown,
                    "Длина нижней платформы" },
                { ParametersTypes.PlatformHeight,
                    "Толщина платформы" },
                { ParametersTypes.StepAmount,
                    "Количество ступеней" },
                { ParametersTypes.StepHeight,
                    "Высота ступени" },
                { ParametersTypes.StepProjectionHeight,
                    "Ширина выступа" },
                { ParametersTypes.StepProjectionLength,
                    "Глубина выступа" },
                { ParametersTypes.Width,
                    "Ширина марша" }
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

        /// <summary>
        /// Сохранить некоторый список параметров в json
        /// </summary>
        /// <param name="parameters">Список параметров</param>
        private void SaveParameters(List<Model.Parameter> parameters)
        {
            var json = JsonSerializer.Serialize(parameters);
            File.WriteAllText(_path, json);
        }

        /// <summary>
        /// Загрузить параметры из json
        /// </summary>
        /// <param name="parameters">Список параметров</param>
        /// <returns>Успех</returns>
        public bool TryLoad(out List<Model.Parameter> parameters)
        {
            parameters = null;

            if (!File.Exists(_path))
                return false;

            var json = File.ReadAllText(_path);
            parameters =
                JsonSerializer.Deserialize<List<Model.Parameter>>(json);
            return parameters != null;
        }

    }


}
