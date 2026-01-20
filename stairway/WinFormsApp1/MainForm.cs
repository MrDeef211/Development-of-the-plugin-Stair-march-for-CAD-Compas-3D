using Accessibility;
using Builders;
using Model;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Security.Cryptography;
using System.Text.Json;


namespace UI
{
    public partial class MainForm : Form
    {
        /// <summary>
        /// Минимальный размер окна
        /// </summary>
        private const int _minWidthForm = 380;
        /// <summary>
        /// Размер окна, при котором скрываются ограничения
        /// </summary>
        private const int _hideLimitsAt = 400;
        /// <summary>
        /// Размер окна при котором скрывается малый чертёж
        /// </summary>
        private const int _hideSmallAt = 550;
        /// <summary>
        /// Размер окна при котором скрывается большой чертёж
        /// </summary>
        private const int _hideBigAt = 1000;
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

            if (TryLoad(out var loaded))
            {
                _parameters = loaded;
            }

            _parameters.ErrorMessageEvent += IsErrorAppered;
            _parameters.UpdateParameterErrorsEvent += ParameterUpdateErrors;
            _parameters.UpdateParameterValueEvent += ParameterUpdateValue;

            _parameters.FullUpdateParameters();
            IsMultiFlightСheckBox.Checked = _parameters.IsMultiFlight;
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
        /// Событие изменение чекбокса строительства пролёта
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void IsMultiFlightСheckBox_MouseClick(
            object sender,
            MouseEventArgs e)
        {
            _parameters.IsMultiFlight = IsMultiFlightСheckBox.Checked;
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
        /// Событие изменение размеров окна
        /// </summary>
        private void MainForm_Resize(object sender, EventArgs e)
        {
            int w = this.ClientSize.Width;

            double size = PicturesSplitContainer.Size.Width;

            if (size > 100)
                PicturesSplitContainer.SplitterDistance = (int)(size / 2.5);

            MainModelPictureBox.Visible = w >= _hideBigAt;
            PicturesSplitContainer.Panel2Collapsed = w < _hideBigAt;
            MiniModelPictureBox.Visible = w >= _hideSmallAt;
            PicturesSplitContainer.Panel1Collapsed = w < _hideSmallAt;
            LimitsPanel.Visible = w >= _hideLimitsAt;

            this.MinimumSize = new Size(_minWidthForm, this.MinimumSize.Height);
        }

        /// <summary>
        /// Событие окончания изменения размеров окна
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_ResizeEnd(object sender, EventArgs e)
        {


            int snapThreshold = 100;
            int newWidth;

            if (Math.Abs(this.Width - _hideSmallAt) <= snapThreshold)
            {
                newWidth = _hideSmallAt;

                this.Size = new Size(newWidth, this.Height);

            }
            if (this.Width - _hideSmallAt < -snapThreshold)
            {
                newWidth = _minWidthForm;

                this.Size = new Size(newWidth, this.Height);
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
                Save(_parameters);
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
                    WidthTextBox},
                { ParametersTypes.FloorsCount,
                    FloorsCountTextBox}

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
                    "Ширина марша" },
                { ParametersTypes.FloorsCount,
                    "Количество этажей"}
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
        public void Save(Parameters parameters)
        {
            var snapshot = parameters.CreateSnapshot();
            File.WriteAllText(_path, JsonSerializer.Serialize(snapshot));
        }

        /// <summary>
        /// Загрузить параметры из json
        /// </summary>
        /// <param name="parameters">Список параметров</param>
        /// <returns>Успех</returns>
        public bool TryLoad(out Parameters parameters)
        {
            parameters = null;

            if (!File.Exists(_path))
                return false;

            parameters = new Parameters();

            var json = File.ReadAllText(_path);
            var snapshot = JsonSerializer.Deserialize<ParametersSnapshot>(json);
            parameters.RestoreFromSnapshot(snapshot);
            return parameters != null;
        }

    }


}
