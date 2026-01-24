
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
    //TODO: RSDN
    public partial class MainForm : Form
    {
        /// <summary>
        /// Минимальный размер окна
        /// </summary>
        private const int _minWidthForm = 380;
		
        /// <summary>
        /// Размер окна, при котором скрываются ограничения
        /// </summary>
        private const int _hideLimitsAt = 450;
		
        /// <summary>
        /// Размер окна, на который магнитится окно при скрытии малой картинки
        /// </summary>
        private const int _firstAnchorForm = 550;
		
        /// <summary>
        /// Размер окна при котором скрывается малый чертёж
        /// </summary>
        private const int _hideSmallAt = 650;
		
        /// <summary>
        /// Размер окна при котором скрывается большой чертёж
        /// </summary>
        private const int _hideBigAt = 1000;
		
        /// <summary>
        /// Строитель модели
        /// </summary>
        private Builder _builder;

        /// <summary>
        /// РћР±СЂР°Р±РѕС‚С‡РёРє РїР°СЂР°РјРµС‚СЂРѕРІ РјРѕРґРµР»Рё
        /// </summary>
        private Parameters _parameters;

        /// <summary>
        /// РџСЂРёРІСЏР·Р°РЅРЅС‹Рµ РїР°СЂР°РјРµС‚СЂС‹ РґР»СЏ TextBox
        /// </summary>
        private Dictionary<ParametersTypes, TextBox> _textboxByParameter;

        /// <summary>
        /// РЎРїРёСЃРѕРє Р°РєС‚РёРІРЅС‹С… РѕС€РёР±РѕРє
        /// </summary>
        private Dictionary<string, List<ParametersTypes>> _activeErrors;

        /// <summary>
        /// РўРµРєСЃС‚РѕРІС‹Р№ СЌРєРІРёРІР°Р»РµРЅС‚ ParametersTypes РЅР° СЂСѓСЃСЃРєРѕРј
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
        /// РћР±СЂР°Р±РѕС‚С‡РёРє СЃРѕР±С‹С‚РёСЏ РїРѕСЏРІР»РµРЅРёСЏ РѕС€РёР±РєРё
        /// </summary>
        /// <param name="sender">РСЃС‚РѕС‡РЅРёРє РѕС€РёР±РєРё</param>
        /// <param name="e">
        /// РЎРѕРѕР±С‰РµРЅРёРµ РѕС€РёР±РєРё СЃРѕ СЃРїРёСЃРєРѕРј РїРµСЂРµРјРµРЅРЅС‹С… СЃ РѕС€РёР±РєРѕР№
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
        /// РћР±СЂР°Р±РѕС‚С‡РёРє СЃРѕР±С‹С‚РёСЏ РѕР±РЅРѕРІР»РµРЅРёСЏ РѕС€РёР±РѕРє РїР°СЂР°РјРµС‚СЂР° 
        /// РґР»СЏ РѕС‡РёСЃС‚РєРё СЃРІСЏР·Р°РЅРЅС‹С… РѕС€РёР±РѕРє
        /// </summary>
        /// <param name="e">РћР±РЅРѕРІР»С‘РЅРЅС‹Р№ РїР°СЂР°РјРµС‚СЂ</param>
        private void ParameterUpdateErrors(object sender, ParametersTypes e)
        {
            // РќР°С…РѕРґРёРј СЃРѕРѕР±С‰РµРЅРёСЏ, СЃРІСЏР·Р°РЅРЅС‹Рµ СЃ СЌС‚РёРј РїР°СЂР°РјРµС‚СЂРѕРј
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
        /// РћР±СЂР°Р±РѕС‚С‡РёРє СЃРѕР±С‹С‚РёСЏ РѕР±РЅРѕРІР»РµРЅРёСЏ Р·РЅР°С‡РµРЅРёСЏ РїР°СЂР°РјРµС‚СЂР°
        /// </summary>
        /// <param name="e">РћР±РЅРѕРІР»С‘РЅРЅС‹Р№ РїР°СЂР°РјРµС‚СЂ</param>
        private void ParameterUpdateValue(object sender, ParametersTypes e)
        {
            _textboxByParameter[e].Text =
                _parameters.GetParameter(e).ToString();
        }

        /// <summary>
        /// РЎРѕР±С‹С‚РёРµ РІРІРµРґРµРЅРёСЏ РїР°СЂР°РјРµС‚СЂР° РІ РїРѕР»Рµ
        /// </summary>
        private void ParameterEntered(object sender, KeyEventArgs e)
        {
            var textBox = (TextBox)sender;

            var parameter =
                _textboxByParameter.First(x => x.Value == textBox).Key;

            if (!_textboxByParameter.ContainsValue(textBox))
            {
                IsErrorAppered(this, new ErrorArgs(
                    "РџР°СЂР°РјРµС‚СЂ РЅРµ РґРѕР»Р¶РµРЅ Р±С‹С‚СЊ РїСѓСЃС‚С‹Рј",
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
                    "РќРµРєРѕСЂСЂРµРєС‚РЅС‹Р№ С„РѕСЂРјР°С‚ С‡РёСЃР»Р°",
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
        /// РќР°С‡Р°С‚СЊ РїРѕСЃС‚СЂРѕРµРЅРёРµ РјРѕРґРµР»Рё
        /// </summary>
        private void BuildModel(object sender, EventArgs e)
        {
            if (_activeErrors.Count != 0)
            {
                MessageBox.Show("РЎРЅР°С‡Р°Р»Р° РёСЃРїСЂР°РІСЊС‚Рµ РІСЃРµ РѕС€РёР±РєРё");
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
                _builder.Build(outputParameters, _parameters.IsMultiFlight);
            }
            catch (BuildException ex)
            {
                MessageBox.Show($"РћС€РёР±РєР° РїСЂРё РїРѕСЃС‚СЂРѕРµРЅРёРё РјРѕРґРµР»Рё: {ex.Message}");
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

            if (this.Width <= _hideSmallAt && this.Width > _hideLimitsAt)
            {
                newWidth = _firstAnchorForm;

                this.Size = new Size(newWidth, this.Height);

            }
            if (this.Width <= _hideLimitsAt)
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
            //TODO: refactor
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
        /// РРЅРёС†РёР°Р»РёР·РёСЂСѓРµС‚ Р·Р°РїРѕР»РЅРµРЅРёРµ _localization
        /// </summary>
        private void InitializeLocalization()
        {
            _localization = new Dictionary<ParametersTypes, string>
            {
                { ParametersTypes.Height, 
                    "Р’С‹СЃРѕС‚Р° РјР°СЂС€Р°" },
                { ParametersTypes.Length, 
                    "Р”Р»РёРЅР° РїСЂРѕР»С‘С‚Р°" },
                { ParametersTypes.PlatformLengthUp, 
                    "Р”Р»РёРЅР° РІРµСЂС…РЅРµР№ РїР»Р°С‚С„РѕСЂРјС‹" },
                { ParametersTypes.PlatformLengthDown, 
                    "Р”Р»РёРЅР° РЅРёР¶РЅРµР№ РїР»Р°С‚С„РѕСЂРјС‹" },
                { ParametersTypes.PlatformHeight, 
                    "РўРѕР»С‰РёРЅР° РїР»Р°С‚С„РѕСЂРјС‹" },
                { ParametersTypes.StepAmount, 
                    "РљРѕР»РёС‡РµСЃС‚РІРѕ СЃС‚СѓРїРµРЅРµР№" },
                { ParametersTypes.StepHeight, 
                    "Р’С‹СЃРѕС‚Р° СЃС‚СѓРїРµРЅРё" },
                { ParametersTypes.StepProjectionHeight, 
                    "РЁРёСЂРёРЅР° РІС‹СЃС‚СѓРїР°" },
                { ParametersTypes.StepProjectionLength, 
                    "Р“Р»СѓР±РёРЅР° РІС‹СЃС‚СѓРїР°" },
                { ParametersTypes.Width, 
                    "РЁРёСЂРёРЅР° РјР°СЂС€Р°" }
            };
        }

        /// <summary>
        /// РџРѕРґСЃРІРµС‚РёС‚СЊ textBox
        /// </summary>
        /// <param name="textBox">Р¦РµР»РµРІРѕР№ textBox</param>
        private void MarkTextboxAsError(TextBox textBox)
        {
            textBox.BackColor = Color.LightCoral;
        }

        /// <summary>
        /// РЈР±СЂР°С‚СЊ РїРѕРґСЃРІРµС‚РєСѓ РґР»СЏ textBox
        /// </summary>
        /// <param name="textBox">Р¦РµР»РµРІРѕР№ textBox</param>
        private void ClearTextboxError(TextBox textBox)
        {
            textBox.BackColor = SystemColors.Window;
        }

        /// <summary>
        /// РћР±РЅРѕРІРёС‚СЊ СЃРїРёСЃРѕРє РѕС€РёР±РѕРє
        /// </summary>
        private void UpdateErrorBox()
        {
            if (_activeErrors.Count == 0)
            {
                ErrorTextBox.Text = "";
                return;
            }

            // Р‘РµСЂС‘Рј Р’РЎР• СЃС‚СЂРѕРєРё РѕС€РёР±РѕРє РёР· РІСЃРµС… РїР°СЂР°РјРµС‚СЂРѕРІ
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
