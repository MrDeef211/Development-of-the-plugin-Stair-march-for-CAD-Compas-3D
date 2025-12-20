using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Kompas6API5;
using Kompas6Constants;
using Kompas6Constants3D;
using KompasAPI7;


namespace Builders
{
    internal class Wrapper
    {
        /// <summary>
        /// Текущий объект компаса API5
        /// </summary>
        private KompasObject _kompas;
        /// <summary>
        /// Текущий документ
        /// </summary>
        private ksDocument3D _doc3D;
        /// <summary>
        /// Текущая деталь
        /// </summary>
        private ksPart _part;   
        /// <summary>
        /// Основной эскиз
        /// </summary>
        private ksEntity _activeSketch;
        /// <summary>
        /// Редактор эскиза
        /// </summary>
        private ksSketchDefinition _sketchDefinition;
        /// <summary>
        /// Редактируемый эскиз
        /// </summary>
        private ksDocument2D _sketchEdit;

        /// <summary>
        /// Открыть существующий объект компаса
        /// </summary>
        /// <param name="kompas">Объект компаса</param>
        /// <exception cref="Exception"></exception>
        public void OpenCAD(KompasObject kompas)
        {
            if (kompas == null)
                throw new Exception("Компас не запущен. Вызови CreateCADWindow().");

            _kompas = kompas;

            // создаём 3D документ
            if (_doc3D  == null)
            {
                // Потом сделать проверку лучше
                // Потом сделать проверку лучше
                // Потом сделать проверку лучше
                // Потом сделать проверку лучше
                // Потом сделать проверку лучше
                _doc3D = (ksDocument3D)_kompas.Document3D();
                _doc3D.Create();
            }
            _doc3D.SetActive();
        }

        public bool KompasIsDefined()
        {
            if (_kompas == null)
                return false;
            else return true;
        }

        /// <summary>
        /// Создание нового окна компаса
        /// </summary>
        /// <exception cref="Exception"></exception>
        public void CreateCADWindow()
        {
            if (_kompas != null)
                return;

            _kompas = (KompasObject)Activator.CreateInstance(
                Type.GetTypeFromProgID("KOMPAS.Application.5"));

            if (_kompas == null)
                throw new Exception("Не удалось создать объект KompasObject");

            _kompas.Visible = true;
            _kompas.ActivateControllerAPI();

        }

        /// <summary>
        /// Открытие существующего файла
        /// </summary>
        /// <param name="path">Путь к файлу</param>
        /// <param name="readOnly">Файл только для чтения</param>
        /// <param name="visible">Видимый</param>
        /// <exception cref="Exception"></exception>
        public void OpenFile(string path, bool readOnly, bool visible)
        {
            if (_kompas == null)
                throw new Exception("Компас не запущен. Вызови OpenCAD().");

            _kompas.Visible = visible;

            ksDocument3D doc = (ksDocument3D)_kompas.Document3D();
            bool ok = doc.Open(path, readOnly);

            if (!ok)
                throw new Exception($"Не удалось открыть файл: {path}");

            _doc3D = doc;
            _doc3D.SetActive();
        }

        /// <summary>
        /// Создать новый файл
        /// </summary>
        /// <exception cref="Exception"></exception>
        public void CreateFile()
        {
            if (_kompas == null)
                throw new Exception("Компас не запущен. Вызови OpenCAD().");

            _doc3D = (ksDocument3D)_kompas.Document3D();
            _doc3D.Create();
            _doc3D.SetActive();

            // получаем Part верхнего уровня
            _part = (ksPart)_doc3D.GetPart((short)Part_Type.pTop_Part);

            if (_part == null)
                throw new Exception("Не удалось получить ksPart для нового документа.");
        }

        /// <summary>
        /// Создать эскиз
        /// </summary>
        public void CreateSketch()
        {
            _activeSketch = (ksEntity)_part.NewEntity((short)Obj3dType.o3d_sketch);
            _sketchDefinition = (ksSketchDefinition)_activeSketch.GetDefinition();
            _sketchDefinition.SetPlane((ksEntity)_part.GetDefaultEntity((short)Obj3dType.o3d_planeXOZ));
            _activeSketch.Create();
            _sketchEdit = (ksDocument2D)_sketchDefinition.BeginEdit();
        }

        /// <summary>
        /// Создать прямую линию
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public long Createline( double x1, double y1,
            double x2, double y2)
        {
            if (_sketchEdit == null)
                throw new Exception("Активный эскиз не установлен");

            return _sketchEdit.ksLineSeg(x1, y1, x2, y2, 1);
        }

        /// <summary>
        /// Выдавливание эскиза
        /// </summary>
        /// <param name="directionNormal">Направление</param>
        /// <param name="type">Тип</param>
        /// <param name="depth">Глубина</param>
        /// <param name="bothDirections">Симметрично</param>
        /// <exception cref="Exception"></exception>
        public void Extrusion(bool directionNormal, short type,
            double depth, bool bothDirections)
        {
            if (_activeSketch == null)
                throw new Exception("Эскиз еще не создан.");

            // Завершаем редактирование эскиза
            _sketchDefinition.EndEdit();

            // Создаем сущность выдавливания
            ksEntity extr = (ksEntity)_part.NewEntity((short)Obj3dType.o3d_bossExtrusion);
            if (extr == null)
                throw new Exception("Не удалось создать сущность выдавливания.");

            // Получаем определение
            ksBossExtrusionDefinition extrDef =
                (ksBossExtrusionDefinition)extr.GetDefinition();

            if (extrDef == null)
                throw new Exception("Не удалось получить ksBossExtrusionDefinition.");

            // Привязываем эскиз
            extrDef.SetSketch(_activeSketch);

            // Выставляем параметры стороны выдавливания
            // directionNormal = true  → по нормали
            // type = тип выдавливания (0..4)
            // depth = глубина

            extrDef.SetSideParam(
                side1: directionNormal,
                type: type,
                depth: depth,
                draftValue: 0,
                draftOutward: false
            );

            // Создаём операцию
            extr.Create();

            extr.Update();
        }


    }
}
