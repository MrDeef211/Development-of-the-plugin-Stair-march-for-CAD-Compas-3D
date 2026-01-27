using Kompas6API5;
using Kompas6Constants;
using Kompas6Constants3D;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;



namespace Builders
{
    //TODO: RSDN (вроде исправил)
    /// <summary>
    /// Класс-обёртка для работы с API5 Компас-3D
    /// </summary>
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
        /// <exception cref="BuildException">Компас не запущен</exception>
        public void OpenCAD(KompasObject kompas)
        {
            //TODO: {} (исправил)
            if (kompas == null)
            {
                throw new BuildException("Компас не запущен. " +
                    "Вызови CreateCADWindow().");
            }

            _kompas = kompas;

            // создаём 3D документ
            if (_doc3D  == null)
            {
                _doc3D = (ksDocument3D)_kompas.Document3D();
                _doc3D.Create();
            }
            _doc3D.SetActive();
        }

        /// <summary>
        /// Инициализирован ли компас
        /// </summary>
        public bool KompasIsDefined()
        {
            if (_kompas == null)
                return false;
            else return true;
        }

        /// <summary>
        /// Создание нового окна компаса
        /// </summary>
        /// <exception cref="BuildException">
        /// Не удалось создать объект KompasObject
        /// </exception>
        public void CreateCADWindow()
        {
            if (_kompas != null)
                return;

            _kompas = (KompasObject)Activator.CreateInstance(
                Type.GetTypeFromProgID("KOMPAS.Application.5"));

            if (_kompas == null)
                throw new BuildException("Не удалось создать " +
                    "объект KompasObject");

            _kompas.Visible = true;
            _kompas.ActivateControllerAPI();

        }

        /// <summary>
        /// Открытие существующего файла
        /// </summary>
        /// <param name="path">Путь к файлу</param>
        /// <param name="readOnly">Файл только для чтения</param>
        /// <param name="visible">Видимый</param>
        /// <exception cref="BuildException">
        /// Компас не запущен или не удалось открыть файл
        /// </exception>
        public void OpenFile(string path, bool readOnly, bool visible)
        {
            //TODO: {} (исправил)
            if (_kompas == null)
            {
                throw new BuildException(
                    "Компас не запущен. Вызови OpenCAD().");
            }

            _kompas.Visible = visible;

            ksDocument3D doc = (ksDocument3D)_kompas.Document3D();
            //TODO: rename
            bool openSucess = doc.Open(path, readOnly);

            //TODO: {} (исправил)
            if (!openSucess)
            {
                throw new BuildException(
                    $"Не удалось открыть файл: {path}");
            }

            _doc3D = doc;
            _doc3D.SetActive();
        }

        /// <summary>
        /// Создать новый файл
        /// </summary>
        /// <exception cref="BuildException">
        /// Компас не запущен или не удалось получить ksPart
        /// </exception>
        public void CreateFile()
        {
            //TODO: {} (исправил)
            if (_kompas == null)
            {
                throw new BuildException(
                    "Компас не запущен. Вызови OpenCAD().");
            }

            _doc3D = (ksDocument3D)_kompas.Document3D();
            _doc3D.Create();
            _doc3D.SetActive();

            // получаем Part верхнего уровня
            _part = (ksPart)_doc3D.GetPart((short)Part_Type.pTop_Part);

            //TODO: {} (исправил)
            if (_part == null)
            {
                throw new BuildException("Не удалось получить " +
                    "ksPart для нового документа.");
            }
        }

        /// <summary>
        /// Закрыть текущий файл
        /// </summary>
        public void CloseCurrentFile()
        {
            if (_doc3D != null)
            {
                _doc3D.close();
                _doc3D = null;
            }
        }

        /// <summary>
        /// Создать эскиз
        /// </summary>
        public void CreateSketch()
        {
            _activeSketch = 
                (ksEntity)_part.NewEntity((short)Obj3dType.o3d_sketch);
            _sketchDefinition = 
                (ksSketchDefinition)_activeSketch.GetDefinition();

            _sketchDefinition.SetPlane((ksEntity)_part.
                GetDefaultEntity((short)Obj3dType.o3d_planeXOZ));

            _activeSketch.Create();

            _sketchEdit = 
                (ksDocument2D)_sketchDefinition.BeginEdit();
        }

        /// <summary>
        /// Создать прямую линию
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <returns></returns>
        /// <exception cref="BuildException">
        /// Активный эскиз не установлен
        /// </exception>
        public long Createline( double x1, double y1,
            double x2, double y2)
        {
            //TODO: {} (исправил)
            if (_sketchEdit == null)
            {
                throw new BuildException("Активный эскиз не установлен");
            }

            return _sketchEdit.ksLineSeg(x1, y1, x2, y2, 1);
        }

        /// <summary>
        /// Выдавливание эскиза
        /// </summary>
        /// <param name="direction">Направление</param>
        /// <param name="type">Тип</param>
        /// <param name="depth">Глубина</param>
        /// <param name="bothDirections">Симметрично</param>
        /// <exception cref="BuildException">
        /// Эскиз еще не создан, не удалось создать сущность выдавливания
        /// или не удалось получить ksBossExtrusionDefinition
        /// </exception>
        public void Extrusion(bool direction, short type,
            double depth, bool bothDirections)
        {
            //TODO: {} (исправил)
            if (_activeSketch == null)
            {
                throw new BuildException("Эскиз еще не создан.");
            }

            // Завершаем редактирование эскиза
            _sketchDefinition.EndEdit();

            // Создаем сущность выдавливания
            ksEntity extr = 
                (ksEntity)_part.NewEntity(
                    (short)Obj3dType.o3d_bossExtrusion);

            //TODO: {} (исправил)
            if (extr == null)
            {
                throw new BuildException("Не удалось создать " +
                    "сущность выдавливания.");
            }

            // Получаем определение
            ksBossExtrusionDefinition extrDef =
                (ksBossExtrusionDefinition)extr.GetDefinition();

            //TODO: {} (исправил)
            if (extrDef == null)
            {
                throw new BuildException("Не удалось получить " +
                    "ksBossExtrusionDefinition.");
            }

            // Привязываем эскиз
            extrDef.SetSketch(_activeSketch);


            if (bothDirections)
            {
                extrDef.directionType = (short)Direction_Type.dtBoth;

                extrDef.SetSideParam(true, type, depth / 2, 0, false);
                extrDef.SetSideParam(false, type, depth / 2, 0, false);
            }
            else
            {
                extrDef.directionType = direction
                    ? (short)Direction_Type.dtNormal
                    : (short)Direction_Type.dtReverse;


                extrDef.SetSideParam(
                    direction,   
                    type,
                    depth,
                    0,
                    false
                );
            }

            extr.Create();

            extr.Update();
        }

        /// <summary>
        /// Масштабирование окна для вида всей модели
        /// </summary>
        public void DocumentZoomOut()
        {
            _doc3D.ZoomPrevNextOrAll(1);
        }


    }
}
