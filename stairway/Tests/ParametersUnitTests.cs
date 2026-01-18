using Model;
using NUnit.Framework;

namespace Tests
{
	[TestFixture]
	public class ParametersUnitTests
	{
		private Parameters Create()
		{
			return new Parameters();
		}

		[Test]
		[Description("Проверка инициализации параметра " +
            "вне допустимого диапазона")]
		public void CreateOutOfRangeParameter()
		{
			Assert.Throws<Exception>(() =>
				new Parameter(ParametersTypes.Height, 8000, 500, 32000));

			Assert.Throws<Exception>(() =>
				new Parameter(ParametersTypes.Height, 8000, 500, 0));
		}

		[Test]
		[Description("Проверка инициализации словаря параметров")]
		public void ConstructorInitializesAllParameters()
		{
			var parameters = Create();
			var dict = parameters.GetParameters();

			Assert.That(dict, Is.Not.Null);
			Assert.That(dict.Count, Is.GreaterThan(0));
			Assert.That(dict.Keys, Does.Contain(ParametersTypes.Height));
		}

		[TestCase(ParametersTypes.Height, 3200)]
		[TestCase(ParametersTypes.Length, 3200)]
		[TestCase(ParametersTypes.StepProjectionLength, 20)]
		[TestCase(ParametersTypes.StepAmount, 20)]
		[TestCase(ParametersTypes.StepHeight, 160)]
		[TestCase(ParametersTypes.Width, 1000)]
		[Description("Проверка передачи параметра")]
		public void SetParameterChangesValue(
            ParametersTypes parameter, 
            double value)
		{
			var parameters = Create();

			parameters.SetParameter(parameter, value);
			double currentValue = parameters.GetParameter(parameter);

			Assert.That(currentValue, Is.EqualTo(value));
		}

		[Test]
		[Description("Проверка передачи параметра вне диапазона")]
		public void SetParameterOutOfRangeRaisesErrorEvent()
		{
			var parameters = Create();
			ErrorArgs? error = null;

			parameters.ErrorMessageEvent += (s, e) => error = e;

			parameters.SetParameter(ParametersTypes.Length, 500);
			parameters.SetParameter(ParametersTypes.StepAmount, 2);
			parameters.SetParameter(ParametersTypes.Height, 400);

			Assert.That(error, Is.Not.Null);
			Assert.That(
                error.ParametersList, 
                Does.Contain(ParametersTypes.Height));
		}

		[Test]
		[Description("Проверка передачи неправильного количества ступеней")]
		public void SetStepAmountOutOfRangeRaisesErrorEvent()
		{
			var parameters = Create();
			ErrorArgs? error = null;

			parameters.ErrorMessageEvent += (s, e) => error = e;

			parameters.SetParameter(ParametersTypes.StepAmount, 100);

			Assert.That(error, Is.Not.Null);
			Assert.That(
                error.ParametersList, 
                Does.Contain(ParametersTypes.StepAmount));
		}

		[Test]
		[Description("Проверка передачи неправильного выступа ступени")]
		public void SetStepProjectionOutOfRangeRaisesErrorEvent()
		{
			var parameters = Create();
			ErrorArgs? error = null;

			parameters.ErrorMessageEvent += (s, e) => error = e;

			parameters.SetParameter(
                ParametersTypes.StepProjectionHeight, 
                90);

			Assert.That(error, Is.Not.Null);
			Assert.That(
                error.ParametersList, 
                Does.Contain(ParametersTypes.StepProjectionHeight));
		}

		[Test]
		[Description("Проверка передачи " +
            "не целочисленного количества ступеней")]
		public void StepAmountNotIntegerRaisesError()
		{
			var parameters = Create();
			ErrorArgs? error = null;

			parameters.ErrorMessageEvent += (s, e) => error = e;

			parameters.SetParameter(ParametersTypes.StepAmount, 5.5);

			Assert.That(error, Is.Not.Null);
			Assert.That(
                error.ParametersList, 
                Does.Contain(ParametersTypes.StepAmount));
		}

		[Test]
		[Description("Проверка пересчета высоты ступени " +
            "при изменении высоты")]
		public void ChangingHeightRecalculatesStepHeight()
		{
			var parameters = Create();

			parameters.SetParameter(ParametersTypes.StepAmount, 10);
			parameters.SetParameter(ParametersTypes.Height, 2000);

			double stepHeight = parameters.GetParameter(
                ParametersTypes.StepHeight);

			Assert.That(stepHeight, Is.EqualTo(200));
		}

		[Test]
		[Description("Проверка пересчета высоты марша " +
            "при изменении высоты ступени")]
		public void ChangingStepHeightRecalculatesHeight()
		{
			var parameters = Create();

			parameters.SetParameter(ParametersTypes.StepAmount, 10);
			parameters.SetParameter(ParametersTypes.StepHeight, 180);

			double height = parameters.GetParameter(ParametersTypes.Height);

			Assert.That(height, Is.EqualTo(1800));
		}

		[Test]
		[Description("Проверка валидации угла марша")]
		public void StairAngleOutOfRangeRaisesError()
		{
			var parameters = new Parameters();
			var errors = new List<ErrorArgs>();

			parameters.ErrorMessageEvent += (s, e) => errors.Add(e);

			parameters.SetParameter(ParametersTypes.Height, 8000);
			parameters.SetParameter(ParametersTypes.Length, 1000);

			Assert.That(errors, Has.Some.Matches<ErrorArgs>(e =>
				e.ParametersList.Contains(ParametersTypes.Height) &&
				e.ParametersList.Contains(ParametersTypes.Length)));
		}

		[Test]
		[Description("Проверка обновления всех параметров")]
		public void FullUpdateParametersSendsAllParameters()
		{
			var parameters = Create();
			List<ParametersTypes>? updated = null;

			parameters.UpdateParametersEvent += (s, list) => updated = list;

			parameters.FullUpdateParameters();

			Assert.That(updated, Is.Not.Null);
			Assert.That(
                updated.Count, 
                Is.EqualTo(parameters.GetParameters().Count));
		}

        [TestCase (1000, 500, 1200)]
        [TestCase (1000, 500, 400)]
        [Description("Проверка создания значения параметра, " +
    "вне доступного диапазона")]
        public void CreateParameterValueOutOfRangeRaisesErrorEvent(
            double min, 
            double max, 
            double value)
        {
            Assert.Throws<Exception>(() =>
                new Parameter(
                    ParametersTypes.Width, 
                    min, 
                    max, 
                    value));
        }


        [Test]
        [Description("Проверка создания максимального значения параметра, " +
    "меньше минимального")]
        public void CreateParameterMaxLessThanMinRaisesErrorEvent()
        {
            Assert.Throws<Exception>(() =>
                new Parameter(
                    ParametersTypes.Width, 
                    500, 
                    1000, 
                    600));
        }

        [Test]
        [Description("Проверка передачи максимального значения параметра, " +
            "меньше минимального")]
        public void SetParameterMaxLessThanMinRaisesErrorEvent()
        {
            var parameter = new Parameter(
                ParametersTypes.Width, 
                1000, 
                500, 
                750);
            parameter.Max = 400;
            Assert.That(parameter.Max, Is.EqualTo(500));
        }

    }
}